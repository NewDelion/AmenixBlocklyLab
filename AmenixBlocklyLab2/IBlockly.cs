using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AmenixBlocklyLab2
{
    public delegate void BlockPickupEventHandler(UIElement el);
    public delegate void BlockMoveEventHandler(UIElement el);
    public delegate void BlockDropEventHandler(UIElement el);

    public enum BLOCK_TYPE
    {
        BLOCK_FUNC,
        BLOCK_ARG_1,
        BLOCK_ARG_2
    }

    public interface IBlockly
    {
        string BlockId { get; set; }
        BLOCK_TYPE BlockType { get; }
        event BlockPickupEventHandler OnBlockPickup;
        event BlockMoveEventHandler OnBlockMove;
        event BlockDropEventHandler OnBlockDrop;
        double GetHeight();
        Point GetLinkForwardPoint();
        Point GetLinkBackwardPoint();
        Point GetPointFromLinkForward(Point link_point);
        Point GetPointFromLinkBackward(Point link_point);
        IBlockly OwnerBlock { get; set; }
        Canvas GetCanvas();
        bool IsChain(UIElement el);
        bool SetChain(UIElement el);
        void ReleaseChain(UIElement el);

    }
}
