using System.Linq.Expressions;
using TsiryulnyaBot.DAL.Interface;
using TsiryulnyaBot.DAL.Model;

namespace TsiryulnyaBot.BLL.Service
{
    public class SpecialistService
    {
        private readonly IRepository<Specialist> _specialistRepository;

        public SpecialistService(IRepository<Specialist> specialistRepository) 
        {
            _specialistRepository = specialistRepository;
        }

        public Specialist Get(Guid id)
        {
            return _specialistRepository.Get(id);
        }

        public IEnumerable<Specialist> Aggregate(params Expression<Func<Specialist, bool>>[] includeProperties)
        {
            return _specialistRepository.Aggregate(includeProperties);
        }

        public IEnumerable<Specialist> GetAll()
        {
            return _specialistRepository.GetAll().ToList();
        }
    }
}
