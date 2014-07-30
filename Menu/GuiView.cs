using System;
using UnityEngine;
using PYIV.Helper;


namespace PYIV.Menu
{
	public abstract class GuiView : BaseView
	{
		
		protected GameObject panelPrefab;
		protected GameObject panel;
		
		public GuiView (string prefabName)
		{
			TouchScreenKeyboard.hideInput = true;
            panelPrefab = Resources.Load<GameObject>("Prefabs/UI/"+prefabName);
		}
	
	
		public virtual void AddToScreen (GameObject guiParent, GameObject sceneParent)
		{
			if(panel == null){
				panel = NGUITools.AddChild(guiParent, panelPrefab);
				panel.name = ViewName();
				OnPanelCreated();
			}
			else{
				NGUITools.SetActive(panel, true);
			}
		}
	
		public virtual void RemoveFromScreen ()
		{
			if(ShouldBeCached()){
				NGUITools.SetActive(panel, false);
			}
			else{
				GameObject.Destroy(panel);
			}
		}
	
		protected string ViewName(){
			
			return this.GetType().Name;
		}
		
		protected virtual void OnPanelCreated(){}
		
		public abstract bool ShouldBeCached();
		
		public virtual void UnpackParameter(object parameter){}
		
		public abstract void Back();
		
	
	}
	
}

