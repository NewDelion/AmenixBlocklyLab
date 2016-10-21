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

namespace AmenixBlocklyLab2.block
{
    public partial class BlockSampleCalcArg : UserControl, IBlockly
    {
        public string BlockId { get; set; }

        public event BlockPickupEventHandler OnBlockPickup;
        public event BlockMoveEventHandler OnBlockMove;
        public event BlockDropEventHandler OnBlockDrop;

        public BLOCK_TYPE BlockType { get { return BLOCK_TYPE.BLOCK_FUNC; } }

        public BlockSampleCalcArg()
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

            ContentPanel.Children.Add(new Label() { Content = "数値をブロードキャストする(笑)" });
            ContentPanel.Children.Add(new BlockArgCalc() { OwnerBlock = this });
        }

        public double GetHeight()
        {
            return 40.0;
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

        public IBlockly OwnerBlock { get; set; }

        public Canvas GetCanvas()
        {
            Func<IBlockly, Canvas> saiki = null;
            saiki = (c) =>
            {
                if (c.OwnerBlock == null)
                    return (c as Control).Parent as Canvas;
                return saiki(c.OwnerBlock);
            };
            return saiki(this);

            //return this.Parent is Canvas ? this.Parent as Canvas : ((IBlockly)((this.Parent as Control).Parent as Control).Parent).GetCanvas();
        }

        public bool IsChain(UIElement el)
        {
            if (ChainPanel.Children.Count == 1)
                return (ChainPanel.Children[0] as IBlockly).IsChain(el);
            return false;
        }

        public bool SetChain(UIElement el)
        {
            if (el.Equals(this))
                return false;
            //引数へのチェイン
            if ((ContentPanel.Children[1] as IBlockly).SetChain(el))
                return true;
            //後ろへのチェイン
            if (BlockType == (el as IBlockly).BlockType && ChainPanel.Children.Count == 0)
            {
                Point pt = (el as IBlockly).GetLinkForwardPoint();
                Point element_pt = (this as IBlockly).GetLinkBackwardPoint();
                if (element_pt.X - 20 < pt.X && pt.X < element_pt.X + 20 && element_pt.Y - 20 < pt.Y && pt.Y < element_pt.Y + 20)
                {
                    GetCanvas().Children.Remove(el);
                    ChainPanel.Children.Add(el);
                    (el as IBlockly).OwnerBlock = this;
                    return true;
                }
            }
            if (ChainPanel.Children.Count == 1)
                return (ChainPanel.Children[0] as IBlockly).SetChain(el);

            return false;
        }
        public void ReleaseChain(UIElement el)
        {
            if (((Control)el).Parent.Equals(this))
                return;
            ChainPanel.Children.Remove(el);
            (el as IBlockly).OwnerBlock = null;
            GetCanvas().Children.Add(el);
        }

        private void DragMove(object sender, MouseButtonEventArgs e)
        {
            var el = sender as Control;
            var canvas = GetCanvas();
            var dragOffset = e.GetPosition(el);
            if ((el as IBlockly).OwnerBlock != null)
                (el as IBlockly).OwnerBlock.ReleaseChain(el);
            el.CaptureMouse();

            MouseEventHandler mouseMove = null;
            MouseButtonEventHandler mouseUp = null;

            mouseMove = new MouseEventHandler((_, __) => {
                Point pt = Mouse.GetPosition(canvas);
                Canvas.SetLeft(el, pt.X - dragOffset.X);
                Canvas.SetTop(el, pt.Y - dragOffset.Y);
                if (OnBlockMove != null)
                    OnBlockMove(el);
            });
            mouseUp = new MouseButtonEventHandler((_, __) => {
                el.ReleaseMouseCapture();
                el.MouseMove -= mouseMove;
                el.MouseUp -= mouseUp;
                for (int i = 0; i < canvas.Children.Count; i++)
                    ((IBlockly)canvas.Children[i]).SetChain(el);
                if (OnBlockDrop != null)
                    OnBlockDrop(el);
            });
            if (OnBlockPickup != null)
                OnBlockPickup(el);
            el.MouseMove += mouseMove;
            el.MouseLeftButtonUp += mouseUp;
            SetForefront(el);
            e.Handled = true;//後ろに存在するChainPanel(元親ブロック)でこのイベントがコールされないようにする
        }

        private void SetForefront(UIElement element)
        {
            int target_z = Canvas.GetZIndex(element);
            if (target_z + 1 == GetCanvas().Children.Count)
                return;
            Canvas.SetZIndex(element, GetCanvas().Children.Count);
            foreach (UIElement ele in GetCanvas().Children)
                if (target_z < Canvas.GetZIndex(ele))
                    Canvas.SetZIndex(ele, Canvas.GetZIndex(ele) - 1);
        }
    }
}
