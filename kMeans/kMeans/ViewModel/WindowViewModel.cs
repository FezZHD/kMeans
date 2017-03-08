using System.Windows.Controls;
using System.Windows.Input;

namespace kMeans.ViewModel
{
    public class WindowViewModel: BaseViewModel
    {

        public WindowViewModel()
        {
            IsAnError = false;
            ExecuteTask = new Command((() =>
            {
                IsAnError = false;
                if (!ConvertCheckingWrite())
                {
                    return;
                }
            }));
        }


        private string classCount;

        public string ClassCount
        {
            get { return classCount; }
            set
            {
                classCount = value;
                OnPropertyChanged();
            }
        }


        private string pointsCount;

        public string PointsCount
        {
            get
            {
                return pointsCount;
            }
            set
            {
                pointsCount = value;
                OnPropertyChanged();
            }
        }


        private string errorString;

        public string ErrorString
        {
            get
            {
                return errorString;
            }
            set
            {
                errorString = value;
                OnPropertyChanged();
            }
        }

        private bool isAnError;

        public bool IsAnError
        {
            get
            {
                return isAnError;
            }
            set
            {
                isAnError = value;
                OnPropertyChanged();
            }
        }

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


        public ICommand ExecuteTask { get; private set; }


        private uint classCountNum;
        private uint pointsCountNum;

        private bool ConvertCheckingWrite()
        {
            if ((CheckWriteValue(ClassCount, out classCountNum, "Error input at class count field")) &&
                CheckWriteValue(PointsCount, out pointsCountNum, "Error input at points count field"))
            {
                 return true;
            }
            return false;
        }


        private bool CheckWriteValue(string fieldValue,out uint fieldNumValue, string errorMessage)
        {
            if (!uint.TryParse(fieldValue, out fieldNumValue))
            {
                ErrorString = errorMessage;
                IsAnError = true;
                return false;
            }
            return true;
        }
    }
}