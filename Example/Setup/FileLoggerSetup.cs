using QS.Core.Setup;
using System;

namespace QS.ExampleLogger.Setup
{
	public class FileLoggerSetup : FileLoggerSetup<LogTopic, LogLevel>
	{
		[Obsolete("Use FileLoggerSetup.Factory instead")]
		public FileLoggerSetup() : base() { } // public for Factory access

		public class Factory : Factory<Factory, FileLoggerSetup>
		{
			public Factory(LogTopic baseTopic, LogLevel baseLevel, LogTopic validTopics, LogLevel logLevel, string folderPath, string fileName)
				: base(baseTopic, baseLevel, validTopics, logLevel, folderPath, fileName)
			{ }
	
		}
	}
}
