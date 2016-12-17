/*
 * auther:ncepuhxh
 * email:ncepuhxh@qq.com
 * time:2016-12-18
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

namespace HCL
{
    /// <summary>
    /// EXCEL数据读取类
    /// </summary>
    public class ReadExcel
    {
        /// <summary>
        /// 将EXCLE转换为DataTable
        /// </summary>
        /// <param name="excelPath">excel存放路径</param>
        /// <param name="excelSql">excel查询语句</param>
        /// <returns></returns>
        private DataTable ExcelToDataTable(string excelPath,string excelSql)
        {
            //定义变量            
            String strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + excelPath + ";Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'";
            String strExcel = excelSql;
            OleDbDataAdapter myCommand = null;
            DataTable dt = new DataTable();

            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            myCommand.Fill(dt);
            conn.Close();
            return dt;
        }
    }
}