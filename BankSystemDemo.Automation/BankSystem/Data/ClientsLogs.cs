using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace BankSystem
{
    class ClientsLogs
    {
        static FileStream fs;
        static BinaryReader br;
        static BinaryWriter bw;
        public static string OpenStreams()
        {
            try
            {
                String basePath1 = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()))));
                String basePath2 = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())));

                string filepath1 = basePath1 + "//BankSystem//bin//Release//Logs.txt";
                string filepath2 = basePath2 + "//BankSystem//bin//Release//Logs.txt";

                if (File.Exists(filepath1))
                {
                    fs = new FileStream(filepath1, FileMode.Open, FileAccess.ReadWrite);
                }
                else
                {
                    fs = new FileStream(filepath2, FileMode.Open, FileAccess.ReadWrite);
                }

               // fs = new FileStream(basePath + "//BankSystem//bin//Release//Logs.txt", FileMode.Open, FileAccess.ReadWrite);
                
                br = new BinaryReader(fs);
                bw = new BinaryWriter(fs);
                return "Opened";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public static void SetAdress(int Address)
        {
            fs.Seek(2 * Address, SeekOrigin.Begin);
        }
        public static void GetLogs(DataGridView dg)
        {
            while (br.PeekChar() == '|')
            {
                br.ReadChar();
                dg.Rows.Add(br.ReadString(), br.ReadString());
            }
        }
        public static void AddLog(string s1,string s2)
        {
            bw.Write('|');
            bw.Write(s1);
            bw.Write(s2);
            bw.Flush();
        }
        public static bool CloseStreams()
        {
            try
            {
                br.Close();
                bw.Close();
                fs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
