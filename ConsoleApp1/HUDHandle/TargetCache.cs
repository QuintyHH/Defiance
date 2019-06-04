using System;
using System.Collections.Generic;
using System.Linq;
using Decal.Adapter.Wrappers;
using Defiance.Utils;

namespace Defiance.HUDHandle
{
    public class TargetCache
    {
        public Dictionary<int, Target> Targets { get; set; }
        public event TargetCache.DebuffExpiredEventHandler OnDebuffExpired;

        public TargetCache()
        {
            try
            {
                this.Targets = new Dictionary<int, Target>();
                PluginCore.LngTmr.Tick += this.tmrExpireDebuffs_Tick;
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public Target GetTarget(int Id)
        {
            try
            {
                if (this.Targets.ContainsKey(Id))
                {
                    return this.Targets[Id];
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
            return null;
        }

        public List<Debuff> GetDebuffs(int Id)
        {
            try
            {
                if (this.Targets.ContainsKey(Id))
                {
                    return this.Targets[Id].Debuffs;
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
            return new List<Debuff>();
        }

        public void DeleteTarget(int id)
        {
            try
            {
                if (this.Targets.ContainsKey(id))
                {
                    this.Targets.Remove(id);
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void AddOrUpdate(WorldObject wo)
        {
            this.AddOrUpdate(wo, string.Empty, 0);
        }
        
        public void AddOrUpdate(WorldObject wo, string Debuff, int Duration)
        {
            try
            {
                if (wo.ObjectClass == ObjectClass.Player)
                {
                    if (this.Targets.ContainsKey(wo.Id))
                    {
                        if (!this.Targets[wo.Id].Name.Equals(wo.Name))
                        {
                            this.Targets[wo.Id] = new Target();
                            this.Targets[wo.Id].Id = wo.Id;
                            this.Targets[wo.Id].Name = wo.Name;
                            this.Targets[wo.Id].CreatedAt = DateTime.Now;
                            this.Targets[wo.Id].TargetType = TargetTypes.Player;
                            this.Targets[wo.Id].Debuffs = new List<Debuff>();
                            if (!string.IsNullOrEmpty(Debuff) && Duration > 0)
                            {
                                this.Targets[wo.Id].Debuffs.Add(new Debuff
                                {
                                    Name = Debuff,
                                    Duration = Duration,
                                    AppliedAt = DateTime.Now
                                });
                            }
                            try
                            {
                                this.Targets[wo.Id].Species = wo.Values(StringValueKey.SecondaryName);
                                this.Targets[wo.Id].Level = wo.Values(LongValueKey.CreatureLevel);
                                goto IL_39B;
                            }
                            catch
                            {
                                goto IL_39B;
                            }
                        }
                        this.Targets[wo.Id].TargetType = TargetTypes.Player;
                        if (!string.IsNullOrEmpty(Debuff))
                        {
                            Debuff debuff = this.Targets[wo.Id].Debuffs.Find((Debuff f) => f.Name.Equals(Debuff));
                            if (debuff == null)
                            {
                                this.Targets[wo.Id].Debuffs.Add(new Debuff
                                {
                                    Name = Debuff,
                                    Duration = Duration,
                                    AppliedAt = DateTime.Now
                                });
                            }
                            else
                            {
                                debuff.AppliedAt = DateTime.Now;
                                this.Targets[wo.Id].Debuffs.RemoveAll((Debuff w) => w.Name.Equals(Debuff));
                                this.Targets[wo.Id].Debuffs.Add(debuff);
                            }
                        }
                    }
                    else
                    {
                        Target target = new Target();
                        target.Id = wo.Id;
                        target.Name = wo.Name;
                        target.CreatedAt = DateTime.Now;
                        target.TargetType = TargetTypes.Player;
                        target.Debuffs = new List<Debuff>();
                        if (!string.IsNullOrEmpty(Debuff) && Duration > 0)
                        {
                            target.Debuffs.Add(new Debuff
                            {
                                Name = Debuff,
                                Duration = Duration,
                                AppliedAt = DateTime.Now
                            });
                        }
                        try
                        {
                            this.Targets[wo.Id].Species = wo.Values(StringValueKey.SecondaryName);
                            this.Targets[wo.Id].Level = wo.Values(LongValueKey.CreatureLevel);
                        }
                        catch
                        {
                        }
                        this.Targets.Add(wo.Id, target);
                    }
                }
                IL_39B:;
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void AddOrUpdate(WorldObject wo, int currentHealth, int maxHealth, int currentStamina, int maxStamina, int currentMana, int maxMana)
        {
            try
            {
                if (wo.ObjectClass == ObjectClass.Player)
                {
                    if (this.Targets.ContainsKey(wo.Id))
                    {
                        this.Targets[wo.Id].CurrentHealth = currentHealth;
                        this.Targets[wo.Id].MaxHealth = maxHealth;
                        try
                        {
                            if (currentStamina >= 0)
                            {
                                this.Targets[wo.Id].CurrentStamina = currentStamina;
                                this.Targets[wo.Id].MaxStamina = maxStamina;
                            }
                            else
                            {
                                this.Targets[wo.Id].CurrentStamina = -1;
                                this.Targets[wo.Id].MaxStamina = -1;
                            }
                            if (currentMana >= 0)
                            {
                                this.Targets[wo.Id].CurrentMana = currentMana;
                                this.Targets[wo.Id].MaxMana = maxMana;
                            }
                            else
                            {
                                this.Targets[wo.Id].CurrentMana = -1;
                                this.Targets[wo.Id].MaxMana = -1;
                            }
                            goto IL_1D2;
                        }
                        catch
                        {
                            goto IL_1D2;
                        }
                    }
                    Target target = new Target();
                    target.Id = wo.Id;
                    target.Name = wo.Name;
                    target.CreatedAt = DateTime.Now;
                    target.Debuffs = new List<Debuff>();
                    target.CurrentHealth = currentHealth;
                    target.MaxHealth = maxHealth;
                    try
                    {
                        if (currentStamina >= 0)
                        {
                            target.CurrentStamina = currentStamina;
                            target.MaxStamina = maxStamina;
                        }
                        else
                        {
                            target.CurrentStamina = -1;
                            target.MaxStamina = -1;
                        }
                        if (currentMana >= 0)
                        {
                            target.CurrentMana = currentMana;
                            target.MaxMana = maxMana;
                        }
                        else
                        {
                            target.CurrentMana = -1;
                            target.MaxMana = -1;
                        }
                    }
                    catch
                    {
                    }
                    this.Targets.Add(wo.Id, target);
                }
                IL_1D2:;
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void tmrExpireDebuffs_Tick(object sender, EventArgs e)
        {
            PluginCore.LngTmr.Tick -= this.tmrExpireDebuffs_Tick;
            try
            {
                IEnumerable<int> enumerable = (from s in this.Targets.Keys
                                               select s).Distinct<int>();
                foreach (int num in enumerable)
                {
                    if (this.Targets[num].Debuffs.Count != 0)
                    {
                        List<Debuff> list = this.Targets[num].Debuffs.FindAll((Debuff f) => f.HasExpired);
                        if (list != null && list.Count != 0)
                        {
                            foreach (Debuff debuff in list)
                            {
                                this.Targets[num].Debuffs.Remove(debuff);
                                if (this.OnDebuffExpired != null)
                                {
                                    this.OnDebuffExpired(this, new DebuffEventArgs(num, debuff.Name));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
            finally
            {
                PluginCore.LngTmr.Tick += this.tmrExpireDebuffs_Tick;
            }
        }

        public delegate void DebuffExpiredEventHandler(object source, DebuffEventArgs e);
    }
}
