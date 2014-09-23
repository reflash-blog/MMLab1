using System.Collections.Generic;

namespace MMLab1.View.Plot
{

    public class MainWindowViewModel : ViewModelBase
    {

        #region Ctor

        public MainWindowViewModel()
        {
            // start timer to update chart data for this example
            UpdateData();
        }

        #endregion

        #region Data to bind

        public List<ConcentrationCell> ConcentrationData
        {
            get { return base.GetValue(() => this.ConcentrationData); }
            set { base.SetValue(() => this.ConcentrationData, value); }
        }

        #endregion
        public void UpdateData(List<ConcentrationCell> concentrationData )
        {
            this.ConcentrationData = concentrationData;
        }

        #region Private metohds

        private void UpdateData()
        {
            // Fill concentrationData
            List<ConcentrationCell> concentrationData = new List<ConcentrationCell>();
            ConcentrationCell concentrationCell = new ConcentrationCell
            {
                ConcentrationName = "Ячейка 1",
                ConcentrationInfos = new List<ConcentrationInfo>
                {
                    new ConcentrationInfo {Time = 1, Concentration = 10000},
                    new ConcentrationInfo {Time = 2, Concentration = 20000},
                    new ConcentrationInfo {Time = 3, Concentration = 25000},
                    new ConcentrationInfo {Time = 4, Concentration = 28000},
                    new ConcentrationInfo {Time = 5, Concentration = 29500},
                    new ConcentrationInfo {Time = 6, Concentration = 30000},
                    new ConcentrationInfo {Time = 7, Concentration = 30300},
                    new ConcentrationInfo {Time = 8, Concentration = 30500},
                    new ConcentrationInfo {Time = 9, Concentration = 30550},
                    new ConcentrationInfo {Time = 10, Concentration = 30610},
                    new ConcentrationInfo {Time = 11, Concentration = 30660},
                    new ConcentrationInfo {Time = 12, Concentration = 30700}
                }
            };
            concentrationData.Add(concentrationCell);
            concentrationCell = new ConcentrationCell
            {
                ConcentrationName = "Ячейка 2",
                ConcentrationInfos =
                    new List<ConcentrationInfo>
                    {
                        new ConcentrationInfo {Time = 1, Concentration = 7000},
                        new ConcentrationInfo {Time = 2, Concentration = 16000},
                        new ConcentrationInfo {Time = 3, Concentration = 21000},
                        new ConcentrationInfo {Time = 4, Concentration = 24000},
                        new ConcentrationInfo {Time = 5, Concentration = 25500},
                        new ConcentrationInfo {Time = 6, Concentration = 26000},
                        new ConcentrationInfo {Time = 7, Concentration = 26300},
                        new ConcentrationInfo {Time = 8, Concentration = 26500},
                        new ConcentrationInfo {Time = 9, Concentration = 26550},
                        new ConcentrationInfo {Time = 10, Concentration = 26610},
                        new ConcentrationInfo {Time = 11, Concentration = 26660},
                        new ConcentrationInfo {Time = 12, Concentration = 26700}
                    }
            };
            concentrationData.Add(concentrationCell);
            this.ConcentrationData = concentrationData;
        }

        #endregion

    }

}
