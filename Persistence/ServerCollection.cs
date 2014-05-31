using System;
using System.Collections.Generic;
using RestSharp;

namespace PYIV.Persistence
{
	public class ServerCollection<T> where T : ServerModel<T>, new()
	{
		
		public List<T> ModelList { get; set; }
		
		public ServerCollection ()
		{
			
		}
		
		public static void FetchAll(Request<ServerCollection<T>>.SuccessDelegate OnSuccess, Request<ServerCollection<T>>.ErrorDelegate OnError){
			var getRequest = new Request<ServerCollection<T>>(ServerModel.ComputeResourceName(typeof(T))+"Collection",Method.GET);
			getRequest.OnError += OnError;
			getRequest.OnSuccess += OnSuccess;
			getRequest.ExecuteAsync();
			
		}
		
		
	}
}

