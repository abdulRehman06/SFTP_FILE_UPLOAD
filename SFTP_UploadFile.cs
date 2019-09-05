using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;

namespace SFTP_UPLOAD
{
    class SFTP_UploadFile
    {

        private string SFTPHostIP = "" ;
        private int    SFTPPort = -1 ;
        private string SFTPUserName = "";
        private string SFTPPassword = "";

       

        public SFTP_UploadFile(string inSFTPHostIP, int inSFTPPort, string inSFTPUserName, string inSFTPPassword) {


            SFTPHostIP = inSFTPHostIP;
            SFTPPort = inSFTPPort;
            SFTPUserName = inSFTPUserName;
            SFTPPassword = inSFTPPassword ;

        }



        public bool UploadFile(  String uploadFileName, String SFTPUploadPath )
        {


            using (var client = new SftpClient(SFTPHostIP, SFTPPort, SFTPUserName, SFTPPassword))
            {
                try
                {
                    client.Connect();
                    Console.WriteLine("Connected to {0}", SFTPHostIP);
                    try
                    {
                        client.ChangeDirectory(SFTPUploadPath);
                        Console.WriteLine("Uploading directory : {0}", SFTPUploadPath);

                        /**
                         * this will print all file names at server 
                         * 
                         * var listDirectory = client.ListDirectory(workingdirectory);
                         Console.WriteLine("Listing directory:");   
                         foreach (var fi in listDirectory)
                         {
                             Console.WriteLine(" - " + fi.Name);
                         } */



                        /**
                         * 
                         * read file from system 
                         */
                        using (var fileStream = new FileStream(uploadFileName, FileMode.Open))
                        {
                            Console.WriteLine("Uploading {0}  ({1:N0} bytes)", uploadFileName, fileStream.Length);
                            client.BufferSize = 4 * 1024; // bypass Payload error large files
                            client.UploadFile(fileStream, Path.GetFileName(uploadFileName));
                        }
                    }

                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString() + "\nSFTP Path not found : " + SFTPUploadPath + "\n");
                        return false;
                    }

                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString() + "\nUnable to connect host : {0}", SFTPHostIP);
                    return false;
                }
            }



          
        }
    }

   
}
