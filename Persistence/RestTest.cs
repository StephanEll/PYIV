using UnityEngine;
using System.Collections;
using RestSharp;
using PYIV.Persistence;
using System;

public class RestTest : MonoBehaviour
{
	private string content;
	// Use this for initialization
	void Start ()
	{
		

		
		Player player = new Player();
		player.Name = "Heinzo";//+DateTime.Now.ToString();
		player.Password = "123456";
		player.Mail = "Heinzo@gmx.de";//+DateTime.Now.ToString();
		player.Save();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void OnGUI(){
		GUI.Label(Rect.MinMaxRect(0,0, 400, 400), new GUIContent(content));
		
	}
}

