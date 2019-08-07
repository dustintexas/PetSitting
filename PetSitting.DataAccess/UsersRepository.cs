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
using System.Web.Helpers;

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
                        if (entity.Role == "Sitter")
                        {
                            dbCommand.CommandText = "Sitter_Insert";
                        } else if (entity.Role == "Owner")
                        {
                            dbCommand.CommandText = "Owner_Insert";
                        }
                        string hashedpassword = Crypto.HashPassword(entity.Password);
                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnUsername", CsType.String, ParameterDirection.Input, entity.Username);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnFirstName", CsType.String, ParameterDirection.Input, entity.FirstName);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnLastName", CsType.String, ParameterDirection.Input, entity.LastName);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnEmail", CsType.String, ParameterDirection.Input, entity.Email);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnPassword", CsType.String, ParameterDirection.Input, hashedpassword);
                        _dataHandler.AddParameterToCommand(dbCommand, "@intAge", CsType.Int, ParameterDirection.Input, entity.Age);
                        _dataHandler.AddParameterToCommand(dbCommand, "@binIsActive", CsType.Boolean, ParameterDirection.Input, entity.IsActive);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnRole", CsType.String, ParameterDirection.Input, entity.Role);
                        
                        // Insert User record information based on role
                        if (entity.Role == "Sitter")
                        {
                            _dataHandler.AddParameterToCommand(dbCommand, "@chnName", CsType.String, ParameterDirection.Input, entity.Name);
                            _dataHandler.AddParameterToCommand(dbCommand, "@decFee", CsType.Decimal, ParameterDirection.Input, entity.Fee);
                            _dataHandler.AddParameterToCommand(dbCommand, "@chnBio", CsType.String, ParameterDirection.Input, entity.Bio);
                            _dataHandler.AddParameterToCommand(dbCommand, "@dtmHiringDate", CsType.DateTime, ParameterDirection.Input, entity.HiringDate);
                            _dataHandler.AddParameterToCommand(dbCommand, "@decGrossSalary", CsType.Decimal, ParameterDirection.Input, entity.GrossSalary);

                        } 
                        else if (entity.Role == "Owner")
                        {
                            _dataHandler.AddParameterToCommand(dbCommand, "@chnOwnerName", CsType.String, ParameterDirection.Input, entity.OwnerName);
                            _dataHandler.AddParameterToCommand(dbCommand, "@chnPetName", CsType.String, ParameterDirection.Input, entity.PetName);
                            _dataHandler.AddParameterToCommand(dbCommand, "@intPetAge", CsType.Int, ParameterDirection.Input, entity.PetAge);
                            _dataHandler.AddParameterToCommand(dbCommand, "@chnContactPhone", CsType.String, ParameterDirection.Input, entity.ContactPhone);
                            
                        }

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

                #region Depricated code By Role
                /* Depricated code
                 * 
                if (entity.Role == "Owner")
                {
                    var newuserid = 0;
                    sb.Clear();
                    sb.Append("SET DATEFORMAT DMY; ");
                    sb.Append("SELECT UserID ");
                    sb.Append("FROM [Users] ");
                    sb.Append("WHERE Username = @chnUsername ");
                    sb.Append("SELECT @intErrorCode=@@ERROR; ");

                    commandText = sb.ToString();
                    sb.Clear();

                    using (var dbConnection = _dbProviderFactory.CreateConnection())
                    {
                        if (dbConnection == null)
                            throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                        dbConnection.ConnectionString = _connectionString;

                        using (var dbCommand = _dbProviderFactory.CreateCommand())
                        {
                            if (dbCommand == null)
                                throw new ArgumentNullException("dbCommand" + " The db Get command for entity [Owners] can't be null. ");

                            dbCommand.Connection = dbConnection;
                            dbCommand.CommandText = commandText;
                            //Input Parameters
                            _dataHandler.AddParameterToCommand(dbCommand, "@chnUsername", CsType.String, ParameterDirection.Input, entity.Username);

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
                                        newuserid = reader.GetInt32(0);
                                    }
                                }
                            }
                        }
                    }
                    if (newuserid != 0)
                    {
                        sb.Clear();
                        sb.Append("SET DATEFORMAT DMY; ");
                        sb.Append("INSERT [Owners] ");
                        sb.Append("( ");
                        sb.Append("[OwnerName], ");
                        sb.Append("[PetName], ");
                        sb.Append("[PetAge], ");
                        sb.Append("[ContactPhone], ");
                        sb.Append("[ModifiedDate], ");
                        sb.Append("[UserID] ");
                        sb.Append(") ");
                        sb.Append("VALUES (");
                        sb.Append("@chnOwnerName, ");
                        sb.Append("@chnPetName, ");
                        sb.Append("@intPetAge, ");
                        sb.Append("@chnContactPhone, ");
                        sb.Append("ISNULL(@dtmModifiedDate, (getdate())), ");
                        sb.Append("@intUserID ");
                        sb.Append(") ");
                        sb.Append("SELECT @intErrorCode=@@ERROR; ");

                        commandText = sb.ToString();
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
                                _dataHandler.AddParameterToCommand(dbCommand, "@chnOwnerName", CsType.String, ParameterDirection.Input, entity.OwnerName);
                                _dataHandler.AddParameterToCommand(dbCommand, "@chnPetName", CsType.String, ParameterDirection.Input, entity.PetName);
                                _dataHandler.AddParameterToCommand(dbCommand, "@intPetAge", CsType.Int, ParameterDirection.Input, entity.PetAge);
                                _dataHandler.AddParameterToCommand(dbCommand, "@chnContactPhone", CsType.String, ParameterDirection.Input, entity.ContactPhone);
                                _dataHandler.AddParameterToCommand(dbCommand, "@intUserID", CsType.Int, ParameterDirection.Input, newuserid);
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
                                    throw new Exception("The Insert method for entity [Owners] reported the Database ErrorCode: " + _errorCode);
                                }

                            }
                        }
                    }
                }
                if (entity.Role == "Sitter")
                {
                    var newuserid = 0;
                    sb.Clear();
                    sb.Append("SET DATEFORMAT DMY; ");
                    sb.Append("SELECT UserID ");
                    sb.Append("FROM [Users] ");
                    sb.Append("WHERE Username = @chnUsername ");
                    sb.Append("SELECT @intErrorCode=@@ERROR; ");

                    commandText = sb.ToString();
                    sb.Clear();

                    using (var dbConnection = _dbProviderFactory.CreateConnection())
                    {
                        if (dbConnection == null)
                            throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                        dbConnection.ConnectionString = _connectionString;

                        using (var dbCommand = _dbProviderFactory.CreateCommand())
                        {
                            if (dbCommand == null)
                                throw new ArgumentNullException("dbCommand" + " The db Get command for entity [Owners] can't be null. ");

                            dbCommand.Connection = dbConnection;
                            dbCommand.CommandText = commandText;
                            //Input Parameters
                            _dataHandler.AddParameterToCommand(dbCommand, "@chnUsername", CsType.String, ParameterDirection.Input, entity.Username);

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
                                        newuserid = reader.GetInt32(0);
                                    }
                                }
                            }
                        }
                    }
                    if (newuserid != 0)
                    {
                        sb.Clear();
                        sb.Append("SET DATEFORMAT DMY; ");
                        sb.Append("INSERT [Sitters] ");
                        sb.Append("( ");
                        sb.Append("[Name], ");
                        sb.Append("[Fee], ");
                        sb.Append("[Bio], ");
                        sb.Append("[Age], ");
                        sb.Append("[HiringDate], ");
                        sb.Append("[GrossSalary], ");
                        sb.Append("[ModifiedDate], ");
                        sb.Append("[UserID] ");
                        sb.Append(") ");
                        sb.Append("VALUES (");
                        sb.Append("@chnName, ");
                        sb.Append("@decFee, ");
                        sb.Append("@chnBio, ");
                        sb.Append("@intAge, ");
                        sb.Append("@dtmHiringDate, ");
                        sb.Append("@decGrossSalary, ");
                        sb.Append("ISNULL(@dtmModifiedDate, (getdate())), ");
                        sb.Append("@intUserID ");
                        sb.Append(") ");
                        sb.Append("SELECT @intErrorCode=@@ERROR; ");

                        commandText = sb.ToString();
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
                                _dataHandler.AddParameterToCommand(dbCommand, "@chnName", CsType.String, ParameterDirection.Input, entity.Name);
                                _dataHandler.AddParameterToCommand(dbCommand, "@decFee", CsType.Decimal, ParameterDirection.Input, entity.Fee);
                                _dataHandler.AddParameterToCommand(dbCommand, "@chnBio", CsType.String, ParameterDirection.Input, entity.Bio);
                                _dataHandler.AddParameterToCommand(dbCommand, "@intAge", CsType.Int, ParameterDirection.Input, entity.Age);
                                _dataHandler.AddParameterToCommand(dbCommand, "@intUserID", CsType.Int, ParameterDirection.Input, newuserid);
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
                                    throw new Exception("The Insert method for entity [Owners] reported the Database ErrorCode: " + _errorCode);
                                }

                            }
                        }
                    }
                }
                */
                #endregion
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
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.CommandText = "UpdateUserById";

                        string hashedpassword = Crypto.HashPassword(entity.Password);
                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intId", CsType.Int, ParameterDirection.Input, entity.UserID);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnUsername", CsType.String, ParameterDirection.Input, entity.Username);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnFirstName", CsType.String, ParameterDirection.Input, entity.FirstName);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnLastName", CsType.String, ParameterDirection.Input, entity.LastName);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnEmail", CsType.String, ParameterDirection.Input, entity.Email);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnPassword", CsType.String, ParameterDirection.Input, hashedpassword);
                        _dataHandler.AddParameterToCommand(dbCommand, "@intAge", CsType.Int, ParameterDirection.Input, entity.Age);
                        _dataHandler.AddParameterToCommand(dbCommand, "@binIsActive", CsType.Boolean, ParameterDirection.Input, entity.IsActive);
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
                        dbCommand.CommandType = CommandType.StoredProcedure;
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

        // the following code will request data from the users database table based on given username
        public UsersEntity FindByUsername(string username)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            UsersEntity returnedEntity = null;

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
                            throw new ArgumentNullException("dbCommand" + " The db FindByUsername command for entity [Users] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.CommandText = "FindUserByUsername";

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnUsername", CsType.String, ParameterDirection.Input, username);

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
                                    entity.UserID = (int)reader["UserID"];
                                    entity.Username = (string)reader["Username"];
                                    entity.FirstName = (string)reader["FirstName"];
                                    entity.LastName = (string)reader["LastName"];
                                    entity.Email = (string)reader["Email"];
                                    entity.Password = (string)reader["Password"];
                                    entity.Age = (int)reader["Age"];
                                    entity.IsActive = (bool)reader["IsActive"];
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
                            throw new Exception("The FindByUsername method for entity [Users] reported the Database ErrorCode: " + _errorCode);
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
                throw new Exception("UsersRepository::FindByUsername::Error occured.", ex);
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
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.CommandText = "SelectUserById";

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
                                    entity.UserID = (int)reader["UserID"];
                                    entity.Username = (string)reader["Username"];
                                    entity.FirstName = (string)reader["FirstName"];
                                    entity.LastName = (string)reader["LastName"];
                                    entity.Email = (string)reader["Email"];
                                    entity.Password = (string)reader["Password"];
                                    entity.Age = (int)reader["Age"];
                                    entity.IsActive = (bool)reader["IsActive"];
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
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.CommandText = "SelectALLUsers";

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
                                    entity.UserID = (int)reader["UserID"];
                                    entity.Username = (string)reader["Username"];
                                    entity.FirstName = (string)reader["FirstName"];
                                    entity.LastName = (string)reader["LastName"];
                                    entity.Email = (string)reader["Email"];
                                    entity.Password = (string)reader["Password"];
                                    entity.Age = (int)reader["Age"];
                                    entity.IsActive = (bool)reader["IsActive"];
                                    entity.Role = (string)reader["Role"];
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
