using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BaseDataAccess.Dac
{
    public class Dac_UserInfo : DBHandler
    {
        public Dac_UserInfo() { }

        public int SaveUserInfo(string id, string password)
        {
            string query = "INSERT INTO USER_INFO (ID, PASSWORD) VALUES(@ID, @PASSWORD)";

            List<object> param = new List<object>();
            param.Add(CustomParameter("ID", id, SqlDbType.VarChar));
            param.Add(CustomParameter("PASSWORD", password, SqlDbType.VarChar));

            int result = SqlExecuteNonQuery(query, param, CommandType.Text);

            return result;
        }
    }
}
