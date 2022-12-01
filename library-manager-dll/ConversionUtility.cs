using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    public static class ConversionUtility
    {
        public static byte[] ToByte(this string hexString)
        {
            return Enumerable
                .Range(0, hexString.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hexString.Substring(x, 2), 16))
                .ToArray();
        }

        public static string ToHex(this byte[] arr)
        {
            var sb = new StringBuilder(arr.Length * 2);
            foreach (byte b in arr)
            {
                sb.AppendFormat("{0:X2}", b);
            }

            return sb.ToString();
        }

        public static bool IsHex(this string str)
        {
            bool returnValue = true;
            try
            {
                str.ToByte();
            }
            catch (Exception)
            {
                return false;
            }

            return returnValue;
        }

        public static int GetInt(this byte[] data, int offset, int length)
        {
            var result = 0;
            for (var i = 0; i < length; i++)
            {
                result = (result << 8) | data[offset + i];
            }

            return result;
        }

        public static string ToAscii(this string hexString)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= hexString.Length - 2; i += 2)
            {
                sb.Append(Convert.ToString(Convert.ToChar(Int32.Parse(hexString.Substring(i, 2), System.Globalization.NumberStyles.HexNumber))));
            }
            return sb.ToString();
        }

        public static int ByteArrayToInteger(byte[] byteArray)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(byteArray);

            return BitConverter.ToInt32(byteArray, 0);
        }
        public static byte[] IntegerToByteArray(int value)
        {
            byte[] intBytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian){
                Array.Reverse(intBytes);
            }

            return intBytes;
        }

        public static byte[] StringListToByteArray(List<string> array,int itemMaxCount, int itemMaxLength)
        {
            byte[] outputBytes = new byte[itemMaxCount*itemMaxLength];
            int index = 0;
            foreach(string val in array)
            {
                byte[] valBytes = StringToByteArray(val);
                Array.Copy(valBytes, 0, outputBytes, index, valBytes.Length);
                index += itemMaxLength;
            }

            return outputBytes;
        }

        public static List<string> ByteArrayToStringList(byte[] byteArray, int itemMaxCount, int itemMaxLength)
        {
            List<string> list = new List<string>();

            for(int i=0;i<itemMaxCount;i++)
            {
                byte[] itemBytes = new byte[itemMaxLength];
                Array.Copy(byteArray, i * itemMaxLength, itemBytes, 0, itemMaxLength);
                string val = ByteArrayToString(itemBytes);
                if(val != null && val != String.Empty)
                {
                    list.Add(val);
                }
            }

            return list;
        }

        public static byte[] StringToByteArray(string data)
        {
            return Encoding.ASCII.GetBytes(data);
        }

        public static string ByteArrayToString(byte[] byteArray)
        {
            return System.Text.Encoding.UTF8.GetString(byteArray, 0, byteArray.Length).Trim('\0');
        }
    }
}
