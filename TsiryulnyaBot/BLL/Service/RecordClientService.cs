using Telegram.Bot.Types;
using TsiryulnyaBot.DAL.Interface;
using TsiryulnyaBot.DAL.Model;

namespace TsiryulnyaBot.BLL.Service
{
    public class RecordClientService
    {
        private readonly IRepository<RecordClient> _recordClientRepository;

        private readonly IRepository<Client> _clientRepository;

        public RecordClientService(
            IRepository<RecordClient> recordClientRepository,
            IRepository<Client> clientRepository) 
        {
            _recordClientRepository = recordClientRepository;
            _clientRepository = clientRepository;
        }

        public RecordClient Get(Update update)
        {
            var client = _clientRepository
                .GetWhere(client => client.TlgId == update.Message!.From!.Id)
                .FirstOrDefault();

            var recordClient = _recordClientRepository
                    .GetWhere(record => record.ClientId == client!.Id)
                    .FirstOrDefault();  
            
            if (recordClient == null)
            {
                recordClient = new RecordClient()
                {
                    ClientId = client!.Id,
                    StatusId = new Guid("84a68145-6a1e-46e8-a83c-1e2eb636ffd3")
                };
            }

            return recordClient;
        }
    }
}
