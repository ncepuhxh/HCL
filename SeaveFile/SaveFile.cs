/*
 * auther:ncepuhxh
 * email:ncepuhxh@qq.com
 * time:2016-12-18
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCL
{
    /// <summary>
    /// 文件存储类
    /// </summary>
    public class SaveFile
    {
        /// <summary>
        /// 存储File
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <param name="savePath">待保存文件路径(包含文件名以及后缀)</param>
        /// <returns></returns>
        public Boolean SaveFile(HttpContext context, string savePath)
        {
            Boolean flag = true;
            try
            {
                //string savepath = Server.MapPath("filesPath");
                //context.Request.ContentType = "application/vnd.ms-excel";
                //context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpFileCollection files = context.Request.Files;//得到所有的FILE控件
                //HttpPostedFile excel = files[0];//取得单个文件              
                //for (int i = 0; i < files.Count; i++)
                //{
                //    HttpPostedFile postedFile = files[i];//取得单个文件

                //}
                //FileStream file = new FileStream(System.AppDomain.CurrentDomain.BaseDirectory+DateTime.Now.Year + "." + DateTime.Now.Month + "人工录入数据.xlsx", FileMode.OpenOrCreate);
                //StreamReader streamReader = new System.IO.StreamReader(excel.InputStream);
                //StreamWriter streamWriter = new StreamWriter(file);
                //streamWriter.Write(streamReader.ReadToEnd());
                //streamWriter.Flush();
                //streamWriter.Close();
                //streamReader.Close();
                HttpPostedFile file = files[0];//取得单个文件
                file.SaveAs(savePath);
            }
            catch (Exception)
            {
                flag = false;
                throw;
            }
            return flag;
        }
    }
}