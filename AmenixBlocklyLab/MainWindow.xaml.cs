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

namespace AmenixBlocklyLab
{
    /**
     * 
     * 
     * 
     * 
     * 
     **/

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            block.Block b = new block.Block(new UIElement[] { new Label() { Content = "イベントを登録する" } });
            b.OnBlockPickup += OnBlockPickup;
            b.OnBlockMove += OnBlockMove;
            b.OnBlockDrop += OnBlockDrop;
            b.SetValue(Canvas.LeftProperty, 0.0);
            b.SetValue(Canvas.TopProperty, 50.0 * 0);

            block.Block b2 = new block.Block(new UIElement[] { new TextBox() { Text = "TestLabel2" } });
            b2.OnBlockPickup += OnBlockPickup;
            b2.OnBlockMove += OnBlockMove;
            b2.OnBlockDrop += OnBlockDrop;
            b2.SetValue(Canvas.LeftProperty, 0.0);
            b2.SetValue(Canvas.TopProperty, 50.0 * 1);

            block.Block b3 = new block.Block(new UIElement[] { new Label() { Content = "TestLabel3" } });
            b3.OnBlockPickup += OnBlockPickup;
            b3.OnBlockMove += OnBlockMove;
            b3.OnBlockDrop += OnBlockDrop;
            b3.SetValue(Canvas.LeftProperty, 0.0);
            b3.SetValue(Canvas.TopProperty, 50.0 * 2);
            
            board.Children.Add(b);
            board.Children.Add(b2);
            board.Children.Add(b3);
        }

        private void OnBlockPickup(UIElement el)
        {
            var canvas = ((Control)el).Parent as Canvas;
            foreach(UIElement element in canvas.Children)
            {
                if (element.Equals(el))
                    continue;
                if (((IBlockly)element).ChainChildElement == null)
                    continue;
                if (((IBlockly)element).ChainChildElement.Equals(el))
                    ((IBlockly)element).ChainChildElement = null;
            }
        }

        private void OnBlockMove(UIElement el)
        {
            
        }

        private void OnBlockDrop(UIElement el)
        {
            var canvas = ((Control)el).Parent as Canvas;
            Point pt = ((IBlockly)el).GetLinkForwardPoint();

            foreach(UIElement element in canvas.Children)
            {
                if (element.Equals(el))
                    continue;
                Point element_pt = ((IBlockly)element).GetLinkBackwardPoint();
                if(element_pt.X - 20 < pt.X && pt.X < element_pt.X + 20 && element_pt.Y -20 < pt.Y && pt.Y<element_pt.Y + 20 && ((IBlockly)element).ChainChildElement == null)
                {
                    Point connected_pt = ((IBlockly)el).GetPointFromLinkForward(element_pt);
                    Canvas.SetLeft(el, connected_pt.X);
                    Canvas.SetTop(el, connected_pt.Y);
                    ((IBlockly)element).ChainChildElement = el;
                    break;
                }
            }
        }
    }

    

    //public static class ControlDragExtenstions
    //{
    //    /// <summary>
    //    /// コントロールのドラッグを有効にする
    //    /// 親コントロールが Canvas であること
    //    /// </summary>
    //    /// <param name="sender"></param>
    //    /// <param name="e"></param>
    //    public static void DragMove(this Control t, object sender, MouseButtonEventArgs e)
    //    {
    //        var el = sender as Control;
    //        var canvas = el.Parent as Canvas;
    //        var dragOffset = e.GetPosition(el);
    //        el.CaptureMouse();

    //        MouseEventHandler mouseMove = null;
    //        MouseButtonEventHandler mouseUp = null;

    //        mouseMove = new MouseEventHandler((_, __) => {
    //            Point pt = Mouse.GetPosition(canvas);
    //            Canvas.SetLeft(el, pt.X - dragOffset.X);
    //            Canvas.SetTop(el, pt.Y - dragOffset.Y);
    //        });
    //        mouseUp = new MouseButtonEventHandler((_, __) => {
    //            el.ReleaseMouseCapture();
    //            el.MouseMove -= mouseMove;
    //            el.MouseUp -= mouseUp;
    //        });
    //        el.MouseMove += mouseMove;
    //        el.MouseLeftButtonUp += mouseUp;
    //        SetForefront(canvas, el);
    //    }

    //    private static void SetForefront(Canvas canvas, UIElement element)
    //    {
    //        int target_z = Canvas.GetZIndex(element);
    //        if (target_z + 1 == canvas.Children.Count)
    //            return;
    //        Canvas.SetZIndex(element, canvas.Children.Count);
    //        foreach (UIElement ele in canvas.Children)
    //            if (target_z < Canvas.GetZIndex(ele))
    //                Canvas.SetZIndex(ele, Canvas.GetZIndex(ele) - 1);
    //    }
    //}
}
