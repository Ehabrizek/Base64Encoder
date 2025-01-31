using System.Drawing;
using System.Windows;
using System.Windows.Controls;

namespace Base64Encoder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".png"; // Default file extension

            bool result = dialog.ShowDialog() ?? false;
            if (result)
            {
                FileTextBox.Text = dialog.FileName;
            }
        }

        private void ProcessButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(FileTextBox.Text))
            {
                try
                {
                    string base64Image = "";
                    using (Bitmap bitmap = new Bitmap(FileTextBox.Text))
                    {
                        byte[]? bytes = ConvertImageToBytes(bitmap);
                        if (bytes != null)
                        {
                            base64Image = Convert.ToBase64String(bytes);
                        }
                    }
                    Output.Visibility = Visibility.Visible;
                    Output.Text = base64Image;
                    OutputLabel.Visibility = Visibility.Visible;
                    ClearButton.Visibility = Visibility.Visible;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Output.Text = "";
                    Output.Visibility = Visibility.Hidden;
                    OutputLabel.Visibility = Visibility.Hidden;
                    ClearButton.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                MessageBox.Show("FilePath cannot be empty");
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Output.Text = "";
            Output.Visibility = Visibility.Hidden;
            OutputLabel.Visibility = Visibility.Hidden;
            FileTextBox.Text = "";
            ClearButton.Visibility = Visibility.Hidden;
        }

        private byte[]? ConvertImageToBytes(System.Drawing.Image image)
        {
            ImageConverter converter = new ImageConverter();
            return converter.ConvertTo(image, typeof(byte[])) as byte[];
        }
    }
}