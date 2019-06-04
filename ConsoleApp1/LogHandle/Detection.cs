using Decal.Adapter.Wrappers;
using Defiance.BaseHandle;
using Defiance.Utils;
using Defiance.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Defiance.LogHandle
{
    class Detection
    {
        public static void DetectionInit()
        {
            if (lib.DetectionIstance < 1)
            {
                PluginCore.ShrtTmr.Tick += PkLogoffTrigger;
                lib.MyCore.WorldFilter.CreateObject += PkDetection;               
                lib.DetectionIstance++;
            }
        }

        public static void Dispose()
        {
            lib.MyCore.WorldFilter.CreateObject -= PkDetection;
            PluginCore.ShrtTmr.Tick -= PkLogoffTrigger;
            lib.DetectionIstance--;
        }

        private static void PkDetection(object sender, CreateObjectEventArgs e)
        {
            try
            {
                if (e.New.ObjectClass == ObjectClass.Player && lib.LogList.Contains(e.New.Id))
                {
                    if (lib.UseAlertSPK)
                    {
                        Sounds.Pk.Play();
                    }
                           
                    if (lib.Mode != 3)
                    {
                        WorldObject obj = e.New;
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
                                    Utility.AddChatText("Enemy Detected: " + obj.Name + " at " + text + " (" + elloc + ")", 6);
                                    if (lib.UseAlertPF == true)
                                    {
                                        Utility.InvokeTextF("Enemy Detected: " + obj.Name + " at " + text + " (" + elloc + ")");
                                    }

                                    if (lib.UseAlertPA == true)
                                    {
                                        Utility.InvokeTextA("Enemy Detected: " + obj.Name + " at " + text + " (" + elloc + ")");
                                    }
                                }
                            }
                        }
                        else
                        {
                            Utility.AddChatText("Enemy Detected: " + obj.Name + " at " + text + " (" + key + " : Unset)", 6);
                            if (lib.UseAlertPF == true)
                            {
                                Utility.InvokeTextF("Enemy Detected: " + obj.Name + " at " + text + " (" + key + " : Unset)");
                            }

                            if (lib.UseAlertPA == true)
                            {
                                Utility.InvokeTextA("Enemy Detected: " + obj.Name + " at " + text + " (" + key + " : Unset)");
                            }
                        }
                    }
                }
            }
            catch (Exception ex2) { Repo.RecordException(ex2); }
        }

        private static void PkLogoffTrigger(object sender, EventArgs e)
        {
            try
            {     
                if (MainView.PlayerList.RowCount > 0 && lib.Mode == 3 && lib.Behaviour == 0)
                {
                    foreach (WorldObject @new in lib.MyCore.WorldFilter.GetByObjectClass(ObjectClass.Player))
                    {
                        double distance = lib.MyCore.WorldFilter.Distance(@new.Id, lib.MyCore.CharacterFilter.Id) * 240.0;
                        if (lib.UseMacroLogic == true)
                        {
                            if (distance < lib.Range && lib.LogList.Contains(@new.Id) && !lib.OnDowntime.Contains(@new.Name) && @new.Id != 1342186030)
                            {
                                lib.reason = @new.Name;
                                LogMethod.ReLogout(@new);
                                PluginCore.ShrtTmr.Tick -= PkLogoffTrigger;
                            }
                        }
                        else if (lib.UseMacroLogic == false)
                        {
                            if (distance < lib.Range && lib.LogList.Contains(@new.Id) && @new.Id != 1342186030)
                            {
                                lib.reason = @new.Name;
                                LogMethod.ReLogout(@new);
                                PluginCore.ShrtTmr.Tick -= PkLogoffTrigger;
                            }
                        }
                    }
                }   
            }
            catch (Exception ex2) { Repo.RecordException(ex2); }
        }
    }
}
