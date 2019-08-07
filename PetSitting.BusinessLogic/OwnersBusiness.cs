
namespace PetSitting.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using PetSitting.Common;
    using PetSitting.DataAccess;
    using PetSitting.Model;
    public class OwnersBusiness : IDisposable
    {
        #region Class Declarations

        private LoggingHandler _loggingHandler;
        private bool _bDisposed;

        #endregion

        #region Class Methods
        // Business Logic pass through code to Insert Owner
        public bool InsertOwner(OwnersEntity entity)
        {
            try
            {
                bool bOpDoneSuccessfully;
                using (var repository = new OwnersRepository())
                {
                    bOpDoneSuccessfully = repository.Insert(entity);
                }

                return bOpDoneSuccessfully;
            }
            catch (Exception ex)
            {
                //Log exception error 
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:OwnersBusiness::InsertOwner::Error occured.", ex);
            }
        }
        // Business Logic pass through code to Update Owner
        public bool UpdateOwner(OwnersEntity entity)
        {
            try
            {
                bool bOpDoneSuccessfully;
                using (var repository = new OwnersRepository())
                {
                    bOpDoneSuccessfully = repository.Update(entity);
                }

                return bOpDoneSuccessfully;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:OwnersBusiness::UpdateOwner::Error occured.", ex);
            }
        }
        // Business Logic pass through code to Delete an Owner by ID
        public bool DeleteOwnerById(int id)
        {
            try
            {
                using (var repository = new OwnersRepository())
                {
                    return repository.DeleteById(id);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:OwnersBusiness::DeleteOwnerById::Error occured.", ex);
            }
        }
        // Business Logic pass through code to Select an Owner by ID
        public OwnersEntity SelectOwnerById(int id)
        {
            try
            {
                OwnersEntity returnedEntity;
                using (var repository = new OwnersRepository())
                {
                    returnedEntity = repository.SelectById(id);
                    if (returnedEntity != null)
                        // Business Calculation function called from here
                        returnedEntity.PetYears = GetPetYears(returnedEntity.PetAge);
                }

                return returnedEntity;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:OwnersBusiness::SelectOwnerById::Error occured.", ex);
            }
        }
        public OwnersEntity FindOwnerByUserId(int userid)
        {
            try
            {
                OwnersEntity returnedEntity;
                using (var repository = new OwnersRepository())
                {
                    returnedEntity = repository.SelectByUserId(userid);
                    if (returnedEntity != null)
                        // Business Calculation function called from here
                        returnedEntity.PetYears = GetPetYears(returnedEntity.PetAge);
                }

                return returnedEntity;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:OwnersBusiness::FindOwnerByUserId::Error occured.", ex);
            }
        }
        // Business Logic pass through code to Select all Owners
        public List<OwnersEntity> SelectAllOwners()
        {
            var returnedEntities = new List<OwnersEntity>();

            try
            {
                using (var repository = new OwnersRepository())
                {
                    // HERE A CALCULATION CAN BE ADDED AS A COLUMN IN SelectAll function
                    foreach (var entity in repository.SelectAll())
                    {
                        entity.PetYears = GetPetYears(entity.PetAge);
                        returnedEntities.Add(entity);
                    }
                }

                return returnedEntities;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:OwnersBusiness::SelectAllOwners::Error occured.", ex);
            }
        }

        // HERE BELOW A CALCULATION CAN BE ADDED AT THE BusinessLogic Level
        private int GetPetYears(int age)
        {
            var petYears = age;
            petYears = age * 7;
            return petYears;
        }
        // Business Logic environment instantiation
        public OwnersBusiness()
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
