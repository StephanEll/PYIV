using UnityEngine;
using System.Collections;
using RestSharp;
using PYIV.Persistence;
using System;
using PYIV.Persistence.Errors;

public class RestTest : MonoBehaviour
{
	private string content;
	// Use this for initialization
	void Start ()
	{
		

		/*
		Player player = new Player();
		player.Name = "PeterMaan"+DateTime.Now.Millisecond.ToString();
		player.Password = "PasswordOla";
		player.Mail = "Heinzo@gmx.de"+DateTime.Now.ToString();
		player.Save(OnSuccess, OnError);*/
	}
	
	void OnSuccess(ServerModel model){
		Player player = (Player)model;
		Debug.Log (player.Name + " " + player.Password);
		player.Login(LoginComplete, OnError);
	}
	
	void LoginComplete(ServerModel model){
		Debug.Log ("login complete");
	}
	
	void OnError(ServerModel model, RestException error){
		Debug.Log (error.Message);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void OnGUI(){
		GUI.Label(Rect.MinMaxRect(0,0, 400, 400), new GUIContent(content));
		
	}
}

