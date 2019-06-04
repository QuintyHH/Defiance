using System;

namespace Defiance.HUDHandle
{
	public class DebuffEffectDetails
	{
		public string DisplayName { get; set; }
		public int DefaultDuration { get; set; }
		public string SpellWords { get; set; }
		public int Icon { get; set; }
		public DebuffEffectDetails()
		{
			this.DefaultDuration = -1;
			this.Icon = 0;
		}
	}
}
