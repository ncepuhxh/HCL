/*
 * auther:ncepuhxh
 * email:ncepuhxh@qq.com
 * time:2016-12-18
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;

namespace HCL
{
    /// <summary>
    /// 压缩解压缩类
    /// </summary>
    public class WinRAR
    {
        // WinRAR安装注册表key    
        private const string WinRAR_KEY = @"WinRAR.ZIP\shell\open\command";
        /// <summary>
        /// 利用 WinRAR 进行压缩     
        /// </summary>
        /// <param name="savePath">将要被压缩的文件（绝对路径）</param>
        /// <param name="rarPath">压缩后的存放目录（绝对路径）</param>
        /// <param name="rarName">压缩文件的名称（包括后缀）</param>
        /// <returns>true 或 false。压缩成功返回 true，反之，false。</returns>    
        public bool RAR(string savePath, string rarPath, string rarName)
        {
            bool flag = false;
            string rarexe;       //WinRAR.exe 的完整路径      
            RegistryKey regkey;  //注册表键      
            Object regvalue;     //键值   
            string cmd;          //WinRAR 命令参数  
            ProcessStartInfo startinfo;
            Process process;
            try
            {
                regkey = Registry.ClassesRoot.OpenSubKey(WinRAR_KEY);
                regvalue = regkey.GetValue("");  // 键值为 "d:\Program Files\WinRAR\WinRAR.exe" "%1"      
                rarexe = regvalue.ToString();
                regkey.Close();
                rarexe = rarexe.Substring(1, rarexe.Length - 7);  // d:\Program Files\WinRAR\WinRAR.exe   
                Directory.CreateDirectory(savePath);             //压缩命令，相当于在要压缩的文件夹(savePath)上点右键->WinRAR->添加到压缩文件->输入压缩文件名(rarName)  
                cmd = string.Format("a {0} {1} -r", rarName, savePath);
                startinfo = new ProcessStartInfo();
                startinfo.FileName = rarexe;
                startinfo.Arguments = cmd;        //设置命令参数    
                startinfo.WindowStyle = ProcessWindowStyle.Hidden;  //隐藏 WinRAR 窗口        
                startinfo.WorkingDirectory = rarPath;
                process = new Process();
                process.StartInfo = startinfo;
                process.Start();
                process.WaitForExit(); //无限期等待进程 winrar.exe 退出      
                if (process.HasExited)
                {
                    flag = true;
                }
                process.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            return flag;
        }
        /// <summary>
        /// 利用 WinRAR 进行解压缩     
        /// </summary>
        /// <param name="rarPath">文件的存放目录（绝对路径）</param>
        /// <param name="rarName">文件名（包括后缀）</param>
        /// <param name="savePath">文件解压后存放路径（绝对）</param>
        /// <returns>true 或 false。解压缩成功返回 true，反之，false。</returns>  
        public bool UnRAR(string rarPath, string rarName, string savePath)
        {
            bool flag = false;
            string rarexe;
            RegistryKey regkey;
            Object regvalue;
            string cmd;
            ProcessStartInfo startinfo;
            Process process;
            try
            {
                regkey = Registry.ClassesRoot.OpenSubKey(WinRAR_KEY);
                regvalue = regkey.GetValue("");
                rarexe = regvalue.ToString();
                regkey.Close();
                rarexe = rarexe.Substring(1, rarexe.Length - 7);
                Directory.CreateDirectory(savePath);
                //解压缩命令，相当于在要压缩文件(rarName)上点右键->WinRAR->解压到当前文件夹       
                cmd = string.Format("x {0} {1} -y", rarName, savePath);
                startinfo = new ProcessStartInfo();
                startinfo.FileName = rarexe;
                startinfo.Arguments = cmd;
                startinfo.WindowStyle = ProcessWindowStyle.Hidden;
                startinfo.WorkingDirectory = rarPath;
                process = new Process();
                process.StartInfo = startinfo;
                process.Start();
                process.WaitForExit();
                if (process.HasExited)
                {
                    flag = true;
                }
                process.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            return flag;
        }
    }
}