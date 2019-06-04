using Decal.Adapter;
using Defiance.Utils;
using System;

namespace Defiance.MacroHandle
{
    class Targetting
    {
        public static void Core_RemoteTarget(object sender, ChatTextInterceptEventArgs x)
        {
            try
            {
                if (lib.Mode == 0 && x.Text.Contains("TargetID: ") && x.Color == 3)
                {
                    int num = int.Parse(x.Text.Split(new string[] { ": " }, StringSplitOptions.None)[3]);
                    if (lib.MyCore.Actions.IsValidObject(num))
                    {
                        CoreManager.Current.Actions.SelectItem(num);
                    }
                }     
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }
    }
}
