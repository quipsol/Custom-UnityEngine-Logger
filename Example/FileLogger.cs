using QS.Core.Logger;
using QS.Core.Setup;
using System;

namespace QS.ExampleLogger
{
	public class FileLogger : FileLogger<LogTopic, LogLevel>, IServiceLogger
	{
		public FileLogger(LogTopic baseTopic, LogLevel baseLevel, LogTopic validTopics, LogLevel logLevel, string folderPath, string fileName,
			Func<FileLogMessageContext<LogTopic, LogLevel>, string> customMessageFormatter = null)
			: base(baseTopic, baseLevel, validTopics, logLevel, folderPath, fileName, customMessageFormatter)
		{ }
		public FileLogger(FileLoggerSetup<LogTopic, LogLevel> fls) : base ( fls ) { }
	}
}