﻿using ConvMVVM2.Core.MVVM;
using ConvMVVM2.WPF.Behaviors.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ConvMVVM2.WPF.Behaviors.Behaviors
{
    public class CompositionRenderBehavior : Behavior<FrameworkElement>
    {

        #region Protected Functions


        protected override void OnAttached()
        {

            CompositionTarget.Rendering -= CompositionTarget_Rendering;
            CompositionTarget.Rendering += CompositionTarget_Rendering;

        }



        protected override void OnDetaching()
        {
            CompositionTarget.Rendering -= CompositionTarget_Rendering;
        }
        #endregion

        #region Dependency Property
        public static DependencyProperty RendererProperty = DependencyProperty.Register("Renderer", typeof(IRenderer), typeof(CompositionRenderBehavior));
        public IRenderer Renderer
        {

            get => (IRenderer)GetValue(RendererProperty);
            set => SetValue(RendererProperty, value);
        }
        #endregion

        #region Event Handler
        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if (!(AssociatedObject is FrameworkElement)) return;

            try
            {
                if (Renderer != null)
                {
                    Renderer.OnRendering();
                    return;
                }


                if (!(AssociatedObject.DataContext is IRenderer render)) return;
                render.OnRendering();


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
        #endregion
    }
}
