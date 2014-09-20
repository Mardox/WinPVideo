using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Media.PhoneExtensions;
using System.IO;
using Windows.Storage;
using System.Threading.Tasks;
using System.Windows.Shapes;
using GoogleAds;
using AppStudio.Resources;

namespace AppStudio.Views
{
    public partial class draw : PhoneApplicationPage
    {
        int listcount = 0;
        public List<BitmapImage> bit = new List<BitmapImage>();
        public int imagenumber = 0;
        bool pickeropen = false;
        bool strokeopen = false;
        int strokethickness = 0;
        public Color selectedcolor = Color.FromArgb(255, 0, 0, 0);
        public Point currentPoint, oldPoint;
        public bool isDrag = false;
        public Point oldPoints;

        public draw()
        {
            InitializeComponent();

            bit.Clear();
            LoadFolder();
            strokethickness = 5;
            painting.MouseLeftButtonDown += painting_MouseLeftButtonDown;
            painting.MouseMove += painting_MouseMove;
            painting.ManipulationDelta += painting_ManipulationDelta;
            painting.ManipulationCompleted += painting_ManipulationCompleted;
            isDrag = false;
            painting.RenderTransform = new TranslateTransform();
            oldPoints = new Point(painting.Margin.Left, painting.Margin.Top);

            AdView bannerAd = new AdView
            {
                // Replace your AD Unit ID before uploading 
                Format = AdFormats.Banner,
                AdUnitID = AppResources.AdMobBanner
            };
            bannerAd.VerticalAlignment = VerticalAlignment.Top;
            AdRequest adRequest = new AdRequest();
            // Assumes we've defined a Grid that has a name
            // directive of ContentPanel.
            Maingrid.Children.Add(bannerAd);
            bannerAd.VerticalAlignment = VerticalAlignment.Top;
            bannerAd.LoadAd(adRequest);
        }

        void painting_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {

        }

        double val = 0, NewX = 0, NewY = 0, old = 0.0;

        void painting_ManipulationDelta(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
        {
            if (isDrag)
            {
                var canvastrans = painting.TransformToVisual(this);
                if (e.PinchManipulation != null)
                {
                    //   if(e.PinchManipulation.)

                    PlaneProjection project = new PlaneProjection();
                    val += e.PinchManipulation.DeltaScale * 10;
                    project.GlobalOffsetZ = val;
                    painting.Projection = project;
                }
                else if (e.DeltaManipulation != null)
                {
                    NewX += e.DeltaManipulation.Translation.X;
                    NewY += e.DeltaManipulation.Translation.Y;
                    painting.Margin = new Thickness(NewX, NewY, 0, 0);
                }
            }
        }

        void painting_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!isDrag)
            {

                currentPoint = e.GetPosition(this.painting);
                Line line = new Line();

                line.X1 = currentPoint.X;
                line.Y1 = currentPoint.Y;
                line.X2 = oldPoint.X;
                line.Y2 = oldPoint.Y;

                line.StrokeThickness = strokethickness;
                line.Stroke = new SolidColorBrush(selectedcolor);

                this.painting.Children.Add(line);
                oldPoint = currentPoint;
            }
        }

