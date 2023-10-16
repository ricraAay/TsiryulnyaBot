using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TsiryulnyaBot.BLL.Constant;
using TsiryulnyaBot.DAL;
using TsiryulnyaBot.DAL.Interface;
using TsiryulnyaBot.DAL.Model;
using TsiryulnyaBot.DAL.Repository;

namespace TsiryulnyaBot.BLL.Service
{
    public class WorkerShiftService
    {
        private readonly IRepository<WorkerShift> _workerShiftsRepository;

        public WorkerShiftService(IRepository<WorkerShift> workerShiftsRepository) 
        {
            _workerShiftsRepository = workerShiftsRepository;
        }

        public WorkerShift Get(Guid id)
        {
            return _workerShiftsRepository.Get(id);
        }

        public IEnumerable<WorkerShift> Get(RecordParameterClient recordParameterClient) 
        {
            return _workerShiftsRepository
                .Aggregate(
                    item => item.SpecialistId == recordParameterClient.UuidValue, 
                    item => item.StatusId == WorkerShiftStatusConstant.Open,
                    item => item.Date >= new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day))
                .ToList();
        }

        public void Add(WorkerShift workerShift)
        {
            _workerShiftsRepository.Add(workerShift);
            _workerShiftsRepository.Commit();
        }

        public void Update(WorkerShift workerShift)
        {
            _workerShiftsRepository.Update(workerShift);
            _workerShiftsRepository.Commit();
        }

        public void Delete(WorkerShift workerShift)
        {
            _workerShiftsRepository.Delete(workerShift);
            _workerShiftsRepository.Commit();
        }
    }
}
