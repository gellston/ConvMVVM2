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

        private double _OffsetX = 0.0;
        private double _OffsetY = 0.0;
        #endregion

        #region Event
        //public event Action<Matrix> UpdateTransform;
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
            private set
            {
                if (value < 0) return;

                _ZoomStep = value;
                this.OnPropertyChanged();
            }
        }

        public double Scale
        {
            get => _Scale;
            private set
            {
                _Scale = value;
                this.OnPropertyChanged();
            }
        }

        public Matrix TransformMatrix
        {
            get => this._TransformMatrix;
            private set
            {
                this._TransformMatrix = value;
                OnPropertyChanged();
            }
        }

        public double OffsetX
        {
            get => _OffsetX;
            private set
            {
                _OffsetX = value;
                OnPropertyChanged();
            }
        }

        public double OffsetY
        {
            get => _OffsetY;
            private set
            {
                _OffsetY = value;
                OnPropertyChanged();
            }
        }
        #endregion


        #region Public Functions

        public void Reset()
        {
            this.TransformMatrix = Matrix.Identity;

            this.Scale = 1.0;

            this.OffsetX = 0.0;
            this.OffsetY = 0.0;
          
        }


        public void Pan(Point target)
        {
            Matrix newMatrix = this.TransformMatrix;
            newMatrix.TranslatePrepend(target.X, target.Y);
            this.TransformMatrix = newMatrix;

            //this.UpdateTransform?.Invoke(this.TransformMatrix);
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
            this.OffsetX = offsetX;
            this.OffsetY = offsetY;
 
        }
        #endregion
    }
}
