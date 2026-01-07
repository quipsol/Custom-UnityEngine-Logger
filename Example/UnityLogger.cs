using QS.Core.Logger;
using QS.Core.Setup;
using System;
using UnityEngine;

namespace QS.ExampleLogger
{
	public class UnityLogger : UnityLogger<LogTopic, LogLevel>, IServiceLogger
	{
		public UnityLogger(UnityLoggerSetup<LogTopic, LogLevel> uls) : base(uls)
		{ }
	}
}
