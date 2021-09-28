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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextEditor
{
    /// <summary>
    /// Interaction logic for Editor.xaml
    /// </summary>
    public partial class Editor : UserControl
    {
        public Editor()
        {
            InitializeComponent();
            FontFamilyBox.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            FontSizeBox.ItemsSource = new List<double>()
            { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
        }

        private void FontFamilyBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TextField == null)
            {
                return;
            }
            //TODO: fix when none selected and only apply on new text.

            if (e.Source is ComboBox box && box.SelectedItem != null)
            {
                TextField.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, box.SelectedItem);
            }
        }

        private void FontSizeBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextField.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, FontSizeBox.Text);
        }

        private void TextField_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object temp = TextField.Selection.GetPropertyValue(TextElement.FontWeightProperty);
            BoldToggle.IsChecked = (temp != DependencyProperty.UnsetValue) && temp.Equals(FontWeights.Bold);
            temp = TextField.Selection.GetPropertyValue(TextElement.FontStyleProperty);
            ItalicToggle.IsChecked = (temp != DependencyProperty.UnsetValue) && temp.Equals(FontStyles.Italic);
            temp = TextField.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            UnderlineToggle.IsChecked = (temp != DependencyProperty.UnsetValue) && temp.Equals(TextDecorations.Underline);

            temp = TextField.Selection.GetPropertyValue(TextElement.FontFamilyProperty);
            FontFamilyBox.SelectedItem = temp;
            temp = TextField.Selection.GetPropertyValue(TextElement.FontSizeProperty);
            FontSizeBox.Text = temp.ToString();
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Open);
                TextRange range = new TextRange(TextField.Document.ContentStart, TextField.Document.ContentEnd);
                range.Load(fileStream, DataFormats.Rtf);
            }
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create);
                TextRange range = new TextRange(TextField.Document.ContentStart, TextField.Document.ContentEnd);
                range.Save(fileStream, DataFormats.Rtf);
            }
        }
    }
}
