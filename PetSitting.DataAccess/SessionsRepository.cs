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
    public class SessionsRepository : IRepository<SessionsEntity>, IDisposable
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
        // Insert function will insert given parameters into record in SQL Server
        public bool Insert(SessionsEntity entity)
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [Sessions] can't be null. ");

                        // CODE BELOW USES A STORED PROCEDURE TO INSERT INTO SESSIONS TABLES
                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.CommandText = "Session_Insert";

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intSitterID", CsType.Int, ParameterDirection.Input, entity.SitterID);
                        _dataHandler.AddParameterToCommand(dbCommand, "@intOwnerID", CsType.Int, ParameterDirection.Input, entity.OwnerID);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnStatus", CsType.String, ParameterDirection.Input, entity.Status);
                        _dataHandler.AddParameterToCommand(dbCommand, "@decFee", CsType.Decimal, ParameterDirection.Input, entity.Fee);
                        _dataHandler.AddParameterToCommand(dbCommand, "@dtmDate", CsType.DateTime, ParameterDirection.Input, entity.Date);
                        
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
                            throw new Exception("The Insert method for entity [Sessions] reported the Database ErrorCode: " + _errorCode);
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
                throw new Exception("SessionsRepository::Insert::Error occured.", ex);
            }
        }
        // Update function will update given parameters to selected record in SQL Server
        public bool Update(SessionsEntity entity)
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
                            throw new ArgumentNullException("dbCommand" + " The db Update command for entity [Sessions] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.CommandText = "Session_Update";

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intSitterID", CsType.Int, ParameterDirection.Input, entity.SitterID);
                        _dataHandler.AddParameterToCommand(dbCommand, "@intOwnerID", CsType.Int, ParameterDirection.Input, entity.OwnerID);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnStatus", CsType.String, ParameterDirection.Input, entity.Status);
                        _dataHandler.AddParameterToCommand(dbCommand, "@intId", CsType.Int, ParameterDirection.Input, entity.SessionID);
                        _dataHandler.AddParameterToCommand(dbCommand, "@decFee", CsType.Decimal, ParameterDirection.Input, entity.Fee);
                        _dataHandler.AddParameterToCommand(dbCommand, "@dtmDate", CsType.DateTime, ParameterDirection.Input, entity.Date);
                        
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
                            throw new Exception("The Update method for entity [Sessions] reported the Database ErrorCode: " + _errorCode);
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
                throw new Exception("SessionsRepository::Update::Error occured.", ex);
            }
        }
        // Delete function will delete given record by ID in SQL Server
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
                            throw new ArgumentNullException("dbCommand" + " The db Delete command for entity [Sessions] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.CommandText = "DeleteSessionById";

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intId", CsType.Int, ParameterDirection.Input, id);

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
                            throw new Exception("The Delete method for entity [Sessions] reported the Database ErrorCode: " + _errorCode);
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
                throw new Exception("SessionsRepository::Delete::Error occured.", ex);
            }
        }

        // SelectById function will pull given record by ID from SQL Server
        public SessionsEntity SelectById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            SessionsEntity returnedEntity = null;

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
                            throw new ArgumentNullException("dbCommand" + " The db SelectById command for entity [Sessions] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.CommandText = "SelectSessionById";

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intId", CsType.Int, ParameterDirection.Input, id);

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
                                    var entity = new SessionsEntity();
                                    entity.SessionID = reader.GetInt32(0);
                                    entity.SitterID = reader.GetInt32(1);
                                    entity.SitterName = reader.GetString(2);
                                    entity.OwnerID = reader.GetInt32(3);
                                    entity.OwnerName = reader.GetString(4);
                                    entity.Status = reader.GetString(5);
                                    entity.Date = reader.GetValue(6) == DBNull.Value ? (DateTime?)null : reader.GetDateTime(6);
                                    entity.Fee = reader.GetDecimal(7);
                                    returnedEntity = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The SelectById method for entity [Sessions] reported the Database ErrorCode: " + _errorCode);
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
                throw new Exception("SessionsRepository::SelectById::Error occured.", ex);
            }
        }

        public List<SessionsEntity> SelectALLBySitterId(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            List<SessionsEntity> returnedEntity = new List<SessionsEntity>();

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
                            throw new ArgumentNullException("dbCommand" + " The db SelectALLBySitterId command for entity [Sessions] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.CommandText = "SelectALLSessionBySitterId";

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intId", CsType.Int, ParameterDirection.Input, id);

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
                                    var entity = new SessionsEntity();

                                    entity.OwnerName = reader.GetString(0);
                                    entity.PetName = reader.GetString(1);
                                    entity.Date = reader.GetValue(2) == DBNull.Value ? (DateTime?)null : reader.GetDateTime(2);
                                    entity.Fee = reader.GetDecimal(3);
                                    entity.Status = reader.GetString(4);
                                    entity.SessionID = reader.GetInt32(5);
                                    returnedEntity.Add(entity);

                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The SelectById method for entity [Sessions] reported the Database ErrorCode: " + _errorCode);
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
                throw new Exception("SessionsRepository::SelectById::Error occured.", ex);
            }
        }

        public List<SessionsEntity> SelectALLByOwnerId(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            List<SessionsEntity> returnedEntity = new List<SessionsEntity>();

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
                            throw new ArgumentNullException("dbCommand" + " The db SelectById command for entity [Sessions] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.CommandText = "SelectALLSessionByOwnerId";

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intId", CsType.Int, ParameterDirection.Input, id);

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
                                    var entity = new SessionsEntity();

                                    entity.SitterName = reader.GetString(0);
                                    entity.Date = reader.GetValue(1) == DBNull.Value ? (DateTime?)null : reader.GetDateTime(1);
                                    entity.Fee = reader.GetDecimal(2);
                                    entity.Status = reader.GetString(3);
                                    entity.SessionID = reader.GetInt32(4);
                                    returnedEntity.Add(entity);

                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The SelectById method for entity [Sessions] reported the Database ErrorCode: " + _errorCode);
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
                throw new Exception("SessionsRepository::SelectById::Error occured.", ex);
            }
        }

        // SelectAll function will pull all Sitter records from SQL Server
        public List<SessionsEntity> SelectAll()
        {
            _errorCode = 0;
            _rowsAffected = 0;

            var returnedEntities = new List<SessionsEntity>();

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
                            throw new ArgumentNullException("dbCommand" + " The db command for entity [Sessions] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.CommandText = "SelectALLSessions";

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
                                    var entity = new SessionsEntity();
                                    entity.SessionID = reader.GetInt32(0);
                                    entity.SitterID = reader.GetInt32(1);
                                    entity.SitterName = reader.GetString(2);
                                    entity.OwnerID = reader.GetInt32(3);
                                    entity.OwnerName = reader.GetString(4);
                                    entity.Date = reader.GetValue(5) == DBNull.Value ? (DateTime?)null : reader.GetDateTime(5);
                                    entity.Status = reader.GetString(6);
                                    entity.Fee = reader.GetDecimal(7);
                                    returnedEntities.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The SelectAll method for entity [Sessions] reported the Database ErrorCode: " + _errorCode);
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
                throw new Exception("SessionsRepository::SelectAll::Error occured.", ex);
            }
        }
        // SittersRepository function instantiates environment for handling connection to database, 
        // logging of errors and other system necessities
        public SessionsRepository()
        {
            //Repository Initializations 
            _configurationHandler = new ConfigurationHandler();
            _loggingHandler = new LoggingHandler();
            _dataHandler = new DataHandler();
            _connectionString = _configurationHandler.ConnectionString;
            _connectionProvider = _configurationHandler.ConnectionProvider;
            _dbProviderFactory = DbProviderFactories.GetFactory(_connectionProvider);
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
