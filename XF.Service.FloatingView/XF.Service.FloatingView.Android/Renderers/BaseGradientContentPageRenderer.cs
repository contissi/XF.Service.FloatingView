﻿using System;
using Android.Content;
using Android.Graphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(XF.Service.FloatingView.Views.BaseGradientContentPage), typeof(XF.Service.FloatingView.Droid.Renderers.BaseGradientContentPageRenderer))]
namespace XF.Service.FloatingView.Droid.Renderers
{
    public class BaseGradientContentPageRenderer : VisualElementRenderer<ContentPage>
    {
        private Android.Graphics.Color? GradientStartColor { get; set; }

        public BaseGradientContentPageRenderer(Context context) : base(context)

        protected override void DispatchDraw(Canvas canvas)
    }
}