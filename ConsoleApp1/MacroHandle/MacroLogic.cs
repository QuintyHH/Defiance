using Decal.Adapter;
using Defiance.Utils;
using System;

namespace Defiance.MacroHandle
{
    class MacroLogic
    {
        public void MacroInit()
        {
            if (lib.MacroInstance < 1)
            {
                lib.MyCore.EchoFilter.ServerDispatch += FailDetect.ServerDispatch_CastFilter;
                lib.MyCore.ChatBoxMessage += Targetting.Core_RemoteTarget;
                lib.MyCore.ChatBoxMessage += RecallDetect.RecallDetection;
                lib.MyCore.CharacterFilter.SpellCast += FailDetect.FailDetection2;
                lib.MyCore.CharacterFilter.LoginComplete += Core_Login_Complete;
                if (lib.gameStatus == 3)
                {
                    Core_Macro_Recheck();
                }
                lib.MacroInstance++;
            }
        }

        public void Dispose()
        {
            lib.MyCore.EchoFilter.ServerDispatch -= FailDetect.ServerDispatch_CastFilter;
            lib.MyCore.ChatBoxMessage -= Targetting.Core_RemoteTarget;
            lib.MyCore.ChatBoxMessage -= RecallDetect.RecallDetection;
            lib.MyCore.CharacterFilter.SpellCast -= FailDetect.FailDetection2;
            lib.MyCore.CharacterFilter.LoginComplete -= Core_Login_Complete;
            lib.MacroInstance--;
        }

        public void Core_Login_Complete(object sender, EventArgs e)
        {
            lib.gameStatus = 3;
            if (lib.vtank == false)
            {
                Utility.AddChatText("Virindi Tank is not running. Macroing limited.", 6);
            }

            if (lib.Mode == 3 && lib.vtank == true && lib.status > 0)
            {
                Utility.DispatchChatToBoxWithPluginIntercept("/vt start");
            }
        }

        public void Core_Macro_Recheck()
        {
            if (lib.gameStatus == 3)
            {
                if (lib.Mode == 3 && lib.vtank == true && lib.status > 0)
                {
                    Utility.DispatchChatToBoxWithPluginIntercept("/vt start");
                }
            }
        }
    }
}
