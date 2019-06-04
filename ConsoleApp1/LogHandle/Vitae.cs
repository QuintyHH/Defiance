using Defiance.BaseHandle;
using Defiance.Utils;
using System;
using System.Drawing;

namespace Defiance.LogHandle
{
    class Vitae
    {
        public static void VitaeInit()
        {
            if (lib.VitaeInstance < 1)
            {
                if (lib.gameStatus < 3)
                {
                    lib.MyCore.CharacterFilter.LoginComplete += Vitae_LoginComplete;
                }
                else if (lib.gameStatus >= 3)
                {
                    Core_VitaeCheck();
                }
                lib.VitaeInstance++;
            }
        }

        public static void Dispose()
        {
            if (lib.VitaeInstance > 0)
            {
                lib.MyCore.CharacterFilter.LoginComplete -= Vitae_LoginComplete;
                Sounds.DeadVP.Stop();
                PluginCore.LngTmr.Tick -= Vptimer_Tick;
                lib.VitaeInstance--;
            }
        }

        private static void Vitae_LoginComplete(object sender, EventArgs e)
        {
            Core_VitaeCheck();
        }

        private static void Core_VitaeCheck()
        {
            try
            {
                lib.vpcounter = 0;
                int vitae = lib.MyCore.CharacterFilter.Vitae;
                if (vitae >= 10)
                {
                    if (lib.DFTEXT != null)
                    {
                        lib.DFTEXT.Visible = false;
                        lib.DFTEXT.Dispose();
                        lib.DFTEXT = null;
                    }
                    else if (lib.DFTEXT == null)
                    {
                        float num = lib.MyHost.Underlying.Hooks.ObjectHeight(lib.MyID);
                        lib.DFTEXT = lib.MyCore.D3DService.MarkObjectWith3DText(lib.MyID, "Warning: "+  vitae + "% vitae!", "Times New Roman", 0);
                        lib.DFTEXT.Anchor(lib.MyCore.CharacterFilter.Id, 0.2f, 0f, 0f, num - 0.05f);
                        lib.DFTEXT.Color = Color.Red.ToArgb();
                        lib.DFTEXT.OrientToCamera(true);
                        lib.DFTEXT.ScaleX = 0.3f;
                        lib.DFTEXT.ScaleZ = 0.3f;
                        lib.DFTEXT.ScaleY = 0.3f;
                        lib.DFTEXT.Visible = true;
                    }

                    Utility.AddChatText("Your VP is " + vitae + "%, exceeding the 10% threshold!", 6);
                    Utility.AddChatText("You have 30 seconds to click the [VP] button or type /ignorevp.", 6);

                    if (lib.UseAlertDT == true)
                    {
                        try
                        {
                            Sounds.DeadVP.PlayLooping();
                        }
                        catch
                        {
                            Utility.AddChatText("Please make sure the sounds files are in the right location!", 6);
                        }
                    }
                    PluginCore.LngTmr.Tick += Vptimer_Tick;
                }
                else
                {
                    Dispose();
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        private static void Vptimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (lib.status > 0)
                {
                    lib.vpcounter = lib.vpcounter + 1;
                    if (lib.vpcounter == 30)
                    {
                        lib.reason = "vitae";                
                        lib.vpcounter = 0;
                        LogMethod.Logout();
                        Dispose();
                    }
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }
    }
}
