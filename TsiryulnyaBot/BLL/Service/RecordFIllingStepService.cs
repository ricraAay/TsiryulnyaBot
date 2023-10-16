using TsiryulnyaBot.DAL.Interface;
using TsiryulnyaBot.DAL.Model;

namespace TsiryulnyaBot.BLL.Service
{
    public class RecordFillingStepService
    {
        private readonly IRepository<RecordFillingStep> _recordFillingStepRepository;

        private readonly IRepository<RecordFillingStepItem> _recordFillingStepItemRepository;

        public RecordFillingStepService(
            IRepository<RecordFillingStep> recordFillingStepRepository, 
            IRepository<RecordFillingStepItem> recordFillingStepItemRepository)
        {
            _recordFillingStepRepository = recordFillingStepRepository;
            _recordFillingStepItemRepository = recordFillingStepItemRepository;
        }

        public RecordFillingStep? Get(RecordClient record)
        {
            var recordFillingStep = _recordFillingStepRepository
                .Get(item => item.RecordId == record.Id)
                .OrderBy(item => item.StepPosition)
                .FirstOrDefault(item => item.Passed == false);

            if (recordFillingStep != null)
            {
                return recordFillingStep;
            }

            foreach(var stepItem in _recordFillingStepItemRepository.GetAll())
            {
                _recordFillingStepRepository.Add(new RecordFillingStep()
                {
                    StepItemId = stepItem.Id,
                    Passed = false,
                    RecordId = record.Id,
                    StepPosition = stepItem.StepPosition
                });
            }

            _recordFillingStepRepository.Commit();

            return _recordFillingStepRepository
                .Get(item => item.RecordId == record.Id)
                .OrderBy(item => item.StepPosition)
                .FirstOrDefault(item => item.Passed == false);
        }

        public void Update(RecordFillingStep recordStep)
        {
            _recordFillingStepRepository.Update(recordStep);
            _recordFillingStepRepository.Commit();
        }
    }
}
