using Telegram.Bot;
using TsiryulnyaBot.DAL;
using TsiryulnyaBot.DAL.Model;
using Microsoft.Extensions.Configuration;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Newtonsoft.Json;
using TsiryulnyaBot.BLL.Service;
using TsiryulnyaBot.DAL.Repository;

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

            // Send cancellation request to stop bot
            cts.Cancel();

            //using (TsiryulnyaContext db = new TsiryulnyaContext(config["ConnectionString"]))
            //{
            //    try
            //    {
            //        var client = new Client { Name = "Supervisor", TlgUserName = "Supervisor", TlgId = -1 };

            //        db.Clients.Add(client);

            //       db.SaveChanges();
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine(e.Message);
            //    }
            //}
        }

        static public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(JsonConvert.SerializeObject(update));

            //var controller = new ControllerService(botClient);

            //await controller.Execute(update);
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true);

            var configuration = builder.Build();

            //var clientService = new ClientService(configuration["ConnectionString"]);

            //var from = update.Message.From;

            //var client = await clientService.Execute((int)from.Id, $"{from.FirstName} {from.LastName}", from.Username);

            await using (var context = new TsiryulnyaContext(configuration["ConnectionString"]!))
            {
                var specialistService = new SpecialistService(
                    specialistRepository: new Repository<Specialist>(context),
                    clientRepository: new Repository<Client>(context)
                );

                await specialistService.GetAll(botClient, update, cancellationToken);
            }

            // Only process Message updates: https://core.telegram.org/bots/api#message
            //if (update.Message is not { } message)
            //    return;
            //// Only process text messages
            //if (message.Text is not { } messageText)
            //    return;

            //var chatId = message.Chat.Id;

            //Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

            //ReplyKeyboardMarkup replyKeyboardMarkup = null;

            //if (messageText == "/start")
            //{
            //    replyKeyboardMarkup = new(new[]
            //    {
            //        new KeyboardButton[] { "Выбрать специалиста" },
            //    })
            //    {
            //        ResizeKeyboard = true,
            //    };
            //}
            //else if (messageText == "Выбрать специалиста")
            //{
            //    replyKeyboardMarkup = new(new[]
            //    {
            //        new KeyboardButton[] { "Выбрать услугу" },
            //        new KeyboardButton[] { "Вернуться назад" }
            //    })
            //    {
            //        ResizeKeyboard = true,
            //    };
            //} else if (messageText == "Вернуться назад")
            //{
            //    replyKeyboardMarkup = new(new[]
            //{
            //        new KeyboardButton[] { "Выбрать специалиста" },
            //    })
            //    {
            //        ResizeKeyboard = true,
            //    };
            //}


            //InlineKeyboardMarkup inlineKeyboard = new(new[]
            //{
            //    // first row
            //    new []
            //    {
            //        InlineKeyboardButton.WithCallbackData(text: "1.1", callbackData: "11"),
            //        InlineKeyboardButton.WithCallbackData(text: "1.2", callbackData: "12"),
            //    },
            //    // second row
            //    new []
            //    {
            //        InlineKeyboardButton.WithCallbackData(text: "2.1", callbackData: "21"),
            //        InlineKeyboardButton.WithCallbackData(text: "2.2", callbackData: "22"),
            //    },
            //});



            //InlineKeyboardMarkup inlineKeyboard1 = new(new[]
            //{
            //    InlineKeyboardButton.WithSwitchInlineQuery(
            //        text: "switch_inline_query"),
            //    InlineKeyboardButton.WithSwitchInlineQueryCurrentChat(
            //        text: "switch_inline_query_current_chat"),
            //});


            // Echo received message text
            //Message sentMessage = await botClient.SendTextMessageAsync(
            //    chatId: update.Message.Chat.Id,
            //    text: "You said:\n" + $"@{client.TlgUserName}",
            //    cancellationToken: cancellationToken
            //);
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