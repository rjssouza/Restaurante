using Module.Dto.Configuration;
using Module.Dto.Enum;
using Module.Factory.Base;
using Module.Factory.Interface.Connection;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

namespace Connection
{
    public class DbConnectionFactory : BaseFactory, IDbConnectionFactory, IDbTransactionFactory
    {
        public IDbConnection _connection;
        public IDbTransaction _transaction;
        private readonly DbConnectionDto _dbConnectionDto;
        private int _transactionCount = 0;

        public DbConnectionFactory(DbConnectionDto dbConnectionDto)
        {
            this._dbConnectionDto = dbConnectionDto;

            this.CreateConnection();
        }

        public IDbConnection Connection
        {
            get
            {
                if (this._connection.State == ConnectionState.Closed)
                    this._connection.Open();

                return this._connection;
            }
        }

        public IDbTransaction Transaction => this._transaction;

        public void BeginTransaction()
        {
            if (this._transaction == null)
                this._transaction = this.Connection.BeginTransaction();

            this._transactionCount++;
        }

        public void Commit()
        {
            if (this._transaction != null && this._transaction.Connection.State == ConnectionState.Open)
            {
                this._transactionCount--;
                if (this._transactionCount <= 0)
                    this._transaction.Commit();
            }
        }

        public void Rollback()
        {
            if (this._transaction != null && this._transaction.Connection != null && this._transaction.Connection.State == ConnectionState.Open)
            {
                this._transaction.Rollback();
                this._transactionCount = 0;
            }
        }

        protected override void Dispose(bool disposing)
        {
            this.TryRollback();
            this.TryCloseConnection();

            base.Dispose(disposing);
        }

        private void CreateConnection()
        {
            this._connection = this._dbConnectionDto.ConnectionType switch
            {
                TipoConexaoEnum.MySql => new MySqlConnection(this._dbConnectionDto.ConnectionString),
                _ => new SqlConnection(this._dbConnectionDto.ConnectionString),
            };
        }

        private void TryCloseConnection()
        {
            if (this._connection != null)
            {
                this._connection.Close();
                this._connection = null;
            }
        }

        private void TryRollback()
        {
            if (this._transaction != null && this._connection.State == ConnectionState.Open)
                this.Rollback();
        }
    }
}