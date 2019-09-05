using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFTP_UPLOAD;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System.IO;

namespace SFTP_UPLOAD
{
    class Program
    {
        static void Main(string[] args)
        {
            const int port = 23;
            const string host = @"192.168.42.186";
            //const string host = @"sftp://192.168.42.186";
            const string username = @"hasan";
            const string password = @"tpstps";
            const string workingdirectory = @"/ImportFiles/Custo/mer/";
            const string uploadfile = @"D:\Encrypted-CardProduction.txtCPE00000057CARDEXPORT20190829184508-AL_Fardan_Debit";

            Console.WriteLine("\r\n Creating client and connecting");
            Console.WriteLine("   \r\n host  :" + host  + "   \r\n port  :" + port + "   \r\n username  :" + username + "   \r\n password  :" + password);

            SFTP_UploadFile abc = new SFTP_UploadFile(host, port, username, password);


            if (abc.UploadFile(uploadfile, workingdirectory) == true)
            {
                Console.Write("File Uploaded ");
            }
            else {
                Console.Write("Unable to  Uploaded File  " );
            }

            Console.Write("hello");
        }
    }
}
