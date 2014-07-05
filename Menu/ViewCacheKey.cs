using System;
using UnityEngine;

namespace PYIV.Menu
{
	public class ViewCacheKey
	{
		public Type Type { get; set; }
		public object Parameter { get; set; }
		
		public ViewCacheKey (Type type, object parameter)
		{
			this.Type = type;
			this.Parameter = parameter;
		}
		
		public override int GetHashCode ()
		{
			var hashCode = Type.GetHashCode();
			
			if(Parameter != null){
				hashCode = hashCode * 17 + Parameter.GetHashCode();
			}
			
			return hashCode;
		}
		
		public override bool Equals (object obj){
			
			if (obj == null){
            	return false;
        	}
			
			var key = obj as ViewCacheKey;
			
			
			var isEqual = Type.Equals(key.Type);
			
			if(Parameter != null){
				isEqual = isEqual && Parameter.Equals(key.Parameter);
			}
			
			return isEqual;
		
		}
		public override string ToString ()
		{
			return string.Format ("[ViewCacheKey: Type={0}, Parameter={1}]", Type, Parameter);
		}

	}
}

