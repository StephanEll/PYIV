using System;
using RestSharp.Deserializers;
using RestSharp;

namespace PYIV.Helper.GCM
{
	public static class DeserializerHelper
	{
		public static T Deserialize<T>(string data){

			var deserializer = new JsonDeserializer ();
			RestResponse r = new RestResponse();
			r.Content = data;
			T deserializedObj = deserializer.Deserialize<T> (r);
			return deserializedObj;
		}
	}
}

