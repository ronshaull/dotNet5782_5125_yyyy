using BO;
using PL.Product;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Shapes;
using System.Threading;
using System.Collections;

namespace PL
{
    /// <summary>
    /// Interaction logic for SimulatorWindow.xaml
    /// </summary>
    public partial class SimulatorWindow : Window
    {
        #region data binding
        BO.Order CurrentHandle
        {
            get { return (BO.Order)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }
        public static readonly DependencyProperty ProgressProperty=
            DependencyProperty.Register("CurrentHandle",typeof(BO.Order),typeof(SimulatorWindow));
        //next order state
        BO.Order NextHandle
        {
            get { return (BO.Order)GetValue(NextProperty); }
            set { SetValue(NextProperty, value); }
        }
        public static readonly DependencyProperty NextProperty =
            DependencyProperty.Register("NextHandle", typeof(BO.Order), typeof(SimulatorWindow));
        //current status label
        public string CurrentStatus
        {
            get { return (string)GetValue(CurrStatusProperty); }
            set { SetValue(CurrStatusProperty, value); }
        }
        public static readonly DependencyProperty CurrStatusProperty =
            DependencyProperty.Register("CurrentStatus", typeof(string), typeof(SimulatorWindow));
        // next status label
        public string NextStatus
        {
            get { return (string)GetValue(NextStatusProperty); }
            set { SetValue(NextStatusProperty, value); }
        }
        public static readonly DependencyProperty NextStatusProperty =
            DependencyProperty.Register("NextStatus", typeof(string), typeof(SimulatorWindow));
        //start time of update
        public string Start
        {
            get { return (string)GetValue(StartProperty); }
            set { SetValue(StartProperty, value); }
        }
        public static readonly DependencyProperty StartProperty =
            DependencyProperty.Register("Start", typeof(string), typeof(SimulatorWindow));
        //End time of update
        public string End
        {
            get { return (string)GetValue(EndProperty); }
            set { SetValue(EndProperty, value); }
        }
        public static readonly DependencyProperty EndProperty =
            DependencyProperty.Register("End", typeof(string), typeof(SimulatorWindow));
        public double Progressbar
        {
            get { return (double)GetValue(BarProperty); }
            set { SetValue(BarProperty, value); }
        }
        public static readonly DependencyProperty BarProperty =
            DependencyProperty.Register("Progressbar", typeof(double), typeof(SimulatorWindow));
        public string Done
        {
            get { return (string)GetValue(DoneProperty); }
            set { SetValue(DoneProperty, value); }
        }
        public static readonly DependencyProperty DoneProperty =
            DependencyProperty.Register("Done", typeof(string), typeof(SimulatorWindow));
        #endregion
        #region stopwatch thread
        Stopwatch stopWatch;
        private volatile bool isTimerRun;
        private Thread timerThread;
        #endregion
        #region background worker
        private BackgroundWorker BgWorker;
        private bool _close=false;
        #endregion
        public SimulatorWindow()
        {
            CurrentHandle = new BO.Order();
            NextHandle = new BO.Order();
            CurrentStatus = "wait";
            NextStatus = "wait";
            Start = DateTime.Now.ToString();
            End=DateTime.Now.ToString();
            Done = "";
            Progressbar = 0;
            InitializeComponent();
            //BgWorker
            BgWorker = new BackgroundWorker();
            BgWorker.DoWork += _DoWork;
            BgWorker.ProgressChanged += _ProgressChanged;
            BgWorker.RunWorkerCompleted += _RunWorkerCompleted;
            BgWorker.WorkerReportsProgress = true;
            BgWorker.WorkerSupportsCancellation = true;
            BgWorker.RunWorkerAsync();
            //watch
            stopWatch = new Stopwatch();
            startTimer();
        }
        #region BgWorker functions
        private void _RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           Complete();
        }

