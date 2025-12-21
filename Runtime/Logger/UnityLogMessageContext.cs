using System;

namespace QS.Core.Logger
{
	public class UnityLogMessageContext<TLogTopics, TLogLevels> where TLogTopics : Enum where TLogLevels : Enum
	{
		public object Message;
		public TLogTopics LogTopic;
		public TLogLevels LogLevel;
		public string CallerFilePath;
		public string CallerMemberName;


		public UnityLogMessageContext()
		{

		}

		public UnityLogMessageContext(object message, TLogTopics logTopic, TLogLevels logLevel, string callerFilePath, string callerMemberName)
		{
			Message = message;
			LogTopic = logTopic;
			LogLevel = logLevel;
			CallerFilePath = callerFilePath;
			CallerMemberName = callerMemberName;
		}
	}
}

