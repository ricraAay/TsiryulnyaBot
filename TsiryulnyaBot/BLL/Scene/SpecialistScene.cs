﻿using Telegram.Bot;
using Telegram.Bot.Types;
using TsiryulnyaBot.BLL.Constant;
using TsiryulnyaBot.BLL.Interface;
using TsiryulnyaBot.BLL.Service;
using TsiryulnyaBot.DAL.Model;
using TsiryulnyaBot.Static.Keyboard;

namespace TsiryulnyaBot.BLL.Scene
{
    public class SpecialistScene : IScene
    {
        private readonly SpecialistService _specialistService;

        private readonly ClientService _clientService;

        private readonly RecordClientService _recordClientService;

        private readonly RecordParameterClientService _recordParameterClientService;

        public delegate void Handler();

        public event Handler? NextScene;

        public SpecialistScene(
            SpecialistService specialistService,
            ClientService clientService,
            RecordClientService recordClientService,
            RecordParameterClientService recordParameterClientService)
        {
            _specialistService = specialistService;
            _clientService = clientService;
            _recordClientService = recordClientService;
            _recordParameterClientService = recordParameterClientService;
        }

        public async Task Execute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var messages = new List<Task<Message>>();

            if (update.Message != null)
            {
                if (update?.Message.Text == "Продолжить")
                {
                    NextScene?.Invoke();
                    return;
                }

                foreach (var speclialist in _specialistService.GetAll())
                {
                    messages.Add(botClient.SendPhotoAsync(
                        chatId: update.Message.Chat.Id,
                        photo: InputFile.FromFileId(_clientService.Get((Guid)speclialist.ClientId).TlgFileId),
                        caption: _clientService.Get((Guid)speclialist.ClientId).Name,
                        replyMarkup: SingleInlineKeyboardButton.Create("Выбрать", speclialist.Id.ToString())
                    ));
                }

                await Task.WhenAll(messages);
            }

            if (update.CallbackQuery != null)
            {
                var client = _clientService.Get(update.CallbackQuery.From.Id);
                var record = _recordClientService.Get(client);

                if (Guid.TryParse(update.CallbackQuery.Data, out Guid specialistId))
                {
                    _recordParameterClientService.Add(new RecordParameterClient()
                    {
                        ParameterId = RecordParameterConstant.Specialist,
                        RecordClientId = record.Id,
                        UuidValue = specialistId
                    });

                    await botClient.SendTextMessageAsync(
                        chatId: update.CallbackQuery.Message!.Chat.Id,
                        text: "Подтвердите выбор специалиста",
                        replyMarkup: SingleReplyKeyboardMarkup.Create("Продолжить"),
                        cancellationToken: cancellationToken
                    );
                }
            }
        }
    }
}
