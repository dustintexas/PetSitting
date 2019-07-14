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
    public class UsersRepository : IRepository<UsersEntity>, IDisposable
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
        public bool Insert(UsersEntity entity)
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append("SET DATEFORMAT DMY; ");
                sb.Append("INSERT [dbo].[Users] ");
                sb.Append("( ");
                sb.Append("[Username], ");
                sb.Append("[FirstName], ");
                sb.Append("[LastName], ");
                sb.Append("[Email], ");
                sb.Append("[Password], ");
                sb.Append("[Age], ");
                sb.Append("[IsActive] ");
                sb.Append(") ");
                sb.Append("VALUES ");
                sb.Append("( ");
                sb.Append(" @chnUsername, ");
                sb.Append(" @chnFirstName, ");
                sb.Append(" @chnLastName, ");
                sb.Append(" @chnEmail, ");
                sb.Append(" @chnPassword, ");
                sb.Append(" @intAge, ");
                sb.Append(" @binIsActive ");
                sb.Append(") ");
                sb.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = sb.ToString();
                sb.Clear();

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
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnUsername", CsType.String, ParameterDirection.Input, entity.Username);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnFirstName", CsType.String, ParameterDirection.Input, entity.FirstName);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnLastName", CsType.String, ParameterDirection.Input, entity.LastName);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnEmail", CsType.String, ParameterDirection.Input, entity.Email);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnPassword", CsType.String, ParameterDirection.Input, entity.Password);
                        _dataHandler.AddParameterToCommand(dbCommand, "@intAge", CsType.Int, ParameterDirection.Input, entity.Age);
                        _dataHandler.AddParameterToCommand(dbCommand, "@binIsActive", CsType.Boolean, ParameterDirection.Input, entity.IsActive);

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
                            throw new Exception("The Insert method for entity [Users] reported the Database ErrorCode: " + _errorCode);
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
                throw new Exception("UsersRepository::Insert::Error occured.", ex);
            }
        }
        // The following code updates data presented from the Edit view
        public bool Update(UsersEntity entity)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            try
            {
                var sb = new StringBuilder();
                sb.Append("SET DATEFORMAT MDY; ");
                sb.Append("UPDATE [dbo].[Users] ");
                sb.Append("SET ");
                sb.Append("[Username] = @chnUsername, ");
                sb.Append("[FirstName] = @chnFirstName, ");
                sb.Append("[LastName] = @chnLastName, ");
                sb.Append("[Email] = @chnEmail, ");
                sb.Append("[Password] = @chnPassword, ");
                sb.Append("[Age] = @intAge, ");
                sb.Append("[IsActive] = @binIsActive ");
                sb.Append("WHERE ");
                sb.Append("[UserID] = @intId ");
                sb.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = sb.ToString();
                sb.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db Update command for entity [Users] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intId", CsType.Int, ParameterDirection.Input, entity.UserID);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnUsername", CsType.String, ParameterDirection.Input, entity.Username);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnFirstName", CsType.String, ParameterDirection.Input, entity.FirstName);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnLastName", CsType.String, ParameterDirection.Input, entity.LastName);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnEmail", CsType.String, ParameterDirection.Input, entity.Email);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnPassword", CsType.String, ParameterDirection.Input, entity.Password);
                        _dataHandler.AddParameterToCommand(dbCommand, "@intAge", CsType.Int, ParameterDirection.Input, entity.Age);
                        _dataHandler.AddParameterToCommand(dbCommand, "@binIsActive", CsType.Boolean, ParameterDirection.Input, entity.IsActive);

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
                            throw new Exception("The Update method for entity [Users] reported the Database ErrorCode: " + _errorCode);
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
                throw new Exception("UsersRepository::Update::Error occured.", ex);
            }
        }
        // The following code deletes from the database based on a given record ID from the delete view
        public bool DeleteById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            try
            {
                var sb = new StringBuilder();
                sb.Append("DELETE FROM [dbo].[Users] ");
                sb.Append("WHERE ");
                sb.Append("[UserID] = @intId ");
                sb.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = sb.ToString();
                sb.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db Delete command for entity [Users] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

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
                            throw new Exception("The Delete method for entity [Users] reported the Database ErrorCode: " + _errorCode);
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
                throw new Exception("UsersRepository::Delete::Error occured.", ex);
            }
        }
        // the following code will request data from the database based on a given record ID
        public UsersEntity SelectById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            UsersEntity returnedEntity = null;

            try
            {
                var sb = new StringBuilder();
                sb.Append("SET DATEFORMAT MDY; ");
                sb.Append("SELECT ");
                sb.Append("[UserID], ");
                sb.Append("[Username], ");
                sb.Append("[FirstName], ");
                sb.Append("[LastName], ");
                sb.Append("[Email], ");
                sb.Append("[Password], ");
                sb.Append("[Age], ");
                sb.Append("[IsActive] ");
                sb.Append("FROM [dbo].[Users] ");
                sb.Append("WHERE ");
                sb.Append("[UserID] = @intId ");
                sb.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = sb.ToString();
                sb.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db SelectById command for entity [Users] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

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
                                    var entity = new UsersEntity();
                                    entity.UserID = reader.GetInt32(0);
                                    entity.Username = reader.GetString(1);
                                    entity.FirstName = reader.GetString(2);
                                    entity.LastName = reader.GetString(3);
                                    entity.Email = reader.GetString(4);
                                    entity.Password = reader.GetString(5);
                                    entity.Age = reader.GetInt32(6);
                                    entity.IsActive = reader.GetBoolean(7);
                                    returnedEntity = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The SelectById method for entity [Users] reported the Database ErrorCode: " + _errorCode);
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
                throw new Exception("UsersRepository::SelectById::Error occured.", ex);
            }
        }

        // the following code will request all data from the owners table as a List
        public List<UsersEntity> SelectAll()
        {
            _errorCode = 0;
            _rowsAffected = 0;

            var returnedEntities = new List<UsersEntity>();

            try
            {
                var sb = new StringBuilder();
                sb.Append("SET DATEFORMAT MDY; ");
                sb.Append("SELECT ");
                sb.Append("[UserID], ");
                sb.Append("[Username], ");
                sb.Append("[FirstName], ");
                sb.Append("[LastName], ");
                sb.Append("[Email], ");
                sb.Append("[Password], ");
                sb.Append("[Age], ");
                sb.Append("[IsActive] ");
                sb.Append("FROM [dbo].[Users] ");
                sb.Append("ORDER BY [Username] ");
                sb.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = sb.ToString();
                sb.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db command for entity [Users] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

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
                                    var entity = new UsersEntity();
                                    entity.UserID = reader.GetInt32(0);
                                    entity.Username = reader.GetString(1);
                                    entity.FirstName = reader.GetString(2);
                                    entity.LastName = reader.GetString(3);
                                    entity.Email = reader.GetString(4);
                                    entity.Password = reader.GetString(5);
                                    entity.Age = reader.GetInt32(6);
                                    entity.IsActive = reader.GetBoolean(7);
                                    returnedEntities.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The SelectAll method for entity [Users] reported the Database ErrorCode: " + _errorCode);
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
                throw new Exception("UsersRepository::SelectAll::Error occured.", ex);
            }
        }
        // the following code initializes or prepares the environment for handling certain parts of the data used
        public UsersRepository()
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
