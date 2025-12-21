using QS.Core;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace QS.Core.Logger
{
/// <summary>
/// An empty implementation of the IServiceLogger interface. Any return values are set as 'default'.
/// </summary>
/// <typeparam name="TLogTopics"></typeparam>
/// <typeparam name="TLogLevels"></typeparam>
public class NothingLogger<TLogTopics, TLogLevels> : IServiceLogger<TLogTopics, TLogLevels> where TLogTopics : Enum where TLogLevels : Enum
{
	public TLogTopics DefaultLogTopic { get { return default; } set { } }
	public TLogLevels DefaultLogLevel { get { return default; } set { } }
	public TLogTopics DisplayTopics { get { return default; } set { } }
	public TLogLevels LogDepth { get { return default; } set { } }

	public string GetChangeLog()
	{
		return default;
	}

	public List<ChangeLogItem> GetChangeLogList()
	{
		return default;
	}

	public void Log(object message, TLogTopics logTopic, TLogLevels logLevel, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
	{
		
	}

	public void Log(object message, TLogTopics logTopic, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
	{
		
	}

	public void Log(object message, TLogLevels logLevel, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
	{
		
	}

	public void Log(object message, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
	{
		
	}

	public void LogChangeLog([CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
	{
		
	}

	public void LogError(object message, TLogTopics logTopic, TLogLevels logLevel, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
	{
		
	}

	public void LogError(object message, TLogTopics logTopic, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
	{
	
	}

	public void LogError(object message, TLogLevels logLevel, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
	{
		
	}

	public void LogError(object message, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
	{
		
	}

	public void LogException(Exception message, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
	{
		
	}

	public void LogWarning(object message, TLogTopics logTopic, TLogLevels logLevel, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
	{
		
	}

	public void LogWarning(object message, TLogTopics logTopic, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
	{
	
	}

	public void LogWarning(object message, TLogLevels logLevel, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
	{
		
	}

	public void LogWarning(object message, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
	{
		
	}
}
}