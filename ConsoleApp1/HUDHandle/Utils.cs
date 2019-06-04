using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using Decal.Adapter.Wrappers;
using Defiance.Utils;

namespace Defiance.HUDHandle
{
	public static class Utils
	{
		public static DebuffDB InitialiseDebuffDB()
        {
            DebuffDB debuffDB = new DebuffDB();
            try
			{
                DebuffEffect debuffEffect = new DebuffEffect();

                Assembly assembly = Assembly.GetExecutingAssembly();
                const string PATH = "Defiance.Resources.effects.txt";
                using (Stream stream = assembly.GetManifestResourceStream(PATH))
                {
                    using (StreamReader streamReader = new StreamReader(stream))
                    {
                        while (!streamReader.EndOfStream)
                        {
                            try
                            {
                                string text = streamReader.ReadLine();
                                if (text.StartsWith("EFFECT"))
                                {
                                    debuffEffect = new DebuffEffect();
                                    debuffEffect.EffectId = Convert.ToInt32(text.Replace("EFFECT=", "").Trim());
                                }
                                else if (text.StartsWith("Category"))
                                {
                                    string text2 = text.Replace("Category=", "").Trim();
                                    string a;
                                    if ((a = text2) != null)
                                    {
                                        if (!(a == "Life"))
                                        {
                                            if (a == "Creature")
                                            {
                                                debuffEffect.DebuffCategory = DebuffCategories.Creature;
                                            }
                                        }
                                        else
                                        {
                                            debuffEffect.DebuffCategory = DebuffCategories.Life;
                                        }
                                    }
                                }
                                else if (text.StartsWith("DefaultDebuffDuration"))
                                {
                                    debuffEffect.DefaultDuration = Convert.ToInt32(text.Replace("DefaultDebuffDuration=", "").Trim());
                                }
                                else if (text.StartsWith("DisplayName"))
                                {
                                    debuffEffect.DisplayName = text.Replace("DisplayName=", "").Trim();
                                }
                                else if (text.StartsWith("SpellWords"))
                                {
                                    debuffEffect.SpellWords = text.Replace("SpellWords=", "").Trim();
                                }
                                else if (text.StartsWith("Icon"))
                                {
                                    debuffEffect.Icon = Convert.ToInt32(text.Replace("Icon=", "").Trim());
                                }
                                else if (text.StartsWith("-DEBUFF"))
                                {
                                    DebuffEffectDetails debuffEffectDetails = new DebuffEffectDetails();
                                    text = text.Replace("-DEBUFF|", "");
                                    string[] array = text.Split(new string[]
                                    {
                                        "|"
                                    }, StringSplitOptions.None);
                                    if (array.Length > 0)
                                    {
                                        debuffEffectDetails.DisplayName = array[0];
                                    }
                                    if (array.Length > 1 && array[1].Length > 0)
                                    {
                                        debuffEffectDetails.DefaultDuration = Convert.ToInt32(array[1]);
                                    }
                                    if (array.Length > 2)
                                    {
                                        debuffEffectDetails.SpellWords = array[2];
                                    }
                                    if (array.Length > 4)
                                    {
                                        debuffEffectDetails.Icon = Convert.ToInt32(array[4].Trim());
                                    }
                                    debuffEffect.EffectDetails.Add(debuffEffectDetails);
                                }
                                else if (text.StartsWith("ENDEFFECT"))
                                {
                                    debuffDB.DebuffEffects.Add(debuffEffect);
                                }
                            }
                            catch (Exception ex) { Repo.RecordException(ex); }
                        }
                    }
                }		
			}
            catch (Exception ex2) { Repo.RecordException(ex2); }
            return debuffDB;
		}        

        public static int ConvertToInteger(this float value, int defaultValue = 0)
		{
			int result;
			try
			{
				result = Convert.ToInt32(Math.Ceiling((double)value));
			}
			catch
			{
				result = defaultValue;
			}
			return result;
		}

		public static Bitmap Resize(this Bitmap imgToResize, Size size)
		{
			Bitmap result;
			try
			{
				Bitmap bitmap = new Bitmap(size.Width, size.Height);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
					graphics.DrawImage(imgToResize, 0, 0, size.Width, size.Height);
				}
				result = bitmap;
			}
			catch
			{
				throw;
			}
			return result;
		}

