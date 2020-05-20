using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using DES;

namespace R07546039YYLiuAss08
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            //test
            InitializeComponent();
            theModel = new DESmodel();
            propertyGrid.SelectedObject = theModel;
            Client.SetClientChartAndArea(chartClient, chartClient.ChartAreas[0]); //  static function 不需要 new CLient 就可以呼叫
            Server.SetServerChartAndPieChart(chartServerAndQueue, chartServerAndQueue.ChartAreas[0], chartPie);
            TimedQueue.SetQueueChartAndArea(chartServerAndQueue, chartServerAndQueue.ChartAreas[1]);

            chartServerAndQueue.ChartAreas[1].AxisX.MajorGrid.LineColor = Color.Silver;
            chartServerAndQueue.ChartAreas[1].AxisY.MajorGrid.LineColor = Color.Silver;
            chartServerAndQueue.ChartAreas[1].AxisY.Minimum = 0;
            chartServerAndQueue.ChartAreas[1].AxisX.Minimum = 0;

            chartClient.ChartAreas[0].CursorX.IsUserEnabled = true;
            chartClient.ChartAreas[0].CursorX.Interval = 0;
            chartClient.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chartClient.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chartClient.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            chartClient.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.ResetZoom;
            chartClient.ChartAreas[0].AxisX.ScaleView.SmallScrollMinSize = 0;

            chartClient.ChartAreas[0].CursorY.IsUserEnabled = true;
            chartClient.ChartAreas[0].CursorY.Interval = 0;
            chartClient.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            chartClient.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chartClient.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;
            chartClient.ChartAreas[0].AxisY.ScrollBar.ButtonStyle = ScrollBarButtonStyles.ResetZoom;
            chartClient.ChartAreas[0].AxisY.ScaleView.SmallScrollMinSize = 0;
        }

        DESmodel theModel;
        private void btnReset_Click(object sender, EventArgs e)
        {          
            if (theModel.Nodes.Count <= 0)
            {
                MessageBox.Show("Simulation Model is not Valid!");
                return;
            }
            else
            {
                if (theModel.Nodes[0].Servers == null || theModel.Nodes[0].Servers.Count <= 0)
                {
                    MessageBox.Show("Simulation Model is not Valid!");
                    return;
                }
            }

            btnRunOneEvent.Enabled = btnRunToEnd.Enabled = true;
            rtbOutput.Clear();

            Client.ResetClientCounter();
            chartClient.Series.Clear();

            chartServerAndQueue.Series.Clear();
            chartPie.Series.Clear();
            chartPie.Titles.Clear();
            chartPie.ChartAreas.Clear();

            theModel.ResetSimulation();

            chartServerAndQueue.Invalidate(); // 類似 refresh
            chartPie.Invalidate();

            rtbEventList.Text = theModel.FutureEventListString();         
        }

        private void btnRunOneEvent_Click(object sender, EventArgs e)
        {
            theModel.RunNextEvent();
            rtbEventList.Text = theModel.FutureEventListString();
            chartServerAndQueue.Invalidate();
            chartPie.Invalidate();
        }

        private void btnRunToEnd_Click(object sender, EventArgs e)
        {            
            rtbOutput.Clear();
            while (theModel.RunNextEvent())
            {
                rtbEventList.Text = theModel.FutureEventListString();
                chartServerAndQueue.Invalidate();
                chartPie.Invalidate();
            }
            rtbOutput.Text = theModel.DisplaySimulationResults();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (dlgSave.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(dlgSave.FileName);
                theModel.SaveToFileStream(sw);
                sw.Close();
            }
        }

        private void tsbOpen_Click(object sender, EventArgs e)
        {
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(dlgOpen.FileName);
                theModel.ReadFromFileStream(sr);
                sr.Close();
            }
        }

        private void tsp_SSQ_Click(object sender, EventArgs e)
        {
            theModel = DESmodel.CreateSSQModel();
            propertyGrid.SelectedObject = theModel;
        }

        private void tsp_bankAndMcDonald_Click(object sender, EventArgs e)
        {
            theModel = DESmodel.CreateBandAndMcDonaldModel();
            propertyGrid.SelectedObject = theModel;
        }

        private void tsp_factory_Click(object sender, EventArgs e)
        {
            theModel = DESmodel.CreateFactoryModel();
            propertyGrid.SelectedObject = theModel;
        }

        private void tsp_computer_Click(object sender, EventArgs e)
        {
            theModel = DESmodel.CreateComputerModel();
            propertyGrid.SelectedObject = theModel;
        }
    }
}
