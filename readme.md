How To use libclamav.dll into a c# application:
The dll file is customized from the dll project library provided by clamav society, by adding three functions scan_file, scan_file_signature and free_memory.

Function scan_file  
==================
  
## Description

    The C++ function int scan_file(char* file, char* virusname, char* error) cans a single file using the database found in files of the directory ./db/
  
## Declaration

    [DllImport("libclamav.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int scan_file(string file, StringBuilder virus, StringBuilder error);
  
## Parameters

    file: Name of the file to be scanned.
    virusname: Name of the virus found if the file is infected.
    error: Error message if exists, else "no error found".

## Return Value

    0: File scanned is clean.
    1: File scanned is infected.
    2: Error while scanning file.
  


Function scan_file_signature
============================
  
## Description

    The C++ function int (char* signature, char* file, char* virusname, char* error) scans a single file using the signature file provided.
  
## Declaration

        [DllImport("libclamav.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int scan_file_signature(string signature, string file, StringBuilder virus, StringBuilder error);

## Parameters

        siganture: File containing the singature (.hdb, .mdb, .fp).
        file: Name of the file to be scanned.
        virusname: Name of the virus found if the file is infected.
        error: Error message if exists, else "no error found".

## Return Value
  
         0: File scanned is clean.
         1: File scanned is infected.
         2: Error while scanning file.


Function free_memory
====================
  
## Description

    The C++ function void free_memory() clears the memory occupied by clamav engine.
  
## Declaration

    [DllImport("libclamav.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void free_memory();

## Parameters


## Return Value
  ------------


Example
=======
The following example shows the usage of the functions scan_file, scan_file_signature and free_memory.

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

        [DllImport("libclamav.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int scan_file(string file, StringBuilder virus, StringBuilder error);
        [DllImport("libclamav.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int scan_file_signature(string signature, string file, StringBuilder virus, StringBuilder error);
        [DllImport("libclamav.dll", CallingConvention = CallingConvention.Cdecl)]
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
