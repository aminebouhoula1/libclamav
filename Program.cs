using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BlackSun_clamav
{
    class Program
    {

        [DllImport("libclamavd.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int scan_file(string file, StringBuilder virus, StringBuilder error);
        [DllImport("libclamavd.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int scan_file_signature(string signature, string file, StringBuilder virus, StringBuilder error);
        [DllImport("libclamavd.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void free_memory();

        static void Main(string[] args)
        {
            StringBuilder virus = new StringBuilder(1024);
            StringBuilder error = new StringBuilder(1024);
            string signature_file;
            int i = 0;
            int start = 0;
            int res = -1;
            if (args[0] == "-d")
            {
                signature_file = args[1];
                start = 2;
            }

            for (i = start; i < args.Length; i++)
            {
                if (start == 2)
                {
                    res = scan_file_signature(args[1], args[i], virus, error);
                }
                else
                {
                    res = scan_file(args[i], virus, error);
                }
                free_memory();
                if (res == 0)
                {
                    Console.Write("File <<" + args[i] + ">> is clean\n");
                }
                else if (res == 1)
                {
                    Console.Write("File <<" + args[i] + ">> is infected with virus : <<" + virus + ">>\n");
                }
                else
                {
                    Console.Write("Error occured while scanning the file <<" + args[i] + ">> : " + error + "\n");
                }
            }
        }
    }
}
