using Decal.Adapter;
using Defiance.BaseHandle;
using Defiance.Utils;
using Defiance.Views;
using System;

namespace Defiance.MacroHandle
{
    class RecallDetect
    {
        public static void RecallDetection(object sender, ChatTextInterceptEventArgs e)
        {
            try
            {
                if (lib.UseMacroLogic == true && lib.Mode == 3)
                {
                    if (e.Color == 0 && (e.Text.Contains(lib.MyName + " is going") || e.Text.Contains(lib.MyName + " is recalling")) || (e.Color == 17 && e.Text.Contains("You say, \"Shurov")))
                    {
                        lib.Mode = 0;
                        MainView.Mode.Current = 0;
                        Report.LogEvent("ModeSwitch");

                        if (lib.vtank == true)
                        {
                            Utility.DispatchChatToBoxWithPluginIntercept("/vt stop");
                            Utility.AddWindowText(lib.MyServer + " : " + lib.MyName + " : " + lib.authtype + " : Current mode: SLAVE - Zone change detected at " + DateTime.Now.ToString("h:mm:ss tt"));
                            Utility.AddChatText("ZONE CHANGE DETECTED! Mode switched: SLAVE!", 6);
                        }
                    }
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }
    }
}
