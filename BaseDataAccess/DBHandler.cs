using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Configuration;

using BaseUtility;

namespace BaseDataAccess
{
    public class DBHandler
    {
        private static IDbConnection _sqlConn;
        private static IDbCommand _sqlCmd;
        private static IDbDataAdapter _sqlAdapter;
        private static string _sqlDbType = ConfigurationManager.AppSettings["DB_TYPE"].ToUpper();
        private static string _connType = _sqlDbType.Equals("ORACLE") ? "ORA_CONNECTION" : "SQL_CONNECTION";
        private static string _connString = ConfigurationManager.ConnectionStrings[_connType].ConnectionString;

        public DBHandler()
        {
            if (_sqlDbType.Equals("ORACLE"))
                SetOracle();
            else
                SetMssql();
        }

        public IDbDataParameter CreateDataParameter(string parameterName, SqlDbType dbType)
        {
            if (_sqlDbType.Equals("ORACLE"))
                return new OracleParameter(":" + parameterName, dbType);
            
            return new SqlParameter("@" + parameterName, dbType);
        }

        public IDbDataParameter CustomParameter(string parameterName, object value, SqlDbType dbType)
        {
            IDbDataParameter param = CreateDataParameter(parameterName, dbType);
            param.Value = value;
            return param;
        }

        public string SqlQueryFilter(string query)
        {
            if (_sqlDbType.Equals("ORACLE"))
            {
                query = query.Replace("@", ":")
                             .Replace("ISNULL", "NVL");
            }

            return query;
        }

        public void SetMssql()
        {
            _sqlConn = new SqlConnection();
            _sqlCmd = new SqlCommand();
            _sqlAdapter = new SqlDataAdapter();
        }

        public void SetOracle()
        {
            _sqlConn = new OracleConnection();
            _sqlCmd = new OracleCommand();
            _sqlAdapter = new OracleDataAdapter();
        }

        public int SqlExecuteNonQuery(string query, List<object> paramlist, CommandType cmdType)
        {
            int result = 0;

            try
            {
                _sqlConn.ConnectionString = _connString;
                _sqlCmd.CommandText = SqlQueryFilter(query);
                _sqlCmd.CommandType = cmdType;
                _sqlCmd.Connection = _sqlConn;
                _sqlCmd.Parameters.Clear();

                if (paramlist != null)
                {
                    foreach (var param in paramlist)
                        _sqlCmd.Parameters.Add(param);
                }

                _sqlConn.Open();
                result = _sqlCmd.ExecuteNonQuery();
                _sqlConn.Close();
            }
            catch (Exception ex)
            {
                CommonUtil.WriteLog(ex.Message);
            }

            return result;
        }

        public object SqlGetScalar(string query, List<object> paramlist, CommandType cmdType)
        {
            object result = null;
            try
            {
                _sqlConn.ConnectionString = _connString;
                _sqlCmd.CommandText = SqlQueryFilter(query);
                _sqlCmd.CommandType = cmdType;
                _sqlCmd.Connection = _sqlConn;
                _sqlCmd.Parameters.Clear();

                if (paramlist != null)
                {
                    foreach (var param in paramlist)
                        _sqlCmd.Parameters.Add(param);
                }

                _sqlConn.Open();
                result = _sqlCmd.ExecuteScalar();
                _sqlConn.Close();
            }
            catch (Exception ex)
            {
                CommonUtil.WriteLog(ex.Message);
            }
            return result;
        }

        public DataSet SqlGetDataSet(string query, List<object> paramlist, CommandType cmdType)
        {
            DataSet ds = new DataSet();
            try
            {
                _sqlConn.ConnectionString = _connString;
                _sqlCmd.CommandText = SqlQueryFilter(query);
                _sqlCmd.CommandType = cmdType;
                _sqlCmd.Connection = _sqlConn;
                _sqlCmd.Parameters.Clear();

                if (paramlist != null)
                {
                    foreach (var param in paramlist)
                        _sqlCmd.Parameters.Add(param);
                }

                _sqlAdapter.SelectCommand = _sqlCmd;
                _sqlConn.Open();
                _sqlAdapter.Fill(ds);
                _sqlConn.Close();
            }
            catch (Exception ex)
            {
                CommonUtil.WriteLog(ex.Message);
            }

            return ds;
        }

        public DataTable SqlGetDataTable(string query, List<object> paramlist, CommandType cmdType)
        {
            DataTable dt = new DataTable();
            DataSet ds = this.SqlGetDataSet(query, paramlist, cmdType);
            if (ds.Tables.Count > 0)
                dt = ds.Tables[0];

            return dt;
        }
    }
}
