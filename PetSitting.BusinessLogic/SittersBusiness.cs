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
    public class SittersBusiness : IDisposable
    {
        #region Class Declarations

        private LoggingHandler _loggingHandler;
        private bool _bDisposed;

        #endregion

        #region Class Methods
        // Business Logic pass through code to Insert Sitter
        public bool InsertSitter(SittersEntity entity)
        {
            try
            {
                bool bOpDoneSuccessfully;
                using (var repository = new SittersRepository())
                {
                    bOpDoneSuccessfully = repository.Insert(entity);
                }

                return bOpDoneSuccessfully;
            }
            catch (Exception ex)
            {
                //Log exception error 
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:SittersBusiness::InsertSitter::Error occured.", ex);
            }
        }
        // Business Logic pass through code to Update Sitter
        public bool UpdateSitter(SittersEntity entity)
        {
            try
            {
                bool bOpDoneSuccessfully;
                using (var repository = new SittersRepository())
                {
                    bOpDoneSuccessfully = repository.Update(entity);
                }

                return bOpDoneSuccessfully;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:SittersBusiness::UpdateSitter::Error occured.", ex);
            }
        }
        // Business Logic pass through code to Delete a Sitter by ID
        public bool DeleteSitterById(int empId)
        {
            try
            {
                using (var repository = new SittersRepository())
                {
                    return repository.DeleteById(empId);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:SittersBusiness::DeleteSitterById::Error occured.", ex);
            }
        }
        // Business Logic pass through code to Select a Sitter by ID
        public SittersEntity SelectSitterById(int empId)
        {
            try
            {
                SittersEntity returnedEntity;
                using (var repository = new SittersRepository())
                {
                    returnedEntity = repository.SelectById(empId);
                    if (returnedEntity != null)
                        returnedEntity.NetSalary = GetNetSalary(returnedEntity.GrossSalary, returnedEntity.Age);

                }

                return returnedEntity;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:SittersBusiness::SelectSitterById::Error occured.", ex);
            }
        }
        // Business Logic pass through code to Select All Sitters
        public List<SittersEntity> SelectAllSitters()
        {
            var returnedEntities = new List<SittersEntity>();

            try
            {
                using (var repository = new SittersRepository())
                {
                    foreach (var entity in repository.SelectAll())
                    {
                        entity.NetSalary = GetNetSalary(entity.GrossSalary, entity.Age);
                        returnedEntities.Add(entity);
                    }
                }

                return returnedEntities;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:SittersBusiness::SelectAllSitters::Error occured.", ex);
            }
        }
        // HERE BELOW A CALCULATION CAN BE ADDED AT THE BusinessLogic Level
        private decimal GetNetSalary(decimal grossSalary, int age)
        {
            var netSalary = grossSalary;

            if (age < 30)
            {
                //Deduct 50% from the Gross Salary 
                netSalary = grossSalary - grossSalary * 0.5M;
            }
            else if (age < 40)
            {
                //Deduct 40% from the Gross Salary 
                netSalary = grossSalary - grossSalary * 0.4M;
            }
            else if (age < 50)
            {
                //Deduct 30% from the Gross Salary 
                netSalary = grossSalary - grossSalary * 0.3M;
            }
            else if (age < 60)
            {
                //Deduct 20% from the Gross Salary 
                netSalary = grossSalary - grossSalary * 0.2M;
            }

            return Math.Round(netSalary, 2);
        }
        // Business Logic environment instantiation
        public SittersBusiness()
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
