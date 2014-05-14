using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PYIV.Menu
{
	public class ViewRouter : MonoBehaviour
	{
		
		private Dictionary<Type, BaseView> viewCache;
		
		private const string GUI_PARENT_TAG = "GuiParent";
		private const string SCENE_PARENT_TAG = "SceneParent";
		
		private GameObject guiParent;
		private GameObject sceneParent;
		
		private BaseView activeView;
		
		void Start(){
			guiParent = GameObject.FindGameObjectWithTag(GUI_PARENT_TAG);
			sceneParent = GameObject.FindGameObjectWithTag(SCENE_PARENT_TAG);
			viewCache = new Dictionary<Type, BaseView>();
			
			ShowView(typeof(StartView));
						
		}
		
		public void ShowView(Type type){
			BaseView view = GetFromCacheOrCreate(type);
			
			
			
			if(activeView != null){
				activeView.RemoveFromScreen();
			}
			
			view.AddToScreen(guiParent, sceneParent);
			activeView = view;
			

			
		}

		public void ShowPopup(Type type){
			BaseView view = GetFromCacheOrCreate(type);

			view.AddToScreen(guiParent, sceneParent);
			//activeView = view;
			
		
		}
		
		private BaseView GetFromCacheOrCreate(Type type){
			BaseView view;
			bool isInCache = viewCache.TryGetValue(type, out view);
			
			if(!isInCache){
				view = (BaseView)Activator.CreateInstance(type);
				if(view.ShouldBeCached())
					viewCache.Add(type, view);
			}
			
			return view;
		}
		
		public void ShowViewWithParameters(Type type, System.Object parameter){
			throw new NotImplementedException();
			
			
		}
		
		public void DestroyView(string type){
			throw new NotImplementedException();
		}

		public void DestroyPopup(BaseView popup){
			popup.RemoveFromScreen();
		}


		
	
	
	}
}