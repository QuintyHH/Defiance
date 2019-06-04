using Decal.Adapter;
using Decal.Adapter.Wrappers;
using Defiance.Utils;
using System;

namespace Defiance.MacroHandle
{
    class FailDetect
    {
        public static void ServerDispatch_CastFilter(object sender, NetworkMessageEventArgs e)
        {
            try
            {
                if (lib.UseMacroLogic == true && lib.Mode == 3)
                {
                    if (e.Message.Type == 0xF74C && (int)e.Message["object"] == lib.MyID)
                    {
                        lib.CurrentStance = (Int16)e.Message["stance"];
                    }
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public static void FailDetection2(object sender, EventArgs e)
        {
            try
            {
                if (lib.UseMacroLogic == true && lib.Mode == 3)
                {
                    if (lib.CurrentStance == 61)
                    {
                        lib.WrongStanceCounter++;
                    }
                    else
                    {
                        lib.WrongStanceCounter = 0;
                    }

                    if (lib.WrongStanceCounter > 1)
                    {
                        StanceFix();
                    }
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        private static void StanceFix()
        {
            Utility.AddChatText("Detected cast failure, fixing!", 6);
            lib.MyCore.Actions.SetCombatMode(CombatState.Peace);
            PluginCore.LngTmr.Tick += StanceFix2;
        }

        private static void StanceFix2(object sender, EventArgs e)
        {
            if (lib.FailCounter < 3)
            {
                lib.FailCounter++;
            }
            else
            {
                lib.FailCounter = 0;
                PluginCore.LngTmr.Tick -= StanceFix2;
                lib.MyCore.Actions.SetCombatMode(CombatState.Magic);
            }
        }
    }
}
