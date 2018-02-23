using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Elements
{
    public class eListCollection
    {
        public eListCollection(string elFile)
        {
            Lists = Load(elFile);
        }
        public short Version;
        public short Signature;
        public int ConversationListIndex;
        public string ConfigFile;
        public eList[] Lists;

        public void RemoveItem(int ListIndex, int ElementIndex)
        {
            Lists[ListIndex].RemoveItem(ElementIndex);
        }
        public void AddItem(int ListIndex, object[] ItemValues)
        {
            Lists[ListIndex].AddItem(ItemValues);
        }
        public string GetOffset(int ListIndex)
        {
            return BitConverter.ToString(Lists[ListIndex].listOffset);
        }

        public void SetOffset(int ListIndex, string Offset)
        {
            if (Offset != "")
            {
                string[] hex = Offset.Split(new char[] { '-' });
                Lists[ListIndex].listOffset = new byte[hex.Length];
                for (int i = 0; i < hex.Length; i++)
                {
                    Lists[ListIndex].listOffset[i] = Convert.ToByte(hex[i], 16);
                }
            }
            else
            {
                Lists[ListIndex].listOffset = new byte[0];
            }
        }

        public string GetValue(int ListIndex, int ElementIndex, int FieldIndex)
        {
            return Lists[ListIndex].GetValue(ElementIndex, FieldIndex);
        }

        public void SetValue(int ListIndex, int ElementIndex, int FieldIndex, string Value)
        {
            Lists[ListIndex].SetValue(ElementIndex, FieldIndex, Value);
        }

        public string GetType(int ListIndex, int FieldIndex)
        {
            return Lists[ListIndex].GetType(FieldIndex);
        }

        private object readValue(BinaryReader br, string type)
        {
            if (type == "int16")
            {
                return br.ReadInt16();
            }
            if (type == "int32")
            {
                return br.ReadInt32();
            }
            if (type == "int64")
            {
                return br.ReadInt64();
            }
            if (type == "float")
            {
                return br.ReadSingle();
            }
            if (type == "double")
            {
                return br.ReadDouble();
            }
            if (type.Contains("byte:"))
            {
                return br.ReadBytes(Convert.ToInt32(type.Substring(5)));
            }
            if (type.Contains("wstring:"))
            {
                return br.ReadBytes(Convert.ToInt32(type.Substring(8)));
            }
            if (type.Contains("string:"))
            {
                return br.ReadBytes(Convert.ToInt32(type.Substring(7)));
            }
            return null;
        }

        private void writeValue(BinaryWriter bw, object value, string type)
        {
            if (type == "int16")
            {
                bw.Write((short)value);
                return;
            }
            if (type == "int32")
            {
                bw.Write((int)value);
                return;
            }
            if (type == "int64")
            {
                bw.Write((int)value);
                return;
            }
            if (type == "float")
            {
                bw.Write((float)value);
                return;
            }
            if (type == "double")
            {
                bw.Write((double)value);
                return;
            }
            if (type.Contains("byte:"))
            {
                bw.Write((byte[])value);
                return;
            }
            if (type.Contains("wstring:"))
            {
                bw.Write((byte[])value);
                return;
            }
            if (type.Contains("string:"))
            {
                bw.Write((byte[])value);
                return;
            }
        }
        private eList[] loadConfiguration(string file)
        {
            StreamReader sr = new StreamReader(file);
            eList[] Li = new eList[Convert.ToInt32(sr.ReadLine())];
            try
            {
                ConversationListIndex = Convert.ToInt32(sr.ReadLine());
            }
            catch
            {
                ConversationListIndex = 58;
            }
            string line;
            for (int i = 0; i < Li.Length; i++)
            {
                Application.DoEvents();

                while ((line = sr.ReadLine()) == "")
                {
                }
                Li[i] = new eList();
                Li[i].listName = line;
                Li[i].listOffset = null;
                string offset = sr.ReadLine();
                if (offset != "AUTO")
                {
                    Li[i].listOffset = new byte[Convert.ToInt32(offset)];
                }
                Li[i].elementFields = sr.ReadLine().Split(new char[] { ';' });
                Li[i].elementTypes = sr.ReadLine().Split(new char[] { ';' });
            }
            sr.Close();

            return Li;
        }

        private System.Collections.Hashtable loadRules(string file)
        {
            StreamReader sr = new StreamReader(file);
            System.Collections.Hashtable result = new System.Collections.Hashtable();
            string key = "";
            string value = "";
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                Application.DoEvents();

                if (line != "" && !line.StartsWith("#"))
                {
                    if (line.Contains("|"))
                    {
                        key = line.Split(new char[] { '|' })[0];
                        value = line.Split(new char[] { '|' })[1];
                    }
                    else
                    {
                        key = line;
                        value = "";
                    }
                    result.Add(key, value);
                }
            }
            sr.Close();

            return result;
        }
        public eList[] Load(string elFile)
        {
            eList[] Li = new eList[0];

            FileStream fs = File.OpenRead(elFile);
            BinaryReader br = new BinaryReader(fs);

            Version = br.ReadInt16();
            Signature = br.ReadInt16();

            string[] configFiles = Directory.GetFiles(Application.StartupPath + "\\configs", "PW_*_v" + Version + ".cfg");
            if (configFiles.Length > 0)
            {
                ConfigFile = configFiles[0];
                Li = loadConfiguration(ConfigFile);

                for (int l = 0; l < Li.Length; l++)
                {
                    Application.DoEvents();

                    if (Li[l].listOffset != null)
                    {
                        if (Li[l].listOffset.Length > 0)
                        {
                            Li[l].listOffset = br.ReadBytes(Li[l].listOffset.Length);
                        }
                    }
                    else
                    {
                        if (l == 20)
                        {
                            byte[] head = br.ReadBytes(4);
                            byte[] count = br.ReadBytes(4);
                            byte[] body = br.ReadBytes(BitConverter.ToInt32(count, 0));
                            byte[] tail = br.ReadBytes(4);
                            Li[l].listOffset = new byte[head.Length + count.Length + body.Length + tail.Length];
                            Array.Copy(head, 0, Li[l].listOffset, 0, head.Length);
                            Array.Copy(count, 0, Li[l].listOffset, 4, count.Length);
                            Array.Copy(body, 0, Li[l].listOffset, 8, body.Length);
                            Array.Copy(tail, 0, Li[l].listOffset, 8 + body.Length, tail.Length);
                        }
                        if (l == 100)
                        {
                            byte[] head = br.ReadBytes(4);
                            byte[] count = br.ReadBytes(4);
                            byte[] body = br.ReadBytes(BitConverter.ToInt32(count, 0));
                            Li[l].listOffset = new byte[head.Length + count.Length + body.Length];
                            Array.Copy(head, 0, Li[l].listOffset, 0, head.Length);
                            Array.Copy(count, 0, Li[l].listOffset, 4, count.Length);
                            Array.Copy(body, 0, Li[l].listOffset, 8, body.Length);
                        }
                    }
                    if (l == ConversationListIndex)
                    {
                        if (Li[l].elementTypes[0].Contains("AUTO"))
                        {
                            byte[] pattern = (Encoding.GetEncoding("GBK")).GetBytes("facedata\\");
                            long sourcePosition = br.BaseStream.Position;
                            int listLength = -72 - pattern.Length;
                            bool run = true;
                            while (run)
                            {
                                run = false;
                                for (int i = 0; i < pattern.Length; i++)
                                {
                                    listLength++;
                                    if (br.ReadByte() != pattern[i])
                                    {
                                        run = true;
                                        break;
                                    }
                                }
                            }
                            br.BaseStream.Position = sourcePosition;
                            Li[l].elementTypes[0] = "byte:" + listLength;
                        }

                        Li[l].elementValues = new object[1][];
                        Li[l].elementValues[0] = new object[Li[l].elementTypes.Length];
                        Li[l].elementValues[0][0] = readValue(br, Li[l].elementTypes[0]);
                    }
                    else
                    {
                        Li[l].elementValues = new object[br.ReadInt32()][];
                        for (int e = 0; e < Li[l].elementValues.Length; e++)
                        {
                            Li[l].elementValues[e] = new object[Li[l].elementTypes.Length];
                            for (int f = 0; f < Li[l].elementValues[e].Length; f++)
                            {
                                Li[l].elementValues[e][f] = readValue(br, Li[l].elementTypes[f]);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Файл конфигурации версии не найден!\nВерсия: " + Version + "\nПуть: " + "configs\\PW_*_v" + Version + ".cfg");
            }

            br.Close();
            fs.Close();

            return Li;
        }

        public void Save(string elFile)
        {
            if (File.Exists(elFile))
            {
                File.Delete(elFile);
            }

            FileStream fs = new FileStream(elFile, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);

            bw.Write(Version);
            bw.Write(Signature);

            for (int l = 0; l < Lists.Length; l++)
            {
                Application.DoEvents();

                if (Lists[l].listOffset.Length > 0)
                {
                    bw.Write(Lists[l].listOffset);
                }

                if (l != ConversationListIndex)
                {
                    bw.Write(Lists[l].elementValues.Length);
                }
                for (int e = 0; e < Lists[l].elementValues.Length; e++)
                {
                    for (int f = 0; f < Lists[l].elementValues[e].Length; f++)
                    {
                        writeValue(bw, Lists[l].elementValues[e][f], Lists[l].elementTypes[f]);
                    }
                }
            }
            bw.Close();
            fs.Close();
        }

        public void Export(string RulesFile, string TargetFile)
        {
            System.Collections.Hashtable rules = loadRules(RulesFile);

            if (File.Exists(TargetFile))
            {
                File.Delete(TargetFile);
            }

            FileStream fs = new FileStream(TargetFile, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);

            if (rules.ContainsKey("SETVERSION"))
            {
                bw.Write(Convert.ToInt16((string)rules["SETVERSION"]));
            }
            else
            {
                MessageBox.Show("Rules file is missing parameter\n\nSETVERSION:", "Export Failed");
                bw.Close();
                fs.Close();
                return;
            }

            if (rules.ContainsKey("SETSIGNATURE"))
            {
                bw.Write(Convert.ToInt16((string)rules["SETSIGNATURE"]));
            }
            else
            {
                MessageBox.Show("Rules file is missing parameter\n\nSETSIGNATURE:", "Export Failed");
                bw.Close();
                fs.Close();
                return;
            }
            for (int l = 0; l < Lists.Length; l++)
            {
                if (rules.ContainsKey("REMOVELIST:" + l))
                {
                    if (rules.ContainsKey("REPLACEOFFSET:" + l))
                    {
                        string[] hex = ((string)rules["REPLACEOFFSET:" + l]).Split(new char[] { '-', ' ' });
                        if (hex.Length > 1)
                        {
                            byte[] b = new byte[hex.Length];
                            for (int i = 0; i < hex.Length; i++)
                            {
                                b[i] = Convert.ToByte(hex[i], 16);
                            }
                            if (b.Length > 0)
                            {
                                bw.Write(b);
                            }
                        }
                    }
                    else
                    {
                        if (Lists[l].listOffset.Length > 0)
                        {
                            bw.Write(Lists[l].listOffset);
                        }
                    }
                    if (l != ConversationListIndex)
                    {
                        bw.Write(Lists[l].elementValues.Length);
                    }
                    for (int e = 0; e < Lists[l].elementValues.Length; e++)
                    {
                        for (int f = 0; f < Lists[l].elementValues[e].Length; f++)
                        {
                            Application.DoEvents();

                            if (!rules.ContainsKey("REMOVEVALUE:" + l + ":" + f))
                            {
                                writeValue(bw, Lists[l].elementValues[e][f], Lists[l].elementTypes[f]);
                            }
                        }
                    }
                }
            }
            bw.Close();
            fs.Close();
        }
    }
}
