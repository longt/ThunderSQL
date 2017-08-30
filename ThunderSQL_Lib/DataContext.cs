using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using ThunderSQL.Provider;
using ThunderSQL.Runtime;
using System.Reflection;

namespace ThunderSQL
{
    public enum Transaction { Yes, No }

    public class DataContext : IDisposable
    {
        static DataContext()
        {
            DefaultConnectionStringName = "Default";
        }

        /// <summary>
        /// Creates a new transactional data context to connection string named "Default".
        /// </summary>
        public DataContext() : this(null, Transaction.No) { }

        /// <summary>
        /// Creates a new data context to connection string named "Default". 
        /// </summary>
        /// <param name="transactionMode">Defines if data context is transactional.</param>
        public DataContext(Transaction transactionMode) : this(null, transactionMode) { }

        /// <summary>
        /// Creates a new data context.
        /// </summary>
        /// <param name="connectionStringName">Connection string name of target database.</param>
        /// <param name="transaction">Defines if data context is transactional.</param>
        public DataContext(string connectionStringName, Transaction transaction)
        {
            TransactionMode = transaction;

            var connectionName = connectionStringName ?? DefaultConnectionStringName;
            ConnectionSettings = ConnectionStringBuffer.Instance.Get(connectionName);

            var providerFactory = new ProviderFactory();
            Provider = providerFactory.Create(ConnectionSettings, transaction);
        }

        /// <summary>
        /// ThunderSQL data provider.
        /// </summary>
        public IDataProvider Provider { get; private set; }

        /// <summary>
        /// Defines the name of default connection string on application config.
        /// </summary>
        public static string DefaultConnectionStringName { get; set; }

        /// <summary>
        /// Connection settings of application config.
        /// </summary>
        public ConnectionStringSettings ConnectionSettings { get; private set; }

        /// <summary>
        /// Defines the transaction mode of data context.
        /// </summary>
        public Transaction TransactionMode { get; private set; }

        /// <summary>
        /// Executes a sql query. Avoid, use First or All.
        /// </summary>
        /// <param name="query">Query sql to execute on database.</param>
        /// <param name="queryParams">Object or array that contains parameters to bind in query.</param>
        /// <returns>An open data reader.</returns>
        public IDataReader Query(string query, params object[] queryParams)
        {
            return Provider.Query(query, queryParams);
        }

        /// <summary>
        /// Executes a sql command.
        /// </summary>
        /// <param name="command">Sql command to execute on database.</param>
        /// <param name="commandParams">Object that contains parameters to bind in query.</param>
        /// <returns>The number of rows affected.</returns>
        public int Execute(string command, params object[] commandParams)
        {
            return Provider.Execute(command, commandParams);
        }

        /// <summary>
        /// Executes a sql command and returns the last identity value inserted.
        /// </summary>
        /// <param name="command">Sql command to execute on database.</param>
        /// <param name="commandParams">Object that contains parameters to bind in query.</param>
        /// <returns>The last identity value inserted into an identity column in the same scope.</returns>
        public int ExecuteGetIdentity(string command, params object[] commandParams)
        {
            return Provider.ExecuteGetIdentity(command, commandParams);
        }

        /// <summary>
        /// Executes a sql query and returns the value of first column of the first row. 
        /// </summary>
        /// <param name="query">Query sql to execute on database.</param>
        /// <param name="queryParams">Object or array that contains parameters to bind in query.</param>
        /// <returns>The value of first column of the first row.</returns>
        public object GetValue(string query, params object[] queryParams)
        {
            return Provider.ExecuteGetValue(query, queryParams);
        }

        /// <summary>
        /// Executes a sql query and returns the value of first column of the first row. 
        /// </summary>
        /// <typeparam name="T">Type of return value.</typeparam>
        /// <param name="query">Query sql to execute on database.</param>
        /// <param name="queryParams">Object or array that contains parameters to bind in query.</param>
        /// <returns>The value of first column of the first row of the type specified on T.</returns>
        public T GetValue<T>(string query, params object[] queryParams)
        {
            var result = GetValue(query, queryParams);
            return DataReader.CastTo<T>(result);
        }

        /// <summary>
        /// Executes a sql query and returns the value of first column. 
        /// </summary>
        /// <param name="query">Query sql to execute on database.</param>
        /// <param name="queryParams">Object or array that contains parameters to bind in query.</param>
        /// <returns>The values of first column.</returns>
        public IEnumerable<T> GetValues<T>(string query, params object[] queryParams)
        {
            var data = Query(query, queryParams);
            return new DataReader(data).ToEnumerable<T>();
        }

