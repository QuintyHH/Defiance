using Defiance.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;

namespace Defiance.BaseHandle
{
    class VersionControl
    {
        public void Dispose()
        {
            lib.MyCore.CharacterFilter.LoginComplete -= VersCheckLogin;
            lib.VersInstance--;
        }

        public void InitVersCheck()
        {
            try
            {
                if (lib.VersInstance < 1)
                {
                    if (lib.gameStatus < 3)
                    {
                        lib.MyCore.CharacterFilter.LoginComplete += VersCheckLogin;
                    }
                    else if (lib.gameStatus >= 3)
                    {
                        VersCheck();
                    }
                    lib.VersInstance++;
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        public void VersCheckLogin(object sender, EventArgs e)
        {
            VersCheck();
        }

        public void VersCheck()
        {
            List<string> list = (from f in lib.AuthIds.Split(new char[] { ',' })
                                 select f.Trim()).ToList<string>();

            if (lib.AuthIds == "failed")
            {
                if (lib.DF2TEXT != null)
                {
                    lib.DF2TEXT.Visible = false;
                    lib.DF2TEXT.Dispose();
                    lib.DF2TEXT = null;
                }
                else if (lib.DF2TEXT == null)
                {
                    float num = lib.MyHost.Underlying.Hooks.ObjectHeight(lib.MyID);
                    lib.DF2TEXT = lib.MyCore.D3DService.MarkObjectWith3DText(lib.MyCore.CharacterFilter.Id, "Temporary Auth!", "Arial", 0);
                    lib.DF2TEXT.Anchor(lib.MyCore.CharacterFilter.Id, 0.5f, 0f, 0f, num - 0.05f);
                    lib.DF2TEXT.Color = Color.HotPink.ToArgb();
                    lib.DF2TEXT.OrientToCamera(true);
                    lib.DF2TEXT.ScaleX = 0.4f;
                    lib.DF2TEXT.ScaleZ = 0.4f;
                    lib.DF2TEXT.ScaleY = 0.4f;
                    lib.DF2TEXT.Visible = true;
                }
            }
            else
            {
                lib.versid = list.Contains("version::" + Assembly.GetExecutingAssembly().GetName().Version.ToString());

                if (lib.versid == false)
                {
                    Utility.AddChatText("Out-dated. Please contact a Core for further instructions.", 6);
                    Utility.AddWindowText("Out-dated. Please check Discord.");

                    if (lib.DF2TEXT != null)
                    {
                        lib.DF2TEXT.Visible = false;
                        lib.DF2TEXT.Dispose();
                        lib.DF2TEXT = null;
                    }
                    else if (lib.DF2TEXT == null)
                    {
                        float num = lib.MyHost.Underlying.Hooks.ObjectHeight(lib.MyID);
                        lib.DF2TEXT = lib.MyCore.D3DService.MarkObjectWith3DText(lib.MyCore.CharacterFilter.Id, "Defiance Out-Dated!", "Times New Roman", 0);
                        lib.DF2TEXT.Anchor(lib.MyCore.CharacterFilter.Id, 0.5f, 0f, 0f, num - 0.05f);
                        lib.DF2TEXT.Color = Color.HotPink.ToArgb();
                        lib.DF2TEXT.OrientToCamera(true);
                        lib.DF2TEXT.ScaleX = 0.4f;
                        lib.DF2TEXT.ScaleZ = 0.4f;
                        lib.DF2TEXT.ScaleY = 0.4f;
                        lib.DF2TEXT.Visible = true;
                    }
                }
            }
            list = null;
        }
    }
}