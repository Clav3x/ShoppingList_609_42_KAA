using ShoppingList_Core.ViewModels;

namespace ShoppingList
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel(App.Database);
        }
    }
}


