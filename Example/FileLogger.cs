using QS.Core.Logger;
using QS.Core.Setup;
using System;

namespace QS.ExampleLogger
{
	public class FileLogger : FileLogger<LogTopic, LogLevel>, IServiceLogger
	{
		public FileLogger(FileLoggerSetup<LogTopic, LogLevel> fls) : base ( fls ) { }
	}
}