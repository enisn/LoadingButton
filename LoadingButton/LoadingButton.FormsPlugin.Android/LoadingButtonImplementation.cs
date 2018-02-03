using LoadingButton.FormsPlugin.Abstractions;
using System;
using Xamarin.Forms;
using LoadingButton.FormsPlugin.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LoadingButton.FormsPlugin.Abstractions.LoadingButtonControl), typeof(LoadingButtonRenderer))]
namespace LoadingButton.FormsPlugin.Android
{
    /// <summary>
    /// LoadingButton Renderer
    /// </summary>
    public class LoadingButtonRenderer //: TRender (replace with renderer type
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init()
        {

        }
    }
}