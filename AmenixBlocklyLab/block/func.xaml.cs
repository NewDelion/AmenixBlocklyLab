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
    public partial class func : UserControl
    {
        public func(UIElement[] contents)
        {
            InitializeComponent();

            ContentPanel.SizeChanged += (s, ev) =>
            {
                polygon.Points[5] = new Point(Math.Max(ContentPanel.ActualWidth + 6, 100) - 5, polygon.Points[5].Y);
                polygon.Points[6] = new Point(Math.Max(ContentPanel.ActualWidth + 6, 100), polygon.Points[6].Y);
                polygon.Points[7] = new Point(Math.Max(ContentPanel.ActualWidth + 6, 100), polygon.Points[7].Y);
            };

            foreach (var content in contents)
                ContentPanel.Children.Add(content);
        }
    }
}
