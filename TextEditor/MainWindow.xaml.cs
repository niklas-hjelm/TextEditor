using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TextEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<BitmapImage> _images = new List<BitmapImage>();
        int _imageIndex = 0;

        public MainWindow()
        {
            InitializeComponent();
            Uri resourceUri = new Uri("/images/dog.jpg", UriKind.Relative);
            _images.Add(new BitmapImage(resourceUri));
            resourceUri = new Uri("/images/kingfisher.jpg", UriKind.Relative);
            _images.Add(new BitmapImage(resourceUri));
            resourceUri = new Uri("/images/praying-mantis.jpg", UriKind.Relative);
            _images.Add(new BitmapImage(resourceUri));
            FontFamilyBox.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            FontSizeBox.ItemsSource = new List<double>()
            { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            Viewer.Source = _images[_imageIndex];
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            _imageIndex++;
            if (_imageIndex == _images.Count)
            {
                _imageIndex = 0;
            }

            Viewer.Source = _images[_imageIndex];
        }

        private void PreviousBtn_Click(object sender, RoutedEventArgs e)
        {
            _imageIndex--;
            if (_imageIndex == -1)
            {
                _imageIndex = _images.Count - 1;
            }
            Viewer.Source = _images[_imageIndex];
        }

        private void FontFamilyBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TextField == null)
            {
                return;
            }

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
