﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using RestSharp;
using PYIV.Helper;
using PYIV.Persistence.Errors;
using System.Runtime.Serialization;

namespace PYIV.Persistence
{
	/// <summary>
	/// Abstract baseclass to be inherited from by models which
	/// communicate with server
	/// </summary>
	/// 
	[DataContract]
	public abstract class ServerModel{
		
		[DataMember]
		public string Id { get; set; }		
		
		private static string urlRoot;

		private RestSharp.Deserializers.JsonDeserializer deserializer;
		
		public ServerModel (){	}
		
		public static string ComputeResourceName(Type T){
			var className = T.Name;
			//fist letter to lower
			return Char.ToLowerInvariant(className[0]) + className.Substring(1);
		}
		
		[IgnoreDataMember]
		public static string UrlRoot {
			get {
				if (urlRoot == null) {
					urlRoot = ConfigReader.Instance.GetSetting ("server", "url");
				}
				return urlRoot;
			}
		}
		
		protected RestSharp.Deserializers.JsonDeserializer Deserializer {
			get {
				if (deserializer == null) {
					deserializer = new RestSharp.Deserializers.JsonDeserializer ();
				}
				return deserializer;
			}
			
		}
		
		public override bool Equals (object obj)
		{
			if (obj == null)
				return false;
			if (ReferenceEquals (this, obj))
				return true;
			if (obj.GetType () != typeof(ServerModel))
				return false;
			PYIV.Persistence.ServerModel other = (PYIV.Persistence.ServerModel)obj;
			return Id == other.Id;
		}


		public override int GetHashCode ()
		{
			unchecked {
				return (Id != null ? Id.GetHashCode () : 0);
			}
		}

		
	}
	
	
	/// <summary>
	/// Generic version of ServerModel
	/// </summary>
	public abstract class ServerModel<T> : ServerModel where T : ServerModel<T>, new()
	{
		
		

		/**
		 * Syncs the model with the Server. 
		 * Will create a new Model on the server if it has no Id,
		 * otherwise it will update the existing one
		 * */
		public void Save (Request<T>.SuccessDelegate OnSuccess, Request<T>.ErrorDelegate OnError)
		{
			
			
			if (this.Id == null) {
				Create (OnSuccess, OnError);
			}
			
		}
		
		public static void Fetch(string Id, Request<T>.SuccessDelegate OnSuccess, Request<T>.ErrorDelegate OnError){

			var getRequest = new Request<T>(ComputeResourceName(typeof(T))+"/{id}",Method.GET);
			getRequest.OnError += OnError;
			getRequest.OnSuccess += OnSuccess;
			
			
			getRequest.AddParameter("id", Id);
			getRequest.ExecuteAsync();
			
		}
		
		
		
		private void Create (Request<T>.SuccessDelegate OnSuccess, Request<T>.ErrorDelegate OnError)
		{
			var postRequest = new Request<T>(ComputeResourceName(typeof(T)),Method.POST);
			postRequest.OnSuccess += ParseOnCreate;
			postRequest.OnError += OnError;
			postRequest.OnSuccess += OnSuccess;
			
			
			postRequest.AddBody(this);
			postRequest.ExecuteAsync();
		}
		
		public virtual void ParseOnCreate (T responseObject)
		{		
			if(this.Id != responseObject.Id) Debug.Log ("changed id of model");
			this.Id = responseObject.Id;
		}
		

		
		
		
		
	}
}
