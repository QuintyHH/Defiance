using System;
using System.Collections.Generic;

namespace Defiance.HUDHandle
{
	public class Target
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<Debuff> Debuffs { get; set; }
		public string Species { get; set; }
		public int Level { get; set; }
		public DateTime CreatedAt { get; set; }
		public TargetTypes TargetType { get; set; }
		public int CurrentHealth { get; set; }
		public int MaxHealth { get; set; }
		public int CurrentStamina { get; set; }
		public int MaxStamina { get; set; }
		public int CurrentMana { get; set; }
		public int MaxMana { get; set; }
		public float CurrentHealthF { get; set; }

        public bool ShowHealth
		{
			get
			{
				return this.CurrentHealth > -1;
			}
		}

        public bool ShowStamina
		{
			get
			{
				return this.CurrentStamina > -1;
			}
		}

		public bool ShowMana
		{
			get
			{
				return this.CurrentMana > -1;
			}
		}

		public string HealthInFormat
		{
			get
			{
				return this.CurrentHealth.ToString() + "/" + this.MaxHealth.ToString();
			}
		}

		public string StaminaInFormat
		{
			get
			{
				return this.CurrentStamina.ToString() + "/" + this.MaxStamina.ToString();
			}
		}

		public string ManaInFormat
		{
			get
			{
				return this.CurrentMana.ToString() + "/" + this.MaxMana.ToString();
			}
		}

		public int CurrentHealthPercentage
		{
			get
			{
				if (!this.ShowHealth)
				{
					return 100;
				}
				if (this.CurrentHealth.Equals(this.MaxHealth))
				{
					return 100;
				}
				return Convert.ToInt32((double)this.CurrentHealth * 100.0 / (double)this.MaxHealth);
			}
		}

		public int CurrentStaminaPercentage
		{
			get
			{
				if (!this.ShowStamina)
				{
					return 100;
				}
				if (this.CurrentStamina.Equals(this.MaxStamina))
				{
					return 100;
				}
				return Convert.ToInt32((double)this.CurrentStamina * 100.0 / (double)this.MaxStamina);
			}
		}

		public int CurrentManaPercentage
		{
			get
			{
				if (!this.ShowMana)
				{
					return 100;
				}
				if (this.CurrentMana.Equals(this.MaxMana))
				{
					return 100;
				}
				return Convert.ToInt32((double)this.CurrentMana * 100.0 / (double)this.MaxMana);
			}
		}

		public string FullName
		{
			get
            {
                string nametoupper = Name.ToUpper();
                string name = "Target: " + nametoupper;
                return name;
            }
		}

		public Target()
		{
			this.Id = 0;
			this.Name = string.Empty;
			this.Species = string.Empty;
			this.Level = 0;
			this.Debuffs = new List<Debuff>();
			this.TargetType = TargetTypes.Player;
	     	this.CurrentHealth = -1;
			this.CurrentStamina = -1;
			this.CurrentMana = -1;
			this.MaxHealth = -1;
			this.MaxStamina = -1;
			this.MaxMana = -1;
			this.CurrentHealthF = 1f;
		}
	}
}
