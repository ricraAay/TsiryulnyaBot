using Telegram.Bot;
using Microsoft.Extensions.Configuration;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using TsiryulnyaBot.BLL;

namespace TsiryulnyaBot
{
    public class Program
    {
        async static Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true);

            var configuration = builder.Build();

            var botClient = new TelegramBotClient(configuration["BotToken"]);

            var botInfo = await botClient.GetMeAsync();

            Console.WriteLine($"Hello, World! I am user {botInfo.Id} and my name is {botInfo.FirstName}.");

            using CancellationTokenSource cts = new();

            var commands = new List<BotCommand>()
            {
                new BotCommand { Command = "/start", Description = "запустить бота" },
                new BotCommand { Command = "/about", Description = "как работать с ботом" },
            };

            await botClient.SetMyCommandsAsync(commands: commands);

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                cancellationToken: cts.Token
            );

            Console.ReadLine();

            cts.Cancel();
        }

        static public Dictionary<string, object> Coockie = new Dictionary<string, object>();

        static public void Session(Update update)
        {
            // поиск
        }

        static public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            //Console.WriteLine(JsonConvert.SerializeObject(update));

            Console.WriteLine($"update.type: {update.Type}");

            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true);

            var configuration = builder.Build();

            var controller = new TelegramBotController(botClient, configuration);

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