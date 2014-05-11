using UnityEngine;
using System.Collections;


public class ConfigsOnStart : MonoBehaviour
{

	//Configuration/Initializationcode at startup
	void Start ()
	{
		//must be created once from the main thread
		var dispatcher = UnityThreadHelper.Dispatcher;
	}

}

