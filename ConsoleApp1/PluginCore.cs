using Decal.Adapter;
using Defiance.BaseHandle;
using Defiance.DeathHandle;
using Defiance.HUDHandle;
using Defiance.LogHandle;
using Defiance.MacroHandle;
using Defiance.Scanning;
using Defiance.Utils;
using Defiance.Views;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace Defiance
{
    [FriendlyName("Defiance v2")]
    public sealed class PluginCore : PluginBase
    {
        internal static string PluginName = "Defiance v2";

        public static Timer ShrtTmr = new Timer();
        public static Timer LngTmr = new Timer();
        public static Timer RlgTmr = new Timer();
        public static Timer LogTmr = new Timer();

        protected override void Startup()
        {
            try
            {
                lib.MyHost = Host;
                lib.MyCore = Core;
                lib.reason = "user";
                PGCore = this;
                lib.gameStatus = 1;

                AppDomain currentDomain = AppDomain.CurrentDomain;
                Assembly[] assemblies = currentDomain.GetAssemblies();
                foreach (Assembly assembly in assemblies)
                {
                    if (assembly.FullName.Contains("uTank2"))
                    {
                        lib.vtank = true;
                    }
                }

                lib.MyCore.CharacterFilter.Login += Core_Login_Init;
                lib.PluginInstance++;
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        protected override void Shutdown()
        {
            try
            {
                lib.gameStatus = 0;
                if (lib.status == 0)
                {
                    Utility.WindowText("Asheron's Call");
                }
                else if (lib.Autorelog == false)
                {
                    Utility.AddWindowText(lib.MyServer + " : " + lib.MyName + " logged off by " + lib.reason + " at " + DateTime.Now.ToString("h:mm:ss tt"));
                    if (lib.UseMacroLogic == true && lib.reason != "user") { Utility.ActivateWindow(); }
                }
                else if (lib.Autorelog == true && lib.longcycle == false)
                {
                    Utility.AddWindowText(lib.MyServer + " : " + lib.MyName + " logged off by " + lib.reason + " at " + DateTime.Now.ToString("h:mm:ss tt") + ". Relogger is set to " + lib.Timer + " seconds. Counter: " + lib.relogcounter);
                    if (lib.UseMacroLogic == true) { Utility.ActivateWindow(); }
                }
                else if (lib.Autorelog == true && lib.longcycle == true)
                {
                    Utility.AddWindowText(lib.MyServer + " : " + lib.MyName + " logged off by " + lib.reason + " at " + DateTime.Now.ToString("h:mm:ss tt") + ". Relogger is on Long Cycle (10 minutes).");
                    lib.longcycle = false;
                    if (lib.UseMacroLogic == true) { Utility.ActivateWindow(); }
                }

                lib.MyCore.CharacterFilter.Login -= Core_Login_Init;

                if (lib.AuthInstance > 0) ATH.Dispose();
                if (lib.CommandsInstance > 0) CMD.Dispose();
                if (lib.LoggingInstance > 0) LGM.Dispose();
                if (lib.ViewControlInstance > 0) VCT.Dispose();
                if (lib.ScannerInstance > 0) SCN.Dispose();
                if (lib.DeathParseInstance > 0) DTH.Dispose();
                if (lib.SoundInstance > 0) SND.Dispose();
                if (lib.MacroInstance > 0) MCL.Dispose();
                if (lib.VersInstance > 0) VSC.Dispose();
                if (lib.HUDInstance > 0) HUD.Dispose();
                if (lib.ReportInstance > 0) REP.Dispose();

                if (SCN != null) SCN = null;
                if (SND != null) SND = null;
                if (ATH != null) ATH = null;
                if (VCT != null) VCT = null;
                if (DTH != null) DTH = null;
                if (CMD != null) CMD = null;
                if (VSC != null) VSC = null;
                if (LGM != null) LGM = null;
                if (MCL != null) MCL = null;
                if (HUD != null) HUD = null;
                if (REP != null) REP = null;

                if (ShrtTmr.Enabled == true) { ShrtTmr.Stop(); ShrtTmr.Dispose(); }
                if (LngTmr.Enabled == true) { LngTmr.Stop(); LngTmr.Dispose(); }
                if (LogTmr.Enabled == true) { LogTmr.Stop(); LogTmr.Dispose(); }

                PGCore = null;
                lib.PluginInstance--;
                lib.Dispose();
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        private void Core_Login_Init(object sender, EventArgs e)
        {
            try
            {
                lib.gameStatus = 2;
                if (lib.PluginInstance == 1)
                {
                    Utility.AddChatText("-- Initializing Defiance v2 --", 6);
                    CMD = new Commands();
                    CMD.InitCommands(PGCore);
                    Core_Login_Check();
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void Core_Login_Check()
        {
            lib.MyName = CoreManager.Current.CharacterFilter.Name;
            lib.MyServer = CoreManager.Current.CharacterFilter.Server;
            lib.MyID = CoreManager.Current.CharacterFilter.Id;

            if (lib.MyName != null)
            {
                ATH = new Auth();
                ATH.Initial(PGCore);
                if (lib.status > 0)
                {
                    Repo.RepoInit();
                    Core_Login_Passed();
                    if (LngTmr.Enabled != true) { LngTmr.Interval = 1000; LngTmr.Start(); }
                    if (ShrtTmr.Enabled != true) { lib.Ticker = 100; ShrtTmr.Interval = lib.Ticker; ShrtTmr.Start(); }
                    if (LogTmr.Enabled != true) { LogTmr.Interval = 60000; LogTmr.Start(); }
                }
            }
        }

        public void Core_Login_Retry()
        {
            try
            {
                if (lib.PluginInstance == 1)
                {
                    if (lib.DFTEXT != null) { lib.DFTEXT.Dispose(); lib.DFTEXT = null; }
                    if (lib.DF2TEXT != null) { lib.DF2TEXT.Dispose(); lib.DF2TEXT = null; }
                    if (lib.AuthInstance > 0) ATH.Dispose();
                    if (lib.CommandsInstance > 0) CMD.Dispose();
                    if (lib.LoggingInstance > 0) LGM.Dispose();
                    if (lib.ViewControlInstance > 0) VCT.Dispose();
                    if (lib.ScannerInstance > 0) SCN.Dispose();
                    if (lib.DeathParseInstance > 0) DTH.Dispose();
                    if (lib.SoundInstance > 0) SND.Dispose();
                    if (lib.MacroInstance > 0) MCL.Dispose();
                    if (lib.VersInstance > 0) VSC.Dispose();
                    if (lib.HUDInstance > 0) HUD.Dispose();
                    if (lib.ReportInstance > 0) REP.Dispose();

                    if (SCN != null) SCN = null;
                    if (SND != null) SND = null;
                    if (ATH != null) ATH = null;
                    if (VCT != null) VCT = null;
                    if (DTH != null) DTH = null;
                    if (CMD != null) CMD = null;
                    if (VSC != null) VSC = null;
                    if (LGM != null) LGM = null;
                    if (MCL != null) MCL = null;
                    if (HUD != null) HUD = null;
                    if (REP != null) REP = null;

                    CMD = new Commands();
                    CMD.InitCommands(PGCore);
                    Core_Login_Check();
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }


        private void Core_Login_Passed()
        {
            try
            {
                VSC = new VersionControl(); VSC.InitVersCheck();
                SND = new Sounds(); SND.SoundsInit();
                SCT = new SettingsControl(); SCT.SettingsInit();
                VCT = new ViewControl(); VCT.ViewInit();
                SCN = new Scanner(); SCN.ScannerInit();
                MCL = new MacroLogic(); MCL.MacroInit();
                LGM = new LogMethod(); LGM.LogInit();
                DTH = new DeathParse(); DTH.ParseInit();
                HUD = new HUDControl(); HUD.HUDInit();
       
                if (lib.Autorelog == true)
                {
                    if (RlgTmr.Enabled == true)
                    {
                        RlgTmr.Stop();
                        lib.Autorelog = false;
                        MainView.Mode.Current = 0;
                        lib.Mode = 0;
                        Utility.AddWindowText(lib.MyServer + " : " + lib.MyName + " : " + lib.authtype + " : Current mode: SLAVE - Forced Login detected at " + DateTime.Now.ToString("h: mm:ss tt"));
                        Utility.AddChatText("FORCED LOGIN DETECTED! Mode switched: SLAVE.", 6);
                    }
                    else if (RlgTmr.Enabled == false)
                    {
                        MainView.Mode.Current = 3;
                        lib.Mode = 3;
                        Utility.AddWindowText(lib.MyServer + " : " + lib.MyName + " : " + lib.authtype + " : Current mode: MACRO - Relogger is set to " + lib.Timer + " seconds.");
                        if (lib.UseMacroLogic == true) { Utility.ActivateWindow(); }
                    }
                }
                else if (lib.Autorelog == false)
                {
                    MainView.Mode.Current = 0;
                    lib.Mode = 0;
                    Utility.AddWindowText(lib.MyServer + " : " + lib.MyName + " : " + lib.authtype + " : Current mode: SLAVE");
                }

                REP = new Report(); REP.ReportInit();
                Report.InitialLog("Login");
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        PluginCore PGCore;
        Auth ATH;
        ViewControl VCT;
        Sounds SND;
        Scanner SCN;
        DeathParse DTH;
        Commands CMD;
        SettingsControl SCT;
        VersionControl VSC;
        LogMethod LGM;
        MacroLogic MCL;
        HUDControl HUD;
        Report REP;
    }
}