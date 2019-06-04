using System.Collections.Generic;

namespace Defiance.HUDHandle
{
    public class DebuffEffect
	{
		public int EffectId { get; set; }
		public DebuffCategories DebuffCategory { get; set; }
		public string DisplayName { get; set; }
		public int DefaultDuration { get; set; }
		public string SpellWords { get; set; }
		public List<DebuffEffectDetails> EffectDetails { get; set; }
		public int Icon { get; set; }

		public DebuffEffect()
		{
			this.EffectDetails = new List<DebuffEffectDetails>();
			this.DefaultDuration = -1;
			this.Icon = 0;
		}
	}
}
