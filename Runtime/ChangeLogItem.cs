using System;
using UnityEngine;


namespace QS.Core
{
	public struct ChangeLogItem
	{
		public string ChangedValue;
		public Enum FromValue;
		public Enum ToValue;
		public DateTime TimeOfChange;

		public override string ToString()
		{
			return string.Format("[{0}] Changed '{1}' from '{2}' to '{3}'", TimeOfChange.ToString("HH.mm:ss:fff"), ChangedValue, FromValue.ToString(), ToValue.ToString());
		}
	}
}
