using System.Globalization;
using Telegram.Bot.Types.ReplyMarkups;

namespace TsiryulnyaBot.Static.Keyboard.Utilities
{
    public static class Row
    {
        public static IEnumerable<InlineKeyboardButton> Date(in DateTime date, DateTimeFormatInfo dtfi)
        {
            return new InlineKeyboardButton[]
            {
                InlineKeyboardButton.WithCallbackData($"{date.ToString("Y", dtfi)}", " ")
            };
        }

        public static IEnumerable<InlineKeyboardButton> DayOfWeek(DateTimeFormatInfo dtfi)
        {
            var firstDayOfWeek = (int)dtfi.FirstDayOfWeek;

            for (int i = 0; i < 7; i++)
            {
                yield return dtfi.AbbreviatedDayNames[(firstDayOfWeek + i) % 7];
            }
        }

        public static IEnumerable<IEnumerable<InlineKeyboardButton>> Month(DateTime date, DateTimeFormatInfo dtfi, List<int> freeDays)
        {
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1).Day;

            for (int dayOfMonth = 1, weekNum = 0; dayOfMonth <= lastDayOfMonth; weekNum++)
            {
                yield return NewWeek(weekNum, freeDays, ref dayOfMonth);
            }

            IEnumerable<InlineKeyboardButton> NewWeek(int weekNum, List<int> freeDays, ref int dayOfMonth)
            {
                var week = new InlineKeyboardButton[7];

                for (int dayOfWeek = 0; dayOfWeek < 7; dayOfWeek++)
                {
                    if (weekNum == 0 && dayOfWeek < FirstDayOfWeek() || dayOfMonth > lastDayOfMonth)
                    {
                        week[dayOfWeek] = " ";

                        continue;
                    }

                    var text = dayOfMonth.ToString();
                    var callbackData = firstDayOfMonth.AddDays(dayOfMonth - 1).ToString(@"dd.MM.yyyy");

                    if (!freeDays.Contains(dayOfMonth))
                    {
                        text = "-";
                        callbackData = " ";
                    }

                    week[dayOfWeek] = InlineKeyboardButton.WithCallbackData(text, callbackData);

                    dayOfMonth++;
                }

                return week;

                int FirstDayOfWeek() => (7 + (int)firstDayOfMonth.DayOfWeek - (int)dtfi.FirstDayOfWeek) % 7;
            }
        }
    }
}
