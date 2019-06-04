using Decal.Adapter;
using Decal.Adapter.Wrappers;
using Defiance.BaseHandle;
using Defiance.Utils;
using Defiance.Views;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Defiance.LogHandle
{
    class Death
    {
        public static Timer DthTmr1 = new Timer();
        public static Timer DthTmr2 = new Timer();

        public static void DeathInit()
        {
            if (lib.DeathInstance < 1)
            {
                lib.MyCore.EchoFilter.ServerDispatch += ServerDispatch_DeathFilter;
                lib.DeathInstance++;
            }
        }

        public static void Dispose()
        {
            lib.MyCore.EchoFilter.ServerDispatch -= ServerDispatch_DeathFilter;
            lib.DeathInstance--;
        }

        public static void ServerDispatch_DeathFilter(object sender, NetworkMessageEventArgs e)
        {
            try
            {
                int num = e.Message.Type;
                if (num == 414)
                {
                    int killed = e.Message.Value<int>("killed");
                    int killer = e.Message.Value<int>("killer");
                    if (killed == lib.MyID)
                    {
                        WorldObject obj = lib.MyCore.WorldFilter[killer];
                        if (lib.Mode == 3)
                        {
                            if (lib.UseAlertDA == true)
                            {
                                Utility.InvokeTextA("Killed by " + obj.Name + " detected, Logging off!");
                            }
                            lib.reason = "death by " + obj.Name;
                            LogMethod.Logout();
                        }
                        else if (lib.Mode != 3)
                        {
                            if (lib.MyCore.CharacterFilter.Vitae >= 10)
                            {
                                lib.reason = "death by " + obj.Name;
                                Vitae.VitaeInit();
                                Report.LogEvent("Death");
                            }
                        }

                        if (obj.ObjectClass == ObjectClass.Player)
                        {
                            if (DthTmr1.Enabled == true)
                            {
                                DthTmr1.Tick -= DthTmr_Tick;
                                DthTmr1.Stop();
                                DthTmr1.Dispose();
                            }

                            if (DthTmr2.Enabled == true)
                            {
                                DthTmr2.Tick -= DthTmr_Tick2;
                                DthTmr2.Stop();
                                DthTmr2.Dispose();
                            }

                            if (DthTmr1.Enabled == false)
                            {
                                DthTmr1.Interval = 290000;
                                DthTmr1.Tick += DthTmr_Tick;
                                DthTmr1.Start();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }
               
        protected static void DthTmr_Tick(object sender, EventArgs e)
        {
            if (lib.status > 0)
            {
                Utility.AddChatText("--- YOU WILL TURN RED IN 10 SECONDS! ---", 6);
                if (lib.DFTEXT != null)
                {
                    lib.DFTEXT.Visible = false;
                    lib.DFTEXT.Dispose();
                    lib.DFTEXT = null;
                }

                if (lib.DFTEXT == null)
                {
                    float num = lib.MyHost.Underlying.Hooks.ObjectHeight(lib.MyID);
                    lib.DFTEXT = CoreManager.Current.D3DService.MarkObjectWith3DText(CoreManager.Current.CharacterFilter.Id, "Warning: Turning RED!", "Times New Roman", 0);
                    lib.DFTEXT.Anchor(lib.MyCore.CharacterFilter.Id, 0.2f, 0f, 0f, num - 0.05f);
                    lib.DFTEXT.Color = Color.Red.ToArgb();
                    lib.DFTEXT.OrientToCamera(true);
                    lib.DFTEXT.ScaleX = 0.3f;
                    lib.DFTEXT.ScaleZ = 0.3f;
                    lib.DFTEXT.ScaleY = 0.3f;
                    lib.DFTEXT.Visible = true;
                }

                if (MainView.AlertPKT.Checked == true)
                {
                    Sounds.PkTime1.Play();
                }
            }

            if (DthTmr1.Enabled == true)
            {                
                DthTmr1.Tick -= DthTmr_Tick;
                DthTmr1.Stop();
                DthTmr1.Dispose();
            }

            if (DthTmr2.Enabled == false)
            {
                DthTmr2.Interval = 10000;
                DthTmr2.Tick += DthTmr_Tick2;
                DthTmr2.Start();
            }
        }

        protected static void DthTmr_Tick2(object sender, EventArgs e)
        {
            if (lib.status > 0)
            {
                Utility.AddChatText("--- YOU ARE NOW RED ---", 6);
                if (lib.DFTEXT != null)
                {
                    lib.DFTEXT.Visible = false;
                    lib.DFTEXT.Dispose();
                    lib.DFTEXT = null;
                }

                if (MainView.AlertPKT.Checked == true)
                {
                    Sounds.PkTime2.Play();
                }
            }

            if (DthTmr2.Enabled == true)
            {
                DthTmr2.Tick -= DthTmr_Tick2;
                DthTmr2.Stop();
                DthTmr2.Dispose();
            }
        }
    }
}
