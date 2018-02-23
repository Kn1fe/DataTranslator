//(c)Rey35
using DataTranslator;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace sTASKedit
{
	class Extensions
	{
		public static string decrypt(int key, byte[] text)
		{
			if (Form1.TaskVersion >= 53)
			{
				string result = "";
				byte[] dbyte = new byte[2];
				for (int i = 0; i < text.Length; i += 2)
				{
					dbyte = BitConverter.GetBytes((short)key);
					byte[] expr_20_cp_0 = dbyte;
					int expr_20_cp_1 = 0;
					expr_20_cp_0[expr_20_cp_1] ^= text[i];
					byte[] expr_37_cp_0 = dbyte;
					int expr_37_cp_1 = 1;
					expr_37_cp_0[expr_37_cp_1] ^= text[i + 1];
					result += BitConverter.ToChar(dbyte, 0);
				}
				string arg_6F_0 = result;
				char[] trimChars = new char[1];
				return arg_6F_0.TrimEnd(trimChars);
			}
			else
			{
				return ByteArray_to_UnicodeString(text);
			}
		}
		public static byte[] encrypt(int key, string text, int length, bool appendZero)
		{
			byte[] result;
			if (appendZero)
				result = new byte[length + 2];
			else
				result = new byte[length];
			if (Form1.TaskVersion >= 53)
			{
				byte[] dbyte = new byte[2];
				for (int i = 0; i < result.Length; i += 2)
				{
					dbyte = BitConverter.GetBytes((short)key);
					result[i] = dbyte[0];
					result[i + 1] = dbyte[1];
					if (i / 2 < text.Length)
					{
						dbyte = BitConverter.GetBytes(text[i / 2]);
						byte[] expr_57_cp_0 = result;
						int expr_57_cp_1 = i;
						expr_57_cp_0[expr_57_cp_1] ^= dbyte[0];
						byte[] expr_70_cp_0 = result;
						int expr_70_cp_1 = i + 1;
						expr_70_cp_0[expr_70_cp_1] ^= dbyte[1];
					}
				}
				return result;
			}
			else
			{
				return UnicodeString_to_ByteArray2(text, length);
			}
		}

        public static IEnumerable<uint> GetPowers(uint value)
        {
            uint v = value;
            while (v > 0)
            {
                yield return (v & 0x01);
                v >>= 1;
            }
        }

        internal static string ByteArray_to_HexString(byte[] value)
		{
			return BitConverter.ToString(value);
		}
		internal static byte[] HexString_to_ByteArray(string value)
		{
			char[] chArray = new char[]
			{
				'-'
			};
			string[] strArray = value.Split(chArray);
			byte[] numArray = new byte[strArray.Length];
			for (int index = 0; index < strArray.Length; index++)
				numArray[index] = Convert.ToByte(strArray[index], 16);
			return numArray;
		}

		internal static string ByteArray_to_GbkString(byte[] text)
		{
			Encoding encoding = Encoding.GetEncoding("GBK");
			char[] array = new char[1];
			char[] chArray = array;
			return encoding.GetString(text).Split(chArray)[0];
		}
		internal static byte[] GbkString_to_ByteArray(string text, int length)
		{
			Encoding encoding = Encoding.GetEncoding("GBK");
			byte[] numArray = new byte[length];
			byte[] bytes = encoding.GetBytes(text);
			if (numArray.Length > bytes.Length)
				Array.Copy(bytes, numArray, bytes.Length);
			else
			{
				byte[] numArray2 = bytes;
				byte[] numArray3 = numArray;
				int length2 = numArray3.Length;
				Array.Copy(numArray2, numArray3, length2);
			}
			return numArray;
		}
		public static byte[] GbkString_to_ByteArray2(string text, int length)
		{
			Encoding enc = Encoding.GetEncoding("GBK");
			byte[] target = new byte[length];
			byte[] source = enc.GetBytes(text);
			if (target.Length > source.Length)
				Array.Copy(source, target, source.Length);
			else
				Array.Copy(source, target, target.Length);
			return target;
		}

		internal static string ByteArray_to_UnicodeString(byte[] text)
		{
			Encoding encoding = Encoding.GetEncoding("Unicode");
			char[] array = new char[1];
			char[] chArray = array;
			return encoding.GetString(text).Split(chArray)[0];
		}
		internal static byte[] UnicodeString_to_ByteArray(string text, int length)
		{
			Encoding encoding = Encoding.GetEncoding("Unicode");
			byte[] numArray = new byte[length];
			byte[] bytes = encoding.GetBytes(text);
			if (numArray.Length > bytes.Length)
				Array.Copy(bytes, numArray, bytes.Length);
			else
			{
				byte[] numArray2 = bytes;
				byte[] numArray3 = numArray;
				int length2 = numArray3.Length;
				Array.Copy(numArray2, numArray3, length2);
			}
			return numArray;
		}
		public static byte[] UnicodeString_to_ByteArray2(string text, int length)
		{
			Encoding enc = Encoding.GetEncoding("Unicode");
			byte[] target = new byte[length];
			byte[] source = enc.GetBytes(text);
			if (target.Length > source.Length)
				Array.Copy(source, target, source.Length);
			else
				Array.Copy(source, target, target.Length);
			return target;
		}

		public static string SecondsToString(uint time)
		{
			uint days = time / 86400;
			time = time - (days * 86400);
			uint hours = time / 3600;
			time = time - (hours * 3600);
			uint minutes = time / 60;
			uint seconds = time - (minutes * 60);
			return (days.ToString("D2") + "-" + hours.ToString("D2") + ":" + minutes.ToString("D2") + ":" + seconds.ToString("D2"));
		}
		public static uint StringToSecond(string time)
		{
			char[] chArray = new char[]
			{
				'-', ':'
			};
			string[] times = time.Split(chArray);
			return (86400 * Convert.ToUInt32(times[0])) + (3600 * Convert.ToUInt32(times[1])) + (60 * Convert.ToUInt32(times[2])) + Convert.ToUInt32(times[3]);
		}

		public static string ConvertToClientX(float x)
		{
			double cx = 400 + Math.Truncate(x * 0.1);
			return cx.ToString();
		}
		public static string ConvertToClientY(float y)
		{
			double cy = Math.Truncate(y * 0.1);
			return cy.ToString();
		}
		public static string ConvertToClientZ(float z)
		{
			double cz = 550 + Math.Truncate(z * 0.1);
			return cz.ToString();
		}

		public static System.Collections.ArrayList SetIndexes(System.Windows.Forms.DataGridViewSelectedCellCollection SelectedCells)
		{
			System.Collections.ArrayList result = new System.Collections.ArrayList();
			for (int i = 0; i < SelectedCells.Count; i++)
			{
				bool check = true;
				for (int k = 0; k < result.Count; k++)
				{
					if ((int)result[k] == SelectedCells[i].RowIndex)
					{
						check = false;
						break;
					}
				}
				if (check)
					result.Add(SelectedCells[i].RowIndex);
			}
			result.Sort();
			return result;
		}

		public static System.Collections.ArrayList SetIndexesColumns(System.Windows.Forms.DataGridViewSelectedCellCollection SelectedCells)
		{
			System.Collections.ArrayList result = new System.Collections.ArrayList();
			for (int i = 0; i < SelectedCells.Count; i++)
			{
				bool check = true;
				for (int k = 0; k < result.Count; k++)
				{
					if ((int)result[k] == SelectedCells[i].ColumnIndex)
					{
						check = false;
						break;
					}
				}
				if (check) result.Add(SelectedCells[i].ColumnIndex);
			}
			result.Sort();
			return result;
		}
	}
}
//(c)Rey35