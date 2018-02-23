using Elements;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using sTASKedit;

namespace DataTranslator
{
    public partial class Form1 : Form
    {
        List<ElementTable> etl = new List<ElementTable>();
        eListCollection src;
        eListCollection dst;
        ElementsData ed = new ElementsData();
        ATaskTemplFixedData[] t_src;
        ATaskTemplFixedData[] t_dst;
        int t_src_v;
        int t_dst_v;
        protected static ArrayList versions = new ArrayList(new int[]
        {
            43,
            53,
            54,
            55,
            56,
            57,
            59,
            60,
            63,
            68,
            69,
            75,
            78,
            80,
            82,
            89,
            90,
            92,
            93,
            99,
            100,
            101,
            102,
            103,
            104,
            105,
            106,
            107,
            108,
            109,
            110,
            111,
            112,
            113,
            114,
            115,
            116,
            117,
            118,
            119,
            120,
            121,
            122,
            123,
            124,
            125,
            127,
            128,
            129,
            130,
            135,
            137,
            9999
        });
        public static int TaskVersion = 0;

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void open_el_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                src = ed.LoadElements(ofd.FileName);
                etl = ed.CreateElTable(src);
                import_el.Enabled = true;
                src_status.Text = "Загружена версия: " + src.Version + "; Загружено элементов: " + etl.Count;
            }
        }

        private void import_el_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Thread t = new Thread(new ParameterizedThreadStart(Import));
                t.Start(ofd.FileName);
            }
        }

        public void Import(object filename)
        {
            dst = ed.LoadElements((string)filename);
            ProgressStatus.Maximum = dst.Lists.Length;
            int a, b, c, d;
            for (a = 0; a < dst.Lists.Length; ++a)
            {
                if (a == dst.ConversationListIndex)
                {
                    dst.Lists[a] = src.Lists[a];
                }
                for (c = 0; c < etl.Count; ++c)
                {
                    if (dst.Lists[a].listName == etl[c].list)
                    {
                        for (b = 0; b < dst.Lists[a].elementValues.Length; ++b)
                        {
                            if (dst.Lists[a].elementFields[0] == "ID")
                            {
                                if (Convert.ToInt32(dst.GetValue(a, b, 0)) == etl[c].id)
                                {
                                    for (d = 0; d < dst.Lists[a].elementFields.Length; ++d)
                                    {
                                        foreach (Fields f in etl[c].f)
                                        {
                                            if (dst.Lists[a].elementFields[d].IndexOf(f.field) > -1)
                                            {
                                                dst.SetValue(a, b, d, f.value);
                                                //debugBox.Text += "ID: " + dst.GetValue(a, b, 0) + " => " + f.value + "\n";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                ProgressStatus.Value++;
            }
            dst.Save((string)filename);
            ProgressStatus.Value = 0;
            debugBox.Text += "Русификация Elements.data успешно завершена\n";
        }

        private void open_task_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileStream input = File.OpenRead(ofd.FileName);
                BinaryReader binaryStream = new BinaryReader(input);
                binaryStream.ReadInt32();
                t_src_v = binaryStream.ReadInt32();
                if (!versions.Contains(t_src_v))
                {
                    binaryStream.Close();
                    input.Close();
                    MessageBox.Show("Version Unsupported: " + t_src_v.ToString());
                }
                else
                {
                    TaskVersion = t_src_v;
                    int m_uTaskCount = binaryStream.ReadInt32();
                    ProgressStatus.Maximum = m_uTaskCount;
                    ProgressStatus.Value = 0;
                    int[] numArray = new int[m_uTaskCount];
                    for (int index = 0; index < m_uTaskCount; index++)
                    {
                        numArray[index] = binaryStream.ReadInt32();
                    }
                    t_src = new ATaskTemplFixedData[m_uTaskCount];
                    Application.DoEvents();
                    for (int num4 = 0; num4 < m_uTaskCount; num4++)
                    {
                        Application.DoEvents();
                        t_src[num4] = new ATaskTemplFixedData(t_src_v, binaryStream, numArray[num4], null);
                        ProgressStatus.Value++;
                    }
                    binaryStream.Close();
                    input.Close();
                }
                import_task.Enabled = true;
                t_src_status.Text = "Загружена версия: " + t_src_v + "; Кол-во квестов: " + t_src.Length;
                ProgressStatus.Value = 0;
            }
        }

        private void import_task_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Thread t = new Thread(new ParameterizedThreadStart(ImportT));
                t.Start(ofd.FileName);
            }
        }

        public void ImportT(object filename)
        {
            FileStream input = File.OpenRead((string)filename);
            BinaryReader binaryStream = new BinaryReader(input);
            binaryStream.ReadInt32();
            t_dst_v = binaryStream.ReadInt32();
            if (!versions.Contains(t_dst_v))
            {
                binaryStream.Close();
                input.Close();
                MessageBox.Show("Version Unsupported: " + t_dst_v.ToString());
            }
            else
            {
                int m_uTaskCount = binaryStream.ReadInt32();
                ProgressStatus.Maximum = m_uTaskCount;
                ProgressStatus.Value = 0;
                int[] numArray = new int[m_uTaskCount];
                for (int index = 0; index < m_uTaskCount; index++)
                {
                    numArray[index] = binaryStream.ReadInt32();
                }
                t_dst = new ATaskTemplFixedData[m_uTaskCount];
                Application.DoEvents();
                for (int num4 = 0; num4 < m_uTaskCount; num4++)
                {
                    Application.DoEvents();
                    t_dst[num4] = new ATaskTemplFixedData(t_dst_v, binaryStream, numArray[num4], null);
                    ProgressStatus.Value++;
                }
                binaryStream.Close();
                input.Close();

                ProgressStatus.Value = 0;
                ProgressStatus.Maximum = t_dst.Length;
                for (int a = 0; a < t_dst.Length; ++a)
                {
                    for (int b = 0; b < t_src.Length; ++b)
                    {
                        if (t_dst[a].ID == t_src[b].ID)
                        {
                            t_dst[a].Name = t_src[b].Name;
                            t_dst[a].m_pTaskChar = t_src[b].m_pTaskChar;
                            t_dst[a].conversation = t_src[b].conversation;
                            t_dst[a].talk_procs = t_src[b].talk_procs;
                        }
                        for (int c = 0; c < t_dst[a].sub_quests.Length; ++c)
                        {
                            if (t_dst[a].sub_quests.Length == t_src[b].sub_quests.Length)
                            {
                                if (t_dst[a].sub_quests[c].ID == t_src[b].sub_quests[c].ID)
                                {
                                    t_dst[a].sub_quests[c].Name = t_src[b].sub_quests[c].Name;
                                    t_dst[a].sub_quests[c].m_pTaskChar = t_src[b].sub_quests[c].m_pTaskChar;
                                    t_dst[a].sub_quests[c].conversation = t_src[b].sub_quests[c].conversation;
                                    t_dst[a].sub_quests[c].talk_procs = t_src[b].sub_quests[c].talk_procs;
                                    //debugBox.Text += "ID: " + t_src[b].sub_quests[c].ID + " изменено название на: " + t_src[b].sub_quests[c].Name + "\n";
                                }
                                if (t_dst[a].sub_quests[c].sub_quests != null)
                                {
                                    for (int d = 0; d < t_dst[a].sub_quests[c].sub_quests.Length; ++d)
                                    {
                                        if (t_dst[a].sub_quests[c].sub_quests.Length == t_src[b].sub_quests[c].sub_quests.Length)
                                        {
                                            if (t_dst[a].sub_quests[c].sub_quests[d].ID == t_src[b].sub_quests[c].sub_quests[d].ID)
                                            {
                                                t_dst[a].sub_quests[c].sub_quests[d].Name = t_src[b].sub_quests[c].sub_quests[d].Name;
                                                t_dst[a].sub_quests[c].sub_quests[d].m_pTaskChar = t_src[b].sub_quests[c].sub_quests[d].m_pTaskChar;
                                                t_dst[a].sub_quests[c].sub_quests[d].conversation = t_src[b].sub_quests[c].sub_quests[d].conversation;
                                                t_dst[a].sub_quests[c].sub_quests[d].talk_procs = t_src[b].sub_quests[c].sub_quests[d].talk_procs;
                                                //debugBox.Text += "ID: " + t_src[b].sub_quests[c].ID + " изменено название на: " + t_src[b].sub_quests[c].Name + "\n";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    ProgressStatus.Value++;
                }
                debugBox.Text += "Русификация Tasks.data успешно завершена\n";
                ProgressStatus.Value = 0;

                int num41 = -1819966623;
                FileStream output = new FileStream((string)filename, FileMode.Create, FileAccess.Write);
                BinaryWriter bw = new BinaryWriter(output);
                bw.Write(num41);
                bw.Write(t_dst_v);
                bw.Write(t_dst.Length);
                int[] numArray1 = new int[t_dst.Length];
                bw.Write(new byte[t_dst.Length * 4]);
                ProgressStatus.Maximum = t_dst.Length;
                ProgressStatus.Value = 0;
                TaskVersion = t_dst_v;
                for (int index = 0; index < t_dst.Length; index++)
                {
                    Application.DoEvents();
                    numArray[index] = (int)bw.BaseStream.Position;
                    t_dst[index].Save(t_dst_v, bw);
                    ProgressStatus.Value++;
                }
                bw.BaseStream.Position = 12L;
                for (int num6 = 0; num6 < numArray.Length; num6++)
                {
                    bw.Write(numArray[num6]);
                }
                bw.Close();
                output.Close();
            }
        }

        private void debugBox_TextChanged(object sender, EventArgs e)
        {
            if (debugBox.Lines.Length > 300)
            {
                debugBox.Text = "";
            }
            debugBox.SelectionStart = debugBox.Text.Length;
            debugBox.ScrollToCaret();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
