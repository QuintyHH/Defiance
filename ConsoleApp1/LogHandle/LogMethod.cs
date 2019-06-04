using Decal.Adapter;
using Decal.Adapter.Wrappers;
using Defiance.BaseHandle;
using Defiance.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Defiance.LogHandle
{
    class LogMethod
    {
        public void LogInit()
        {
            if (lib.LoggingInstance < 1)
            {
                PluginCore.LngTmr.Tick += Logoff_Tick;
                lib.MyCore.CharacterFilter.Logoff += Logoff_Request;
                lib.MyCore.ChatBoxMessage += Logoff_Request2;
                Vitae.VitaeInit();
                Death.DeathInit();
                Components.CompsInit();
                Inventory.InventoryInit();
                Detection.DetectionInit();
                Remote.RemoteInit();

                lib.LoggingInstance++;
            }        
        }

        public void Dispose()
        {
            lib.MyCore.CharacterFilter.Logoff -= Logoff_Request;
            lib.MyCore.ChatBoxMessage -= Logoff_Request2;
            PluginCore.LngTmr.Tick -= Logoff_Tick;

            Remote.Dispose();
            Detection.Dispose();
            Inventory.Dispose();
            Components.Dispose();
            Vitae.Dispose();
            Death.Dispose();

            lib.LoggingInstance--;
        }

        public static void ReLogout(WorldObject obj)
        {
            try
            {
                if (lib.gameStatus != 4)
                {
                    if (lib.UseAlertLogP == true)
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
                                    Utility.InvokeTextA("Logged by: " + obj.Name + " at " + text + " (" + elloc + ")");
                                }
                            }
                        }
                        else
                        {
                            Utility.InvokeTextA("Logged by: " + obj.Name + " at " + text + " (" + key + " : Unset)");
                        }
                    }

                    if (lib.UseMacroLogic == false)
                    {
                        PluginCore.RlgTmr.Interval = lib.Timer * 1000;
                        lib.relogcounter = 0;
                    }
                    else if (lib.UseMacroLogic == true)
                    {
                        if (lib.relogcounter < 5)
                        {
                            lib.longcycle = false;
                            lib.relogcounter = lib.relogcounter + 1;
                            PluginCore.RlgTmr.Interval = lib.Timer * 1000;
                        }
                        else if (lib.relogcounter >= 5 && lib.UseMacroLogic == true)
                        {
                            lib.longcycle = true;
                            lib.relogcounter = 0;
                            PluginCore.RlgTmr.Interval = 600000;
                        }
                    }
                    if (PluginCore.RlgTmr.Enabled == false)
                    {
                        PluginCore.RlgTmr.Start();
                        PluginCore.RlgTmr.Tick += Relogger_Tick;
                    }

                    lib.gameStatus = 4;
                    lib.Autorelog = true;
                    lib.MyCore.Actions.Logout();
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        private static void Relogger_Tick(object sender, EventArgs e)
        {
            try
            {
                Utility.SendMouseClick(348, 389);
                PluginCore.RlgTmr.Stop();
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public static void Logout()
        {
            try
            {
                if (lib.gameStatus != 4)
                {
                    lib.gameStatus = 4;
                    lib.Autorelog = false;       
                    lib.MyCore.Actions.Logout();
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        private void Logoff_Request(object sender, EventArgs e)
        {
            try
            {                      
                lib.logging = true;
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        private void Logoff_Request2(object sender, ChatTextInterceptEventArgs e)
        {
            try
            {
                if (e.Color == 0 && e.Text.Contains("Logging off..."))
                {
                    lib.logging = true;
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        private static void Logoff_Tick(object sender, EventArgs e)
        {

            if (lib.logging == true)
            {
                Utility.AddChatText("Attempting to log off: " + lib.logoffcounter, 6);
                lib.logoffcounter = lib.logoffcounter + 1;
                lib.MyCore.Actions.Logout();
                if (lib.logoffcounter == 1)
                {
                    Report.LogEvent("Logout");
                }
                if (lib.logoffcounter >= 30)
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
