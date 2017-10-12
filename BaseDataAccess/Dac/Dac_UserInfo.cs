using System;
using System.Collections.Generic;
using System.Data;

namespace BaseDataAccess.Dac
{
    public class Dac_UserInfo : DBHandler
    {
        public Dac_UserInfo() { }

        public int SelectCountUserInfo(string userid, string password)
        {
            string query = @"
                            SELECT COUNT(ID) AS CNT
                            FROM USER_INFO
                            WHERE ID = @ID AND PASSWORD = @PASSWORD        
                            ";

            List<object> param = new List<object>();
            param.Add(CustomParameter("ID", userid, SqlDbType.VarChar));
            param.Add(CustomParameter("PASSWORD", password, SqlDbType.VarChar));

            object objCnt = SqlGetScalar(query, param, CommandType.Text);

            return BaseUtility.DataUtil.GetInt32(objCnt);
        }

        public int SelectCountUserId(string userid)
        {
            string query = "SELECT COUNT(ID) AS CNT FROM USER_INFO WHERE ID = @ID";

            List<object> param = new List<object>();
            param.Add(CustomParameter("ID", userid, SqlDbType.VarChar));

            object objCnt = SqlGetScalar(query, param, CommandType.Text);

            return BaseUtility.DataUtil.GetInt32(objCnt);
        }

        public int InsertUserInfo(string userid, string password)
        {
            string query = "INSERT INTO USER_INFO (ID, PASSWORD) VALUES(@ID, @PASSWORD)";

            List<object> param = new List<object>();
            param.Add(CustomParameter("ID", userid, SqlDbType.VarChar));
            param.Add(CustomParameter("PASSWORD", password, SqlDbType.VarChar));

            int result = SqlExecuteNonQuery(query, param, CommandType.Text);

            return result;
        }
    }
}
