using System.Linq.Expressions;
using Telegram.Bot.Types;
using TsiryulnyaBot.BLL.Interface;
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
            var from = update.Message != null 
                ? update.Message.From 
                : update.CallbackQuery.From;

            var client = _clientRepository
                .Get(client => client.TlgId == from.Id)
                .FirstOrDefault();

            if (client !=  null)
            {
                return client;
            }

            client = new Client()
            {
                Name = $"{from.FirstName} {from.LastName}".Trim(),
                TlgUserName = from.Username,
                TlgId = from.Id
            };

            _clientRepository.Add(client);
            _clientRepository.Commit();

            return client;
        }

        public Client Get(Guid id)
        {
            return _clientRepository.Get(id);
        }

        public IEnumerable<Client> Get(Expression<Func<Client, bool>> predicate) 
        {
            return _clientRepository.Get(predicate).ToList();
        }
    }
}
