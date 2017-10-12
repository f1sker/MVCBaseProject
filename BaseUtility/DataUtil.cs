using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseUtility
{
    public class DataUtil
    {
        public static string GetString(object obj)
        {
            if (obj == null
                || obj == DBNull.Value)
                return "";

            return obj.ToString();
        }

        public static Int32 GetInt32(object obj)
        {
            int result = 0;
            try
            {
                if (obj != null && obj != DBNull.Value)
                    result = Int32.Parse(obj.ToString());
            }
            catch (Exception ex)
            {
                result = 0;
            }

            return result;
        }

    }
}
