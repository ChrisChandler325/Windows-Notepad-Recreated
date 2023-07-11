using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Forms;


namespace ChristopherChandlerM8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TextDocument textdoc = new TextDocument();
        Boolean modified = false;

        public MainWindow()
        {
            
            InitializeComponent();
            
        }
        private void openFile(object sender, RoutedEventArgs e)
        {
            if (handler())
            {
                textdoc.Open();
                box.Text = textdoc.Text;
                unsetModifiedState();
            }
        }
        private Boolean handler()
        {
            if (!modified) return true;

            string messageBoxText = "The text needs to be saved.\nDo you want to save?";
            string caption = "Text Editor";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            // Display message box
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);
            // Process message box results
            switch (messageBoxResult)
            {
                case MessageBoxResult.Yes: // Save document and exit
                    //startSave();
                    return true;

                case MessageBoxResult.No: // Exit without saving
                    return true;

                case MessageBoxResult.Cancel: // Don't exit
                    return false;
            }
            return false;
        }
        private void about(object sender, RoutedEventArgs e)
        {
            string messageBoxText = "This is a text editor made by Christopher Chandler with a pawprint of cjc7nw.";
            string caption = "About";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            // Display message box
            System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);
            // No need to process message box results since OK is the only option.
        }
        private void exit(object sender, RoutedEventArgs e)
        {
            if (handler())
            {
                Close();
            }
        }
        private void txtNew(object sender, RoutedEventArgs e)
        {
            if (handler())
            {
                textdoc.New();
                box.Text = textdoc.Text;
                unsetModifiedState();
            }
        }
        private void saveasTxt(object sender, RoutedEventArgs e)
        {
            textdoc.startSave();
            unsetModifiedState();
        }
        private void savetxt(object sender, RoutedEventArgs e)
        {
            textdoc.saveDoc();
            unsetModifiedState();
        }
        private void textChanged(Object sender, RoutedEventArgs e)
        {
            textdoc.Text = box.Text;
            setModifiedState();
        } 
        private void setModifiedState()
        {
            modified = true;
            save.IsEnabled = true;
        }
        private void unsetModifiedState()
        {
            modified = false;
            save.IsEnabled = false;
        }
    }
}
