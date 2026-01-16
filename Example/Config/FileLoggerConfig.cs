using QS.Core.Config;
using System;

namespace QS.ExampleLogger.Config
{
	public class FileLoggerConfig : FileLoggerConfig<LogTopic, LogLevel>
	{
		[Obsolete("Use FileLoggerSetup.Factory instead")]
		public FileLoggerConfig() : base() { } // public for Factory access

		public class Factory : Factory<Factory, FileLoggerConfig>
		{
			public Factory(LogTopic baseTopic, LogLevel baseLevel, LogTopic validTopics, LogLevel logLevel, string folderPath, string fileName)
				: base(baseTopic, baseLevel, validTopics, logLevel, folderPath, fileName)
			{ }
	
		}
	}
}
