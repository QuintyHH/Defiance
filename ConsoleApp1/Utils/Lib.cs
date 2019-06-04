using Decal.Adapter;
using Decal.Adapter.Wrappers;
using Defiance.CollectionHandle;
using System.Collections.Generic;

namespace Defiance.Utils
{
    class lib
    {
        //Instance Tracker
        public static int PluginInstance;
        public static int CommandsInstance;
        public static int SoundInstance;
        public static int AuthInstance;
        public static int ScannerInstance;
        public static int ViewInstance;
        public static int ViewControlInstance;
        public static int DeathParseInstance;
        public static int LoggingInstance;
        public static int RedTimerInstance;
        public static int DeathInstance;
        public static int VitaeInstance;
        public static int CompsInstance;
        public static int InventoryInstance;
        public static int DetectionIstance;
        public static int MacroInstance;
        public static int RemoteInstance;
        public static int RemoteLogInstance;
        public static int VersInstance;
        public static int HUDInstance;
        public static int ReportInstance;

        //FailCorrection
        public static int CurrentStance;
        public static int WrongStanceCounter;
        public static int FailCounter;

        //Base
        public static string authurl = "http://rustasylum.net/kos/userauth.json";
        public static string landblocks = "http://rustasylum.net/kos/landblocks.json";
        public static string Logger;
        public static string log = "/w ";

        public static bool versid;
        public static int moncheck;

        public static bool AdminAuth;
        public static bool UserAuth;
        public static string authtype;

        public static string AuthIds;
        public static string LocKey;
        public static int reauthcounter;
        public static int status;
        public static int gameStatus;

        // gameStatus = 0 : Character Select Screen
        // gameStatus = 1 : Startup
        // gameStatus = 2 : OnLogin
        // gameStatus = 3 : OnLoginComplete
        // gameStatus = 4 : Logoff
        
        public static PluginHost MyHost;
        public static CoreManager MyCore;

        public static List<int> LogList;
        public static List<string> OnDowntime;
        public static List<string> OnDowntime2;
        public static List<int> DistanceCheck;

        public static string MyName;
        public static string MyServer;
        public static int MyID;

        //Loggers
        public static bool Autorelog;
        public static int relogcounter;
        public static int logoffcounter;
        public static bool longcycle;
        public static bool logging;
        public static int vpcounter;

        //Scanner
        public static D3DObj PointArrow;
        public static D3DObj DFTEXT;
        public static D3DObj DF2TEXT;
        public static int LastSelected;
        public static Enemy[] Enemies;
        public static Friend[] Friends;

        //Log
        public static string reason;

        //Settings
        public static int Ticker;
        public static int Range;
        public static int Timer;
        public static int Comps;
        public static int LastID;
        public static int Slots;

        public static int Width;
        public static int Height;
        public static int X;
        public static int Y;

        public static int warspell;
        public static int streak;
        public static int vuln;

        //Buttons
        public static bool Flicker;
        public static bool UseAlertOdds;
        public static bool UseAlertDT;
        public static bool UseAlertPKT;
        public static bool UseAlertBc;
        public static bool UseAlertDA;
        public static bool UseAlertPA;
        public static bool UseAlertPF;
        public static bool UseAlertLogP;
        public static bool UseAlertSPK;
        public static bool UseAlertSS;
        public static bool UseAlertLS;
        public static bool UseMacroLogic;
        public static bool UseLogDie;

        public static int Element;
        public static int Behaviour;
        public static int Mode;

        //Assemblies
        public static bool vtank;

        public static void Dispose()
        {

            RemoteLogInstance = 0;
            versid = false;
            moncheck = 0;
            authtype = null;

            AdminAuth = false;
            UserAuth = false;
            Logger = null;

            AuthIds = null;
            LocKey = null;
            status = 0;

            MyHost = null;
            MyCore = null;

            OnDowntime = null;
            OnDowntime2 = null;
            DistanceCheck = null;

            MyName = null;
            MyServer = null;
            MyID = 0;

            DFTEXT = null;
            DF2TEXT = null;
            PointArrow = null;
            LastSelected = 0;
            CurrentStance = 0;
            WrongStanceCounter = 0;

            Enemies = null;
            Friends = null;

            vpcounter = 0;
            reason = null;
            logging = false;

            CurrentStance = 0;
            WrongStanceCounter = 0;
            FailCounter = 0;

            Ticker = 0;
            Range = 0;
            Timer = 0;
            Comps = 0;
            logoffcounter = 0;
            LastID = 0;
            Slots = 0;

            Width = 0;
            Height = 0;
            X = 0;
            Y = 0;

            warspell = 0;
            streak = 0;
            vuln = 0;

            Element = 0;
            Mode = 0;
            Behaviour = 0;

            vtank = false;
        }
    }
}
