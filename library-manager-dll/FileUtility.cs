using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    public class FileUtility
    {
        public static bool DeleteFile(string path)
        {
            if(File.Exists(path))
                File.Delete(path);

            return true;
        }
        public static byte[] ReadBlock(int count,int blocksize, string path)
        {

            byte[] buffer = new byte[blocksize];

            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
            {
                fileStream.Seek((count-1)*blocksize, SeekOrigin.Begin);
                fileStream.Read(buffer, 0, buffer.Length);
            }

            return buffer;
        }

        public static bool AppendBlock(byte[] data,string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                fileStream.Seek(0, SeekOrigin.End);
                fileStream.Write(data, 0, data.Length);
            }

            return true;
        }

        public static bool UpdateBlock(byte[] data, int count, int blocksize, string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                fileStream.Seek((count-1) * blocksize, SeekOrigin.Begin);
                fileStream.Write(data, 0, data.Length);
            }

            return true;
        }

        public static bool DeleteBlock(int count, int blocksize, string path)
        {
            byte[] data = new byte[blocksize];

            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                fileStream.Seek((count-1) * blocksize, SeekOrigin.Begin);
                fileStream.Write(data, 0, data.Length);
            }

            return true;
        }
    }
}
