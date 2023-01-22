using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    public class SqlDatabaseConnection : DatabaseConnection<SqlDatabaseConnection>
    {
        public SqlDatabaseConnection(string ConnectionStringName)
            : base(ConnectionStringName)
        {
        }

        protected SqlDatabaseConnection()
            : this(MannFrameworkConfiguration.DefaultConnectionStringName)
        {

        }

        protected override DbConnection CreateConnection()
        {
            return new SqlConnection(this.ConnectionString);
        }

        //internal DataTable GetTables()
        //{
        //    DbDataReader reader = this.ExecuteReader("select ", );
          
        //}
    }
}