using System;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LoadingButton.FormsPlugin.Abstractions
{
    /// <summary>
    /// LoadingButton Interface
    /// </summary>
    public class LoadingButtonControl : Image
    {

        public LoadingButtonControl()
        {

            var tgr = new TapGestureRecognizer();
            tgr.Tapped += Tgr_Tapped;
            this.GestureRecognizers.Add(tgr);
        }

        public static readonly BindableProperty ClickCommandProperty = BindableProperty.Create(nameof(ClickCommand), typeof(Command), typeof(LoadingButtonControl), null, propertyChanged: ClickCommandChanged);
        public event EventHandler<bool> Clicked;
        private bool _isIndicatorRunning = false;
        private string _icon;
        private string _iconDone;

        public static void ClickCommandChanged(BindableObject bindableObject, object oldValue, object newValue) => (bindableObject as LoadingButtonControl).ClickCommand = (Command)newValue;

        public ICommand ClickCommand { get; set; }

        public static readonly BindableProperty IsIndicatorRunningProperty = BindableProperty.Create(nameof(IsIndicatorRunning), typeof(bool), typeof(LoadingButtonControl), false, propertyChanged: IsIndicatorRunningChanged);
        public static void IsIndicatorRunningChanged(BindableObject bindableObject, object oldValue, object newValue) => (bindableObject as LoadingButtonControl).IsIndicatorRunning = (bool)newValue;

        public bool IsIndicatorRunning { get => _isIndicatorRunning; set { SetIndicatorRunning(value); _isIndicatorRunning = value; } }

        public static readonly BindableProperty IconPropert = BindableProperty.Create(nameof(Icon), typeof(string), typeof(LoadingButtonControl), "floaticon_save.png", propertyChanged: IconPropertyChanged);
        public static readonly BindableProperty IconDoneProperty = BindableProperty.Create(nameof(IconDone), typeof(string), typeof(LoadingButtonControl), "done.png", propertyChanged:IconDonePropertyChanged);
        public static void IconPropertyChanged(BindableObject bindableObject, object oldValue, object newValue) => (bindableObject as LoadingButtonControl).Icon = (string)newValue ?? "";
        public static void IconDonePropertyChanged(BindableObject bindableObject, object oldValue, object newValue) => (bindableObject as LoadingButtonControl).IconDone = (string)newValue ?? "";
        public string Icon { get => _icon; set => SetIcon(value); }
        public string IconDone { get => _iconDone; set => SetIconDone(value); }




        private void SetIcon(string value)
        {
            if (!IsIndicatorRunning)
                this.Source = value;
            _icon = value;
        }
        private void SetIconDone(string value)
        {
            if (IsIndicatorRunning)
                this.Source = value;

            _iconDone = value;
        }
      
        private void Tgr_Tapped(object sender, EventArgs e)
        {
            ClickCommand?.Execute(this);
            Clicked?.Invoke(this,IsIndicatorRunning);
        }
        
        void SetIndicatorRunning(bool value)
        {
            if (value && _isIndicatorRunning != value)
            {
                Opacity = 0.4;
                InputTransparent = true;
                Task.Run(async () =>
                {
                    Point:
                    await this.RotateTo(this.Rotation + 360, 500, Easing.SinIn);


                    if (IsIndicatorRunning)
                        goto Point;


                    this.FadeTo(1);
                    Device.BeginInvokeOnMainThread(() => this.Source = "done.png");
                    await Task.Delay(2000);
                    Device.BeginInvokeOnMainThread(() => this.Source = "floaticon_save.png");
                    InputTransparent = false;
                });
            }
        }


    }
}
