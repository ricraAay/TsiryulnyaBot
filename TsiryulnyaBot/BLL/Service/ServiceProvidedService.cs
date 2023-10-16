using System.Linq.Expressions;
using TsiryulnyaBot.DAL.Interface;
using TsiryulnyaBot.DAL.Model;

namespace TsiryulnyaBot.BLL.Service
{
    public class ServiceProvidedService
    {
        private readonly IRepository<ServiceProvided> _serivceProvidedRepository;

        public ServiceProvidedService(IRepository<ServiceProvided> serivceProvidedRepository) 
        {
            _serivceProvidedRepository = serivceProvidedRepository;
        }

        public ServiceProvided Get(Guid serviceId)
        {
            return _serivceProvidedRepository.Get(serviceId);
        }

        public IEnumerable<ServiceProvided> Get(Expression<Func<ServiceProvided, bool>> predicate) 
        {
            return _serivceProvidedRepository.Get(predicate);
        }
    }
}
