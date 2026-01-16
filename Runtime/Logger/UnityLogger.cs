using QS.Core;
using QS.Core.Setup;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace QS.Core.Logger
{
	public class UnityLogger<TLogTopics, TLogLevels> : BaseLogger<TLogTopics, TLogLevels> where TLogTopics : Enum where TLogLevels : Enum
	{

		#region Fields & Properties

		private readonly string timestampFormat = "[{0}]";
		private readonly string topicFormat = "[{0}]";
		private readonly string callerFormat = "[{0}.{1}]";
		private readonly string messageFormat = "{0} {1} {2}\r\n  {3}";
		private readonly string dateTimeFormat = "HH.mm:ss:fff";
		private readonly string messageIsNullInfoText = "<b><color=#4A8CFF>NULL</color></b>";
		private readonly string messageIsEmptyInfoText = "<b><color=#4ACC4A>String</color>.<color=#EEEEEE>Empty</color></b>";


		private readonly bool useDefaultMessageColor = true;
		private readonly string defaultMessageColor = "<color=#E4E4E4>";
		private readonly bool useCallStackColor = true;
		private readonly string callStackColor = "<color=#A9A9A9>";

		private readonly Func<UnityLogMessageContext<TLogTopics, TLogLevels>, string> messageFormatter;


		#endregion Fields & Properties



		public UnityLogger(UnityLoggerSetup<TLogTopics, TLogLevels> uls) : base(uls.DefaultTopic, uls.DefaultLevel, uls.DisplayTopics, uls.LogDepth)
		{
			this.messageFormatter = uls.CustomMessageFormatter ?? BaseMessageFormatter;
			this.useDefaultMessageColor = uls.UseDefaultMessageColor;
			this.useCallStackColor = uls.UseCallStackColor;

			if (uls.DefaultMessageColor != null)
			{
				Color c = uls.DefaultMessageColor ?? Color.clear;
				this.defaultMessageColor = string.Format("<color=#{0}>", ColorUtility.ToHtmlStringRGB(c));
			}
			if (uls.CallStackColor != null)
			{
				Color c = uls.CallStackColor ?? Color.clear;
				this.callStackColor = string.Format("<color=#{0}>", ColorUtility.ToHtmlStringRGB(c));
			}
		}



		#region BaseLogger

		[HideInCallstack]
		public override void Log(object message, TLogTopics logTopic, TLogLevels logLevel,
												[CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			if (!CheckForMatchAny(DisplayTopics, logTopic))
				return;
			if (!CheckForValidLevel(logLevel))
				return;


			Debug.Log(FormatMessage(message, logTopic, logLevel, callerFilePath, callerMemberName));
		}

		[HideInCallstack]
		public override void LogWarning(object message, TLogTopics logTopic, TLogLevels logLevel,
												[CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			if (!CheckForMatchAny(DisplayTopics, logTopic))
				return;
			if (!CheckForValidLevel(logLevel))
				return;
			Debug.LogWarning(FormatMessage(message, logTopic, logLevel, callerFilePath, callerMemberName));
		}

		[HideInCallstack]
		public override void LogError(object message, TLogTopics logTopic, TLogLevels logLevel,
												[CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			if (!CheckForMatchAny(DisplayTopics, logTopic))
				return;
			if (!CheckForValidLevel(logLevel))
				return;
			Debug.LogError(FormatMessage(message, logTopic, logLevel, callerFilePath, callerMemberName));
		}

		public override void LogException(Exception message, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			Debug.LogException(message);
		}

		#endregion BaseLogger


		[HideInCallstack]
		internal string FormatMessage(object message, TLogTopics logTopic, TLogLevels logLevel, string callerFilePath, string callerMemberName)
		{
			string msg = (useDefaultMessageColor ? defaultMessageColor : "")
							+ messageFormatter(new UnityLogMessageContext<TLogTopics, TLogLevels>(message, logTopic, logLevel, callerFilePath, callerMemberName))
							+ (useDefaultMessageColor ? "</color>" : "")
							+ (useCallStackColor ? callStackColor : "");

			return msg;
		}

		[HideInCallstack]
		private string BaseMessageFormatter(UnityLogMessageContext<TLogTopics, TLogLevels> ulmc)
		{
			string time = string.Format(timestampFormat, DateTime.Now.ToString(dateTimeFormat));
			string topic = string.Format(topicFormat, ulmc.LogTopic.ToString());
			string callerName = string.Format(callerFormat, Path.GetFileNameWithoutExtension(ulmc.CallerFilePath), ulmc.CallerMemberName);

			string msg;
			if (ulmc.Message == null)
			{
				msg = messageIsNullInfoText;
			}
			else
			{
				msg = ulmc.Message.ToString();
				if (msg == String.Empty)
					msg = messageIsEmptyInfoText;
			}
			return string.Format(messageFormat, time, topic, callerName, msg);
		}



	}
}
