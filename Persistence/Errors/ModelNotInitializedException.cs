using System;

namespace PYIV.Persistence.Errors
{
	public class ModelNotInitializedException : Exception
	{
		public ModelNotInitializedException () : base( "Model has no ID." )
		{
		}
	}
}