		public static Rectangle DrawText(string text, Font font, Pen pen, out Bitmap result)
		{
			result = null;
			Rectangle result3;
			try
			{
				using (Bitmap bitmap = new Bitmap(50, 50))
				{
					using (Graphics graphics = Graphics.FromImage(bitmap))
					{
						SizeF sizeF = graphics.MeasureString(text, font);
						if (!sizeF.IsEmpty)
						{
							Rectangle result2 = new Rectangle(0, 0, sizeF.ToSize().Width, sizeF.ToSize().Height);
							result = new Bitmap(result2.Width, result2.Height);
							using (Graphics graphics2 = Graphics.FromImage(result))
							{
								graphics2.SmoothingMode = SmoothingMode.AntiAlias;
								graphics2.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
								graphics2.PixelOffsetMode = PixelOffsetMode.HighQuality;
								graphics2.Clear(Color.Transparent);
								pen.Width.ConvertToInteger(0);
								graphics2.DrawString(text, font, pen.Brush, new PointF(0f, 0f));
								graphics2.Flush();
								return result2;
							}
						}
						result3 = default(Rectangle);
					}
				}
			}
			catch (Exception ex)
			{
                result3 = default(Rectangle);
			}
			return result3;
		}

		public static Rectangle DrawRectangle(Rectangle rectangle, Pen pen, out Bitmap result, bool fill = false, int percentageOfMaxSize = 100, int borderWidth = 8)
		{
			result = null;
			Rectangle result2;
			try
			{
				pen.Width = (float)borderWidth;
				Rectangle rectangle2 = rectangle;
				if (percentageOfMaxSize < 100)
				{
					rectangle2.Width = percentageOfMaxSize * rectangle2.Width / 100;
				}
				using (Bitmap bitmap = new Bitmap(rectangle2.Width, rectangle2.Height))
				{
					using (Graphics.FromImage(bitmap))
					{
						result = new Bitmap(rectangle2.Width, rectangle2.Height);
						using (Graphics graphics2 = Graphics.FromImage(result))
						{
							graphics2.SmoothingMode = SmoothingMode.None;
							graphics2.PixelOffsetMode = PixelOffsetMode.HighQuality;
							if (fill)
							{
								graphics2.FillRectangle(pen.Brush, rectangle2);
							}
							else
							{
								graphics2.DrawRectangle(pen, rectangle2);
							}
							graphics2.Flush();
							result2 = rectangle2;
						}
					}
				}
			}
			catch (Exception ex)
			{
                result2 = default(Rectangle);
			}
			return result2;
		}

		public static D3DObj CreateD3DObject(HUDControl pc, int targetID, int icon)
		{
			try
			{
                float num = lib.MyHost.Underlying.Hooks.ObjectHeight(targetID);
                D3DObj d3DObj = lib.MyCore.D3DService.NewD3DObj();
                d3DObj.SetIcon(icon);
				d3DObj.Anchor(targetID, 0.2f, 0f, 0f, num - pc.CurrentProfile.MonsterHeightOffset);
				d3DObj.Scale(pc.CurrentProfile.FloatingDebuffSizeF);
				d3DObj.ROrbit = 0f;
				d3DObj.OrientToCamera(true);
				d3DObj.Visible = true;
				return d3DObj;
			}
            catch (Exception ex) { Repo.RecordException(ex); }
            return null;
		}

		public static D3DObj CreateD3DObject(HUDControl pc, int targetID, int icon, float radius, float rotation, float scale, float animationPhase)
		{
			try
			{
				float num = lib.MyHost.Underlying.Hooks.ObjectHeight(targetID);
				D3DObj d3DObj = lib.MyCore.D3DService.NewD3DObj();
				d3DObj.SetIcon(icon);
				d3DObj.Anchor(targetID, 0.2f, 0f, 0f, num - pc.CurrentProfile.MonsterHeightOffset);
				d3DObj.ROrbit = radius;
				d3DObj.POrbit = rotation;
				d3DObj.Scale(scale);
				d3DObj.AnimationPhaseOffset = animationPhase;
				d3DObj.OrientToCamera(true);
				d3DObj.Visible = true;
				return d3DObj;
			}
            catch (Exception ex) { Repo.RecordException(ex); }
            return null;
		}

		public static void ClearD3DObjects(List<TargetD3D> objects)
		{
            if (objects.Count >= 0)
            {
                for (int i = 0; i < objects.Count; i++)
                {
                    objects[i].D3DObject.Visible = false;
                    objects[i].D3DObject = null;
                }
            }
		}
	}
}
