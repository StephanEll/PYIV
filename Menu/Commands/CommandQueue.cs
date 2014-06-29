using System;
using System.Collections.Generic;
using PYIV.Menu.Commands;

namespace PYIV.Menu.Commands
{
	public class CommandQueue : Queue<QueuedCommand>
	{
		
		
		public delegate void CommandAddedDelegate();
		
		public event CommandAddedDelegate OnFirstCommandAdded;
		
		private object lockObject = new object();
		
		private int commandsInProgress = 0;
		
		public void IncreaseInProgress(){
			lock(lockObject){
				commandsInProgress++;
			}
		}
		
		public void DecreaseInProgress(){
			lock(lockObject){
				commandsInProgress--;
			}
		}
		
		
		public CommandQueue () : base()
		{
		}
		
		public new void Enqueue(QueuedCommand item){
			
			lock(lockObject){
				base.Enqueue(item);
			
				if(this.Count == 1 && commandsInProgress == 0 && OnFirstCommandAdded != null){
					OnFirstCommandAdded();
				}
			}
			
		}
		
		
		
		
	}
}