        private void _ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                switch (e.ProgressPercentage)
                {
                    /*case 0: // complete
                        {
                            Progressbar = (Progressbar >= 100)?0:Progressbar+new Random().Next(1,3)*10;
                            Done =(Progressbar>=100)?"Complete!":"In Progress";
                        }
                        break;*/
                    case 1:
                        {
                            ArrayList args = (ArrayList)e.UserState;
                            CurrentHandle = (BO.Order?)args[0];
                            NextHandle = (CurrentHandle.ShipDate == DateTime.MinValue) ? new BO.Order()
                            {
                                ID = CurrentHandle.ID,
                                CustomerAdress = CurrentHandle.CustomerAdress,
                                CustomerEmail = CurrentHandle.CustomerEmail,
                                CustomerName = CurrentHandle.CustomerName,
                                OrderDate = CurrentHandle.OrderDate,
                                ShipDate = DateTime.Now,
                                DeliveryDate = CurrentHandle.DeliveryDate,
                            } : new BO.Order()
                            {
                                ID = CurrentHandle.ID,
                                CustomerAdress = CurrentHandle.CustomerAdress,
                                CustomerEmail = CurrentHandle.CustomerEmail,
                                CustomerName = CurrentHandle.CustomerName,
                                OrderDate = CurrentHandle.OrderDate,
                                ShipDate = CurrentHandle.ShipDate,
                                DeliveryDate = DateTime.Now
                            };
                            Done = "";
                            CurrentStatus = ((BO.Enums.Status)args[1]).ToString();
                            NextStatus = ((BO.Enums.Status)args[2]).ToString();
                            Start = args[3].ToString();
                            End = args[4].ToString();
                            int duration = ((DateTime)args[4] - (DateTime)args[3]).Seconds*1000;
                            Progressbar = 0;
                            Done = "In Progress";
                            Progressbar += 50;
                            /*
                            for (int i = 0; i < 101; i+=new Random().Next(15,30))
                            {
                                Thread.Sleep();
                                Progressbar = (Progressbar+i>=100)?100:Progressbar+i;
                                Done = (Progressbar >= 100) ? "Complete" : Done;
                            }*/
                            Progressbar = 0;
                            Thread.Sleep(1000);
                        }
                        break;
                    case 3:
                        {
                            Progressbar = 0;
                            Done = "Complete!";
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //throw new NotImplementedException();
        }

        private void _DoWork(object sender, DoWorkEventArgs e)
        {
            Simulator.Simulator.UpdateReg(Update);
            Simulator.Simulator.SimulatorCompleteReg(Complete);
            Simulator.Simulator.StartSimulation();
            /*
             * CancellationPending:
             * true if the application has requested cancellation of a background operation; otherwise, false. The default is false.
             */
            while (!BgWorker.CancellationPending)
            {
                Thread.Sleep(1000);
                BgWorker.ReportProgress(0); //what should be here?
            }
            e.Cancel = true;
        }

        private void Complete()
        {
           //BgWorker.ReportProgress(3);
        }

        private void Update(BO.Order? current, Enums.Status oldstatus, Enums.Status newstatus,DateTime starttime, DateTime finishtime)
        {
            ArrayList args = new ArrayList();
            args.Add(current);
            args.Add(oldstatus);
            args.Add(newstatus);
            args.Add(starttime);
            args.Add(finishtime);
            BgWorker.ReportProgress(1, args);
        }
        #endregion
        #region timer functions 
        private void startTimer()
        {
            stopWatch.Start();
            isTimerRun = true;
            timerThread = new Thread(runTimer);
            timerThread.Start();
        }
        private void runTimer()
        {
            while(isTimerRun)
            {
                string timerText=stopWatch.Elapsed.ToString();
                timerText = timerText.Substring(0, 8);
                setTextInvoke(timerText);
                Thread.Sleep(1000);
            }
        }
        void setTextInvoke(string text)
        {
            if (!CheckAccess())
            {
                Action<string> d = setTextInvoke;
                Dispatcher.BeginInvoke(d, new object[] { text });
            }
            else
            {
                this.timer.Text=text;
            }
        }
        #endregion
        #region button functions and closing
        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            isTimerRun=false; //stop timer thread.
            Simulator.Simulator.StopSimulator();
            Simulator.Simulator.UpdateDeletion(Update);
            Simulator.Simulator.SimulatorCompleteDeletion(Complete);
            BgWorker.CancelAsync(); //stop bg worker.
            _close = true;
            this.Close();
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (!_close)
            {
                e.Cancel = true;
                MessageBox.Show("Please use close button to close this window.");
            }
            e.Cancel = false;
            base.OnClosing(e);
        }
        #endregion
        #region Progress bar function 
      
        #endregion
    }
}
