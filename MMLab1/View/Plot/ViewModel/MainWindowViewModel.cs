using System;
using System.Globalization;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Threading;
using System.Diagnostics;

namespace MultiChartDemo
{

    public class MainWindowViewModel : ViewModelBase
    {

        #region Ctor

        public MainWindowViewModel()
        {
            // start timer to update chart data for this example
            DispatcherTimer updateTimer = new DispatcherTimer();
            updateTimer.Tick += new EventHandler(updateTimer_Tick);
            updateTimer.Interval = TimeSpan.FromMilliseconds(3000);
            //updateTimer.Start();

            UpdateData();
        }

        #endregion

        #region Data to bind

        public List<SalesPerformance> SalesData
        {
            get { return base.GetValue(() => this.SalesData); }
            set { base.SetValue(() => this.SalesData, value); }
        }

        public List<SalesPerformance> SalesDataWithMedian
        {
            get { return base.GetValue(() => this.SalesDataWithMedian); }
            set { base.SetValue(() => this.SalesDataWithMedian, value); }
        }


        public SalesPerformance SalesDetail
        {
            get { return base.GetValue(() => this.SalesDetail); }
            set { base.SetValue(() => this.SalesDetail, value); }
        }

        #endregion

        #region Private metohds

        void updateTimer_Tick(object sender, EventArgs e)
        {
            //Boolean isInDesignMode = false;

            //if(Application.Current.MainWindow != null)
            //    isInDesignMode = System.ComponentModel.DesignerProperties.GetIsInDesignMode(Application.Current.MainWindow);
            
            //if (!isInDesignMode)
                UpdateData();
        }

        private void UpdateData()
        {
            // Fill SalesData
            List<SalesPerformance> salesData = new List<SalesPerformance>();
            SalesPerformance salesPerf = new SalesPerformance();
            salesPerf.SalesName = "Miller";
            salesPerf.SalesTotals = new List<SalesInfo>();
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("01/31/2009","MM/dd/yyyy",CultureInfo.InvariantCulture).ToString(), SalesTotal = 10000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("02/28/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 12000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("03/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 14000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("04/28/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 15000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("05/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 14500 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("06/30/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 13000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("07/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 14300 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("08/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 17100 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("09/30/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 11000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("10/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 18000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("11/30/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 20400 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("12/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 21200 });
            salesData.Add(salesPerf);
            salesPerf = new SalesPerformance();
            salesPerf.SalesName = "Smith";
            salesPerf.SalesTotals = new List<SalesInfo>();
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("01/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 9000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("02/28/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 12000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("03/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 13000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("04/30/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 11000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("05/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 14500 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("06/30/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 16000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("07/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 14300 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("08/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 18100 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("09/30/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 12000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("10/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 13000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("11/30/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 21200 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("12/30/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 16500 });
            salesData.Add(salesPerf);
            salesPerf = new SalesPerformance();
            salesPerf.SalesName = "James";
            salesPerf.SalesTotals = new List<SalesInfo>();
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("01/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 17000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("02/28/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 16000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("03/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 18000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("04/30/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 19400 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("05/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 20500 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("06/30/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 21000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("07/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 21300 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("08/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 22100 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("09/30/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 19000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("10/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 18700 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("11/30/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 20400 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("12/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 23200 });
            salesData.Add(salesPerf);
            salesPerf = new SalesPerformance();
            salesPerf.SalesName = "Matthews";
            salesPerf.SalesTotals = new List<SalesInfo>();
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("01/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 11400 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("02/28/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 14500 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("03/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 13000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("04/30/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 17000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("05/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 17500 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("06/30/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 17700 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("07/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 18300 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("08/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 19100 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("09/30/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 20000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("10/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 21100 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("11/30/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 20400 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("12/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 22200 });
            salesData.Add(salesPerf);
            salesPerf = new SalesPerformance();
            salesPerf.SalesName = "Simpson";
            salesPerf.SalesTotals = new List<SalesInfo>();
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("01/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 18000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("02/28/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 17000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("03/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 14000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("04/30/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 15000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("05/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 14500 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("06/30/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 13600 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("07/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 14700 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("08/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 17900 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("09/30/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 19900 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("10/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 18500 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("11/30/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 20700 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("12/31/2009", "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = 20200 });
            salesData.Add(salesPerf);

            // Set SalesDetails
            Random random = new Random();
            int randomInt = random.Next(0, salesData.Count);
            this.SalesDetail = salesData[randomInt];

            // Set dynamic SalesData
            List<SalesPerformance> tmpData = new List<SalesPerformance>();
            randomInt = random.Next(1, salesData.Count);
            for (int i = 0; i < randomInt; i++)
            {
                tmpData.Add(salesData[random.Next(0, salesData.Count)]);
            }
            tmpData = tmpData.Distinct().ToList();
            this.SalesData = tmpData;


            SalesPerformance median = new SalesPerformance();
            median.SalesName = "Median";
            median.SalesTotals = new List<SalesInfo>();


            for (int i = 0; i < 12; i++)
            {
                var query = (from s in tmpData
                             where s.SalesTotals.Count > i
                             select s.SalesTotals[i].SalesTotal).Average();

                median.SalesTotals.Add(new SalesInfo { Date = DateTime.ParseExact("01." + (i + 1).ToString().PadLeft(2, '0') + ".2009", "MM.dd.yyyy", CultureInfo.InvariantCulture).ToString("MMM"), SalesTotal = (int)query });
            }

            List<SalesPerformance> salesDataWithMedian = new List<SalesPerformance>(tmpData);
            salesDataWithMedian.Insert(0, median);
            this.SalesDataWithMedian = salesDataWithMedian;
        }

        #endregion

    }

}
