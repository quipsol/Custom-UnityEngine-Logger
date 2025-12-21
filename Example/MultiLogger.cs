using QS.Core;

namespace QS.ExampleLogger
{
	public class MultiLogger : MultiLogger<LogTopic, LogLevel>, IServiceLogger
	{
		public MultiLogger() : base()
		{ }
	}
}
