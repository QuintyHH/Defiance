using Decal.Adapter;
using Defiance.BaseHandle;
using Defiance.Utils;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using VirindiViewService;
using VirindiViewService.Controls;
using VirindiViewService.XMLParsers;

namespace Defiance.Views
{
    public class MainView
    {
        public void CreateView()
        {
            try
            {
                if (lib.ViewInstance < 1)
                {
                    UI ui = Repo.GetUI(CoreManager.Current.CharacterFilter.Server, CoreManager.Current.CharacterFilter.Name);

                    Decal3XMLParser decal3XMLParser = new Decal3XMLParser();
                    decal3XMLParser.ParseFromResource("Defiance.Views.MainView.xml", out properties, out controls);

                    Assembly assembly = lib.MyCore.GetType().Assembly;
                    properties.Title = GetPluginName();
                    properties.Icon = GetIcon();
                    View = new HudView(properties, controls);

                    Wrapper = (View.Controls["Wrapper"] as HudFixedLayout);
                    Wrapper2 = (View.Controls["Wrapper"] as HudFixedLayout);
                    Tabs = (View.Controls["Tabs"] as HudTabView);

                    DefinedLayout = (View.Controls["DefinedLayout"] as HudFixedLayout);
                    CorpseList = (View.Controls["CorpseList"] as HudList);
                    CorpseLayout = (View.Controls["CorpseLayout"] as HudFixedLayout);
                    PortalList = (View.Controls["PortalList"] as HudList);
                    PortalLayout = (View.Controls["PortalLayout"] as HudFixedLayout);
                    GuildList = (View.Controls["GuildList"] as HudList);
                    PlayerList = (View.Controls["PlayerList"] as HudList);
                    PlayerLayout = (View.Controls["PlayerLayout"] as HudFixedLayout);
                    FriendsList = (View.Controls["FriendsList"] as HudList);
                    EnemiesList = (View.Controls["EnemiesList"] as HudList);
                    TimerList = (View.Controls["TimerList"] as HudList);
                    AdminLayout = (View.Controls["AdminLayout"] as HudFixedLayout);
                    LogoutLayout = (View.Controls["LogoutLayout"] as HudFixedLayout);
                    AlertLayout = (View.Controls["AlertLayout"] as HudFixedLayout);
                    SentryLayout = (View.Controls["SentryLayout"] as HudFixedLayout);

                    ModeLbl = (View.Controls["ModeLbl"] as HudStaticText);
                    Mode = (View.Controls["Modes"] as HudCombo);
                    ElementLbl = (View.Controls["ElementLbl"] as HudStaticText);
                    Element = (View.Controls["Element"] as HudCombo);
                    BehaviourLbl = (View.Controls["BehaviourLbl"] as HudStaticText);
                    Behaviour = (View.Controls["Behaviour"] as HudCombo);
                    AdminNote = (View.Controls["AdminNote"] as HudStaticText);
                    ForcelogNote = (View.Controls["ForcelogNote"] as HudStaticText);
                    ForcelogBox = (View.Controls["ForcelogBox"] as HudTextBox);
                    Range = (View.Controls["Range"] as HudTextBox);
                    Timer = (View.Controls["Timer"] as HudTextBox);
                    Ticker = (View.Controls["Ticker"] as HudTextBox);
                    Slots = (View.Controls["Slots"] as HudTextBox);
                    Comps = (View.Controls["Comps"] as HudTextBox);
                    Friend = (View.Controls["Friend"] as HudTextBox);
                    Enemy = (View.Controls["Enemy"] as HudTextBox);
                    ForcelogButton = (View.Controls["ForcelogButton"] as HudButton);
                    ForceRelogButton = (View.Controls["ForceRelogButton"] as HudButton);
                    ForcelocButton = (View.Controls["ForcelocButton"] as HudButton);
                    ForcedieButton = (View.Controls["ForcedieButton"] as HudButton);
        
                    AddFriend = (View.Controls["AddFriend"] as HudButton);
                    AddEnemy = (View.Controls["AddEnemy"] as HudButton);

                    Tag = (View.Controls["Tag"] as HudButton);
                    Imperil = (View.Controls["Imperil"] as HudButton);
                    Bludge = (View.Controls["Bludge"] as HudButton);
                    Slash = (View.Controls["Slash"] as HudButton);
                    Pierce = (View.Controls["Pierce"] as HudButton);
                    Light = (View.Controls["Light"] as HudButton);
                    Acid = (View.Controls["Acid"] as HudButton);
                    Fire = (View.Controls["Fire"] as HudButton);
                    Cold = (View.Controls["Cold"] as HudButton);
                    IgnoreVP = (View.Controls["IgnoreVP"] as HudButton);
                    Fixbusy = (View.Controls["Fixbusy"] as HudButton);
                    Loc = (View.Controls["Loc"] as HudButton);
                    Die = (View.Controls["Die"] as HudButton);
                    SaveSettings2 = (View.Controls["SaveSettings2"] as HudButton);
                    SaveSettings3 = (View.Controls["SaveSettings3"] as HudButton);

                    ResetAlarms = (View.Controls["ResetAlarms"] as HudButton);
                    ResetOptions = (View.Controls["ResetOptions"] as HudButton);

                    Strength = (View.Controls["Strength"] as HudButton);
                    Endurance = (View.Controls["Endurance"] as HudButton);
                    Coordination = (View.Controls["Coordination"] as HudButton);
                    Quickness = (View.Controls["Quickness"] as HudButton);
                    Focus = (View.Controls["Focus"] as HudButton);
                    Willpower = (View.Controls["Willpower"] as HudButton);
                    Run = (View.Controls["Run"] as HudButton);
                    Creature = (View.Controls["Creature"] as HudButton);
                    Life = (View.Controls["Life"] as HudButton);
                    War = (View.Controls["War"] as HudButton);
                    MagicD = (View.Controls["MagicD"] as HudButton);
                    HeavyW = (View.Controls["HeavyW"] as HudButton);
                    MelD = (View.Controls["MelD"] as HudButton);
                    MissW = (View.Controls["MissW"] as HudButton);
                    MissD = (View.Controls["MissD"] as HudButton);
                    Stam = (View.Controls["Stam"] as HudButton);
                    Harm = (View.Controls["Harm"] as HudButton);
                    StamRegen = (View.Controls["StamRegen"] as HudButton);

                    AlertOdds = (View.Controls["AlertOdds"] as HudCheckBox);
                    AlertPKT = (View.Controls["AlertPKT"] as HudCheckBox);
                    AlertDT = (View.Controls["AlertDT"] as HudCheckBox);
                    AlertBc = (View.Controls["AlertBc"] as HudCheckBox);
                    AlertPA = (View.Controls["AlertPA"] as HudCheckBox);
                    AlertPF = (View.Controls["AlertPF"] as HudCheckBox);
                    AlertLogP = (View.Controls["AlertLogP"] as HudCheckBox);
                    AlertDA = (View.Controls["AlertDA"] as HudCheckBox);
                    AlertSPK = (View.Controls["AlertSPK"] as HudCheckBox);
                    AlertSS = (View.Controls["AlertSS"] as HudCheckBox);
                    UseMacroLogic = (View.Controls["MacroLogic"] as HudCheckBox);
                    UseLogDie = (View.Controls["LogDie"] as HudCheckBox);
                    AlertLS = (View.Controls["AlertLS"] as HudCheckBox);

                    RangeNote = (View.Controls["RangeNote"] as HudStaticText);
                    TimerNote = (View.Controls["TimerNote"] as HudStaticText);
                    TickerNote = (View.Controls["TickerNote"] as HudStaticText);
                    TickerNote2 = (View.Controls["TickerNote2"] as HudStaticText);
                    SlotsNote = (View.Controls["SlotsNote"] as HudStaticText);
                    TapersNote = (View.Controls["TapersNote"] as HudStaticText);
                    FriendsNote = (View.Controls["FriendsNote"] as HudStaticText);
                    EnemiesNote = (View.Controls["EnemiesNote"] as HudStaticText);
                    TextNote = (View.Controls["TextNote"] as HudStaticText);
                    AudioNote = (View.Controls["AudioNote"] as HudStaticText);
                    VersusNote = (View.Controls["VersusNote"] as HudStaticText);
                    GuildNote = (View.Controls["GuildNote"] as HudStaticText);
                    EnemyNote = (View.Controls["EnemyNote"] as HudStaticText);

                    View.UserResizeable = true;
                    View.MinimumClientArea = new Size(330, 280);
                    
                    EnemyNote.TextColor = Color.Red;
                    GuildNote.TextColor = Color.LawnGreen;
                    VersusNote.TextAlignment = WriteTextFormats.Center;
                    EnemiesNote.TextAlignment = WriteTextFormats.Center;
                    GuildNote.TextAlignment = WriteTextFormats.Center; 
                    
                    AlertDT.Checked = ui.UseAlertDT;
                    AlertPKT.Checked = ui.UseAlertPKT;
                    AlertLS.Checked = ui.UseAlertLS;
                    AlertPA.Checked = ui.UseAlertPA;
                    AlertPF.Checked = ui.UseAlertPF;
                    AlertLogP.Checked = ui.UseAlertLogP;
                    AlertDA.Checked = ui.UseAlertDA;
                    AlertSPK.Checked = ui.UseAlertSPK;
                    AlertSS.Checked = ui.UseAlertSS;
                    AlertOdds.Checked = ui.UseAlertOdds;
                    AlertBc.Checked = ui.UseAlertBc;
                    UseMacroLogic.Checked = ui.UseMacroLogic;
                    Range.Text = ui.Range.ToString();
                    Timer.Text = ui.Timer.ToString();
                    Comps.Text = ui.Comps.ToString();
                    Slots.Text = ui.Slots.ToString();
                    Behaviour.Current = ui.Behaviour;
                    Element.Current = ui.Element;
                    UseLogDie.Checked = ui.UseLogDie;
                    Ticker.Text = ui.Ticker.ToString();

                    Strength.OverlayImage = 100668300;
                    Endurance.OverlayImage = 100668273;
                    Coordination.OverlayImage = 100668268;
                    Quickness.OverlayImage = 100668294;
                    Focus.OverlayImage = 100668277;
                    Willpower.OverlayImage = 100668296;
                    StamRegen.OverlayImage = 100668299;
                    Run.OverlayImage = 100668295;
                    Creature.OverlayImage = 100668358;
                    Life.OverlayImage = 100668337;
                    War.OverlayImage = 100668272;
                    MagicD.OverlayImage = 100668330;
                    HeavyW.OverlayImage = 100692226;
                    MelD.OverlayImage = 100668331;
                    MissW.OverlayImage = 100668266;
                    MissD.OverlayImage = 100669126;

                    if (ui.Width > 0)
                    {
                        View.Width = ui.Width;
                    }
                    if (ui.Height > 0)
                    {
                        View.Height = ui.Height;
                    }
                    if (ui.X >= 0 && ui.Y >= 0)
                    {
                        View.Location = new Point(ui.X, ui.Y);
                    }
                    View.Resize += View_Resize;
                    lib.ViewInstance++;
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        private void View_Resize(object sender, EventArgs e)
        {
            try
            {
                int width = View.Width;
                int height = View.Height;

                Wrapper.SetControlRect(Tabs, new Rectangle(45, 50, width - 45, height - 65));

                DefinedLayout.SetControlRect(TimerList, new Rectangle(0, 0, width - 45, height - 80));
                PlayerLayout.SetControlRect(PlayerList, new Rectangle(0, 0, width - 45, (height - 65) / 2));
                PlayerLayout.SetControlRect(GuildList, new Rectangle(0, (height - 65) / 2, width - 45, (height - 65) / 2));
                LogoutLayout.SetControlRect(FriendsList, new Rectangle(0, 20, (width - 45) / 2, height - 145));
                LogoutLayout.SetControlRect(EnemiesList, new Rectangle((width - 45) / 2, 20, (width - 45) / 2, height - 145));
                CorpseLayout.SetControlRect(CorpseList, new Rectangle(0, 0, width - 45, height - 80));
                PortalLayout.SetControlRect(PortalList, new Rectangle(0, 0, width - 45, height - 80));

                PlayerList.SetColumnWidth(1, width - 215);
                PlayerList.SetColumnWidth(2, 45);
                PlayerList.SetColumnWidth(3, 75);
                PlayerList.SetColumnWidth(4, 0);

                GuildList.SetColumnWidth(1, width - 215);
                GuildList.SetColumnWidth(2, 45);
                GuildList.SetColumnWidth(3, 75);
                GuildList.SetColumnWidth(4, 0);

                TimerList.SetColumnWidth(1, width - 215);
                TimerList.SetColumnWidth(2, 45);
                TimerList.SetColumnWidth(3, 75);
                TimerList.SetColumnWidth(4, 0);

                PortalList.SetColumnWidth(1, width - 215);
                PortalList.SetColumnWidth(2, 45);
                PortalList.SetColumnWidth(3, 75);
                PortalList.SetColumnWidth(4, 0);

                CorpseList.SetColumnWidth(1, width - 215);
                CorpseList.SetColumnWidth(2, 45);
                CorpseList.SetColumnWidth(3, 75);
                CorpseList.SetColumnWidth(4, 0);

                FriendsList.SetColumnWidth(0, (width - 45) / 2 - 45);
                EnemiesList.SetColumnWidth(0, (width - 45) / 2 - 45);

                Wrapper2.SetControlRect(Tabs2, new Rectangle(45, 100, width - 45, height - 100));

                Rectangle controlRect = Wrapper.GetControlRect(VersusNote);
                Wrapper.SetControlRect(VersusNote, new Rectangle(width - 125, 32, 35, 15));
                controlRect = Wrapper.GetControlRect(GuildNote);
                Wrapper.SetControlRect(GuildNote, new Rectangle(width - 205, 32, 70, 15));
                controlRect = Wrapper.GetControlRect(EnemyNote);
                Wrapper.SetControlRect(EnemyNote, new Rectangle(width - 75, 32, 70, 15));

                controlRect = LogoutLayout.GetControlRect(AddFriend);
                LogoutLayout.SetControlRect(AddFriend, new Rectangle((width - 45) / 2 - 40, height - 125, 40, 15));
                controlRect = LogoutLayout.GetControlRect(AddEnemy);
                LogoutLayout.SetControlRect(AddEnemy, new Rectangle(width - 85, height - 125, 40, 15));
                controlRect = LogoutLayout.GetControlRect(Friend);
                LogoutLayout.SetControlRect(Friend, new Rectangle(4, height - 125, (width - 45) / 2 - 45, 15));
                controlRect = LogoutLayout.GetControlRect(Enemy);
                LogoutLayout.SetControlRect(Enemy, new Rectangle((width - 45) / 2 + 5, height - 125, (width - 45) / 2 - 45, 15));

                controlRect = LogoutLayout.GetControlRect(FriendsNote);
                LogoutLayout.SetControlRect(FriendsNote, new Rectangle(5, 0, (width / 2) - 5, 15));
                controlRect = LogoutLayout.GetControlRect(EnemiesNote);
                LogoutLayout.SetControlRect(EnemiesNote, new Rectangle((width / 2) + 5, 0, (width / 2) - 5, 15));

                controlRect = AlertLayout.GetControlRect(AudioNote);
                AlertLayout.SetControlRect(AudioNote, new Rectangle(5, 5, width - 10, 20));
                controlRect = AlertLayout.GetControlRect(AlertSPK);
                AlertLayout.SetControlRect(AlertSPK, new Rectangle(5, 25, ((width - 45) / 2) - 10, 20));
                controlRect = AlertLayout.GetControlRect(AlertSS);
                AlertLayout.SetControlRect(AlertSS, new Rectangle(((width - 45) / 2) + 10, 25, (width / 2) - 10, 20));
                controlRect = AlertLayout.GetControlRect(AlertOdds);
                AlertLayout.SetControlRect(AlertOdds, new Rectangle(5, 45, ((width - 45) / 2) - 10, 20));
                controlRect = AlertLayout.GetControlRect(AlertBc);
                AlertLayout.SetControlRect(AlertBc, new Rectangle(((width - 45) / 2) + 10, 45, (width / 2) - 10, 20));
                controlRect = AlertLayout.GetControlRect(AlertDT);
                AlertLayout.SetControlRect(AlertDT, new Rectangle(5, 65, ((width - 45) / 2) - 10, 20));
                controlRect = AlertLayout.GetControlRect(AlertPKT);
                AlertLayout.SetControlRect(AlertPKT, new Rectangle(((width - 45) / 2) + 10, 65, (width / 2) - 10, 20));

                controlRect = AlertLayout.GetControlRect(TextNote);
                AlertLayout.SetControlRect(TextNote, new Rectangle(5, 85, width - 10, 20));
                controlRect = AlertLayout.GetControlRect(AlertPA);
                AlertLayout.SetControlRect(AlertPA, new Rectangle(5, 105, ((width - 45) / 2) - 10, 20));
                controlRect = AlertLayout.GetControlRect(AlertPF);
                AlertLayout.SetControlRect(AlertPF, new Rectangle(5, 125, ((width - 45) / 2) - 10, 20));
                controlRect = AlertLayout.GetControlRect(AlertLogP);
                AlertLayout.SetControlRect(AlertLogP, new Rectangle(((width - 45) / 2) + 10, 105, ((width - 45) / 2) - 10, controlRect.Height));
                controlRect = AlertLayout.GetControlRect(AlertDA);
                AlertLayout.SetControlRect(AlertDA, new Rectangle(((width - 45) / 2) + 10, 125, ((width - 45) / 2) - 10, controlRect.Height));
                controlRect = AlertLayout.GetControlRect(AlertLS);
                AlertLayout.SetControlRect(AlertLS, new Rectangle(5, 145, ((width - 45) / 2) - 10, 20));

                controlRect = AlertLayout.GetControlRect(SaveSettings2);
                AlertLayout.SetControlRect(SaveSettings2, new Rectangle(0, height - 107, width - 45 - 60, 15));
                controlRect = AlertLayout.GetControlRect(ResetAlarms);
                AlertLayout.SetControlRect(ResetAlarms, new Rectangle(width - 45 - 60, height - 107, 60, 15));

                controlRect = SentryLayout.GetControlRect(ElementLbl);
                SentryLayout.SetControlRect(ElementLbl, new Rectangle(5, 5, 60, 20));
                controlRect = SentryLayout.GetControlRect(BehaviourLbl);
                SentryLayout.SetControlRect(BehaviourLbl, new Rectangle((width - 45) / 2 + 5, 5, 60, 20));

                controlRect = SentryLayout.GetControlRect(Element);
                SentryLayout.SetControlRect(Element, new Rectangle(5, 25, (width - 45) / 2 - 10, 20));
                controlRect = SentryLayout.GetControlRect(Behaviour);
                SentryLayout.SetControlRect(Behaviour, new Rectangle((width - 45) / 2 + 5, 25, (width - 45) / 2 - 10, 20));

                controlRect = SentryLayout.GetControlRect(Ticker);
                SentryLayout.SetControlRect(Ticker, new Rectangle(5, 45, 60, 15));
                controlRect = SentryLayout.GetControlRect(TickerNote);
                SentryLayout.SetControlRect(TickerNote, new Rectangle(70, 45, (width - 45) / 2 - 45, 15));
                controlRect = SentryLayout.GetControlRect(TickerNote2);
                SentryLayout.SetControlRect(TickerNote2, new Rectangle(5, 60, width - 45, 15));

                controlRect = SentryLayout.GetControlRect(Range);
                SentryLayout.SetControlRect(Range, new Rectangle(5, 85, 30, 15));
                controlRect = SentryLayout.GetControlRect(RangeNote);
                SentryLayout.SetControlRect(RangeNote, new Rectangle(40, 85, 120, 15));

                controlRect = SentryLayout.GetControlRect(Timer);
                SentryLayout.SetControlRect(Timer, new Rectangle(5, 105, 30, 15));
                controlRect = SentryLayout.GetControlRect(TimerNote);
                SentryLayout.SetControlRect(TimerNote, new Rectangle(40, 105, 120, 15));

                controlRect = SentryLayout.GetControlRect(Comps);
                SentryLayout.SetControlRect(Comps, new Rectangle(5, 125, 30, 15));
                controlRect = SentryLayout.GetControlRect(TapersNote);
                SentryLayout.SetControlRect(TapersNote, new Rectangle(40, 125, 120, 15));

                controlRect = SentryLayout.GetControlRect(Slots);
                SentryLayout.SetControlRect(Slots, new Rectangle(5, 145, 30, 15));
                controlRect = SentryLayout.GetControlRect(SlotsNote);
                SentryLayout.SetControlRect(SlotsNote, new Rectangle(40, 145, 120, 15));



                controlRect = SentryLayout.GetControlRect(UseMacroLogic);
                SentryLayout.SetControlRect(UseMacroLogic, new Rectangle((width - 45) / 2 + 5, 85, (width - 45) / 2 - 10, 15));
                controlRect = SentryLayout.GetControlRect(UseLogDie);
                SentryLayout.SetControlRect(UseLogDie, new Rectangle((width - 45) / 2 + 5, 105, (width - 45) / 2 - 10, 15));

                controlRect = SentryLayout.GetControlRect(SaveSettings3);
                SentryLayout.SetControlRect(SaveSettings3, new Rectangle(0, height - 107, width - 45 - 60, 15));
                controlRect = SentryLayout.GetControlRect(ResetOptions);
                SentryLayout.SetControlRect(ResetOptions, new Rectangle(width - 45 - 60, height - 107, 60, 15));

                controlRect = Wrapper.GetControlRect(Die);
                Wrapper.SetControlRect(Die, new Rectangle(0, height - 15, 40, 15));
                controlRect = Wrapper.GetControlRect(IgnoreVP);
                Wrapper.SetControlRect(IgnoreVP, new Rectangle(40, height - 15, 40, 15));
                controlRect = Wrapper.GetControlRect(Fixbusy);
                Wrapper.SetControlRect(Fixbusy, new Rectangle(80, height - 15, 40, 15));
                controlRect = Wrapper.GetControlRect(Loc);
                Wrapper.SetControlRect(Loc, new Rectangle(120, height - 15, 40, 15));

                controlRect = Wrapper.GetControlRect(ModeLbl);
                Wrapper.SetControlRect(ModeLbl, new Rectangle(165, height - 15, 35, 15));
                controlRect = Wrapper.GetControlRect(Mode);
                Wrapper.SetControlRect(Mode, new Rectangle(200, height - 15, width - 190, 15));

                controlRect = Wrapper.GetControlRect(Imperil);
                Wrapper.SetControlRect(Imperil, new Rectangle(0, 0, 40, 15));
                controlRect = Wrapper.GetControlRect(Bludge);
                Wrapper.SetControlRect(Bludge, new Rectangle(40, 0, 40, 15));
                controlRect = Wrapper.GetControlRect(Slash);
                Wrapper.SetControlRect(Slash, new Rectangle(80, 0, 40, 15));
                controlRect = Wrapper.GetControlRect(Pierce);
                Wrapper.SetControlRect(Pierce, new Rectangle(120, 0, 40, 15));
                controlRect = Wrapper.GetControlRect(Light);
                Wrapper.SetControlRect(Light, new Rectangle(0, 15, 40, 15));
                controlRect = Wrapper.GetControlRect(Acid);
                Wrapper.SetControlRect(Acid, new Rectangle(40, 15, 40, 15));
                controlRect = Wrapper.GetControlRect(Fire);
                Wrapper.SetControlRect(Fire, new Rectangle(80, 15, 40, 15));
                controlRect = Wrapper.GetControlRect(Cold);
                Wrapper.SetControlRect(Cold, new Rectangle(120, 15, 40, 15));

                controlRect = Wrapper.GetControlRect(Stam);
                Wrapper.SetControlRect(Stam, new Rectangle(200, 0, 40, 15));
                controlRect = Wrapper.GetControlRect(Harm);
                Wrapper.SetControlRect(Harm, new Rectangle(200, 15, 40, 15));
                controlRect = Wrapper.GetControlRect(Tag);
                Wrapper.SetControlRect(Tag, new Rectangle(240, 0, width - 240, 30));

                controlRect = AdminLayout.GetControlRect(AdminNote);
                AdminLayout.SetControlRect(AdminNote, new Rectangle(5, 5, width - 50, 15));
                controlRect = AdminLayout.GetControlRect(ForcelogNote);
                AdminLayout.SetControlRect(ForcelogNote, new Rectangle(5, 35, 100, 15));
                controlRect = AdminLayout.GetControlRect(ForcelogBox);
                AdminLayout.SetControlRect(ForcelogBox, new Rectangle(5, 55, width - 110, 15));
                controlRect = AdminLayout.GetControlRect(ForcelogButton);
                AdminLayout.SetControlRect(ForcelogButton, new Rectangle(0, 85, (width - 50)/2, 15));
                controlRect = AdminLayout.GetControlRect(ForceRelogButton);
                AdminLayout.SetControlRect(ForceRelogButton, new Rectangle((width - 50) / 2, 85, (width - 50) / 2, 15));

                controlRect = AdminLayout.GetControlRect(ForcelocButton);
                AdminLayout.SetControlRect(ForcelocButton, new Rectangle(0, 105, (width - 50) / 2, 15));
                controlRect = AdminLayout.GetControlRect(ForcedieButton);
                AdminLayout.SetControlRect(ForcedieButton, new Rectangle((width - 50) / 2, 105, (width - 50) / 2, 15));

                controlRect = Wrapper.GetControlRect(Strength);
                Wrapper.SetControlRect(Strength, new Rectangle(0, 50, 20, 20));
                controlRect = Wrapper.GetControlRect(Endurance);
                Wrapper.SetControlRect(Endurance, new Rectangle(20, 50, 20, 20));
                controlRect = Wrapper.GetControlRect(Coordination);
                Wrapper.SetControlRect(Coordination, new Rectangle(0, 70, 20, 20));
                controlRect = Wrapper.GetControlRect(Quickness);
                Wrapper.SetControlRect(Quickness, new Rectangle(20, 70, 20, 20));
                controlRect = Wrapper.GetControlRect(Focus);
                Wrapper.SetControlRect(Focus, new Rectangle(0, 90, 20, 20));
                controlRect = Wrapper.GetControlRect(Willpower);
                Wrapper.SetControlRect(Willpower, new Rectangle(20, 90, 20, 20));

                controlRect = Wrapper.GetControlRect(Run);
                Wrapper.SetControlRect(Run, new Rectangle(0, 120, 20, 20));
                controlRect = Wrapper.GetControlRect(StamRegen);
                Wrapper.SetControlRect(StamRegen, new Rectangle(20, 120, 20, 20));

                controlRect = Wrapper.GetControlRect(Creature);
                Wrapper.SetControlRect(Creature, new Rectangle(0, 150, 20, 20));
                controlRect = Wrapper.GetControlRect(Life);
                Wrapper.SetControlRect(Life, new Rectangle(20, 150, 20, 20));
                controlRect = Wrapper.GetControlRect(War);
                Wrapper.SetControlRect(War, new Rectangle(0, 170, 20, 20));
                controlRect = Wrapper.GetControlRect(MagicD);
                Wrapper.SetControlRect(MagicD, new Rectangle(20, 170, 20, 20));
                controlRect = Wrapper.GetControlRect(HeavyW);
                Wrapper.SetControlRect(HeavyW, new Rectangle(0, 190, 20, 20));
                controlRect = Wrapper.GetControlRect(MelD);
                Wrapper.SetControlRect(MelD, new Rectangle(20, 190, 20, 20));
                controlRect = Wrapper.GetControlRect(MissW);
                Wrapper.SetControlRect(MissW, new Rectangle(0, 210, 20, 20));
                controlRect = Wrapper.GetControlRect(MissD);
                Wrapper.SetControlRect(MissD, new Rectangle(20, 210, 20, 20));
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void Dispose()
        {
            View.Dispose();          
            lib.ViewInstance--;
        }

        public ACImage GetIcon()
        {
            ACImage result = null;
            try
            {
                Assembly assembly = typeof(MainView).Assembly;
                using (Stream manifestResourceStream = assembly.GetManifestResourceStream("Defiance.Resources.icon.png"))
                {
                    if (manifestResourceStream != null)
                    {
                        using (Bitmap bitmap = new Bitmap(manifestResourceStream))
                        {
                            result = new ACImage(bitmap);
                        }
                    }
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
            return result;
        }

        public string GetPluginName()
        {
            string result = string.Empty;
            try
            {
                Assembly assembly = typeof(MainView).Assembly;
                result = string.Format("{0} v{1}", assembly.GetName().Name, Assembly.GetExecutingAssembly().GetName().Version.ToString());
            }
            catch (Exception ex) { Repo.RecordException(ex); }
            return result;
        }

        private static ViewProperties properties;
        private static ControlGroup controls;

        public static HudView View;
        public static HudView TargetHud;

        private static HudFixedLayout Wrapper;
        private static HudFixedLayout Wrapper2;

        private static HudFixedLayout DefinedLayout;
        private static HudFixedLayout SentryLayout;
        private static HudFixedLayout AdminLayout;
        private static HudFixedLayout PlayerLayout;
        private static HudFixedLayout PortalLayout;
        private static HudFixedLayout CorpseLayout;
        private static HudFixedLayout LogoutLayout;
        private static HudFixedLayout AlertLayout;

        public static HudTabView Tabs;
        public static HudTabView Tabs2;

        public static HudList TimerList;
        public static HudList PlayerList;
        public static HudList GuildList;
        public static HudList SettingList;
        public static HudList FriendsList;
        public static HudList EnemiesList;
        public static HudList PortalList;
        public static HudList CorpseList;

        public static HudCombo Mode;
        public static HudCombo Element;
        public static HudCombo Behaviour;

        public static HudTextBox ForcelogBox;
        public static HudTextBox Range;
        public static HudTextBox Timer;
        public static HudTextBox Ticker;
        public static HudTextBox Slots;
        public static HudTextBox Comps;
        public static HudTextBox Friend;
        public static HudTextBox Enemy;

        public static HudStaticText ElementLbl;
        public static HudStaticText BehaviourLbl;
        public static HudStaticText FriendsNote;
        public static HudStaticText AdminNote;
        public static HudStaticText ForcelogNote;
        public static HudStaticText ModeLbl;
        public static HudStaticText EnemiesNote;
        public static HudStaticText TextNote;
        public static HudStaticText AudioNote;
        public static HudStaticText TapersNote;
        public static HudStaticText TimerNote;
        public static HudStaticText TickerNote;
        public static HudStaticText TickerNote2;
        public static HudStaticText SlotsNote;
        public static HudStaticText RangeNote;
        public static HudStaticText GuildNote;
        public static HudStaticText EnemyNote;
        public static HudStaticText VersusNote;

        public static HudButton SaveSettings2;
        public static HudButton SaveSettings3;
        public static HudButton ResetOptions;
        public static HudButton ResetAlarms;

        public static HudButton AddFriend;
        public static HudButton AddEnemy;
        public static HudButton Rescan;
        public static HudButton ForcelogButton;
        public static HudButton ForceRelogButton;
        public static HudButton ForcelocButton;
        public static HudButton ForcedieButton;

        public static HudButton Reload;
        public static HudButton Imperil;
        public static HudButton Bludge;
        public static HudButton Slash;
        public static HudButton Pierce;
        public static HudButton Light;
        public static HudButton Acid;
        public static HudButton Fire;
        public static HudButton Cold;
        public static HudButton Tag;
        public static HudButton Strength;
        public static HudButton Endurance;
        public static HudButton Coordination;
        public static HudButton Quickness;
        public static HudButton Focus;
        public static HudButton Willpower;
        public static HudButton Run;
        public static HudButton Creature;
        public static HudButton Life;
        public static HudButton War;
        public static HudButton MagicD;
        public static HudButton HeavyW;
        public static HudButton MelD;
        public static HudButton MissW;
        public static HudButton MissD;
        public static HudButton Stam;
        public static HudButton Harm;
        public static HudButton StamRegen;
        public static HudButton Fixbusy;
        public static HudButton Loc;
        public static HudButton Die;
        public static HudButton IgnoreVP;

        public static HudCheckBox UseLogDie;
        public static HudCheckBox AlertLS;
        public static HudCheckBox AlertPA;
        public static HudCheckBox AlertDA;
        public static HudCheckBox AlertPF;
        public static HudCheckBox AlertLogP;
        public static HudCheckBox AlertSPK;
        public static HudCheckBox AlertSS;
        public static HudCheckBox AlertOdds;
        public static HudCheckBox AlertBc;
        public static HudCheckBox UseMacroLogic;
        public static HudCheckBox AlertPKT;
        public static HudCheckBox AlertDT;
    }
}