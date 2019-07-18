using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetSitting.Common;
using PetSitting.DataAccess;
using PetSitting.DataAccess.Common;
using PetSitting.Model;

namespace PetSitting.BusinessLogic
{
    public class SessionsBusiness : IDisposable
    {
        #region Class Declarations

        private LoggingHandler _loggingHandler;
        private bool _bDisposed;

        #endregion

        #region Class Methods
        // Business Logic pass through code to Insert Session
        public bool InsertSession(SessionsEntity entity)
        {
            try
            {
                bool bOpDoneSuccessfully;
                using (var repository = new SessionsRepository())
                {
                    bOpDoneSuccessfully = repository.Insert(entity);
                }

                return bOpDoneSuccessfully;
            }
            catch (Exception ex)
            {
                //Log exception error 
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:SessionsBusiness::InsertSession::Error occured.", ex);
            }
        }
        // Business Logic pass through code to Update Session
        public bool UpdateSession(SessionsEntity entity)
        {
            try
            {
                bool bOpDoneSuccessfully;
                using (var repository = new SessionsRepository())
                {
                    bOpDoneSuccessfully = repository.Update(entity);
                }

                return bOpDoneSuccessfully;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:SessionsBusiness::UpdateSession::Error occured.", ex);
            }
        }
        // Business Logic pass through code to Delete a Session by ID
        public bool DeleteSessionById(int empId)
        {
            try
            {
                using (var repository = new SessionsRepository())
                {
                    return repository.DeleteById(empId);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:SessionsBusiness::DeleteSessionById::Error occured.", ex);
            }
        }
        // Business Logic pass through code to Select a Session by ID
        public SessionsEntity SelectSessionById(int empId)
        {
            try
            {
                SessionsEntity returnedEntity;
                using (var repository = new SessionsRepository())
                {
                    returnedEntity = repository.SelectById(empId);
                    if (returnedEntity != null)
                        returnedEntity.FeeCap = GetFeeCap(returnedEntity.Fee);

                }

                return returnedEntity;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:SessionsBusiness::SelectSessionById::Error occured.", ex);
            }
        }

        // Business Logic pass through code to Select All Sessions
        public List<SessionsEntity> SelectAllSessions()
        {
            var returnedEntities = new List<SessionsEntity>();

            try
            {
                using (var repository = new SessionsRepository())
                {
                    foreach (var entity in repository.SelectAll())
                    {
                        entity.FeeCap = GetFeeCap(entity.Fee);
                        returnedEntities.Add(entity);
                    }
                }

                return returnedEntities;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:SessionsBusiness::SelectAllSessions::Error occured.", ex);
            }
        }
        // HERE BELOW A CALCULATION CAN BE ADDED AT THE BusinessLogic Level
        private decimal GetFeeCap(decimal fee)
        {
            var feecap = fee;

            feecap = 500;

            return feecap;
        }
        // Business Logic environment instantiation
        public SessionsBusiness()
        {
            _loggingHandler = new LoggingHandler();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool bDisposing)
        {
            // Check to see if Dispose has already been called.
            if (!_bDisposed)
            {
                if (bDisposing)
                {
                    // Dispose managed resources.
                    _loggingHandler = null;
                }
            }
            _bDisposed = true;
        }
        #endregion
    }
}
