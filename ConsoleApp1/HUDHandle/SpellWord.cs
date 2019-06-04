using System;

namespace Defiance.HUDHandle
{
	public class SpellWord
	{
		public int Effect { get; set; }
		public string SpellWords { get; set; }
		public DateTime CastTime { get; set; }
		public SpellWord()
		{
			this.CastTime = DateTime.Now;
		}
	}
}
