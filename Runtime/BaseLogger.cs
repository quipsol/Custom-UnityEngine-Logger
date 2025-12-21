using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace QS.Core
{
	public abstract class BaseLogger<TLogTopics, TLogLevels> : IServiceLogger<TLogTopics, TLogLevels> where TLogTopics : Enum where TLogLevels : Enum
	{

		#region Fields & Properties

		private List<ChangeLogItem> ChangeLog = new();

		private TLogTopics defaultLogTopic;
		private TLogLevels defaultLogLevel;
		private TLogTopics displayTopics;
		private TLogLevels logDepth;

		public TLogTopics DefaultLogTopic 
		{
			get { return defaultLogTopic; } 
			set 
			{
				ChangeLog.Add(new ChangeLogItem() { ChangedValue = "DefaultLogTopic", FromValue = defaultLogTopic, ToValue = value, TimeOfChange = DateTime.Now });
				defaultLogTopic = value; 
			} 
		}
		public TLogLevels DefaultLogLevel 
		{
			get { return defaultLogLevel; } 
			set 
			{
				ChangeLog.Add(new ChangeLogItem() { ChangedValue = "DefaultLogLevel", FromValue = defaultLogLevel, ToValue = value, TimeOfChange = DateTime.Now });
				defaultLogLevel = value; 
			}
		}
		public TLogTopics DisplayTopics
		{
			get { return displayTopics; } 
			set
			{
				ChangeLog.Add(new ChangeLogItem() { ChangedValue = "DisplayTopics", FromValue = displayTopics, ToValue = value, TimeOfChange = DateTime.Now });
				displayTopics = value; 
			}
		}
		public TLogLevels LogDepth
		{
			get { return logDepth; }
			set 
			{
				ChangeLog.Add(new ChangeLogItem() {ChangedValue = "LogDepth", FromValue = logDepth, ToValue=value, TimeOfChange = DateTime.Now });
				logDepth = value;
			} 
		}

		#endregion Fields & Properties


		public BaseLogger(TLogTopics defaultTopic, TLogLevels defaultLevel, TLogTopics displayTopics, TLogLevels logDepth)
		{
			this.defaultLogTopic = defaultTopic;
			this.defaultLogLevel = defaultLevel;
			this.displayTopics = displayTopics;
			this.logDepth = logDepth;
		}


		#region IServiceLogger

		#region Log

		[HideInCallstack]
		public abstract void Log(object message, TLogTopics logTopic, TLogLevels logLevel, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "");

		[HideInCallstack]
		public void Log(object message, TLogTopics logTopic, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			Log(message, logTopic, DefaultLogLevel, callerFilePath, callerMemberName);
		}

		[HideInCallstack]
		public void Log(object message, TLogLevels logLevel, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			Log(message, DefaultLogTopic, logLevel, callerFilePath, callerMemberName);
		}

		[HideInCallstack]
		public void Log(object message, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			Log(message, DefaultLogTopic, DefaultLogLevel, callerFilePath, callerMemberName);
		}

		#endregion Log

		#region LogWarning

		[HideInCallstack]
		public abstract void LogWarning(object message, TLogTopics logTopic, TLogLevels logLevel, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "");
		
		[HideInCallstack]
		public void LogWarning(object message, TLogTopics logTopic, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			LogWarning(message, logTopic, DefaultLogLevel, callerFilePath, callerMemberName);
		}

		[HideInCallstack]
		public void LogWarning(object message, TLogLevels logLevel, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			LogWarning(message, DefaultLogTopic, logLevel, callerFilePath, callerMemberName);
		}

		[HideInCallstack]
		public void LogWarning(object message, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			LogWarning(message, DefaultLogTopic, DefaultLogLevel, callerFilePath, callerMemberName);
		}

		#endregion LogWarning

		#region LogError

		[HideInCallstack]
		public abstract void LogError(object message, TLogTopics logTopic, TLogLevels logLevel, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "");

		[HideInCallstack]
		public void LogError(object message, TLogTopics logTopic, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			LogError(message, logTopic, DefaultLogLevel, callerFilePath, callerMemberName);
		}

		[HideInCallstack]
		public void LogError(object message, TLogLevels logLevel, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			LogError(message, DefaultLogTopic, logLevel, callerFilePath, callerMemberName);
		}

		[HideInCallstack]
		public void LogError(object message, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			LogError(message, DefaultLogTopic, DefaultLogLevel, callerFilePath, callerMemberName);
		}

		#endregion LogError

		[HideInCallstack]
		public abstract void LogException(Exception message, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "");


		#region ChangeLog

		[HideInCallstack]
		public void LogChangeLog([CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			Log("Logger Changelog:\r\n" + GetChangeLog(), DisplayTopics, LogDepth, callerFilePath, callerMemberName);
		}

		[HideInCallstack]
		public string GetChangeLog()
		{
			string retVal = "";
			foreach(ChangeLogItem log in ChangeLog)
			{
				retVal += log.ToString() + "\r\n";
			}
			retVal = retVal.Remove(retVal.Length - 2, 2);
			return retVal;
		}

		[HideInCallstack]
		public List<ChangeLogItem> GetChangeLogList()
		{
			return ChangeLog;
		}

		#endregion ChangeLog


		#endregion IServiceLogger



		#region Helper

		[HideInCallstack]
		protected bool CheckForMatchAny(Enum validEnum, Enum testEnum)
		{
			if(validEnum.GetType() != testEnum.GetType())
			{
				LogError("Type mismatch: " + validEnum.GetType() + " does not match " + testEnum.GetType());
				return false;
			}	
			long validSigned = Convert.ToInt64(validEnum);
			long testSigned = Convert.ToInt64(testEnum);
			ulong valid = unchecked((ulong)validSigned);
			ulong test = unchecked((ulong)testSigned);
			return (valid & test) != 0;
		}

		[HideInCallstack]
		protected bool CheckForValidLevel(TLogLevels level)
		{
			long validSigned = Convert.ToInt64(LogDepth);
			long testSigned = Convert.ToInt64(level);
			ulong valid = unchecked((ulong)validSigned);
			ulong test = unchecked((ulong)testSigned);
			return test <= valid;
		}

		#endregion Helper

	}
}
