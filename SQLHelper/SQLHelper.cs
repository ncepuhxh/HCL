using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace Insert
{
    public class SQLHelper
    {
        private SqlConnection conn = null;
        private SqlCommand cmd = null;
        private SqlDataReader sdr = null;
        public SQLHelper()
        {
            // TODO: 部署 数据库连接字符串
            string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            conn = new SqlConnection(connStr);
        }
        private SqlConnection GetConn()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        }

        /// <summary>
        /// 该方法执行传入的增删改SQL语句
        /// </summary>
        /// <param name="sql">要执行的SQL语句</param>
        /// <returns>返回更新的记录数</returns>
        public int ExecuteNonQuery(string sql)
        {
            int res;
            try
            {
                cmd = new SqlCommand(sql, GetConn());
                res = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return res;
        }
        /// <summary>
        /// 执行带参数的SQL增删改语句
        /// </summary>
        /// <param name="sql">增删改SQL语句</param>
        /// <param name="para">参数集合</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, SqlParameter[] para)
        {
            int res;
            using (cmd = new SqlCommand(sql, GetConn()))
            {
                try
                {
                    cmd.Parameters.AddRange(para);
                    res = cmd.ExecuteNonQuery();
                }
                catch (Exception )
                {

                    //throw e;
                    res = 0;
                }

            }
            return res;
        }
        /// <summary>
        /// 该方法执行传入SQL查询语句
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <returns></returns>
        public DataTable ExecuteQuery(string sql)
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand(sql, GetConn());
            using (sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                dt.Load(sdr);
            }
            return dt;
        }
        /// <summary>
        /// 执行带参数的SQL查询
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">参数集合</param>
        /// <returns></returns>
        public DataTable ExecuteQuery(string sql, SqlParameter[] para)
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand(sql, GetConn());
            cmd.Parameters.AddRange(para);
            using (sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                dt.Load(sdr);
            }
            return dt;
        }
    }
}
