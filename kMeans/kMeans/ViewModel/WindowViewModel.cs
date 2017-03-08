using System.Windows.Controls;

namespace kMeans.ViewModel
{
    public class WindowViewModel: BaseViewModel
    {


        private Image image;
        public Image ImageSource
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                OnPropertyChanged();
            }
        }
    }
}