        void painting_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!isDrag)
            {

                currentPoint = e.GetPosition(this.painting);
                oldPoint = currentPoint;

            }
        }

        public async void LoadFolder()
        {
            listcount = await Folder(@"images\" + MainPage.index.ToString());

            for (int i = 1; i < listcount; i++)
            {
                bit.Add(new BitmapImage(new Uri(("/images/" + MainPage.index.ToString() + "/" + MainPage.index.ToString() + i.ToString() + ".jpg"), UriKind.Relative)));
            }

            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = bit[imagenumber];
            painting.Background = imageBrush;

        }
        public async Task<int> Folder(string path)
        {
            StorageFolder folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var fol = await folder.GetFolderAsync(path);
            var list = await fol.GetFilesAsync();

            return Convert.ToInt32(list.Count);
        }

        private void nextimg_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (isDrag)
            {
                isDrag = false;
            }

            imagenumber++;

            if (imagenumber < listcount - 1)
            {
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bit[imagenumber];
                painting.Background = imageBrush;
            }
            else
            {
                NavigationService.GoBack();
            }
        }

        private void backimg_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (isDrag)
            {
                isDrag = false;
            }


            if (imagenumber > 0)
            {
                imagenumber--;

                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bit[imagenumber];
                painting.Background = imageBrush;
            }
        }

        private void color_Click(object sender, RoutedEventArgs e)
        {
            if (pickeropen == false)
            {
                picker.Visibility = Visibility.Visible;
                pickeropen = true;
            }
            else
            {
                pickeropen = false;
                picker.Visibility = Visibility.Collapsed;
            }
        }

        private void picker_ColorChanged(object sender, System.Windows.Media.Color color)
        {
            selectedcolor = color;
        }




        private void pen_Click(object sender, RoutedEventArgs e)
        {
            if (isDrag)
            {
                isDrag = false;
            }


            selectedcolor = Colors.Black;
            if (strokeopen == false)
            {
                strokeopen = true;
                penstroke.Visibility = Visibility.Visible;
            }
            else
            {
                strokeopen = false;
                penstroke.Visibility = Visibility.Collapsed;
            }
        }

        private void penstroke_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            if (penstroke != null)
            {
                strokethickness = (int)penstroke.Value;
            }
        }

        private void erase_Click(object sender, RoutedEventArgs e)
        {

            if (isDrag)
            {
                isDrag = false;
            }

            selectedcolor = Colors.White;
            if (strokeopen == false)
            {
                strokeopen = true;
                penstroke.Visibility = Visibility.Visible;
            }
            else
            {
                strokeopen = false;
                penstroke.Visibility = Visibility.Collapsed;
            }
        }

        private void circle_Click(object sender, RoutedEventArgs e)
        {
            if (isDrag)
            {
                isDrag = false;
            }

            this.painting.Children.Clear();
        }

        private void share_Click(object sender, RoutedEventArgs e)
        {
            capture(true);
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            capture(false);
        }

        public void capture(bool a)
        {
            try
            {


                var bmp = new WriteableBitmap(this.painting, null);
                var width = (int)bmp.PixelWidth;
                var height = (int)bmp.PixelHeight;
                using (var ms = new MemoryStream(width * height * 4))
                {
                    bmp.SaveJpeg(ms, width, height, 0, 100);
                    ms.Seek(0, SeekOrigin.Begin);
                    MediaLibrary lib = new MediaLibrary();
                    Picture picture = lib.SavePicture(string.Format("drawing.jpg"), ms);
                    if (a)
                    {
                        var task = new ShareMediaTask();
                        task.FilePath = picture.GetPath();


                        task.Show();
                    }


                }

                if (!a)
                {
                    MessageBox.Show("Drawing saved successfully.");
                }
            }
            catch (Exception)
            {
                if (a)
                {
                    MessageBox.Show("Share action failed, Try again", "Alert", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Save action failed, Try again", "Alert", MessageBoxButton.OK);

                }
            }
            //string currentFileName = fileName;ga
        }

        private void zoom_Click(object sender, RoutedEventArgs e)
        {
            painting.Margin = new Thickness(oldPoints.X, oldPoints.Y, 0, 0);
            var canvastrans = painting.TransformToVisual(this);

            //   if(e.PinchManipulation.)

            PlaneProjection project = new PlaneProjection();
            val = 0;
            project.GlobalOffsetZ = val;
            painting.Projection = project;

        }

        private void resize_Click(object sender, RoutedEventArgs e)
        {
            if (!isDrag)
            {
                isDrag = true;
            }
            else
            {
                isDrag = false;
            }
        }

    }
}