using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetSitting.Common;
using PetSitting.DataAccess.Common;
using PetSitting.Model;

namespace PetSitting.DataAccess
{
    public class OwnersRepository : IRepository<OwnersEntity>, IDisposable
    {
        #region Private Declarations 

        private LoggingHandler _loggingHandler;
        private DataHandler _dataHandler;
        private ConfigurationHandler _configurationHandler;
        private DbProviderFactory _dbProviderFactory;
        private string _connectionString;
        private string _connectionProvider;
        private int _errorCode, _rowsAffected;
        private bool _bDisposed;

        #endregion

        #region Class Methods 
        // The following function will insert data from the Create view
        public bool Insert(OwnersEntity entity)
        {
            try
            {
                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [Owners] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.CommandText = "Owner_Insert";

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnUsername", CsType.String, ParameterDirection.Input, entity.Username);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnFirstName", CsType.String, ParameterDirection.Input, entity.FirstName);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnLastName", CsType.String, ParameterDirection.Input, entity.LastName);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnEmail", CsType.String, ParameterDirection.Input, entity.Email);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnPassword", CsType.String, ParameterDirection.Input, entity.Password);
                        _dataHandler.AddParameterToCommand(dbCommand, "@binIsActive", CsType.Boolean, ParameterDirection.Input, entity.IsActive);
                        _dataHandler.AddParameterToCommand(dbCommand, "@intAge", CsType.Int, ParameterDirection.Input, entity.Age);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnRole", CsType.String, ParameterDirection.Input, entity.Role);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnOwnerName", CsType.String, ParameterDirection.Input, entity.OwnerName);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnPetName", CsType.String, ParameterDirection.Input, entity.PetName);
                        _dataHandler.AddParameterToCommand(dbCommand, "@intPetAge", CsType.Int, ParameterDirection.Input, entity.PetAge);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnContactPhone", CsType.String, ParameterDirection.Input, entity.ContactPhone);
                        
