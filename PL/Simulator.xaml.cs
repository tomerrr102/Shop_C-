using System;
using System.ComponentModel;
using System.Windows;
using Simulation;
using BO;
using System.Threading;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace PL
{
    /// <summary>
    /// Interaction logic for Simultor.xaml
    /// </summary>
    public partial class SimultorWindow : Window
    {

        public ProccessDetails MyProccessDetails
        {
            get { return (ProccessDetails)GetValue(MyProccessDetailsProperty); }
            set { SetValue(MyProccessDetailsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProccessDetails.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyProccessDetailsProperty =
            DependencyProperty.Register("MyProccessDetails", typeof(ProccessDetails), typeof(SimultorWindow));

        public string MyClock
        {
            get { return (string)GetValue(MyClockProperty); }
            set { SetValue(MyClockProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyClock.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyClockProperty =
            DependencyProperty.Register("MyClock", typeof(string), typeof(SimultorWindow));

        public double MyProgressBarValue
        {
            get { return (double)GetValue(MyProgressBarValueProperty); }
            set { SetValue(MyProgressBarValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyProgressBarValueProperty =
            DependencyProperty.Register("MyProgressBarValue", typeof(double), typeof(SimultorWindow));

        private int duration;
        private int precent;

        private Stopwatch stopwatch;

        private BackgroundWorker worker;

        private enum Progress { Clock, Update, Complete }

        public SimultorWindow()
        {
            InitializeComponent();

            stopwatch = new Stopwatch();
            stopwatch.Start();
            MyClock = stopwatch.Elapsed.ToString().Substring(0, 8);

            MyProgressBarValue = 0;

            worker = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };

            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }
        private void worker_DoWork(object? sender, DoWorkEventArgs e)
        {
            Simulator.RegisterToUpdtes(updateProgres);
            Simulator.RegisterToStop(stopWorker);
            Simulator.RegisterToComplete(UpdateComplete);

            Simulator.StartSimulation();

            while (!worker.CancellationPending)
            {
                Thread.Sleep(1000);
                worker.ReportProgress((int)Progress.Clock);
            }

        }

        private void worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            switch ((Progress)e.ProgressPercentage)
            {
                case Progress.Clock:
                    MyClock = stopwatch.Elapsed.ToString().Substring(0, 8);
                    MyProgressBarValue = (precent++ * 100 / duration);
                    break;
                case Progress.Update:
                    MyProccessDetails = (e.UserState as ProccessDetails)!;
                    break;
                case Progress.Complete:
                    break;
                default:
                    break;
            }
        }

        private void worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            Simulator.DeRegisterToUpdtes(updateProgres);
            Simulator.DeRegisterToStop(stopWorker);
            Simulator.DeRegisterToComplete(UpdateComplete);

            MessageBox.Show("Simulation Completed");
            Close();
        }

        private void stopWorker()
        {
            worker.CancelAsync();
        }

        private void updateProgres(Order order, Status? status, DateTime tretTime, int treatDuration)
        {
            precent = 0;
            duration = treatDuration;
            ProccessDetails proc = new()
            {

                id = order.OrderUniqCode,
                CurrentStatus = order.OrderStatus,
                NextStatus = status,
                CurrentTreatTime = tretTime,
                EstimatedTreatTime = tretTime + TimeSpan.FromSeconds(treatDuration)
            };

            worker.ReportProgress((int)Progress.Update, proc);
        }

        private void UpdateComplete(Status? status) => worker.ReportProgress((int)Progress.Complete, status);

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            Simulator.StopSimulation();
        }
    }

    public class ProccessDetails : DependencyObject
    {
        public int id { get; set; }
        public Status? CurrentStatus { get; set; }
        public Status? NextStatus { get; set; }
        public DateTime CurrentTreatTime { get; set; }
        public DateTime EstimatedTreatTime { get; set; }

    }
}
