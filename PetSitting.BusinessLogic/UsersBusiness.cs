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
    public class UsersBusiness : IDisposable
    {
        #region Class Declarations

        private LoggingHandler _loggingHandler;
        private bool _bDisposed;

        #endregion

        #region Class Methods
        // Business Logic pass through code to Insert User
        public bool InsertUser(UsersEntity entity)
        {
            try
            {
                bool bOpDoneSuccessfully;
                using (var repository = new UsersRepository())
                {
                    bOpDoneSuccessfully = repository.Insert(entity);
                }

                return bOpDoneSuccessfully;
            }
            catch (Exception ex)
            {
                //Log exception error 
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:UsersBusiness::InsertUser::Error occured.", ex);
            }
        }
        // Business Logic pass through code to Update User
        public bool UpdateUser(UsersEntity entity)
        {
            try
            {
                bool bOpDoneSuccessfully;
                using (var repository = new UsersRepository())
                {
                    bOpDoneSuccessfully = repository.Update(entity);
                }

                return bOpDoneSuccessfully;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:UsersBusiness::UpdateUser::Error occured.", ex);
            }
        }
        // Business Logic pass through code to Delete User 
        public bool DeleteUserById(int empId)
        {
            try
            {
                using (var repository = new UsersRepository())
                {
                    return repository.DeleteById(empId);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:UsersBusiness::DeleteUserById::Error occured.", ex);
            }
        }
        // Business Logic pass through code to Select a User
        public UsersEntity SelectUserById(int empId)
        {
            try
            {
                UsersEntity returnedEntity;
                using (var repository = new UsersRepository())
                {
                    returnedEntity = repository.SelectById(empId);
                    if (returnedEntity != null)
                        // Business Calculation function called from here
                        returnedEntity.Discount = GetDiscount(returnedEntity.Age);
                }

                return returnedEntity;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:UsersBusiness::SelectOwnerById::Error occured.", ex);
            }
        }
        // Business Logic pass through code to Select All Users
        public List<UsersEntity> SelectAllUsers()
        {
            var returnedEntities = new List<UsersEntity>();

            try
            {
                using (var repository = new UsersRepository())
                {
                    // HERE A CALCULATION CAN BE ADDED AS A COLUMN IN SelectAll function
                    foreach (var entity in repository.SelectAll())
                    {
                        entity.Discount = GetDiscount(entity.Age);
                        returnedEntities.Add(entity);
                    }
                }

                return returnedEntities;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:UsersBusiness::SelectAllUsers::Error occured.", ex);
            }
        }
        // HERE BELOW A CALCULATION CAN BE ADDED AT THE BusinessLogic Level
        private decimal GetDiscount(int age)
        {
            var calculation = 0.0M;
            if (age >= 65)
            {
                calculation = 0.25M;
            }
            else
            {
                calculation = 0.0M;
            }
            return calculation;
        }
        // Business Logic environment instantiation
        public UsersBusiness()
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
