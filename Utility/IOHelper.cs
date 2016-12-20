using System;
using System.Text;
using System.IO;

namespace Utility
{
    public class IOHelper
    {
        #region Folder
        public static bool FolderHandle(Enums.Handle handle, string folderName, string newFolderName, string folderPath)
        {
            bool status = false;
            string directory = folderPath + folderName;
            switch (handle)
            {
                case Enums.Handle.Create:
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                        status = true;
                    }
                    break;
                case Enums.Handle.Delete:
                    if (Directory.Exists(directory))
                    {
                        Directory.Delete(directory, true);
                        status = true;
                    }
                    break;
                case Enums.Handle.Update:
                    if (Directory.Exists(directory))
                    {
                        Directory.Delete(directory, false);
                        Directory.CreateDirectory(folderPath + newFolderName);
                        status = true;
                    }
                    break;
            }
            return status;
        }

        /// <summary>
        /// folder list 
        /// </summary>
        /// <param name="path"></param>
        /// <returns>folderName1,folderName,folderName3,|fileName1,fileName2,fileName3,</returns>
        public static string FolderList(string path)
        {
            StringBuilder sb = new StringBuilder();
            string[] directories = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
            if (directories.Length > 0 && directories != null)
            {
                foreach (string directory in directories)
                {
                    DirectoryInfo di = new DirectoryInfo(directory);
                    sb.Append(string.Concat(di.Name, ","));
                }
            }
            string[] files = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly);
            if (files.Length > 0 && files != null)
            {
                sb.Append('|');
                foreach (string file in files)
                {
                    DirectoryInfo info = new DirectoryInfo(file);
                    sb.Append(string.Concat(info.Name, ","));
                }
            }
            return sb.ToString();
        }
        #endregion

        #region File
        public static bool WriteFile(string fileName, string fileContent, string filePath)
        {
            bool status = false;
            string directory = filePath + fileName;
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.Write(fileContent);
                sw.Flush();
                status = true;
            }
            return status;
        }

        public static string ReadFile(string filePath)
        {
            string fileContent = string.Empty;
            using (StreamReader sr = new StreamReader(filePath))
            {
                fileContent = sr.ReadToEnd();
            }
            return fileContent;
        }
        #endregion

        #region Path
        public static string GetPath(Enums.Handle handle, string path)
        {          
            string currentDirectory = Environment.CurrentDirectory;
            string fileParentPath = Path.GetDirectoryName(path);     
            string dirParentPath = Path.GetDirectoryName(path);      
            string fileExt = Path.GetExtension(path);         
            string fileName = Path.GetFileName(path);
            string fileNameNoExt = Path.GetFileNameWithoutExtension(path);
            string fullPath = Path.GetFullPath(path);
            string rootPath = Path.GetPathRoot(path);
            return "";
        }
        #endregion
    }
}
