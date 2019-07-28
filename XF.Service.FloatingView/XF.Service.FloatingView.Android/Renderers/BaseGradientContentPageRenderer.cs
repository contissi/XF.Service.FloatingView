using System;
using Android.Content;
using Android.Graphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(XF.Service.FloatingView.Views.BaseGradientContentPage), typeof(XF.Service.FloatingView.Droid.Renderers.BaseGradientContentPageRenderer))]
namespace XF.Service.FloatingView.Droid.Renderers
{
    public class BaseGradientContentPageRenderer : VisualElementRenderer<ContentPage>
    {
        private Android.Graphics.Color? GradientStartColor { get; set; }        private Android.Graphics.Color? GradientEndColor { get; set; }

        public BaseGradientContentPageRenderer(Context context) : base(context)        {        }

        protected override void DispatchDraw(Canvas canvas)        {            base.DispatchDraw(canvas);            if (!GradientStartColor.HasValue || !GradientEndColor.HasValue)                return;            var gradient = new LinearGradient(0, 0, 0, Height,                GradientStartColor.Value,                GradientEndColor.Value,                Shader.TileMode.Clamp);            var paint = new Paint() { Dither = true };            paint.SetShader(gradient);            canvas.DrawPaint(paint);            base.DispatchDraw(canvas);        }        protected override void OnElementChanged(ElementChangedEventArgs<ContentPage> e)        {            base.OnElementChanged(e);            if (e.OldElement != null || Element == null)            {                return;            }            try            {                var baseGradientContentPage = e.NewElement as XF.Service.FloatingView.Views.BaseGradientContentPage;                GradientStartColor = baseGradientContentPage.GradientStartColor.ToAndroid();                GradientEndColor = baseGradientContentPage.GradientEndColor.ToAndroid();            }            catch            {                throw;            }        }
    }
}
