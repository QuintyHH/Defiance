using Defiance.Utils;
using System;
using System.Media;

namespace Defiance.BaseHandle
{
    class Sounds
    {
        public static SoundPlayer Spell;
        public static SoundPlayer Pk;
        public static SoundPlayer Odds;
        public static SoundPlayer DeadVP;
        public static SoundPlayer PkTime1;
        public static SoundPlayer PkTime2;

        public void Dispose()
        {
            Pk.Dispose();
            Spell.Dispose();
            DeadVP.Dispose();
            PkTime1.Dispose();
            PkTime2.Dispose();
            Odds.Dispose();

            Pk = null;
            Spell = null;
            DeadVP = null;
            PkTime1 = null;
            PkTime2 = null;
            Odds = null;

            lib.SoundInstance--;
        }
        
        public void SoundsInit()
        {
            try
            {
                if (lib.SoundInstance < 1)
                {
                    Spell = new SoundPlayer(Defiance.Properties.Resources.spell);
                    Pk = new SoundPlayer(Defiance.Properties.Resources.pk);
                    Odds = new SoundPlayer(Defiance.Properties.Resources.overwhelmed);
                    DeadVP = new SoundPlayer(Defiance.Properties.Resources.death);
                    PkTime1 = new SoundPlayer(Defiance.Properties.Resources.pktimer1);
                    PkTime2 = new SoundPlayer(Defiance.Properties.Resources.pktimer2);
                    lib.SoundInstance++;
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }
    }
}