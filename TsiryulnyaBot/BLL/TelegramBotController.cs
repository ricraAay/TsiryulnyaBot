using Telegram.Bot;
using Telegram.Bot.Types;
using TsiryulnyaBot.BLL.Service;
using TsiryulnyaBot.DAL;
using TsiryulnyaBot.DAL.Model;
using TsiryulnyaBot.DAL.Repository;
using Microsoft.Extensions.Configuration;
using TsiryulnyaBot.BLL.Scene;
using TsiryulnyaBot.BLL.Interface;

namespace TsiryulnyaBot.BLL
{
    public class TelegramBotController
    {
        private readonly ITelegramBotClient _botClient;

        private readonly IConfiguration _configuration;

        public TelegramBotController(ITelegramBotClient botClient, IConfigurationRoot configuration)
        {
            _botClient = botClient;
            _configuration = configuration;
        }

        public async Task Execute(Update update, CancellationToken cancellationToken)
        {
            await using (var context = new TsiryulnyaContext(_configuration["ConnectionString"]))
            {
                var clientRepository = new Repository<Client>(context);
                var recordClientRepository = new Repository<RecordClient>(context);
                var recordFillingStepRepository = new Repository<RecordFillingStep>(context);
                var recordFillingStepItemRepository = new Repository<RecordFillingStepItem>(context);
                var specialistRepository = new Repository<Specialist>(context);
                var recordParameterClientRepository = new Repository<RecordParameterClient>(context);
                var specialistAndServiceRepository = new Repository<SpecialistAndService>(context);
                var categoryRepository = new Repository<Category>(context);
                var serviveProvidedRepository = new Repository<ServiceProvided>(context);
                var workerShiftRepository = new Repository<WorkerShift>(context);

                var clientService = new ClientService(clientRepository);
                var recordClientService = new RecordClientService(recordClientRepository);
                var recordFillingStepService = new RecordFillingStepService(recordFillingStepRepository, recordFillingStepItemRepository);
                var specialistService = new SpecialistService(specialistRepository);
                var recordParameterClientService = new RecordParameterClientService(recordParameterClientRepository);
                var specialistAndServiceService = new SpecialistAndServiceProvidedService(specialistAndServiceRepository);
                var categoryService = new CategoryService(categoryRepository);
                var serviceProvidedService = new ServiceProvidedService(serviveProvidedRepository);
                var workerShiftService = new WorkerShiftService(workerShiftRepository);

                var client = clientService.Get(update);
                var recordClient = recordClientService.Get(client);
                var recordFillingStep = recordFillingStepService.Get(recordClient);

                var specialistScene = new SpecialistScene(
                    specialistService: specialistService,
                    clientService: clientService,
                    recordClientService: recordClientService,
                    recordParameterClientService: recordParameterClientService
                );

                var serviceScene = new ServiceScene(
                    specialistAndServiceService: specialistAndServiceService,
                    recordParameterClientService: recordParameterClientService,
                    clientService: clientService,
                    recordClientService: recordClientService,
                    categoryService: categoryService,
                    serviceProvidedService: serviceProvidedService
                );

                var wokerShiftScene = new WorkerShiftScene(
                    clientService: clientService,
                    workerShiftService: workerShiftService,
                    recordClientService: recordClientService,
                    recordParameterClientService: recordParameterClientService
                );

                var confirmationEntryScene = new ConfirmationEntryScene(
                    clientService: clientService,
                    recordClientService: recordClientService,
                    recordParameterClientService: recordParameterClientService,
                    specialistService: specialistService,
                    serviceProvidedService: serviceProvidedService,
                    workerShiftService: workerShiftService
                );

                var scenes = new Dictionary<int, IScene>
                {
                    { 1, specialistScene },
                    { 2, serviceScene },
                    { 3, wokerShiftScene },
                    { 4, confirmationEntryScene }
                };

                specialistScene.NextScene += nextSceneHandler;
                serviceScene.NextScene += nextSceneHandler;
                wokerShiftScene.NextScene += nextSceneHandler;

                async void nextSceneHandler()
                {
                    var stepPosition = recordFillingStepService.Get(recordClient);
                    stepPosition.Passed = true;

                    recordFillingStepService.Update(stepPosition);

                    update.Message.Text = "/start";

                    await Execute(update, cancellationToken);
                };

                //return;

                #region test async
                // await _botClient.SendTextMessageAsync(
                //    chatId: update.Message!.Chat.Id,
                //    text: "Hello",
                //    cancellationToken: cancellationToken
                //);

                //await Task.Delay(10000);
                #endregion

                var stepPosition = (int)recordFillingStepService.Get(recordClient).StepPosition;

                await scenes[stepPosition].Execute(_botClient, update, cancellationToken);

            }
        }
    }
}
