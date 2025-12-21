using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace QS.Core
{
	public class MultiLogger<TLogTopics, TLogLevels> : IServiceLogger<TLogTopics, TLogLevels> where TLogTopics : Enum where TLogLevels : Enum
	{

		private readonly List<IServiceLogger<TLogTopics, TLogLevels>> _loggers = new();

		public MultiLogger() { }

		public void AddLogger(IServiceLogger<TLogTopics, TLogLevels> logger)
		{
			if(_loggers.Contains(logger)) 
				return;
			_loggers.Add(logger);
		}

		public void RemoveLogger(IServiceLogger<TLogTopics, TLogLevels> logger)
		{
			if (_loggers.Contains(logger))
				_loggers.Remove(logger);
		}

		#region IServiceLogger

		#region Properties

		public TLogTopics DefaultLogTopic
		{
			get
			{
				if (_loggers.Count == 0)
					return default(TLogTopics);
				return _loggers.FirstOrDefault().DefaultLogTopic;
			}
			set
			{
				foreach (var logger in _loggers)
				{
					logger.DefaultLogTopic = value;
				}
			}
		}

		public TLogLevels DefaultLogLevel
		{
			get
			{
				if (_loggers.Count == 0)
					return default(TLogLevels);
				return _loggers.FirstOrDefault().DefaultLogLevel;
			}
			set
			{
				foreach (var logger in _loggers)
				{
					logger.DefaultLogLevel = value;
				}
			}
		}

		public TLogTopics DisplayTopics
		{
			get
			{
				if (_loggers.Count == 0)
					return default(TLogTopics);
				return _loggers.FirstOrDefault().DisplayTopics;
			}
			set
			{
				foreach (var logger in _loggers)
				{
					logger.DisplayTopics = value;
				}
			}
		}

		public TLogLevels LogDepth
		{
			get
			{
				if (_loggers.Count == 0)
					return default(TLogLevels);
				return _loggers.FirstOrDefault().LogDepth;
			}
			set
			{
				foreach (var logger in _loggers)
				{
					logger.LogDepth = value;
				}
			}
		}


		#endregion Properties

		#region Log

		public void Log(object message, TLogTopics logTopic, TLogLevels logLevel, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			foreach (var logger in _loggers)
			{
				logger.Log(message, logTopic, logLevel, callerFilePath, callerMemberName);
			}
		}

		public void Log(object message, TLogTopics logTopic, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			foreach (var logger in _loggers)
			{
				logger.Log(message, logTopic, callerFilePath, callerMemberName);
			}
		}

		public void Log(object message, TLogLevels logLevel, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			foreach (var logger in _loggers)
			{
				logger.Log(message, logLevel, callerFilePath, callerMemberName);
			}
		}

		public void Log(object message, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			foreach (var logger in _loggers)
			{
				logger.Log(message, callerFilePath, callerMemberName);
			}
		}

		#endregion Log

		#region LogWarning

		public void LogWarning(object message, TLogTopics logTopic, TLogLevels logLevel, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			foreach (var logger in _loggers)
			{
				logger.LogWarning(message, logTopic, logLevel, callerFilePath, callerMemberName);
			}
		}

		public void LogWarning(object message, TLogTopics logTopic, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			foreach (var logger in _loggers)
			{
				logger.LogWarning(message, logTopic, callerFilePath, callerMemberName);
			}
		}

		public void LogWarning(object message, TLogLevels logLevel, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			foreach (var logger in _loggers)
			{
				logger.LogWarning(message, logLevel, callerFilePath, callerMemberName);
			}
		}

		public void LogWarning(object message, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			foreach (var logger in _loggers)
			{
				logger.LogWarning(message, callerFilePath, callerMemberName);
			}
		}

		#endregion LogWarning

		#region LogError

		public void LogError(object message, TLogTopics logTopic, TLogLevels logLevel, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			foreach (var logger in _loggers)
			{
				logger.LogError(message, logTopic, logLevel, callerFilePath, callerMemberName);
			}
		}

		public void LogError(object message, TLogTopics logTopic, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			foreach (var logger in _loggers)
			{
				logger.LogError(message, logTopic, callerFilePath, callerMemberName);
			}
		}

		public void LogError(object message, TLogLevels logLevel, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			foreach (var logger in _loggers)
			{
				logger.LogError(message, logLevel, callerFilePath, callerMemberName);
			}
		}

		public void LogError(object message, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			foreach (var logger in _loggers)
			{
				logger.LogError(message, callerFilePath, callerMemberName);
			}
		}

		#endregion LogError

		public void LogException(Exception message, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			foreach (var logger in _loggers)
			{
				logger.LogException(message, callerFilePath, callerMemberName);
			}
		}


		public void LogChangeLog([CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			foreach (var logger in _loggers)
			{
				logger.LogChangeLog(callerFilePath, callerMemberName);
			}
		}

		public string GetChangeLog()
		{
			if (_loggers.Count == 0)
				return "";
			return _loggers.FirstOrDefault().GetChangeLog();
		}

		public List<ChangeLogItem> GetChangeLogList()
		{
			if (_loggers.Count == 0)
				return default;
			return _loggers.FirstOrDefault().GetChangeLogList();
		}

		#endregion IServiceLogger

	}
}
