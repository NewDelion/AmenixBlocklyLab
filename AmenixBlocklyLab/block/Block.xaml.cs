using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AmenixBlocklyLab.block
{
    public partial class Block : UserControl, IBlockly
    {
        public event BlockPickupEventHandler OnBlockPickup;
        public event BlockMoveEventHandler OnBlockMove;
        public event BlockDropEventHandler OnBlockDrop;

        public Block(UIElement[] contents)
        {
            InitializeComponent();

            ContentPanel.SizeChanged += (s, ev) =>
            {
                polygon.Points[5] = new Point(Math.Max(ContentPanel.ActualWidth + 6, 100) - 5, polygon.Points[5].Y);
                polygon.Points[6] = new Point(Math.Max(ContentPanel.ActualWidth + 6, 100), polygon.Points[6].Y);
                polygon.Points[7] = new Point(Math.Max(ContentPanel.ActualWidth + 6, 100), polygon.Points[7].Y);
            };

            MouseLeftButtonDown += (sender, ev) =>
            {
                this.DragMove(sender, ev);
            };

            foreach (var content in contents)
                ContentPanel.Children.Add(content);
        }

        public Point GetLinkForwardPoint()
        {
            return new Point((double)GetValue(Canvas.LeftProperty) + 25, (double)GetValue(Canvas.TopProperty) + 2.5);
        }
        public Point GetLinkBackwardPoint()
        {
            return new Point((double)GetValue(Canvas.LeftProperty) + 25, (double)GetValue(Canvas.TopProperty) + 37.5);
        }
        public Point GetPointFromLinkForward(Point link_point)
        {
            return new Point(link_point.X - 25, link_point.Y - 2.5);
        }
        public Point GetPointFromLinkBackward(Point link_point)
        {
            return new Point(link_point.X - 25, link_point.Y - 37.5);
        }
        
        public UIElement ChainChildElement { get; set; }
        
        private void DragMove(object sender, MouseButtonEventArgs e)
        {
            var el = sender as Control;
            var canvas = el.Parent as Canvas;
            var dragOffset = e.GetPosition(el);
            el.CaptureMouse();

            MouseEventHandler mouseMove = null;
            MouseButtonEventHandler mouseUp = null;

            mouseMove = new MouseEventHandler((_, __) => {
                Point pt = Mouse.GetPosition(canvas);
                Canvas.SetLeft(el, pt.X - dragOffset.X);
                Canvas.SetTop(el, pt.Y - dragOffset.Y);
                ChainMove(el);
                if (OnBlockMove != null)
                    OnBlockMove(el);
            });
            mouseUp = new MouseButtonEventHandler((_, __) => {
                el.ReleaseMouseCapture();
                el.MouseMove -= mouseMove;
                el.MouseUp -= mouseUp;
                if (OnBlockDrop != null)
                    OnBlockDrop(el);
                ChainMove(el);
            });
            if (OnBlockPickup != null)
                OnBlockPickup(el);
            el.MouseMove += mouseMove;
            el.MouseLeftButtonUp += mouseUp;
            SetForefront(canvas, el);
        }
        
        private void ChainMove(UIElement block)
        {
            if (((IBlockly)block).ChainChildElement == null)
                return;
            var child = ((IBlockly)block).ChainChildElement;
            var pt = ((IBlockly)child).GetPointFromLinkForward(((IBlockly)block).GetLinkBackwardPoint());
            Canvas.SetLeft(child, pt.X);
            Canvas.SetTop(child, pt.Y);
            ChainMove(child);
        }

        private void SetForefront(Canvas canvas, UIElement element)
        {
            int target_z = Canvas.GetZIndex(element);
            if (target_z + 1 == canvas.Children.Count)
                return;
            Canvas.SetZIndex(element, canvas.Children.Count);
            foreach (UIElement ele in canvas.Children)
                if (target_z < Canvas.GetZIndex(ele))
                    Canvas.SetZIndex(ele, Canvas.GetZIndex(ele) - 1);
        }
    }
}
