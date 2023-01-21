using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    public static class DatabaseFactory
    {
        public static IDatabaseConnection CreateDatabase(string ConnectionStringName)
        {
            // TODO
            //return new SqlDatabase(ConnectionStringName);
            return new SqlDatabaseConnection(ConnectionStringName);
        }
    }
}
