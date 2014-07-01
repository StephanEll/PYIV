using System;
using System.Collections.Generic;
using PYIV.Helper.GCM;

namespace PYIV.Menu.Commands
{
	public abstract class QueuedCommand : ICommand
	{
		public CommandQueue CommandQueue { get; private set; }
		
		public QueuedCommand (CommandQueue commandQueue)
		{
			this.CommandQueue = commandQueue;
		}

		
		public virtual void Execute(){
			CommandQueue.IncreaseInProgress();
		}
		
		protected void HandleNextCommand(){
			if(CommandQueue.Count > 0){
				CommandQueue.Dequeue().Execute();
			}
			this.CommandQueue.DecreaseInProgress();
		}
		
	}
}

