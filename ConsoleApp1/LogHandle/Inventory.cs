using Decal.Adapter.Wrappers;
using Defiance.Utils;
using System;

namespace Defiance.LogHandle
{
    class Inventory
    {
        public static void InventoryInit()
        {
            if (lib.InventoryInstance < 1)
            {
                lib.MyCore.CharacterFilter.ActionComplete += InvCheck;
                lib.InventoryInstance++;
            }
        }

        public static void Dispose()
        {
            lib.MyCore.CharacterFilter.ActionComplete -= InvCheck;
            lib.InventoryInstance--;
        }

        private static void InvCheck(object sender, EventArgs e)
        {
            try
            {
                if (lib.Slots > 0)
                {
                    if (lib.Mode == 3)
                    {
                        int slotcount = 0;
                        int totalslots = 0;
                        int totalitem = 0;

                        foreach (WorldObject item in lib.MyCore.WorldFilter.GetInventory())
                        {
                            if (item.ObjectClass == ObjectClass.Container)
                            {
                                slotcount = slotcount + item.Values(LongValueKey.ItemSlots);
                            }
                            if (item.ObjectClass != ObjectClass.Container && item.ObjectClass != ObjectClass.Foci && item.Values(LongValueKey.EquippedSlots) == 0)
                            {
                                totalitem = totalitem + 1;
                            }
                        }

                        totalslots = slotcount + 102;

                        if (totalitem >= (totalslots - lib.Slots))
                        {
                            lib.reason = "inventory space";
                            LogMethod.Logout();
                        }
                    }
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }
    }
}
