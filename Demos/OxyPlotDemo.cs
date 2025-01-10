using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using PlotDemo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace PlotDemo.Demos
{
    public partial class OxyPlotDemoFrm : Form
    {
        protected const double AxisPading = 0.95;
        public bool PositionTierEnable { get; set; }
        IList<TestModel> models;
        System.Windows.Forms.Timer UpdatePlotTimer = new System.Windows.Forms.Timer() { Interval = 50, Enabled = true };

        public PlotModel PlotModel { get => plotView1.Model; }
        public OxyPlotDemoFrm()
        {
            InitializeComponent();
            r = new Random();
            models = GenerateModels(5);
            this.Load += OxyPlotDemoFrm_Load;
        }
        private void OxyPlotDemoFrm_Load(object sender, EventArgs e)
        {
            Button b = new Button()
            {
                Text = "start",
                Name = "btnStart",

            };
            b.Size = new System.Drawing.Size(100, 20);
            b.Location = new System.Drawing.Point(5, 5);
            b.Click += this.Button1_Click;
            this.Controls.Add(b);


            plotView1.Model = new PlotModel()
            {
                Title = "实时数据",
                IsLegendVisible = false,
                //Background = OxyColors.Black,
                //TextColor = OxyColors.White,
            };

            //x轴
            plotView1.Model.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom,
            });

            //var controller = new PlotController();
            //controller.UnbindMouseDown(OxyMouseButton.Right);
            //controller.BindMouseDown(OxyMouseButton.Left, PlotCommands.PanAt);
            //plotView1.Controller = controller;
            ////plotView1.Model = PlotModel;
            PlotModel.Series.Clear();
            AddAxesToPlot();
            for (int i = 0; i < models.Count; i++)
            //for (int i = FormItem.Signals.Count - 1; i >= 0; i--)
            {
                var signal = models[i];

                //增加曲线
                LineSeries series = InitlineSeriesStyle();
                series.Title = signal.ToString();
                series.YAxisKey = signal.ToString();

                PlotModel.Series.Add(series);
                //AddcheckBox(signal, i);
            }

            PlotModel.InvalidatePlot(true);

            UpdatePlotTimer.Tick += (s, ee) =>
            {
                plotView1.InvalidatePlot(true);
            };
        }

        IList<TestModel> GenerateModels(int count)
        {
            List<TestModel> models = new List<TestModel>();

            for (int i = 0; i < count; i++)
            {
                TestModel model = new TestModel()
                {
                    Name = "Model" + i,
                };
                model.PropertyChanged += Model_PropertyChanged;
                models.Add(model);
            }

            return models;
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TestModel.TimeStamp))
            {
                var signal = sender as TestModel;
                AddPoint(signal);
            }
        }
        int CurvePointMaxCount = 1000;
        void AddPoint(TestModel model)
        {
            var lineSer = plotView1.Model.Series.FirstOrDefault(x => x.Title == model.Name) as LineSeries;

            //PlotModel.Axes[0].Maximum = model.TimeStamp + 1;

            lineSer.Points.Add(new DataPoint(model.TimeStamp, double.Parse(model.Value)));
            //LogHelper.WriteToOutput(this.Name, $"{item.SignalName} 添加点【{item.StrValue}】,");
            if (lineSer.Points.Count > CurvePointMaxCount)
            {
                lineSer.Points.RemoveAt(0);
            }
        }

        /// <summary>
        /// 根据所有信号来添加Y轴
        /// </summary>
        protected virtual void AddAxesToPlot()
        {
            if (models != null)
            {
                int j = models.Count - 1;
                for (int i = 0; i < models.Count; i++)
                //for (int i = FormItem.Signals.Count - 1; i >= 0; i--)
                {
                    var signal = models[i];
                    AddAxesToPlot(signal, j, models.Count);
                    j--;
                }
            }
            PlotModel.InvalidatePlot(true);
        }

        void AddAxesToPlot(TestModel signal, int index, int totalAxisCount)
        {
            signal.Value = "0";
            var existAxes = GetAxisByKey(signal.ToString());
            if (existAxes != null)
                plotView1.Model.Axes.Remove(existAxes);

            var axis = InitLinearAxisStyle();
            axis.Title = signal.ToString();
            axis.Key = signal.ToString();
            //axis.Maximum = signal.Maximum;
            //axis.Minimum = signal.Minimum;

            if (PositionTierEnable)
            {
                axis.PositionTier = index;
                axis.Angle = 60;
            }
            else
            {
                //int tempIndex = totalAxisCount - index;
                int tempIndex = index;
                axis.StartPosition = tempIndex / (double)totalAxisCount;
                axis.EndPosition = (tempIndex + AxisPading) / totalAxisCount;
                if (tempIndex + 1 == totalAxisCount)
                    axis.EndPosition = 1;
            }

            plotView1.Model.Axes.Add(axis);

        }

        /// <summary>
        /// 初始化Y轴 <para><see cref="LinearAxis"/>.Position;</para><see cref="LinearAxis"/>.AxislineStyle
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        protected LinearAxis InitLinearAxisStyle()
        {
            var axis = new LinearAxis
            {
                Position = AxisPosition.Left,
                AxislineStyle = LineStyle.Solid,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
            };
            ChangeLinearAxisColor(axis);
            return axis;
        }

        /// <summary>
        /// change color
        /// <para>TextColor;TicklineColor;TitleColor;MajorGridlineColor;MinorGridlineColor;AxislineColor
        /// </para>
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="color"></param>
        protected void ChangeLinearAxisColor(LinearAxis axis)
        {
            //axis.TextColor = color;
            //axis.TicklineColor = color;
            //axis.TitleColor = color;
            //axis.MajorGridlineColor = OxyColor.FromAColor(40, color);
            //axis.MinorGridlineColor = OxyColor.FromAColor(20, color);
            //axis.AxislineColor = color;
        }

        /// <summary>
        /// 初始化曲线
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        protected LineSeries InitlineSeriesStyle()
        {
            var series = new LineSeries()
            {

                StrokeThickness = 1,
                //MarkerSize = 3,
                //MarkerStroke = OxyColors.DarkGreen,
                MarkerType = MarkerType.None,
                InterpolationAlgorithm = null,//InterpolationAlgorithms.CanonicalSpline，
                Decimator = Decimator.Decimate
            };

            return series;
        }

        /// <summary>
        /// 修改曲线颜色
        /// </summary>
        /// <param name="lineSeries"></param>
        /// <param name="color"></param>
        protected void ChangeSeriesColor(LineSeries lineSeries, OxyColor color)
        {
            lineSeries.Color = color;
        }

        private Axis GetAxisByKey(string key)
        {
            //PlotModel
            return plotView1.Model.Axes.FirstOrDefault(x => x.Title == key || x.Key == key);
        }

        Thread thread;
        private bool dataChanging;
        private Random r;
        private DateTime startTime;
        private void Button1_Click(object sender, EventArgs e)
        {
            //start
            foreach (LineSeries item in plotView1.Model.Series)
            {
                item.Points.Clear();
            }

            dataChanging = true;
            startTime = DateTime.Now;
            thread = new Thread(DataChangeLoop);
            thread.IsBackground = true;
            thread.Start();
        }

        private void DataChangeLoop()
        {
            do
            {
                foreach (var model in models)
                {
                    model.Value = (r.NextDouble() * 100).ToString("f2");
                    model.TimeStamp = (DateTime.Now - startTime).TotalMilliseconds;
                }

                Thread.Sleep(10);
            } while (dataChanging);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //stop
            Stop();
        }

        void Stop()
        {
            dataChanging = false;
            if (thread != null)
                thread.Join();
        }
    }
}
