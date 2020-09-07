using Autofac;
using FFImageLoading.Forms;
using FFImageLoading.Transformations;
using System;
using TrailHeadTestApp.Infrastructure.ApiModel;
using TrailHeadTestApp.Interfaces.Infrastructure.Repositories;
using TrailHeadTestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrailHeadTestApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeeListPage : ContentPage
    {
        private EmployeeListViewModel viewModel;
        private ListView ItemsListView;

        private readonly IEmployeesRepository _employeesRepository;
        public EmployeeListPage(IEmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }
        public EmployeeListPage() : this(DIService.Container.Resolve<IEmployeesRepository>())
        {
            //InitializeComponent();

            BindingContext = viewModel = new EmployeeListViewModel();

            Label pullToDownloadText = new Label { Text = "Pull to download", HorizontalOptions = LayoutOptions.Center, Margin = new Thickness(10) };
            pullToDownloadText.SetBinding(IsVisibleProperty, "ItemsIsEmpty", BindingMode.OneWay);

            ItemsListView = new ListView(ListViewCachingStrategy.RecycleElement);
            ItemsListView.SetBinding(ListView.ItemsSourceProperty, "Items");
            ItemsListView.VerticalOptions = LayoutOptions.FillAndExpand;
            ItemsListView.HasUnevenRows = true;
            ItemsListView.SetBinding(ListView.RefreshCommandProperty, "LoadEmployeesFromWebServiceCommand");
            ItemsListView.IsPullToRefreshEnabled = true;
            ItemsListView.SetBinding(ListView.IsRefreshingProperty, "IsBusy", BindingMode.OneWay);
            ItemsListView.ItemSelected += OnItemSelected;

            var personDataTemplate = new DataTemplate(() =>
            {
                var mainStack = new StackLayout() { Padding = new Thickness(10), Orientation = StackOrientation.Horizontal };

                var firstNameLabel = new Label { FontSize = 16 };
                firstNameLabel.SetBinding(Label.TextProperty, "FirstName");

                var lastNameLabel = new Label { FontSize = 13 };
                lastNameLabel.SetBinding(Label.TextProperty, "LastName");

                var circleTransformation = new CircleTransformation { BorderSize = 15, BorderHexColor = "#3199DC" };
                var avatarImage = new CachedImage()
                {
                    ErrorPlaceholder = "default_profile.png",
                    LoadingPlaceholder = "default_profile.png",
                    HeightRequest = 100,
                    WidthRequest = 100,
                    Aspect = Aspect.Fill,
                    Margin = 15,
                    HorizontalOptions = LayoutOptions.Start
                };
                avatarImage.SetBinding(CachedImage.SourceProperty, "Avatar");
                avatarImage.SetBinding(CachedImage.SourceProperty, "Avatar");
                avatarImage.Transformations.Add(circleTransformation);

                var userNameStack = new StackLayout() { Padding = new Thickness(10), VerticalOptions = LayoutOptions.Center };
                userNameStack.Children.Add(firstNameLabel);
                userNameStack.Children.Add(lastNameLabel);

                mainStack.Children.Add(avatarImage);
                mainStack.Children.Add(userNameStack);

                return new ViewCell { View = mainStack };
            });

            ItemsListView.ItemTemplate = personDataTemplate;


            Content = new StackLayout
            {
                Children = { pullToDownloadText, ItemsListView }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var list = _employeesRepository.GetEmployeeList(0);
            }
            catch (Exception e)
            {

            }
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Employee;
            if (item == null)
                return;


            //await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

    }
}