using Decal.Adapter;
using Defiance.CollectionHandle;
using Defiance.Utils;
using Defiance.Views;
using System;
using System.Drawing;
using VirindiViewService.Controls;

namespace Defiance.BaseHandle
{
    class SettingsControl
    {
        public void SettingsInit()
        {
            try
            {
                UI ui = Repo.GetUI(CoreManager.Current.CharacterFilter.Server, CoreManager.Current.CharacterFilter.Name);

                lib.moncheck = ui.MonCheck;
                lib.UseAlertPKT = ui.UseAlertPKT;
                lib.UseAlertDT = ui.UseAlertDT;
                lib.UseAlertLS = ui.UseAlertLS;
                lib.UseAlertPA = ui.UseAlertPA;
                lib.UseAlertPF = ui.UseAlertPF;
                lib.UseAlertLogP = ui.UseAlertLogP;
                lib.UseAlertDA = ui.UseAlertDA;
                lib.UseAlertSPK = ui.UseAlertSPK;
                lib.UseAlertSS = ui.UseAlertSS;
                lib.UseAlertOdds = ui.UseAlertOdds;
                lib.UseAlertBc = ui.UseAlertBc;

                lib.UseMacroLogic = ui.UseMacroLogic;
                lib.UseLogDie = ui.UseLogDie;
                lib.Range = ui.Range;
                lib.Timer = ui.Timer;
                lib.Slots = ui.Slots;
                lib.Comps = ui.Comps;
                lib.Element = ui.Element;
                lib.Behaviour = ui.Behaviour;            
                lib.Ticker = ui.Ticker;

                lib.logoffcounter = 0;
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public static void SaveSettings(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (Convert.ToInt32(MainView.Range.Text) < 85)
                    {
                        Utility.AddChatText("Detection range is too low (minimum 85 yards).", 6);
                        MainView.Range.Text = "85";
                    }
                    else if (Convert.ToInt32(MainView.Range.Text) >= 85 && Convert.ToInt32(MainView.Range.Text) < 120)
                    {
                        Utility.AddChatText("WARNING: Detection range is EXTREMELY LOW! (Recommended value: 120)", 6);
                    }
                    else if (Convert.ToInt32(MainView.Range.Text) > 500)
                    {
                        Utility.AddChatText("Detection range is too high (maximum 500 yards).", 6);
                        MainView.Range.Text = "500";
                    }
                }
                catch
                {
                    Utility.AddChatText("Detection range - unrecognized format. Resetting to Recommended.", 6);
                    MainView.Range.Text = "120";
                }

                try
                {
                    if (Convert.ToInt32(MainView.Timer.Text) < 60)
                    {
                        Utility.AddChatText("Relog timer is too fast (minimum 60 seconds).", 6);
                        MainView.Timer.Text = "60";
                    }
                    else if (Convert.ToInt32(MainView.Timer.Text) > 3600)
                    {
                        Utility.AddChatText("Relog timer is too slow (maximum 3600 seconds).", 6);
                        MainView.Timer.Text = "3600";
                    }
                }
                catch
                {
                    Utility.AddChatText("Relog timer - unrecognized format. Resetting to Recommended.", 6);
                    MainView.Timer.Text = "120";
                }

                try
                {
                    if (Convert.ToInt32(MainView.Comps.Text) == 0)
                    {
                        Utility.AddChatText("Tapers logout DISABLED.", 6);
                        MainView.Comps.Text = "0";
                    }
                    else if (Convert.ToInt32(MainView.Comps.Text) > 15000)
                    {
                        Utility.AddChatText("Tapers value is too high (maximum 15000 tapers).", 6);
                        MainView.Comps.Text = "15000";
                    }
                }
                catch
                {
                    Utility.AddChatText("Tapers value - unrecognized format. Resetting to Recommended.", 6);
                    MainView.Comps.Text = "50";
                }

                try
                {
                    if (Convert.ToInt32(MainView.Slots.Text) == 0)
                    {
                        Utility.AddChatText("Inventory logout DISABLED.", 6);
                        MainView.Slots.Text = "0";
                    }
                    else if (Convert.ToInt32(MainView.Slots.Text) > 100)
                    {
                        Utility.AddChatText("Cannot set Slots higher then 100.", 6);
                        MainView.Slots.Text = "100";
                    }
                }
                catch
                {
                    Utility.AddChatText("Slot number - unrecognized format. Resetting to Recommended.", 6);
                    MainView.Slots.Text = "3";
                }

                try
                {
                    if (Convert.ToInt32(MainView.Ticker.Text) < 1)
                    {
                        Utility.AddChatText("Cannot set Ticker Rate lower then 1 ms.", 6);
                        MainView.Ticker.Text = "1";
                    }
                    else if (Convert.ToInt32(MainView.Ticker.Text) > 2000)
                    {
                        Utility.AddChatText("Cannot set Ticker Rate higher then 2000 ms.", 6);
                        MainView.Ticker.Text = "2000";
                    }
                }
                catch
                {
                    Utility.AddChatText("Ticker Rate - unrecognized format. Resetting to Recommended.", 6);
                    MainView.Ticker.Text = "100";
                }

                lib.Range = Convert.ToInt32(MainView.Range.Text);
                lib.Timer = Convert.ToInt32(MainView.Timer.Text);
                lib.Comps = Convert.ToInt32(MainView.Comps.Text);
                lib.Ticker = Convert.ToInt32(MainView.Ticker.Text);

                PluginCore.ShrtTmr.Interval = lib.Ticker;
                ViewControl.SaveUI();

                Utility.AddChatText("Settings saved.", 6);

            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }             

        public static void ResetAlarms_Hit(object sender, EventArgs e)
        {
            try
            {
                lib.UseAlertSPK = true;
                MainView.AlertSPK.Checked = lib.UseAlertSPK;

                lib.UseAlertOdds = true;
                MainView.AlertOdds.Checked = lib.UseAlertOdds;

                lib.UseAlertSS = true;
                MainView.AlertSS.Checked = lib.UseAlertSS;

                lib.UseAlertBc = true;
                MainView.AlertBc.Checked = lib.UseAlertBc;

                lib.UseAlertDT = true;
                MainView.AlertDT.Checked = lib.UseAlertDT;

                lib.UseAlertPKT = true;
                MainView.AlertPKT.Checked = lib.UseAlertPKT;

                lib.UseAlertPA = true;
                MainView.AlertPA.Checked = lib.UseAlertPA;

                lib.UseAlertLogP = true;
                MainView.AlertLogP.Checked = lib.UseAlertLogP;

                lib.UseAlertPF = false;
                MainView.AlertPF.Checked = lib.UseAlertPF;

                lib.UseAlertDA = true;
                MainView.AlertDA.Checked = lib.UseAlertDA;

                lib.UseAlertLS = true;
                MainView.AlertLS.Checked = lib.UseAlertLS;

                ViewControl.SaveUI();
                Utility.AddChatText("Alerts reset to default.", 6);
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public static void ResetOptions_Hit(object sender, EventArgs e)
        {
            try
            {
                lib.Ticker = 100;
                MainView.Ticker.Text = lib.Ticker.ToString();

                lib.Element = 0;
                MainView.Element.Current = lib.Element;

                lib.Behaviour = 0;
                MainView.Behaviour.Current = lib.Behaviour;

                lib.Range = 120;
                MainView.Range.Text = lib.Range.ToString();

                lib.Timer = 120;
                MainView.Timer.Text = lib.Timer.ToString();

                lib.Comps = 50;
                MainView.Comps.Text = lib.Comps.ToString();

                lib.Slots = 3;
                MainView.Slots.Text = lib.Slots.ToString();

                lib.UseMacroLogic = true;
                MainView.UseMacroLogic.Checked = lib.UseMacroLogic;

                lib.UseLogDie = false;
                MainView.UseLogDie.Checked = lib.UseLogDie;

                ViewControl.SaveUI();
                Utility.AddChatText("Options reset to default.", 6);
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }
    }
}
