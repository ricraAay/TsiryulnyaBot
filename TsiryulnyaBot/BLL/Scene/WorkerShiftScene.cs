using System.Globalization;
using System.Threading;
using System.Timers;
using Telegram.Bot;
using Telegram.Bot.Types;
using TsiryulnyaBot.BLL.Constant;
using TsiryulnyaBot.BLL.Interface;
using TsiryulnyaBot.BLL.Service;
using TsiryulnyaBot.DAL.Interface;
using TsiryulnyaBot.DAL.Model;
using TsiryulnyaBot.UIL.Keyboard;

namespace TsiryulnyaBot.BLL.Scene
{
    public class WorkerShiftScene : IScene
    {
        private readonly ClientService _clientService;

        private readonly WorkerShiftService _workerShiftService;

        private readonly RecordClientService _recordClientService;

        private readonly RecordParameterClientService _recordParameterClientService;

        public delegate void Handler();

        public event Handler? NextScene;

        public WorkerShiftScene(
            ClientService clientService,
            WorkerShiftService workerShiftService,
            RecordClientService recordClientService,
            RecordParameterClientService recordParameterClientService) 
        {
            _clientService = clientService;
            _workerShiftService = workerShiftService;
            _recordClientService = recordClientService;
            _recordParameterClientService = recordParameterClientService;
        }

        public async Task Execute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {           
            var client = _clientService.Get(update);
            var recordClient = _recordClientService.Get(client);
            var recordParameterClientSpecialist = _recordParameterClientService
                .Aggregate(item => item.RecordClientId == recordClient.Id, item => item.ParameterId == RecordParameterConstant.Specialist)
                .Single();

            var workerShift = _workerShiftService.Get(recordParameterClientSpecialist);

            if (update.Message != null)
            {
                if(update.Message.Text == "Подтвердить")
                {
                    NextScene?.Invoke();
                    return;
                }

                await botClient.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: "Выберите свободную дату:",
                    replyMarkup: DatePickerKeyboardButton.Create(
                        date: DateTime.Now, 
                        dateTimeFormatInfo: CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat, 
                        filter: workerShift.GroupBy(item => item.Date).Select((value) => (DateOnly)value.Key)
                    )
                );
            }

            if (update.CallbackQuery != null)
            {
                var data = update.CallbackQuery.Data;

                if (DateOnly.TryParseExact(data, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly date))
                {
                    await botClient.SendTextMessageAsync(
                        chatId: update.CallbackQuery.Message!.Chat.Id,
                        text: "Выберите свободное время:",
                        replyMarkup: TimePickerKeyboardButton.Create(workerShift.Where(item => item.Date == date).OrderBy(item => item.Time).ToList())
                    );
                }

                if (Guid.TryParse(data, out Guid workerShiftId))
                {
                    var recordParameterClientDateTime = _recordParameterClientService
                        .Aggregate(
                            item => item.ParameterId == RecordParameterConstant.DateTime, 
                            item => item.RecordClientId == recordClient.Id)
                        .FirstOrDefault();

                    if (recordParameterClientDateTime != null)
                    {
                        _recordParameterClientService.Delete(recordParameterClientDateTime);
                    }

                    _recordParameterClientService.Add(new RecordParameterClient()
                    {
                        ParameterId = RecordParameterConstant.DateTime,
                        RecordClientId = recordClient.Id,
                        UuidValue = workerShiftId
                    });

                    var selectedWorkerShift = _workerShiftService.Get(workerShiftId);
                    selectedWorkerShift.StatusId = WorkerShiftStatusConstant.Closed;

                    _workerShiftService.Update(selectedWorkerShift);

                    await botClient.SendTextMessageAsync(
                       chatId: update.CallbackQuery.Message!.Chat.Id,
                       text: "Подтвердите запись",
                       replyMarkup: SingleReplyKeyboardMarkup.Create("Подтвердить")
                   );
                }
            }
        }
    }
}
