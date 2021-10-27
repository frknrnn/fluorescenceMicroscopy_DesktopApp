using System;
using System.Drawing;
using System.Windows.Forms;
namespace zoomer
{
    public class ModifiedPictureBox : PictureBox
    {

        private Point? _clickedPoint;
        public ProTransformation _transformation, transformation;
        Point pos1, pos2;
        Size rectSize, max;
        RectangleF rect = new RectangleF();
        public Rectangle imRect;
        double maxScale;
        public int zoom_boundary = 0;


        int tabButtonSelect = 1, newWidth, newHeight, ptabPictureboxW, ptabPictureboxH;
        double ratioX, ratioY, ratio;
        Bitmap newImage;
        Graphics graphics;

        public ProTransformation Transformation
        {
            set
            {
                _transformation = FixTranslation(value);
                Invalidate();
            }
            get
            {
                return _transformation;
            }
        }

        public ModifiedPictureBox()
        {
            _transformation = new ProTransformation(new Point(0, 0), 2f);
            MouseDown += OnMouseDown;
            MouseMove += OnMouseMove;
            MouseUp += OnMouseUp;
            MouseWheel += OnMouseWheel;
            Resize += OnResize;

        }

        private ProTransformation FixTranslation(ProTransformation value)
        {
            maxScale = Math.Max((double)Image.Width / ClientRectangle.Width, (double)Image.Height / ClientRectangle.Height);
            if (value.Scale > maxScale)
                value = value.SetScale(maxScale);
            if (value.Scale < 0.3)
                value = value.SetScale(0.3);
            rectSize = value.ConvertToIm(ClientRectangle.Size);
            max = new Size(Image.Width - rectSize.Width, Image.Height - rectSize.Height);

            value = value.SetTranslate((new Point(Math.Min(value.Translation.X, max.Width), Math.Min(value.Translation.Y, max.Height))));

            if (value.Translation.X < 0 || value.Translation.Y < 0)
            {
                value = value.SetTranslate(new Point(Math.Max(value.Translation.X, 0), Math.Max(value.Translation.Y, 0)));
            }
            return value;
        }

        private void OnResize(object sender, EventArgs eventArgs)
        {

            if (Image == null)
                return;
            Transformation = Transformation;
        }

        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            transformation = _transformation;
            pos1 = transformation.ConvertToIm(e.Location);
            if (e.Delta > 0)
            {
                if (zoom_boundary < 10)
                {
                    transformation = (transformation.SetScale(Transformation.Scale / 1.25));
                    zoom_boundary += 1;
                }

            }
            else
            {
                transformation = (transformation.SetScale(Transformation.Scale * 1.25));
                zoom_boundary -= 1;
                if (zoom_boundary < 0)
                {
                    zoom_boundary = 0;
                }
            }
            pos2 = transformation.ConvertToIm(e.Location);
            transformation = transformation.AddTranslate(pos1 - (Size)pos2);
            Transformation = transformation;
        }

        private void OnMouseUp(object sender, MouseEventArgs mouseEventArgs)
        {
            _clickedPoint = null;
            this.Cursor = Cursors.Arrow;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_clickedPoint == null)
                return;
            var p = _transformation.ConvertToIm((Size)e.Location);
            Transformation = _transformation.SetTranslate(_clickedPoint.Value - p);
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            Focus();
            this.Cursor = Cursors.SizeAll;
            _clickedPoint = _transformation.ConvertToIm(e.Location);

        }

        protected override void OnPaint(PaintEventArgs e)
        {

            imRect = Transformation.ConvertToIm(ClientRectangle);

            e.Graphics.DrawImage(Image, ClientRectangle, imRect, GraphicsUnit.Pixel);



            ////////////////// Zoom Ayarları Deneme////////////////////////



        }



        public void DecideInitialTransformation()
        {
            Transformation = new ProTransformation(Point.Empty, int.MaxValue);
        }
    }
    public class ProTransformation
    {
        public Point Translation { get { return _translation; } }
        public double Scale
        {
            get { return _scale; }
            set { _scale = value; }

        }
        public Point _translation;
        private double _scale;

        public ProTransformation(Point translation, double scale)
        {
            _translation = translation;
            _scale = scale;
        }

        public Point ConvertToIm(Point p)
        {
            return new Point((int)(p.X * _scale + _translation.X), (int)(p.Y * _scale + _translation.Y));
        }

        public Size ConvertToIm(Size p)
        {
            return new Size((int)(p.Width * _scale), (int)(p.Height * _scale));
        }

        public Rectangle ConvertToIm(Rectangle r)
        {
            return new Rectangle(ConvertToIm(r.Location), ConvertToIm(r.Size));
        }

        public Point ConvertToPb(Point p)
        {
            return new Point((int)((p.X - _translation.X) / _scale), (int)((p.Y - _translation.Y) / _scale));
        }

        public Size ConvertToPb(Size p)
        {
            return new Size((int)(p.Width / _scale), (int)(p.Height / _scale));
        }

        public Rectangle ConvertToPb(Rectangle r)
        {
            return new Rectangle(ConvertToPb(r.Location), ConvertToPb(r.Size));
        }

        public ProTransformation SetTranslate(Point p)
        {
            return new ProTransformation(p, _scale);
        }

        public ProTransformation AddTranslate(Point p)
        {
            return SetTranslate(new Point(p.X + _translation.X, p.Y + _translation.Y));
        }

        public ProTransformation SetScale(double scale)
        {
            return new ProTransformation(_translation, scale);
        }
    }
}
