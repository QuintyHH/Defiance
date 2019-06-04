using Defiance.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Defiance.HUDHandle
{
	public class DebuffDB
	{
		public List<DebuffEffect> DebuffEffects { get; set; }
		public Dictionary<string, int> SpellWordsToDebuffHash { get; set; }
		public List<SpellWord> SpellWords { get; set; }
		public Profile CurrentProfile { get; set; }
		public DebuffDB()
		{
			this.DebuffEffects = new List<DebuffEffect>();
			this.SpellWords = new List<SpellWord>();
		}

		public void BuildHash()
		{
			try
			{
				this.SpellWordsToDebuffHash = new Dictionary<string, int>();
				foreach (DebuffEffect debuffEffect in this.DebuffEffects)
				{
					foreach (DebuffEffectDetails debuffEffectDetails in debuffEffect.EffectDetails)
					{
						if (!this.SpellWordsToDebuffHash.ContainsKey(debuffEffectDetails.SpellWords.ToLower().Replace(" ", "")))
						{
							this.SpellWordsToDebuffHash.Add(debuffEffectDetails.SpellWords.ToLower().Replace(" ", ""), debuffEffect.EffectId);
						}
					}
				}
			}
            catch (Exception ex) { Repo.RecordException(ex); }
        }

		public int SpellWordsToParticleEffect(string spellWords)
		{
			try
			{
				spellWords = spellWords.Replace(" ", "").ToLower();
				if (this.SpellWordsToDebuffHash.ContainsKey(spellWords))
				{
					return this.SpellWordsToDebuffHash[spellWords];
				}
			}
            catch (Exception ex) { Repo.RecordException(ex); }
            return 0;
		}

        public DebuffInfo GetDebuff(int effect)
        {
            try
            {
                var enumerable = from s in this.SpellWords
                                 where s.Effect.Equals(effect)
                                 select s into sp
                                 join de in this.DebuffEffects on sp.Effect equals de.EffectId
                                 select new
                                 {
                                     CastTime = sp.CastTime,
                                     Effect = sp.Effect,
                                     SpSpellWords = sp.SpellWords,
                                     DebuffCategory = de.DebuffCategory,
                                     DefaultDuration = de.DefaultDuration,
                                     DisplayName = de.DisplayName,
                                     EffectDetails = de.EffectDetails,
                                     EffectId = de.EffectId,
                                     SpellWords = de.SpellWords
                                 };
                foreach (var f__AnonymousType in enumerable)
                {
                    TimeSpan timeSpan = DateTime.Now.Subtract(f__AnonymousType.CastTime);
                    if (((f__AnonymousType.DebuffCategory == DebuffCategories.Life && timeSpan.TotalSeconds >= (double)this.CurrentProfile.DebuffWindowMinLife && timeSpan.TotalSeconds <= (double)this.CurrentProfile.DebuffWindowMaxLife)) && f__AnonymousType.EffectDetails.Count > 0)
                    {
                        foreach (DebuffEffectDetails debuffEffectDetails in f__AnonymousType.EffectDetails)
                        {
                            if (debuffEffectDetails.SpellWords.Trim().Replace(" ", "").ToLower().Equals(f__AnonymousType.SpSpellWords.Trim().Replace(" ", "").ToLower()))
                            {
                                return new DebuffInfo
                                {
                                    Effect = f__AnonymousType.Effect,
                                    DisplayName = debuffEffectDetails.DisplayName,
                                    DefaultDuration = ((debuffEffectDetails.DefaultDuration <= 0) ? ((f__AnonymousType.DefaultDuration <= 0) ? this.CurrentProfile.GetDefaultDuration(f__AnonymousType.DebuffCategory) : f__AnonymousType.DefaultDuration) : debuffEffectDetails.DefaultDuration)
                                };
                            }
                        }
                    }
                }
                IEnumerable<DebuffEffect> enumerable2 = from w in this.DebuffEffects
                                                        where w.EffectId.Equals(effect)
                                                        select w;
                if (enumerable2 != null && enumerable2.Count<DebuffEffect>() > 0)
                {
                    DebuffEffect debuffEffect = enumerable2.FirstOrDefault<DebuffEffect>();
                    return new DebuffInfo
                    {
                        DefaultDuration = ((debuffEffect.DefaultDuration <= 0) ? this.CurrentProfile.GetDefaultDuration(debuffEffect.DebuffCategory) : debuffEffect.DefaultDuration),
                        DisplayName = enumerable2.FirstOrDefault<DebuffEffect>().DisplayName,
                        Effect = enumerable2.FirstOrDefault<DebuffEffect>().EffectId
                    };
                }
            }      
            catch (Exception ex) { Repo.RecordException(ex); }
            return null;
		}

		public void AddCasting(string spellword, int effect)
		{
			try
			{
				this.SpellWords.Add(new SpellWord
				{
					SpellWords = spellword,
					Effect = effect,
					CastTime = DateTime.Now
				});
			}
            catch (Exception ex) { Repo.RecordException(ex); }
        }

		public DebuffDisplayInfo GetDebuffDisplayInfo(string name)
		{
			DebuffDisplayInfo debuffDisplayInfo = new DebuffDisplayInfo();
			try
			{
				DebuffEffect debuffEffect = this.DebuffEffects.Find((DebuffEffect f) => f.EffectDetails.Count<DebuffEffectDetails>() > 0 && (from w in f.EffectDetails
				where w.DisplayName.Trim().Equals(name.Trim(), StringComparison.CurrentCultureIgnoreCase)
				select w).Count<DebuffEffectDetails>() > 0);
				if (debuffEffect != null)
				{
					debuffDisplayInfo.Icon = debuffEffect.Icon;
					DebuffEffectDetails debuffEffectDetails = (from w in debuffEffect.EffectDetails
					where w.DisplayName.Trim().Equals(name.Trim(), StringComparison.CurrentCultureIgnoreCase)
					select w).First<DebuffEffectDetails>();
                    if (debuffEffectDetails != null)
                    {
                        debuffDisplayInfo.Icon = debuffEffectDetails.Icon;
                    }
				}
				else
				{
					DebuffEffect debuffEffect2 = this.DebuffEffects.Find((DebuffEffect f) => f.DisplayName.Trim().Equals(name.Trim(), StringComparison.CurrentCultureIgnoreCase));
					if (debuffEffect2 != null)
					{
						debuffDisplayInfo.Icon = debuffEffect2.Icon;
					}
				}
			}
            catch (Exception ex) { Repo.RecordException(ex); }
            return debuffDisplayInfo;
		}
	}
}
