using Decal.Adapter.Wrappers;
using Defiance.Utils;
using System;

namespace Defiance.LogHandle
{
    class Components
    {
        public static void CompsInit()
        {
            if (lib.CompsInstance < 1)
            {
                lib.MyCore.CharacterFilter.SpellCast += CompCheck;
                lib.CompsInstance++;
            }
        }

        public static void Dispose()
        {
            lib.MyCore.CharacterFilter.SpellCast -= CompCheck;
            lib.CompsInstance--;
        }

        private static void CompCheck(object sender, EventArgs e)
        {
            try
            {
                if (lib.Comps > 0)
                {
                    int temptapers = 0;
                    foreach (WorldObject taper in lib.MyCore.WorldFilter.GetInventory())
                    {
                        if (taper.Name == "Prismatic Taper")
                        {
                            temptapers = temptapers + taper.Values(LongValueKey.StackCount);
                        }
                    }
                    if (temptapers <= lib.Comps)
                    {
                        if (lib.Mode == 3)
                        {
                            lib.reason = "low components";
                            LogMethod.Logout();
                        }
                        else if (lib.Mode != 3)
                        {
                            Utility.AddChatText("WARNING - " + temptapers + " tapers left!", 6);
                        }
                    }
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }
    }
}
