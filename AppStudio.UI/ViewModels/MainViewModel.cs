using System.Threading.Tasks;
using System.Windows.Input;

using AppStudio.Data;
using AppStudio.Services;

namespace AppStudio
{
    public class MainViewModels : BindableBase
    {
       private TrailersViewModel _trailersModel;
       private MusicViewModel _musicModel;

        private ViewModelBase _selectedItem = null;
        private PrivacyViewModel _privacyModel;

        public MainViewModels()
        {
            _selectedItem = TrailersModel;
            _privacyModel = new PrivacyViewModel();
        }
 
        public TrailersViewModel TrailersModel
        {
            get { return _trailersModel ?? (_trailersModel = new TrailersViewModel()); }
        }
 
        public MusicViewModel MusicModel
        {
            get { return _musicModel ?? (_musicModel = new MusicViewModel()); }
        }

        public void SetViewType(ViewTypes viewType)
        {
            TrailersModel.ViewType = viewType;
            MusicModel.ViewType = viewType;
        }

        public ViewModelBase SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                UpdateAppBar();
            }
        }

        public bool IsAppBarVisible
        {
            get
            {
                if (SelectedItem == null || SelectedItem == TrailersModel)
                {
                    return true;
                }
                return SelectedItem != null ? SelectedItem.IsAppBarVisible : false;
            }
        }

        public bool IsLockScreenVisible
        {
            get { return SelectedItem == null || SelectedItem == TrailersModel; }
        }

        public bool IsAboutVisible
        {
            get { return SelectedItem == null || SelectedItem == TrailersModel; }
        }

        public bool IsPrivacyVisible
        {
            get { return SelectedItem == null || SelectedItem == TrailersModel; }
        }


        public void UpdateAppBar()
        {
            OnPropertyChanged("IsAppBarVisible");
            OnPropertyChanged("IsLockScreenVisible");
            OnPropertyChanged("IsAboutVisible");
        }

        /// <summary>
        /// Load ViewModel items asynchronous
        /// </summary>
        public async Task LoadData(bool isNetworkAvailable)
        {
            var loadTasks = new Task[]
            { 
                TrailersModel.LoadItems(isNetworkAvailable),
                MusicModel.LoadItems(isNetworkAvailable),
            };
            await Task.WhenAll(loadTasks);
        }

        //
        //  ViewModel command implementation
        //
        public ICommand AboutCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateToPage("AboutThisAppPage");
                });
            }
        }

        public ICommand PrivacyCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateTo(_privacyModel.Url);
                });
            }
        }

        public ICommand LockScreenCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    LockScreenServices.SetLockScreen("/Assets/LockScreenImage.png");
                });
            }
        }
    }
}
