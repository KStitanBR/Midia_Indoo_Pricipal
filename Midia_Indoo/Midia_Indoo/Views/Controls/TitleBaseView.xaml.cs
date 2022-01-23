using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Midia_Indoo.Views.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]




    public partial class TitleBaseView : ContentView
    {
        public static readonly BindableProperty TituloProperty = BindableProperty.Create(propertyName: "Titulo", returnType: typeof(string), declaringType: typeof(TitleBaseView), defaultValue: "", defaultBindingMode: BindingMode.TwoWay, propertyChanged: lblTituloPropertyChanged);
        public static readonly BindableProperty InvisibleRefreshProperty = 
            BindableProperty.Create(propertyName: "InvisibleRefresh", returnType: typeof(bool),
                declaringType: typeof(TitleBaseView), defaultValue: false,
                defaultBindingMode: BindingMode.TwoWay, propertyChanged: InvisibleRefreshPropertyChanged);

        public static readonly BindableProperty BackCommandProperty =
               BindableProperty.Create(nameof(BackCommand), typeof(ICommand), typeof(TitleBaseView));
        public static readonly BindableProperty UpDateCommandProperty =
               BindableProperty.Create(nameof(UpDateCommand), typeof(ICommand), typeof(TitleBaseView));

        public ICommand BackCommand
        {
            get { return (ICommand)GetValue(BackCommandProperty); }
            set { SetValue(BackCommandProperty, value); }
        }
        public ICommand UpDateCommand
        {
            get { return (ICommand)GetValue(UpDateCommandProperty); }
            set { SetValue(UpDateCommandProperty, value); }
        }
        public string Titulo
        {
            get { return (string)GetValue(TituloProperty); }
            set { SetValue(TituloProperty, value); }
        }
        public bool InvisibleRefresh
        {
            get { return (bool)GetValue(InvisibleRefreshProperty); }
            set { SetValue(InvisibleRefreshProperty, value); }
        }
        //public ICommand BackCommand
        //{
        //    get { return (Command)GetValue(TituloProperty); }
        //    set { SetValue(TituloProperty, value); }
        //}
        public TitleBaseView()
        {
            InitializeComponent();
        }
        //private static void BackCommandPropertyChanged(BindableObject bindable, Object value, Object newValue)
        //{
        //    var _myControl = (TitleBaseView)bindable;
        //    _myControl.btnBack.Command = (Command)newValue;
        //}
        private static void lblTituloPropertyChanged(BindableObject bindable, Object value, Object newValue)
        {
            var _myControl = (TitleBaseView)bindable;
            _myControl.lblTitulo.Text = (string)newValue;
        } 
        private static void InvisibleRefreshPropertyChanged(BindableObject bindable, Object value, Object newValue)
        {
            var _myControl = (TitleBaseView)bindable;
            _myControl.BtnRefresh.IsVisible = (bool)newValue;
        }
        public static void Execute(ICommand command)
        {
            if (command == null) return;
            if (command.CanExecute(null))
            {
                command.Execute(null);
            }
        }
        public Command OnTap => new Command(() => Execute(BackCommand));

    }
}