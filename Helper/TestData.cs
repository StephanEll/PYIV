using System;
using UnityEngine;
using System.Collections.Generic;
using PYIV.Persistence;
using PYIV.Gameplay.Enemy;



namespace PYIV.Helper
{
	public class TestData
	{
		
		Player player1;
		Player player2;
		GameData data;
		
		
		public Request<GameData>.SuccessDelegate callback;
		
		public TestData (Request<GameData>.SuccessDelegate callback)
		{
			this.callback = callback;
			player1 = new Player();
			player1.Name = "Henrik"+DateTime.Now.Millisecond;
			player1.Mail = "Test@test.de+"+DateTime.Now.Millisecond;
			player1.Password = "123456";
			
			player1.Save(Player1Created, null);
			
			
		}
		
		void Player1Created(Player player){
			LoggedInPlayer.LogOut();
			
			player2 = new Player();
			player2.Name = "Manfred"+DateTime.Now.Millisecond;
			player2.Password = "123456";
			player2.Mail = "Test@test.de"+DateTime.Now.Millisecond;
			
			player2.Save(Player2Created, null);
			
		}
		
		void Player2Created(Player player){
			LoggedInPlayer.Instance = player2;
			Debug.Log ("someone logged in? " + LoggedInPlayer.IsLoggedIn());
			data = new GameData(player1, player2);
			
			List<EnemyType> types = new List<EnemyType>();
			types.AddRange(EnemyTypeCollection.Instance.EnemyType);
			
			
			
			Round round = new Round();
			round.SentAttackers = types;
			data.OpponentStatus.AddRound(round);
			data.MyStatus.AddRound(round);
			
			data.Save(callback, null);
			
		}
		
	}
}

