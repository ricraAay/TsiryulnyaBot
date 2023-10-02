using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TsiryulnyaBot.DAL.Interface;
using TsiryulnyaBot.DAL.Model;

namespace TsiryulnyaBot.BLL.Service
{
    public class SpecialistService
    {
        private readonly IRepository<Specialist> _specialistRepository;

        private readonly IRepository<Client> _clientRepository;

        public SpecialistService(
            IRepository<Specialist> specialistRepository,
            IRepository<Client> clientRepository) 
        {
            _specialistRepository = specialistRepository;
            _clientRepository = clientRepository;
        }

        public async Task GetAll(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var result = new List<Task<Message>>();

            foreach (var item in _specialistRepository.GetAll())
            {
                var inlineKeyboard = (InlineKeyboardMarkup)new(new[]
                {
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData(
                            text: "Выбрать",
                            callbackData: item.Id.ToString()
                        ),
                    }
                });

                var speclialist = _clientRepository
                    .GetWhere(client => client.Id == item.ClientId)
                    .FirstOrDefault();

                var message = botClient.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: speclialist.Name,
                    replyMarkup: inlineKeyboard,
                    cancellationToken: cancellationToken
                );

                result.Add(message);
            }

            var replyKeyboardMarkup = (ReplyKeyboardMarkup)new(new[]
            {
                new KeyboardButton[] { "Продолжить" },
            })
            {
                ResizeKeyboard = true
            };

            result.Add(botClient.SendTextMessageAsync(
                chatId: update.Message!.Chat.Id,
                text: "Выберите специалиста и нажмитие кнопку продолжить",
                replyMarkup: replyKeyboardMarkup,
                cancellationToken: cancellationToken
            ));

            await Task.WhenAll(result);
        }
    }
}
