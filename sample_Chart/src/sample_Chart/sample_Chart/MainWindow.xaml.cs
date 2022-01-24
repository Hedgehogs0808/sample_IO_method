using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ScottPlot;

namespace sample_Chart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            double[] values1 = DataGen.RandomWalk(100_000);
            double[] values2 = DataGen.RandomWalk(10_000);
            double[] values3 = DataGen.RandomWalk(1_000);
            WpfPlot1.Plot.AddSignal(values1, sampleRate: 1000);
            WpfPlot1.Plot.AddSignal(values2, sampleRate: 100);
            WpfPlot1.Plot.AddSignal(values3, sampleRate: 10);

            //描画されているSignalPlotをリスト化
            var Signals = GetPlotableList<ScottPlot.Plottable.SignalPlot>(WpfPlot1);

            //現在のY軸をリスト化
            var YAxes = WpfPlot1.Plot.GetSettings().Axes.Where(x => x.IsVertical == true).ToList();
            //足りないY軸を追加
            for (int YAxisIndex = YAxes.Count; YAxisIndex < Signals.Count; YAxisIndex++) //軸インデックスがない場合は追加する
            {
                YAxes.Add(WpfPlot1.Plot.AddAxis(ScottPlot.Renderable.Edge.Left, YAxisIndex));
            }

            //各軸に描画
            for (int No = 0; No < Signals.Count; No++)
            {
                Signals[No].YAxisIndex = No;
            }

            //オートスケール
            WpfPlot1.Plot.AxisAuto();

            //X軸を表示制限
            var AxisLimits = WpfPlot1.Plot.GetAxisLimits();
            WpfPlot1.Plot.SetAxisLimits(AxisLimits.XMin, AxisLimits.XMax);

            //各Y軸の設定（色指定、ラベル、表示制限）
            for (int No = 0; No < YAxes.Count; No++)
            {
                YAxes[No].Ticks(true);
                YAxes[No].Color(Signals[No].Color);
                YAxes[No].Label($"Signal{No}");
                AxisLimits = WpfPlot1.Plot.GetAxisLimits(yAxisIndex: YAxes[No].AxisIndex);
                YAxes[No].Dims.SetAxis(AxisLimits.YMin, AxisLimits.YMax);
            }

            WpfPlot1.Plot.SaveFig(@"C:\Users\hiro1\Desktop\reps\sample_Chart\data\sample_Chart_bf.bmp");
            WpfPlot1.Refresh();
            WpfPlot1.Plot.SaveFig(@"C:\Users\hiro1\Desktop\reps\sample_Chart\data\sample_Chart_af.bmp");
            WpfPlot1.Plot.SaveFig(@"sample_Chart_af.png");
        }

        /// <summary>
        /// WpfPlotの中にあるすべてのTのリストを返す
        /// </summary>
        /// <typeparam name="T">ScottPlot.Plottable Type</typeparam>
        /// <param name="fp"> WpfPlot</param>
        /// <returns></returns>
        private List<T> GetPlotableList<T>(WpfPlot fp)
        {
            List<T> Plotables = new List<T>();
            var AllPlottables = fp.Plot.GetPlottables();
            foreach (var Plottable in AllPlottables)
            {
                switch (Plottable)
                {
                    case T Plotable:
                        Plotables.Add(Plotable);
                        break;
                }
            }
            return Plotables;
        }
    }
}
