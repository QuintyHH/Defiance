using System;

namespace Defiance.HUDHandle
{
	public class Debuff
	{
		public string Name { get; set; }
		public DateTime AppliedAt { get; set; }
        public int Duration { get; set; }
		public bool HasExpired
		{
			get
			{
				return DateTime.Now.Subtract(this.AppliedAt).TotalSeconds >= (double)this.Duration;
			}
		}

		public int TimeRemaining
		{
			get
			{
				TimeSpan timeSpan = DateTime.Now.Subtract(this.AppliedAt);
				return this.Duration - Convert.ToInt32(Math.Ceiling(timeSpan.TotalSeconds));
			}
		}
	}
}
