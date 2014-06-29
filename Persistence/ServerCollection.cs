using System;
using System.Collections.Generic;
using RestSharp;

namespace PYIV.Persistence
{
	public class ServerCollection<T> where T : ServerModel<T>, new()
	{
		
		public List<T> ModelList { get; set; }
		public delegate void ModelAddedDelegate(List<T> newList, T newModel);
		public event ModelAddedDelegate OnModelAdded;
		
		
		public ServerCollection ()
		{
			ModelList = new List<T>();
		}
		
		public void FetchAll(Request<ServerCollection<T>>.SuccessDelegate OnSuccess, Request<ServerCollection<T>>.ErrorDelegate OnError){
			var getRequest = new Request<ServerCollection<T>>(ServerModel.ComputeResourceName(typeof(T))+"Collection",Method.GET);
			getRequest.OnSuccess += CopyList;
			getRequest.OnError += OnError;
			getRequest.OnSuccess += OnSuccess;
			getRequest.ExecuteAsync();
			
		}

		void CopyList (ServerCollection<T> responseObject)
		{
			this.ModelList = responseObject.ModelList;
		}
		
		public void AddModel(T model){
			this.ModelList.Add(model);
			if(OnModelAdded != null){
				OnModelAdded(this.ModelList, model);
			}
		}
		
		
	}
}

