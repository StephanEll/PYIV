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
		
		private NotificationType _type;
		
		[DataMember]
		public NotificationType type { 
			get{
				return _type;
			} 
			set{
				type = (NotificationType)value;
			}
		}
		
		public PushNotificationData ()
		{
		}
		
		public override string ToString ()
		{
			return string.Format ("[PushNotificationData: Message={0}, Title={1}]", message, title);
		}
	}
}

