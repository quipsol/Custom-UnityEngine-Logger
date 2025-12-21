using QS.Core.Logger;
using QS.Core.Setup;
using System;
using UnityEngine;

namespace QS.ExampleLogger
{
	public class UnityLogger : UnityLogger<LogTopic, LogLevel>, IServiceLogger
	{
		public UnityLogger(
			LogTopic baseTopic, LogLevel baseLevel, LogTopic validTopics, LogLevel validLevels,
			bool useDefaultMessageColor = false, Color? defaultMessageColor = null,
			bool useStackTraceColor = false, Color? stackTraceColor = null,
			Func<UnityLogMessageContext<LogTopic, LogLevel>, string> customFormatter = null)
			: base(baseTopic, baseLevel, validTopics, validLevels, useDefaultMessageColor, defaultMessageColor, useStackTraceColor, stackTraceColor, customFormatter)
		{ }
		public UnityLogger(UnityLoggerSetup<LogTopic, LogLevel> uls) : base(uls)
		{ }
	}
}
