using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace ReSCat.View
{
    /// <summary>
    /// Logika interakcji dla klasy NotesView.xaml
    /// </summary>
    public partial class NotesView : Window
    {
        private string fileNameSet;
        public NotesView()
        {
            InitializeComponent();
        }

        private void DragnMove(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            this.DragMove();
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(fileNameSet))
            {
                TextRange Range = new TextRange(NoteInTextBox.Document.ContentStart, NoteInTextBox.Document.ContentEnd);
                File.WriteAllText(fileNameSet, Range.Text);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Do you want to create a new note? (YES), or select existing one? (NO) " +
                "- messagebox needs fixing", "Warning", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);


            if (result == MessageBoxResult.No)
            {

                OpenFileDialog dialog = new OpenFileDialog()
                {
                    Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
                };
                if (dialog.ShowDialog() == true)
                {
                    TextRange Range = new TextRange(NoteInTextBox.Document.ContentStart, NoteInTextBox.Document.ContentEnd);
                    FileStream fileStream = new FileStream(dialog.FileName, FileMode.OpenOrCreate);
                    Range.Load(fileStream, DataFormats.Text);
                    fileNameSet = dialog.FileName;

                    fileStream.Close();
                }

            }
            else if (result == MessageBoxResult.Yes)
            {
                
                SaveFileDialog dialog = new SaveFileDialog()
                {
                    Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
                };

                if (dialog.ShowDialog() == true)
                {
                    TextRange Range = new TextRange(NoteInTextBox.Document.ContentStart, NoteInTextBox.Document.ContentEnd);
                    File.WriteAllText(dialog.FileName, String.Empty);
                    FileStream fileStream = new FileStream(dialog.FileName, FileMode.OpenOrCreate);
                    Range.Load(fileStream, DataFormats.Text);
                    fileNameSet = dialog.FileName;

                    fileStream.Close();
                }
            }
            else
            {

            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
            };
            if (dialog.ShowDialog() == true)
            {
                TextRange Range = new TextRange(NoteInTextBox.Document.ContentStart, NoteInTextBox.Document.ContentEnd);
                FileStream fileStream = new FileStream(dialog.FileName, FileMode.OpenOrCreate);
                Range.Load(fileStream, DataFormats.Text);
                fileNameSet = dialog.FileName;

                fileStream.Close();
            }
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
            };

            if (dialog.ShowDialog() == true)
            {
                TextRange Range = new TextRange(NoteInTextBox.Document.ContentStart, NoteInTextBox.Document.ContentEnd);
                File.WriteAllText(dialog.FileName, Range.Text);
                
                fileNameSet = dialog.FileName;                
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Close Window?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //After pressing No, currently no idea
            }
            else
            {
                this.Close();
                //Application.Current.Shutdown(); //Close app, saving is automatic so no msg needed?
            }
        }
    }
}
