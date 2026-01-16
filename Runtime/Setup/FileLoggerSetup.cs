using QS.Core.Logger;
using System;

namespace QS.Core.Setup
{
	public class FileLoggerSetup<TLogTopics, TLogLevels> where TLogTopics : Enum where TLogLevels : Enum
	{
		public TLogTopics DefaultTopic { get; internal set; }
		public TLogLevels DefaultLevel { get; internal set; }
		public TLogTopics DisplayTopics { get; internal set; }
		public TLogLevels LogDepth { get; internal set; }
		public string FolderPath { get; internal set; }
		public string FileName { get; internal set; }

		public int MaxBatchSize { get; internal set; }
		public TimeSpan MaxBatchInterval { get; internal set; }
		public bool RemoveRichTextBrackets { get; internal set; }

		public Func<FileLogMessageContext<TLogTopics, TLogLevels>, string> CustomMessageFormatter { get; internal set; }

		internal FileLoggerSetup() { }
		public class Factory<TFactory, TSetup> where TFactory : Factory<TFactory, TSetup> where TSetup : FileLoggerSetup<TLogTopics, TLogLevels>, new()
		{

			private TLogTopics _defaultTopic;
			private TLogLevels _defaultLevel;
			private TLogTopics _displayTopics;
			private TLogLevels _logDepth;
			private string _folderPath;
			private string _fileName;

			private int _maxBatchSize = 100;
			private TimeSpan _maxBatchInterval = TimeSpan.FromSeconds(5);
			private bool _removeRichTextBrackets = true;
			private Func<FileLogMessageContext<TLogTopics, TLogLevels>, string> _customMessageFormatter = null;

			public Factory(TLogTopics baseTopic, TLogLevels baseLevel, TLogTopics validTopics, TLogLevels logLevel, string folderPath, string fileName)
			{
				_defaultTopic = baseTopic;
				_defaultLevel = baseLevel;
				_displayTopics = validTopics;
				_logDepth = logLevel;
				_folderPath = folderPath;
				_fileName = fileName;
			}

			public TFactory SetDefaultTopic(TLogTopics v)
			{
				_defaultTopic = v;
				return (TFactory)this;
			}
			public TFactory SetDefaultLevel(TLogLevels v)
			{
				_defaultLevel = v;
				return (TFactory)this;
			}
			public TFactory SetDisplayTopics(TLogTopics v)
			{
				_displayTopics = v;
				return (TFactory)this;
			}
			public TFactory SetLogDepth(TLogLevels v)
			{
				_logDepth = v;
				return (TFactory)this;
			}

			public TFactory SetFolderPath(string v)
			{
				_folderPath = v;
				return (TFactory)this;
			}
			public TFactory SetFileName(string v)
			{
				_fileName = v;
				return (TFactory)this;
			}

			public TFactory SetMaxBatchSize(int v)
			{
				_maxBatchSize = v;
				return (TFactory)this;
			}
			public TFactory SetMaxBatchInterval(TimeSpan v)
			{
				_maxBatchInterval = v;
				return (TFactory)this;
			}
			public TFactory SetRemoveRichTextBrackets(bool v)
			{
				_removeRichTextBrackets = v;
				return (TFactory)this;
			}

			public TFactory SetCustomMessageFormatter(Func<FileLogMessageContext<TLogTopics, TLogLevels>, string> v)
			{
				_customMessageFormatter = v;
				return (TFactory)this;
			}


			public virtual TSetup Create()
			{
				TSetup uls = new()
				{
					DefaultTopic = _defaultTopic,
					DefaultLevel = _defaultLevel,
					DisplayTopics = _displayTopics,
					LogDepth = _logDepth,
					FolderPath = _folderPath,
					FileName = _fileName,
					MaxBatchSize = _maxBatchSize,
					MaxBatchInterval = _maxBatchInterval,
					RemoveRichTextBrackets = _removeRichTextBrackets,
					CustomMessageFormatter = _customMessageFormatter
				};

				return uls;
			}
		}
	}
}