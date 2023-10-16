using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using TsiryulnyaBot.BLL;
using TsiryulnyaBot.Static;

namespace TsiryulnyaBot
{
    public class Program
    {
        async static Task Main(string[] args)
        {
            var botClient = new TelegramBotClient(Configuration.Get("BotToken"));

            var botInfo = await botClient.GetMeAsync();

            Console.WriteLine($"Start bot\nID: {botInfo.Id}\nNAME: {botInfo.FirstName}.");


            var commands = new List<BotCommand>()
            {
                new BotCommand { Command = "/start", Description = "запустить/перезапустить бота" }
            };

            await botClient.SetMyCommandsAsync(commands);

            using CancellationTokenSource cts = new();

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                cancellationToken: cts.Token
            );

            Console.ReadLine();

            cts.Cancel();
        }

        static public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {

            Console.WriteLine($"update.type: {update.Type}");

            var controller = new TelegramBotController(
                botClient: botClient,
                connectionString: Configuration.Get("ConnectionString")
            );

            await Task.Run(async () => await controller.Execute(update, cancellationToken));
        }

        static public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                  _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);

            return Task.CompletedTask;
        }
    }
}