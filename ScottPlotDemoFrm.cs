using ScottPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScottPlotDemo
{
    public partial class ScottPlotDemoFrm : Form
    {
        internal class TestModel : BindingBase
        {
            private double timeStamp;

            public string Name { get; set; }
            public string Value { get; set; }
            /// <summary>
            /// ms
            /// </summary>
            public double TimeStamp { get => timeStamp; set => SetProperty(ref timeStamp, value); }
        }
        private List<ScottPlot.Plottables.DataLogger> dataLoggers;
        private List<ScottPlot.Plottables.DataStreamerXY> dataStreamerXYs;
        private List<ScottPlot.Plottables.DataStreamer> dataStreamers;
        List<ScottPlot.Plot> plots;
        System.Windows.Forms.Timer UpdatePlotTimer = new System.Windows.Forms.Timer() { Interval = 50, Enabled = true };
        IList<TestModel> models;
        private int lineType = 2;

        public ScottPlotDemoFrm()
        {
            InitializeComponent();
            r = new Random();
            dataLoggers = new List<ScottPlot.Plottables.DataLogger>();
            dataStreamers = new List<ScottPlot.Plottables.DataStreamer>();
            dataStreamerXYs = new List<ScottPlot.Plottables.DataStreamerXY>();
            plots = new List<Plot>();

            this.rbDataLogger.CheckedChanged += RbDataLogger_CheckedChanged;
            this.rbDataStreamer.CheckedChanged += RbDataLogger_CheckedChanged;
            this.rbDataStreamerXY.CheckedChanged += RbDataLogger_CheckedChanged;

            models = GenerateModels(5);

            this.Load += ScottPlotDemoFrm_Load;
        }

        private void RbDataLogger_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == rbDataLogger && rbDataLogger.Checked)
            {
                Stop();
                lineType = 0;
                GeneratePlots(models);
            }
            else if (sender == rbDataStreamer && rbDataStreamer.Checked)
            {
                Stop();
                lineType = 1;
                GeneratePlots(models);
            }
            else if (sender == rbDataStreamerXY && rbDataStreamerXY.Checked)
            {
                Stop();
                lineType = 2;
                GeneratePlots(models);
            }
        }

        private void ScottPlotDemoFrm_Load(object sender, EventArgs e)
        {
            GeneratePlots(models);

            UpdatePlotTimer.Tick += (s, ee) =>
            {
                if (dataLoggers.Any(x => x.HasNewData)) formsPlot1.Refresh();
                if (dataStreamers.Any(x => x.HasNewData)) formsPlot1.Refresh();
                if (dataStreamerXYs.Any()) formsPlot1.Refresh();
            };
        }
        void GeneratePlots(IEnumerable<TestModel> models)
        {
            ClearDataLine();
            Plot plot = formsPlot1.Plot;
            formsPlot1.Multiplot.Reset(plot);
            var firstModel = models.First();

            CreateDataLine(plot, firstModel, lineType);
            foreach (var item in models.Skip(1))
            {
                plot = new Plot();
                //CreateDataLogger(plot, item);
                CreateDataLine(plot, item, lineType);
                formsPlot1.Multiplot.AddPlot(plot);
                plots.Add(plot);
            }
            formsPlot1.Multiplot.ShareX(plots);
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
                AddPoint(signal, lineType);
                //ScottPlot.Plottables.DataLogger datalogger = dataLoggers.FirstOrDefault(x => x.LegendText == signal.Name);
                //datalogger.Add(double.Parse(signal.Value)); 
                //ScottPlot.Plottables.DataStreamer dataStreamer = dataStreamers.FirstOrDefault(x => x.LegendText == signal.Name);
                //dataStreamer.Add(double.Parse(signal.Value));
            }
        }

        void CreateDataLine(Plot plot, TestModel model, int type)
        {
            switch (type)
            {
                case 0:
                    CreateDataLogger(plot, model);
                    break;
                case 1:
                    CreateDataStreamer(plot, model);
                    break;
                case 2:
                    CreateDataStreamerXY(plot, model);
                    break;
            }
        }

        void ClearDataLine()
        {
            formsPlot1.Plot.Clear();
            //dataLoggers.FirstOrDefault().d
            dataLoggers.Clear();
            dataStreamers.Clear();
            dataStreamerXYs.Clear();
            plots.Clear();
        }

        void AddPoint(TestModel model, int type)
        {
            switch (type)
            {
                case 0:
                    //CreateDataLogger(plot, model);
                    ScottPlot.Plottables.DataLogger datalogger = dataLoggers.FirstOrDefault(x => x.LegendText == model.Name);
                    datalogger.Add(double.Parse(model.Value));
                    break;
                case 1:
                    //CreateDataStreamer(plot, model);
                    ScottPlot.Plottables.DataStreamer dataStreamer = dataStreamers.FirstOrDefault(x => x.LegendText == model.Name);
                    dataStreamer.Add(double.Parse(model.Value));
                    break;
                case 2:
                    //CreateDataStreamerXY(plot, model);
                    ScottPlot.Plottables.DataStreamerXY dataStreamerxy = dataStreamerXYs.FirstOrDefault(x => x.LegendText == model.Name);
                    dataStreamerxy.Add(model.TimeStamp, double.Parse(model.Value));
                    break;
            }
        }

        void CreateDataLogger(Plot plot, TestModel model)
        {
            ScottPlot.Plottables.DataLogger dataLogger = plot.Add.DataLogger();
            plot.YLabel(model.Name);
            //plot1.Axes.Left
            dataLogger.ViewSlide();
            dataLogger.LegendText = model.Name;
            dataLoggers.Add(dataLogger);
        }
        //private List<ScottPlot.Plottables.DataStreamer> dataStreamers;
        void CreateDataStreamer(Plot plot, TestModel model)
        {
            ScottPlot.Plottables.DataStreamer dataStreamer = plot.Add.DataStreamer(1000);
            plot.YLabel(model.Name);
            dataStreamer.ViewScrollLeft();
            dataStreamer.LegendText = model.Name;
            dataStreamers.Add(dataStreamer);
        }

        void CreateDataStreamerXY(Plot plot, TestModel model)
        {
            ScottPlot.Plottables.DataStreamerXY dataStreamer = plot.Add.DataStreamerXY(1000);
            plot.YLabel(model.Name);
            //dataStreamer.ViewScrollLeft();
            dataStreamer.LegendText = model.Name;
            dataStreamerXYs.Add(dataStreamer);
        }

        Thread thread;
        private bool dataChanging;
        private Random r;
        private DateTime startTime;
        private void Button1_Click(object sender, EventArgs e)
        {
            //start change value
            foreach (var item in dataLoggers)
            {
                item.Clear();
            }
            foreach (var item in dataStreamers)
            {
                item.Clear(double.NaN);
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
            Stop();
        }

        void Stop()
        {
            dataChanging = false;
            if (thread != null)
                thread.Join();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            //add a new Model and plot
            int count = models.Count;
            var newModel = new TestModel()
            {
                Name = "Model" + count,
            };
            newModel.PropertyChanged += Model_PropertyChanged;
            models.Add(newModel);
            //add plot
            Plot p = new Plot();
            //CreateDataLogger(p, newModel);
            CreateDataLine(p, newModel, lineType);
            formsPlot1.Multiplot.AddPlot(p);
            plots.Add(p);
        }
    }

    public abstract class BindingBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public SynchronizationContext UIContext;
        protected virtual bool SetProperty<T>(ref T storage, T value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

        protected void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            if (UIContext != null)
            {
                UIContext.Post(_ =>
                {
                    OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
                }, null);

            }
            else
            {
                OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            try
            {
                //PropertyChanged?.BeginInvoke(this,args,null,null);
                this.PropertyChanged?.Invoke(this, args);
            }
            catch (System.Exception)
            {
            }
        }

    }
}
