using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    public interface IDatabaseConnection
    {
        DbDataReader ExecuteReader(string ObjectName, Dictionary<string, object> Parameters, Guid? TransactionId);
        DbDataReader ExecuteReader(string ObjectName, Dictionary<string, object> Parameters);
        object ExecuteScalar(string ObjectName, Dictionary<string, object> Parameters, Guid? TransactionId);
        object ExecuteScalar(string ObjectName, Dictionary<string, object> Parameters);
        DatabaseResponse ExecuteItems(string ObjectName, Dictionary<string, object> Parameters, Guid? TransactionId);
        DatabaseResponse ExecuteItems(string ObjectName, Dictionary<string, object> Parameters);
        DatabaseResponse ExecuteItems(string ObjectName, Dictionary<string, object> Parameters, Guid? TransactionId, int PageNumber, int ValuesCount);
        DatabaseResponse ExecuteItems(string ObjectName, Dictionary<string, object> Parameters, int PageNumber, int ValuesCount);
        int ExecuteCount(string ObjectName, Dictionary<string, object> Parameters);
        int ExecuteNonQuery(string ObjectName, Dictionary<string, object> Parameters, Guid? TransactionId, CommandType? CommandType = null);
        int ExecuteNonQuery(string ObjectName, Dictionary<string, object> Parameters, CommandType? CommandType = null);
    }

    public abstract class DatabaseConnection<T> : SingletonBase<T>, IDatabaseConnection
    {
        protected CommandType CommandType { get; set; }
        protected Dictionary<Guid, DbTransaction> Transactions { get; set; }
        public int? CommandTimeout { get; set; }
        protected string ConnectionString { get; set; }
        protected abstract DbConnection CreateConnection();

        //internal DatabaseConnection()
        //{

        //}

        public DatabaseConnection(string ConnectionStringName)
        {
            this.ConnectionString = this.GetConnectionString(ConnectionStringName);
            this.CommandType = GarciaConfiguration.DefaultDatabaseConnectionType == DatabaseConnectionType.DynamicSql ? CommandType.Text : CommandType.StoredProcedure;
            this.Transactions = new Dictionary<Guid, DbTransaction>();
            this.CommandTimeout = 30;
        }

        protected virtual string GetConnectionString(string ConnectionStringName)
        {
            return ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
        }

        protected virtual DbTransaction GetTransaction(Guid? TransactionId)
        {
            if (this.Transactions.Count == 0
                || !TransactionId.HasValue
                || !this.Transactions.ContainsKey(TransactionId.Value))
            {
                return null;
            }

            return this.Transactions[TransactionId.Value];
        }

        protected virtual DbCommand GetSqlCommand(string ObjectName, Dictionary<string, object> Parameters, Guid? TransactionId, CommandType? CommandType = null)
        {
            DbConnection connection = null;
            DbTransaction transaction = this.GetTransaction(TransactionId);

            if (transaction == null)
            {
                connection = this.CreateConnection();
            }
            else
            {
                connection = transaction.Connection;
            }

            DbCommand command = connection.CreateCommand();
            command.CommandText = ObjectName;
            command.CommandType = CommandType.HasValue ? CommandType.Value : this.CommandType;
            command.Connection = connection;

            if (this.CommandTimeout.HasValue)
            {
                command.CommandTimeout = this.CommandTimeout.Value;
            }

            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            if (Parameters != null && Parameters.Count != 0)
            {
                IDictionaryEnumerator ienum = Parameters.GetEnumerator();

                while (ienum.MoveNext())
                {
                    command.AddParameterWithValue(ienum.Key.ToString(), ienum.Value);
                }
            }

            return command;
        }

        public virtual DbDataReader ExecuteReader(string ObjectName, Dictionary<string, object> Parameters, Guid? TransactionId)
        {
            DbCommand command = this.GetSqlCommand(ObjectName, Parameters, TransactionId);

            if (command.Connection.State != ConnectionState.Open)
            {
                command.Connection.Open();
            }

            return command.ExecuteReader();
        }

        public virtual DbDataReader ExecuteReader(string ObjectName, Dictionary<string, object> Parameters)
        {
            DbDataReader reader = this.ExecuteReader(ObjectName, Parameters, null);
            return reader;
        }

        public virtual object ExecuteScalar(string ObjectName, Dictionary<string, object> Parameters, Guid? TransactionId)
        {
            DbCommand command = this.GetSqlCommand(ObjectName, Parameters, TransactionId);

            if (command.Connection.State != ConnectionState.Open)
            {
                command.Connection.Open();
            }

            object result = command.ExecuteScalar();
            DbTransaction transaction = this.GetTransaction(TransactionId);

            if (transaction == null)
            {
                command.Connection.Close();
            }

            return result;
        }

        public virtual object ExecuteScalar(string ObjectName, Dictionary<string, object> Parameters)
        {
            object result = this.ExecuteScalar(ObjectName, Parameters, null);
            return result;
        }

        public virtual DatabaseResponse ExecuteItems(string ObjectName, Dictionary<string, object> Parameters, Guid? TransactionId)
        {
            DbCommand cmd = this.GetSqlCommand(ObjectName, Parameters, TransactionId);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            DatabaseResponse items = new DatabaseResponse();
            DbDataReader reader = cmd.ExecuteReader();

            //DbDataAdapter da = new SqlDataAdapter(cmd as SqlCommand);
            //DataTable dt = new DataTable();
            //da.Fill(dt);

            while (reader.Read())
            {
                Dictionary<string, object> item = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string name = reader.GetName(i);
                    object value = reader.GetValue(i);
                    item.Add(name, value);
                }

                items.Add(item);
            }

            DbTransaction transaction = this.GetTransaction(TransactionId);

            if (transaction == null && cmd.Connection.State == ConnectionState.Open)
            {
                cmd.Connection.Close();
            }

            return items;
        }

        public virtual DatabaseResponse ExecuteItems(string ObjectName, Dictionary<string, object> Parameters)
        {
            DatabaseResponse dataTable = this.ExecuteItems(ObjectName, Parameters, null);
            return dataTable;
        }

        public virtual DatabaseResponse ExecuteItems(string ObjectName, Dictionary<string, object> Parameters, Guid? TransactionId, int PageNumber, int ValuesCount)
        {
            DbCommand cmd = this.GetSqlCommand(ObjectName, Parameters, TransactionId);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            List<Dictionary<string, object>> items = new List<Dictionary<string, object>>();
            DbDataReader reader = cmd.ExecuteReader();
            DatabaseResponse res = new DatabaseResponse();

            while (reader.Read())
            {
                Dictionary<string, object> item = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string name = reader.GetName(i);
                    object value = reader.GetValue(i);
                    item.Add(name, value);
                }

                items.Add(item);
            }

            DbTransaction transaction = this.GetTransaction(TransactionId);

            if (transaction == null)
            {
                cmd.Connection.Close();
            }

            int skip = (PageNumber - 1) * ValuesCount;
            int totalValue = skip + ValuesCount;

            if (skip >= items.Count() || totalValue > items.Count())
            {
                return res;
            }

            items = items.Skip(skip).Take(ValuesCount).ToList();
            res.AddRange(items);
            return res;
        }

        public virtual DatabaseResponse ExecuteItems(string ObjectName, Dictionary<string, object> Parameters, int PageNumber, int ValuesCount)
        {
            DatabaseResponse items = this.ExecuteItems(ObjectName, Parameters, null, PageNumber, ValuesCount);
            return items;
        }

        public virtual int ExecuteCount(string ObjectName, Dictionary<string, object> Parameters)
        {
            DbCommand cmd = this.GetSqlCommand(ObjectName, Parameters, null);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            DatabaseResponse items = new DatabaseResponse();
            DbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Dictionary<string, object> item = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string name = reader.GetName(i);
                    object value = reader.GetValue(i);
                    item.Add(name, value);
                }

                items.Add(item);
            }

            return items.Count();
        }

        public virtual int ExecuteNonQuery(string ObjectName, Dictionary<string, object> Parameters, Guid? TransactionId, CommandType? CommandType = null)
        {
            DbCommand cmd = this.GetSqlCommand(ObjectName, Parameters, TransactionId, CommandType);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            int rowCount = cmd.ExecuteNonQuery();
            DbTransaction transaction = this.GetTransaction(TransactionId);

            if (transaction == null)
            {
                cmd.Connection.Close();
            }

            return rowCount;
        }

        public virtual int ExecuteNonQuery(string ObjectName, Dictionary<string, object> Parameters, CommandType? CommandType = null)
        {
            int rowCount = this.ExecuteNonQuery(ObjectName, Parameters, null, CommandType);
            return rowCount;
        }

        public Guid BeginTransaction()
        {
            Guid transactionId = Guid.NewGuid();
            DbConnection connection = this.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();
            this.Transactions.Add(transactionId, transaction);
            return transactionId;
        }

        public bool CommitTransaction(Guid TransactionId)
        {
            DbTransaction transaction = this.GetTransaction(TransactionId);

            if (transaction == null)
            {
                throw new Exception("Transaction " + TransactionId.ToString() + " not found");
            }

            transaction.Commit();
            this.Transactions.Remove(TransactionId);
            return true;
        }

        public bool RollbackTransaction(Guid TransactionId)
        {
            DbTransaction transaction = this.GetTransaction(TransactionId);

            if (transaction == null)
            {
                throw new Exception("Transaction " + TransactionId.ToString() + " not found");
            }

            transaction.Rollback();
            this.Transactions.Remove(TransactionId);
            return true;
        }

        public int ExecuteNonQuery(string ObjectName, Dictionary<string, object> Parameters, CommandType CommandType)
        {
            throw new NotImplementedException();
        }
    }
}
