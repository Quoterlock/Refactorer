using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Refactorer
{
    public class FileManager
    {
        public FileManager() { }

        public string Read(string filePath)
        {
            string fileContent = string.Empty;

            try
            {
                if (File.Exists(filePath))
                {
                    fileContent = File.ReadAllText(filePath);
                }
                else
                {
                    throw new Exception("File doesn't exist");
                }
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred: " + e.Message);
            }

            return fileContent;
        }

        public void Save(string filePath, string text)
        {
            try
            {
                File.WriteAllText(filePath, text);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void SaveAs(string filePath, string text)
        {
            try
            {
                var file = File.Create(filePath);
                file.Close();
                File.WriteAllText(filePath, text);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
