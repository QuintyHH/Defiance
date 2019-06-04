using System;

namespace Defiance.HUDHandle
{
	public class DebuffEventArgs : EventArgs
	{
	    public int TargetID { get; set; }
		public string DebuffName { get; set; }
		public DebuffEventArgs(int targetID, string debuffName)
		{
			this.TargetID = targetID;
			this.DebuffName = debuffName;
		}
	}
}
