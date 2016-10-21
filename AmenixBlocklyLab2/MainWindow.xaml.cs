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

namespace AmenixBlocklyLab2
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            block.BlockRegisterEvents registerEvents = new block.BlockRegisterEvents() { BlockId = "aaaa" };
            registerEvents.SetValue(Canvas.LeftProperty, 0.0);
            registerEvents.SetValue(Canvas.TopProperty, 50.0 * 0);
            board.Children.Add(registerEvents);

            block.BlockRegisterEvents registerEvents2 = new block.BlockRegisterEvents() { BlockId = "bbbb" };
            registerEvents2.SetValue(Canvas.LeftProperty, 0.0);
            registerEvents2.SetValue(Canvas.TopProperty, 50.0 * 1);
            board.Children.Add(registerEvents2);

            block.BlockStringTextBox stringTextBox = new block.BlockStringTextBox();
            stringTextBox.SetValue(Canvas.LeftProperty, 0.0);
            stringTextBox.SetValue(Canvas.TopProperty, 50.0 * 2);
            board.Children.Add(stringTextBox);

            block.BlockStringTextBox stringTextBox2 = new block.BlockStringTextBox();
            stringTextBox2.SetValue(Canvas.LeftProperty, 0.0);
            stringTextBox2.SetValue(Canvas.TopProperty, 50.0 * 3);
            board.Children.Add(stringTextBox2);

            block.BlockCalcNumber calcNumber = new block.BlockCalcNumber();
            calcNumber.SetValue(Canvas.LeftProperty, 0.0);
            calcNumber.SetValue(Canvas.TopProperty, 50.0 * 4);
            board.Children.Add(calcNumber);

            block.BlockCalcNumber calcNumber2 = new block.BlockCalcNumber();
            calcNumber2.SetValue(Canvas.LeftProperty, 0.0);
            calcNumber2.SetValue(Canvas.TopProperty, 50.0 * 5);
            board.Children.Add(calcNumber2);

            block.BlockSampleCalcArg sampleCalcArg = new block.BlockSampleCalcArg();
            sampleCalcArg.SetValue(Canvas.LeftProperty, 0.0);
            sampleCalcArg.SetValue(Canvas.TopProperty, 50.0 * 6);
            board.Children.Add(sampleCalcArg);
        }
    }
}
