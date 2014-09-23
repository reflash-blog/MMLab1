using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MMLab1.View.Plot
{

    public class DataWindowViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void SetImageData(byte[] data)
        {
            var source = new BitmapImage();
            source.BeginInit();
            source.StreamSource = new MemoryStream(data);
            source.EndInit();

            // use public setter
            ImageSource = source;
        }

        ImageSource imageSource;
        public ImageSource ImageSource
        {
            get { return imageSource; }
            set
            {
                imageSource = value;
                OnPropertyChanged("ImageSource");
            }
        }

        protected void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

    }

}
