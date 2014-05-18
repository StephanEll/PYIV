using UnityEngine;
using System;
using System.Collections;
using PYIV.Persistence;
using PYIV.Persistence.Errors;
using RestSharp;


public class ConfigsOnStart : MonoBehaviour
{

	//Configuration/Initializationcode at startup
	void Start ()
	{
		//must be created once from the main thread
		var dispatcher = UnityThreadHelper.Dispatcher;
		
		
		
		
		
		
		Player player = new Player();
		player.Name = "Manfred"+DateTime.Now.Millisecond;
		player.Password = "123456";
		player.Save(PlayerSaved, null);
		
		
		
		
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
	
	private void OnSucess(PlayerStatus status){
		Debug.Log(status);
	}
	private void OnError(RestException e){
		Debug.Log(e.Message);
	}

}

