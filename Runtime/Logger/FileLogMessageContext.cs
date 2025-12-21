using System;

namespace QS.Core.Logger
{
	public class FileLogMessageContext<TLogTopics, TLogLevels> where TLogTopics : Enum where TLogLevels : Enum
	{
		public object Message;
		public TLogTopics LogTopic;
		public TLogLevels LogLevel;
		/// <summary>
		/// Contains one of the following strings (Log, Warning, Error, Exception)
		/// </summary>
		public string LogType;
		public string CallerFilePath;
		public string CallerMemberName;

		public FileLogMessageContext()
		{

		}

		public FileLogMessageContext(object message, TLogTopics logTopic, TLogLevels logLevel, string logType, string callerFilePath, string callerMemberName)
		{
			Message = message;
			LogTopic = logTopic;
			LogLevel = logLevel;
			LogType = logType;
			CallerFilePath = callerFilePath;
			CallerMemberName = callerMemberName;
		}
	}
}
