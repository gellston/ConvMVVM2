using ConvMVVM2.Core.Attributes;
using ConvMVVM2.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ConvMVVM2.WPF.ViewModels
{
    public class ZoomAndPanViewModel : ViewModelBase
    {
        #region Private Property
        private Point _PanStart { get; set; }
        private double _Scale { get; set; } = 1.0;
        private Matrix _TransformMatrix = Matrix.Identity;

        private double _MaxZoom = 5.0;
        private double _MinZoom = 0.2;
        private double _ZoomStep = 1.1;

        #endregion


        #region Public Property
        public double MaxZoom
        {
            get => _MaxZoom;
            set
            {
                if (value < 0) return;

                if (value < 1.0)
                    value = 1.0;

                _MaxZoom = value;
                this.OnPropertyChanged();
            }
        }

        public double MinZoom
        {
            get => _MinZoom;
            set
            {
                if (value < 0) return;

                if (value > 1.0)
                    value = 1.0;

                _MinZoom = value;
                this.OnPropertyChanged();
            }
        }

        public double ZoomStep
        {
            get => _ZoomStep;
            protected set
            {
                if (value < 0) return;

                _ZoomStep = value;
                this.OnPropertyChanged();
            }
        }

        public double Scale
        {
            get => _Scale;
            protected set
            {
                _Scale = value;
                this.OnPropertyChanged();
            }
        }

        public Matrix TransformMatrix
        {
            get => this._TransformMatrix;
            protected set
            {
                this._TransformMatrix = value;
                OnPropertyChanged();
            }
        }

        #endregion


        #region Public Functions

        public void Reset()
        {
            this.TransformMatrix = Matrix.Identity;

            this.Scale = 1.0;

        }


        public void Pan(double offsetX, double offsetY)
        {
            Matrix newMatrix = this.TransformMatrix;
            newMatrix.TranslatePrepend(offsetX, offsetY);


            this.TransformMatrix = newMatrix;
        }

        public System.Windows.Point ApplyTransform(double x, double y)
        {
            Matrix newMatrix = this.TransformMatrix;
            System.Windows.Point point = new System.Windows.Point(x, y);
            return newMatrix.Transform(point);
        }

        public System.Windows.Point ApplyInvTransform(double x, double y)
        {
            Matrix newMatrix = this.TransformMatrix;
            newMatrix.Invert();
            if (!newMatrix.HasInverse)
            {
                throw new InvalidOperationException("Inverse Matrix doesn't exist");
            }
            System.Windows.Point point = new System.Windows.Point(x, y);
            return newMatrix.Transform(point);
        }

        public void Zoom(double centerX, double centerY, bool isUp)
        {

            double nextScale = isUp ? this.Scale * this._ZoomStep : this.Scale / this._ZoomStep;

            if (nextScale >= MinZoom && nextScale <= MaxZoom)
            {
                double scaleFactor  = isUp ? this._ZoomStep : 1.0 / this._ZoomStep;

                Matrix newMatrix = this.TransformMatrix;
                newMatrix.ScaleAtPrepend(scaleFactor, scaleFactor, centerX, centerY);
                //this.UpdateTransform?.Invoke(this.TransformMatrix);

                this.Scale = nextScale;
                this.OnPropertyChanged("Scale");

                this.TransformMatrix = newMatrix;
            }
        }

        public void FitToViewPort(double viewportWidth, double viewportHeight, double imageWidth, double imageHeight)
        {
            if (imageWidth <= 0 || imageHeight <= 0 || viewportWidth <= 0 || viewportHeight <= 0)
                return;


            double scaleX = viewportWidth / imageWidth;
            double scaleY = viewportHeight / imageHeight;
            double scale = Math.Min(scaleX, scaleY);


            double offsetX = (viewportWidth - imageWidth * scale) / 2;
            double offsetY = (viewportHeight - imageHeight * scale) / 2;


            Matrix matrix = Matrix.Identity;
            matrix.Scale(scale, scale);
            matrix.Translate(offsetX, offsetY);


            this.TransformMatrix = matrix;
            this.Scale = scale;

            this.OnPropertyChanged("TransformMatrix");

        }
        #endregion
    }
}
