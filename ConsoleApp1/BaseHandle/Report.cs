using Decal.Adapter;
using Decal.Adapter.Wrappers;
using Defiance.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Defiance.BaseHandle
{
    class Report
    {
        public void Dispose()
        {
            lib.MyCore.CharacterFilter.LoginComplete -= ReportStart;
            PluginCore.LogTmr.Tick -= Report_Tick;
            lib.ReportInstance--;
        }

        public void ReportInit()
        {
            try
            {
                if (lib.ReportInstance < 1)
                {
                    lib.MyCore.CharacterFilter.LoginComplete += ReportStart;                
                    lib.ReportInstance++;
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }


        public void ReportStart(object sender, EventArgs e)
        {
            PluginCore.LogTmr.Tick += Report_Tick;
        }

        public static void LoggerIdentity()
        {
            string key = "::Logger";
            List<string> list = (from f in lib.AuthIds.Split(new char[] { ',' })
                                 select f.Trim()).ToList<string>();

            if (lib.AuthIds.Contains(key))
            {
                foreach (string log in list)
                {
                    if (log.Contains(key))
                    {
                        lib.Logger = log.Split(new string[] { "::Logger" }, StringSplitOptions.None)[0];
                    }
                }
            }
        }

        public static void LogEvent(string Event)
        {
            try
            {
                if (lib.Logger != null && lib.Logger != lib.MyName)
                {
                    WorldObject obj = lib.MyCore.WorldFilter[lib.MyID];
                    CoordsObject coordsObject = obj.Coordinates();
                    string coords;

                    if (coordsObject.NorthSouth >= 0.0)
                    {
                        coords = string.Format("{0:N1}", coordsObject.NorthSouth) + "No, ";
                    }
                    else
                    {
                        coords = string.Format("{0:N1}", coordsObject.NorthSouth * -1.0) + "So, ";
                    }
                    if (coordsObject.EastWest >= 0.0)
                    {
                        coords = coords + string.Format("{0:N1}", coordsObject.EastWest) + "Ea";
                    }
                    else
                    {
                        coords = coords + string.Format("{0:N1}", coordsObject.EastWest * -1.0) + "We";
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
                    CoreManager.Current.Actions.InvokeChatParser(string.Format("{0} {1}, Logging::{2}:{3}:{4}:{5}:{6}:{7}:", lib.log, lib.Logger, lib.MyName, lib.Mode, Event, lib.reason, coords, elloc));
                }
            }
            catch
            { }
        }

        public static void InitialLog(string Event)
        {
            try
            {
                string Message;

                if (lib.Autorelog == true)
                {
                    Message = "Relogging";
                }
                else
                {
                    Message = "Manual";
                }
                CoreManager.Current.Actions.InvokeChatParser(string.Format("{0} {1}, Logging::{2}:{3}:{4}:{5}:{6}:{7}:", lib.log, lib.Logger, lib.MyName, lib.Mode, Event, Message, "0.0", "Unset"));
            }
            catch
            { }
        }

        public void Report_Tick(object sender, EventArgs e)
        {
            if (lib.logoffcounter < 1)
            {
                LogEvent("Refresh");
            }
        }
    }
}
