using UnityEngine;
using System.Collections;
using RestSharp;
using PYIV.Persistence;

public class RestTest : MonoBehaviour
{
	private string content;
	// Use this for initialization
	void Start ()
	{
		Player player = new Player();
		player.Name = "Heinzo";
		player.Password = "123456";
		player.Mail = "Heinzo@gmx.de";
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

