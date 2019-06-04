using Decal.Adapter;
using Decal.Adapter.Wrappers;
using Defiance.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Defiance.LogHandle
{
    class Remote
    {
        public static void RemoteInit()
        {
            if (lib.RemoteInstance < 1)
            {
                lib.MyCore.ChatBoxMessage += Core_RemoteCommands;
                lib.RemoteLogInstance = 0;
                lib.RemoteInstance++;
            }
        }

        public static void Dispose()
        {
            lib.MyCore.ChatBoxMessage -= Core_RemoteCommands;
            lib.RemoteInstance--;
        }

        private static void Core_RemoteCommands(object sender, ChatTextInterceptEventArgs e)
        {
            try
            {
                if (e.Text.Contains("Logging::") && lib.Logger != lib.MyName)
                {
                    e.Eat = true;
                }

                if (e.Color == 3 && e.Text.Contains("DFcom"))
                {
                    string pass = DateTime.Now.Ticks.ToString();
                    if (pass.Length > 9)
                    {
                        pass = pass.Substring(0, 9);
                    }

                    if (e.Text.Contains(pass) && e.Text.Contains(":Log:"))
                    {
                        if (lib.RemoteLogInstance == 0)
                        {
                            lib.reason = "ADMINISTRATOR";
                            Utility.InvokeTextA("Force-Logged by " + lib.reason + " at " + DateTime.Now.ToString("h:mm:ss tt"));
                            Utility.AddChatText("Force-Logged by " + lib.reason + "!", 6);

                            LogMethod.Logout();
                            lib.RemoteLogInstance++;
                        }
                        else if (lib.RemoteLogInstance != 0)
                        {
                            Utility.InvokeTextA("Already processing Force-Log request!");
                        }
                    }

                    if (e.Text.Contains(pass) && e.Text.Contains(":Relog:"))
                    {
                        if (lib.RemoteLogInstance == 0)
                        {
                            lib.reason = "ADMINISTRATOR";
                            Utility.InvokeTextA("Force-Relogged by " + lib.reason + " at " + DateTime.Now.ToString("h:mm:ss tt"));
                            Utility.AddChatText("Force-Relogged by " + lib.reason + "!", 6);
                            WorldObject obj = lib.MyCore.WorldFilter[lib.MyID];
                            LogMethod.ReLogout(obj);
                            lib.RemoteLogInstance++;
                        }
                        else if (lib.RemoteLogInstance != 0)
                        {
                            Utility.InvokeTextA("Already processing Force-Relog request!");

                        }
                    }

                    if (e.Text.Contains(pass) && e.Text.Contains(":Location:"))
                    {
                        if (lib.RemoteLogInstance == 0)
                        {
                            WorldObject obj = lib.MyCore.WorldFilter[lib.MyID];
                            CoordsObject coordsObject = obj.Coordinates();
                            string coords;

                            if (coordsObject.NorthSouth >= 0.0)
                            {
                                coords = string.Format("{0:N1}", coordsObject.NorthSouth) + "N, ";
                            }
                            else
                            {
                                coords = string.Format("{0:N1}", coordsObject.NorthSouth * -1.0) + "S, ";
                            }
                            if (coordsObject.EastWest >= 0.0)
                            {
                                coords = coords + string.Format("{0:N1}", coordsObject.EastWest) + "E";
                            }
                            else
                            {
                                coords = coords + string.Format("{0:N1}", coordsObject.EastWest * -1.0) + "W";
                            }
                            string key = obj.Values(LongValueKey.Landblock).ToString("X8").Substring(0, 4);
                            List<string> Landblock = (from f in lib.LocKey.Split(new char[] { ',' })
                                                      select f.Trim()).ToList<string>();

                            string elloc = null;
                            if (lib.LocKey.Contains(key))
                            {
                                foreach (string el in Landblock)
                                {
                                    if (el.Contains(key))
                                    {
                                        elloc = el.Split(new string[] { "=" }, StringSplitOptions.None)[1];
                                    }
                                }
                            }
                            else
                            {
                                elloc = key;
                            }
                            CoreManager.Current.Actions.InvokeChatParser("/r [Defiance]: I am currently at " + coords  + " (" + elloc + ")");
                        }
                    }

                    if (e.Text.Contains(pass) && e.Text.Contains(":Die:"))
                    {
                        if (lib.RemoteLogInstance == 0)
                        {
                            lib.reason = "ADMINISTRATOR";
                            Utility.InvokeTextA("Smitten by " + lib.reason + " at " + DateTime.Now.ToString("h:mm:ss tt"));
                            Utility.AddChatText("Smitten by " + lib.reason + "!", 6);
             
                            Utility.DispatchChatToBoxWithPluginIntercept("/die");
                            Utility.ClickYes();
                            lib.reason = "user";
                        }
                     
                    }
                    e.Eat = true;
                }                
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }
    }
}
