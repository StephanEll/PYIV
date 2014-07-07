using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;



namespace PYIV.Persistence
{

	
	public static class LocalDataPersistence
	{
		public const string GAMES_FILENAME = "persisted_games.dat"; 
		
		public static void Save(object obj, string filename){
			
			Debug.Log("save file");
			
			BinaryFormatter bf = new BinaryFormatter();
			FileStream fs = File.Open(LocalDataPersistence.fullPath(filename), FileMode.Create);
			
			bf.Serialize(fs, obj);
			fs.Close();
		}
		
		public static T Load<T>(string filename){
			Debug.Log("Load from persistence file");
			Debug.Log(File.Exists(LocalDataPersistence.fullPath(filename)));
			
			if(File.Exists(LocalDataPersistence.fullPath(filename))){
				BinaryFormatter bf = new BinaryFormatter();
				FileStream fs = File.Open(LocalDataPersistence.fullPath(filename), FileMode.Open);
				T data = (T)bf.Deserialize(fs);
				return data;
			}
			else{
				throw new IOException(String.Format("File with name '{0}' doesn't exist", filename));
			}
			return default(T);

		}
		
		public static void DeleteFile(string filename){
			if(File.Exists(LocalDataPersistence.fullPath(filename))){
				File.Delete(fullPath(filename));
			}
		}
					
		private static string fullPath(string filename){
			return Application.persistentDataPath + "/" + filename;
		}
	}
}

