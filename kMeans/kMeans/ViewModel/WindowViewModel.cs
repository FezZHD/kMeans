using System.Diagnostics;

namespace kMeans.ViewModel
{
    public class WindowViewModel: BaseViewModel
    {
        private double height;
        public double CanvasHeight
        {
            get { return height; }
            set
            {
                Debug.WriteLine(value);
            }
        }
    }
}