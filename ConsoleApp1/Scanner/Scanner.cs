using Decal.Adapter;
using Decal.Adapter.Wrappers;
using Defiance.BaseHandle;
using Defiance.CollectionHandle;
using Defiance.Utils;
using Defiance.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using VirindiViewService.Controls;

namespace Defiance.Scanning
{
    public class Scanner
    {
        public void ScannerInit()
        {
            try
            {
                if (lib.ScannerInstance < 1)
                {
                    lib.DistanceCheck = new List<int>();
                    lib.LogList = new List<int>();

                    lib.MyCore.WorldFilter.CreateObject += WorldFilter_CreateObject;
                    lib.MyCore.WorldFilter.ReleaseObject += WorldFilter_ReleaseObject;
                    lib.MyCore.ItemDestroyed += Core_ItemDestroyed;
                    lib.MyCore.ItemSelected += Core_ItemSelected;
                    lib.MyCore.CharacterFilter.LoginComplete += Core_SetMon;
                    lib.MyCore.EchoFilter.ServerDispatch += AttackDetection.ServerDispatch_EchoFilter;

                    if (PluginCore.ShrtTmr != null)
                    {
                        PluginCore.ShrtTmr.Tick += Refresher_Tick;
                    }

                    if (PluginCore.LngTmr != null)
                    {
                        PluginCore.LngTmr.Tick += Refresher2_Tick;
                    }

                    Rescan();
                    lib.ScannerInstance++;
                }             
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void Dispose()
        {
            PluginCore.ShrtTmr.Tick -= Refresher_Tick;
            PluginCore.LngTmr.Tick -= Refresher2_Tick;

            lib.MyCore.CharacterFilter.LoginComplete -= Core_SetMon;
            lib.MyCore.WorldFilter.CreateObject -= WorldFilter_CreateObject;
            lib.MyCore.WorldFilter.ReleaseObject -= WorldFilter_ReleaseObject;
            lib.MyCore.ItemDestroyed -= Core_ItemDestroyed;
            lib.MyCore.ItemSelected -= Core_ItemSelected;
            lib.MyCore.EchoFilter.ServerDispatch -= AttackDetection.ServerDispatch_EchoFilter;

            lib.Friends = null;
            lib.Enemies = null;

            lib.DistanceCheck = null;
            lib.LogList = null;
            lib.ScannerInstance--; 
        }

        protected void Refresher_Tick(object sender, EventArgs e)
        {
            try
            {
                if (lib.gameStatus != 4)
                {
                    Refresh();
                    if ((MainView.PlayerList.RowCount > 0 || MainView.GuildList.RowCount > 1) && lib.OnDowntime2.Count > 0)
                    {
                        lib.Flicker = !lib.Flicker;
                    }
                }
            }
            catch
            {}
        }

        protected void Refresher2_Tick(object sender, EventArgs e)
        {
            try
            {
                if (lib.gameStatus != 4)
                {
                    Refresh2();
                }
            }
            catch
            {}
        }

        protected void Core_SetMon(object sender, EventArgs e)
        {
            int tempmon;
            try
            {
                tempmon = lib.MyCore.CharacterFilter.Monarch.Id;
            }
            catch
            {
                tempmon = lib.MyCore.CharacterFilter.Id;
            }
            
            if (lib.moncheck != tempmon)
            {
                lib.moncheck = tempmon;
                ViewControl.SaveUI();
                Utility.AddChatText("Setting Monarch ID to " + lib.moncheck, 6);
            }  
        }

        protected void WorldFilter_CreateObject(object sender, CreateObjectEventArgs e)
        {
            try
            {
                WorldObject @new = e.New;
                if (@new.ObjectClass == ObjectClass.Corpse)
                {
                    AddWorldObject(@new, MainView.CorpseList);
                }
                if (@new.ObjectClass == ObjectClass.Portal)
                {
                    AddWorldObject(@new, MainView.PortalList);
                }
                if (@new.ObjectClass == ObjectClass.Player && @new.Id == lib.MyCore.CharacterFilter.Id)
                {
                    AddWorldObject(@new, MainView.GuildList);
                }
                if (@new.ObjectClass == ObjectClass.Player)
                {
                    if (@new.Id == lib.MyCore.CharacterFilter.Id)
                    {
                        AddWorldObject(@new, MainView.GuildList);
                        if (!lib.DistanceCheck.Contains(@new.Id))
                        {
                            lib.DistanceCheck.Add(@new.Id);
                        }
                    }
                    if (IsFriend(@new))
                    {
                        AddWorldObject(@new, MainView.GuildList);
                        if (!lib.DistanceCheck.Contains(@new.Id))
                        {
                            lib.DistanceCheck.Add(@new.Id);
                        }
                    }
                    if (IsEnemy(@new))
                    {
                        AddWorldObject(@new, MainView.PlayerList);
                        if (!lib.LogList.Contains(@new.Id))
                        {
                            lib.LogList.Add(@new.Id);
                        }
                        if (!lib.DistanceCheck.Contains(@new.Id))
                        {
                            lib.DistanceCheck.Add(@new.Id);
                        }
                    }
                    if (@new.Values(LongValueKey.Monarch) == lib.moncheck && !IsEnemy(@new) && @new.Id != lib.MyCore.CharacterFilter.Id)
                    {
                        AddWorldObject(@new, MainView.GuildList);
                        if (!lib.DistanceCheck.Contains(@new.Id))
                        {
                            lib.DistanceCheck.Add(@new.Id);
                        }
                    }
                    if (@new.Values(LongValueKey.Monarch) != lib.moncheck && !IsFriend(@new) && @new.Id != lib.MyCore.CharacterFilter.Id)
                    {
                        AddWorldObject(@new, MainView.PlayerList);
                        if (!lib.LogList.Contains(@new.Id))
                        {
                            lib.LogList.Add(@new.Id);
                        }
                        if (!lib.DistanceCheck.Contains(@new.Id))
                        {
                            lib.DistanceCheck.Add(@new.Id);
                        }
                    }
                    if (MainView.PlayerList.RowCount - MainView.GuildList.RowCount >= 3)
                    {
                        Utility.AddChatText("WARNING! Odds Overwhelming: " + MainView.PlayerList.RowCount + " enemies!", 6);
                        if (lib.UseAlertOdds == true)
                        {
                            Sounds.Odds.Play();
                        }
                    }
                }            
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        private static bool IsFriend(WorldObject obj)
        {
            bool flag = false;
            if (obj.Container != 0)
            {
                return false;
            }
            try
            {
                foreach (Friend friend in lib.Friends)
                {
                    if (!string.IsNullOrEmpty(friend.Name))
                    {
                        flag = obj.Name.ToLower().Contains(friend.Name.ToLower());
                    }
                    if (flag)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
            return flag;
        }

        private static bool IsEnemy(WorldObject obj)
        {
            bool flag = false;
            if (obj.Container != 0)
            {
                return false;
            }
            try
            {
                foreach (Enemy enemy in lib.Enemies)
                {
                    if (!string.IsNullOrEmpty(enemy.Name))
                    {
                        flag = obj.Name.Contains(enemy.Name);
                    }
                    if (flag)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
            return flag;
        }

        protected void WorldFilter_ReleaseObject(object sender, ReleaseObjectEventArgs e)
        {
            try
            {
                if (lib.DistanceCheck.Contains(e.Released.Id))
                {
                    lib.DistanceCheck.Remove(e.Released.Id);
                }
                if (lib.LogList.Contains(e.Released.Id))
                {
                    lib.LogList.Remove(e.Released.Id);
                }
                if (e.Released.Id == lib.LastSelected && lib.PointArrow != null)
                {
                    lib.LastSelected = 0;
                    lib.PointArrow.Dispose();
                    lib.PointArrow = null;              
                }            
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        protected void Core_ItemDestroyed(object sender, ItemDestroyedEventArgs e)
        {
            try
            {
                if (lib.DistanceCheck.Contains(e.ItemGuid))
                {
                    lib.DistanceCheck.Remove(e.ItemGuid);
                }
                if (lib.LogList.Contains(e.ItemGuid))
                {
                    lib.LogList.Remove(e.ItemGuid);
                }        
                if (e.ItemGuid == lib.LastSelected && lib.PointArrow != null)
                {  
                    lib.LastSelected = 0;
                    lib.PointArrow.Dispose();
                    lib.PointArrow = null;
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        protected void Core_ItemSelected(object sender, ItemSelectedEventArgs e)
        {

            WorldObject obj = CoreManager.Current.WorldFilter[e.ItemGuid];

            if (obj.ObjectClass == ObjectClass.Player && obj.Id != lib.MyCore.CharacterFilter.Id)
            {
                if (obj.Values(LongValueKey.Monarch) == lib.moncheck && !IsEnemy(obj))
                {
                    if (lib.PointArrow != null)
                    {
                        lib.PointArrow.Dispose();
                        lib.PointArrow = null;
                    }

                    lib.PointArrow = lib.MyCore.D3DService.PointToObject(e.ItemGuid, Color.Green.ToArgb());
                    lib.PointArrow.ScaleX = 1f;
                    lib.PointArrow.ScaleY = 2f;
                    lib.PointArrow.ScaleZ = 1f;
                    lib.LastSelected = e.ItemGuid;
                }
                else if (obj.Values(LongValueKey.Monarch) != lib.moncheck && !IsFriend(obj))
                {
                    if (lib.PointArrow != null)
                    {
                        lib.PointArrow.Dispose();
                        lib.PointArrow = null;
                    }

                    lib.PointArrow = lib.MyCore.D3DService.PointToObject(e.ItemGuid, Color.Red.ToArgb());
                    lib.PointArrow.ScaleX = 1f;
                    lib.PointArrow.ScaleY = 2f;
                    lib.PointArrow.ScaleZ = 1f;
                    lib.LastSelected = e.ItemGuid;
                }
                else if (IsFriend(obj))
                {
                    if (lib.PointArrow != null)
                    {
                        lib.PointArrow.Dispose();
                        lib.PointArrow = null;
                    }

                    lib.PointArrow = lib.MyCore.D3DService.PointToObject(e.ItemGuid, Color.Green.ToArgb());
                    lib.PointArrow.ScaleX = 1f;
                    lib.PointArrow.ScaleY = 2f;
                    lib.PointArrow.ScaleZ = 1f;
                    lib.LastSelected = e.ItemGuid;
                }
                else if (IsEnemy(obj))
                {
                    if (lib.PointArrow != null)
                    {
                        lib.PointArrow.Dispose();
                        lib.PointArrow = null;
                    }

                    lib.PointArrow = lib.MyCore.D3DService.PointToObject(e.ItemGuid, Color.Red.ToArgb());
                    lib.PointArrow.ScaleX = 1f;
                    lib.PointArrow.ScaleY = 2f;
                    lib.PointArrow.ScaleZ = 1f;
                    lib.LastSelected = e.ItemGuid;
                }
            }
            else if (obj.ObjectClass == ObjectClass.Corpse || obj.ObjectClass == ObjectClass.Portal)
            {
                if (lib.PointArrow != null)
                {
                    lib.PointArrow.Dispose();
                    lib.PointArrow = null;
                }

                lib.PointArrow = lib.MyCore.D3DService.PointToObject(e.ItemGuid, Color.Purple.ToArgb());
                lib.PointArrow.ScaleX = 1f;
                lib.PointArrow.ScaleY = 2f;
                lib.PointArrow.ScaleZ = 1f;
                lib.LastSelected = e.ItemGuid;
            }
            else if (lib.PointArrow != null && e.ItemGuid != 0)
            {
                lib.LastSelected = 0;
                lib.PointArrow.Dispose();
                lib.PointArrow = null;
            }
        }

        private void AddWorldObject(WorldObject obj, HudList hl)
        {
            try
            {
                bool flag = false;
                for (int i = 0; i < hl.RowCount; i++)
                {
                    HudList.HudListRowAccessor hudListRowAccessor = hl[i];
                    HudStaticText hudStaticText = (HudStaticText)hudListRowAccessor[5];
                    if (Convert.ToInt32(hudStaticText.Text) == obj.Id)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    UpdateWorldObject(obj, hl);
                }
                else
                {
                    CoordsObject coordsObject = obj.Coordinates();
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
                    double num = CoreManager.Current.WorldFilter.Distance(obj.Id, CoreManager.Current.CharacterFilter.Id) * 240.0;
                    int num2 = 0;
                    HudList.HudListRowAccessor hudListRowAccessor;
                    if (hl.RowCount > 0)
                    {
                        num2 = hl.RowCount;
                        for (int j = 0; j < hl.RowCount; j++)
                        {
                            hudListRowAccessor = hl[j];
                            HudStaticText hudStaticText2 = (HudStaticText)hudListRowAccessor[1];
                            if (hudStaticText2.Text.Trim().CompareTo(obj.Name.Trim()) > 0)
                            {
                                num2 = j;
                                break;
                            }
                        }
                    }
                    hudListRowAccessor = hl.InsertRow(num2);
                    ((HudPictureBox)hudListRowAccessor[0]).Image = obj.Icon;
                    ((HudStaticText)hudListRowAccessor[1]).Text = obj.Name;
                    ((HudStaticText)hudListRowAccessor[2]).Text = text;
                    ((HudStaticText)hudListRowAccessor[3]).Text = string.Format("{0:N1}", num);
                    ((HudPictureBox)hudListRowAccessor[4]).Image = 100667896;
                    ((HudStaticText)hudListRowAccessor[5]).Text = obj.Id.ToString();
                    ((HudStaticText)hudListRowAccessor[5]).Visible = false;
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        private void UpdateWorldObject(WorldObject obj, HudList hl)
        {
            try
            {
                if (obj.Container != 0)
                {
                    RemoveWorldObject(obj.Id, hl);
                }
                else
                {
                    for (int i = 0; i < hl.RowCount; i++)
                    {
                        HudList.HudListRowAccessor hudListRowAccessor = hl[i];
                        HudStaticText hudStaticText = (HudStaticText)hudListRowAccessor[5];
                        if (Convert.ToInt32(hudStaticText.Text) == obj.Id)
                        {
                            bool flag = false;
                            if (!flag && hl.Equals(MainView.CorpseList) && obj.ObjectClass == ObjectClass.Corpse)
                            {
                                flag = true;
                            }
                            if (!flag && hl.Equals(MainView.PortalList) && obj.ObjectClass == ObjectClass.Portal)
                            {
                                flag = true;
                            }
                            if (!flag && obj.ObjectClass == ObjectClass.Player)
                            {
                                if (hl.Equals(MainView.GuildList) && obj.Id == lib.MyCore.CharacterFilter.Id)
                                {
                                    flag = true;
                                }
                                else if (hl.Equals(MainView.GuildList) && IsFriend(obj))
                                {
                                    flag = true;
                                }
                                else if (hl.Equals(MainView.PlayerList) && IsEnemy(obj))
                                {
                                    flag = true;
                                }
                                else if (hl.Equals(MainView.GuildList) && obj.Values(LongValueKey.Monarch) == lib.moncheck && obj.Id != lib.MyCore.CharacterFilter.Id)
                                {
                                    flag = true;
                                }
                                else if (hl.Equals(MainView.PlayerList) && obj.Values(LongValueKey.Monarch) != lib.moncheck && obj.Id != lib.MyCore.CharacterFilter.Id)
                                {
                                    flag = true;
                                }
                            }
                            if (flag)
                            {
                                UpdateRow(obj, hudListRowAccessor);
                            }
                            else
                            {
                                RemoveWorldObject(obj.Id, hl);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        private void RemoveWorldObject(int id, HudList hl)
        {
            try
            {
                for (int i = hl.RowCount - 1; i >= 0; i--)
                {
                    HudList.HudListRowAccessor hudListRowAccessor = hl[i];
                    HudStaticText hudStaticText = (HudStaticText)hudListRowAccessor[5];
                    if (Convert.ToInt32(hudStaticText.Text) == id)
                    {
                        hl.RemoveRow(i);
                    }
                }           
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        private void UpdateRow(WorldObject obj, HudList.HudListRowAccessor row)
        {
            try
            {
                double distance = CoreManager.Current.WorldFilter.Distance(obj.Id, CoreManager.Current.CharacterFilter.Id) * 240.0;
                CoordsObject coordsObject = obj.Coordinates();
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

                if (obj.ObjectClass != ObjectClass.Corpse && obj.Id != lib.MyCore.CharacterFilter.Id)
                {
                    foreach (WorldObject worldObject in lib.MyCore.WorldFilter.GetByOwner(obj.Id))
                    {
                        if (worldObject.Name.IndexOf("arrow", 0) < 0)
                        {
                            string a = worldObject.ObjectClass.ToString().Trim();
                            if (a == "MeleeWeapon" || a == "WandStaffOrb" || a == "MissileWeapon")
                            {
                                ((HudPictureBox)row[0]).Image = worldObject.Icon;
                                break;
                            }
                        }
                    }
                }
                else if (obj.Id == lib.MyCore.CharacterFilter.Id)
                {
                    ((HudPictureBox)row[0]).Image = 100689830;
                }

                if (obj.ObjectClass == ObjectClass.Player && obj.Id == lib.MyCore.CharacterFilter.Id)
                {
                    if (obj.Id == lib.MyCore.CharacterFilter.Id)
                    {
                        ((HudPictureBox)row[0]).Image = 100689830;
                        ((HudStaticText)row[1]).Text = obj.Name;
                        ((HudStaticText)row[2]).Text = "My LOC";
                        ((HudStaticText)row[3]).Text = "";
                        ((HudPictureBox)row[4]).Image = 100667896;
                        ((HudStaticText)row[5]).Text = obj.Id.ToString();
                        ((HudStaticText)row[5]).Visible = false;
                        if (lib.MyCore.Actions.CurrentSelection == obj.Id)
                        {
                            ((HudStaticText)row[1]).TextColor = Color.HotPink;
                            ((HudStaticText)row[2]).TextColor = Color.HotPink;
                            ((HudStaticText)row[3]).TextColor = Color.HotPink;
                        }
                        else
                        {
                            ((HudStaticText)row[1]).TextColor = Color.LawnGreen;
                            ((HudStaticText)row[2]).TextColor = Color.LawnGreen;
                            ((HudStaticText)row[3]).TextColor = Color.LawnGreen;
                        }
                    }
                }

                if (obj.ObjectClass == ObjectClass.Player && obj.Id != lib.MyCore.CharacterFilter.Id)
                {
                    ((HudStaticText)row[1]).Text = obj.Name;
                    ((HudStaticText)row[2]).Text = string.Format("{0:N1}", distance);
                    ((HudStaticText)row[3]).Text = text;
                    ((HudPictureBox)row[4]).Image = 100667896;
                    ((HudStaticText)row[5]).Text = obj.Id.ToString();
                    ((HudStaticText)row[5]).Visible = false;

                    if (lib.MyCore.Actions.CurrentSelection == obj.Id)
                    {
                        ((HudStaticText)row[1]).TextColor = Color.HotPink;
                        ((HudStaticText)row[2]).TextColor = Color.HotPink;
                        ((HudStaticText)row[3]).TextColor = Color.HotPink;
                    }
                    else if (obj.Values(LongValueKey.Monarch) != lib.moncheck || IsEnemy(obj))
                    {
                        if (distance < 63)
                        {
                            if(lib.OnDowntime.Contains(obj.Name))
                            {
                                ((HudStaticText)row[1]).TextColor = Color.White;
                                ((HudStaticText)row[2]).TextColor = Color.White;
                                ((HudStaticText)row[3]).TextColor = Color.White;
                            }
                            else if (lib.OnDowntime2.Contains(obj.Name) && lib.Flicker == false)
                            {
                                ((HudStaticText)row[1]).TextColor = Color.Red;
                                ((HudStaticText)row[2]).TextColor = Color.Red;
                                ((HudStaticText)row[3]).TextColor = Color.Red;
                            }
                            else if (lib.OnDowntime2.Contains(obj.Name) && lib.Flicker == true)
                            {
                                ((HudStaticText)row[1]).TextColor = Color.White;
                                ((HudStaticText)row[2]).TextColor = Color.White;
                                ((HudStaticText)row[3]).TextColor = Color.White;
                            }
                            else if (!lib.OnDowntime2.Contains(obj.Name))
                            {
                                ((HudStaticText)row[1]).TextColor = Color.Red;
                                ((HudStaticText)row[2]).TextColor = Color.Red;
                                ((HudStaticText)row[3]).TextColor = Color.Red;
                            }
                        }
                        else if (distance >= 63 && distance <= 500)
                        {
                            if (lib.OnDowntime.Contains(obj.Name))
                            {
                                ((HudStaticText)row[1]).TextColor = Color.White;
                                ((HudStaticText)row[2]).TextColor = Color.White;
                                ((HudStaticText)row[3]).TextColor = Color.White;
                            }
                            else if (lib.OnDowntime2.Contains(obj.Name) && lib.Flicker == false)
                            {
                                ((HudStaticText)row[1]).TextColor = Color.DarkRed;
                                ((HudStaticText)row[2]).TextColor = Color.DarkRed;
                                ((HudStaticText)row[3]).TextColor = Color.DarkRed;
                            }
                            else if (lib.OnDowntime2.Contains(obj.Name) && lib.Flicker == true)
                            {
                                ((HudStaticText)row[1]).TextColor = Color.White;
                                ((HudStaticText)row[2]).TextColor = Color.White;
                                ((HudStaticText)row[3]).TextColor = Color.White;
                            }
                            else if (!lib.OnDowntime2.Contains(obj.Name))
                            {
                                ((HudStaticText)row[1]).TextColor = Color.DarkRed;
                                ((HudStaticText)row[2]).TextColor = Color.DarkRed;
                                ((HudStaticText)row[3]).TextColor = Color.DarkRed;
                            }
                        }
                        else if (distance > 500)
                        {
                            ((HudStaticText)row[2]).Text = "LOCATE";
                            ((HudStaticText)row[1]).TextColor = Color.Gold;
                            ((HudStaticText)row[2]).TextColor = Color.Gold;
                            ((HudStaticText)row[3]).TextColor = Color.Gold;

                            if (lib.DistanceCheck.Contains(obj.Id))
                            {
                                lib.DistanceCheck.Remove(obj.Id);
                                string key = obj.Values(LongValueKey.Landblock).ToString("X8").Substring(0, 4);
                                List<string> Landblock = (from f in lib.LocKey.Split(new char[] { ',' })
                                                          select f.Trim()).ToList<string>();

                                if (lib.LocKey.Contains(key))
                                {
                                    foreach (string el in Landblock)
                                    {
                                        if (el.Contains(key))
                                        {
                                            string elloc = el.Split(new string[] { "=" }, StringSplitOptions.None)[1];
                                            Utility.AddChatText(obj.Name + " was last seen at " + text + " (" + elloc + ")", 6);
                                            if (lib.UseAlertLS == true)
                                            {
                                                Utility.InvokeTextA(obj.Name + " was last seen at " + text + " (" + elloc + ")");
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Utility.AddChatText(obj.Name + " was last seen at " + text + " (" + key + " : Unset)", 6);
                                    if (lib.UseAlertLS == true)
                                    {
                                        Utility.InvokeTextA(obj.Name + " was last seen at " + text + " (" + key + " : Unset)");
                                    }
                                }
                            }
                        }
                    }
                    else if (obj.Values(LongValueKey.Monarch) == lib.moncheck || IsFriend(obj))
                    {
                        if (distance < 63)
                        {
                            if (lib.OnDowntime.Contains(obj.Name))
                            {
                                ((HudStaticText)row[1]).TextColor = Color.White;
                                ((HudStaticText)row[2]).TextColor = Color.White;
                                ((HudStaticText)row[3]).TextColor = Color.White;
                            }
                            else if (lib.OnDowntime2.Contains(obj.Name) && lib.Flicker == false)
                            {
                                ((HudStaticText)row[1]).TextColor = Color.LawnGreen;
                                ((HudStaticText)row[2]).TextColor = Color.LawnGreen;
                                ((HudStaticText)row[3]).TextColor = Color.LawnGreen;
                            }
                            else if (lib.OnDowntime2.Contains(obj.Name) && lib.Flicker == true)
                            {
                                ((HudStaticText)row[1]).TextColor = Color.White;
                                ((HudStaticText)row[2]).TextColor = Color.White;
                                ((HudStaticText)row[3]).TextColor = Color.White;
                            }
                            else if (!lib.OnDowntime2.Contains(obj.Name))
                            {
                                ((HudStaticText)row[1]).TextColor = Color.LawnGreen;
                                ((HudStaticText)row[2]).TextColor = Color.LawnGreen;
                                ((HudStaticText)row[3]).TextColor = Color.LawnGreen;
                            }
                        }
                        else if (distance >= 63 && distance <= 500)
                        {
                            if (lib.OnDowntime.Contains(obj.Name))
                            {
                                ((HudStaticText)row[1]).TextColor = Color.White;
                                ((HudStaticText)row[2]).TextColor = Color.White;
                                ((HudStaticText)row[3]).TextColor = Color.White;
                            }
                            else if (lib.OnDowntime2.Contains(obj.Name) && lib.Flicker == false)
                            {
                                ((HudStaticText)row[1]).TextColor = Color.Green;
                                ((HudStaticText)row[2]).TextColor = Color.Green;
                                ((HudStaticText)row[3]).TextColor = Color.Green;
                            }
                            else if (lib.OnDowntime2.Contains(obj.Name) && lib.Flicker == true)
                            {
                                ((HudStaticText)row[1]).TextColor = Color.White;
                                ((HudStaticText)row[2]).TextColor = Color.White;
                                ((HudStaticText)row[3]).TextColor = Color.White;
                            }
                            else if (!lib.OnDowntime2.Contains(obj.Name))
                            {
                                ((HudStaticText)row[1]).TextColor = Color.Green;
                                ((HudStaticText)row[2]).TextColor = Color.Green;
                                ((HudStaticText)row[3]).TextColor = Color.Green;
                            }
                        }
                        else if (distance > 500)
                        {
                            ((HudStaticText)row[2]).Text = "LOCATE";
                            ((HudStaticText)row[1]).TextColor = Color.Gold;
                            ((HudStaticText)row[2]).TextColor = Color.Gold;
                            ((HudStaticText)row[3]).TextColor = Color.Gold;

                            if (lib.DistanceCheck.Contains(obj.Id))
                            {
                                lib.DistanceCheck.Remove(obj.Id);
                                string key = obj.Values(LongValueKey.Landblock).ToString("X8").Substring(0, 4);
                                List<string> Landblock = (from f in lib.LocKey.Split(new char[] { ',' })
                                                          select f.Trim()).ToList<string>();

                                if (lib.LocKey.Contains(key))
                                {
                                    foreach (string el in Landblock)
                                    {
                                        if (el.Contains(key))
                                        {
                                            string elloc = el.Split(new string[] { "=" }, StringSplitOptions.None)[1];
                                            Utility.AddChatText(obj.Name + " was last seen at " + text + " (" + elloc + ")", 6);
                                        }
                                    }
                                }
                                else
                                {
                                    Utility.AddChatText(obj.Name + " was last seen at " + text + " (" + key + " : Unset)", 6);
                                }
                            }
                        }
                    }
                }

                if (obj.ObjectClass != ObjectClass.Player)
                {
                    ((HudPictureBox)row[0]).Image = obj.Icon;
                    ((HudStaticText)row[1]).Text = obj.Name;
                    ((HudStaticText)row[2]).Text = string.Format("{0:N1}", distance);
                    ((HudStaticText)row[3]).Text = text;
                    ((HudPictureBox)row[4]).Image = 100667896;
                    ((HudStaticText)row[5]).Text = obj.Id.ToString();
                    ((HudStaticText)row[5]).Visible = false;

                    if (obj.Id == lib.MyCore.Actions.CurrentSelection)
                    {
                        ((HudStaticText)row[1]).TextColor = Color.HotPink;
                        ((HudStaticText)row[2]).TextColor = Color.HotPink;
                        ((HudStaticText)row[3]).TextColor = Color.HotPink;
                    }
                    else if (obj.Id != lib.MyCore.Actions.CurrentSelection)
                    {
                        if (distance <= 500)
                        {
                            ((HudStaticText)row[1]).TextColor = Color.White;
                            ((HudStaticText)row[2]).TextColor = Color.White;
                            ((HudStaticText)row[3]).TextColor = Color.White;
                        }
                        else if (distance > 500)
                        {
                            ((HudStaticText)row[2]).Text = "Too far!";
                            ((HudStaticText)row[1]).TextColor = Color.Gold;
                            ((HudStaticText)row[2]).TextColor = Color.Gold;
                            ((HudStaticText)row[3]).TextColor = Color.Gold;
                        }
                    }
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        private void Refresh()
        {
            if (lib.gameStatus > 0)
            {
                for (int n = 0; n < MainView.PlayerList.RowCount; n++)
                {
                    bool flag1 = true;
                    HudList.HudListRowAccessor hudListRowAccessor = MainView.PlayerList[n];
                    HudStaticText hudStaticText1 = (HudStaticText)hudListRowAccessor[5];
                    int num1 = Convert.ToInt32(hudStaticText1.Text);
                    if (lib.MyCore.Actions.IsValidObject(num1))
                    {
                        WorldObject worldObject1 = lib.MyCore.WorldFilter[num1];
                        if (worldObject1.ObjectClass == ObjectClass.Player)
                        {
                            UpdateRow(worldObject1, hudListRowAccessor);
                            flag1 = false;
                        }
                    }
                    if (flag1)
                    {
                        MainView.PlayerList.RemoveRow(n);
                    }
                }

                for (int m = 0; m < MainView.GuildList.RowCount; m++)
                {
                    bool flag2 = true;
                    HudList.HudListRowAccessor hudListRowAccessor = MainView.GuildList[m];
                    HudStaticText hudStaticText2 = (HudStaticText)hudListRowAccessor[5];
                    int num2 = Convert.ToInt32(hudStaticText2.Text);
                    if (lib.MyCore.Actions.IsValidObject(num2))
                    {
                        WorldObject worldObject2 = lib.MyCore.WorldFilter[num2];
                        if (worldObject2.ObjectClass == ObjectClass.Player)
                        {
                            UpdateRow(worldObject2, hudListRowAccessor);
                            flag2 = false;
                        }
                    }
                    if (flag2)
                    {
                        MainView.GuildList.RemoveRow(m);
                    }
                }

                MainView.GuildNote.Text = "Friendlies " + Convert.ToString(MainView.GuildList.RowCount);
                MainView.EnemyNote.Text = Convert.ToString(MainView.PlayerList.RowCount) + " Enemies";
     
                if (lib.LastSelected != 0 && !lib.MyCore.Actions.IsValidObject(lib.LastSelected))
                {
                    lib.LastSelected = 0;
                    lib.PointArrow.Dispose();
                    lib.PointArrow = null;    
                }
            }          
        }

        private void Refresh2()
        {

            if (lib.gameStatus > 0)
            {
                for (int k = 0; k < MainView.CorpseList.RowCount; k++)
                {
                    bool flag3 = true;
                    HudList.HudListRowAccessor hudListRowAccessor = MainView.CorpseList[k];
                    HudStaticText hudStaticText3 = (HudStaticText)hudListRowAccessor[5];
                    int num3 = Convert.ToInt32(hudStaticText3.Text);
                    if (lib.MyCore.Actions.IsValidObject(num3))
                    {
                        WorldObject worldObject2 = lib.MyCore.WorldFilter[num3];
                        if (worldObject2.ObjectClass == ObjectClass.Corpse)
                        {
                            UpdateRow(worldObject2, hudListRowAccessor);
                            flag3 = false;
                        }
                    }
                    if (flag3)
                    {
                        MainView.CorpseList.RemoveRow(k);
                    }
                }
                for (int l = 0; l < MainView.PortalList.RowCount; l++)
                {
                    bool flag4 = true;
                    HudList.HudListRowAccessor hudListRowAccessor = MainView.PortalList[l];
                    HudStaticText hudStaticText4 = (HudStaticText)hudListRowAccessor[5];
                    int num4 = Convert.ToInt32(hudStaticText4.Text);
                    if (lib.MyCore.Actions.IsValidObject(num4))
                    {
                        WorldObject worldObject3 = lib.MyCore.WorldFilter[num4];
                        if (worldObject3.ObjectClass == ObjectClass.Portal)
                        {
                            UpdateRow(worldObject3, hudListRowAccessor);
                            flag4 = false;
                        }
                    }
                    if (flag4)
                    {
                        MainView.PortalList.RemoveRow(l);
                    }
                }
                if (lib.LastSelected != 0 && !lib.MyCore.Actions.IsValidObject(lib.LastSelected))
                {
                    lib.LastSelected = 0;
                    lib.PointArrow.Dispose();
                    lib.PointArrow = null;             
                }
            }
        }

        public void Rescan()
        {
            try
            {
                foreach (WorldObject @new in lib.MyCore.WorldFilter.GetAll())
                {
                    if (@new.ObjectClass == ObjectClass.Corpse)
                    {
                        AddWorldObject(@new, MainView.CorpseList);
                    }
                    if (@new.ObjectClass == ObjectClass.Portal)
                    {
                        AddWorldObject(@new, MainView.PortalList);
                    }
                    if (@new.ObjectClass == ObjectClass.Player && @new.Id == lib.MyCore.CharacterFilter.Id)
                    {
                        AddWorldObject(@new, MainView.GuildList);
                    }
                    if (@new.ObjectClass == ObjectClass.Player)
                    {
                        if (@new.Id == lib.MyCore.CharacterFilter.Id)
                        {
                            AddWorldObject(@new, MainView.GuildList);
                            if (!lib.DistanceCheck.Contains(@new.Id))
                            {
                                lib.DistanceCheck.Add(@new.Id);
                            }
                        }
                        if (IsFriend(@new))
                        {
                            AddWorldObject(@new, MainView.GuildList);
                            if (!lib.DistanceCheck.Contains(@new.Id))
                            {
                                lib.DistanceCheck.Add(@new.Id);
                            }
                        }
                        if (IsEnemy(@new))
                        {
                            AddWorldObject(@new, MainView.PlayerList);
                            if (!lib.LogList.Contains(@new.Id))
                            {
                                lib.LogList.Add(@new.Id);
                            }
                            if (!lib.DistanceCheck.Contains(@new.Id))
                            {
                                lib.DistanceCheck.Add(@new.Id);
                            }
                        }
                        if (@new.Values(LongValueKey.Monarch) == lib.moncheck && !IsEnemy(@new) && @new.Id != lib.MyCore.CharacterFilter.Id)
                        {
                            AddWorldObject(@new, MainView.GuildList);
                            if (!lib.DistanceCheck.Contains(@new.Id))
                            {
                                lib.DistanceCheck.Add(@new.Id);
                            }
                        }
                        if (@new.Values(LongValueKey.Monarch) != lib.moncheck && !IsFriend(@new) && @new.Id != lib.MyCore.CharacterFilter.Id)
                        {
                            AddWorldObject(@new, MainView.PlayerList);
                            if (!lib.LogList.Contains(@new.Id))
                            {
                                lib.LogList.Add(@new.Id);
                            }
                            if (!lib.DistanceCheck.Contains(@new.Id))
                            {
                                lib.DistanceCheck.Add(@new.Id);
                            }
                        }
                    }
                }        
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }
    }
}
