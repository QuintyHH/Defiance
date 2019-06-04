using Decal.Adapter;
using Decal.Adapter.Wrappers;
using Defiance.Utils;
using Defiance.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using VirindiViewService.Controls;

namespace Defiance.DeathHandle
{
    class DeathParse
    {
        public void ParseInit()
        {
            if (lib.DeathParseInstance < 1)
            {
                lib.OnDowntime = new List<string>();
                lib.OnDowntime2 = new List<string>();
                lib.MyCore.ChatBoxMessage += Parse_Death;        
                lib.DeathParseInstance++;
            }
        }

        public void Dispose()
        {
            lib.MyCore.ChatBoxMessage -= Parse_Death;
            lib.OnDowntime = null;
            lib.OnDowntime2 = null;

            if (redtimer != null)
            {
                redtimer.Dispose();
                redtimer = null;
            }
            lib.DeathParseInstance--;
        }

        private void Parse_Death(object sender, ChatTextInterceptEventArgs e)
        {
            try
            {
                if (e.Color == 0)
                {
                    WorldObject worldObject = CoreManager.Current.WorldFilter[CoreManager.Current.Actions.CurrentSelection];
                    string tempname;
                    if (worldObject != null)
                    {
                        tempname = worldObject.Name;
                    }
                    else
                    {
                        tempname = "unknown";
                    }

                    if (e.Text.Contains("Your assault sends ") && e.Text.Contains(" to an icy death!"))
                    {
                        string temp1 = e.Text.Split(new string[] { "Your assault sends " }, StringSplitOptions.None)[1];
                        string name = temp1.Split(new string[] { " to an icy death!" }, StringSplitOptions.None)[0];
                        if (name == tempname)
                        {
                            if (worldObject.ObjectClass == ObjectClass.Player && worldObject.Id != CoreManager.Current.CharacterFilter.Id)
                            {
                                AddPkObject(name, lib.MyName);
                            }
                        }
                    }

                    if (e.Text.Contains(" suffers a frozen fate!"))
                    {
                        string name = e.Text.Split(new string[] { " suffers a frozen fate!" }, StringSplitOptions.None)[0];
                        if (name == tempname)
                        {
                            if (worldObject.ObjectClass == ObjectClass.Player && worldObject.Id != CoreManager.Current.CharacterFilter.Id)
                            {
                                AddPkObject(name, lib.MyName);
                            }
                        }
                    }

                    if (e.Text.Contains("Your attack stops ") && e.Text.Contains(" cold!"))
                    {
                        string temp1 = e.Text.Split(new string[] { "Your attack stops " }, StringSplitOptions.None)[1];
                        string name = temp1.Split(new string[] { " cold!" }, StringSplitOptions.None)[0];
                        if (name == tempname)
                        {
                            if (worldObject.ObjectClass == ObjectClass.Player && worldObject.Id != CoreManager.Current.CharacterFilter.Id)
                            {
                                AddPkObject(name, lib.MyName);
                            }
                        }
                    }

                    if (e.Text.Contains("You reduced ") && e.Text.Contains(" to cinders!"))
                    {
                        string temp1 = e.Text.Split(new string[] { "You reduced " }, StringSplitOptions.None)[1];
                        string name = temp1.Split(new string[] { " to cinders!" }, StringSplitOptions.None)[0];
                        if (name == tempname)
                        {
                            if (worldObject.ObjectClass == ObjectClass.Player && worldObject.Id != CoreManager.Current.CharacterFilter.Id)
                            {
                                AddPkObject(name, lib.MyName);
                            }
                        }
                    }

                    if (e.Text.Contains(" is incinerated by your assault!"))
                    {
                        string name = e.Text.Split(new string[] { " is incinerated by your assault!" }, StringSplitOptions.None)[0];
                        if (name == tempname)
                        {
                            if (worldObject.ObjectClass == ObjectClass.Player && worldObject.Id != CoreManager.Current.CharacterFilter.Id)
                            {
                                AddPkObject(name, lib.MyName);
                            }
                        }
                    }

                    if (e.Text.Contains("'s seared corpse smolders before you!"))
                    {
                        string name = e.Text.Split(new string[] { "'s seared corpse smolders before you!" }, StringSplitOptions.None)[0];
                        if (name == tempname)
                        {
                            if (worldObject.ObjectClass == ObjectClass.Player && worldObject.Id != CoreManager.Current.CharacterFilter.Id)
                            {
                                AddPkObject(name, lib.MyName);
                            }
                        }
                    }

                    if (e.Text.Contains("You bring ") && e.Text.Contains(" to a fiery end!"))
                    {
                        string temp1 = e.Text.Split(new string[] { "You bring " }, StringSplitOptions.None)[1];
                        string name = temp1.Split(new string[] { " to a fiery end!" }, StringSplitOptions.None)[0];
                        if (name == tempname)
                        {
                            if (worldObject.ObjectClass == ObjectClass.Player && worldObject.Id != CoreManager.Current.CharacterFilter.Id)
                            {
                                AddPkObject(name, lib.MyName);
                            }
                        }
                    }

                    if (e.Text.Contains("Your lightning coruscates over ") && e.Text.Contains("'s mortal remains!"))
                    {
                        string temp1 = e.Text.Split(new string[] { "Your lightning coruscates over " }, StringSplitOptions.None)[1];
                        string name = temp1.Split(new string[] { "'s mortal remains!" }, StringSplitOptions.None)[0];
                        if (name == tempname)
                        {
                            if (worldObject.ObjectClass == ObjectClass.Player && worldObject.Id != CoreManager.Current.CharacterFilter.Id)
                            {
                                AddPkObject(name, lib.MyName);
                            }
                        }
                    }

                    if (e.Text.Contains("Electricity tears ") && e.Text.Contains(" apart!"))
                    {
                        string temp1 = e.Text.Split(new string[] { "Electricity tears " }, StringSplitOptions.None)[1];
                        string name = temp1.Split(new string[] { " apart!" }, StringSplitOptions.None)[0];
                        if (name == tempname)
                        {
                            if (worldObject.ObjectClass == ObjectClass.Player && worldObject.Id != CoreManager.Current.CharacterFilter.Id)
                            {
                                AddPkObject(name, lib.MyName);
                            }
                        }
                    }

                    if (e.Text.Contains("You reduce ") && e.Text.Contains(" to a sizzling, oozing mass!"))
                    {
                        string temp1 = e.Text.Split(new string[] { "You reduce " }, StringSplitOptions.None)[1];
                        string name = temp1.Split(new string[] { " to a sizzling, oozing mass!" }, StringSplitOptions.None)[0];
                        if (name == tempname)
                        {
                            if (worldObject.ObjectClass == ObjectClass.Player && worldObject.Id != CoreManager.Current.CharacterFilter.Id)
                            {
                                AddPkObject(name, lib.MyName);
                            }
                        }
                    }

                    if (e.Text.Contains("'s last strength dissolves before you!"))
                    {
                        string name = e.Text.Split(new string[] { "'s last strength dissolves before you!" }, StringSplitOptions.None)[0];
                        if (name == tempname)
                        {
                            if (worldObject.ObjectClass == ObjectClass.Player && worldObject.Id != CoreManager.Current.CharacterFilter.Id)
                            {
                                AddPkObject(name, lib.MyName);
                            }
                        }
                    }

                    if (e.Text.Contains(" is liquified by your attack!"))
                    {
                        string name = e.Text.Split(new string[] { " is liquified by your attack!" }, StringSplitOptions.None)[0];
                        if (name == tempname)
                        {
                            if (worldObject.ObjectClass == ObjectClass.Player && worldObject.Id != CoreManager.Current.CharacterFilter.Id)
                            {
                                AddPkObject(name, lib.MyName);
                            }
                        }
                    }

                    if (e.Text.Contains("You knock ") && e.Text.Contains(" into next Morningthaw"))
                    {
                        string temp1 = e.Text.Split(new string[] { "You knock " }, StringSplitOptions.None)[1];
                        string name = temp1.Split(new string[] { " into next Morningthaw" }, StringSplitOptions.None)[0];
                        if (name == tempname)
                        {
                            if (worldObject.ObjectClass == ObjectClass.Player && worldObject.Id != CoreManager.Current.CharacterFilter.Id)
                            {
                                AddPkObject(name, lib.MyName);
                            }
                        }
                    }

                    if (e.Text.Contains(" is utterly destroyed by your attack!"))
                    {
                        string name = e.Text.Split(new string[] { " is utterly destroyed by your attack!" }, StringSplitOptions.None)[0];
                        if (name == tempname)
                        {
                            if (worldObject.ObjectClass == ObjectClass.Player && worldObject.Id != CoreManager.Current.CharacterFilter.Id)
                            {
                                AddPkObject(name, lib.MyName);
                            }
                        }
                    }

                    if (e.Text.Contains("You beat ") && e.Text.Contains(" to a lifeless pulp!"))
                    {
                        string temp1 = e.Text.Split(new string[] { "You beat " }, StringSplitOptions.None)[1];
                        string name = temp1.Split(new string[] { " to a lifeless pulp!" }, StringSplitOptions.None)[0];
                        if (name == tempname)
                        {
                            if (worldObject.ObjectClass == ObjectClass.Player && worldObject.Id != CoreManager.Current.CharacterFilter.Id)
                            {
                                AddPkObject(name, lib.MyName);
                            }
                        }
                    }

                    if (e.Text.Contains("You flatten ") && e.Text.Contains("'s body with the force of your assault!"))
                    {
                        string temp1 = e.Text.Split(new string[] { "You flatten " }, StringSplitOptions.None)[1];
                        string name = temp1.Split(new string[] { "'s body with the force of your assault!" }, StringSplitOptions.None)[0];
                        if (name == tempname)
                        {
                            if (worldObject.ObjectClass == ObjectClass.Player && worldObject.Id != CoreManager.Current.CharacterFilter.Id)
                            {
                                AddPkObject(name, lib.MyName);
                            }
                        }
                    }

                    if (e.Text.Contains("The thunder of crushing ") && e.Text.Contains(" is followed by the deafening silence of death!"))
                    {
                        string temp1 = e.Text.Split(new string[] { "The thunder of crushing " }, StringSplitOptions.None)[1];
                        string name = temp1.Split(new string[] { " is followed by the deafening silence of death!" }, StringSplitOptions.None)[0];
                        if (name == tempname)
                        {
                            if (worldObject.ObjectClass == ObjectClass.Player && worldObject.Id != CoreManager.Current.CharacterFilter.Id)
                            {
                                AddPkObject(name, lib.MyName);
                            }
                        }
                    }

                    if (e.Text.Contains("'s perforated corpse falls before you!"))
                    {
                        string name = e.Text.Split(new string[] { "'s perforated corpse falls before you!" }, StringSplitOptions.None)[0];
                        if (name == tempname)
                        {
                            if (worldObject.ObjectClass == ObjectClass.Player && worldObject.Id != CoreManager.Current.CharacterFilter.Id)
                            {
                                AddPkObject(name, lib.MyName);
                            }
                        }
                    }

                    if (e.Text.Contains("You killed "))
                    {
                        string temp = e.Text.Split(new string[] { "You killed " }, StringSplitOptions.None)[1];
                        string name = temp.Split(new string[] { "!" }, StringSplitOptions.None)[0];
                        if (name == tempname)
                        {
                            if (worldObject.ObjectClass == ObjectClass.Player && worldObject.Id != CoreManager.Current.CharacterFilter.Id)
                            {
                                AddPkObject(name, lib.MyName);
                            }
                        }
                    }

                    if (e.Text.Contains("You run ") && e.Text.Contains("through!"))
                    {
                        string temp1 = e.Text.Split(new string[] { "You run " }, StringSplitOptions.None)[1];
                        string name = temp1.Split(new string[] { " through" }, StringSplitOptions.None)[0];
                        if (name == tempname)
                        {
                            if (worldObject.ObjectClass == ObjectClass.Player && worldObject.Id != CoreManager.Current.CharacterFilter.Id)
                            {
                                AddPkObject(name, lib.MyName);
                            }
                        }
                    }

                    if (e.Text.Contains(" is fatally punctured!"))
                    {
                        string name = e.Text.Split(new string[] { " is fatally punctured!" }, StringSplitOptions.None)[0];
                        if (name == tempname)
                        {
                            if (worldObject.ObjectClass == ObjectClass.Player && worldObject.Id != CoreManager.Current.CharacterFilter.Id)
                            {
                                AddPkObject(name, lib.MyName);
                            }
                        }
                    }

                    if (e.Text.Contains("'s death is preceded by a sharp, stabbing pain!"))
                    {
                        string name = e.Text.Split(new string[] { "'s death is preceded by a sharp, stabbing pain!" }, StringSplitOptions.None)[0];
                        if (name == tempname)
                        {
                            if (worldObject.ObjectClass == ObjectClass.Player && worldObject.Id != CoreManager.Current.CharacterFilter.Id)
                            {
                                AddPkObject(name, lib.MyName);
                            }
                        }
                    }

                    if (e.Text.Contains("You split ") && e.Text.Contains(" apart!"))
                    {
                        string temp1 = e.Text.Split(new string[] { "You split " }, StringSplitOptions.None)[1];
                        string name = temp1.Split(new string[] { " apart!" }, StringSplitOptions.None)[0];
                        if (name == tempname)
                        {
                            if (worldObject.ObjectClass == ObjectClass.Player && worldObject.Id != CoreManager.Current.CharacterFilter.Id)
                            {
                                AddPkObject(name, lib.MyName);
                            }
                        }
                    }

                    if (e.Text.Contains("You slay ") && e.Text.Contains(" viciously enough to impart death several times over!"))
                    {
                        string temp1 = e.Text.Split(new string[] { "You slay " }, StringSplitOptions.None)[1];
                        string name = temp1.Split(new string[] { " viciously enough to impart death several times over!" }, StringSplitOptions.None)[0];
                        if (name == tempname)
                        {
                            if (worldObject.ObjectClass == ObjectClass.Player && worldObject.Id != CoreManager.Current.CharacterFilter.Id)
                            {
                                AddPkObject(name, lib.MyName);
                            }
                        }
                    }

                    if (e.Text.Contains("You cleave ") && e.Text.Contains(" in twain!"))
                    {
                        string temp1 = e.Text.Split(new string[] { "You cleave " }, StringSplitOptions.None)[1];
                        string name = temp1.Split(new string[] { " in twain!" }, StringSplitOptions.None)[0];
                        if (name == tempname)
                        {
                            if (worldObject.ObjectClass == ObjectClass.Player && worldObject.Id != CoreManager.Current.CharacterFilter.Id)
                            {
                                AddPkObject(name, lib.MyName);
                            }
                        }
                    }

                    if (e.Text.Contains("'s electricity tears") && e.Text.Contains(" apart!"))
                    {
                        string temp1 = e.Text.Split(new string[] { "'s electricity tears " }, StringSplitOptions.None)[1];
                        string killer = e.Text.Split(new string[] { "'s electricity tears " }, StringSplitOptions.None)[0];
                        string name = temp1.Split(new string[] { " apart!" }, StringSplitOptions.None)[0];
                        AddPkObject(name, killer);
                    }

                    if (e.Text.Contains("'s lightning coruscates over ") && e.Text.Contains("'s mortal remains"))
                    {
                        string temp1 = e.Text.Split(new string[] { "'s lightning coruscates over " }, StringSplitOptions.None)[1];
                        string killer = e.Text.Split(new string[] { "'s lightning coruscates over " }, StringSplitOptions.None)[0];
                        string name = temp1.Split(new string[] { "'s mortal remains" }, StringSplitOptions.None)[0];
                        AddPkObject(name, killer);
                    }

                    if (e.Text.Contains(" knocks ") && e.Text.Contains(" into next Morningthaw"))
                    {
                        string temp1 = e.Text.Split(new string[] { " knocks " }, StringSplitOptions.None)[1];
                        string killer = e.Text.Split(new string[] { " knocks " }, StringSplitOptions.None)[0];
                        string name = temp1.Split(new string[] { " into" }, StringSplitOptions.None)[0];
                        AddPkObject(name, killer);
                    }

                    if (e.Text.Contains(" is incinerated by ") && e.Text.Contains("'s assault!"))
                    {
                        string name = e.Text.Split(new string[] { " is incinerated by " }, StringSplitOptions.None)[0];
                        string temp1 = e.Text.Split(new string[] { " is incinerated by " }, StringSplitOptions.None)[1];
                        string killer = temp1.Split(new string[] { "'s assault!" }, StringSplitOptions.None)[0];
                        AddPkObject(name, killer);
                    }

                    if (e.Text.Contains(" slays ") && e.Text.Contains(" viciously enough to impart death several times over!"))
                    {
                        string temp1 = e.Text.Split(new string[] { " slays " }, StringSplitOptions.None)[1];
                        string killer = e.Text.Split(new string[] { " slays " }, StringSplitOptions.None)[0];
                        string name = temp1.Split(new string[] { " viciously enough" }, StringSplitOptions.None)[0];
                        AddPkObject(name, killer);
                    }

                    if (e.Text.Contains(" is utterly destroyed by ") && !e.Text.Contains("your attack!"))
                    {
                        string name = e.Text.Split(new string[] { " is utterly destroyed by " }, StringSplitOptions.None)[0];
                        string temp1 = e.Text.Split(new string[] { " is utterly destroyed by " }, StringSplitOptions.None)[1];
                        string killer = temp1.Split(new string[] { "'s attack!" }, StringSplitOptions.None)[0];
                        AddPkObject(name, killer);
                    }

                    if (e.Text.Contains(" reduced ") && e.Text.Contains(" to cinders!") && !e.Text.Contains("You reduced"))
                    {
                        string temp1 = e.Text.Split(new string[] { " reduced " }, StringSplitOptions.None)[1];
                        string killer = e.Text.Split(new string[] { " reduced " }, StringSplitOptions.None)[0];
                        string name = temp1.Split(new string[] { " to cinders!" }, StringSplitOptions.None)[0];
                        AddPkObject(name, killer);
                    }

                    if (e.Text.Contains(" brings ") && e.Text.Contains(" to a fiery end!"))
                    {
                        string temp1 = e.Text.Split(new string[] { " brings " }, StringSplitOptions.None)[1];
                        string killer = e.Text.Split(new string[] { " brings " }, StringSplitOptions.None)[0];
                        string name = temp1.Split(new string[] { " to a fiery end!" }, StringSplitOptions.None)[0];
                        AddPkObject(name, killer);
                    }

                    if (e.Text.Contains("'s seared corpse smolders before ") && !e.Text.Contains("'s seared corpse smolders before you!"))
                    {
                        string name = e.Text.Split(new string[] { "'s seared corpse smolders before " }, StringSplitOptions.None)[0];
                        string temp1 = e.Text.Split(new string[] { "'s seared corpse smolders before " }, StringSplitOptions.None)[1];
                        string killer = temp1.Split(new string[] { "!" }, StringSplitOptions.None)[0];
                        AddPkObject(name, killer);
                    }

                    if (e.Text.Contains(" runs ") && e.Text.Contains(" through!"))
                    {
                        string temp1 = e.Text.Split(new string[] { " runs " }, StringSplitOptions.None)[1];
                        string killer = e.Text.Split(new string[] { " runs " }, StringSplitOptions.None)[0];
                        string name = temp1.Split(new string[] { " through!" }, StringSplitOptions.None)[0];
                        AddPkObject(name, killer);
                    }

                    if (e.Text.Contains(" is liquified by ") && !e.Text.Contains(" is liquified by your attack!"))
                    {
                        string name = e.Text.Split(new string[] { " is liquified by " }, StringSplitOptions.None)[0];
                        string temp1 = e.Text.Split(new string[] { " is liquified by " }, StringSplitOptions.None)[1];
                        string killer = temp1.Split(new string[] { "'s attack!" }, StringSplitOptions.None)[0];
                        AddPkObject(name, killer);
                    }

                    if (e.Text.Contains(" died!"))
                    {
                        string name = e.Text.Split(new string[] { " died!" }, StringSplitOptions.None)[0];
                        string killer = "Unknown";
                        AddPkObject(name, killer);
                    }

                    if (e.Text.Contains(" splits ") && e.Text.Contains(" apart!"))
                    {
                        string temp1 = e.Text.Split(new string[] { " splits " }, StringSplitOptions.None)[1];
                        string killer = e.Text.Split(new string[] { " splits " }, StringSplitOptions.None)[0];
                        string name = temp1.Split(new string[] { " apart!" }, StringSplitOptions.None)[0];
                        AddPkObject(name, killer);
                    }

                    if (e.Text.Contains("'s last strength dissolves before ") && !e.Text.Contains("'s last strength dissolves before you!"))
                    {
                        string name = e.Text.Split(new string[] { "'s last strength dissolves before " }, StringSplitOptions.None)[0];
                        string temp1 = e.Text.Split(new string[] { "'s last strength dissolves before " }, StringSplitOptions.None)[1];
                        string killer = temp1.Split(new string[] { "!" }, StringSplitOptions.None)[0];
                        AddPkObject(name, killer);
                    }

                    if (e.Text.Contains(" suffers a frozen fate at the hands of "))
                    {
                        string name = e.Text.Split(new string[] { " suffers a frozen fate at the hands of " }, StringSplitOptions.None)[0];
                        string temp1 = e.Text.Split(new string[] { " suffers a frozen fate at the hands of " }, StringSplitOptions.None)[1];
                        string killer = temp1.Split(new string[] { "!" }, StringSplitOptions.None)[0];
                        AddPkObject(name, killer);
                    }

                    if (e.Text.Contains(" beats ") && e.Text.Contains(" to a lifeless pulp!"))
                    {
                        string temp1 = e.Text.Split(new string[] { " beats " }, StringSplitOptions.None)[1];
                        string killer = e.Text.Split(new string[] { " beats " }, StringSplitOptions.None)[0];
                        string name = temp1.Split(new string[] { " to a lifeless pulp" }, StringSplitOptions.None)[0];
                        AddPkObject(name, killer);
                    }

                    if (e.Text.Contains(" reduces ") && e.Text.Contains(" to a sizzling, oozing mass!"))
                    {
                        string temp1 = e.Text.Split(new string[] { " reduces " }, StringSplitOptions.None)[1];
                        string killer = e.Text.Split(new string[] { " reduces " }, StringSplitOptions.None)[0];
                        string name = temp1.Split(new string[] { " to a sizzling, oozing mass!" }, StringSplitOptions.None)[0];
                        AddPkObject(name, killer);
                    }

                    if (e.Text.Contains(" slayed "))
                    {
                        string temp1 = e.Text.Split(new string[] { " slayed " }, StringSplitOptions.None)[1];
                        string killer = e.Text.Split(new string[] { " slayed " }, StringSplitOptions.None)[0];
                        string name = temp1.Split(new string[] { "!" }, StringSplitOptions.None)[0];
                        AddPkObject(name, killer);
                    }

                    if (e.Text.Contains(" is fatally punctured by "))
                    {
                        string name = e.Text.Split(new string[] { " is fatally punctured by " }, StringSplitOptions.None)[0];
                        string temp1 = e.Text.Split(new string[] { " is fatally punctured by " }, StringSplitOptions.None)[1];
                        string killer = temp1.Split(new string[] { "!" }, StringSplitOptions.None)[0];
                        AddPkObject(name, killer);
                    }

                    if (e.Text.Contains(" stops ") && e.Text.Contains(" cold!") && !e.Text.Contains("Your attack "))
                    {
                        string temp1 = e.Text.Split(new string[] { " stops " }, StringSplitOptions.None)[1];
                        string killer = e.Text.Split(new string[] { " stops " }, StringSplitOptions.None)[0];
                        string name = temp1.Split(new string[] { " cold!" }, StringSplitOptions.None)[0];
                        AddPkObject(name, killer);
                    }

                    if (e.Text.Contains("'s death is preceded by a sharp, stabbing pain courtesy of "))
                    {
                        string name = e.Text.Split(new string[] { "'s death is preceded by a sharp, stabbing pain courtesy of " }, StringSplitOptions.None)[0];
                        string temp1 = e.Text.Split(new string[] { "'s death is preceded by a sharp, stabbing pain courtesy of " }, StringSplitOptions.None)[1];
                        string killer = temp1.Split(new string[] { "!" }, StringSplitOptions.None)[0];
                        AddPkObject(name, killer);
                    }

                    if (e.Text.Contains(" cleaves ") && e.Text.Contains(" in twain!"))
                    {
                        string temp1 = e.Text.Split(new string[] { " cleaves " }, StringSplitOptions.None)[1];
                        string killer = e.Text.Split(new string[] { " cleaves " }, StringSplitOptions.None)[0];
                        string name = temp1.Split(new string[] { " in twain!" }, StringSplitOptions.None)[0];
                        AddPkObject(name, killer);
                    }

                    if (e.Text.Contains("'s thunder of crushing ") && e.Text.Contains(" is followed by the deafening silence of death!"))
                    {
                        string temp1 = e.Text.Split(new string[] { "'s thunder of crushing " }, StringSplitOptions.None)[1];
                        string killer = e.Text.Split(new string[] { "'s thunder of crushing " }, StringSplitOptions.None)[0];
                        string name = temp1.Split(new string[] { " is followed by the deafening silence of death!" }, StringSplitOptions.None)[0];
                        AddPkObject(name, killer);
                    }

                    if (e.Text.Contains("'s assault sends ") && e.Text.Contains(" to an icy death!"))
                    {
                        string temp1 = e.Text.Split(new string[] { "'s assault sends " }, StringSplitOptions.None)[1];
                        string killer = e.Text.Split(new string[] { "'s assault sends " }, StringSplitOptions.None)[0];
                        string name = temp1.Split(new string[] { " to an icy death!" }, StringSplitOptions.None)[0];
                        AddPkObject(name, killer);
                    }

                    if (e.Text.Contains("'s perforated corpse falls before ") && !e.Text.Contains("'s perforated corpse falls before you!"))
                    {
                        string name = e.Text.Split(new string[] { "'s perforated corpse falls before " }, StringSplitOptions.None)[0];
                        string temp1 = e.Text.Split(new string[] { "'s perforated corpse falls before " }, StringSplitOptions.None)[1];
                        string killer = temp1.Split(new string[] { "!" }, StringSplitOptions.None)[0];
                        AddPkObject(name, killer);
                    }
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        private void AddPkObject(string name,string killer)
        {
            try
            {
                if (name != "you" && name != "You")
                {
                    DateTime deathtime = DateTime.Now;
                    int num2 = 0;
                    if (MainView.TimerList.RowCount >= 0)
                    {
                        num2 = MainView.TimerList.RowCount;
                        for (int j = 0; j < MainView.TimerList.RowCount; j++)
                        {
                            if (((HudStaticText)MainView.TimerList[j][1]).Text.Trim().CompareTo(name.Trim()) > 0)
                            {
                                num2 = j;
                                break;
                            }
                        }
                    }

                    HudList.HudListRowAccessor hudListRowAccessor = MainView.TimerList.InsertRow(num2);

                    ((HudPictureBox)hudListRowAccessor[0]).Image = 100667896;
                    ((HudStaticText)hudListRowAccessor[1]).Text = name;
                    ((HudStaticText)hudListRowAccessor[1]).TextColor = Color.LawnGreen;
                    ((HudStaticText)hudListRowAccessor[2]).Text = "Just died!";
                    ((HudStaticText)hudListRowAccessor[2]).TextColor = Color.LawnGreen;
                    ((HudStaticText)hudListRowAccessor[3]).Text = deathtime.ToString("h:mm:ss tt");
                    ((HudStaticText)hudListRowAccessor[3]).TextColor = Color.LawnGreen;
                    ((HudStaticText)hudListRowAccessor[5]).Text = killer;
                    ((HudStaticText)hudListRowAccessor[5]).Visible = false;

                    redtimer = new RedTimer(name, deathtime, killer);
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); ; }
        }

        public static RedTimer redtimer;
    }    
}