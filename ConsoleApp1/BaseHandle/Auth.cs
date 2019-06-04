using Decal.Adapter;
using Defiance.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Defiance.BaseHandle
{
    class Auth
    {
        public void Dispose()
        {           
            lib.AuthInstance--;
        }

        private string Fetch(string url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            string result;

            try
            {
                using (Stream responseStream = httpWebRequest.GetResponse().GetResponseStream())
                {
                    result = new StreamReader(responseStream, Encoding.UTF8).ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                using (Stream responseStream2 = ex.Response.GetResponseStream())
                {
                    new StreamReader(responseStream2, Encoding.GetEncoding("utf-8")).ReadToEnd();
                }
                result = "failed";
            }
            return result;
        }

        private void Get()
        {
            try
            {
                lib.AuthIds = Fetch(lib.authurl);
                lib.LocKey = Fetch(lib.landblocks);
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        private void ReAuth_Tick(object sender, EventArgs e)
        {
            if (lib.AuthIds == "failed")
            {
                if (lib.reauthcounter < 29) { lib.reauthcounter++; }
                else
                {
                    lib.reauthcounter = 0;
                    PluginCore.LngTmr.Tick -= ReAuth_Tick;
                    Core.Core_Login_Retry();
                }
            }
        }

        public void Initial(PluginCore PGCore)
        {
            try
            {
                Core = PGCore;
                if (lib.AuthInstance < 1)
                {
                    this.Get();
                    Report.LoggerIdentity();
       
                    if (lib.AuthIds == "failed" && CoreManager.Current.CharacterFilter.Name != "")
                    {
                        lib.status = 3;
                        lib.authtype = "Temporary";
                        Utility.AddChatText("Failed to connect to auth server! You are temporarely authorized.", 6);
                        Utility.AddWindowText(lib.MyServer + " : " + lib.MyName + " : " + lib.authtype + " : Loading...");
                        PluginCore.LngTmr.Tick += ReAuth_Tick;
                    }
                    else
                    {
                        List<string> list = (from f in lib.AuthIds.Split(new char[] { ',' })
                                             select f.Trim()).ToList<string>();

                        if (list.Contains(lib.MyName + "::Admin") || list.Contains(lib.MyName + "::Logger"))
                        {
                            lib.AdminAuth = true;
                            lib.UserAuth = false;
                            lib.status = 2;
                            lib.authtype = "Admin";
                        }
                        else if (list.Contains(lib.MyName))
                        {
                            lib.AdminAuth = false;
                            lib.UserAuth = true;
                            lib.status = 1;
                            lib.authtype = "User";
                        }
                        else
                        {
                            lib.AdminAuth = false;
                            lib.UserAuth = false;
                            lib.status = 0;
                            lib.authtype = "None";
                        }                 

                        if (lib.status == 1)
                        {
                            Utility.AddChatText("You are authorized! Type /defiance help for more information!", 6);
                            Utility.AddWindowText(lib.MyServer + " : " + lib.MyName + " : " + lib.authtype + " : Loading...");
                        }
                        else if (lib.status == 2)
                        {
                            Utility.AddChatText("You are authorized as Administrator! Type /defiance help for more information!", 6);
                            Utility.AddWindowText(lib.MyServer + " : " + lib.MyName + " : " + lib.authtype + " : Loading...");
                        }
                        else if (lib.status == 0)
                        {
                            Utility.AddChatText("You are not authorized! Check Discord for more information!", 6);
                            Utility.AddWindowText(lib.MyServer + " : " + lib.MyName + " : " + " Failed to authorize.");
                            Utility.AddChatText(lib.authurl, 6);
                        }
                        list.Clear();
                    }
                    lib.AuthInstance++;           
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }
        PluginCore Core;
    }
}