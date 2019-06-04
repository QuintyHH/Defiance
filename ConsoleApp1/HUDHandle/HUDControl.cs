using Decal.Adapter;
using Decal.Adapter.Wrappers;
using Defiance.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using VirindiViewService;
using VirindiViewService.Controls;

namespace Defiance.HUDHandle
{
    public class HUDControl
	{
		public void HUDInit()
		{
			try
            {
                if (lib.HUDInstance < 1)
                {
                    this.CreateEvents();
                    this.CreateTargetHudView();
                    this.TargetCache = new TargetCache();
                    this.DebuffDB = Utils.InitialiseDebuffDB();
                    this.DebuffDB.BuildHash();
                    this.TargetCache.OnDebuffExpired += this.TargetCache_OnDebuffExpired;
                    this.InitialiseTargetHUD();
                    this.ApplySettings();
                    lib.HUDInstance++;
                }
			}
            catch (Exception ex) { Repo.RecordException(ex); }
        }

		public void Dispose()
		{
			try
			{
				this.DestroyTargetHudView();
				this.DestroyEvents();
				this.TargetCache.OnDebuffExpired -= this.TargetCache_OnDebuffExpired;
				this.TargetCache = null;
				this.DestroyTargetHUD();
                lib.HUDInstance--;
			}
            catch (Exception ex) { Repo.RecordException(ex); }
        }


