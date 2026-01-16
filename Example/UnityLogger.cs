using QS.Core.Logger;
using QS.Core.Config;
using System;
using UnityEngine;

namespace QS.ExampleLogger
{
	public class UnityLogger : UnityLogger<LogTopic, LogLevel>, IServiceLogger
	{
		public UnityLogger(UnityLoggerConfig<LogTopic, LogLevel> uls) : base(uls)
		{ }
	}
}
