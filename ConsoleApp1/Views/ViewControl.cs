using Decal.Adapter;
using Decal.Adapter.Wrappers;
using Defiance.BaseHandle;
using Defiance.CollectionHandle;
using Defiance.LogHandle;
using Defiance.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using VirindiViewService;
using VirindiViewService.Controls;

namespace Defiance.Views
{
    class ViewControl
    {
        public void ViewInit()
        {
            try
            {
                if (lib.ViewControlInstance < 1)
                {
                    try
                    {
                        Assembly assembly = typeof(PluginCore).Assembly;
                        using (Stream manifestResourceStream = typeof(PluginCore).Assembly.GetManifestResourceStream("Defiance.Resources.icon.png"))
                        {
                            if (manifestResourceStream != null)
                            {
                                using (Bitmap bitmap = new Bitmap(manifestResourceStream))
                                {
                                    new ACImage(bitmap);
                                }
                            }
                        }
                    }
                    catch (Exception ex) { Repo.RecordException(ex); }

                    View = new MainView();
                    View.CreateView();

                    MainView.Ticker.Text = lib.Ticker.ToString();
                    if (lib.Ticker == 0)
                    {
                        MainView.Ticker.Text = "100";
                        lib.Ticker = 100;
                    }
        
                    MainView.Range.Text = lib.Range.ToString();
                    lib.Range = Convert.ToInt32(MainView.Range.Text);
                    if (lib.Range == 0)
                    {
                        MainView.Range.Text = "150";
                        lib.Range = 150;
                    }

                    MainView.Timer.Text = lib.Timer.ToString();
                    lib.Timer = Convert.ToInt32(MainView.Timer.Text);
                    if (lib.Timer == 0)
                    {
                        MainView.Timer.Text = "120";
                        lib.Timer = 120;
                    }

                    if (lib.Width > 0)
                    {
                        MainView.View.Width = lib.Width;
                    }
                    if (lib.Height > 0)
                    {
                        MainView.View.Height = lib.Height;
                    }
                    if (lib.X >= 0 && lib.Y >= 0)
                    {
                        MainView.View.Location = new Point(lib.X, lib.Y);
                    }

                    MainView.Mode.AddItem("SLAVE", 0);
                    MainView.Mode.AddItem("MASTER", 1);
                    MainView.Mode.AddItem("SOLO", 2);
                    MainView.Mode.AddItem("MACRO", 3);

                    MainView.Element.AddItem("Life Magic", 0);
                    MainView.Element.AddItem("Piercing", 1);
                    MainView.Element.AddItem("Bludgeoning", 2);
                    MainView.Element.AddItem("Slashing", 3);
                    MainView.Element.AddItem("Lightning", 4);
                    MainView.Element.AddItem("Acid", 5);
                    MainView.Element.AddItem("Fire", 6);
                    MainView.Element.AddItem("Cold", 7);

                    MainView.Behaviour.AddItem("Logger", 0);
                    MainView.Behaviour.AddItem("Attack", 1);
                    MainView.Behaviour.AddItem("Vuln + Attack", 2);
                    MainView.Behaviour.AddItem("Vuln + Debuff", 3);

                    MainView.PlayerList.Click += new HudList.delClickedControl(PlayerList_Click);
                    MainView.GuildList.Click += new HudList.delClickedControl(GuildList_Click);
                    MainView.FriendsList.Click += new HudList.delClickedControl(FriendsList_Click);
                    MainView.EnemiesList.Click += new HudList.delClickedControl(EnemiesList_Click);
                    MainView.TimerList.Click += new HudList.delClickedControl(TimerList_Click);
                    MainView.CorpseList.Click += new HudList.delClickedControl(TrackerList_Click);
                    MainView.PortalList.Click += new HudList.delClickedControl(TrackerList_Click);

                    MainView.AddFriend.Hit += AddFriend_Hit;
                    MainView.AddEnemy.Hit += AddEnemy_Hit;
                    MainView.Imperil.Hit += Imperil_Hit;
                    MainView.Bludge.Hit += Bludge_Hit;
                    MainView.Tag.Hit += Tag_Hit;
                    MainView.Slash.Hit += Slash_Hit;
                    MainView.Pierce.Hit += Pierce_Hit;
                    MainView.Light.Hit += Light_Hit;
                    MainView.Acid.Hit += Acid_Hit;
                    MainView.Fire.Hit += Fire_Hit;
                    MainView.Cold.Hit += Cold_Hit;

                    MainView.Strength.Hit += Strength_Hit;
                    MainView.Endurance.Hit += Endurance_Hit;
                    MainView.Coordination.Hit += Coordination_Hit;
                    MainView.Quickness.Hit += Quickness_Hit;
                    MainView.Focus.Hit += Focus_Hit;
                    MainView.Willpower.Hit += Willpower_Hit;

                    MainView.Run.Hit += Run_Hit;

                    MainView.Creature.Hit += Creature_Hit;
                    MainView.Life.Hit += Life_Hit;
                    MainView.War.Hit += War_Hit;
                    MainView.MagicD.Hit += MagicD_Hit;
                    MainView.HeavyW.Hit += HeavyW_Hit;
                    MainView.MelD.Hit += MelD_Hit;
                    MainView.MissW.Hit += MissW_Hit;
                    MainView.MissD.Hit += MissD_Hit;
                    MainView.Stam.Hit += Stam_Hit;
                    MainView.Harm.Hit += Harm_Hit;
                    MainView.StamRegen.Hit += StamRegen_Hit;
                    MainView.IgnoreVP.Hit += IgnoreVP_Hit;
                    MainView.Fixbusy.Hit += Fixbusy_Hit;
                    MainView.Loc.Hit += Loc_Hit;
                    MainView.Die.Hit += Die_Hit;
                    MainView.Mode.Change += Mode_Change;
                    MainView.ResetAlarms.Hit += SettingsControl.ResetAlarms_Hit;
                    MainView.ResetOptions.Hit += SettingsControl.ResetOptions_Hit;
                    MainView.Element.Change += Element_Change;
                    MainView.AlertLS.Change += AlertLS_Change;
                    MainView.AlertOdds.Change += AlertOdds_Change;
                    MainView.AlertBc.Change += AlertBc_Change;
                    MainView.AlertPA.Change += AlertPA_Change;
                    MainView.AlertPF.Change += AlertPF_Change;
                    MainView.AlertLogP.Change += AlertLogP_Change;
                    MainView.AlertDA.Change += AlertDA_Change;
                    MainView.AlertSPK.Change += AlertSPK_Change;
                    MainView.AlertSS.Change += AlertSS_Change;
                    MainView.UseMacroLogic.Change += MacroLogic_Change;
                    MainView.SaveSettings2.Hit += SettingsControl.SaveSettings;
                    MainView.SaveSettings3.Hit += SettingsControl.SaveSettings;
                    MainView.ForcelogButton.Hit += Forcelog_Hit;
                    MainView.ForcelocButton.Hit += Forceloc_Hit;
                    MainView.ForceRelogButton.Hit += ForceRelog_Hit;
                    MainView.ForcedieButton.Hit += Forcedie_Hit;
                    MainView.UseLogDie.Change += LogDie_Change;
                    MainView.AlertDT.Change += AlertDT_Change;
                    MainView.AlertPKT.Change += AlertPKT_Change;

                    ReloadSetting();
                    lib.ViewControlInstance++;
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void Dispose()
        {
            try
            {
                MainView.AddFriend.Hit -= AddFriend_Hit;
                MainView.AddEnemy.Hit -= AddEnemy_Hit;
                MainView.Imperil.Hit -= Imperil_Hit;
                MainView.Bludge.Hit -= Bludge_Hit;
                MainView.Tag.Hit -= Tag_Hit;
                MainView.Slash.Hit -= Slash_Hit;
                MainView.Pierce.Hit -= Pierce_Hit;
                MainView.Light.Hit -= Light_Hit;
                MainView.Acid.Hit -= Acid_Hit;
                MainView.Fire.Hit -= Fire_Hit;
                MainView.Cold.Hit -= Cold_Hit;

                MainView.Strength.Hit -= Strength_Hit;
                MainView.Endurance.Hit -= Endurance_Hit;
                MainView.Coordination.Hit -= Coordination_Hit;
                MainView.Quickness.Hit -= Quickness_Hit;
                MainView.Focus.Hit -= Focus_Hit;
                MainView.Willpower.Hit -= Willpower_Hit;

                MainView.Run.Hit -= Run_Hit;

                MainView.Creature.Hit -= Creature_Hit;
                MainView.Life.Hit -= Life_Hit;
                MainView.War.Hit -= War_Hit;
                MainView.MagicD.Hit -= MagicD_Hit;
                MainView.HeavyW.Hit -= HeavyW_Hit;
                MainView.MelD.Hit -= MelD_Hit;
                MainView.MissW.Hit -= MissW_Hit;
                MainView.MissD.Hit -= MissD_Hit;
                MainView.Stam.Hit -= Stam_Hit;
                MainView.Harm.Hit -= Harm_Hit;
                MainView.StamRegen.Hit -= StamRegen_Hit;
                MainView.IgnoreVP.Hit -= IgnoreVP_Hit;
                MainView.Fixbusy.Hit -= Fixbusy_Hit;
                MainView.Loc.Hit -= Loc_Hit;
                MainView.Die.Hit -= Die_Hit;
                MainView.Mode.Change -= Mode_Change;
                MainView.ResetAlarms.Hit -= SettingsControl.ResetAlarms_Hit;
                MainView.ResetOptions.Hit -= SettingsControl.ResetOptions_Hit;
                MainView.Element.Change -= Element_Change;
                MainView.AlertOdds.Change -= AlertOdds_Change;
                MainView.AlertBc.Change -= AlertBc_Change;
                MainView.AlertLS.Change -= AlertLS_Change;
                MainView.AlertPA.Change -= AlertPA_Change;
                MainView.AlertPF.Change -= AlertPF_Change;
                MainView.AlertLogP.Change -= AlertLogP_Change;
                MainView.AlertDA.Change -= AlertDA_Change;
                MainView.AlertSPK.Change -= AlertSPK_Change;
                MainView.AlertSS.Change -= AlertSS_Change;
                MainView.UseMacroLogic.Change -= MacroLogic_Change;
                MainView.SaveSettings2.Hit -= SettingsControl.SaveSettings;
                MainView.SaveSettings3.Hit -= SettingsControl.SaveSettings;
                MainView.ForcelogButton.Hit -= Forcelog_Hit;
                MainView.ForcelocButton.Hit -= Forceloc_Hit;
                MainView.ForceRelogButton.Hit -= ForceRelog_Hit;
                MainView.ForcedieButton.Hit -= Forcedie_Hit;
                MainView.UseLogDie.Change -= LogDie_Change;
                MainView.AlertDT.Change -= AlertDT_Change;
                MainView.AlertPKT.Change -= AlertPKT_Change;

                MainView.Mode = null;
                MainView.Element = null;
                MainView.Behaviour = null;
                View.Dispose();
                View = null;
                lib.ViewControlInstance--;
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void ReloadSetting()
        {
            try
            {
                MainView.FriendsList.ClearRows();
                MainView.EnemiesList.ClearRows();

                lib.Friends = Repo.GetFriends();
                lib.Enemies = Repo.GetEnemies();

                foreach (Friend friend in lib.Friends)
                {
                    HudList.HudListRowAccessor hudListRowAccessor = MainView.FriendsList.AddRow();
                    ((HudStaticText)hudListRowAccessor[0]).Text = (string.IsNullOrEmpty(friend.Name) ? "Name" : friend.Name);
                    ((HudPictureBox)hudListRowAccessor[1]).Image = 100667896;
                    ((HudStaticText)hudListRowAccessor[2]).Text = friend.ID.ToString();
                    ((HudStaticText)hudListRowAccessor[2]).Visible = false;
                    ((HudStaticText)hudListRowAccessor[0]).TextColor = Color.Green;
                }

                foreach (Enemy enemy in lib.Enemies)
                {
                    HudList.HudListRowAccessor hudListRowAccessor = MainView.EnemiesList.AddRow();
                    ((HudStaticText)hudListRowAccessor[0]).Text = (string.IsNullOrEmpty(enemy.Name) ? "Name" : enemy.Name);
                    ((HudPictureBox)hudListRowAccessor[1]).Image = 100667896;
                    ((HudStaticText)hudListRowAccessor[2]).Text = enemy.ID.ToString();
                    ((HudStaticText)hudListRowAccessor[2]).Visible = false;
                    ((HudStaticText)hudListRowAccessor[0]).TextColor = Color.Red;
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public static void SaveUI()
        {
            try
            {
                UI obj = new UI
                {
                    ServerName = CoreManager.Current.CharacterFilter.Server,
                    PlayerName = CoreManager.Current.CharacterFilter.Name,

                    Height = MainView.View.Height,
                    Width = MainView.View.Width,
                    X = MainView.View.Location.X,
                    Y = MainView.View.Location.Y,

                    UseAlertLS = MainView.AlertLS.Checked,
                    UseAlertPA = MainView.AlertPA.Checked,
                    UseAlertPF = MainView.AlertPF.Checked,
                    UseAlertLogP = MainView.AlertLogP.Checked,
                    UseAlertDA = MainView.AlertDA.Checked,
                    UseAlertSPK = MainView.AlertSPK.Checked,
                    UseAlertSS = MainView.AlertSS.Checked,
                    UseAlertOdds = MainView.AlertOdds.Checked,
                    UseAlertBc = MainView.AlertBc.Checked,
                    UseAlertPKT = MainView.AlertPKT.Checked,
                    UseAlertDT = MainView.AlertDT.Checked,

                    UseMacroLogic = MainView.UseMacroLogic.Checked,
                    UseLogDie = MainView.UseLogDie.Checked,
                    Range = Convert.ToInt32(MainView.Range.Text),
                    Timer = Convert.ToInt32(MainView.Timer.Text),
                    Comps = Convert.ToInt32(MainView.Comps.Text),
                    Slots = Convert.ToInt32(MainView.Slots.Text),
                    Behaviour = MainView.Behaviour.Current,
                    Element = MainView.Element.Current,
                    Ticker = Convert.ToInt32(MainView.Ticker.Text),
                    MonCheck = lib.moncheck,
                };
                Repo.SaveUI(obj);
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }
        
        protected void TrackerList_Click(object sender, int row, int col)
        {
            try
            {
                HudList hudList = (HudList)sender;
                if (col == 4)
                {
                    hudList.RemoveRow(row);
                }
                else
                {
                    int num = int.Parse(((HudStaticText)hudList[row][5]).Text);
                    if (lib.MyHost.Actions.IsValidObject(num))
                    {
                        CoreManager.Current.Actions.SelectItem(num);
                        if (lib.PointArrow != null)
                        {
                            lib.PointArrow.Dispose();
                        }

                        lib.PointArrow = lib.MyCore.D3DService.PointToObject(num, Color.Purple.ToArgb());
                        lib.PointArrow.ScaleX = 1f;
                        lib.PointArrow.ScaleY = 2f;
                        lib.PointArrow.ScaleZ = 1f;
                        lib.LastSelected = num;
                    }
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void EnemiesList_Click(object sender, int row, int col)
        {
            try
            {
                if (col == 1)
                {
                    HudList hudList = (HudList)sender;
                    string name = Convert.ToString(((HudStaticText)MainView.EnemiesList[row][0]).Text);
                    int id = int.Parse(((HudStaticText)MainView.EnemiesList[row][2]).Text);
                    hudList.RemoveRow(row);
                    Repo.DeleteEnemy(id);
                    ReloadSetting();
     
                    Utility.AddChatText("Enemy removed: " + name, 6);
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void FriendsList_Click(object sender, int row, int col)
        {
            try
            {
                if (col == 1)
                {
                    HudList hudList = (HudList)sender;
                    string name = Convert.ToString(((HudStaticText)MainView.FriendsList[row][0]).Text);
                    int id = int.Parse(((HudStaticText)MainView.FriendsList[row][2]).Text);
                    hudList.RemoveRow(row);
                    Repo.DeleteFriend(id);
                    ReloadSetting();

                    Utility.AddChatText("Friend removed: " + name, 6);
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void GuildList_Click(object sender, int row, int col)
        {
            try
            {
                HudList hudList = (HudList)sender;

                string name = ((HudStaticText)hudList[row][1]).Text;
                string coords = ((HudStaticText)hudList[row][3]).Text;
                int num = int.Parse(((HudStaticText)hudList[row][5]).Text);
                double distance = CoreManager.Current.WorldFilter.Distance(num, CoreManager.Current.CharacterFilter.Id) * 240.0;

                WorldObject obj = CoreManager.Current.WorldFilter[num];
                string key = obj.Values(LongValueKey.Landblock).ToString("X8").Substring(0, 4);
                List<string> Landblock = (from f in lib.LocKey.Split(new char[] { ',' })
                                          select f.Trim()).ToList<string>();
                if (distance <= 500)
                {
                    if (col == 4)
                    {
                        hudList.RemoveRow(row);
                    }
                    else if (col == 0)
                    {
                        if (lib.MyCore.Actions.IsValidObject(num))
                        {
                            if (num == lib.MyCore.CharacterFilter.Id)
                            {
                                CoreManager.Current.Actions.SelectItem(num);
                            }
                            else
                            {
                                CoreManager.Current.Actions.SelectItem(num);
                                if (lib.PointArrow != null)
                                {
                                    lib.PointArrow.Dispose();
                                }

                                lib.PointArrow = lib.MyCore.D3DService.PointToObject(num, Color.Green.ToArgb());
                                lib.PointArrow.ScaleX = 1f;
                                lib.PointArrow.ScaleY = 2f;
                                lib.PointArrow.ScaleZ = 1f;
                                lib.LastSelected = num;
                            }
                        }
                    }
                    else if (col == 1)
                    {
                        if (lib.MyCore.Actions.IsValidObject(num))
                        {
                            if (num == lib.MyCore.CharacterFilter.Id)
                            {
                                CoreManager.Current.Actions.SelectItem(num);
                                Utility.InvokeTextF("Requesting HEAL! Current HP:" + lib.MyCore.CharacterFilter.Health);
                            }
                            else if (num != lib.MyCore.CharacterFilter.Id && distance <= 82)
                            {
                                if (CoreManager.Current.Actions.CombatMode == CombatState.Magic)
                                {
                                    if (num != lib.MyCore.CharacterFilter.Id)
                                    {
                                        Utility.InvokeTextF("Casting heal on: " + name + " arriving in 3.5 seconds!");
                                        if (lib.MyCore.CharacterFilter.IsSpellKnown(4310))
                                        {
                                            lib.MyHost.Actions.CastSpell(4310, num);
                                        }
                                        else
                                        {
                                            lib.MyHost.Actions.CastSpell(2072, num);
                                        }
                                    }
                                }
                            }
                            else if (num != lib.MyCore.CharacterFilter.Id && distance > 82 && distance < 500)
                            {
                                if (CoreManager.Current.Actions.CombatMode == CombatState.Magic)
                                {
                                    Utility.InvokeTextF(name + " is too far away for heals!");
                                }
                            }

                        }
                    }
                    else if (col == 2 || col == 3)
                    {
                        if (lib.MyCore.Actions.IsValidObject(num))
                        {
                            if (num == lib.MyCore.CharacterFilter.Id)
                            {
                                Post_LOC();
                            }
                        }
                    }
                }
                else
                {
                    if (lib.LocKey.Contains(key))
                    {
                        foreach (string el in Landblock)
                        {
                            if (el.Contains(key))
                            {
                                string elloc = el.Split(new string[] { "=" }, StringSplitOptions.None)[1];
                                Utility.InvokeTextA(obj.Name + " was last seen at " + coords + " (" + elloc + ")");
                            }
                        }
                    }
                    else
                    {
                        Utility.InvokeTextA(obj.Name + " was last seen at " + coords + " (" + key + " : Unset)");
                    }
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void PlayerList_Click(object sender, int row, int col)
        {
            try
            {
                HudList hudList = (HudList)sender;

                string name = ((HudStaticText)hudList[row][1]).Text;
                string coords = ((HudStaticText)hudList[row][3]).Text;
                int num = int.Parse(((HudStaticText)hudList[row][5]).Text);
                double distance = CoreManager.Current.WorldFilter.Distance(num, CoreManager.Current.CharacterFilter.Id) * 240.0;

                if (distance <= 500)
                {
                    if (col == 4)
                    {
                        hudList.RemoveRow(row);
                    }

                    else if (col == 0)
                    {
                        if (lib.MyCore.Actions.IsValidObject(num))
                        {
                            CoreManager.Current.Actions.SelectItem(num);
                            if (lib.PointArrow != null)
                            {
                                lib.PointArrow.Dispose();
                            }

                            lib.PointArrow = lib.MyCore.D3DService.PointToObject(num, Color.Red.ToArgb());
                            lib.PointArrow.ScaleX = 1f;
                            lib.PointArrow.ScaleY = 2f;
                            lib.PointArrow.ScaleZ = 1f;
                            lib.LastSelected = num;

                            foreach (WorldObject worldObject in lib.MyCore.WorldFilter.GetByOwner(num))
                            {
                                if (worldObject.Name.IndexOf("arrow", 0) < 0)
                                {
                                    string a = worldObject.ObjectClass.ToString().Trim();
                                    if (a == "WandStaffOrb" || a == "MissileWeapon")
                                    {
                                        if (CoreManager.Current.Actions.CombatMode == CombatState.Magic)
                                        {
                                            if (lib.MyCore.CharacterFilter.IsSpellKnown(2112))
                                            {
                                                if (distance <= 27)
                                                {
                                                    lib.MyHost.Actions.CastSpell(2112, num);
                                                }
                                                else
                                                {
                                                   Utility.AddChatText("Target too far away for Wi's Folly!", 6);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Utility.AddChatText("You don't have Wi's Folly!", 6);
                                        }
                                    }
                                    else if (a == "MeleeWeapon")
                                    {
                                        if (CoreManager.Current.Actions.CombatMode == CombatState.Magic)
                                        {
                                            if (lib.MyCore.CharacterFilter.IsSpellKnown(2118))
                                            {
                                                if (distance <= 27)
                                                {
                                                    lib.MyHost.Actions.CastSpell(2118, num);
                                                }
                                                else
                                                {
                                                    Utility.AddChatText("Target too far away for Clouded Motives!", 6);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Utility.AddChatText("You don't have Clouded Motives!", 6);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (col == 1)
                    {

                        if (lib.MyCore.Actions.IsValidObject(num))
                        {
                            CoreManager.Current.Actions.SelectItem(num);
                            if (lib.PointArrow != null)
                            {
                                lib.PointArrow.Dispose();
                            }

                            lib.PointArrow = lib.MyCore.D3DService.PointToObject(num, Color.Red.ToArgb());
                            lib.PointArrow.ScaleX = 1f;
                            lib.PointArrow.ScaleY = 2f;
                            lib.PointArrow.ScaleZ = 1f;
                            lib.LastSelected = num;

                            if (lib.Mode == 1)
                            {
                                Utility.InvokeTextF("Calling target: " + name + " at " + coords + "! TargetID: " + num + ":");
                            }
                        }
                    }
                    else if (col == 2 || col == 3)
                    {

                        if (lib.MyCore.Actions.IsValidObject(num))
                        {
                            CoreManager.Current.Actions.SelectItem(num);
                            if (lib.PointArrow != null)
                            {
                                lib.PointArrow.Dispose();
                            }

                            lib.PointArrow = lib.MyCore.D3DService.PointToObject(num, Color.Red.ToArgb());
                            lib.PointArrow.ScaleX = 1f;
                            lib.PointArrow.ScaleY = 2f;
                            lib.PointArrow.ScaleZ = 1f;
                            lib.LastSelected = num;
                        }
                    }
                }
                else
                {

                    string key = CoreManager.Current.WorldFilter[num].Values(LongValueKey.Landblock).ToString("X8").Substring(0, 4);
                    List<string> Landblock = (from f in lib.LocKey.Split(new char[] { ',' })
                                              select f.Trim()).ToList<string>();

                    if (lib.LocKey.Contains(key))
                    {
                        foreach (string el in Landblock)
                        {
                            if (el.Contains(key))
                            {
                                string elloc = el.Split(new string[] { "=" }, StringSplitOptions.None)[1];
                                Utility.InvokeTextA(name + " was last seen at " + coords + " (" + elloc + ")");
                            }
                        }
                    }
                    else
                    {
                        Utility.InvokeTextA(name + " was last seen at " + coords + " (" + key + " : Unset)");
                    }
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void TimerList_Click(object sender, int row, int col)
        {
            try
            {
                HudList hudList = (HudList)sender;
                string name = Convert.ToString(((HudStaticText)hudList[row][1]).Text);
                string timer = Convert.ToString(((HudStaticText)hudList[row][2]).Text);
                string killer = Convert.ToString(((HudStaticText)hudList[row][5]).Text);

                if (col == 0)
                {
                    hudList.RemoveRow(row);
                    if (lib.OnDowntime.Contains(name))
                    {
                        lib.OnDowntime.Remove(name);
                    }
                    if (lib.OnDowntime2.Contains(name))
                    {
                        lib.OnDowntime2.Remove(name);
                    }
                }
                else
                {
                    Utility.InvokeTextA(name + " will turn red in " + timer + " seconds! (Killed by " + killer + ")");
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void Post_LOC()
        {
            try
            {
                WorldObject me = CoreManager.Current.WorldFilter[lib.MyCore.CharacterFilter.Id];
                CoordsObject coordsObject = me.Coordinates();

                string text;
                if (coordsObject.NorthSouth >= 0.0)
                {
                    text = string.Format("{0:N1}", coordsObject.NorthSouth) + "N, ";
                }
                else
                {
                    text = string.Format("{0:N1}", coordsObject.NorthSouth * -1.0) + "S, ";
                }
                if (coordsObject.EastWest >= 0.0)
                {
                    text = text + string.Format("{0:N1}", coordsObject.EastWest) + "E";
                }
                else
                {
                    text = text + string.Format("{0:N1}", coordsObject.EastWest * -1.0) + "W";
                }

                double heading = me.Values(DoubleValueKey.Heading);

                string direction = "nowhere";
                if (heading <= 110 && heading >= 70)
                {
                    direction = "East";
                }
                else if ((heading <= 360 && heading >= 340) || (heading >= 0 && heading <= 20))
                {
                    direction = "North";
                }
                else if (heading <= 200 && heading >= 160)
                {
                    direction = "South";
                }
                else if (heading <= 290 && heading >= 250)
                {
                    direction = "West";
                }
                else if (heading < 70 && heading > 20)
                {
                    direction = "North-East";
                }
                else if (heading < 160 && heading > 110)
                {
                    direction = "South-East";
                }
                else if (heading < 250 && heading > 200)
                {
                    direction = "South-West";
                }
                else if (heading < 340 && heading > 290)
                {
                    direction = "North-West";
                }

                string key = me.Values(LongValueKey.Landblock).ToString("X8").Substring(0, 4);
                List<string> Landblock = (from f in lib.LocKey.Split(new char[] { ',' })
                                          select f.Trim()).ToList<string>();

                if (lib.LocKey.Contains(key))
                {
                    foreach (string el in Landblock)
                    {
                        if (el.Contains(key))
                        {
                            string elloc = el.Split(new string[] { "=" }, StringSplitOptions.None)[1];
                            Utility.InvokeTextA("My coords are " + text + " , heading " + direction + " (" + elloc + ")");
                        }
                    }
                }
                else
                {
                    Utility.InvokeTextA("My coords are " + text + " , heading " + direction + " (" + key + " : Unset)");
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }
        
        public void AddFriend_Hit(object sender, EventArgs e)
        {
            try
            {
                Friend friend = new Friend();
                string text = MainView.Friend.Text;
                if (!string.IsNullOrEmpty(text))
                {
                    if (text != lib.MyName)
                    {
                        if (text != "Name")
                        {
                            if (!string.IsNullOrEmpty(text))
                            {
                                friend.Name = text;
                            }
                            else
                            {
                                friend.Name = string.Empty;
                            }
                            Repo.AddFriend(friend);
                            Utility.AddChatText("Friend added: " + friend.Name, 6);
                            ReloadSetting();
                        }
                    }
                    else
                    {
                        Utility.AddChatText("You can't add yourself.", 6);
                    }
                }
                MainView.Friend.Text = "Name";
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void AddEnemy_Hit(object sender, EventArgs e)
        {
            try
            {
                Enemy enemy = new Enemy();
                string text = MainView.Enemy.Text;
                if (!string.IsNullOrEmpty(text))
                {
                    if (text != lib.MyName)
                    {
                        if (text != "Name")
                        {
                            if (!string.IsNullOrEmpty(text))
                            {
                                enemy.Name = text;
                            }
                            else
                            {
                                enemy.Name = string.Empty;
                            }
                            Repo.AddEnemy(enemy);
                            Utility.AddChatText("Friend added: " + enemy.Name, 6);
                            ReloadSetting();
                        }
                    }
                    else
                    {
                        Utility.AddChatText("You can't add yourself.", 6);
                    }
                }
                MainView.Friend.Text = "Name";
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void Die_Hit(object sender, EventArgs e)
        {
            if (lib.UseLogDie == true)
            {
                Utility.DispatchChatToBoxWithPluginIntercept("/die");
                lib.reason = "suicide";
                Utility.ClickYes();
            }
            else
            {
                Utility.AddChatText("Die button not enabled. Enable it in Settings > Adv. Options.", 6);
            }
        }
       
        public void Forcelog_Hit(object sender, EventArgs e)
        {
            if (lib.status == 2)
            {
                List<string> list = (from f in lib.AuthIds.Split(new char[] { ',' })
                                     select f.Trim()).ToList<string>();
                bool IsAuthed = list.Contains(MainView.ForcelogBox.Text);
                if (IsAuthed == true)
                {
                    string pass = DateTime.Now.Ticks.ToString();
                    CoreManager.Current.Actions.InvokeChatParser("/w " + MainView.ForcelogBox.Text + ", DFcom:Log: " + pass);
                }
                else
                {
                    Utility.AddChatText( "Can not use that command on " + MainView.ForcelogBox.Text + "!", 6);
                }
            }
            else
            {
                Utility.AddChatText("Not authorized.", 6);
            }
        }

        public void ForceRelog_Hit(object sender, EventArgs e)
        {
            if (lib.status == 2)
            {
                List<string> list = (from f in lib.AuthIds.Split(new char[] { ',' })
                                     select f.Trim()).ToList<string>();
                bool IsAuthed = list.Contains(MainView.ForcelogBox.Text);
                if (IsAuthed == true)
                {
                    string pass = DateTime.Now.Ticks.ToString();
                    CoreManager.Current.Actions.InvokeChatParser("/w " + MainView.ForcelogBox.Text + ", DFcom:Relog: " + pass);
                }
                else
                {
                    Utility.AddChatText("Can not use that command on " + MainView.ForcelogBox.Text + "!", 6);
                }
            }
            else
            {
                Utility.AddChatText("Not authorized.", 6);
            }
        }

        public void Forceloc_Hit(object sender, EventArgs e)
        {
            if (lib.status == 2)
            {
                List<string> list = (from f in lib.AuthIds.Split(new char[] { ',' })
                                     select f.Trim()).ToList<string>();
                bool IsAuthed = list.Contains(MainView.ForcelogBox.Text);
                if (IsAuthed == true)
                {
                    string pass = DateTime.Now.Ticks.ToString();
                    CoreManager.Current.Actions.InvokeChatParser("/w " + MainView.ForcelogBox.Text + ", DFcom:Location: " + pass);
                }
                else
                {
                    Utility.AddChatText("Can not use that command on " + MainView.ForcelogBox.Text + "!", 6);
                }
            }
            else
            {
                Utility.AddChatText("Not authorized.", 6);
            }
        }

        public void Forcedie_Hit(object sender, EventArgs e)
        {
            if (lib.status == 2)
            {
                List<string> list = (from f in lib.AuthIds.Split(new char[] { ',' })
                                     select f.Trim()).ToList<string>();
                bool IsAuthed = list.Contains(MainView.ForcelogBox.Text);
                if (IsAuthed == true)
                {
                    string pass = DateTime.Now.Ticks.ToString();
                    CoreManager.Current.Actions.InvokeChatParser("/w " + MainView.ForcelogBox.Text + ", DFcom:Die: " + pass);
                }
                else
                {
                    Utility.AddChatText("Can not use that command on " + MainView.ForcelogBox.Text + "!", 6);
                }
            }
            else
            {
                Utility.AddChatText("Not authorized.", 6);
            }
        }

        public void Tag_Hit(object sender, EventArgs e)
        {
            WorldObject worldObject = lib.MyCore.WorldFilter[lib.MyHost.Actions.CurrentSelection];
            if (CoreManager.Current.Actions.CombatMode == CombatState.Magic)
            {
                if (lib.MyCore.CharacterFilter.IsSpellKnown(280))
                {
                    lib.MyHost.Actions.CastSpell(280, lib.MyHost.Actions.CurrentSelection);
                }
                else
                {
                    Utility.AddChatText("TAG Spell not known. (Magic Yield Other I)", 6);
                }
            }
            else if (CoreManager.Current.Actions.CombatMode == CombatState.Melee || CoreManager.Current.Actions.CombatMode == CombatState.Missile)
            {
                Utility.InvokeTextF("Requesting TAG on " + lib.MyHost.Actions.CurrentSelection);
            }
        }

        public void Imperil_Hit(object sender, EventArgs e)
        {
            WorldObject worldObject = lib.MyCore.WorldFilter[lib.MyHost.Actions.CurrentSelection];
            if (CoreManager.Current.Actions.CombatMode == CombatState.Magic)
            {
                if (lib.MyCore.CharacterFilter.IsSpellKnown(4312))
                {
                    lib.MyHost.Actions.CastSpell(4312, lib.MyHost.Actions.CurrentSelection);
                }
                else
                {
                    lib.MyHost.Actions.CastSpell(2074, lib.MyHost.Actions.CurrentSelection);
                }
            }
            else if (CoreManager.Current.Actions.CombatMode == CombatState.Melee || CoreManager.Current.Actions.CombatMode == CombatState.Missile)
            {
                Utility.InvokeTextF("Requesting IMPERIL on " + lib.MyHost.Actions.CurrentSelection);
            }
        }

        public void Bludge_Hit(object sender, EventArgs e)
        {
            WorldObject worldObject = lib.MyCore.WorldFilter[lib.MyHost.Actions.CurrentSelection];
            if (CoreManager.Current.Actions.CombatMode == CombatState.Magic)
            {
                if (lib.MyCore.CharacterFilter.IsSpellKnown(4477))
                {
                    lib.MyHost.Actions.CastSpell(4477, lib.MyHost.Actions.CurrentSelection);
                }
                else
                {
                    lib.MyHost.Actions.CastSpell(2166, lib.MyHost.Actions.CurrentSelection);
                }
            }
            else if (CoreManager.Current.Actions.CombatMode == CombatState.Melee || CoreManager.Current.Actions.CombatMode == CombatState.Missile)
            {
                Utility.InvokeTextF("Requesting BLUDGE VULN on " + lib.MyHost.Actions.CurrentSelection);
            }
        }

        public void Slash_Hit(object sender, EventArgs e)
        {
            WorldObject worldObject = lib.MyCore.WorldFilter[lib.MyHost.Actions.CurrentSelection];
            if (CoreManager.Current.Actions.CombatMode == CombatState.Magic)
            {
                if (lib.MyCore.CharacterFilter.IsSpellKnown(4475))
                {
                    lib.MyHost.Actions.CastSpell(4475, lib.MyHost.Actions.CurrentSelection);
                }
                else
                {
                    lib.MyHost.Actions.CastSpell(2164, lib.MyHost.Actions.CurrentSelection);
                }
            }
            else if (CoreManager.Current.Actions.CombatMode == CombatState.Melee || CoreManager.Current.Actions.CombatMode == CombatState.Missile)
            {
                Utility.InvokeTextF("Requesting SLASH VULN on " + lib.MyHost.Actions.CurrentSelection);
            }
        }

        public void Pierce_Hit(object sender, EventArgs e)
        {
            WorldObject worldObject = lib.MyCore.WorldFilter[lib.MyHost.Actions.CurrentSelection];
            if (CoreManager.Current.Actions.CombatMode == CombatState.Magic)
            {
                if (lib.MyCore.CharacterFilter.IsSpellKnown(4485))
                {
                    lib.MyHost.Actions.CastSpell(4485, lib.MyHost.Actions.CurrentSelection);
                }
                else
                {
                    lib.MyHost.Actions.CastSpell(2174, lib.MyHost.Actions.CurrentSelection);
                }
            }
            else if (CoreManager.Current.Actions.CombatMode == CombatState.Melee || CoreManager.Current.Actions.CombatMode == CombatState.Missile)
            {
                Utility.InvokeTextF("Requesting PIERCE VULN on " + lib.MyHost.Actions.CurrentSelection);
            }
        }

        public void Light_Hit(object sender, EventArgs e)
        {
            WorldObject worldObject = lib.MyCore.WorldFilter[lib.MyHost.Actions.CurrentSelection];
            if (CoreManager.Current.Actions.CombatMode == CombatState.Magic)
            {
                if (lib.MyCore.CharacterFilter.IsSpellKnown(4483))
                {
                    lib.MyHost.Actions.CastSpell(4483, lib.MyHost.Actions.CurrentSelection);
                }
                else
                {
                    lib.MyHost.Actions.CastSpell(2172, lib.MyHost.Actions.CurrentSelection);
                }
            }
            else if (CoreManager.Current.Actions.CombatMode == CombatState.Melee || CoreManager.Current.Actions.CombatMode == CombatState.Missile)
            {
                Utility.InvokeTextF("Requesting LIGHT VULN on " + lib.MyHost.Actions.CurrentSelection);
            }
        }

        public void Acid_Hit(object sender, EventArgs e)
        {
            WorldObject worldObject = lib.MyCore.WorldFilter[lib.MyHost.Actions.CurrentSelection];
            if (CoreManager.Current.Actions.CombatMode == CombatState.Magic)
            {
                if (lib.MyCore.CharacterFilter.IsSpellKnown(4473))
                {
                    lib.MyHost.Actions.CastSpell(4473, lib.MyHost.Actions.CurrentSelection);
                }
                else
                {
                    lib.MyHost.Actions.CastSpell(2162, lib.MyHost.Actions.CurrentSelection);
                }
            }
            else if (CoreManager.Current.Actions.CombatMode == CombatState.Melee || CoreManager.Current.Actions.CombatMode == CombatState.Missile)
            {
                Utility.InvokeTextF("Requesting ACID VULN on " + lib.MyHost.Actions.CurrentSelection);
            }
        }

        public void Fire_Hit(object sender, EventArgs e)
        {
            WorldObject worldObject = lib.MyCore.WorldFilter[lib.MyHost.Actions.CurrentSelection];
            if (CoreManager.Current.Actions.CombatMode == CombatState.Magic)
            {
                if (lib.MyCore.CharacterFilter.IsSpellKnown(4481))
                {
                    lib.MyHost.Actions.CastSpell(4481, lib.MyHost.Actions.CurrentSelection);
                }
                else
                {
                    lib.MyHost.Actions.CastSpell(2170, lib.MyHost.Actions.CurrentSelection);
                }
            }
            else if (CoreManager.Current.Actions.CombatMode == CombatState.Melee || CoreManager.Current.Actions.CombatMode == CombatState.Missile)
            {
                Utility.InvokeTextF("Requesting FIRE VULN on " + worldObject.Name);
            }
        }

        public void Cold_Hit(object sender, EventArgs e)
        {
            WorldObject worldObject = lib.MyCore.WorldFilter[lib.MyHost.Actions.CurrentSelection];
            if (CoreManager.Current.Actions.CombatMode == CombatState.Magic)
            {
                if (lib.MyCore.CharacterFilter.IsSpellKnown(4479))
                {
                    lib.MyHost.Actions.CastSpell(4479, lib.MyHost.Actions.CurrentSelection);
                }
                else
                {
                    lib.MyHost.Actions.CastSpell(2168, lib.MyHost.Actions.CurrentSelection);
                }
            }
            else if (CoreManager.Current.Actions.CombatMode == CombatState.Melee || CoreManager.Current.Actions.CombatMode == CombatState.Missile)
            {
                Utility.InvokeTextF("Requesting COLD VULN on " + lib.MyHost.Actions.CurrentSelection);
            }
        }

        public void Strength_Hit(object sender, EventArgs e)
        {
            WorldObject worldObject = lib.MyCore.WorldFilter[lib.MyHost.Actions.CurrentSelection];
            if (CoreManager.Current.Actions.CombatMode == CombatState.Magic)
            {
                if (lib.MyCore.CharacterFilter.IsSpellKnown(4326))
                {
                    lib.MyHost.Actions.CastSpell(4326, lib.MyHost.Actions.CurrentSelection);
                }
                else
                {
                    lib.MyHost.Actions.CastSpell(2088, lib.MyHost.Actions.CurrentSelection);
                }
            }
            else if (CoreManager.Current.Actions.CombatMode == CombatState.Melee || CoreManager.Current.Actions.CombatMode == CombatState.Missile)
            {
                Utility.InvokeTextF("Requesting STRENGTH DEBUFF on " + lib.MyHost.Actions.CurrentSelection);
            }
        }

        public void Endurance_Hit(object sender, EventArgs e)
        {
            WorldObject worldObject = lib.MyCore.WorldFilter[lib.MyHost.Actions.CurrentSelection];
            if (CoreManager.Current.Actions.CombatMode == CombatState.Magic)
            {
                if (lib.MyCore.CharacterFilter.IsSpellKnown(4306))
                {
                    lib.MyHost.Actions.CastSpell(4306, lib.MyHost.Actions.CurrentSelection);
                }
                else
                {
                    lib.MyHost.Actions.CastSpell(2068, lib.MyHost.Actions.CurrentSelection);
                }
            }
            else if (CoreManager.Current.Actions.CombatMode == CombatState.Melee || CoreManager.Current.Actions.CombatMode == CombatState.Missile)
            {
                Utility.InvokeTextF("Requesting ENDURANCE DEBUFF on " + lib.MyHost.Actions.CurrentSelection);
            }
        }

        public void Coordination_Hit(object sender, EventArgs e)
        {
            WorldObject worldObject = lib.MyCore.WorldFilter[lib.MyHost.Actions.CurrentSelection];
            if (CoreManager.Current.Actions.CombatMode == CombatState.Magic)
            {
                if (lib.MyCore.CharacterFilter.IsSpellKnown(4294))
                {
                    lib.MyHost.Actions.CastSpell(4294, lib.MyHost.Actions.CurrentSelection);
                }
                else
                {
                    lib.MyCore.Actions.CastSpell(2056, lib.MyHost.Actions.CurrentSelection);
                }
            }
            else if (CoreManager.Current.Actions.CombatMode == CombatState.Melee || CoreManager.Current.Actions.CombatMode == CombatState.Missile)
            {
                Utility.InvokeTextF("Requesting COORDINATION DEBUFF on " + lib.MyHost.Actions.CurrentSelection);
            }
        }

        public void Quickness_Hit(object sender, EventArgs e)
        {
            WorldObject worldObject = lib.MyCore.WorldFilter[lib.MyHost.Actions.CurrentSelection];
            if (CoreManager.Current.Actions.CombatMode == CombatState.Magic)
            {
                if (lib.MyCore.CharacterFilter.IsSpellKnown(4322))
                {
                    lib.MyHost.Actions.CastSpell(4322, lib.MyHost.Actions.CurrentSelection);
                }
                else
                {
                    lib.MyHost.Actions.CastSpell(2084, lib.MyHost.Actions.CurrentSelection);
                }
            }
            else if (CoreManager.Current.Actions.CombatMode == CombatState.Melee || CoreManager.Current.Actions.CombatMode == CombatState.Missile)
            {
                Utility.InvokeTextF("Requesting QUICKNESS DEBUFF on " + lib.MyHost.Actions.CurrentSelection);
            }
        }

        public void Focus_Hit(object sender, EventArgs e)
        {
            WorldObject worldObject = lib.MyCore.WorldFilter[lib.MyHost.Actions.CurrentSelection];
            if (CoreManager.Current.Actions.CombatMode == CombatState.Magic)
            {
                if (lib.MyCore.CharacterFilter.IsSpellKnown(4293))
                {
                    lib.MyHost.Actions.CastSpell(4293, lib.MyHost.Actions.CurrentSelection);
                }
                else
                {
                    lib.MyHost.Actions.CastSpell(2054, lib.MyHost.Actions.CurrentSelection);
                }
            }
            else if (CoreManager.Current.Actions.CombatMode == CombatState.Melee || CoreManager.Current.Actions.CombatMode == CombatState.Missile)
            {
                Utility.InvokeTextF("Requesting FOCUS DEBUFF on " + lib.MyHost.Actions.CurrentSelection);
            }
        }

        public void Willpower_Hit(object sender, EventArgs e)
        {
            WorldObject worldObject = lib.MyCore.WorldFilter[lib.MyHost.Actions.CurrentSelection];
            if (CoreManager.Current.Actions.CombatMode == CombatState.Magic)
            {
                if (lib.MyCore.CharacterFilter.IsSpellKnown(4302))
                {
                    lib.MyHost.Actions.CastSpell(4302, lib.MyHost.Actions.CurrentSelection);
                }
                else
                {
                    lib.MyHost.Actions.CastSpell(2064, lib.MyHost.Actions.CurrentSelection);
                }
            }
            else if (CoreManager.Current.Actions.CombatMode == CombatState.Melee || CoreManager.Current.Actions.CombatMode == CombatState.Missile)
            {
                Utility.InvokeTextF("Requesting WILLPOWER DEBUFF on " + lib.MyHost.Actions.CurrentSelection);
            }
        }

        public void Run_Hit(object sender, EventArgs e)
        {
            WorldObject worldObject = lib.MyCore.WorldFilter[lib.MyHost.Actions.CurrentSelection];
            if (CoreManager.Current.Actions.CombatMode == CombatState.Magic)
            {
                if (lib.MyCore.CharacterFilter.IsSpellKnown(4573))
                {
                    lib.MyHost.Actions.CastSpell(4573, lib.MyHost.Actions.CurrentSelection);
                }
                else
                {
                    lib.MyHost.Actions.CastSpell(2258, lib.MyHost.Actions.CurrentSelection);
                }
            }
            else if (CoreManager.Current.Actions.CombatMode == CombatState.Melee || CoreManager.Current.Actions.CombatMode == CombatState.Missile)
            {
                Utility.InvokeTextF("Requesting RUN DEBUFF on " + lib.MyHost.Actions.CurrentSelection);
            }
        }

        public void Creature_Hit(object sender, EventArgs e)
        {
            WorldObject worldObject = lib.MyCore.WorldFilter[lib.MyHost.Actions.CurrentSelection];
            if (CoreManager.Current.Actions.CombatMode == CombatState.Magic)
            {
                if (lib.MyCore.CharacterFilter.IsSpellKnown(4527))
                {
                    lib.MyHost.Actions.CastSpell(4527, lib.MyHost.Actions.CurrentSelection);
                }
                else
                {
                    lib.MyHost.Actions.CastSpell(2212, lib.MyHost.Actions.CurrentSelection);
                }
            }
            else if (CoreManager.Current.Actions.CombatMode == CombatState.Melee || CoreManager.Current.Actions.CombatMode == CombatState.Missile)
            {
                Utility.InvokeTextF("Requesting CREATURE MAGIC DEBUFF on " + lib.MyHost.Actions.CurrentSelection);
            }
        }

        public void Life_Hit(object sender, EventArgs e)
        {
            WorldObject worldObject = lib.MyCore.WorldFilter[lib.MyHost.Actions.CurrentSelection];
            if (CoreManager.Current.Actions.CombatMode == CombatState.Magic)
            {
                if (lib.MyCore.CharacterFilter.IsSpellKnown(4579))
                {
                    lib.MyHost.Actions.CastSpell(4579, lib.MyHost.Actions.CurrentSelection);
                }
                else
                {
                    lib.MyHost.Actions.CastSpell(2264, lib.MyHost.Actions.CurrentSelection);
                }
            }
            else if (CoreManager.Current.Actions.CombatMode == CombatState.Melee || CoreManager.Current.Actions.CombatMode == CombatState.Missile)
            {
                Utility.InvokeTextF("Requesting LIFE MAGIC DEBUFF on " + lib.MyHost.Actions.CurrentSelection);
            }
        }

        public void War_Hit(object sender, EventArgs e)
        {
            WorldObject worldObject = lib.MyCore.WorldFilter[lib.MyHost.Actions.CurrentSelection];
            if (CoreManager.Current.Actions.CombatMode == CombatState.Magic)
            {
                if (lib.MyCore.CharacterFilter.IsSpellKnown(4635))
                {
                    lib.MyHost.Actions.CastSpell(4635, lib.MyHost.Actions.CurrentSelection);
                }
                else
                {
                    lib.MyHost.Actions.CastSpell(2320, lib.MyHost.Actions.CurrentSelection);
                }
            }
            else if (CoreManager.Current.Actions.CombatMode == CombatState.Melee || CoreManager.Current.Actions.CombatMode == CombatState.Missile)
            {
                Utility.InvokeTextF("Requesting WAR MAGIC DEBUFF on " + lib.MyHost.Actions.CurrentSelection);
            }
        }

        public void MagicD_Hit(object sender, EventArgs e)
        {
            WorldObject worldObject = lib.MyCore.WorldFilter[lib.MyHost.Actions.CurrentSelection];
            if (CoreManager.Current.Actions.CombatMode == CombatState.Magic)
            {
                if (lib.MyCore.CharacterFilter.IsSpellKnown(4597))
                {
                    lib.MyHost.Actions.CastSpell(4597, lib.MyHost.Actions.CurrentSelection);
                }
                else
                {
                    lib.MyHost.Actions.CastSpell(2282, lib.MyHost.Actions.CurrentSelection);
                }
            }
            else if (CoreManager.Current.Actions.CombatMode == CombatState.Melee || CoreManager.Current.Actions.CombatMode == CombatState.Missile)
            {
                Utility.InvokeTextF("Requesting MAGIC DEFENSE DEBUFF on " + lib.MyHost.Actions.CurrentSelection);
            }
        }

        public void HeavyW_Hit(object sender, EventArgs e)
        {
            WorldObject worldObject = lib.MyCore.WorldFilter[lib.MyHost.Actions.CurrentSelection];
            if (CoreManager.Current.Actions.CombatMode == CombatState.Magic)
            {
                if (lib.MyCore.CharacterFilter.IsSpellKnown(4621))
                {
                    lib.MyHost.Actions.CastSpell(4621, lib.MyHost.Actions.CurrentSelection);
                }
                else
                {
                    lib.MyHost.Actions.CastSpell(2306, lib.MyHost.Actions.CurrentSelection);
                }
            }
            else if (CoreManager.Current.Actions.CombatMode == CombatState.Melee || CoreManager.Current.Actions.CombatMode == CombatState.Missile)
            {
                Utility.InvokeTextF("Requesting HEAVY WEAPONS DEBUFF on " + lib.MyHost.Actions.CurrentSelection);
            }
        }

        public void MelD_Hit(object sender, EventArgs e)
        {
            WorldObject worldObject = lib.MyCore.WorldFilter[lib.MyHost.Actions.CurrentSelection];
            if (CoreManager.Current.Actions.CombatMode == CombatState.Magic)
            {
                if (lib.MyCore.CharacterFilter.IsSpellKnown(4633))
                {
                    lib.MyHost.Actions.CastSpell(4633, lib.MyHost.Actions.CurrentSelection);
                }
                else
                {
                    lib.MyHost.Actions.CastSpell(2318, lib.MyHost.Actions.CurrentSelection);
                }
            }
            else if (CoreManager.Current.Actions.CombatMode == CombatState.Melee || CoreManager.Current.Actions.CombatMode == CombatState.Missile)
            {
                Utility.InvokeTextF("Requesting MELEE DEFENSE DEBUFF on " + lib.MyHost.Actions.CurrentSelection);
            }
        }

        public void MissW_Hit(object sender, EventArgs e)
        {
            WorldObject worldObject = lib.MyCore.WorldFilter[lib.MyHost.Actions.CurrentSelection];
            if (CoreManager.Current.Actions.CombatMode == CombatState.Magic)
            {
                if (lib.MyCore.CharacterFilter.IsSpellKnown(4625))
                {
                    lib.MyHost.Actions.CastSpell(4625, lib.MyHost.Actions.CurrentSelection);
                }
                else
                {
                    lib.MyHost.Actions.CastSpell(2204, lib.MyHost.Actions.CurrentSelection);
                }
            }
            else if (CoreManager.Current.Actions.CombatMode == CombatState.Melee || CoreManager.Current.Actions.CombatMode == CombatState.Missile)
            {
                Utility.InvokeTextF("Requesting MISSILE WEAPON DEBUFF on " + lib.MyHost.Actions.CurrentSelection);
            }
        }

        public void MissD_Hit(object sender, EventArgs e)
        {
            WorldObject worldObject = lib.MyCore.WorldFilter[lib.MyHost.Actions.CurrentSelection];
            if (CoreManager.Current.Actions.CombatMode == CombatState.Magic)
            {
                if (lib.MyCore.CharacterFilter.IsSpellKnown(4543))
                {
                    lib.MyHost.Actions.CastSpell(4543, lib.MyHost.Actions.CurrentSelection);
                }
                else
                {
                    lib.MyHost.Actions.CastSpell(2228, lib.MyHost.Actions.CurrentSelection);
                }
            }
            else if (CoreManager.Current.Actions.CombatMode == CombatState.Melee || CoreManager.Current.Actions.CombatMode == CombatState.Missile)
            {
                Utility.InvokeTextF("Requesting MISSILE DEFENSE DEBUFF on " + lib.MyHost.Actions.CurrentSelection);
            }
        }

        public void IgnoreVP_Hit(object sender, EventArgs e)
        {
            try
            {
                if (lib.DFTEXT != null)
                {
                    lib.DFTEXT.Visible = false;
                    lib.DFTEXT.Dispose();
                    lib.DFTEXT = null;
                }

                if (lib.vpcounter != 0)
                {
                    Vitae.Dispose();
                    Utility.AddChatText("10% VP Warning ignored until the next death.", 6);
                    lib.vpcounter = 0;
                    Sounds.DeadVP.Stop();
                }
                else
                {
                    Utility.AddChatText("Not enough VP or already disabled.", 6);
                    lib.vpcounter = 0;
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void Fixbusy_Hit(object sender, EventArgs e)
        {
            try
            {
                Utility.DispatchChatToBoxWithPluginIntercept("/fixbusy");
                Utility.AddChatText("Running /fixbusy.", 6);
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void Stam_Hit(object sender, EventArgs e)
        {
            WorldObject worldObject = lib.MyCore.WorldFilter[lib.MyHost.Actions.CurrentSelection];
            if (CoreManager.Current.Actions.CombatMode == CombatState.Magic)
            {
                if (lib.MyCore.CharacterFilter.IsSpellKnown(2062))
                {
                    lib.MyHost.Actions.CastSpell(2062, lib.MyHost.Actions.CurrentSelection);
                }
                else
                {
                    Utility.AddChatText("Stam! Spell not known. (Anemia)", 6);
                }
            }
            else if (CoreManager.Current.Actions.CombatMode == CombatState.Melee || CoreManager.Current.Actions.CombatMode == CombatState.Missile)
            {
                Utility.InvokeTextF("Requesting STAM DRAIN on " + lib.MyHost.Actions.CurrentSelection);
            }
        }

        public void Harm_Hit(object sender, EventArgs e)
        {
            WorldObject worldObject = lib.MyCore.WorldFilter[lib.MyHost.Actions.CurrentSelection];
            if (CoreManager.Current.Actions.CombatMode == CombatState.Magic)
            {
                if (lib.MyCore.CharacterFilter.IsSpellKnown(4308))
                {
                    lib.MyHost.Actions.CastSpell(4308, lib.MyHost.Actions.CurrentSelection);
                }
                else
                {
                    lib.MyHost.Actions.CastSpell(2070, lib.MyHost.Actions.CurrentSelection);
                }
            }
            else if (CoreManager.Current.Actions.CombatMode == CombatState.Melee || CoreManager.Current.Actions.CombatMode == CombatState.Missile)
            {
                Utility.InvokeTextF("Requesting HARM on " + lib.MyHost.Actions.CurrentSelection);
            }
        }

        public void StamRegen_Hit(object sender, EventArgs e)
        {
            WorldObject worldObject = lib.MyCore.WorldFilter[lib.MyHost.Actions.CurrentSelection];
            if (CoreManager.Current.Actions.CombatMode == CombatState.Magic)
            {
                if (lib.MyCore.CharacterFilter.IsSpellKnown(4487))
                {
                    lib.MyHost.Actions.CastSpell(4487, lib.MyHost.Actions.CurrentSelection);
                }
                else
                {
                    lib.MyHost.Actions.CastSpell(2176, lib.MyHost.Actions.CurrentSelection);
                }
            }
            else if (CoreManager.Current.Actions.CombatMode == CombatState.Melee || CoreManager.Current.Actions.CombatMode == CombatState.Missile)
            {
                Utility.InvokeTextF("Requesting STAM REGEN DEBUFF on " + lib.MyHost.Actions.CurrentSelection);
            }
        }

        public void Loc_Hit(object sender, EventArgs e)
        {
            try
            {

                WorldObject me = CoreManager.Current.WorldFilter[lib.MyCore.CharacterFilter.Id];
                CoordsObject coordsObject = me.Coordinates();

                string text;
                if (coordsObject.NorthSouth >= 0.0)
                {
                    text = string.Format("{0:N1}", coordsObject.NorthSouth) + "N, ";
                }
                else
                {
                    text = string.Format("{0:N1}", coordsObject.NorthSouth * -1.0) + "S, ";
                }
                if (coordsObject.EastWest >= 0.0)
                {
                    text = text + string.Format("{0:N1}", coordsObject.EastWest) + "E";
                }
                else
                {
                    text = text + string.Format("{0:N1}", coordsObject.EastWest * -1.0) + "W";
                }

                double heading = me.Values(DoubleValueKey.Heading);

                string direction = "nowhere";
                if (heading <= 110 && heading >= 70)
                {
                    direction = "East";
                }
                else if ((heading <= 360 && heading >= 340) || (heading >= 0 && heading <= 20))
                {
                    direction = "North";
                }
                else if (heading <= 200 && heading >= 160)
                {
                    direction = "South";
                }
                else if (heading <= 290 && heading >= 250)
                {
                    direction = "West";
                }
                else if (heading < 70 && heading > 20)
                {
                    direction = "North-East";
                }
                else if (heading < 160 && heading > 110)
                {
                    direction = "South-East";
                }
                else if (heading < 250 && heading > 200)
                {
                    direction = "South-West";
                }
                else if (heading < 340 && heading > 290)
                {
                    direction = "North-West";
                }

                string key = me.Values(LongValueKey.Landblock).ToString("X8").Substring(0, 4);
                List<string> Landblock = (from f in lib.LocKey.Split(new char[] { ',' })
                                          select f.Trim()).ToList<string>();

                if (lib.LocKey.Contains(key))
                {
                    foreach (string el in Landblock)
                    {
                        if (el.Contains(key))
                        {
                            string elloc = el.Split(new string[] { "=" }, StringSplitOptions.None)[1];
                            Utility.InvokeTextA("I NEED HELP AT " + text + " , heading " + direction + " (" + elloc + ")");
                        }
                    }
                }
                else
                {
                    Utility.InvokeTextA("I NEED HELP AT " + text + " , heading " + direction + " (" + key + " : Unset)");
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void Mode_Change(object sender, EventArgs e)
        {
            try
            {
                if (MainView.Mode.Current == 0)
                {
                    lib.Mode = 0;
                    Utility.AddWindowText(lib.MyServer + " : " + lib.MyName + " : " + lib.authtype + " : Current mode: SLAVE");
                    Utility.AddChatText("Mode switched: SLAVE.", 6);

                    if (lib.vtank == true && lib.status > 0)
                    {
                        Utility.DispatchChatToBoxWithPluginIntercept("/vt stop");
                    }
                }

                else if (MainView.Mode.Current == 1)
                {
                    lib.Mode = 1;
                    Utility.AddWindowText(lib.MyServer + " : " + lib.MyName + " : " + lib.authtype + " : Current mode: MASTER");
                    Utility.AddChatText("Mode switched: MASTER.", 6);

                    if (lib.vtank == true && lib.status > 0)
                    {
                        Utility.DispatchChatToBoxWithPluginIntercept("/vt stop");
                    }
                }

                else if (MainView.Mode.Current == 2)
                {
                    lib.Mode = 2;
                    Utility.AddWindowText(lib.MyServer + " : " + lib.MyName + " : " + lib.authtype + " : Current mode: SOLO");
                    Utility.AddChatText("Mode switched: SOLO.", 6);

                    if (lib.vtank == true && lib.status > 0)
                    {
                        Utility.DispatchChatToBoxWithPluginIntercept("/vt stop");
                    }
                }

                else if (MainView.Mode.Current == 3)
                {
                    lib.Mode = 3;
                    Utility.AddWindowText(lib.MyServer + " : " + lib.MyName + " : " + lib.authtype + " : Current mode: MACRO - Relogger is set to " + lib.Timer + " seconds.");
                    Utility.AddChatText("Mode switched: MACRO.", 6);

                    if (lib.vtank == true && lib.status > 0)
                    {
                        Utility.DispatchChatToBoxWithPluginIntercept("/vt start");
                    }
                    lib.Timer = Convert.ToInt32(MainView.Timer.Text);
                    lib.relogcounter = 0;
                }
                Report.LogEvent("ModeSwitch");
                SaveUI();
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void Element_Change(object sender, EventArgs e)
        {
            try
            {
                if (MainView.Element.Current == 0)
                {
                    if (lib.MyCore.CharacterFilter.IsSpellKnown(4428))
                    { lib.warspell = 4428; }
                    else { lib.warspell = 2766; }

                    if (lib.MyCore.CharacterFilter.IsSpellKnown(4308))
                    { lib.streak = 4308; }
                    else { lib.streak = 2070; }
                    lib.vuln = 0;
                }
                if (MainView.Element.Current == 1)
                {
                    if (lib.MyCore.CharacterFilter.IsSpellKnown(4424))
                    { lib.warspell = 4424; }
                    else { lib.warspell = 2724; }
                    
                    if (lib.MyCore.CharacterFilter.IsSpellKnown(4444))
                    { lib.streak = 4444; }
                    else { lib.streak = 2133; }

                    if (lib.MyCore.CharacterFilter.IsSpellKnown(4485))
                    { lib.vuln = 4485; }
                    else { lib.vuln = 2174; }
                }
                if (MainView.Element.Current == 2)
                {
                    lib.warspell = 4427;
                    lib.streak = 4456;
                    lib.vuln = 4477;
                }
                if (MainView.Element.Current == 3)
                {
                    lib.warspell = 4422;
                    lib.streak = 4458;
                    lib.vuln = 4475;
                }
                if (MainView.Element.Current == 4)
                {
                    lib.warspell = 4426;
                    lib.streak = 4452;
                    lib.vuln = 4483;
                }
                if (MainView.Element.Current == 5)
                {
                    lib.warspell = 4421;
                    lib.streak = 4432;
                    lib.vuln = 4473;
                }
                if (MainView.Element.Current == 6)
                {
                    lib.warspell = 4423;
                    lib.streak = 4440;
                    lib.vuln = 4481;
                }
                if (MainView.Element.Current == 7)
                {
                    lib.warspell = 4425;
                    lib.streak = 4448;
                    lib.vuln = 4479;
                }
                SaveUI();
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void AlertPA_Change(object sender, EventArgs e)
        {
            try
            {
                lib.UseAlertPA = MainView.AlertPA.Checked;
                bool useAlertPA = lib.UseAlertPA;
                SaveUI();
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void AlertPKT_Change(object sender, EventArgs e)
        {
            try
            {
                lib.UseAlertPKT = MainView.AlertPKT.Checked;
                bool useAlertPKT = lib.UseAlertPKT;
                SaveUI();
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void AlertDT_Change(object sender, EventArgs e)
        {
            try
            {
                lib.UseAlertDT = MainView.AlertDT.Checked;
                bool useAlertDT = lib.UseAlertDT;
                SaveUI();
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void AlertLS_Change(object sender, EventArgs e)
        {
            try
            {
                lib.UseAlertLS = MainView.AlertLS.Checked;
                bool useAlertLS = lib.UseAlertLS;
                SaveUI();
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void LogDie_Change(object sender, EventArgs e)
        {
            try
            {
                lib.UseLogDie = MainView.UseLogDie.Checked;
                bool useLogDie = lib.UseLogDie;
                SaveUI();
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void AlertOdds_Change(object sender, EventArgs e)
        {
            try
            {
                lib.UseAlertOdds = MainView.AlertOdds.Checked;
                bool useAlertOdds = lib.UseAlertOdds;
                SaveUI();
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void AlertBc_Change(object sender, EventArgs e)
        {
            try
            {
                lib.UseAlertBc = MainView.AlertBc.Checked;
                bool useAlerBc = lib.UseAlertBc;
                SaveUI();
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void MacroLogic_Change(object sender, EventArgs e)
        {
            try
            {
                lib.UseMacroLogic = MainView.UseMacroLogic.Checked;
                bool useMacroLogic = lib.UseMacroLogic;
                SaveUI();
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void AlertPF_Change(object sender, EventArgs e)
        {
            try
            {
                lib.UseAlertPF = MainView.AlertPF.Checked;
                bool useAlertPF = lib.UseAlertPF;
                SaveUI();
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void AlertLogP_Change(object sender, EventArgs e)
        {
            try
            {
                lib.UseAlertLogP = MainView.AlertLogP.Checked;
                bool useAlertLogP = lib.UseAlertLogP;
                SaveUI();
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void AlertDA_Change(object sender, EventArgs e)
        {
            try
            {
                lib.UseAlertDA = MainView.AlertDA.Checked;
                bool useAlertDA = lib.UseAlertDA;
                SaveUI();
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void AlertSPK_Change(object sender, EventArgs e)
        {
            try
            {
                lib.UseAlertSPK = MainView.AlertSPK.Checked;
                bool useAlertSPK = lib.UseAlertSPK;
                SaveUI();
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void AlertSS_Change(object sender, EventArgs e)
        {
            try
            {
                lib.UseAlertSS = MainView.AlertSS.Checked;
                bool useAlertSS = lib.UseAlertSS;
                SaveUI();
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        MainView View;
    }
}
