using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PYIV.Menu
{
	public class ViewRouter : MonoBehaviour
	{
		
		private const string VIEW_ROUTER_TAG = "ViewRouter";
		
		private Dictionary<ViewCacheKey, BaseView> viewCache;
		
		private const string GUI_PARENT_TAG = "GuiParent";
		private const string SCENE_PARENT_TAG = "SceneParent";
		
		private GameObject guiParent;
		private GameObject sceneParent;
		
		private BaseView activeView;
		
		
		void Start(){
			guiParent = GameObject.FindGameObjectWithTag(GUI_PARENT_TAG);
			sceneParent = GameObject.FindGameObjectWithTag(SCENE_PARENT_TAG);
			viewCache = new Dictionary<ViewCacheKey, BaseView>();						
		}
		
		public void ShowView(Type type){
			ShowViewWithParameter(type, null);
		}
		
	
		
		public void ShowViewWithParameter(Type type, object parameter){
			BaseView view = GetFromCacheOrCreate(type, parameter);
			
			if(activeView != null){
				activeView.RemoveFromScreen();
			}
			
			view.AddToScreen(guiParent, sceneParent);
			activeView = view;
		}

		public void ShowPopup(PopupView popup){
			//BaseView view = GetFromCacheOrCreate(type);

			popup.AddToScreen(guiParent, sceneParent);
			//activeView = view;
			
		
		}
		
		private BaseView GetFromCacheOrCreate(Type type, object parameter){
			BaseView view;
			ViewCacheKey key = new ViewCacheKey(type, parameter);
			bool isInCache = viewCache.TryGetValue(key, out view);
			
			if(!isInCache){
				view = CreateAndCacheView(type, key);
			}

			
			return view;
		}
		private BaseView CreateAndCacheView(Type type, ViewCacheKey cacheKey){
			BaseView view = (BaseView)Activator.CreateInstance(type);
			view.UnpackParameter(cacheKey.Parameter);
			
			if(view.ShouldBeCached()){
				viewCache.Add(cacheKey, view);
			}
			return view;
		}
		
		
		public void DestroyView(string type){
			throw new NotImplementedException();
		}

		public void DestroyPopup(BaseView popup){
			popup.RemoveFromScreen();
			
		}
		
		public static ViewRouter TheViewRouter{
			
			get{
				return GameObject.FindGameObjectWithTag(VIEW_ROUTER_TAG).GetComponent<ViewRouter>();
			}
			
			
		}

		
	
	
	}
}