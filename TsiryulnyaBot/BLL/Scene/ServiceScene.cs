using System.Globalization;
using Telegram.Bot;
using Telegram.Bot.Types;
using TsiryulnyaBot.BLL.Constant;
using TsiryulnyaBot.BLL.Interface;
using TsiryulnyaBot.BLL.Service;
using TsiryulnyaBot.DAL.Model;
using TsiryulnyaBot.UIL.Keyboard;

namespace TsiryulnyaBot.BLL.Scene
{
    public class ServiceScene : IScene
    {
        private readonly SpecialistAndServiceProvidedService _specialistAndServiceProvidedService;

        private readonly RecordParameterClientService _recordParameterClientService;

        private readonly ClientService _clientService;

        private readonly RecordClientService _recordClientService;

        private readonly CategoryService _categoryService;

        private readonly ServiceProvidedService _serviceProvidedService;

        public delegate void Handler();

        public event Handler? NextScene;

        public ServiceScene(
            SpecialistAndServiceProvidedService specialistAndServiceService,
            RecordParameterClientService recordParameterClientService,
            ClientService clientService,
            RecordClientService recordClientService,
            CategoryService categoryService,
            ServiceProvidedService serviceProvidedService)
        {
            _specialistAndServiceProvidedService = specialistAndServiceService;
            _recordParameterClientService = recordParameterClientService;
            _clientService = clientService;
            _recordClientService = recordClientService;
            _categoryService = categoryService;
            _serviceProvidedService = serviceProvidedService;
        }

        public async Task Execute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message != null)
            {
                if (update?.Message.Text == "Продолжить")
                {
                    NextScene?.Invoke();
                    return;
                }
            }

            if (update.CallbackQuery != null)
            {
                var record = _recordClientService.Get(_clientService.Get(client => client.TlgId == update.CallbackQuery.From.Id).FirstOrDefault());

                _recordParameterClientService.Add(new RecordParameterClient()
                {
                    ParameterId = RecordParameterConstant.Service,
                    RecordClientId = record.Id,
                    UuidValue = new Guid(update.CallbackQuery.Data)
                });

                //if (_recordParameterClientService.GetWhere(parameter => parameter.RecordClientId == record.Id)
                //    .Where(parameter => parameter.ParameterId == RecordParameterConstant.Service).Count() == 0)
                //{

                //}

                await botClient.SendTextMessageAsync(
                    chatId: update.CallbackQuery.Message!.Chat.Id,
                    text: "Подтвердите выбор услуги",
                    replyMarkup: SingleReplyKeyboardMarkup.Create("Продолжить"),
                    cancellationToken: cancellationToken
                );

                return;
            }

            var client = _clientService.Get(update);
            var recordClient = _recordClientService.Get(client);
            var recordParameter = _recordParameterClientService
                .Get(item => item.RecordClientId == recordClient.Id)
                .Where(item => item.ParameterId == RecordParameterConstant.Specialist)
                .FirstOrDefault();

            var messages = new List<Task<Message>>();

            foreach (var item in _specialistAndServiceProvidedService.GetWhere(item => item.SpecialistId == recordParameter!.UuidValue))
            {
                var id = item.Id;
                var price = item.Price;
                var service = _serviceProvidedService.Get((Guid)item.ServiceId).Name;
                var category = _categoryService.Get((Guid)item.CategoryId).Name;
                var textMessage = string.Format("{0}\n({1})\n{2} руб.", service, category.ToLower(), price);

                var message = botClient.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: textMessage,
                    replyMarkup: SingleInlineKeyboardButton.Create("Выбрать", item.ServiceId.ToString()),
                    cancellationToken: cancellationToken
                );

                messages.Add(message);
            }

            await Task.WhenAll(messages);
        }
    }
}
