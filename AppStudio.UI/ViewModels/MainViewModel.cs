using System.Threading.Tasks;
using System.Windows.Input;

using AppStudio.Data;
using AppStudio.Services;
using AppStudio.Resources;
using System;
using Microsoft.Phone.Tasks;
using System.ComponentModel;

namespace AppStudio
{
    public class MainViewModels : BindableBase
    {
       private Item1ViewModel _item1Model;
       private Item2ViewModel _item2Model;
       private SearchViewModel _searchModel;
       private Item3ViewModel _item3Model;
       private Item4ViewModel _item4Model;
       //private Item5ViewModel _item5Model;

        private ViewModelBase _selectedItem = null;
        private PrivacyViewModel _privacyModel;

        public MainViewModels()
        {
            _selectedItem = Item1Model;
            _privacyModel = new PrivacyViewModel();
        }
 
        public Item1ViewModel Item1Model
        {
            get { return _item1Model ?? (_item1Model = new Item1ViewModel()); }
        }
 
        public Item2ViewModel Item2Model
        {
            get { return _item2Model ?? (_item2Model = new Item2ViewModel()); }
        }

        public Item3ViewModel Item3Model
        {
            get { return _item3Model ?? (_item3Model = new Item3ViewModel()); }
        }

        public Item4ViewModel Item4Model
        {
            get { return _item4Model ?? (_item4Model = new Item4ViewModel()); }
        }

        //public Item5ViewModel Item5Model
        //{
        //    get { return _item5Model ?? (_item5Model = new Item5ViewModel()); }
        //}

        public SearchViewModel SearchModel
        {
            get { return _searchModel ?? (_searchModel = new SearchViewModel()); }
        }

        public void SetViewType(ViewTypes viewType)
        {
            Item1Model.ViewType = viewType;
            Item2Model.ViewType = viewType;
            Item3Model.ViewType = viewType;
            Item4Model.ViewType = viewType;
            //Item5Model.ViewType = viewType;
            SearchModel.ViewType = viewType;
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
                if (SelectedItem == null || SelectedItem == Item1Model)
                {
                    return true;
                }
                return SelectedItem != null ? SelectedItem.IsAppBarVisible : false;
            }
        }

        public bool IsLockScreenVisible
        {
            get { return SelectedItem == null || SelectedItem == Item1Model; }
        }

        public bool IsAboutVisible
        {
            get { return SelectedItem == null || SelectedItem == Item1Model; }
        }

        public bool IsPrivacyVisible
        {
            get { return SelectedItem == null || SelectedItem == Item1Model; }
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
                Item1Model.LoadItems(isNetworkAvailable),
                Item2Model.LoadItems(isNetworkAvailable),
                Item3Model.LoadItems(isNetworkAvailable),
                Item4Model.LoadItems(isNetworkAvailable),
                //Item5Model.LoadItems(isNetworkAvailable),
            };
            await Task.WhenAll(loadTasks);
        }

        public async Task LoadSearchData(bool isNetworkAvailable)
        {
            var loadTasks = new Task[]
            { 
                SearchModel.LoadItems(isNetworkAvailable),
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

        public ICommand SearchCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateToPage("SearchPage");
                });
            }
        }

        public ICommand SettingsCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateToPage("SettingsPage");
                });
            }
        }

        public ICommand MoreCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    MarketplaceSearchTask marketplaceSearchTask = new MarketplaceSearchTask();

                    marketplaceSearchTask.ContentType = MarketplaceContentType.Applications;
                    marketplaceSearchTask.SearchTerms = AppResources.MarketPlaceName;

                    marketplaceSearchTask.Show(); ;
                });
            }
        }

        public class ItemViewModel : INotifyPropertyChanged
        {
            private string title;

            public string Title
            {
                get
                {
                    return title;
                }
                set
                {
                    if (value != title)
                    {
                        title = value;
                        NotifyPropertyChanged("Title");
                    }
                }
            }

            private string image;

            public string LargeImage
            {
                get
                {
                    return image;
                }
                set
                {
                    if (value != image)
                    {
                        image = value;
                        NotifyPropertyChanged("LargeImage");
                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            private void NotifyPropertyChanged(String propertyName)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (null != handler)
                {
                    handler(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }

    }
}
