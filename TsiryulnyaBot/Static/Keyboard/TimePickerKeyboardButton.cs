using Telegram.Bot.Types.ReplyMarkups;
using TsiryulnyaBot.DAL.Model;

namespace TsiryulnyaBot.Static.Keyboard
{
    public static class TimePickerKeyboardButton
    {
        public static InlineKeyboardMarkup Create(List<WorkerShift> timeCollection)
        {
            var buttons = new List<List<InlineKeyboardButton>>();
            var maxCountButton = 2;
            var maxTimeCollection = timeCollection.Count();
            var maxRowButton = (int)Math.Ceiling((decimal)maxTimeCollection / maxCountButton);

            for (var i = 0; i < maxRowButton; i++)
            {
                var list = new List<InlineKeyboardButton>();

                foreach (var time in timeCollection.Skip(i * maxCountButton).Select((value, idx) => new { value, idx }))
                {
                    list.Add(InlineKeyboardButton.WithCallbackData($" {time.value.Time} ", $"{time.value.Id}"));

                    if (list.Count == maxCountButton)
                    {
                        break;
                    }
                }

                buttons.Add(list);
            }

            return new InlineKeyboardMarkup(buttons);
        }
    }
}
