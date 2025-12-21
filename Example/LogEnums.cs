
namespace QS.ExampleLogger
{

	[System.Flags]
	public enum LogTopic
	{
		None = 0,
		Temp = 1 << 0,
		Input = 1 << 1,
		Audio = 1 << 2,
		GameState = 1 << 3,
		Network = 1 << 4,
		All = ~0
	}

	public enum LogLevel
	{
		Basic,
		Detailed,
		Verbose
	}

}