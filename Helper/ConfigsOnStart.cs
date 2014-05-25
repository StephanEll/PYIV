using UnityEngine;
using System;
using System.Collections;
using PYIV.Persistence;
using PYIV.Persistence.Errors;
using RestSharp;


public class ConfigsOnStart : MonoBehaviour
{
	GameData gameData;

	//Configuration/Initializationcode at startup
	void Start ()
	{
		//must be created once from the main thread
		var dispatcher = UnityThreadHelper.Dispatcher;
		
		Player player1 = new Player();
		player1.Name = "Henrik";
		player1.Id = "6270652252160000";
		
		Player player2 = new Player();
		player2.Name = "Manfred";
		player2.Id = "5144752345317376";
		
		gameData = new GameData(player1, player2);
		gameData.Save(OnSucess, OnError);
		
		
	}
	
	
	private void PlayerSaved(Player player){
		Debug.Log (player.Id);
		/*Round round = new Round();
		round.RemainingVillageLifepoints = 99;
		round.SentAttackerIds = new System.Collections.Generic.List<int>();
		round.SentAttackerIds.Add(43);
		
		PlayerStatus status = new PlayerStatus((Player)player);
		status.AddRound(round);
		status.Save(OnSucess, OnError);
		*/
	}
	
	private void OnSucess(GameData data){
		
		Debug.Log (gameData.ToString());
		Debug.Log (gameData.PlayerStatus[0].ToString());
	}
	private void OnError(RestException e){
		Debug.Log(e.Message);
	}

}

