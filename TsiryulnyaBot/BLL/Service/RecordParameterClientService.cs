using System.Linq.Expressions;
using TsiryulnyaBot.DAL.Interface;
using TsiryulnyaBot.DAL.Model;

namespace TsiryulnyaBot.BLL.Service
{
    public class RecordParameterClientService
    {
        public readonly IRepository<RecordParameterClient> _recordParameterClientRepository;

        public RecordParameterClientService(IRepository<RecordParameterClient> recordParameterClientRepository)
        {
            _recordParameterClientRepository = recordParameterClientRepository;
        }

        public IEnumerable<RecordParameterClient> Get(Expression<Func<RecordParameterClient, bool>> predicate) 
        {
            return _recordParameterClientRepository.Get(predicate);
        }

        public IEnumerable<RecordParameterClient> Aggregate(params Expression<Func<RecordParameterClient, bool>>[] includeProperties)
        {
            return _recordParameterClientRepository.Aggregate(includeProperties);
        }

        public void Add(RecordParameterClient paramter)
        {
            _recordParameterClientRepository.Add(paramter);
            _recordParameterClientRepository.Commit();
        }

        public void Delete(RecordParameterClient paramter)
        {
            _recordParameterClientRepository.Delete(paramter);
            _recordParameterClientRepository.Commit();
        }
    }
}
