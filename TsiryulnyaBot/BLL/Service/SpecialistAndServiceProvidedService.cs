using System.Linq.Expressions;
using TsiryulnyaBot.DAL.Interface;
using TsiryulnyaBot.DAL.Model;

namespace TsiryulnyaBot.BLL.Service
{
    public class SpecialistAndServiceProvidedService
    {
        private readonly IRepository<SpecialistAndService> _specialistAndServiceRepository;

        public SpecialistAndServiceProvidedService(IRepository<SpecialistAndService> specialistAndServiceRepository)
        {
            _specialistAndServiceRepository = specialistAndServiceRepository;
        }

        public IEnumerable<SpecialistAndService> Get(Expression<Func<SpecialistAndService, bool>> predicate)
        {
            return _specialistAndServiceRepository.Get(predicate).ToList();
        }
    }
}
