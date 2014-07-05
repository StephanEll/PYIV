﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using PYIV.Menu.Popup;

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

			if(activeView != null){
				
				activeView.RemoveFromScreen();
			}
			
			var newCacheKey = new ViewCacheKey(type, parameter);
			BaseView view = GetFromCacheOrCreate(newCacheKey);
			view.AddToScreen(guiParent, sceneParent);
			view.UnpackParameter(parameter);

			activeView = view;
			Debug.Log ("ActiveView = " + activeView.GetType());

		}

		public BaseView ShowPopupWithParameter(Type type, PopupParam parameter){
			//won't cache in this case
			BaseView popup = CreateAndCacheView(new ViewCacheKey(type, parameter));
			popup.AddToScreen(guiParent, sceneParent);
			popup.UnpackParameter(parameter);
			return popup;

		}
		
		public void DestroyView(string type){
		  throw new NotImplementedException();
   
		}
		
		public static ViewRouter TheViewRouter{
			
			get{
				return GameObject.FindGameObjectWithTag(VIEW_ROUTER_TAG).GetComponent<ViewRouter>();
			}
		}
		
		private BaseView GetFromCache(ViewCacheKey key){
			BaseView view = null;
			bool isInCache = viewCache.TryGetValue(key, out view);
			return view;
		}
		
		private BaseView GetFromCacheOrCreate(ViewCacheKey key){
			var view = GetFromCache(key);
			
			if(view == null){
				view = CreateAndCacheView(key);
			}

			
			return view;
		}
		private BaseView CreateAndCacheView(ViewCacheKey cacheKey){
			BaseView view = (BaseView)Activator.CreateInstance(cacheKey.Type);
			
			if(view.ShouldBeCached())
				viewCache.Add(cacheKey, view);
			
			return view;
		}
		
		public void GoBack(){
			if(activeView != null)
				activeView.Back();
		}
		public void Quit(){
			Application.Quit();
		}
		
		
		

		
	
	
	}
}