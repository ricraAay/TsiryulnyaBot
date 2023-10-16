using Telegram.Bot.Types.ReplyMarkups;

namespace TsiryulnyaBot.UIL.Keyboard
{
    public static class SingleInlineKeyboardButton
    {
        public static InlineKeyboardMarkup Create(string text, string callbackData)
        {
            return new(new[]
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text, callbackData)
                }
            });
        }
    }
}
