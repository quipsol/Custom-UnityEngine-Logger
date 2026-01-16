using QS.Core.Logger;
using System;
using UnityEngine;


namespace QS.Core.Config
{
	public class UnityLoggerConfig<TLogTopics, TLogLevels> where TLogTopics : Enum where TLogLevels : Enum
	{
		public TLogTopics DefaultTopic { get; internal set; }
		public TLogLevels DefaultLevel { get; internal set; }
		public TLogTopics DisplayTopics { get; internal set; }
		public TLogLevels LogDepth { get; internal set; }
		public bool UseDefaultMessageColor { get; internal set; }
		public Color? DefaultMessageColor { get; internal set; }
		public bool UseCallStackColor { get; internal set; }
		public Color? CallStackColor { get; internal set; }
		public Func<UnityLogMessageContext<TLogTopics, TLogLevels>, string> CustomMessageFormatter { get; internal set; }

		public UnityLoggerConfig() { }

		public class Factory<TFactory, TSetup> where TFactory : Factory<TFactory, TSetup> where TSetup : UnityLoggerConfig<TLogTopics, TLogLevels>, new()
		{

			private TLogTopics _defaultTopic;
			private TLogLevels _defaultLevel;
			private TLogTopics _displayTopics;
			private TLogLevels _logDepth;

			private bool _useDefaultMessageColor = true;
			private Color? _defaultMessageColor = null;
			private bool _useCallStackColor = true;
			private Color? _callStackColor = null;

			private Func<UnityLogMessageContext<TLogTopics, TLogLevels>, string> _customMessageFormatter = null;

			public Factory(TLogTopics baseTopic, TLogLevels baseLevel, TLogTopics validTopics, TLogLevels logLevel)
			{
				_defaultTopic = baseTopic;
				_defaultLevel = baseLevel;
				_displayTopics = validTopics;
				_logDepth = logLevel;
			}

			public TFactory SetDefaultTopic(TLogTopics v)
			{
				_defaultTopic = v;
				return (TFactory)this;
			}
			public TFactory SetDisplayTopics(TLogTopics v)
			{
				_displayTopics = v;
				return (TFactory)this;
			}
			public TFactory SetDefaultLevel(TLogLevels v)
			{
				_defaultLevel = v;
				return (TFactory)this;
			}
			public TFactory SetLogDepth(TLogLevels v)
			{
				_logDepth = v;
				return (TFactory)this;
			}

			public TFactory SetUseDefaultMessageColor(bool v)
			{
				_useDefaultMessageColor = v;
				return (TFactory)this;
			}
			public TFactory SetUseCallStackColor(bool v)
			{
				_useCallStackColor = v;
				return (TFactory)this;
			}
			public TFactory SetDefaultMessageColor(Color? v)
			{
				_defaultMessageColor = v;
				return (TFactory)this;
			}
			public TFactory SetCallStackColor(Color? v)
			{
				_callStackColor = v;
				return (TFactory)this;
			}

			public TFactory SetCustomMessageFormatter(Func<UnityLogMessageContext<TLogTopics, TLogLevels>, string> v)
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
					UseDefaultMessageColor = _useDefaultMessageColor,
					UseCallStackColor = _useCallStackColor,
					DefaultMessageColor = _defaultMessageColor,
					CallStackColor = _callStackColor,
					CustomMessageFormatter = _customMessageFormatter
				};

				return uls;
			}
		}
	}

}