                        //Output Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The Insert method for entity [Owners] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                //Bubble error to caller and encapsulate Exception object
                throw new Exception("OwnersRepository::Insert::Error occured.", ex);
            }
        }

        // The following code updates data presented from the Edit view
        public bool Update(OwnersEntity entity)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            try
            {
                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db Update command for entity [Owners] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.CommandText = "UpdateOwnerById";

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intOwnerID", CsType.Int, ParameterDirection.Input, entity.OwnerID);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnOwnerName", CsType.String, ParameterDirection.Input, entity.OwnerName);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnPetName", CsType.String, ParameterDirection.Input, entity.PetName);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnContactPhone", CsType.String, ParameterDirection.Input, entity.ContactPhone);
                        _dataHandler.AddParameterToCommand(dbCommand, "@intPetAge", CsType.Int, ParameterDirection.Input, entity.PetAge);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnUsername", CsType.String, ParameterDirection.Input, entity.Username);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnFirstName", CsType.String, ParameterDirection.Input, entity.FirstName);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnLastName", CsType.String, ParameterDirection.Input, entity.LastName);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnEmail", CsType.String, ParameterDirection.Input, entity.Email);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnPassword", CsType.String, ParameterDirection.Input, entity.Password);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnRole", CsType.String, ParameterDirection.Input, entity.Role);
                        _dataHandler.AddParameterToCommand(dbCommand, "@intAge", CsType.Int, ParameterDirection.Input, entity.Age);
                        _dataHandler.AddParameterToCommand(dbCommand, "@bitIsActive", CsType.Boolean, ParameterDirection.Input, entity.IsActive);
                        
                        //Output Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The Update method for entity [Owners] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                //Bubble error to caller and encapsulate Exception object
                throw new Exception("OwnersRepository::Update::Error occured.", ex);
            }
        }

        // The following code deletes from the database based on a given record ID from the delete view
        public bool DeleteById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            try
            {
                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db Delete command for entity [Owners] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.CommandText = "DeleteOwnerById";

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intOwnerId", CsType.Int, ParameterDirection.Input, id);

                        //Output Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The Delete method for entity [Owners] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                //Bubble error to caller and encapsulate Exception object
                throw new Exception("OwnersRepository::Delete::Error occured.", ex);
            }
        }

        // the following code will request data from the database based on a given record ID
        public OwnersEntity SelectById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            OwnersEntity returnedEntity = null;

            try
            {
                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db SelectById command for entity [Owners] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.CommandText = "SelectOwnerById";

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intOwnerID", CsType.Int, ParameterDirection.Input, id);

                        //Output Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        using (var reader = dbCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var entity = new OwnersEntity();
                                    entity.OwnerID = reader.GetInt32(0);
                                    entity.OwnerName = reader.GetString(1);
                                    entity.PetName = reader.GetString(2);
                                    entity.ContactPhone = reader.GetString(3);
                                    entity.PetAge = reader.GetInt32(4);
                                    entity.ModifiedDate = reader.GetDateTime(5);
                                    entity.Username = reader.GetString(6);
                                    entity.FirstName = reader.GetString(7);
                                    entity.LastName = reader.GetString(8);
                                    entity.Email = reader.GetString(9);
                                    entity.Password = reader.GetString(10);
                                    entity.Age = reader.GetInt32(11);
                                    entity.Role = reader.GetString(12);
                                    returnedEntity = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The SelectById method for entity [Owners] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }
                SessionsRepository SeshRepo = new SessionsRepository();
                returnedEntity.Sessions = SeshRepo.SelectALLByOwnerId(returnedEntity.OwnerID);
                return returnedEntity;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                //Bubble error to caller and encapsulate Exception object
                throw new Exception("OwnersRepository::SelectById::Error occured.", ex);
            }
        }

        public OwnersEntity SelectByUserId(int userid)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            OwnersEntity returnedEntity = null;

            try
            {
                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db SelectById command for entity [Owners] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.CommandText = "SelectOwnerByUserId";

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intUserId", CsType.Int, ParameterDirection.Input, userid);

                        //Output Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        using (var reader = dbCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var entity = new OwnersEntity();
                                    entity.OwnerID = reader.GetInt32(0);
                                    entity.OwnerName = reader.GetString(1);
                                    entity.PetName = reader.GetString(2);
                                    entity.ContactPhone = reader.GetString(3);
                                    entity.PetAge = reader.GetInt32(4);
                                    entity.ModifiedDate = reader.GetDateTime(5);
                                    returnedEntity = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The SelectById method for entity [Owners] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                return returnedEntity;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                //Bubble error to caller and encapsulate Exception object
                throw new Exception("OwnersRepository::SelectByUserId::Error occured.", ex);
            }
        }
        // the following code will request all data from the owners table as a List
        public List<OwnersEntity> SelectAll()
        {
            _errorCode = 0;
            _rowsAffected = 0;

            var returnedEntities = new List<OwnersEntity>();

            try
            {
                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db command for entity [Owners] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.CommandText = "SelectAllOwners";

                        //Input Parameters - None

                        //Output Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        using (var reader = dbCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var entity = new OwnersEntity();
                                    entity.OwnerID = reader.GetInt32(0);
                                    entity.OwnerName = reader.GetString(1);
                                    entity.PetName = reader.GetString(2);
                                    entity.ContactPhone = reader.GetString(3);
                                    entity.PetAge = reader.GetInt32(4);
                                    entity.ModifiedDate = reader.GetDateTime(5);
                                    returnedEntities.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The SelectAll method for entity [Owners] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                return returnedEntities;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                //Bubble error to caller and encapsulate Exception object
                throw new Exception("OwnersRepository::SelectAll::Error occured.", ex);
            }
        }

        // the following code initializes or prepares the environment for handling certain parts of the data used
        public OwnersRepository()
        {
            //Repository Initializations 
            _configurationHandler = new ConfigurationHandler();
            _loggingHandler = new LoggingHandler();
            _dataHandler = new DataHandler();
            _connectionString = _configurationHandler.ConnectionString;
            _connectionProvider = _configurationHandler.ConnectionProvider;
            _dbProviderFactory = DbProviderFactories.GetFactory(_connectionProvider);
        }

        // the following 2 pieces of code is just used to close out used information so it is free from memory
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
                    _configurationHandler = null;
                    _loggingHandler = null;
                    _dataHandler = null;
                    _dbProviderFactory = null;
                }
            }
            _bDisposed = true;
        }
        #endregion
    }
}
