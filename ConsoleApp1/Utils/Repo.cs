using Decal.Adapter;
using Decal.Adapter.Wrappers;
using Defiance.BaseHandle;
using Defiance.CollectionHandle;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Defiance.Utils
{
    public class Repo
    {
        public static string SettingFile;
        public static string file = CoreManager.Current.CharacterFilter.Id.ToString();

        public static void RepoInit()
        {
            SettingFile = GetSettingFolder() + file + ".xml";
        }

        public static int AddFriend(Friend Obj)
        {
            int result = -1;
            try
            {
                if (Obj != null)
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    if (File.Exists(SettingFile))
                    {
                        xmlDocument.Load(SettingFile);
                    }
                    if (xmlDocument.DocumentElement == null)
                    {
                        XmlDeclaration newChild = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
                        XmlElement documentElement = xmlDocument.DocumentElement;
                        xmlDocument.InsertBefore(newChild, documentElement);
                        xmlDocument.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "Settings", string.Empty));
                    }
                    XmlNode xmlNode = xmlDocument.SelectSingleNode("/Settings/Friends");
                    if (xmlNode == null)
                    {
                        xmlNode = xmlDocument.DocumentElement.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "Friends", string.Empty));
                        xmlDocument.DocumentElement.AppendChild(xmlNode);
                    }
                    XmlNode xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "Friend", string.Empty);
                    XmlNode xmlNode3 = xmlDocument.CreateNode(XmlNodeType.Element, "ID", string.Empty);
                    XmlNode xmlNode4 = xmlDocument.CreateNode(XmlNodeType.Element, "Name", string.Empty);
                    result = GetNextID();
                    xmlNode3.InnerText = result.ToString();
                    xmlNode4.InnerText = (Obj.Name ?? string.Empty);
                    xmlNode.AppendChild(xmlNode2);
                    xmlNode2.AppendChild(xmlNode3);
                    xmlNode2.AppendChild(xmlNode4);
                    xmlDocument.Save(SettingFile);
                }
            }
            catch (Exception ex)
            {
                RecordException(ex);
                result = -1;
            }
            return result;
        }

        public static int AddEnemy(Enemy Obj)
        {
            int result = -1;
            try
            {
                if (Obj != null)
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    if (File.Exists(SettingFile))
                    {                        
                        xmlDocument.Load(SettingFile);
                    }
                    if (xmlDocument.DocumentElement == null)
                    {
                        XmlDeclaration newChild = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
                        XmlElement documentElement = xmlDocument.DocumentElement;
                        xmlDocument.InsertBefore(newChild, documentElement);
                        xmlDocument.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "Settings", string.Empty));
                    }
                    XmlNode xmlNode = xmlDocument.SelectSingleNode("/Settings/Enemies");
                    if (xmlNode == null)
                    {
                        xmlNode = xmlDocument.DocumentElement.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "Enemies", string.Empty));
                        xmlDocument.DocumentElement.AppendChild(xmlNode);
                    }
                    XmlNode xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "Enemy", string.Empty);
                    XmlNode xmlNode3 = xmlDocument.CreateNode(XmlNodeType.Element, "ID", string.Empty);
                    XmlNode xmlNode4 = xmlDocument.CreateNode(XmlNodeType.Element, "Name", string.Empty);
                    result = GetNextID();
                    xmlNode3.InnerText = result.ToString();
                    xmlNode4.InnerText = (Obj.Name ?? string.Empty);
                    xmlNode.AppendChild(xmlNode2);
                    xmlNode2.AppendChild(xmlNode3);
                    xmlNode2.AppendChild(xmlNode4);

                    xmlDocument.Save(SettingFile);
                }
            }
            catch (Exception ex)
            {
                Repo.RecordException(ex);
                result = -1;
            }
            return result;
        }
        
        public static bool DeleteFriend(int Id)
        {
            bool result = false;
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                if (File.Exists(SettingFile))
                {
                    xmlDocument.Load(SettingFile);
                }
                XmlNode xmlNode = xmlDocument.SelectSingleNode(string.Format("/Settings/Friends", Id));
                XmlNode xmlNode2 = xmlDocument.SelectSingleNode(string.Format("/Settings/Friends/Friend[ID={0}]", Id));
                if (xmlNode != null && xmlNode2 != null)
                {
                    xmlNode.RemoveChild(xmlNode2);
                    xmlDocument.Save(SettingFile);
                    result = true;
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
            return result;
        }

        public static bool DeleteEnemy(int Id)
        {
            bool result = false;
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                if (File.Exists(SettingFile))
                {
                    xmlDocument.Load(SettingFile);
                }
                XmlNode xmlNode = xmlDocument.SelectSingleNode(string.Format("/Settings/Enemies", Id));
                XmlNode xmlNode2 = xmlDocument.SelectSingleNode(string.Format("/Settings/Enemies/Enemy[ID={0}]", Id));
                if (xmlNode != null && xmlNode2 != null)
                {
                    xmlNode.RemoveChild(xmlNode2);
                    xmlDocument.Save(SettingFile);
                    result = true;
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
            return result;
        }

        public static bool SaveUI(UI Obj)
        {
            bool result = false;
            try
            {
                if (Obj != null && Obj.ServerName != null && Obj.PlayerName != null)
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    if (File.Exists(SettingFile))
                    {
                        xmlDocument.Load(SettingFile);
                    }
                    if (xmlDocument.DocumentElement == null)
                    {
                        XmlDeclaration newChild = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
                        XmlElement documentElement = xmlDocument.DocumentElement;
                        xmlDocument.InsertBefore(newChild, documentElement);
                        xmlDocument.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "Settings", string.Empty));
                    }
                    XmlNode xmlNode = xmlDocument.SelectSingleNode("/Settings/Players");
                    XmlNode xmlNode2 = xmlDocument.SelectSingleNode(string.Format("/Settings/Players/Player[@Server=\"{0}\" and @Name=\"{1}\"]", Obj.ServerName, Obj.PlayerName));
                    if (xmlNode == null)
                    {
                        xmlNode = xmlDocument.DocumentElement.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "Players", string.Empty));
                        xmlDocument.DocumentElement.AppendChild(xmlNode);
                    }
                    if (xmlNode2 == null)
                    {
                        xmlNode2 = xmlDocument.DocumentElement.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "Player", string.Empty));
                        XmlAttribute xmlAttribute = xmlDocument.CreateAttribute("Name");
                        xmlAttribute.Value = Obj.PlayerName;
                        xmlNode2.Attributes.Append(xmlAttribute);
                        xmlAttribute = xmlDocument.CreateAttribute("Server");
                        xmlAttribute.Value = Obj.ServerName;
                        xmlNode2.Attributes.Append(xmlAttribute);
                        xmlNode.AppendChild(xmlNode2);
                    }
                    XmlNode xmlNode3 = xmlNode2.SelectSingleNode("Width");
                    XmlNode xmlNode4 = xmlNode2.SelectSingleNode("Height");
                    XmlNode xmlNode5 = xmlNode2.SelectSingleNode("X");
                    XmlNode xmlNode6 = xmlNode2.SelectSingleNode("Y");
                    XmlNode xmlNode7 = xmlNode2.SelectSingleNode("UseMacroLogic");
                    XmlNode xmlNode8 = xmlNode2.SelectSingleNode("UseAlertOdds");
                    XmlNode xmlNode9 = xmlNode2.SelectSingleNode("UseAlertBc");
                    XmlNode xmlNode10 = xmlNode2.SelectSingleNode("Element");
                    XmlNode xmlNode11 = xmlNode2.SelectSingleNode("UseAlertPA");
                    XmlNode xmlNode12 = xmlNode2.SelectSingleNode("UseAlertPF");
                    XmlNode xmlNode13 = xmlNode2.SelectSingleNode("UseAlertLogP");
                    XmlNode xmlNode14 = xmlNode2.SelectSingleNode("UseAlertDA");
                    XmlNode xmlNode15 = xmlNode2.SelectSingleNode("UseAlertSPK");
                    XmlNode xmlNode16 = xmlNode2.SelectSingleNode("UseAlertSS");
                    XmlNode xmlNode17 = xmlNode2.SelectSingleNode("Range");
                    XmlNode xmlNode18 = xmlNode2.SelectSingleNode("Timer");
                    XmlNode xmlNode19 = xmlNode2.SelectSingleNode("Comps");
                    XmlNode xmlNode20 = xmlNode2.SelectSingleNode("Behaviour");
                    XmlNode xmlNode21 = xmlNode2.SelectSingleNode("UseLogDie");
                    XmlNode xmlNode22 = xmlNode2.SelectSingleNode("Slots");
                    XmlNode xmlNode23 = xmlNode2.SelectSingleNode("UseAlertLS");
                    XmlNode xmlNode24 = xmlNode2.SelectSingleNode("UseAlertDT");
                    XmlNode xmlNode25 = xmlNode2.SelectSingleNode("UseAlertPKT");
                    XmlNode xmlNode26 = xmlNode2.SelectSingleNode("Ticker");
                    XmlNode xmlNode27 = xmlNode2.SelectSingleNode("MonCheck");

                    if (xmlNode3 == null)
                    {
                        xmlNode3 = xmlDocument.DocumentElement.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "Width", string.Empty));
                        xmlNode2.AppendChild(xmlNode3);
                    }
                    if (xmlNode4 == null)
                    {
                        xmlNode4 = xmlDocument.DocumentElement.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "Height", string.Empty));
                        xmlNode2.AppendChild(xmlNode4);
                    }
                    if (xmlNode5 == null)
                    {
                        xmlNode5 = xmlDocument.DocumentElement.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "X", string.Empty));
                        xmlNode2.AppendChild(xmlNode5);
                    }
                    if (xmlNode6 == null)
                    {
                        xmlNode6 = xmlDocument.DocumentElement.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "Y", string.Empty));
                        xmlNode2.AppendChild(xmlNode6);
                    }
                    if (xmlNode7 == null)
                    {
                        xmlNode7 = xmlDocument.DocumentElement.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "UseMacroLogic", string.Empty));
                        xmlNode2.AppendChild(xmlNode7);
                    }
                    if (xmlNode8 == null)
                    {
                        xmlNode8 = xmlDocument.DocumentElement.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "UseAlertOdds", string.Empty));
                        xmlNode2.AppendChild(xmlNode8);
                    }
                    if (xmlNode9 == null)
                    {
                        xmlNode9 = xmlDocument.DocumentElement.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "UseAlertBc", string.Empty));
                        xmlNode2.AppendChild(xmlNode9);
                    }
                    if (xmlNode10 == null)
                    {
                        xmlNode10 = xmlDocument.DocumentElement.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "Element", string.Empty));
                        xmlNode2.AppendChild(xmlNode10);
                    }
                    if (xmlNode11 == null)
                    {
                        xmlNode11 = xmlDocument.DocumentElement.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "UseAlertPA", string.Empty));
                        xmlNode2.AppendChild(xmlNode11);
                    }
                    if (xmlNode12 == null)
                    {
                        xmlNode12 = xmlDocument.DocumentElement.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "UseAlertPF", string.Empty));
                        xmlNode2.AppendChild(xmlNode12);
                    }
                    if (xmlNode13 == null)
                    {
                        xmlNode13 = xmlDocument.DocumentElement.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "UseAlertLogP", string.Empty));
                        xmlNode2.AppendChild(xmlNode13);
                    }
                    if (xmlNode14 == null)
                    {
                        xmlNode14 = xmlDocument.DocumentElement.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "UseAlertDA", string.Empty));
                        xmlNode2.AppendChild(xmlNode14);
                    }
                    if (xmlNode15 == null)
                    {
                        xmlNode15 = xmlDocument.DocumentElement.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "UseAlertSPK", string.Empty));
                        xmlNode2.AppendChild(xmlNode15);
                    }
                    if (xmlNode16 == null)
                    {
                        xmlNode16 = xmlDocument.DocumentElement.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "UseAlertSS", string.Empty));
                        xmlNode2.AppendChild(xmlNode16);
                    }
                    if (xmlNode17 == null)
                    {
                        xmlNode17 = xmlDocument.DocumentElement.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "Range", string.Empty));
                        xmlNode2.AppendChild(xmlNode17);
                    }
                    if (xmlNode18 == null)
                    {
                        xmlNode18 = xmlDocument.DocumentElement.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "Timer", string.Empty));
                        xmlNode2.AppendChild(xmlNode18);
                    }
                    if (xmlNode19 == null)
                    {
                        xmlNode19 = xmlDocument.DocumentElement.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "Comps", string.Empty));
                        xmlNode2.AppendChild(xmlNode19);
                    }
                    if (xmlNode20 == null)
                    {
                        xmlNode20 = xmlDocument.DocumentElement.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "Behaviour", string.Empty));
                        xmlNode2.AppendChild(xmlNode20);
                    }
                    if (xmlNode21 == null)
                    {
                        xmlNode21 = xmlDocument.DocumentElement.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "UseLogDie", string.Empty));
                        xmlNode2.AppendChild(xmlNode21);
                    }
                    if (xmlNode22 == null)
                    {
                        xmlNode22 = xmlDocument.DocumentElement.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "Slots", string.Empty));
                        xmlNode2.AppendChild(xmlNode22);
                    }
                    if (xmlNode23 == null)
                    {
                        xmlNode23 = xmlDocument.DocumentElement.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "UseAlertLS", string.Empty));
                        xmlNode2.AppendChild(xmlNode23);
                    }
                    if (xmlNode24 == null)
                    {
                        xmlNode24 = xmlDocument.DocumentElement.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "UseAlertDT", string.Empty));
                        xmlNode2.AppendChild(xmlNode24);
                    }
                    if (xmlNode25 == null)
                    {
                        xmlNode25 = xmlDocument.DocumentElement.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "UseAlertPKT", string.Empty));
                        xmlNode2.AppendChild(xmlNode25);
                    }
                    if (xmlNode26 == null)
                    {
                        xmlNode26 = xmlDocument.DocumentElement.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "Ticker", string.Empty));
                        xmlNode2.AppendChild(xmlNode26);
                    }
                    if (xmlNode27 == null)
                    {
                        xmlNode27 = xmlDocument.DocumentElement.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "MonCheck", string.Empty));
                        xmlNode2.AppendChild(xmlNode27);
                    }

                    xmlNode3.InnerText = Obj.Width.ToString();
                    xmlNode4.InnerText = Obj.Height.ToString();
                    xmlNode5.InnerText = Obj.X.ToString();
                    xmlNode6.InnerText = Obj.Y.ToString();
                    xmlNode7.InnerText = Obj.UseMacroLogic.ToString();
                    xmlNode8.InnerText = Obj.UseAlertOdds.ToString();
                    xmlNode9.InnerText = Obj.UseAlertBc.ToString();
                    xmlNode10.InnerText = Obj.Element.ToString();
                    xmlNode11.InnerText = Obj.UseAlertPA.ToString();
                    xmlNode12.InnerText = Obj.UseAlertPF.ToString();
                    xmlNode13.InnerText = Obj.UseAlertLogP.ToString();
                    xmlNode14.InnerText = Obj.UseAlertDA.ToString();
                    xmlNode15.InnerText = Obj.UseAlertSPK.ToString();
                    xmlNode16.InnerText = Obj.UseAlertSS.ToString();
                    xmlNode17.InnerText = Obj.Range.ToString();
                    xmlNode18.InnerText = Obj.Timer.ToString();
                    xmlNode19.InnerText = Obj.Comps.ToString();
                    xmlNode20.InnerText = Obj.Behaviour.ToString();
                    xmlNode21.InnerText = Obj.UseLogDie.ToString();
                    xmlNode22.InnerText = Obj.Slots.ToString();
                    xmlNode23.InnerText = Obj.UseAlertLS.ToString();
                    xmlNode24.InnerText = Obj.UseAlertDT.ToString();
                    xmlNode25.InnerText = Obj.UseAlertPKT.ToString();
                    xmlNode26.InnerText = Obj.Ticker.ToString();
                    xmlNode27.InnerText = Obj.MonCheck.ToString();

                    xmlDocument.Save(SettingFile);
                    result = true;
                }
            }

            catch (Exception ex)
            {
                Repo.RecordException(ex);
                result = false;
            }

            return result;
        }

        public static UI GetUI(string ServerName, string PlayerName)
        {
            UI ui = new UI();
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                if (File.Exists(SettingFile))
                {
                    xmlDocument.Load(SettingFile);
                    XmlNode xmlNode = xmlDocument.SelectSingleNode(string.Format("/Settings/Players/Player[@Server=\"{0}\" and @Name=\"{1}\"]/Width", ServerName, PlayerName));
                    ui.Width = ((xmlNode != null) ? Convert.ToInt32(xmlNode.InnerText) : 0);

                    xmlNode = xmlDocument.SelectSingleNode(string.Format("/Settings/Players/Player[@Server=\"{0}\" and @Name=\"{1}\"]/Height", ServerName, PlayerName));
                    ui.Height = ((xmlNode != null) ? Convert.ToInt32(xmlNode.InnerText) : 0);

                    xmlNode = xmlDocument.SelectSingleNode(string.Format("/Settings/Players/Player[@Server=\"{0}\" and @Name=\"{1}\"]/X", ServerName, PlayerName));
                    ui.X = ((xmlNode != null) ? Convert.ToInt32(xmlNode.InnerText) : 0);

                    xmlNode = xmlDocument.SelectSingleNode(string.Format("/Settings/Players/Player[@Server=\"{0}\" and @Name=\"{1}\"]/Y", ServerName, PlayerName));
                    ui.Y = ((xmlNode != null) ? Convert.ToInt32(xmlNode.InnerText) : 0);

                    xmlNode = xmlDocument.SelectSingleNode(string.Format("/Settings/Players/Player[@Server=\"{0}\" and @Name=\"{1}\"]/UseMacroLogic", ServerName, PlayerName));
                    ui.UseMacroLogic = (xmlNode == null || Convert.ToBoolean(xmlNode.InnerText));

                    xmlNode = xmlDocument.SelectSingleNode(string.Format("/Settings/Players/Player[@Server=\"{0}\" and @Name=\"{1}\"]/UseAlertOdds", ServerName, PlayerName));
                    ui.UseAlertOdds = (xmlNode == null || Convert.ToBoolean(xmlNode.InnerText));

                    xmlNode = xmlDocument.SelectSingleNode(string.Format("/Settings/Players/Player[@Server=\"{0}\" and @Name=\"{1}\"]/UseAlertBc", ServerName, PlayerName));
                    ui.UseAlertBc = (xmlNode == null || Convert.ToBoolean(xmlNode.InnerText));

                    xmlNode = xmlDocument.SelectSingleNode(string.Format("/Settings/Players/Player[@Server=\"{0}\" and @Name=\"{1}\"]/UseAlertPA", ServerName, PlayerName));
                    ui.UseAlertPA = (xmlNode == null || Convert.ToBoolean(xmlNode.InnerText));

                    xmlNode = xmlDocument.SelectSingleNode(string.Format("/Settings/Players/Player[@Server=\"{0}\" and @Name=\"{1}\"]/UseAlertPF", ServerName, PlayerName));
                    ui.UseAlertPF = (xmlNode == null || Convert.ToBoolean(xmlNode.InnerText));

                    xmlNode = xmlDocument.SelectSingleNode(string.Format("/Settings/Players/Player[@Server=\"{0}\" and @Name=\"{1}\"]/UseAlertLogP", ServerName, PlayerName));
                    ui.UseAlertLogP = (xmlNode == null || Convert.ToBoolean(xmlNode.InnerText));

                    xmlNode = xmlDocument.SelectSingleNode(string.Format("/Settings/Players/Player[@Server=\"{0}\" and @Name=\"{1}\"]/UseAlertDA", ServerName, PlayerName));
                    ui.UseAlertDA = (xmlNode == null || Convert.ToBoolean(xmlNode.InnerText));

                    xmlNode = xmlDocument.SelectSingleNode(string.Format("/Settings/Players/Player[@Server=\"{0}\" and @Name=\"{1}\"]/UseAlertSPK", ServerName, PlayerName));
                    ui.UseAlertSPK = (xmlNode == null || Convert.ToBoolean(xmlNode.InnerText));

                    xmlNode = xmlDocument.SelectSingleNode(string.Format("/Settings/Players/Player[@Server=\"{0}\" and @Name=\"{1}\"]/UseAlertSS", ServerName, PlayerName));
                    ui.UseAlertSS = (xmlNode == null || Convert.ToBoolean(xmlNode.InnerText));

                    xmlNode = xmlDocument.SelectSingleNode(string.Format("/Settings/Players/Player[@Server=\"{0}\" and @Name=\"{1}\"]/Range", ServerName, PlayerName));
                    ui.Range = ((xmlNode != null) ? Convert.ToInt32(xmlNode.InnerText) : 0);

                    xmlNode = xmlDocument.SelectSingleNode(string.Format("/Settings/Players/Player[@Server=\"{0}\" and @Name=\"{1}\"]/Timer", ServerName, PlayerName));
                    ui.Timer = ((xmlNode != null) ? Convert.ToInt32(xmlNode.InnerText) : 0);

                    xmlNode = xmlDocument.SelectSingleNode(string.Format("/Settings/Players/Player[@Server=\"{0}\" and @Name=\"{1}\"]/Comps", ServerName, PlayerName));
                    ui.Comps = ((xmlNode != null) ? Convert.ToInt32(xmlNode.InnerText) : 0);

                    xmlNode = xmlDocument.SelectSingleNode(string.Format("/Settings/Players/Player[@Server=\"{0}\" and @Name=\"{1}\"]/Behaviour", ServerName, PlayerName));
                    ui.Behaviour = ((xmlNode != null) ? Convert.ToInt32(xmlNode.InnerText) : 0);

                    xmlNode = xmlDocument.SelectSingleNode(string.Format("/Settings/Players/Player[@Server=\"{0}\" and @Name=\"{1}\"]/Element", ServerName, PlayerName));
                    ui.Element = ((xmlNode != null) ? Convert.ToInt32(xmlNode.InnerText) : 0);

                    xmlNode = xmlDocument.SelectSingleNode(string.Format("/Settings/Players/Player[@Server=\"{0}\" and @Name=\"{1}\"]/UseLogDie", ServerName, PlayerName));
                    ui.UseLogDie = (xmlNode == null || Convert.ToBoolean(xmlNode.InnerText));

                    xmlNode = xmlDocument.SelectSingleNode(string.Format("/Settings/Players/Player[@Server=\"{0}\" and @Name=\"{1}\"]/Slots", ServerName, PlayerName));
                    ui.Slots = ((xmlNode != null) ? Convert.ToInt32(xmlNode.InnerText) : 0);

                    xmlNode = xmlDocument.SelectSingleNode(string.Format("/Settings/Players/Player[@Server=\"{0}\" and @Name=\"{1}\"]/UseAlertLS", ServerName, PlayerName));
                    ui.UseAlertLS = (xmlNode == null || Convert.ToBoolean(xmlNode.InnerText));

                    xmlNode = xmlDocument.SelectSingleNode(string.Format("/Settings/Players/Player[@Server=\"{0}\" and @Name=\"{1}\"]/UseAlertDT", ServerName, PlayerName));
                    ui.UseAlertDT = (xmlNode == null || Convert.ToBoolean(xmlNode.InnerText));

                    xmlNode = xmlDocument.SelectSingleNode(string.Format("/Settings/Players/Player[@Server=\"{0}\" and @Name=\"{1}\"]/UseAlertPKT", ServerName, PlayerName));
                    ui.UseAlertPKT = (xmlNode == null || Convert.ToBoolean(xmlNode.InnerText));

                    xmlNode = xmlDocument.SelectSingleNode(string.Format("/Settings/Players/Player[@Server=\"{0}\" and @Name=\"{1}\"]/Ticker", ServerName, PlayerName));
                    ui.Ticker = ((xmlNode != null) ? Convert.ToInt32(xmlNode.InnerText) : 0);

                    xmlNode = xmlDocument.SelectSingleNode(string.Format("/Settings/Players/Player[@Server=\"{0}\" and @Name=\"{1}\"]/MonCheck", ServerName, PlayerName));
                    ui.MonCheck = ((xmlNode != null) ? Convert.ToInt32(xmlNode.InnerText) : 0);
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
            return ui;
        }

        public static Friend[] GetFriends()
        {
            List<Friend> list = new List<Friend>();
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                if (File.Exists(SettingFile))
                {
                    xmlDocument.Load(SettingFile);
                }
                xmlDocument.SelectNodes("Settings/Friends/Friend");
                foreach (object obj in xmlDocument.SelectNodes("Settings/Friends/Friend"))
                {
                    XmlNode xmlNode = (XmlNode)obj;
                    try
                    {
                        list.Add(new Friend
                        {
                            ID = ((xmlNode.SelectSingleNode("ID") != null) ? Convert.ToInt32(xmlNode.SelectSingleNode("ID").InnerText) : -1),
                            Name = ((xmlNode.SelectSingleNode("Name") != null) ? xmlNode.SelectSingleNode("Name").InnerText : string.Empty),
                        });
                    }
                    catch (Exception ex) { Repo.RecordException(ex); }
                }
            }
            catch (Exception ex2) { Repo.RecordException(ex2); }
            list.Sort((Friend x, Friend y) => x.Name.CompareTo(y.Name));
            return list.ToArray();
        }

        public static Enemy[] GetEnemies()
        {
            List<Enemy> list = new List<Enemy>();
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                if (File.Exists(SettingFile))
                {
                    xmlDocument.Load(SettingFile);
                }
                xmlDocument.SelectNodes("Settings/Enemies/Enemy");
                foreach (object obj in xmlDocument.SelectNodes("Settings/Enemies/Enemy"))
                {
                    XmlNode xmlNode = (XmlNode)obj;
                    try
                    {
                        list.Add(new Enemy
                        {
                            ID = ((xmlNode.SelectSingleNode("ID") != null) ? Convert.ToInt32(xmlNode.SelectSingleNode("ID").InnerText) : -1),
                            Name = ((xmlNode.SelectSingleNode("Name") != null) ? xmlNode.SelectSingleNode("Name").InnerText : string.Empty),
                        });
                    }
                    catch (Exception ex) { Repo.RecordException(ex); }
                }
            }
            catch (Exception ex2) { Repo.RecordException(ex2); }
            list.Sort((Enemy x, Enemy y) => x.Name.CompareTo(y.Name));
            return list.ToArray();
        }

        public static void RecordException(Exception ex)
        {
            string path = GetExceptionFolder() + DateTime.Now.Ticks + ".xml";
            try
            {
                using (FileStream fileStream = File.Create(path))
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(fileStream, new XmlWriterSettings
                    {
                        Encoding = new UTF8Encoding(false),
                        ConformanceLevel = ConformanceLevel.Document,
                        Indent = true
                    }))
                    {
                        xmlWriter.WriteStartDocument();
                        xmlWriter.WriteStartElement("Exception");
                        xmlWriter.WriteStartElement("EventTime");
                        xmlWriter.WriteString(DateTime.Now.ToString("g"));
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("HelpLink");
                        xmlWriter.WriteString((ex.HelpLink == null) ? string.Empty : ex.HelpLink.Trim());
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("Message");
                        xmlWriter.WriteString((ex.Message == null) ? string.Empty : ex.Message.Trim());
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("Source");
                        xmlWriter.WriteString((ex.Source == null) ? string.Empty : ex.Source.Trim());
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("StackTrace");
                        xmlWriter.WriteString((ex.StackTrace == null) ? string.Empty : ex.StackTrace.Trim());
                        xmlWriter.WriteEndElement();

                        if (!ex.Equals(ex.GetBaseException()))
                        {
                            Exception baseException = ex.GetBaseException();
                            xmlWriter.WriteStartElement("BaseException");
                            xmlWriter.WriteStartElement("HelpLink");
                            xmlWriter.WriteString((baseException.HelpLink == null) ? string.Empty : baseException.HelpLink.Trim());
                            xmlWriter.WriteEndElement();
                            xmlWriter.WriteStartElement("Message");
                            xmlWriter.WriteString((baseException.Message == null) ? string.Empty : baseException.Message.Trim());
                            xmlWriter.WriteEndElement();
                            xmlWriter.WriteStartElement("Source");
                            xmlWriter.WriteString((baseException.Source == null) ? string.Empty : baseException.Source.Trim());
                            xmlWriter.WriteEndElement();
                            xmlWriter.WriteStartElement("StackTrace");
                            xmlWriter.WriteString((baseException.StackTrace == null) ? string.Empty : baseException.StackTrace.Trim());
                            xmlWriter.WriteEndElement();
                            xmlWriter.WriteEndElement();
                        }
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndDocument();
                    }
                }
            }
            catch (Exception ex2) { Repo.RecordException(ex2); }
        }

        public static void DumpWorldObject(WorldObject obj)
        {
            try
            {
                Utility.AddChatText("--- DEFIANCE Dump ---",6);
                using (FileStream fileStream = File.Create(GetDumpFolder() + DateTime.Now.Ticks + ".xml"))
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(fileStream, new XmlWriterSettings
                    {
                        Encoding = new UTF8Encoding(false),
                        ConformanceLevel = ConformanceLevel.Document,
                        Indent = true
                    }))
                    {
                        xmlWriter.WriteStartDocument();
                        xmlWriter.WriteStartElement("WorldObject");
                        Utility.AddChatText(string.Format("ID: {0}", obj.Id),0);
                        xmlWriter.WriteStartElement("ID");
                        xmlWriter.WriteString(obj.Id.ToString());
                        xmlWriter.WriteEndElement();
                        Utility.AddChatText(string.Format("Name: {0}", obj.Name), 0);
                        xmlWriter.WriteStartElement("Name");
                        xmlWriter.WriteString(string.Format("Name: {0}", obj.Name ?? string.Empty));
                        xmlWriter.WriteEndElement();
                        if (obj.ActiveSpellCount > 0)
                        {
                            xmlWriter.WriteStartElement("ActiveSpells");
                            Utility.AddChatText("***ActiveSpells***",6);
                            for (int i = 0; i < obj.ActiveSpellCount; i++)
                            {
                                Utility.AddChatText(string.Format("Spell: {0}", obj.ActiveSpell(i)),0);
                                xmlWriter.WriteStartElement("Spell");
                                xmlWriter.WriteString(obj.ActiveSpell(i).ToString());
                                xmlWriter.WriteEndElement();
                            }
                            xmlWriter.WriteEndElement();
                        }
                        Utility.AddChatText(string.Format("Behavior: {0}", obj.Behavior), 0);
                        xmlWriter.WriteStartElement("Behavior");
                        xmlWriter.WriteString(obj.Behavior.ToString());
                        xmlWriter.WriteEndElement();
                        if (obj.BoolKeys.Count > 0)
                        {
                            xmlWriter.WriteStartElement("BoolKeys");
                            Utility.AddChatText("***BoolKeys***", 6);
                            foreach (int num in obj.BoolKeys)
                            {
                                Utility.AddChatText(string.Format("Key: {0}, {1}", (BoolValueKey)num, obj.Values((BoolValueKey)num, false)), 0);
                                xmlWriter.WriteStartElement("Key");
                                xmlWriter.WriteStartElement("Bool");
                                XmlWriter xmlWriter2 = xmlWriter;
                                BoolValueKey boolValueKey = (BoolValueKey)num;
                                xmlWriter2.WriteString(boolValueKey.ToString());
                                xmlWriter.WriteEndElement();
                                xmlWriter.WriteStartElement("Value");
                                xmlWriter.WriteString(obj.Values((BoolValueKey)num, false).ToString());
                                xmlWriter.WriteEndElement();
                                xmlWriter.WriteEndElement();
                            }
                            xmlWriter.WriteEndElement();
                        }
                        Utility.AddChatText(string.Format("Category: {0}", obj.Category), 0);
                        xmlWriter.WriteStartElement("Category");
                        xmlWriter.WriteString(obj.Category.ToString());
                        xmlWriter.WriteEndElement();
                        Utility.AddChatText(string.Format("Container: {0}", obj.Container), 0);
                        xmlWriter.WriteStartElement("Container");
                        xmlWriter.WriteString(obj.Container.ToString());
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("Coordinates");
                        Utility.AddChatText("***Coordinates***", 6);
                        Utility.AddChatText(string.Format("EastWest: {0}", obj.Coordinates().EastWest), 0);
                        xmlWriter.WriteStartElement("EastWest");
                        xmlWriter.WriteString(obj.Coordinates().EastWest.ToString());
                        xmlWriter.WriteEndElement();
                        Utility.AddChatText(string.Format("NorthSouth: {0}", obj.Coordinates().NorthSouth), 0);
                        xmlWriter.WriteStartElement("NorthSouth");
                        xmlWriter.WriteString(obj.Coordinates().NorthSouth.ToString());
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                        if (obj.DoubleKeys.Count > 0)
                        {
                            xmlWriter.WriteStartElement("DoubleKeys");
                            Utility.AddChatText("***DoubleKeys***", 6);
                            foreach (int num2 in obj.DoubleKeys)
                            {
                                Utility.AddChatText(string.Format("Key: {0}, {1}", (DoubleValueKey)num2, obj.Values((DoubleValueKey)num2, -1.0)), 0);
                                xmlWriter.WriteStartElement("Key");
                                xmlWriter.WriteStartElement("Double");
                                XmlWriter xmlWriter3 = xmlWriter;
                                DoubleValueKey doubleValueKey = (DoubleValueKey)num2;
                                xmlWriter3.WriteString(doubleValueKey.ToString());
                                xmlWriter.WriteEndElement();
                                xmlWriter.WriteStartElement("Value");
                                xmlWriter.WriteString(obj.Values((DoubleValueKey)num2, -1.0).ToString());
                                xmlWriter.WriteEndElement();
                                xmlWriter.WriteEndElement();
                            }
                            xmlWriter.WriteEndElement();
                        }
                        Utility.AddChatText(string.Format("GameDataFlags1: {0}", obj.GameDataFlags1), 0);
                        xmlWriter.WriteStartElement("GameDataFlags1");
                        xmlWriter.WriteString(obj.GameDataFlags1.ToString());
                        xmlWriter.WriteEndElement();
                        Utility.AddChatText(string.Format("HasIdData: {0}", obj.HasIdData), 0);
                        xmlWriter.WriteStartElement("HasIdData");
                        xmlWriter.WriteString(obj.HasIdData.ToString());
                        xmlWriter.WriteEndElement();
                        Utility.AddChatText(string.Format("Icon: {0}", obj.Icon), 0);
                        xmlWriter.WriteStartElement("Icon");
                        xmlWriter.WriteString(obj.Icon.ToString());
                        xmlWriter.WriteEndElement();
                        Utility.AddChatText(string.Format("LastIdTime: {0}", obj.LastIdTime), 0);
                        xmlWriter.WriteStartElement("LastIdTime");
                        xmlWriter.WriteString(obj.LastIdTime.ToString());
                        xmlWriter.WriteEndElement();
                        if (obj.LongKeys.Count > 0)
                        {
                            xmlWriter.WriteStartElement("LongKeys");
                            Utility.AddChatText("***LongKeys***", 6);
                            foreach (int num3 in obj.LongKeys)
                            {
                                Utility.AddChatText(string.Format("Key: {0}, {1}", (LongValueKey)num3, obj.Values((LongValueKey)num3, -1)), 0);
                                xmlWriter.WriteStartElement("Key");
                                xmlWriter.WriteStartElement("Long");
                                XmlWriter xmlWriter4 = xmlWriter;
                                LongValueKey longValueKey = (LongValueKey)num3;
                                xmlWriter4.WriteString(longValueKey.ToString());
                                xmlWriter.WriteEndElement();
                                xmlWriter.WriteStartElement("Value");
                                xmlWriter.WriteString(obj.Values((LongValueKey)num3, -1).ToString());
                                xmlWriter.WriteEndElement();
                                xmlWriter.WriteEndElement();
                            }
                            xmlWriter.WriteEndElement();
                        }
                        Utility.AddChatText(string.Format("PhysicsDataFlags: {0}", obj.PhysicsDataFlags), 0);
                        xmlWriter.WriteStartElement("PhysicsDataFlags");
                        xmlWriter.WriteString(obj.PhysicsDataFlags.ToString());
                        xmlWriter.WriteEndElement();
                        Utility.AddChatText(string.Format("ObjectClass: {0}", obj.ObjectClass), 0);
                        xmlWriter.WriteStartElement("ObjectClass");
                        xmlWriter.WriteString(obj.ObjectClass.ToString());
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("RawCoordinates");
                        Utility.AddChatText("***RawCoordinates***", 6);
                        Utility.AddChatText(string.Format("X: {0}", obj.RawCoordinates().X), 0);
                        xmlWriter.WriteStartElement("X");
                        xmlWriter.WriteString(obj.RawCoordinates().X.ToString());
                        xmlWriter.WriteEndElement();
                        Utility.AddChatText(string.Format("Y: {0}", obj.RawCoordinates().Y), 0);
                        xmlWriter.WriteStartElement("Y");
                        xmlWriter.WriteString(obj.RawCoordinates().Y.ToString());
                        xmlWriter.WriteEndElement();
                        Utility.AddChatText(string.Format("Z: {0}", obj.RawCoordinates().Z), 0);
                        xmlWriter.WriteStartElement("Z");
                        xmlWriter.WriteString(obj.RawCoordinates().Z.ToString());
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                        if (obj.StringKeys.Count > 0)
                        {
                            xmlWriter.WriteStartElement("StringKeys");
                            Utility.AddChatText("***StringKeys***", 6);
                            foreach (int num4 in obj.StringKeys)
                            {
                                Utility.AddChatText(string.Format("Key: {0}, {1}", (StringValueKey)num4, obj.Values((StringValueKey)num4, string.Empty)), 0);
                                xmlWriter.WriteStartElement("Key");
                                xmlWriter.WriteStartElement("String");
                                XmlWriter xmlWriter5 = xmlWriter;
                                StringValueKey stringValueKey = (StringValueKey)num4;
                                xmlWriter5.WriteString(stringValueKey.ToString());
                                xmlWriter.WriteEndElement();
                                xmlWriter.WriteStartElement("Value");
                                xmlWriter.WriteString(obj.Values((StringValueKey)num4, string.Empty) ?? string.Empty);
                                xmlWriter.WriteEndElement();
                                xmlWriter.WriteEndElement();
                            }
                            xmlWriter.WriteEndElement();
                        }
                        if (obj.SpellCount > 0)
                        {
                            xmlWriter.WriteStartElement("SpellCount");
                            Utility.AddChatText("***SpellCount***", 6);
                            for (int j = 0; j < obj.SpellCount; j++)
                            {
                                Utility.AddChatText(string.Format("Spell: {0}", obj.Spell(j)), 0);
                                xmlWriter.WriteStartElement("Spell");
                                xmlWriter.WriteString(obj.Spell(j).ToString());
                                xmlWriter.WriteEndElement();
                            }
                            xmlWriter.WriteEndElement();
                        }
                        Utility.AddChatText(string.Format("Type: {0}", obj.Type), 0);
                        xmlWriter.WriteStartElement("Type");
                        xmlWriter.WriteString(obj.Type.ToString());
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndDocument();
                    }
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
        }

        private static int GetNextID()
        {
            int num = 0;
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                if (File.Exists(SettingFile))
                {
                    xmlDocument.Load(SettingFile);
                }
               
                foreach (object obj in xmlDocument.SelectNodes("Settings/Friends/Friend"))
                {
                    XmlNode xmlNode = (XmlNode)obj;
                    try
                    {
                        int num2 = (xmlNode.SelectSingleNode("ID") != null) ? Convert.ToInt32(xmlNode.SelectSingleNode("ID").InnerText) : -1;
                        num = ((num2 > num) ? num2 : num);
                    }
                    catch (Exception ex) { Repo.RecordException(ex); }
                }
                foreach (object obj in xmlDocument.SelectNodes("Settings/Enemies/Enemy"))
                {
                    XmlNode xmlNode = (XmlNode)obj;
                    try
                    {
                        int num2 = (xmlNode.SelectSingleNode("ID") != null) ? Convert.ToInt32(xmlNode.SelectSingleNode("ID").InnerText) : -1;
                        num = ((num2 > num) ? num2 : num);
                    }
                    catch (Exception ex) { Repo.RecordException(ex); }
                }
            }
            catch (Exception ex2) { Repo.RecordException(ex2); }
            return num + 1;
        }

        private static string GetSettingFolder()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Decal Plugins\\Defiance v2\\");
            try
            {
                if (!directoryInfo.Exists)
                {
                    directoryInfo.Create();
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
            return directoryInfo.FullName;
        }

        private static string GetExceptionFolder()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Decal Plugins\\Defiance v2\\Exception\\");
            try
            {
                if (!directoryInfo.Exists)
                {
                    directoryInfo.Create();
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
            return directoryInfo.FullName;
        }

        private static string GetDumpFolder()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Decal Plugins\\Defiance v2\\Dump\\");
            try
            {
                if (!directoryInfo.Exists)
                {
                    directoryInfo.Create();
                }
            }
            catch (Exception ex) { Repo.RecordException(ex); }
            return directoryInfo.FullName;
        }
    }
}