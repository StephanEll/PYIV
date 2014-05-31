using System;
using UnityEngine;

namespace PYIV.Persistence
{
	public class LoggedInPlayer
	{
		
		private static volatile Player instance;
		private static object syncRoot = new object();
		
		public static Player Instance {
			get {
				lock(syncRoot){
					return instance;
				}
			}
			set {
				lock(syncRoot){
					if(instance == null){
						instance = value;
					}
					else{
						Debug.Log ("there ist already a logged in player");
					}
				}
			}
		}
		
		
		public static bool IsLoggedIn(){
			return Instance != null;
		}
		
		public static void LogOut(){
			lock(syncRoot){
				PlayerPrefs.DeleteAll();
				instance = null;
			}
		}
		
	}
}

