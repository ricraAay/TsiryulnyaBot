using Telegram.Bot.Types.ReplyMarkups;

namespace TsiryulnyaBot.Static.Keyboard
{
    public static class SingleReplyKeyboardMarkup
    {
        public static ReplyKeyboardMarkup Create(string text)
        {
            return new(new[]
            {
                new KeyboardButton[] { text },
            })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };
        }
    }
}
