using QS.Core.Setup;
using System;

namespace QS.ExampleLogger.Setup
{
	public class UnityLoggerSetup : UnityLoggerSetup<LogTopic, LogLevel>
	{
		[Obsolete("Use UnityLoggerSetup.Factory instead")]
		public UnityLoggerSetup() : base() { } // public for Factory access

		public class Factory : Factory<Factory, UnityLoggerSetup>
		{
			public Factory(LogTopic baseTopic, LogLevel baseLevel, LogTopic validTopics, LogLevel logLevel)
				: base(baseTopic, baseLevel, validTopics, logLevel)
			{ }
		}

	}
}
