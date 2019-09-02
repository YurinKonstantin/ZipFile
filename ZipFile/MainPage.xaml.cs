using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.IO;
using System.IO.Compression;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace ZipFile
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                var picker = new Windows.Storage.Pickers.FileOpenPicker();
                picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
                picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
                picker.FileTypeFilter.Add(".zip");
                picker.FileTypeFilter.Add(".jpeg");
                picker.FileTypeFilter.Add(".png");


                StorageFile file = await picker.PickSingleFileAsync();
                if (file != null)
                {
                    StorageFolder picturesFolder = ApplicationData.Current.LocalFolder;
                    List<ZipArchiveEntry> zipArchiveEntries = await ZipArchiveManager.GetFileZip(file);

                  TreeViewNode rootNode = new TreeViewNode() { Content = "Flavors" };
                    rootNode.IsExpanded = true;
                    rootNode.Children.Add(new TreeViewNode() { Content = "Vanilla" });
                    rootNode.Children.Add(new TreeViewNode() { Content = "Strawberry" });
                    rootNode.Children.Add(new TreeViewNode() { Content = "Chocolate" });

                   



                    string s = String.Empty;
                   foreach(var d in zipArchiveEntries)
                    {
                        s += d.FullName +"\t"+d.Name+"\t"+d+ "\n";
                    }

                    sampleTreeView.RootNodes.Add(rootNode);
                    MessageDialog messageDialog1 = new MessageDialog(s);
                    await messageDialog1.ShowAsync();

                }
                else
                {

                }
            }
            catch(Exception ex)
            {
                MessageDialog messageDialog = new MessageDialog(ex.Message);
                await messageDialog.ShowAsync();
            }
        }
    }
}
