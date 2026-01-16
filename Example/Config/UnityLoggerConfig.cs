using QS.Core.Config;
using System;

namespace QS.ExampleLogger.Config
{
	public class UnityLoggerConfig : UnityLoggerConfig<LogTopic, LogLevel>
	{
		[Obsolete("Use UnityLoggerSetup.Factory instead")]
		public UnityLoggerConfig() : base() { } // public for Factory access

		public class Factory : Factory<Factory, UnityLoggerConfig>
		{
			public Factory(LogTopic baseTopic, LogLevel baseLevel, LogTopic validTopics, LogLevel logLevel)
				: base(baseTopic, baseLevel, validTopics, logLevel)
			{ }
		}

	}
}
