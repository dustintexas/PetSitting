
namespace PetSitting.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using PetSitting.Common;
    using PetSitting.DataAccess.Common;
    using PetSitting.Model;
    using System.Web.Helpers;
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
                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [Sitters] can't be null. ");

                       //CODE BELOW INSERTS SITTER USER INFORMATION INTO MULTIPLE TABLES USING STORED PROCEDURE
                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.CommandText = "Sitter_Insert";

                        // PASSWORD HASHING
                        string hashedpassword = Crypto.HashPassword(entity.Password);
                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnUsername", CsType.String, ParameterDirection.Input, entity.Username);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnFirstName", CsType.String, ParameterDirection.Input, entity.FirstName);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnLastName", CsType.String, ParameterDirection.Input, entity.LastName);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnEmail", CsType.String, ParameterDirection.Input, entity.Email);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnPassword", CsType.String, ParameterDirection.Input, hashedpassword);
                        _dataHandler.AddParameterToCommand(dbCommand, "@binIsActive", CsType.Boolean, ParameterDirection.Input, entity.IsActive);
                        _dataHandler.AddParameterToCommand(dbCommand, "@intAge", CsType.Int, ParameterDirection.Input, entity.Age);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnRole", CsType.String, ParameterDirection.Input, entity.Role);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnName", CsType.String, ParameterDirection.Input, entity.Name);
                        _dataHandler.AddParameterToCommand(dbCommand, "@decFee", CsType.Decimal, ParameterDirection.Input, entity.Fee);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnBio", CsType.String, ParameterDirection.Input, entity.Bio);
                        _dataHandler.AddParameterToCommand(dbCommand, "@dtmHiringDate", CsType.DateTime, ParameterDirection.Input, entity.HiringDate);
                        _dataHandler.AddParameterToCommand(dbCommand, "@decGrossSalary", CsType.Decimal, ParameterDirection.Input, entity.GrossSalary);

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
                // INSTANTIATE DATABASE CONNECTION VARIABLE
                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");
                    
                    // GET DATABASE CONNECTION STRING FROM COMMON TIER
                    dbConnection.ConnectionString = _connectionString;
                    
                    // INSTANTIATE DATABASE COMMAND VARIABLE
                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db Update command for entity [Sitters] can't be null. ");

                        // BUILD SQL COMMAND CONNECTING TO DBCONNECTION AND SPECIFYING COMMANDTYPE AS STORED PROCEDURE
                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.CommandText = "UpdateSitterById";

                        // PASSWORD HASHING
                        string hashedpassword = Crypto.HashPassword(entity.Password);

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intSitterID", CsType.Int, ParameterDirection.Input, entity.SitterID);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnName", CsType.String, ParameterDirection.Input, entity.Name);
                        _dataHandler.AddParameterToCommand(dbCommand, "@decFee", CsType.Decimal, ParameterDirection.Input, entity.Fee);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnBio", CsType.String, ParameterDirection.Input, entity.Bio);
                        _dataHandler.AddParameterToCommand(dbCommand, "@intAge", CsType.Int, ParameterDirection.Input, entity.Age);
                        _dataHandler.AddParameterToCommand(dbCommand, "@dtmHiringDate", CsType.DateTime, ParameterDirection.Input, entity.HiringDate);
                        _dataHandler.AddParameterToCommand(dbCommand, "@decGrossSalary", CsType.Decimal, ParameterDirection.Input, entity.GrossSalary);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnUsername", CsType.String, ParameterDirection.Input, entity.Username);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnFirstName", CsType.String, ParameterDirection.Input, entity.FirstName);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnLastName", CsType.String, ParameterDirection.Input, entity.LastName);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnEmail", CsType.String, ParameterDirection.Input, entity.Email);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnPassword", CsType.String, ParameterDirection.Input, hashedpassword);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnRole", CsType.String, ParameterDirection.Input, entity.Role);

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
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.CommandText = "DeleteSitterById";

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intSitterId", CsType.Int, ParameterDirection.Input, id);

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
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.CommandText = "SelectSitterById";

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intSitterID", CsType.Int, ParameterDirection.Input, id);

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
                                    entity.SitterID = (int)reader["SitterID"];
                                    entity.Name = (string)reader["Name"];
                                    entity.Fee = (decimal)reader["Fee"];
                                    entity.Bio = (string)reader["Bio"];
                                    entity.Age = (int)reader["SitterAge"];
                                    entity.HiringDate = reader["HiringDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["HiringDate"];
                                    entity.SessionsCount = (int)reader["SessionsCount"];
                                    entity.TotalSales = (decimal)reader["TotalSales"];
                                    entity.GrossSalary = (decimal)reader["GrossSalary"];
                                    entity.ModifiedDate = (DateTime)reader["ModifiedDate"];
                                    entity.Username = (string)reader["UserName"];
                                    entity.FirstName = (string)reader["FirstName"];
                                    entity.LastName = (string)reader["LastName"];
                                    entity.Email = (string)reader["Email"];
                                    entity.Password = (string)reader["Password"];
                                    entity.Role = (string)reader["Role"];
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
                SessionsRepository SeshRepo = new SessionsRepository();
                returnedEntity.Sessions = SeshRepo.SelectALLBySitterId(returnedEntity.SitterID);
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
        // SelectByUserId function will get record by UserID from SQL Server
        public SittersEntity SelectByUserId(int userid)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            SittersEntity returnedEntity = null;

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
                            throw new ArgumentNullException("dbCommand" + " The db SelectById command for entity [Sitters] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.CommandText = "SelectSitterByUserId";

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
                                    entity.SitterID = (int)reader["SitterID"];
                                    entity.Name = (string)reader["Name"];
                                    entity.Fee = (decimal)reader["Fee"];
                                    entity.Bio = (string)reader["Bio"];
                                    entity.Age = (int)reader["SitterAge"];
                                    entity.HiringDate = reader["HiringDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["HiringDate"];
                                    entity.GrossSalary = (decimal)reader["GrossSalary"];
                                    entity.Username = (string)reader["Username"];
                                    entity.FirstName = (string)reader["FirstName"];
                                    entity.LastName = (string)reader["LastName"];
                                    entity.Email = (string)reader["Email"];
                                    entity.Password = (string)reader["Password"];
                                    entity.IsActive = (bool)reader["IsActive"];
                                    entity.Role = (string)reader["Role"];
                                    entity.TotalSales = (decimal)reader["TotalSales"];
                                    entity.ModifiedDate = (DateTime)reader["ModifiedDate"];
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
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.CommandText = "SelectALLSitters";

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
                                    //LOOP THROUGH DATA RESULTS AND ASSIGN TO MODEL CLASS
                                    var entity = new SittersEntity();
                                    entity.SitterID = (int)reader["SitterID"];
                                    entity.Name = (string)reader["Name"];
                                    entity.Fee = (decimal)reader["Fee"];
                                    entity.Bio = (string)reader["Bio"];
                                    entity.Age = (int)reader["SitterAge"];
                                    entity.HiringDate = reader["HiringDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["HiringDate"];
                                    entity.GrossSalary = (decimal)reader["GrossSalary"];
                                    entity.ModifiedDate = (DateTime)reader["ModifiedDate"];
                                    entity.SessionsCount = (int)reader["SessionsCount"];
                                    entity.TotalSales = (decimal)reader["TotalSales"];
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
