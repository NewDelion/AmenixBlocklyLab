using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BlockChain2
{
    public delegate void BlockPickupEventHandler(UIElement el);
    public delegate void BlockMoveEventHandler(UIElement el);
    public delegate void BlockDropEventHandler(UIElement el);

    public interface IBlockly
    {
        BlockInfo info { get; set; }
        event BlockPickupEventHandler OnBlockPickup;
        event BlockMoveEventHandler OnBlockMove;
        event BlockDropEventHandler OnBlockDrop;
        Point GetLinkForwardPoint();
        Point GetLinkBackwardPoint();
        Point GetPointFromLinkForward(Point link_point);
        Point GetPointFromLinkBackward(Point link_point);
        Canvas GetCanvas();
        bool SetChain(UIElement el);
        void ReleaseChain(UIElement el);

    }
}
