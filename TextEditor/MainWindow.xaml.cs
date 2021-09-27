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

namespace TextEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static List<FontFamily> fonts = new List<FontFamily>() {
            new FontFamily("Courier New"),
            new FontFamily("Arial"),
            new FontFamily("Constantia")
    };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void FontFamilyBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TextField == null)
            {
                return;
            }
            //TODO: fix when none selected and only apply on new text.

            if (e.Source is ComboBox box)
            {
                TextField.Selection.ApplyPropertyValue(FontFamilyProperty, fonts[box.SelectedIndex]);
            }
        }

        private void BoldToggle_Checked(object sender, RoutedEventArgs e)
        {
            if (TextField == null)
            {
                return;
            }
            //TODO: fix when none selected and only apply on new text.

            TextField.Selection.ApplyPropertyValue(FontWeightProperty, FontWeights.Bold);
        }

        private void BoldToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            if (TextField == null)
            {
                return;
            }
            //TODO: fix when none selected and only apply on new text.

            TextField.Selection.ApplyPropertyValue(FontWeightProperty, FontWeights.Normal);
        }

        private void ItalicToggle_Checked(object sender, RoutedEventArgs e)
        {
            if (TextField == null)
            {
                return;
            }
            //TODO: fix when none selected and only apply on new text.

            TextField.Selection.ApplyPropertyValue(FontStyleProperty, FontStyles.Italic);
        }

        private void ItalicToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            if (TextField == null)
            {
                return;
            }
            //TODO: fix when none selected and only apply on new text.

            TextField.Selection.ApplyPropertyValue(FontStyleProperty, FontStyles.Normal);
        }

        private void UnderlineToggle_Checked(object sender, RoutedEventArgs e)
        {
            if (TextField == null)
            {
                return;
            }
            //TODO: fix when none selected and only apply on new text.

            TextField.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
        }

        private void UnderlineToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            if (TextField == null)
            {
                return;
            }
            //TODO: fix when none selected and only apply on new text.

            TextField.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
        }

        private void FontSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (TextField == null)
            {
                return;
            }
            //TODO: fix when none selected and only apply on new text.

            if (e.Source is Slider slider)
            {
                TextField.Selection.ApplyPropertyValue(FontSizeProperty, slider.Value);
            }
        }
      
    }
}
