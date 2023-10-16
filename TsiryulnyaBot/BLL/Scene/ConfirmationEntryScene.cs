using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TsiryulnyaBot.BLL.Constant;
using TsiryulnyaBot.BLL.Interface;
using TsiryulnyaBot.BLL.Service;

namespace TsiryulnyaBot.BLL.Scene
{
    public class ConfirmationEntryScene : IScene
    {
        private readonly ClientService _clientService;

        private readonly RecordClientService _recordClientService;

        private readonly RecordParameterClientService _recordParameterClientService;

        private readonly SpecialistService _specialistService;

        private readonly ServiceProvidedService _serviceProvidedService;

        private readonly WorkerShiftService _workerShiftService;

        public ConfirmationEntryScene(
            ClientService clientService,
            RecordClientService recordClientService,
            RecordParameterClientService recordParameterClientService,
            SpecialistService specialistService,
            ServiceProvidedService serviceProvidedService,
            WorkerShiftService workerShiftService) 
        {
            _clientService = clientService;
            _recordClientService = recordClientService;
            _recordParameterClientService = recordParameterClientService;
            _specialistService = specialistService;
            _serviceProvidedService = serviceProvidedService;
            _workerShiftService = workerShiftService;
        }

        public async Task Execute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var messages = new List<string>();

            var client = _clientService.Get(update);
            var recordClient = _recordClientService.Get(client);
            var recordParameters = _recordParameterClientService.Get(item => item.RecordClientId == recordClient.Id);

            var specialist = _specialistService.Get((Guid)recordParameters.Where(item => item.ParameterId == RecordParameterConstant.Specialist).FirstOrDefault().UuidValue);
            var workerShip = _workerShiftService.Get((Guid)recordParameters.Where(item => item.ParameterId == RecordParameterConstant.DateTime).FirstOrDefault().UuidValue);

            recordClient.StatusId = RecordStatusConstant.Confirmed;

            _recordClientService.Update(recordClient);

            messages.Add($"Вы записаны к мастеру {_clientService.Get(item => item.Id == specialist.ClientId).FirstOrDefault().Name}");

            foreach (var item in recordParameters.Where(item => item.ParameterId == RecordParameterConstant.Service))
            {
                messages.Add($"- {_serviceProvidedService.Get((Guid)item.UuidValue).Name}");
            }

            messages.Add($"\nНа {workerShip.Date} в {workerShip.Time}, до встречи!");

            // Добавить отправку уведомления специалисту
            await botClient.SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: string.Join("\n", messages),
                replyMarkup: new ReplyKeyboardRemove()
            );            
        }
    }
}
