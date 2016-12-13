using System;
using TrustSystems3.Repository.Infrastructure;
using TrustSystems3.Repository.Infrastructure.Contract;

namespace TrustSystems3.Repository
{
    public class LoggerRepository : BaseRepository<Logger>
    {
        public LoggerRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void AddLog(string message, LogType type)
        {
            try
            {
                Insert(new Logger {
                    Type = type,
                    Message = message,
                    Date = DateTime.Now
                });
            }
            catch (Exception ex) { }
        }
    }
}