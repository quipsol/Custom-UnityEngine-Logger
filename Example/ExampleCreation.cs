using System;
using System.IO;
using UnityEngine;

using QS.ExampleLogger.Config;
using QS.Core.Logger;


namespace QS.ExampleLogger
{
	public class ExampleCreation : MonoBehaviour
	{
		[SerializeField] private LogTopic defaultTopic;
		[SerializeField] private LogTopic displayTopics;
		[SerializeField] private LogLevel defaultLevel;
		[SerializeField] private LogLevel loggingDepth;

		public IServiceLogger CreateServiceLogger()
		{
			UnityLoggerConfig unityLoggerConfig = new UnityLoggerConfig.Factory(defaultTopic, defaultLevel, displayTopics, loggingDepth)
										.SetCustomMessageFormatter(MyCustomFormatterUnity)
										.SetUseDefaultMessageColor(false)
										.SetCallStackColor(Color.yellow)
										.Create();
			UnityLogger unityLogger = new UnityLogger(unityLoggerConfig);

			FileLoggerConfig fileLoggerConfig = new FileLoggerConfig.Factory(defaultTopic, defaultLevel, displayTopics, loggingDepth, "C:/folder", "filename.txt")
										.SetCustomMessageFormatter(MyCustomFormatterFile)
										.SetMaxBatchInterval(TimeSpan.FromSeconds(15))
										.SetMaxBatchSize(20)
										.SetRemoveRichTextBrackets(true)
										.Create();
			FileLogger fileLogger = new FileLogger(fileLoggerConfig);

			MultiLogger multiLogger = new MultiLogger();
			multiLogger.AddLogger(unityLogger);
			multiLogger.AddLogger(fileLogger);

			NothingLogger nothingLogger = new NothingLogger();
			
			return unityLogger; // return fileLogger; | return multiLogger; | return nothingLogger;
		}

		private string MyCustomFormatterUnity(UnityLogMessageContext<LogTopic, LogLevel> ulmc)
		{
			string time = string.Format("{0}", DateTime.Now.ToString("HH.mm:ss:fff"));
			string topic = string.Format("Topics: ({0})", ulmc.LogTopic.ToString());
			string callerName = string.Format("Caller: ({0}.{1})", Path.GetFileNameWithoutExtension(ulmc.CallerFilePath), ulmc.CallerMemberName);

			string header = string.Format("[{0}, {1}, {2}]", time, topic, callerName);

			string msg;
			if (ulmc.Message == null)
			{
				msg = "<b><color=#4A8CFF>null</color></b>";
			}
			else
			{
				msg = ulmc.Message.ToString();
				if (msg == String.Empty)
					msg = "<b><color=#4A7A4A>String</color>.<color=#D3D3D3>Empty</color></b>";
			}
			return string.Format("{0}\r\n{1}", header, msg);
		}


		private string MyCustomFormatterFile(FileLogMessageContext<LogTopic, LogLevel> flmc)
		{
			string time = string.Format("{0}", DateTime.Now.ToString("HH.mm:ss:fff"));
			string topic = string.Format("Topics: ({0})", flmc.LogTopic.ToString());
			string callerName = string.Format("Caller: ({0}.{1})", Path.GetFileNameWithoutExtension(flmc.CallerFilePath), flmc.CallerMemberName);
			string header = string.Format("[{0}, {1}, {2}]", time, topic, callerName);

			string msg = flmc.Message.ToString();

			return string.Format("{0}: {1}\r\n{2}",flmc.LogType, header, msg);
		}
	}
}