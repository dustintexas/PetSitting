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
    public class SittersRepository : IRepository<SittersEntity>, IDisposable
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
        public bool Insert(SittersEntity entity)
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append("SET DATEFORMAT MDY; ");
                sb.Append("INSERT [dbo].[Sitters] ");
                sb.Append("( ");
                sb.Append("[Name], ");
                sb.Append("[Fee], ");
                sb.Append("[Bio], ");
                sb.Append("[Age], ");
                sb.Append("[HiringDate], ");
                sb.Append("[GrossSalary], ");
                sb.Append("[ModifiedDate] ");
                sb.Append(") ");
                sb.Append("VALUES ");
                sb.Append("( ");
                sb.Append(" @chnName, ");
                sb.Append(" @decFee, ");
                sb.Append(" @chnBio, ");
                sb.Append(" @intAge, ");
                sb.Append(" @dtmHiringDate, ");
                sb.Append(" @decGrossSalary, ");
                sb.Append(" ISNULL(@dtmModifiedDate, (getdate())) ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [Sitters] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnName", CsType.String, ParameterDirection.Input, entity.Name);
                        _dataHandler.AddParameterToCommand(dbCommand, "@decFee", CsType.Decimal, ParameterDirection.Input, entity.Fee);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnBio", CsType.String, ParameterDirection.Input, entity.Bio);
                        _dataHandler.AddParameterToCommand(dbCommand, "@intAge", CsType.Int, ParameterDirection.Input, entity.Age);
                        _dataHandler.AddParameterToCommand(dbCommand, "@dtmHiringDate", CsType.DateTime, ParameterDirection.Input, entity.HiringDate);
                        _dataHandler.AddParameterToCommand(dbCommand, "@decGrossSalary", CsType.Decimal, ParameterDirection.Input, entity.GrossSalary);
                        _dataHandler.AddParameterToCommand(dbCommand, "@dtmModifiedDate", CsType.DateTime, ParameterDirection.Input, null);

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
                            throw new Exception("The Insert method for entity [Sitters] reported the Database ErrorCode: " + _errorCode);
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
                throw new Exception("SittersRepository::Insert::Error occured.", ex);
            }
        }
        // Update function will update given parameters to selected record in SQL Server
        public bool Update(SittersEntity entity)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            try
            {
                var sb = new StringBuilder();
                sb.Append("SET DATEFORMAT MDY; ");
                sb.Append("UPDATE [dbo].[Sitters] ");
                sb.Append("SET ");
                sb.Append("[Name] = @chnName, ");
                sb.Append("[Fee] = @decFee, ");
                sb.Append("[Bio] = @chnBio, ");
                sb.Append("[Age] = @intAge, ");
                sb.Append("[HiringDate] = @dtmHiringDate, ");
                sb.Append("[GrossSalary] = @decGrossSalary, ");
                sb.Append("[ModifiedDate] = ISNULL(@dtmModifiedDate, (getdate())) ");
                sb.Append("WHERE ");
                sb.Append("[SitterID] = @intId ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Update command for entity [Owners] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intId", CsType.Int, ParameterDirection.Input, entity.SitterID);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnName", CsType.String, ParameterDirection.Input, entity.Name);
                        _dataHandler.AddParameterToCommand(dbCommand, "@decFee", CsType.Decimal, ParameterDirection.Input, entity.Fee);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnBio", CsType.String, ParameterDirection.Input, entity.Bio);
                        _dataHandler.AddParameterToCommand(dbCommand, "@intAge", CsType.Int, ParameterDirection.Input, entity.Age);
                        _dataHandler.AddParameterToCommand(dbCommand, "@dtmHiringDate", CsType.DateTime, ParameterDirection.Input, entity.HiringDate);
                        _dataHandler.AddParameterToCommand(dbCommand, "@decGrossSalary", CsType.Decimal, ParameterDirection.Input, entity.GrossSalary);
                        _dataHandler.AddParameterToCommand(dbCommand, "@dtmModifiedDate", CsType.DateTime, ParameterDirection.Input, null);

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
                            throw new Exception("The Update method for entity [Sitters] reported the Database ErrorCode: " + _errorCode);
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
                throw new Exception("SittersRepository::Update::Error occured.", ex);
            }
        }
        // Delete function will delete given record by ID in SQL Server
        public bool DeleteById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            try
            {
                var sb = new StringBuilder();
                sb.Append("DELETE FROM [dbo].[Sitters] ");
                sb.Append("WHERE ");
                sb.Append("[SitterID] = @intId ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Delete command for entity [Sitters] can't be null. ");

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
                            throw new Exception("The Delete method for entity [Sitters] reported the Database ErrorCode: " + _errorCode);
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
                throw new Exception("SittersRepository::Delete::Error occured.", ex);
            }
        }

        // SelectById function will pull given record by ID from SQL Server
        public SittersEntity SelectById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            SittersEntity returnedEntity = null;

            try
            {
                var sb = new StringBuilder();
                sb.Append("SET DATEFORMAT DMY; ");
                sb.Append("SELECT ");
                sb.Append("[SitterID], ");
                sb.Append("[Name], ");
                sb.Append("[Fee], ");
                sb.Append("[Bio], ");
                sb.Append("[Age], ");
                sb.Append("[HiringDate], ");
                sb.Append("[GrossSalary], ");
                sb.Append("[ModifiedDate] ");
                sb.Append("FROM [dbo].[Sitters] ");
                sb.Append("WHERE ");
                sb.Append("[SitterID] = @intId ");
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
                            throw new ArgumentNullException("dbCommand" + " The db SelectById command for entity [Sitters] can't be null. ");

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
                                    var entity = new SittersEntity();
                                    entity.SitterID = reader.GetInt32(0);
                                    entity.Name = reader.GetString(1);
                                    entity.Fee = reader.GetDecimal(2);
                                    entity.Bio = reader.GetString(3);
                                    entity.Age = reader.GetInt32(4);
                                    entity.HiringDate = reader.GetValue(5) == DBNull.Value ? (DateTime?)null : reader.GetDateTime(5);
                                    entity.GrossSalary = reader.GetDecimal(6);
                                    entity.ModifiedDate = reader.GetDateTime(7);
                                    returnedEntity = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The SelectById method for entity [Sitters] reported the Database ErrorCode: " + _errorCode);
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
                throw new Exception("SittersRepository::SelectById::Error occured.", ex);
            }
        }
        public SittersEntity SelectByUserId(int userid)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            SittersEntity returnedEntity = null;

            try
            {
                var sb = new StringBuilder();
                sb.Append("SET DATEFORMAT DMY; ");
                sb.Append("SELECT ");
                sb.Append("[SitterID], ");
                sb.Append("[Name], ");
                sb.Append("[Fee], ");
                sb.Append("[Bio], ");
                sb.Append("[Age], ");
                sb.Append("[HiringDate], ");
                sb.Append("[GrossSalary], ");
                sb.Append("[ModifiedDate] ");
                sb.Append("FROM [dbo].[Sitters] ");
                sb.Append("WHERE ");
                sb.Append("[UserID] = @intUserId ");
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
                            throw new ArgumentNullException("dbCommand" + " The db SelectById command for entity [Sitters] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

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
                                    var entity = new SittersEntity();
                                    entity.SitterID = reader.GetInt32(0);
                                    entity.Name = reader.GetString(1);
                                    entity.Fee = reader.GetDecimal(2);
                                    entity.Bio = reader.GetString(3);
                                    entity.Age = reader.GetInt32(4);
                                    entity.HiringDate = reader.GetValue(5) == DBNull.Value ? (DateTime?)null : reader.GetDateTime(5);
                                    entity.GrossSalary = reader.GetDecimal(6);
                                    entity.ModifiedDate = reader.GetDateTime(7);
                                    returnedEntity = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The SelectById method for entity [Sitters] reported the Database ErrorCode: " + _errorCode);
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
                throw new Exception("SittersRepository::SelectByUserId::Error occured.", ex);
            }
        }
        // SelectAll function will pull all Sitter records from SQL Server
        public List<SittersEntity> SelectAll()
        {
            _errorCode = 0;
            _rowsAffected = 0;

            var returnedEntities = new List<SittersEntity>();

            try
            {
                var sb = new StringBuilder();
                sb.Append("SET DATEFORMAT DMY; ");
                sb.Append("SELECT ");
                sb.Append("[SitterID], ");
                sb.Append("[Name], ");
                sb.Append("[Fee], ");
                sb.Append("[Bio], ");
                sb.Append("[Age], ");
                sb.Append("[HiringDate], ");
                sb.Append("[GrossSalary], ");
                sb.Append("[ModifiedDate] ");
                sb.Append("FROM [dbo].[Sitters] ");
                sb.Append("ORDER BY [Name] ");
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
                            throw new ArgumentNullException("dbCommand" + " The db command for entity [Sitters] can't be null. ");

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
                                    var entity = new SittersEntity();
                                    entity.SitterID = reader.GetInt32(0);
                                    entity.Name = reader.GetString(1);
                                    entity.Fee = reader.GetDecimal(2);
                                    entity.Bio = reader.GetString(3);
                                    entity.Age = reader.GetInt32(4);
                                    entity.HiringDate = reader.GetValue(5) == DBNull.Value ? (DateTime?)null : reader.GetDateTime(5);
                                    entity.GrossSalary = reader.GetDecimal(6);
                                    entity.ModifiedDate = reader.GetDateTime(7);
                                    returnedEntities.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The SelectAll method for entity [Sitters] reported the Database ErrorCode: " + _errorCode);
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
                throw new Exception("SittersRepository::SelectAll::Error occured.", ex);
            }
        }
        // SittersRepository function instantiates environment for handling connection to database, 
        // logging of errors and other system necessities
        public SittersRepository()
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