        /// <summary>
        /// Executes a sql query and return all results in array.
        /// </summary>
        /// <typeparam name="T">Type of object to bind each row of the result.</typeparam>
        /// <param name="query">Query sql to execute on database.</param>
        /// <param name="queryParams">Object or array that contains parameters to bind in query.</param>
        /// <returns>All row of query result in array of specified type.</returns>
        public IEnumerable<T> All<T>(string query, params object[] queryParams) where T : new()
        {
            var data = Query(query, queryParams);
            return new DataReader(data).ToObjectList<T>();
        }

        /// <summary>
        /// Executes a sql query and return first results.
        /// </summary>
        /// <typeparam name="T">Type of object to bind first row of the result.</typeparam>
        /// <param name="query">Query sql to execute on database.</param>
        /// <param name="queryParams">Object or array that contains parameters to bind in query.</param>
        /// <returns>First row of query result in specified type.</returns>
        public T First<T>(string query, params object[] queryParams) where T : new()
        {
            var data = Query(query, queryParams);
            return new DataReader(data).ToObjectList<T>().FirstOrDefault();
        }
              /// <summary>
        /// Executes a sql query and return the datatable of the result
        /// </summary>
        /// <param name="query">Query sql to execute on database.</param>
        /// <returns>The result datatable. </returns>
        public DataTable GetDataTable(string query)
        {
            DataTable dt = new DataTable();
            dt.Load(Query(query));
            return dt;
        }
        /// <summary>
        ///  Executes a sql query and return the datatable of the result
        /// </summary>
        /// <param name="query">Query sql to execute on database.</param>
        /// <param name="queryParams">Object or array that contains parameters to bind in query.</param>
        /// <returns></returns>
        public DataTable GetDataTable(string query,params object[] queryParams)
        {
            DataTable dt = new DataTable();
            dt.Load(Query(query, queryParams));
            return dt;
        }
        /// <summary>
        /// Return the result as list format.
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<string> GetList(string sql)
        {
            return ReaderToList(Query(sql));
        }
        /// <summary>
        /// Return the result as Class T format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<T> GetList<T>(string sql)
        {
            return ReaderToList<T>(Query(sql));
        }

        /// <summary>
        /// Commit data context commands on database. Just works on transactional data contexts.
        /// </summary>
        public void Commit()
        {
            Provider.Commit();
        }

        /// <summary>
        /// Closes the data context and database connection. Don't commit commands on transactional data contexts.
        /// </summary>
        public void Dispose()
        {
            Provider.Dispose();
        }

        ///////////////////////////////////////////////////////
        /// private code:
        ///////////////////////////////////////////////////////

        /// <summary>
        /// 将IDataReader转换为DataTable
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private   DataTable IDataReaderToDataTable(IDataReader reader)
        {
            DataTable objDataTable = new DataTable("Table");
            int intFieldCount = reader.FieldCount;
            for (int intCounter = 0; intCounter < intFieldCount; ++intCounter)
            {
                objDataTable.Columns.Add(reader.GetName(intCounter), reader.GetFieldType(intCounter));
            }
            objDataTable.BeginLoadData();
            object[] objValues = new object[intFieldCount];
            while (reader.Read())
            {
                reader.GetValues(objValues);
                objDataTable.LoadDataRow(objValues, true);
            }
            reader.Close();
            objDataTable.EndLoadData();
            return objDataTable;
        }
        private   bool IsNullOrDBNull(object obj)
        {
            return ((obj is DBNull) || string.IsNullOrEmpty(obj.ToString())) ? true : false;
        }

        private  List<string> ReaderToList(IDataReader dr)
        {
            using (dr)
            {
                List<string> list = new List<string>(); 
                while(dr.Read())
                {
                    list.Add(dr[0].ToString());
                }
                return list;
            }
        }
        private  List<T> ReaderToList<T>(IDataReader dr)
        {
            using (dr)
            {
                List<string> field = new List<string>(dr.FieldCount);
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    field.Add(dr.GetName(i).ToLower());
                }
                List<T> list = new List<T>();
                while (dr.Read())
                {
                    T model = Activator.CreateInstance<T>();
                    foreach (PropertyInfo property in model.GetType().GetProperties(BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance))
                    {
                        if (field.Contains(property.Name.ToLower()))
                        {
                            if (!IsNullOrDBNull(dr[property.Name]))
                            {
                                property.SetValue(model, HackType(dr[property.Name], property.PropertyType), null);
                            }
                        }
                    }
                    list.Add(model);
                }
                return list;
            }
        }
        private static object HackType(object value, Type conversionType)
        {
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                    return null;

                System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, conversionType);
        }

    }
}