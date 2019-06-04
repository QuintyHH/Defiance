using Defiance.Utils;
using System;
using System.Drawing;

namespace Defiance.HUDHandle
{
    public class Profile
    {
        public bool TargetPlayers { get; set; }
        public bool RequestVitalsMethod { get; set; }
        public double TimeBetweenIDRequests { get; set; }
        public bool ShowStaminaBar { get; set; }
        public bool ShowManaBar { get; set; }
        public int LifeDebuffTime { get; set; }
        public float DebuffWindowMinLife { get; set; }
        public float DebuffWindowMaxLife { get; set; }
        public bool ShowDebuffIcons { get; set; }
        public bool HighlightDebuffTime { get; set; }
        public int HighlightDebuffTimeSeconds { get; set; }
        public bool FloatingDebuffs { get; set; }
        public int FloatingDebuffSize { get; set; }
        public float MonsterHeightOffset { get; set; }
        public float FloatingDebuffSizeF
        {
            get
            {
                return (float)((double)this.FloatingDebuffSize * 1.0 / 100.0);
            }
        }

        public Font HudFont
        {
            get
            {
                return new Font(this.HUDFontName, (float)this.HUDFontSize, FontStyle.Bold);
            }
        }

        public bool SingleProfile { get; set; }
        public string HUDFontName { get; set; }
        public int HUDFontSize { get; set; }
        public string ColorWhite { get; set; }
        public string ColorGrey { get; set; }
        public string ColorYellow { get; set; }
        public string ColorOrange { get; set; }
        public string ColorBlue { get; set; }
        public string ColorPurple { get; set; }
        public string ColorRed { get; set; }
        public string ColorGreen { get; set; }
        public string BackgroundColor { get; set; }
        public int BackgroundAlpha { get; set; }

        public Profile()
        {
            this.TargetPlayers = true;
            this.RequestVitalsMethod = true;
            this.TimeBetweenIDRequests = 0.5;
            this.ShowStaminaBar = true;
            this.ShowManaBar = true;
            this.LifeDebuffTime = 240;
            this.DebuffWindowMinLife = 2.45f;
            this.DebuffWindowMaxLife = 3.1f;
            this.ShowDebuffIcons = true;
            this.HighlightDebuffTime = false;
            this.HighlightDebuffTimeSeconds = 15;
            this.FloatingDebuffs = true;
            this.FloatingDebuffSize = 45;
            this.MonsterHeightOffset = 0.05f;
            this.SingleProfile = true;
            this.HUDFontName = "Arial";
            this.HUDFontSize = 9;
            this.ColorWhite = "#FAFAFA";
            this.ColorGrey = "#A4A4A4";
            this.ColorYellow = "#FACC2E";
            this.ColorOrange = "#FF8000";
            this.ColorBlue = "#0174DF";
            this.ColorPurple = "#8181F7";
            this.ColorRed = "#8A0808";
            this.ColorGreen = "#04B404";
            this.BackgroundColor = "#2E2E2E";
            this.BackgroundAlpha = 127;
        }

        public int GetDefaultDuration(DebuffCategories category)
        {
            try
            {
                switch (category)
                {
                    case DebuffCategories.Life:
                        return this.LifeDebuffTime;
                    default:
                        return 300;
                }
            }
            catch (Exception ex)
            {
                Repo.RecordException(ex);
            }
            return 300;
        }
    }
}
