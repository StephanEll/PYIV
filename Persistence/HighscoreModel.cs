using System;
using System.Runtime.Serialization;

namespace PYIV.Persistence
{
	[DataContract]
	[Serializable]
	public class HighscoreModel
	{
		
		public string PlayerName { get; set; }
		public int Score { get; set; }
		public int Position { get; set; }
		
		public HighscoreModel ()
		{
		}
	}
}

