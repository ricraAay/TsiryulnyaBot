using TsiryulnyaBot.DAL.Interface;
using TsiryulnyaBot.DAL.Model;

namespace TsiryulnyaBot.BLL.Service
{
    public class RecordFIllingStepService
    {
        private readonly IRepository<RecordFillingStep> _recordFillingStepRepository;

        private readonly IRepository<RecordFillingStepItem> _recordFillingStepItemRepository;

        public RecordFIllingStepService(
            IRepository<RecordFillingStep> recordFillingStepRepository, 
            IRepository<RecordFillingStepItem> recordFillingStepItemRepository)
        {
            _recordFillingStepRepository = recordFillingStepRepository;
            _recordFillingStepItemRepository = recordFillingStepItemRepository;
        }

        public RecordFillingStep Get(RecordClient record)
        {
            var recordFillingStep = _recordFillingStepRepository
                .GetWhere(item => item.RecordId == record.Id)
                .OrderBy(item => item.StepPosition)
                .FirstOrDefault(item => item.Passed == false);

            if (recordFillingStep != null)
            {
                return recordFillingStep;
            }

            foreach(var stepItem in _recordFillingStepItemRepository.GetAll())
            {
                Add(new RecordFillingStep()
                {
                    StepItemId = stepItem.Id,
                    Passed = false,
                    RecordId = record.Id,
                    StepPosition = stepItem.StepPosition
                });
            }

            recordFillingStep = _recordFillingStepRepository
                .GetWhere(item => item.RecordId == record.Id)
                .OrderBy(item => item.StepPosition)
                .FirstOrDefault(item => item.Passed == false);

            return recordFillingStep!;
        }

        public void Add(RecordFillingStep recordStep)
        {
            _recordFillingStepRepository.Add(recordStep);
        }

        public void Update(RecordFillingStep recordStep)
        {
            _recordFillingStepRepository.Update(recordStep);
        }
    }
}
