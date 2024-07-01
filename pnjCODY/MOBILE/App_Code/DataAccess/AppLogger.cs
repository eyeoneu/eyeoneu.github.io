using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Configuration;

/// <summary>
/// AppLogger의 요약 설명입니다.
/// </summary>
public class AppLogger
{
    public enum LoggingCategory { NoticeSend, DataBase, Exception }

    private string _logFilePath = @"D:\www_HappyEng\logs";

    public static void LogWrite(LoggingCategory loggingCategory, string logTitle, string logMessage)
    {
        AppLogger appLogger = new AppLogger(loggingCategory);

        appLogger.Write(logTitle, logMessage);
    }

    public static void ExceptionLogWrite(Type type, Exception ex)
    {
        AppLogger appLogger = new AppLogger(LoggingCategory.Exception);

        string logTitle = "[예외발생 Type] : " + type.ToString() + Environment.NewLine;
        string logMessage = "[Exception message] : " + ex.Message + Environment.NewLine;
        if (ex.StackTrace != null)
            logMessage += "[Stack Trace] : " + ex.StackTrace + Environment.NewLine;

        appLogger.Write(logTitle, logMessage);
    }

    public AppLogger(LoggingCategory loggingCategory)
    {
        Initialize(loggingCategory);
    }

    private void Initialize(LoggingCategory loggingCategory)
    {
        string logPath = ConfigurationManager.AppSettings.Get("LogFileRoot"); // @"C:\MarketingOnLog"

        switch (loggingCategory)
        {
            case LoggingCategory.NoticeSend:
                logPath += @"\NoticeSend";
                break;
            case LoggingCategory.DataBase:
                logPath += @"\DataBase";
                break;
            case LoggingCategory.Exception:
                logPath += @"\Exception";
                break;
        }

        if (!System.IO.Directory.Exists(logPath))
        {
            System.IO.Directory.CreateDirectory(logPath);
        }

        _logFilePath = logPath + string.Format("\\{0}.log", DateTime.Now.ToString("yyyyMMdd"));
    }

    public void Write(string logTitle, string logMessage)
    {
        Type type = this.GetType();
        FileStream fsLog = null;
        StreamWriter swLog = null;
        StringBuilder sbMsg = new StringBuilder();

        try
        {
            Monitor.Enter(type);

            //	파일 출력 로그를 처리한다.
            if (this._logFilePath != null)
            {
                fsLog = new FileStream(this._logFilePath, FileMode.Append, FileAccess.Write, FileShare.Write);
                swLog = new StreamWriter(fsLog);

                sbMsg.Append(string.Format("{0} {1} {2} : {3}\r\n", DateTime.Now.ToLongTimeString(), DateTime.Now.Millisecond.ToString(), logTitle, logMessage));
                //sbMsg.Append(DateTime.Now.ToShortDateString() + " ");
                //sbMsg.Append(DateTime.Now.ToShortTimeString() + " \r\n\r\n");
                //sbMsg.Append(logMessage);
                //sbMsg.Append("\r\n--------------------------------------------------------------------------------\r\n\r\n");

                swLog.Write(sbMsg.ToString());
            }
        }
        catch
        {

        }
        finally
        {
            if (swLog != null)
            {
                swLog.Flush();
                swLog.Close();
                swLog = null;
            }

            if (fsLog != null)
            {
                fsLog.Close();
                fsLog = null;
            }

            Monitor.Exit(type);		//	Lock을 해제한다.
        }
    }
}
