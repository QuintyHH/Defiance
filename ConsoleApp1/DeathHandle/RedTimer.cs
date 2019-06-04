using Decal.Adapter;
using Defiance.BaseHandle;
using Defiance.Utils;
using Defiance.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using VirindiViewService.Controls;

namespace Defiance.DeathHandle
{

    public class RedTimer : IDisposable
    {
        private string name;
        private readonly DateTime deathtime;
        private string killer;
        private int counter;
        private bool disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.name = null;
                    this.killer = null;
                    this.counter = 0;
                    PluginCore.LngTmr.Tick -= this.Redtimer_Tick;
                    lib.RedTimerInstance--;
                }
                this.disposed = true;
            }
        }

        public RedTimer(string Name, DateTime Deathtime, string Killer)
        {
            lib.RedTimerInstance++; 
            this.name = Name;
            this.deathtime = Deathtime;
            this.killer = Killer;
     
            if (PluginCore.LngTmr.Enabled == true)
            {
                PluginCore.LngTmr.Tick += this.Redtimer_Tick;
                if (name == CoreManager.Current.CharacterFilter.Name)
                {
                    this.name = "You";
                }
                lib.OnDowntime.Add(name);
            } 
        }

        protected void Redtimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (this.counter <= 300)
                {
                    this.counter = this.counter + 1;
                    this.UpdatePkObject(this.name, this.deathtime);
                }
                if (this.counter == 290)
                {
                    if (this.name != "You")
                    {
                        Utility.AddChatText(this.name + " will turn RED in " + (300 - this.counter) + " seconds!", 6);
                    }

                    if (lib.OnDowntime.Contains(this.name))
                    {
                        lib.OnDowntime.Remove(this.name);
                        lib.OnDowntime2.Add(name);
                    }

                    if (MainView.AlertPKT.Checked == true)
                    {
                        try
                        {
                            Sounds.PkTime1.Play();
                        }
                        catch
                        {
                            Utility.AddChatText("Please make sure the sounds files are in the right location!", 6);
                        }
                    }
                    this.counter = this.counter + 1;
                }
                else if (this.counter == 300)
                {
                    if (this.name != "You")
                    {
                        Utility.AddChatText(this.name + " is turning RED!", 6);
                    }
                    if (lib.OnDowntime2.Contains(this.name))
                    {
                        lib.OnDowntime2.Remove(this.name);
                    }
                    this.RemovePkObject(name);

                    if (MainView.AlertPKT.Checked == true)
                    {
                        try
                        {
                            Sounds.PkTime2.Play();
                        }
                        catch
                        {
                            Utility.AddChatText("Please make sure the sounds files are in the right location!", 6);
                        }
                    }
                    PluginCore.LngTmr.Tick -= this.Redtimer_Tick;
                    this.counter = 0;
                    this.Dispose();
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        private void UpdatePkRow(string name, DateTime deathtime, HudList.HudListRowAccessor row, string killer)
        {
            try
            {
                List<string> list = (from f in lib.AuthIds.Split(new char[] { ',' })
                                     select f.Trim()).ToList<string>();

                ((HudPictureBox)row[0]).Image = 100667896;
                ((HudStaticText)row[1]).Text = this.name;
                if (list.Contains(name + "::Admin") == true || list.Contains(name))
                {
                    ((HudStaticText)row[1]).TextColor = Color.LawnGreen;
                }
                else
                {
                    ((HudStaticText)row[1]).TextColor = Color.Red;
                }
                    ((HudStaticText)row[2]).Text = (300 - counter).ToString();
                if (counter >= 290)
                {
                    ((HudStaticText)row[2]).TextColor = Color.Red;
                }
                else
                {
                    ((HudStaticText)row[2]).TextColor = Color.White;
                }
                    ((HudStaticText)row[3]).Text = this.deathtime.ToString("h:mm:ss tt");
                    ((HudStaticText)row[3]).TextColor = Color.Red;
                    ((HudStaticText)row[5]).Text = this.killer;
                    ((HudStaticText)row[5]).Visible = false;
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        private void RemovePkObject(string tempname)
        {
            try
            {

                for (int i = MainView.TimerList.RowCount - 1; i >= 0; i--)
                {
                    if (Convert.ToString(((HudStaticText)MainView.TimerList[i][1]).Text) == this.name)
                    {
                        MainView.TimerList.RemoveRow(i);
                    }
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        private void UpdatePkObject(string tempname, DateTime tempdeathtime)
        {
            try
            { 
                for (int i = 0; i < MainView.TimerList.RowCount; i++)
                {
                    HudList.HudListRowAccessor hudListRowAccessor = MainView.TimerList[i];
                    if (Convert.ToString(((HudStaticText)MainView.TimerList[i][1]).Text) == this.name)
                    {
                        UpdatePkRow(this.name, this.deathtime, hudListRowAccessor, this.killer);
                    }
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        } 
    }
}