using QS.Core.Logger;
using QS.Core.Config;
using System;

namespace QS.ExampleLogger
{
	public class FileLogger : FileLogger<LogTopic, LogLevel>, IServiceLogger
	{
		public FileLogger(FileLoggerConfig<LogTopic, LogLevel> fls) : base ( fls ) { }
	}
}