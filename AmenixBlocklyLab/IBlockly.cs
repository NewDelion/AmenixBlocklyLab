using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AmenixBlocklyLab
{
    public delegate void BlockPickupEventHandler(UIElement el);
    public delegate void BlockMoveEventHandler(UIElement el);
    public delegate void BlockDropEventHandler(UIElement el);

    public interface IBlockly
    {
        event BlockPickupEventHandler OnBlockPickup;
        event BlockMoveEventHandler OnBlockMove;
        event BlockDropEventHandler OnBlockDrop;
        Point GetLinkForwardPoint();
        Point GetLinkBackwardPoint();
        Point GetPointFromLinkForward(Point link_point);
        Point GetPointFromLinkBackward(Point link_point);
        UIElement ChainChildElement { get; set; }
    }
}
