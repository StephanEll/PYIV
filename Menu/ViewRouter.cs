using UnityEngine;
using System.Collections;


namespace PYIV.Menu
{
	public class ViewRouter : MonoBehaviour
	{
		private const string GUI_PARENT_TAG = "GuiParent";
		private const string SCENE_PARENT_TAG = "SceneParent";
		
		private GameObject guiParent;
		private GameObject sceneParent;
		
		void Start(){
			guiParent = GameObject.FindGameObjectWithTag(GUI_PARENT_TAG);
			sceneParent = GameObject.FindGameObjectWithTag(SCENE_PARENT_TAG);
			
			var gameObject = new GameObject();
			gameObject.AddComponent("RegisterView");
			
			RegisterView view = gameObject.GetComponent<RegisterView>();
			
			view.transform.parent = guiParent.transform;
			
		}
		
		public void ShowView(string type){
			//reflection
			
		}
		
		public void ShowViewWithId(string type, string id){
			//reflection
			
			
		}
		
		public void DestroyView(string type){
			
		}
		
	
	
	}
}