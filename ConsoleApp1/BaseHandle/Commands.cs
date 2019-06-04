using Decal.Adapter;
using Defiance.LogHandle;
using Defiance.Utils;
using System;
using System.Reflection;

namespace Defiance.BaseHandle
{
    class Commands
    {
        public void Dispose()
        {
            lib.MyCore.CommandLineText -= Core_CommandLineText;
            lib.CommandsInstance--;
        }

        public void InitCommands(PluginCore PGCore)
        {
            try
            {
                Core = PGCore;
                if (lib.CommandsInstance < 1)
                {
                    lib.MyCore.CommandLineText += Core_CommandLineText;
                    lib.CommandsInstance++;
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        private void Core_CommandLineText(object sender, ChatParserInterceptEventArgs e)
        {
            try
            {
                if (lib.status > 0 && e.Text.Equals("/dump", StringComparison.OrdinalIgnoreCase))
                {
                    int currentSelection = CoreManager.Current.Actions.CurrentSelection;
                    if (currentSelection != 0)
                    {
                        Repo.DumpWorldObject(lib.MyCore.WorldFilter[currentSelection]);
                    }
                    else
                    {
                        Utility.AddChatText("No object selected.",6);
                    }
                    e.Eat = true;
                }

                if (e.Text.Equals("/ignorevp", StringComparison.OrdinalIgnoreCase))
                {
                    if (lib.DFTEXT != null)
                    {
                        lib.DFTEXT.Visible = false;
                        lib.DFTEXT.Dispose();
                        lib.DFTEXT = null;
                    }
                    Sounds.DeadVP.Stop();
                    Vitae.Dispose();
                    lib.vpcounter = 0;
                    Utility.AddChatText("10% VP Warning ignored until the next death.", 6);
                    e.Eat = true;
                }
          
                if (e.Text.Equals("/vitae", StringComparison.OrdinalIgnoreCase))
                {
                    if (lib.DFTEXT != null)
                    {
                        lib.DFTEXT.Visible = false;
                        lib.DFTEXT.Dispose();
                        lib.DFTEXT = null;
                    }
                    Sounds.DeadVP.Stop();
                    Vitae.Dispose();
                    lib.vpcounter = 0;
                    Utility.AddChatText("10% VP Warning ignored until the next death.", 6);
                    e.Eat = true;
                }

                if (lib.status > 0 && e.Text.Equals("/defiance help", StringComparison.OrdinalIgnoreCase))
                {
                    Utility.AddChatText("--- Welcome to the [DEFIANCE] Help Menu ---", 6);
                    Utility.AddChatText("", 0);
                    Utility.AddChatText("Use these commands to navigate the Defiance Help Menu ", 0);
                    Utility.AddChatText("- /defiance functions - Displays a list of the passive functions", 0);
                    Utility.AddChatText("- /defiance commands  - Displays a list of available commands.", 0);
                    Utility.AddChatText("- /defiance modes     - Displays and describes Modes.", 0);
                    Utility.AddChatText("- /defiance options   - Displays and describes Advanced Options.", 0);
                    Utility.AddChatText("- /defiance buttons   - Describes the Main HUD Buttons.", 0);
                    Utility.AddChatText("- /defiance tabs      - Explains the way different Tabs work.", 0);
                    Utility.AddChatText("- /defiance retry     - Manually retry the authorization process.", 0);
                    e.Eat = true;
                }

                if (lib.status > 0 && e.Text.Equals("/defiance functions", StringComparison.OrdinalIgnoreCase))
                {
                    Utility.AddChatText("--- PASSIVE FUNCTIONALITY ---", 6);
                    Utility.AddChatText("", 0);
                    Utility.AddChatText("- Fix for the logging bug, keeping your character in-game when it's trying to log off.", 0);
                    Utility.AddChatText("- Included Incoming Spell Detection (not 100% guaranteed).", 0);
                    Utility.AddChatText("- Internal Broadcast System", 0);
                    Utility.AddChatText("- Split Tab for PK's and Guildies.", 0);
                    Utility.AddChatText("- Internal Loggers and Alerts.", 0);
                    Utility.AddChatText("- Fellowship target calling and visual aid.", 0);
                    Utility.AddChatText("- Resizable (hold Ctrl and drag the corners).", 0);
                    Utility.AddChatText("- Allegiance status changes detection/alert.", 0);
                    Utility.AddChatText("- Shortcuts to level 7/8 vulns and debuffs.", 0);
                    Utility.AddChatText("- Shortcut to <TAG> spell (Magic Yield other 1).", 0);
                    Utility.AddChatText("- Global PK Timer tracker.", 0);
                    Utility.AddChatText("- 10 Second warning on turning PK.", 0);
                    Utility.AddChatText("- 10% Vitae Penalty warning and automatic logout.", 0);
                    Utility.AddChatText("- Tabs selection highlight.", 0);
                    e.Eat = true;
                }

                if (lib.status > 0 && e.Text.Equals("/defiance modes", StringComparison.OrdinalIgnoreCase))
                {
                    Utility.AddChatText("--- MODES EXPLAINED ---", 6);
                    Utility.AddChatText("", 0);
                    Utility.AddChatText("  MODES are a quick way of repurposing [DEFIANCE]. ", 6);
                    Utility.AddChatText("- MASTER: If MASTER mode is checked, Target Calling is enabled, and automatic .", 0);
                    Utility.AddChatText("          fellowship targetting will be disabled.", 0);
                    Utility.AddChatText("- SLAVE:  If SLAVE mode is checked, Target Calling is disabled, and automatic .", 0);
                    Utility.AddChatText("          fellowship targetting will be enabled.", 0);
                    Utility.AddChatText("- SOLO:   If SOLO mode is checked, Target Calling is disabled, and automatic .", 0);
                    Utility.AddChatText("          fellowship targetting will be disabled.", 0);
                    Utility.AddChatText("- MACRO:  If MACRO mode is checked, [DEFIANCE] will start vTank and begin macroing.", 0);
                    Utility.AddChatText("          It will use internal loggers to keep your character safe.", 0);
                    e.Eat = true;
                }

                if (lib.status > 0 && e.Text.Equals("/defiance commands", StringComparison.OrdinalIgnoreCase))
                {
                    Utility.AddChatText("--- DEFIANCE CHAT COMMANDS ---", 6);
                    Utility.AddChatText("", 0);
                    Utility.AddChatText("- /dump - Dump currently selected object info to chat window and XML file.", 0);
                    Utility.AddChatText("- /ignorevp or /vitae - Will force [DEFIANCE] to ignore the 10% vitae alert until the next relog.", 0);
                    Utility.AddChatText("- /defiance version - Will post current version of [DEFIANCE].", 0);
                    e.Eat = true;
                }

                if (lib.status > 0 && e.Text.Equals("/defiance buttons", StringComparison.OrdinalIgnoreCase))
                {
                    Utility.AddChatText("--- DEFIANCE BUTTONS ---", 6);
                    Utility.AddChatText("", 0);
                    Utility.AddChatText("[DIE]  - Will instantly kill your character. Enabling required.", 0);
                    Utility.AddChatText("[VP]   - Will force [DEFIANCE] to ignore the 10% vitae alert until the next relog.", 0);
                    Utility.AddChatText("[FIX]  - Will force [DEFIANCE] to run the /fixbusy command.", 0);
                    Utility.AddChatText("[HELP] - Will call for reinforcements at current coords in Allegiance chat", 0);
                    e.Eat = true;
                }
                if (lib.status > 0 && e.Text.Equals("/defiance tabs", StringComparison.OrdinalIgnoreCase))
                {
                    Utility.AddChatText("--- DEFIANCE TABS ---", 6);
                    Utility.AddChatText("", 0);
                    Utility.AddChatText(" - Clicking on a GREEN Player's WEAPON in the lists selects that player.", 0);
                    Utility.AddChatText("- Clicking on a RED Player's WEAPON in the lists debuffs that player's weapon.", 0);
                    Utility.AddChatText("-             If Meelee Weapon - Clouded Motives.", 0);
                    Utility.AddChatText("-             If Bow or Wand - Wi's Folly", 0);
                    Utility.AddChatText("- Clicking on GREEN names automatically tosses the target a lvl 7 Heal Other.", 0);
                    Utility.AddChatText("- Dark GREEN name means target is outside casting range.", 0);
                    Utility.AddChatText("- Clicking on RED names calls fellowship target / selects target, depending on Mode.", 0);
                    Utility.AddChatText("- Dark RED name means target is outside casting range.", 0);
                    Utility.AddChatText("- My LOC - Will force [DEFIANCE] to post current coords and heading in Allegiance chat", 0);
                    Utility.AddChatText("- Clicking on a name in the TIMERS tab will display PK Timer and Killer in Allegiance chat", 0);
                    e.Eat = true;
                }

                if (e.Text.Equals("/defiance options", StringComparison.OrdinalIgnoreCase))
                {
                    Utility.AddChatText("--- DEFIANCE ADVANCED OPTIONS ---", 6);
                    Utility.AddChatText("", 0);
                    Utility.AddChatText("- ELEMENT: The element used in Behaviour actions.", 0);
                    Utility.AddChatText("- BEHAVIOUR: The actionset it uses in MACRO mode when NOT logged by an Enemy.", 0);
                    Utility.AddChatText("- Min. Enemies: The minimum number of enemies required to initiate the logger.", 0);
                    Utility.AddChatText("- Min. Enemy Level: The minimum level an enemy needs to be to initiate the logger.", 0);
                    Utility.AddChatText("- Relog Timer: The number of seconds before the logger logs back in. ", 0);
                    Utility.AddChatText("- Min. Tapers: The minimum number of tapers needed to initiate the logger.", 0);
                    Utility.AddChatText("- Min. Slots: The minimum number of empty inventory slots needed to initiate the logger.", 0);
                    Utility.AddChatText("- Movement detect: A smart way of going against other Loggers.", 0);
                    Utility.AddChatText("- Long Cycle: A smart way of changing the logger Timer if logged off too often.", 0);
                    Utility.AddChatText("- Enable Die Button: Enabled the DIE button function.", 0);
                    Utility.AddChatText("- Ticker Rate: The rate at which it refreshes all Player-related information.", 0);
                    Utility.AddChatText("-        Lowering this number will make readings more accurate, but take more  ", 0);
                    Utility.AddChatText("-        processing power. However, increasing it WILL get you killed.", 0);
                    Utility.AddChatText("-        Set this according to your computer specs, but Recommended value", 0);
                    Utility.AddChatText("         will work fine for most computers.", 0);
                    e.Eat = true;
                }

                if (e.Text.Equals("/defiance version", StringComparison.OrdinalIgnoreCase))
                {
                    Utility.AddChatText("You are currently running version: " + Assembly.GetExecutingAssembly().GetName().Version.ToString(), 6);
                    e.Eat = true;
                }

                if (e.Text.Equals("/defiance retry", StringComparison.OrdinalIgnoreCase))
                {
                    Core.Core_Login_Retry();
                    Utility.AddChatText("Retrying to authorize. Standby!", 6);
                    e.Eat = true;
                }

                if (e.Text.Equals("/defiance instance", StringComparison.OrdinalIgnoreCase))
                {
                    Utility.AddChatText("PluginInstance " + lib.PluginInstance, 6);
                    Utility.AddChatText("CommandsInstance " + lib.CommandsInstance, 6);
                    Utility.AddChatText("SoundInstance " + lib.SoundInstance, 6);
                    Utility.AddChatText("AuthInstance " + lib.AuthInstance, 6);
                    Utility.AddChatText("ScannerInstance " + lib.ScannerInstance, 6);
                    Utility.AddChatText("ViewInstance " + lib.ViewInstance, 6);
                    Utility.AddChatText("ViewControlInstance " + lib.ViewControlInstance, 6);
                    Utility.AddChatText("DeathParseInstance " + lib.DeathParseInstance, 6);
                    Utility.AddChatText("LoggingInstance " + lib.DeathParseInstance, 6);
                    Utility.AddChatText("RedTimerInstance " + lib.RedTimerInstance, 6);
                    Utility.AddChatText("DeathInstance " + lib.DeathInstance, 6);
                    Utility.AddChatText("VitaeInstance " + lib.VitaeInstance, 6);
                    Utility.AddChatText("CompsInstance " + lib.CompsInstance, 6);
                    Utility.AddChatText("InventoryInstance " + lib.InventoryInstance, 6);
                    Utility.AddChatText("DetectionIstance " + lib.DetectionIstance, 6);
                    Utility.AddChatText("MacroInstance " + lib.MacroInstance, 6);
                    Utility.AddChatText("RemoteInstance " + lib.RemoteInstance, 6);
                    Utility.AddChatText("RemoteLogInstance " + lib.RemoteLogInstance, 6);
                    Utility.AddChatText("VersInstance " + lib.VersInstance, 6);
                    Utility.AddChatText("HUDInstance " + lib.HUDInstance, 6);
                    Utility.AddChatText("ReportInstance " + lib.ReportInstance, 6);
                    e.Eat = true;
                }

                if (e.Text.Equals("/defiance status", StringComparison.OrdinalIgnoreCase))
                {
                    Utility.AddChatText("Current game status: " + lib.gameStatus, 6);
                    e.Eat = true;
                }

            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        PluginCore Core;
    }
}
