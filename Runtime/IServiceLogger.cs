using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace QS.Core
{
	public interface IServiceLogger<TLogTopics, TLogLevels> where TLogTopics : Enum where TLogLevels : Enum
	{

		
		public TLogTopics DefaultLogTopic { get; set; }
		public TLogLevels DefaultLogLevel { get; set; }
		public TLogTopics DisplayTopics { get; set; }
		public TLogLevels LogDepth { get; set; }

		public void LogChangeLog([CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "");
		public string GetChangeLog();
		public List<ChangeLogItem> GetChangeLogList();


		#region Log
		
		[HideInCallstack]
		public void Log(object message, TLogTopics logTopic, TLogLevels logLevel, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "");
		[HideInCallstack]
		public void Log(object message, TLogTopics logTopic, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "");
		[HideInCallstack]
		public void Log(object message, TLogLevels logLevel, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "");
		[HideInCallstack]
		public void Log(object message, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "");

		#endregion Log

		#region LogWarning
		public void LogWarning(object message, TLogTopics logTopic, TLogLevels logLevel, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "");
		public void LogWarning(object message, TLogTopics logTopic, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "");
		public void LogWarning(object message, TLogLevels logLevel, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "");
		public void LogWarning(object message, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "");

		#endregion LogWarning

		#region LogError
		public void LogError(object message, TLogTopics logTopic, TLogLevels logLevel, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "");
		public void LogError(object message, TLogTopics logTopic, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "");
		public void LogError(object message, TLogLevels logLevel, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "");
		public void LogError(object message, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "");

		#endregion LogError

		public void LogException(Exception message, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "");


	}
}
