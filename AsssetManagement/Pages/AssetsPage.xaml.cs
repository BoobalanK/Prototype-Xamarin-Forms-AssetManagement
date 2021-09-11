using AsssetManagement.ViewModels;
using FreshMvvm;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AsssetManagement.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssetsPage : ContentPage
    {
        public AssetsPage()
        {
            InitializeComponent();
            BindingContext = FreshIOC.Container.Resolve<AssetsViewModel>();
        }
    }
}