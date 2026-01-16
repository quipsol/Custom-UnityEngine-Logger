using UnityEngine;

namespace QS.ExampleLogger
{
	public class ExampleUsage : MonoBehaviour
	{
		private IServiceLogger logger; // Inject using your DI system

		void Start()
		{
			logger.Log("Begin example Logs", LogTopic.All);
			logger.Log("Logs with default topic and level");

			logger.Log("", LogTopic.All, LogLevel.Basic);
			logger.Log(null, LogTopic.All, LogLevel.Basic);

			logger.LogDepth = LogLevel.Verbose;
			logger.Log("This message will show", LogLevel.Detailed);
			logger.LogDepth = LogLevel.Basic;
			logger.Log("This message won't show", LogLevel.Detailed);

			logger.DisplayTopics = LogTopic.Input | LogTopic.Network | LogTopic.Temp;
			logger.Log("This message won't show", LogTopic.Audio);
			logger.DisplayTopics = LogTopic.All;
			logger.Log("This message will show", LogTopic.Audio);

			logger.LogChangeLog();
			logger.Log("End example Logs", LogTopic.All);
		}

	}
}