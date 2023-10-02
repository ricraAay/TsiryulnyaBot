using TsiryulnyaBot.DAL.Interface;
using TsiryulnyaBot.DAL.Model;

namespace TsiryulnyaBot.BLL.Service
{
    public class RecordFillingStepItemService
    {
        private readonly IRepository<RecordFillingStepItem> _recordFillingStepItemRepository;

        public RecordFillingStepItemService(IRepository<RecordFillingStepItem> recordFillingStepItemRepository) 
        {
            _recordFillingStepItemRepository = recordFillingStepItemRepository;
        }

        public List<RecordFillingStepItem> GetAll()
        {
            return _recordFillingStepItemRepository.GetAll().ToList();
        }
    }
}
