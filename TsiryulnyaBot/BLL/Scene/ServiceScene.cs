﻿using Telegram.Bot;
using Telegram.Bot.Types;
using TsiryulnyaBot.BLL.Constant;
using TsiryulnyaBot.BLL.Interface;
using TsiryulnyaBot.BLL.Service;
using TsiryulnyaBot.DAL.Model;
using TsiryulnyaBot.Static.Keyboard;

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

                // Клиент
                var client = _clientService.Get(update.Message.From.Id);
                // Текущая запись
                var recordClient = _recordClientService.Get(client);
                // Парметр записи "Специалист"
                var recordParameter = _recordParameterClientService
                    .Aggregate(
                        item => item.RecordClientId == recordClient.Id,
                        item => item.ParameterId == RecordParameterConstant.Specialist)
                    .FirstOrDefault();

                var messages = new List<Task<Message>>();

                foreach (var item in _specialistAndServiceProvidedService.Get(item => item.SpecialistId == recordParameter!.UuidValue))
                {
                    var id = item.Id;
                    var price = item.Price;
                    var service = _serviceProvidedService.Get((Guid)item.ServiceId).Name;
                    var category = _categoryService.Get((Guid)item.CategoryId).Name;

                    var textMessage = string.Format("{0}\n({1})\n{2} руб.", service, category.ToLower(), price);

                    var message = botClient.SendTextMessageAsync(
                        chatId: update.Message.Chat.Id,
                        text: textMessage,
                        replyMarkup: SingleInlineKeyboardButton.Create("Выбрать", item.ServiceId.ToString())
                    );

                    messages.Add(message);
                }

                await Task.WhenAll(messages);
            }

            if (update?.CallbackQuery != null)
            {
                var record = _recordClientService.Get(_clientService.Get(update.CallbackQuery.From.Id));

                if (Guid.TryParse(update.CallbackQuery.Data, out Guid serviceId))
                {
                    _recordParameterClientService.Add(new RecordParameterClient()
                    {
                        ParameterId = RecordParameterConstant.Service,
                        RecordClientId = record.Id,
                        UuidValue = serviceId
                    });

                    var recordParameterClientCount = _recordParameterClientService
                        .Aggregate(
                            item => item.RecordClientId == record.Id,
                            item => item.ParameterId == RecordParameterConstant.Service)
                        .Count();

                    if (recordParameterClientCount == 0)
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: update.CallbackQuery.Message.Chat.Id,
                            text: "Подтвердите выбор услуги",
                            replyMarkup: SingleReplyKeyboardMarkup.Create("Продолжить")
                        );
                    }
                }

                return;
            }
        }
    }
}
