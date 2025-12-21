using QS.Core.Setup;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;


namespace QS.Core.Logger
{
	public class FileLogger<TLogTopics, TLogLevels> : BaseLogger<TLogTopics, TLogLevels>, IDisposable where TLogTopics : Enum where TLogLevels : Enum
	{
		private readonly string timestampFormat = "[{0}]";
		private readonly string topicFormat = "[{0}]";
		private readonly string callerFormat = "[{0}.{1}]";
		private readonly string messageFormat = "{0}: {1} {2} {3}\r\n  {4}";
		private readonly string dateTimeFormat = "HH.mm:ss:fff";
		private readonly string messageIsNullInfoText = "<b><color=#4A8CFF>NULL</color></b>";
		private readonly string messageIsEmptyInfoText = "<b><color=#4ACC4A>String</color>.<color=#EEEEEE>Empty</color></b>";

		private readonly ConcurrentQueue<string> messageQueue = new();
		private readonly ManualResetEventSlim hasNewMessages = new(false);
		private readonly CancellationTokenSource cts = new();
		private readonly Task backgroundTask;


		private readonly string folderPath;
		private readonly string fileName;

		private readonly int MaxBatchSize = 5;
		private readonly TimeSpan MaxBatchInterval = TimeSpan.FromSeconds(5);
		private readonly bool removeRichTextBrackets = true;

		private readonly Func<FileLogMessageContext<TLogTopics, TLogLevels>, string> messageFormatter;

		public FileLogger(TLogTopics defaultTopic, TLogLevels defaultLevel, TLogTopics displayTopics, TLogLevels logDepth, string folderPath, string fileName,
			Func<FileLogMessageContext<TLogTopics, TLogLevels>, string> customMessageFormatter = null) : base(defaultTopic, defaultLevel, displayTopics, logDepth)
		{
			this.folderPath = folderPath;
			this.fileName = fileName;
			messageFormatter = customMessageFormatter ?? BaseMessageFormatter;
			backgroundTask = Task.Run(() => ProcessQueue(cts.Token));
		}

		public FileLogger(FileLoggerSetup<TLogTopics, TLogLevels> fls) : base(fls.DefaultTopic, fls.DefaultLevel, fls.DisplayTopics, fls.LogDepth)
		{
			this.folderPath = fls.FolderPath;
			this.fileName = fls.FileName;
			MaxBatchSize = fls.MaxBatchSize;
			MaxBatchInterval = fls.MaxBatchInterval;
			removeRichTextBrackets = fls.RemoveRichTextBrackets;
			messageFormatter = fls.CustomMessageFormatter ?? BaseMessageFormatter;
			backgroundTask = Task.Run(() => ProcessQueue(cts.Token));
		}


		private void ProcessQueue(CancellationToken token)
		{
			string path = Path.Combine(folderPath, fileName);
			Directory.CreateDirectory(folderPath);

			using StreamWriter writer = new StreamWriter(path, append: true);

			var batch = new List<string>(MaxBatchSize);
			DateTime lastFlushTime = DateTime.UtcNow;

			while (!token.IsCancellationRequested)
			{
				hasNewMessages.Wait(TimeSpan.FromMilliseconds(200), token);

				while (batch.Count < MaxBatchSize && messageQueue.TryDequeue(out var msg))
				{
					batch.Add(msg);
				}

				bool flushNeeded = batch.Count >= MaxBatchSize || (batch.Count > 0 && DateTime.UtcNow - lastFlushTime >= MaxBatchInterval);

				if (flushNeeded)
				{
					FlushBatch(writer, batch);
					lastFlushTime = DateTime.UtcNow;
					batch.Clear();
				}

				if (messageQueue.IsEmpty)
					hasNewMessages.Reset();
			}


			while (messageQueue.TryDequeue(out var remaining))
				writer.WriteLine(remaining);
			writer.Flush();
		}

		private void FlushBatch(StreamWriter writer, List<string> batch)
		{
			const int maxRetries = 3;

			foreach (var msg in batch)
			{
				int retryCount = 0;
				while (retryCount < maxRetries)
				{
					try
					{
						writer.WriteLine(msg);
						break;
					}
					catch (IOException)
					{
						retryCount++;
						Debug.LogWarning($"Log write failed, retrying... ({retryCount}/{maxRetries})");
						Thread.Sleep(100);
					}
				}
			}

			writer.Flush();
		}

		private void CacheMessage(string message)
		{
			messageQueue.Enqueue(message);
			hasNewMessages.Set();
		}


		#region BaseLogger

		public override void Log(object message, TLogTopics logTopic, TLogLevels logLevel, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			if (!CheckForMatchAny(DisplayTopics, logTopic))
				return;
			if (!CheckForValidLevel(logLevel))
				return;
			FileLogMessageContext<TLogTopics, TLogLevels> flmc = new FileLogMessageContext<TLogTopics, TLogLevels>(message, logTopic, logLevel, "Log", callerFilePath, callerMemberName);
			CacheMessage(FormatMessage(flmc));
		}
		public override void LogWarning(object message, TLogTopics logTopic, TLogLevels logLevel, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			if (!CheckForMatchAny(DisplayTopics, logTopic))
				return;
			if (!CheckForValidLevel(logLevel))
				return;
			FileLogMessageContext<TLogTopics, TLogLevels> flmc = new FileLogMessageContext<TLogTopics, TLogLevels>(message, logTopic, logLevel, "Warning", callerFilePath, callerMemberName);
			CacheMessage(FormatMessage(flmc));
		}

		public override void LogError(object message, TLogTopics logTopic, TLogLevels logLevel, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			if (!CheckForMatchAny(DisplayTopics, logTopic))
				return;
			if (!CheckForValidLevel(logLevel))
				return;
			FileLogMessageContext<TLogTopics, TLogLevels> flmc = new FileLogMessageContext<TLogTopics, TLogLevels>(message, logTopic, logLevel, "Error", callerFilePath, callerMemberName);
			CacheMessage(FormatMessage(flmc));
		}

		public override void LogException(Exception message, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
			CacheMessage(message.ToString());
		}

		#endregion BaseLogger

		#region Helper

		private string RemoveRichText(string message)
		{
			return Regex.Replace(message.ToString(), @"<\s*(b|/b|i|/i|size=[^>]*|/size|color=[^>]*|/color)\s*>", "", RegexOptions.IgnoreCase);
		}


		private string FormatMessage(FileLogMessageContext<TLogTopics, TLogLevels> flmc)
		{
			string msg = messageFormatter(flmc);
			if (removeRichTextBrackets)
				msg = RemoveRichText(msg);
			return msg;
		}

		private string BaseMessageFormatter(FileLogMessageContext<TLogTopics, TLogLevels> flmc)
		{
			string time = string.Format(timestampFormat, DateTime.Now.ToString(dateTimeFormat));
			string topic = string.Format(topicFormat, flmc.LogTopic.ToString());
			string callerName = string.Format(callerFormat, Path.GetFileNameWithoutExtension(flmc.CallerFilePath), flmc.CallerMemberName);

			string msg;
			if (flmc.Message == null)
			{
				msg = messageIsNullInfoText;
			}
			else
			{
				msg = flmc.Message.ToString();
				if (msg == String.Empty)
					msg = messageIsEmptyInfoText;
			}
			return string.Format(messageFormat, flmc.LogType, time, topic, callerName, msg);
		}


		#endregion Helper

		public void Dispose()
		{
			cts.Cancel();
			hasNewMessages.Set(); // wake thread if waiting
			backgroundTask.Wait();
			hasNewMessages.Dispose();
			cts.Dispose();
		}

	}
}