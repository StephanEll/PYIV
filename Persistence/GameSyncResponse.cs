using System;
using System.Collections.Generic;

namespace PYIV.Persistence
{
	public class GameSyncResponse
	{
		
		public List<GameData> ModelList { get; set; }

		
		public DateTime Timestamp { get; set; }
		
		
		public GameSyncResponse ()
		{
		}
	}
}

