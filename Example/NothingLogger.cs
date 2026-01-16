using QS.Core.Logger;

namespace QS.ExampleLogger
{
	/// <inheritdoc />
	public class NothingLogger : NothingLogger<LogTopic, LogLevel>, IServiceLogger
	{
		public NothingLogger() : base()
		{ }
	}
}