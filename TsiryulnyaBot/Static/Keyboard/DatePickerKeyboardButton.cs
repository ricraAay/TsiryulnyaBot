using System.Globalization;
using Telegram.Bot.Types.ReplyMarkups;
using TsiryulnyaBot.Static.Keyboard.Utilities;

namespace TsiryulnyaBot.Static.Keyboard
{
    public static class DatePickerKeyboardButton
    {
        public static InlineKeyboardMarkup Create(in DateTime date, DateTimeFormatInfo dateTimeFormatInfo, IEnumerable<DateOnly> filter)
        {
            var keyboardRows = new List<IEnumerable<InlineKeyboardButton>>
            {
                Row.Date(date, dateTimeFormatInfo),
                Row.DayOfWeek(dateTimeFormatInfo)
            };

            keyboardRows.AddRange(Row.Month(date, dateTimeFormatInfo, filter.Select(item => item.Day).ToList()));

            return new InlineKeyboardMarkup(keyboardRows);
        }
    }
}
