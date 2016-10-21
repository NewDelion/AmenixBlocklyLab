using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TabControlSample2.chrome
{
    public class MyTabClass : TabBase
    {
        public int CanvasIndex { get; set; }
        public Canvas CanvasPanel { get; set; }
    }
}
