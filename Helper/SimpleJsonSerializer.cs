using System.IO;
using RestSharp;
using RestSharp.Serializers;
using SimpleJson;


namespace PYIV.Helper
{
/// <summary>
/// Default JSON serializer for request bodies
/// Doesn't currently use the SerializeAs attribute, defers to Newtonsoft's attributes
/// </summary>
	public class SimpleJsonSerializer : ISerializer
	{
	/// <summary>
	/// Default serializer
	/// </summary>
	public SimpleJsonSerializer()
	{
		ContentType = "application/json";
		SimpleJson.SimpleJson.CurrentJsonSerializerStrategy = SimpleJson.SimpleJson.DataContractJsonSerializerStrategy;
	}
	
	/// <summary>
	/// Serialize the object as JSON
	/// </summary>
	/// <param name="obj">Object to serialize</param>
	/// <returns>JSON as String</returns>
	public string Serialize(object obj)
	{
		return SimpleJson.SimpleJson.SerializeObject(obj);
	}
	
	/// <summary>
	/// Unused for JSON Serialization
	/// </summary>
	public string DateFormat { get; set; }
	/// <summary>
	/// Unused for JSON Serialization
	/// </summary>
	public string RootElement { get; set; }
	/// <summary>
	/// Unused for JSON Serialization
	/// </summary>
	public string Namespace { get; set; }
	/// <summary>
	/// Content type for serialized content
	/// </summary>
	public string ContentType { get; set; }
	}
}
