using Telegram.Bot;
using Telegram.Bot.Types;

namespace TsiryulnyaBot.BLL.Interface
{
    public interface IScene
    {
        Task Execute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
    }
}
