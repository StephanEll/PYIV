using System;
using System.Runtime.Serialization;
namespace PYIV.Helper.GCM
{
	[DataContract]
	public class PushNotificationData
	{
		[DataMember]
		public string message { get; set; }
		
		[DataMember]
		public string title { get; set; }
		
		[DataMember]
		public String type { 
			
			set{
				NotificationType = (NotificationType)Int32.Parse(value);
			}
		}
		
		public NotificationType NotificationType { get; private set; }
		
		public PushNotificationData ()
		{
		}
		
		public override string ToString ()
		{
			return string.Format ("[PushNotificationData: Message={0}, Title={1}]", message, title);
		}
	}
}

