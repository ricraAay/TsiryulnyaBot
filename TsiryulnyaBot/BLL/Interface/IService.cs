using Telegram.Bot;
using Telegram.Bot.Types;

namespace TsiryulnyaBot.BLL.Interface
{
    public interface IService
    {
        Task Execute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);

        Task Previous(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
            => throw new NotImplementedException();

        Task Next(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
            => throw new NotImplementedException();
    }
}
