using Telegram.Bot.Types;
using TsiryulnyaBot.DAL.Interface;
using TsiryulnyaBot.DAL.Model;

namespace TsiryulnyaBot.BLL.Service
{
    public class ClientService
    {
        private readonly IRepository<Client> _clientRepository;

        public ClientService(IRepository<Client> clientRepository) 
        {
            _clientRepository = clientRepository;
        }

        public Client Get(Update update)
        {
            var message = update?.Message;
            var from = message?.From;

            var client = _clientRepository
                .GetWhere(item => item.TlgId == from.Id)
                .FirstOrDefault();

            if (client == null)
            {
                client = new Client()
                {
                    Name = $"{from.FirstName} {from.LastName}".Trim(),
                    TlgUserName = from.Username,
                    TlgId = (int)from.Id,
                };

                _clientRepository.Add(client);
            }

            return client;
        }
    }
}
