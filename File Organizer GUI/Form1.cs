using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace File_Organizer_GUI
{
    public partial class Form1 : Form
    {
        //Common file extensions. Too lazy to get more.
        string[] docs = { ".doc", ".docx", ".eml", ".log", ".msg", ".odt",
                ".pages", ".rtf", ".tex", ".txt", ".wpd", ".numbers", ".ods",
                ".xlr", ".xls", ".xlsx", ".ppt", ".pptx", ".csv", ".pdf" };
        string[] compressed = { ".7z", ".cbr", ".deb", ".gz", ".pak", ".pkg",
                ".rar", ".rpm", ".tar.gz", ".xapk", ".zip", ".zipx" };
        string[] fonts = { ".fnt", ".otf", ".ttf", ".woff", ".woff2" };
        string[] audios = { ".aif", ".flac", ".m3u", ".m4a", ".mid", ".mp3",
                ".ogg", ".wav", ".wma" };
        string[] videos = { ".3gp", ".asf", ".avi", ".flv", ".m4v", ".mov",
                ".mp4", ".mpg", ".srt", ".swf", ".ts", ".vob", ".wmv" };
        string[] pics = { ".bmp", ".dcm", ".dds", ".djvu", ".gif", ".heic",
                ".jpg", ".jpeg", ".png", ".psd", ".tga", ".tif",".ai", ".cdr", ".emf",
                ".eps", ".ps", ".sketch", ".svg", ".vsdx",".3dm", ".3ds", ".blend",
                ".dae", ".fbx", ".max", ".obj" };
        string[] misc = { ".crdownload", ".ics", ".msi", ".nomedia",
                ".part", ".pkpass", ".torrent", ".iso" };
        string[] exec = { ".apk", ".app", ".bat", ".bin", ".cmd", ".com",
                ".exe", ".ipa", ".jar", ".run", ".sh" };
        string[] web = { ".asp", ".aspx", ".cer", ".cfm", ".csr", ".css",
                ".html", ".js", ".json", ".jsp", ".php", ".xhtml" };

        //Gets the user path of whoever is currently logged in on the computer.
        static string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        static List<string> files = new List<string>();
        static string movedFiles = Path.Combine(userPath, "Documents", "storage.txt");
        static string createdFolders = Path.Combine(userPath, "Documents", "folders.txt");
        
        public Form1()
        {
            InitializeComponent();
        }
        
        private void moveFilesBtn_Click(object sender, EventArgs e)
        {
            //Checks if inputted path is valid
            if (!Directory.Exists(currentPathTxt.Text) ||
                string.IsNullOrWhiteSpace(currentPathTxt.Text))
            {
                logsList.Items.Add("Error: Please input a valid path.");
                return;
            }

            //Disable all buttons until function has been completed.
            moveFilesBtn.Enabled = false;
            arrangeFilesBtn.Enabled = false;
            undoBtn.Enabled = false;

            string sourcePath = currentPathTxt.Text;

            //Get all files from main path
            GetPaths(sourcePath);

            //For each file, move to corresponding folder 
            foreach (string file in files)
            {
                if (Path.GetFileName(file) != "storage.txt" &&
                    Path.GetFileName(file) != "folders.txt")
                {
                    try
                    {
                        MoveFile(file);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }

            moveFilesBtn.Enabled = true;
            arrangeFilesBtn.Enabled = true;
            undoBtn.Enabled = true;
        }

        private void GetPaths(string path)
        {
            //Ensures that the list is always empty
            if (files.Count != 0)
                files.Clear();

            try
            {
                string[] tempFiles = Directory.GetFiles(path);
                foreach (string file in tempFiles)
                {
                    //Ensures that no files are added twice
                    if (!files.Contains(file))
                        files.Add(file);
                }
            }
            catch (UnauthorizedAccessException e) { logsList.Items.Add($"Error: {e.Message}"); }
        }

        private void MoveFile(string filePath)
        {
            //Gets the file extension
            string ext = Path.GetExtension(filePath);
            string destDir = "";

            //Creates destination path based on file extension
            if (docs.Contains(ext))
                destDir = Path.Combine(userPath, "Documents", $"{ext.Remove(0, 1).ToUpper()} files");

            else if (compressed.Contains(ext))
                destDir = Path.Combine(userPath, "Documents", "Compressed", $"{ext.Remove(0, 1).ToUpper()} files");

            else if (fonts.Contains(ext))
                destDir = Path.Combine(userPath, "Documents", "Fonts", $"{ext.Remove(0, 1).ToUpper()} files");

            else if (audios.Contains(ext))
                destDir = Path.Combine(userPath, "Music");

            else if (videos.Contains(ext))
                destDir = Path.Combine(userPath, "Videos");

            else if (pics.Contains(ext))
                destDir = Path.Combine(userPath, "Pictures", $"{ext.Remove(0, 1).ToUpper()} files");

            else if (ext == ".lnk")
                destDir = Path.Combine(userPath, "Desktop");

            else if (misc.Contains(ext))
                destDir = Path.Combine(userPath, "Documents", "Miscellaneous Files", $"{ext.Remove(0, 1).ToUpper()} files");

            //Terminates the function if the extension doesn't exist in any of the arrays
            else
                return;

            //Creates subfolder if it doesn't exist
            if (!Directory.Exists(destDir))
            {
                Directory.CreateDirectory(destDir);
                using (var writer = new StreamWriter(createdFolders, true))
                { writer.WriteLine(destDir); }
            }

            //Deletes file in subfolder if it already exists
            if (File.Exists(Path.Combine(destDir, Path.GetFileName(filePath))))
                File.Delete(Path.Combine(destDir, Path.GetFileName(filePath)));

            try
            {
                //Moves file to subfolder
                File.Move(filePath, Path.Combine(destDir, Path.GetFileName(filePath)));

                //Notes file as a moved file and keeps track of new location
                using (StreamWriter writer = new StreamWriter(movedFiles, true))
                { writer.WriteLine($"{filePath}, {Path.Combine(destDir, Path.GetFileName(filePath))}"); }

                logsList.Items.Add($"Info: {Path.GetFileName(filePath)} moved to {destDir}");
            }
            catch (UnauthorizedAccessException) { logsList.Items.Add($"Error: Access to the path {filePath} was denied."); }
            catch (IOException) { }
        }

        private void arrangeFilesBtn_Click(object sender, EventArgs e)
        {
            //Get source path
            if (!Directory.Exists(currentPathTxt.Text) ||
                string.IsNullOrWhiteSpace(currentPathTxt.Text))
            {
                logsList.Items.Add("Error: Please input a valid path.");
                return;
            }

            //Disable all buttons until function has been completed.
            moveFilesBtn.Enabled = false;
            arrangeFilesBtn.Enabled = false;
            undoBtn.Enabled = false;

            string sourcePath = currentPathTxt.Text;

            //Get all files from main path
            GetPaths(sourcePath);

            //For each file, move to corresponding folder 
            foreach (string file in files)
            {
                if (Path.GetFileName(file) != "storage.txt" &&
                    Path.GetFileName(file) != "folders.txt")
                {
                    try
                    {
                        ArrangeFile(file);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }

            moveFilesBtn.Enabled = true;
            arrangeFilesBtn.Enabled = true;
            undoBtn.Enabled = true;
        }

        private void ArrangeFile(string filePath)
        {
            //Gets the file extension and the current file directory
            string folderName = Path.GetDirectoryName(filePath);
            string ext = Path.GetExtension(filePath);
            string destDir = "";
            string rootDestDir = "";

            //Creates destination path based on file extension
            if (docs.Contains(ext))
            {
                rootDestDir = Path.Combine(folderName, "Documents");
                destDir = Path.Combine(rootDestDir, $"{ext.Remove(0, 1).ToUpper()} files");
            }

            else if (compressed.Contains(ext))
            {
                rootDestDir = Path.Combine(folderName, "Compressed Files");
                destDir = Path.Combine(rootDestDir, $"{ext.Remove(0, 1).ToUpper()} files");
            }

            else if (fonts.Contains(ext))
            {
                rootDestDir = Path.Combine(folderName, "Fonts");
                destDir = Path.Combine(rootDestDir, $"{ext.Remove(0, 1).ToUpper()} files");
            }

            else if (audios.Contains(ext))
            {
                rootDestDir = Path.Combine(folderName, "Audio Files");
                destDir = Path.Combine(rootDestDir, $"{ext.Remove(0, 1).ToUpper()} files");
            }

            else if (videos.Contains(ext))
            {
                rootDestDir = Path.Combine(folderName, "Videos");
                destDir = Path.Combine(rootDestDir, $"{ext.Remove(0, 1).ToUpper()} files");
            }

            else if (pics.Contains(ext))
            {
                rootDestDir = Path.Combine(folderName, "Pictures");
                destDir = Path.Combine(rootDestDir, $"{ext.Remove(0, 1).ToUpper()} files");
            }

            else if (misc.Contains(ext))
            {
                rootDestDir = Path.Combine(folderName, "Miscellaneous Files");
                destDir = Path.Combine(rootDestDir, $"{ext.Remove(0, 1).ToUpper()} files");
            }

            else if (exec.Contains(ext))
            {
                rootDestDir = Path.Combine(folderName, "Executables");
                destDir = Path.Combine(rootDestDir, $"{ext.Remove(0, 1).ToUpper()} files");
            }

            else if (web.Contains(ext))
            {
                rootDestDir = Path.Combine(folderName, "Web Files");
                destDir = Path.Combine(rootDestDir, $"{ext.Remove(0, 1).ToUpper()} files");
            }

            else if (ext == ".lnk")
                destDir = Path.Combine(folderName, "Links");

            //Terminates the function if the extension doesn't exist in any of the arrays
            else
                return;

            //Creates subfolders if they doesn't exist
            if (!Directory.Exists(rootDestDir))
            {
                Directory.CreateDirectory(rootDestDir);
                using (var writer = new StreamWriter(createdFolders, true))
                { writer.WriteLine(rootDestDir); }
            }

            if (!Directory.Exists(destDir))
            {
                Directory.CreateDirectory(destDir);
                using (var writer = new StreamWriter(createdFolders, true))
                { writer.WriteLine(destDir); }
            }

            //Deletes file in subfolder if it already exists
            if (File.Exists(Path.Combine(destDir, Path.GetFileName(filePath))))
                File.Delete(Path.Combine(destDir, Path.GetFileName(filePath)));

            try
            {
                //Moves file to subfolder
                File.Move(filePath, Path.Combine(destDir, Path.GetFileName(filePath)));

                //Notes file as a moved file and keeps track of new location
                using (StreamWriter writer = new StreamWriter(movedFiles, true))
                { writer.WriteLine($"{filePath}, {Path.Combine(destDir, Path.GetFileName(filePath))}"); }

                logsList.Items.Add($"Info: {Path.GetFileName(filePath)} moved to {destDir}");
            }
            catch (UnauthorizedAccessException) { logsList.Items.Add($"Error: Access to the path {filePath} was denied."); }
            catch (IOException) { }
        }

        private void undoBtn_Click(object sender, EventArgs e)
        {
            //Checks if file exists and isn't empty
            if (File.Exists(movedFiles) && !IsTextFileEmpty(movedFiles))
            {
                //Disable all buttons until function has been completed.
                moveFilesBtn.Enabled = false;
                arrangeFilesBtn.Enabled = false;
                undoBtn.Enabled = false;

                //Passes each file path in the text file as a parameter to the Undo function
                foreach (var line in File.ReadLines(movedFiles))
                    Undo(line);

                //Empties list
                if (files.Count != 0)
                    files.Clear();

                //Clears all text in file
                var write = new StreamWriter(movedFiles, false);
                write.Write(string.Empty);
                write.Close();

                //Deletes any created folders
                if (File.Exists(createdFolders) && !IsTextFileEmpty(createdFolders))
                {
                    foreach (var line in File.ReadLines(createdFolders))
                        DeleteFolder(line);

                    //Clears all text in file
                    write = new StreamWriter(createdFolders, false);
                    write.Write(string.Empty);
                    write.Close();
                }

                logsList.Items.Add("Info: All files have been returned to their original locations.");

                moveFilesBtn.Enabled = true;
                arrangeFilesBtn.Enabled = true;
                undoBtn.Enabled = true;
            }
            else
                logsList.Items.Add("Info: No files have been moved.");
        }

        //Checks if text file is truly empty
        private bool IsTextFileEmpty(string fileName)
        {
            var info = new FileInfo(fileName);
            if (info.Length == 0)
                return true;

            // Checks if Bit Format character is the only thing in the file
            if (info.Length < 6)
            {
                var content = File.ReadAllText(fileName);
                return content.Length == 0;
            }
            return false;
        }

        //Moves all files back to their previous locations
        private void Undo(string file)
        {
            //Creates an array whose elements are the strings before and after
            //the comma in the text file
            string[] locations = file.Split(',');

            //Stops method if source file isn't found
            if (!File.Exists(locations[1]))
                return;

            //Delete destination file if it already exists
            if (File.Exists(locations[0]))
                File.Delete(locations[0]);

            //Move file to previous location
            File.Move(locations[1], locations[0]);
        }

        private void DeleteFolder(string directory)
        {
            //Exits method if folder has already been deleted
            if (!Directory.Exists(directory))
                return;

            //Deletes folder and subfolders/files
            Directory.Delete(directory, true);
        }
    }
}