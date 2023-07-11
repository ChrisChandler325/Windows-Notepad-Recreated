using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace ChristopherChandlerM8
{
    [Serializable]

    //open and save text file methods
    class TextDocument
    {
        
        public TextDocument() 
        { 
        }
        public String Text { get; set; }
        public String FilePath { get; set; }

        public Boolean Load()
        {
            this.Text = "";
            try
            {
                using (StreamReader sr = new StreamReader(this.FilePath))
                {
                    String line = "";
                    while((line = sr.ReadLine()) != null)
                    {
                        this.Text += line;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error",ex.Message);
                return false;
            }

            
        }
        public void New() 
        {
            this.Text = "";
        }
        public void Open()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "txt files (*.txt) |*.txt";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.FilePath= openFileDialog.FileName;
                this.Load();
            }
        }
        public void startSave()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            // http://msdn.microsoft.com/en-us/library/system.io.path.getdirectoryname.aspx

            if (this.FilePath != "")
            {
                saveFileDialog.FileName = System.IO.Path.GetFileName(this.FilePath);
                saveFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(this.FilePath);
            }

            saveFileDialog.DefaultExt = ".txt"; // Default file extension
            saveFileDialog.Filter = "txt files (*.txt)|*.txt"; // Filter files by extension
            // Show save file dialog box
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.FilePath = saveFileDialog.FileName;
                saveDoc();
            }
        }
        public void saveDoc()
        {
            var currentTxt = new TextDocument();
            if (this.Text != null)
            {
                currentTxt.Text = this.Text.ToString();
            }
            else
            {
                currentTxt.Text = " ";
            }
            
            currentTxt.FilePath = this.FilePath;
            if (currentTxt.FilePath != null)
            {
                try
                {
                    var fStreamTxt = new FileStream(FilePath, FileMode.Create);
                    using (StreamWriter s = new StreamWriter(fStreamTxt))
                    {
                        s.Write(currentTxt.Text);
                    }
                    fStreamTxt.Close();
                }catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                startSave();
            }
            this.FilePath= currentTxt.FilePath;
        }
    }
}
