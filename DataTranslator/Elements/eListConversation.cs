using System;
using System.IO;
using System.Text;

namespace Elements
{
    public class eListConversation
    {
        public eListConversation(byte[] Bytes)
        {
            MemoryStream ms = new MemoryStream(Bytes);
            BinaryReader br = new BinaryReader(ms);
            DialogCount = br.ReadInt32();
            Dialogs = new eDialog[DialogCount];
            for (int d = 0; d < DialogCount; d++)
            {
                Dialogs[d] = new eDialog();
                Dialogs[d].DialogID = br.ReadInt32();
                Dialogs[d].DialogName = br.ReadBytes(128);
                Dialogs[d].QuestionCount = br.ReadInt32();
                Dialogs[d].Questions = new eQuestion[Dialogs[d].QuestionCount];
                for (int q = 0; q < Dialogs[d].QuestionCount; q++)
                {
                    Dialogs[d].Questions[q] = new eQuestion();
                    Dialogs[d].Questions[q].QuestionID = br.ReadInt32();
                    Dialogs[d].Questions[q].Control = br.ReadInt32();
                    Dialogs[d].Questions[q].QuestionTextLength = br.ReadInt32();
                    Dialogs[d].Questions[q].QuestionText = br.ReadBytes(2 * Dialogs[d].Questions[q].QuestionTextLength);
                    Dialogs[d].Questions[q].ChoiceQount = br.ReadInt32();
                    Dialogs[d].Questions[q].Choices = new eChoice[Dialogs[d].Questions[q].ChoiceQount];
                    for (int c = 0; c < Dialogs[d].Questions[q].ChoiceQount; c++)
                    {
                        Dialogs[d].Questions[q].Choices[c] = new eChoice();
                        Dialogs[d].Questions[q].Choices[c].Control = br.ReadInt32();
                        Dialogs[d].Questions[q].Choices[c].ChoiceText = br.ReadBytes(132);
                    }
                }
            }
            br.Close();
            ms.Close();
        }
        public int DialogCount;
        public eDialog[] Dialogs;
        public byte[] GetBytes()
        {
            MemoryStream ms = new MemoryStream(DialogCount);
            BinaryWriter bw = new BinaryWriter(ms);

            bw.Write(DialogCount);
            for (int d = 0; d < DialogCount; d++)
            {
                bw.Write(Dialogs[d].DialogID);
                bw.Write(Dialogs[d].DialogName);
                bw.Write(Dialogs[d].QuestionCount);
                for (int q = 0; q < Dialogs[d].QuestionCount; q++)
                {
                    bw.Write(Dialogs[d].Questions[q].QuestionID);
                    bw.Write(Dialogs[d].Questions[q].Control);
                    bw.Write(Dialogs[d].Questions[q].QuestionTextLength);
                    bw.Write(Dialogs[d].Questions[q].QuestionText);
                    bw.Write(Dialogs[d].Questions[q].ChoiceQount);
                    for (int c = 0; c < Dialogs[d].Questions[q].ChoiceQount; c++)
                    {
                        bw.Write(Dialogs[d].Questions[q].Choices[c].Control);
                        bw.Write(Dialogs[d].Questions[q].Choices[c].ChoiceText);
                    }
                }
            }

            byte[] result = ms.ToArray();
            bw.Close();
            ms.Close();
            return result;
        }
    }

    public class eChoice
    {
        public int Control;
        public byte[] ChoiceText;
        public string GetText()
        {
            Encoding enc = Encoding.GetEncoding("Unicode");
            return enc.GetString(ChoiceText);
        }
        public void SetText(string Value)
        {
            Encoding enc = Encoding.GetEncoding("Unicode");
            byte[] target = new byte[132];
            byte[] source = enc.GetBytes(Value);
            if (target.Length > source.Length)
            {
                Array.Copy(source, target, source.Length);
            }
            else
            {
                Array.Copy(source, target, target.Length);
            }
            ChoiceText = target;
        }
    }

    public class eQuestion
    {
        public int QuestionID;
        public int Control;
        public int QuestionTextLength;
        public byte[] QuestionText;
        public string GetText()
        {
            Encoding enc = Encoding.GetEncoding("Unicode");
            return enc.GetString(QuestionText);
        }
        public void SetText(string Value)
        {
            Encoding enc = Encoding.GetEncoding("Unicode");
            QuestionText = enc.GetBytes(Value + (char)0);
            QuestionTextLength = QuestionText.Length / 2;
        }
        public int ChoiceQount;
        public eChoice[] Choices;
    }

    public class eDialog
    {
        public int DialogID;
        public byte[] DialogName;
        public string GetText()
        {
            Encoding enc = Encoding.GetEncoding("Unicode");
            return enc.GetString(DialogName);
        }
        public void SetText(string Value)
        {
            Encoding enc = Encoding.GetEncoding("Unicode");
            byte[] target = new byte[128];
            byte[] source = enc.GetBytes(Value);
            if (target.Length > source.Length)
            {
                Array.Copy(source, target, source.Length);
            }
            else
            {
                Array.Copy(source, target, target.Length);
            }
            DialogName = target;
        }
        public int QuestionCount;
        public eQuestion[] Questions;
    }
}
