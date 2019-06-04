using Decal.Adapter;
using Decal.Adapter.Wrappers;
using Defiance.BaseHandle;
using Defiance.Utils;
using System;

namespace Defiance.Scanning
{
    class AttackDetection
    {
        public static void ServerDispatch_EchoFilter(object sender, NetworkMessageEventArgs e)
        {
            try
            {
                int num = e.Message.Type;
                if (num == 63308)
                {
                    AnimationChecks(e.Message.Value<int>("object"), e.Message.Value<int>("target"), e.Message.Value<short>("stance"), e.Message.Value<byte>("animation_type"), e.Message.Value<int>("flags"));
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public static void AnimationChecks(int CharacterGUID, int TargetGUID, short TargetStance, byte TargetAnimation, int TargetFlags)
        {
            try
            {
                if (lib.gameStatus >= 3)
                {
                    if (TargetGUID == lib.MyCore.CharacterFilter.Id && TargetStance == 73 && TargetAnimation == 8)
                    {
                        if (lib.MyCore.Actions.IsValidObject(CharacterGUID))
                        {
                            WorldObject obj = lib.MyCore.WorldFilter[CharacterGUID];
                            if (obj.ObjectClass == ObjectClass.Player)
                            {

                                Utility.AddChatText(obj.Name + " IS CASTING A SPELL AT YOU!", 10);
                                if (lib.UseAlertSS == true)
                                {
                                    Sounds.Spell.Play();
                                }
                            }
                        }
                    }

                    if (TargetGUID == lib.MyCore.CharacterFilter.Id && (TargetStance == 60 || TargetStance == 62 || TargetStance == 64) && (TargetAnimation == 6 || TargetAnimation == 8))
                    {
                        if (lib.MyCore.Actions.IsValidObject(CharacterGUID))
                        {
                            WorldObject obj = lib.MyCore.WorldFilter[CharacterGUID];
                            if (obj.ObjectClass == ObjectClass.Player)
                            {

                                Utility.AddChatText(obj.Name + " IS ATTACKING YOU!", 10);
                                if (lib.UseAlertSS == true)
                                {
                                    Sounds.Spell.Play();
                                }
                            }
                        }
                    }

                    if (TargetGUID == lib.MyCore.CharacterFilter.Id && TargetStance == 63 && TargetAnimation == 8)
                    {
                        if (lib.MyCore.Actions.IsValidObject(CharacterGUID))
                        {
                            WorldObject obj = lib.MyCore.WorldFilter[CharacterGUID];
                            if (obj.ObjectClass == ObjectClass.Player)
                            {

                                Utility.AddChatText(obj.Name + " IS SHOOTING AT YOU!", 10);
                                if (lib.UseAlertSS == true)
                                {
                                    Sounds.Spell.Play();
                                }
                            }
                        }
                    }                    
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }
    }
}
