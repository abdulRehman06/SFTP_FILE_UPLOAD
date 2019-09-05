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
            const string workingdirectory = @"/ImportFiles/Customer/";
            const string uploadfile = @"D:\SFTP_UPLOAD.txt";

            Console.WriteLine("\r\n Creating client and connecting");
            Console.WriteLine("   \r\n host  :" + host  + "   \r\n port  :" + port + "   \r\n username  :" + username + "   \r\n password  :" + password);
            using (var client = new SftpClient(host, port, username, password))
            {
                client.Connect();
                Console.WriteLine("Connected to {0}", host);

                client.ChangeDirectory(workingdirectory);
                Console.WriteLine("Changed directory to {0}", workingdirectory);

                var listDirectory = client.ListDirectory(workingdirectory);
                Console.WriteLine("Listing directory:");
                foreach (var fi in listDirectory)
                {
                    Console.WriteLine(" - " + fi.Name);
                }

                using (var fileStream = new FileStream(uploadfile, FileMode.Open))
                {
                    Console.WriteLine("Uploading {0} ({1:N0} bytes)", uploadfile, fileStream.Length);
                    client.BufferSize = 4 * 1024; // bypass Payload error large files
                    client.UploadFile(fileStream, Path.GetFileName(uploadfile));
                }
            }

            Console.Write("hello");
        }
    }
}
