using System.Windows.Controls;
using System.Windows.Input;
using kMeans.Model;

namespace kMeans.ViewModel
{
    public class WindowViewModel: BaseViewModel
    {

        public WindowViewModel()
        {
            IsEnableToPress = true;
            ExecuteTask = new Command((() =>
            {
                if (!ConvertCheckingWrite())
                { 
                    return;
                }
                IsEnableToPress = false;
                var model = new PointsModel(pointsCountNum, classCountNum);
                model.Execute();
                IsEnableToPress = true;
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


        private int classCountNum;
        private int pointsCountNum;

        private bool isWorking;

        public bool IsWorking
        {
            get
            {
                return isWorking;
                
            }
            set
            {
                isWorking = value;
                OnPropertyChanged();
            }
        }

        private bool isEnableToPress;

        public bool IsEnableToPress
        {
            get
            {
                return isEnableToPress; 
            }
            set
            {
                isEnableToPress = value;
                OnPropertyChanged();
            }
        }

        private bool ConvertCheckingWrite()
        {
            if ((CheckWriteValue(ClassCount, out classCountNum, "Error input at class count field")) &&
                CheckWriteValue(PointsCount, out pointsCountNum, "Error input at points count field"))
            {
                 return true;
            }
            return false;
        }


        private bool CheckWriteValue(string fieldValue,out int fieldNumValue, string errorMessage)
        {
            if (!int.TryParse(fieldValue, out fieldNumValue))
            {
                ErrorString = errorMessage;
                IsAnError = true;
                return false;
            }
            return true;
        }
    }
}