        public void ApplySettings()
        {
            try
            {
                CurrentProfile.TargetPlayers = true;
                CurrentProfile.RequestVitalsMethod = true;
                CurrentProfile.TimeBetweenIDRequests = 0.5;
                CurrentProfile.ShowStaminaBar = true;
                CurrentProfile.ShowManaBar = true;
                CurrentProfile.LifeDebuffTime = 300;
                CurrentProfile.DebuffWindowMinLife = 2.45f;
                CurrentProfile.DebuffWindowMaxLife = 3.1f;
                CurrentProfile.ShowDebuffIcons = true;
                CurrentProfile.FloatingDebuffs = true;
                CurrentProfile.FloatingDebuffSize = 45;
                CurrentProfile.MonsterHeightOffset = 0.05f;
                CurrentProfile.SingleProfile = true;
                DebuffDB.CurrentProfile = CurrentProfile;
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        private void CreateEvents()
		{
			try
			{
                lib.MyCore.ItemSelected += this.Core_ItemSelected;
                lib.MyCore.WorldFilter.ChangeObject += this.WorldFilter_ChangeObject;
                lib.MyCore.WorldFilter.CreateObject += this.WorldFilter_CreateObject;
                lib.MyCore.ItemDestroyed += this.Core_ItemDestroyed;
                lib.MyCore.EchoFilter.ServerDispatch += this.EchoFilter_ServerDispatch;
			}
            catch (Exception ex) { Repo.RecordException(ex); }
        }

		private void DestroyEvents()
		{
			try
			{
                lib.MyCore.WorldFilter.ChangeObject -= this.WorldFilter_ChangeObject;
                lib.MyCore.WorldFilter.CreateObject -= this.WorldFilter_CreateObject;
                lib.MyCore.ItemDestroyed -= this.Core_ItemDestroyed;
                lib.MyCore.EchoFilter.ServerDispatch -= this.EchoFilter_ServerDispatch;
			}
            catch (Exception ex) { Repo.RecordException(ex); }
        }

		private void CreateTargetHudView()
		{
			try
			{
               this.TargetHud = new HudView("Defiance HUD", 285, 120, new ACImage(Color.Coral));

                this.TargetHud.ShowInBar = false;
                this.TargetHud.UserMinimizable = false;
                this.TargetHud.Ghosted = true;
                this.TargetHud.ClickThrough = true;
                this.TargetHud.Location = new Point(50, 100);
                this.TargetHud.UserResizeable = false;
                this.TargetHud.MinimumClientArea = new Size(285, 120);
                this.TargetHud.MaximumClientArea = new Size(300, 120);
                this.TargetHud.LoadUserSettings();
                this.TargetHud.Visible = true;
                this.TargetSurface = new HudEmulator();
                this.TargetHud.Controls.HeadControl = this.TargetSurface;
                this.TargetSurface.LeaveSurfaceInBeginRender = true;
                this.TargetSurface.Draw += new HudEmulator.delDraw(this.TargetSurface_Draw);

            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

		private void DestroyTargetHudView()
		{
			try
			{
				if (this.TargetSurface != null)
				{
					this.TargetSurface.Draw -= new HudEmulator.delDraw(this.TargetSurface_Draw);
				}
				this.TargetSurface = null;
				this.TargetHud = null;
			}
            catch (Exception ex) { Repo.RecordException(ex); }
        }

		private void Core_ItemSelected(object sender, ItemSelectedEventArgs e)
		{
			try
			{
				if (this.TargetHud != null)
				{
					this.TargetHud.Visible = false;
					WorldObject worldObject = lib.MyCore.WorldFilter[e.ItemGuid];
					if (worldObject != null)
					{
                        if (worldObject.ObjectClass == ObjectClass.Player)
                        {
                            Target target = this.TargetCache.GetTarget(e.ItemGuid);
                            if (target == null)
                            {
                                this.TargetCache.AddOrUpdate(worldObject);
                                lib.MyHost.Actions.RequestId(e.ItemGuid);
                            }
                            else if (target.MaxHealth == -1)
                            {
                                lib.MyHost.Actions.RequestId(e.ItemGuid);
                            }
                            this.TargetHud.Visible = true;
                            if (this.TargetSurface != null)
                            {
                                this.TargetSurface.Invalidate();
                            }
                        }
					}
				}
			}
            catch (Exception ex) { Repo.RecordException(ex); }
        }

		private void WorldFilter_ChangeObject(object sender, ChangeObjectEventArgs e)
		{
			try
			{
				if (e.Changed.ObjectClass == ObjectClass.Player)
				{
					this.TargetCache.AddOrUpdate(e.Changed);
				}
			}
            catch (Exception ex) { Repo.RecordException(ex); }
        }

		private void WorldFilter_CreateObject(object sender, CreateObjectEventArgs e)
		{
			try
			{
				if (e.New.ObjectClass == ObjectClass.Player)
				{
					this.TargetCache.AddOrUpdate(e.New);
				}
			}
            catch (Exception ex) { Repo.RecordException(ex); }
        }

		private void Core_ItemDestroyed(object sender, ItemDestroyedEventArgs e)
		{
			try
			{
				this.TargetCache.DeleteTarget(e.ItemGuid);
				if (this.TargetsWithDebuffIcons.ContainsKey(e.ItemGuid))
				{
					Utils.ClearD3DObjects(this.TargetsWithDebuffIcons[e.ItemGuid]);
                    this.TargetsWithDebuffIcons.Remove(e.ItemGuid);
                }           
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

		private void EchoFilter_ServerDispatch(object sender, NetworkMessageEventArgs e)
		{
			try
			{
				if (e != null && e.Message != null)
				{
					int type = e.Message.Type;
					if (type <= 63301)
					{
						if (type == 699)
						{
						    this.SpellCasting(e);
						}
					}
					else if (type != 63317)
					{
						if (type == 63408)
						{
							if (e.Message.Value<int>("event") == 201)
							{
								this.UpdateTargetVitals(e);
							}							
						}
					}
					else if (e != null)
					{
						this.ApplyEffect(e);
					}
				}
			}
            catch (Exception ex) { Repo.RecordException(ex); }
        }

		private void SpellCasting(NetworkMessageEventArgs e)
		{
			try
			{
				if (e.Message.Value<int>("type") == 17)
				{
					string text = e.Message.Value<string>("text");
					int num = this.DebuffDB.SpellWordsToParticleEffect(text);
					if (num != 0)
					{
						this.DebuffDB.AddCasting(text, num);
					}
				}
			}
            catch (Exception ex) { Repo.RecordException(ex); }
        }

		private void ApplyEffect(NetworkMessageEventArgs e)
		{
			try
			{
				int effect = e.Message.Value<int>("effect");
				int guid = e.Message.Value<int>("object");
				if (lib.MyCore.WorldFilter[guid] != null)
				{
					WorldObject worldObject = lib.MyCore.WorldFilter[guid];
					bool flag = true;
					if (worldObject.ObjectClass == ObjectClass.Player)
					{
						flag = false;
					}
					if (!flag)
					{
                        if (worldObject.Id != lib.MyID)
                        {
                            DebuffInfo debuff = this.DebuffDB.GetDebuff(effect);
                            if (debuff != null)
                            {
                                this.TargetCache.AddOrUpdate(worldObject, debuff.DisplayName, debuff.DefaultDuration);
                                this.MaintainDebuffIcons(worldObject.Id);
                                if (lib.MyHost.Actions.CurrentSelection == worldObject.Id && this.TargetSurface != null)
                                {
                                    this.TargetSurface.Invalidate();
                                }
                            }
                        }
					}
				}
			}
            catch (Exception ex) { Repo.RecordException(ex); }
        }

		private void UpdateTargetVitals(NetworkMessageEventArgs e)
		{
       
                int guid = e.Message.Value<int>("object");
                WorldObject worldObject = lib.MyCore.WorldFilter[guid];
                if (worldObject == null)
                {
                    return;
                }
            try
            {
                if (guid.Equals(lib.MyCore.Actions.CurrentSelection))
                {
                    bool flag = false;
                    if ((e.Message.Value<int>("flags") & 256) > 0)
                    {
                        int currentHealth = e.Message.Value<int>("health");
                        int maxHealth = e.Message.Value<int>("healthMax");
                        int currentStamina = e.Message.Value<int>("stamina");
                        int maxStamina = e.Message.Value<int>("staminaMax");
                        int currentMana = e.Message.Value<int>("mana");
                        int maxMana = e.Message.Value<int>("manaMax");
                        TargetCache.AddOrUpdate(worldObject, currentHealth, maxHealth, currentStamina, maxStamina, currentMana, maxMana);
                        flag = true;
                    }
                    if (TargetSurface != null && flag)
                    {
                        TargetSurface.Invalidate();
                    }
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }        		

        public void InitialiseTargetHUD()
        {
            try
            {
                PluginCore.ShrtTmr.Tick += this.HUDTick;
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

		public void DestroyTargetHUD()
		{
			try
			{				
				PluginCore.ShrtTmr.Tick -= this.HUDTick;
			}
            catch (Exception ex) { Repo.RecordException(ex); }         
		}

        protected void HUDTick(object sender, EventArgs e)
        {
            try
            {
                WorldObject worldObject = lib.MyCore.WorldFilter[lib.MyHost.Actions.CurrentSelection];
                if (worldObject == null)
                {
                    this.TargetHud.Visible = false;
                }
                else if (worldObject.ObjectClass == ObjectClass.Player)
                {
                    if (LastID.Equals(0) || !LastID.Equals(lib.MyHost.Actions.CurrentSelection))
                    {
                        LastID = lib.MyHost.Actions.CurrentSelection;
                        this.LastRequestUpdateTime = DateTime.MinValue;
                    }
                    if (DateTime.Now.Subtract(this.LastRequestUpdateTime).TotalSeconds >= 0.5)
                    {
                        this.LastRequestUpdateTime = DateTime.Now;
                        lib.MyHost.Actions.RequestId(lib.MyHost.Actions.CurrentSelection);
                    }
                    this.TargetHud.Visible = true;
                    if (!this.CurrentProfile.ShowDebuffIcons && this.TargetSurface != null)
                    {
                        this.TargetSurface.Invalidate();
                    }
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void TargetSurface_Draw(HudEmulator Caller, DxTexture Target, Rectangle TargetRegion, HudEmulator.delClearRegion dClearOp)
        {
            try
            {
                if (TargetHud != null)
                {
                    if (lib.MyCore.Actions.CurrentSelection != 0)
                    {
                        WorldObject worldObject = lib.MyCore.WorldFilter[lib.MyCore.Actions.CurrentSelection];
                        if (worldObject != null && worldObject.ObjectClass == ObjectClass.Player)
                        {

                            if (Target != null)
                            {
                                Color color = Color.Transparent;
                                Target.Fill(TargetRegion, color);
                                Target target = TargetCache.GetTarget(worldObject.Id);
                                if (target == null)
                                {
                                    TargetCache.AddOrUpdate(worldObject);
                                    target = TargetCache.GetTarget(worldObject.Id);
                                }
                                int num = 0;
                                int num2 = 15;
                                int num3 = 285;
                                int num4 = 30;
                                Pen pen = new Pen(ColorTranslator.FromHtml(CurrentProfile.ColorRed), 8f);
                                Pen pen2 = new Pen(ColorTranslator.FromHtml(CurrentProfile.ColorWhite), 8f);
                                Pen pen3 = new Pen(ColorTranslator.FromHtml(CurrentProfile.ColorGrey), 8f);
                                Pen pen4 = new Pen(ColorTranslator.FromHtml(CurrentProfile.ColorGreen), 8f);
                                Pen pen5 = new Pen(ColorTranslator.FromHtml(CurrentProfile.ColorPurple), 8f);
                                Pen pen6 = new Pen(ColorTranslator.FromHtml(CurrentProfile.ColorBlue), 8f);
                                Pen pen7 = new Pen(ColorTranslator.FromHtml(CurrentProfile.ColorYellow), 8f);
                                Pen pen8 = new Pen(ColorTranslator.FromHtml(CurrentProfile.ColorOrange), 8f);
                                Pen pen10 = new Pen(ColorTranslator.FromHtml(CurrentProfile.ColorOrange), 8f);

                                new Bitmap(50, 50);
                                Bitmap bitmap;
                                if (worldObject.Values(LongValueKey.Monarch) == lib.moncheck)
                                {
                                    pen10 = pen4;
                                }
                                else
                                {
                                    pen10 = pen;
                                }
                                Rectangle rectangle = Utils.DrawText(target.FullName, CurrentProfile.HudFont, pen10, out bitmap);

                                rectangle.Y = 10;
                                rectangle.X = 5;
                                num = 0;
                                num2 += 10;
                                if (rectangle.Width > num3)
                                {
                                    num3 = rectangle.Width;
                                }
                                num4 += rectangle.Height;
                                Target.DrawImage(bitmap, rectangle, Color.Transparent);
                                rectangle = default(Rectangle);
                                num2 += 10;
                                Bitmap bitmap2 = null;

                                Rectangle rectangle2 = new Rectangle(0, 0, 230, 10);
                                rectangle = Utils.DrawRectangle(rectangle2, pen, out bitmap2, false, 100, 2);
                                rectangle.Y = 25;
                                rectangle.X = 2;
                                int num5 = num;
                                int num6 = num2;
                                int width = rectangle.Width;
                                num2 += 7;
                                int num7;
                                num7 = target.CurrentHealthPercentage;

                                if (rectangle.Width > num3)
                                {
                                    num3 = rectangle.Width;
                                }
                                num4 += rectangle.Height;

                                Target.DrawImage(bitmap2, rectangle, Color.Transparent);
                                bitmap2 = null;
                                rectangle = Utils.DrawRectangle(rectangle2, pen, out bitmap2, true, num7, 2);
                                rectangle.X = 2;
                                rectangle.Y = 25;
                                Target.DrawImage(bitmap2, rectangle, Color.Transparent);
                                if (target.MaxHealth > 0)
                                {
                                    string text;
                                    if (!target.HealthInFormat.Contains("-1"))
                                    {
                                        text = target.HealthInFormat;
                                    }
                                    else
                                    {
                                        text = (target.MaxHealth * num7 / 100).ToString() + "/" + target.MaxHealth.ToString();
                                    }
                                    Bitmap bitmap3 = null;
                                    Font font = new Font(CurrentProfile.HudFont.Name, 8f, FontStyle.Bold);
                                    rectangle = Utils.DrawText(text, font, pen2, out bitmap3);
                                    rectangle.X = 0;
                                    rectangle.Y = 22;
                                    rectangle.X = 240;
                                    Target.DrawImage(bitmap3, rectangle, Color.Transparent);
                                    rectangle = default(Rectangle);
                                    num2 += 7;

                                    Bitmap bitmap4 = null;
                                    rectangle2 = new Rectangle(0, 0, 230, 10);
                                    rectangle = Utils.DrawRectangle(rectangle2, pen7, out bitmap4, false, 100, 2);
                                    rectangle.Y = 37;
                                    rectangle.X = 2;
                                    num5 = num;
                                    num6 = num2;
                                    int width2 = rectangle.Width;
                                    num2 += 7;
                                    if (rectangle.Width > num3)
                                    {
                                        num3 = rectangle.Width;
                                    }
                                    num4 += rectangle.Height;
                                    Target.DrawImage(bitmap4, rectangle, Color.Transparent);
                                    rectangle = Utils.DrawRectangle(rectangle2, pen7, out bitmap4, true, target.CurrentStaminaPercentage, 2);
                                    rectangle.X = 2;
                                    rectangle.Y = 37;
                                    Target.DrawImage(bitmap4, rectangle, Color.Transparent);
                                    if (!target.StaminaInFormat.Contains("-1"))
                                    {
                                        Bitmap bitmap5 = null;
                                        Font font2 = new Font(CurrentProfile.HudFont.Name, 8f, FontStyle.Bold);
                                        rectangle = Utils.DrawText(target.StaminaInFormat, font2, pen2, out bitmap5);
                                        rectangle.X = 0;
                                        rectangle.Y = 34;
                                        rectangle.X = 240;
                                        Target.DrawImage(bitmap5, rectangle, Color.Transparent);
                                    }
                                    rectangle = default(Rectangle);
                                    num2 += 5;

                                    Bitmap bitmap6 = null;
                                    rectangle2 = new Rectangle(0, 0, 230, 10);
                                    rectangle = Utils.DrawRectangle(rectangle2, pen6, out bitmap6, false, 100, 2);
                                    rectangle.Y = 49;
                                    rectangle.X = 2;
                                    num5 = num;
                                    num6 = num2;
                                    int width3 = rectangle.Width;
                                    num2 += rectangle.Height;
                                    if (rectangle.Width > num3)
                                    {
                                        num3 = rectangle.Width;
                                    }
                                    num4 += rectangle.Height;
                                    Target.DrawImage(bitmap6, rectangle, Color.Transparent);
                                    rectangle = Utils.DrawRectangle(rectangle2, pen6, out bitmap6, true, target.CurrentManaPercentage, 2);
                                    rectangle.X = 2;
                                    rectangle.Y = 49;
                                    Target.DrawImage(bitmap6, rectangle, Color.Transparent);
                                    if (!target.ManaInFormat.Contains("-1"))
                                    {
                                        Bitmap bitmap7 = null;
                                        Font font3 = new Font(CurrentProfile.HudFont.Name, 8f, FontStyle.Bold);
                                        rectangle = Utils.DrawText(target.ManaInFormat, font3, pen2, out bitmap7);
                                        rectangle.X = 0;
                                        rectangle.Y = 46;
                                        rectangle.X = 240;
                                        Target.DrawImage(bitmap7, rectangle, Color.Transparent);
                                    }
                                    int num8 = 0;
                                    int num9 = 0;
                                    num2 += 7;
                                    foreach (Debuff debuff in target.Debuffs)
                                    {
                                        rectangle = default(Rectangle);
                                        rectangle2 = new Rectangle(0, 0, 22, 22);
                                        DebuffDisplayInfo debuffDisplayInfo = DebuffDB.GetDebuffDisplayInfo(debuff.Name);
                                        Pen pen9 = pen2;

                                        rectangle = new Rectangle(0, 0, 22, 22);

                                        rectangle.Y = 61;
                                        rectangle.X = 5 + num9;
                                        num9 += rectangle.Width + 7;
                                        Target.DrawPortalImage(debuffDisplayInfo.Icon, rectangle);
                                        num8++;
                                    }
                                    if (num9 > num3)
                                    {
                                        num3 = num9;
                                    }             
                                }

                                TargetHud.Width = num3 + (int)((double)0);
                                TargetHud.Height = num4;
                            }
                            else
                            {
                                TargetHud.Visible = false;
                            }
                        }
                    }
                }
            }
            catch { Utility.AddChatText("Error generating Defiance HUD, retrying...", 6); }
        }

        private void TargetCache_OnDebuffExpired(object source, DebuffEventArgs e)
		{
			try
			{
				this.MaintainDebuffIcons(e.TargetID);
				if (lib.MyHost.Actions.CurrentSelection == e.TargetID && this.TargetSurface != null)
				{
					this.TargetSurface.Invalidate();
				}
			}
            catch (Exception ex) { Repo.RecordException(ex); }
        }

		private void MaintainDebuffIcons(int targetId)
		{
			if (this.TargetsWithDebuffIcons.ContainsKey(targetId))
			{
				Utils.ClearD3DObjects(this.TargetsWithDebuffIcons[targetId]);
				this.TargetsWithDebuffIcons.Remove(targetId);
			}
			this.TargetsWithDebuffIcons.Add(targetId, new List<TargetD3D>());
			WorldObject worldObject = lib.MyCore.WorldFilter[targetId];
            if (worldObject != null && worldObject.ObjectClass != ObjectClass.Player)
            {
                return;
            }
            List<Debuff> debuffs = this.TargetCache.GetDebuffs(targetId);
			List<int> list = new List<int>();
			if (debuffs.Count == 1)
			{
				DebuffDisplayInfo debuffDisplayInfo = this.DebuffDB.GetDebuffDisplayInfo(debuffs.FirstOrDefault<Debuff>().Name);
				this.TargetsWithDebuffIcons[targetId].Add(new TargetD3D
				{
					Id = worldObject.Id,
					Icon = debuffDisplayInfo.Icon,
					D3DObject = Utils.CreateD3DObject(this, worldObject.Id, debuffDisplayInfo.Icon)
				});
				return;
			}
			if (debuffs.Count > 1)
			{
				IOrderedEnumerable<Debuff> orderedEnumerable = from o in debuffs
				orderby o.TimeRemaining descending
				select o;
				new Dictionary<int, TargetD3D>();
				foreach (Debuff debuff in orderedEnumerable)
				{
					DebuffDisplayInfo debuffDisplayInfo2 = this.DebuffDB.GetDebuffDisplayInfo(debuff.Name);
					if (debuffDisplayInfo2 != null && !list.Contains(debuffDisplayInfo2.Icon))
					{
						list.Add(debuffDisplayInfo2.Icon);
					}
				}
				float num = 4f;
				float num2 = num / (float)list.Count;
				float num3 = 0f;
				float scale = this.CurrentProfile.FloatingDebuffSizeF / (float)Math.Pow((double)list.Count, 0.2);
				float radius = 0.1f + (float)Math.Pow((double)list.Count, 0.3) * 0.2f;
				foreach (int icon in list)
				{
					this.TargetsWithDebuffIcons[targetId].Add(new TargetD3D
					{
						Id = worldObject.Id,
						Icon = icon,
						D3DObject = Utils.CreateD3DObject(this, worldObject.Id, icon, radius, num, scale, num3)
					});
					num3 += num2;
				}
			}
		}

		public TargetCache TargetCache;
		public DebuffDB DebuffDB;
		public Profile CurrentProfile = new Profile();
		private HudView TargetHud;
		private HudEmulator TargetSurface;
		private DateTime TestDT = DateTime.Now;
		private int LastID;
		private DateTime LastRequestUpdateTime = DateTime.MinValue;
		private Dictionary<int, List<TargetD3D>> TargetsWithDebuffIcons = new Dictionary<int, List<TargetD3D>>();
	}
}
