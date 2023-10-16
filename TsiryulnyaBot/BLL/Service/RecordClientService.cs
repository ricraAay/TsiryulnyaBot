﻿using TsiryulnyaBot.BLL.Constant;
using TsiryulnyaBot.BLL.Interface;
using TsiryulnyaBot.DAL.Interface;
using TsiryulnyaBot.DAL.Model;

namespace TsiryulnyaBot.BLL.Service
{
    public class RecordClientService
    {
        private readonly IRepository<RecordClient> _recordClientRepository;

        public RecordClientService(IRepository<RecordClient> recordClientRepository)        
        {
            _recordClientRepository = recordClientRepository;
        }

        public RecordClient Get(Client client)
        {
            var recordClient = _recordClientRepository
                .Get(record => record.ClientId == client!.Id)
                .Where(record => record.StatusId == RecordStatusConstant.ProcessRegistration)
                .FirstOrDefault();

            if (recordClient != null)
            {
                return recordClient;
            }

            recordClient = new RecordClient()
            {
                ClientId = client.Id,
                StatusId = RecordStatusConstant.ProcessRegistration
            };

            _recordClientRepository.Add(recordClient);
            _recordClientRepository.Commit();

            return recordClient!;
        }

        public void Update(RecordClient recordClient)
        {
            _recordClientRepository.Update(recordClient);
            _recordClientRepository.Commit();
        }
    }
}
