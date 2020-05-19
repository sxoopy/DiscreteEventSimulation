namespace R07546039YYLiuAss08
{
    partial class MainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.chartPie = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnRunOneEvent = new System.Windows.Forms.Button();
            this.btnRunToEnd = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabData = new System.Windows.Forms.TabControl();
            this.pageEventList = new System.Windows.Forms.TabPage();
            this.rtbEventList = new System.Windows.Forms.RichTextBox();
            this.pageResult = new System.Windows.Forms.TabPage();
            this.rtbOutput = new System.Windows.Forms.RichTextBox();
            this.tabChart = new System.Windows.Forms.TabControl();
            this.pageServersAndQueue = new System.Windows.Forms.TabPage();
            this.chartServerAndQueue = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pageClient = new System.Windows.Forms.TabPage();
            this.chartClient = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pagePie = new System.Windows.Forms.TabPage();
            this.tsp = new System.Windows.Forms.ToolStrip();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbCase = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsp_SSQ = new System.Windows.Forms.ToolStripMenuItem();
            this.tsp_bankAndMcDonald = new System.Windows.Forms.ToolStripMenuItem();
            this.tsp_factory = new System.Windows.Forms.ToolStripMenuItem();
            this.tsp_computer = new System.Windows.Forms.ToolStripMenuItem();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.chartPie)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabData.SuspendLayout();
            this.pageEventList.SuspendLayout();
            this.pageResult.SuspendLayout();
            this.tabChart.SuspendLayout();
            this.pageServersAndQueue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartServerAndQueue)).BeginInit();
            this.pageClient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartClient)).BeginInit();
            this.pagePie.SuspendLayout();
            this.tsp.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartPie
            // 
            this.chartPie.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartPie.Location = new System.Drawing.Point(0, 0);
            this.chartPie.Name = "chartPie";
            this.chartPie.Size = new System.Drawing.Size(849, 463);
            this.chartPie.TabIndex = 3;
            this.chartPie.Text = "chart1";
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Location = new System.Drawing.Point(27, 19);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(265, 40);
            this.btnReset.TabIndex = 6;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnRunOneEvent
            // 
            this.btnRunOneEvent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRunOneEvent.Enabled = false;
            this.btnRunOneEvent.Location = new System.Drawing.Point(27, 80);
            this.btnRunOneEvent.Name = "btnRunOneEvent";
            this.btnRunOneEvent.Size = new System.Drawing.Size(265, 40);
            this.btnRunOneEvent.TabIndex = 5;
            this.btnRunOneEvent.Text = "Run One Event";
            this.btnRunOneEvent.UseVisualStyleBackColor = true;
            this.btnRunOneEvent.Click += new System.EventHandler(this.btnRunOneEvent_Click);
            // 
            // btnRunToEnd
            // 
            this.btnRunToEnd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRunToEnd.Enabled = false;
            this.btnRunToEnd.Location = new System.Drawing.Point(27, 139);
            this.btnRunToEnd.Name = "btnRunToEnd";
            this.btnRunToEnd.Size = new System.Drawing.Size(265, 40);
            this.btnRunToEnd.TabIndex = 7;
            this.btnRunToEnd.Text = "Run To End";
            this.btnRunToEnd.UseVisualStyleBackColor = true;
            this.btnRunToEnd.Click += new System.EventHandler(this.btnRunToEnd_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1178, 640);
            this.splitContainer1.SplitterDistance = 317;
            this.splitContainer1.TabIndex = 8;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.BackColor = System.Drawing.Color.AntiqueWhite;
            this.splitContainer4.Panel1.Controls.Add(this.btnRunToEnd);
            this.splitContainer4.Panel1.Controls.Add(this.btnRunOneEvent);
            this.splitContainer4.Panel1.Controls.Add(this.btnReset);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.propertyGrid);
            this.splitContainer4.Size = new System.Drawing.Size(317, 640);
            this.splitContainer4.SplitterDistance = 204;
            this.splitContainer4.TabIndex = 8;
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(317, 432);
            this.propertyGrid.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tabData);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabChart);
            this.splitContainer2.Size = new System.Drawing.Size(857, 640);
            this.splitContainer2.SplitterDistance = 147;
            this.splitContainer2.TabIndex = 0;
            // 
            // tabData
            // 
            this.tabData.Controls.Add(this.pageEventList);
            this.tabData.Controls.Add(this.pageResult);
            this.tabData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabData.Location = new System.Drawing.Point(0, 0);
            this.tabData.Name = "tabData";
            this.tabData.SelectedIndex = 0;
            this.tabData.Size = new System.Drawing.Size(857, 147);
            this.tabData.TabIndex = 0;
            // 
            // pageEventList
            // 
            this.pageEventList.Controls.Add(this.rtbEventList);
            this.pageEventList.Location = new System.Drawing.Point(4, 23);
            this.pageEventList.Name = "pageEventList";
            this.pageEventList.Padding = new System.Windows.Forms.Padding(3);
            this.pageEventList.Size = new System.Drawing.Size(849, 120);
            this.pageEventList.TabIndex = 0;
            this.pageEventList.Text = "Current Event List";
            this.pageEventList.UseVisualStyleBackColor = true;
            // 
            // rtbEventList
            // 
            this.rtbEventList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbEventList.Location = new System.Drawing.Point(3, 3);
            this.rtbEventList.Name = "rtbEventList";
            this.rtbEventList.Size = new System.Drawing.Size(843, 114);
            this.rtbEventList.TabIndex = 0;
            this.rtbEventList.Text = "***** Current Event List *****";
            // 
            // pageResult
            // 
            this.pageResult.Controls.Add(this.rtbOutput);
            this.pageResult.Location = new System.Drawing.Point(4, 22);
            this.pageResult.Name = "pageResult";
            this.pageResult.Padding = new System.Windows.Forms.Padding(3);
            this.pageResult.Size = new System.Drawing.Size(849, 121);
            this.pageResult.TabIndex = 1;
            this.pageResult.Text = "Result Data";
            this.pageResult.UseVisualStyleBackColor = true;
            // 
            // rtbOutput
            // 
            this.rtbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbOutput.Location = new System.Drawing.Point(3, 3);
            this.rtbOutput.Name = "rtbOutput";
            this.rtbOutput.Size = new System.Drawing.Size(843, 115);
            this.rtbOutput.TabIndex = 1;
            this.rtbOutput.Text = "***** Result *****";
            // 
            // tabChart
            // 
            this.tabChart.Controls.Add(this.pageServersAndQueue);
            this.tabChart.Controls.Add(this.pageClient);
            this.tabChart.Controls.Add(this.pagePie);
            this.tabChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabChart.Location = new System.Drawing.Point(0, 0);
            this.tabChart.Name = "tabChart";
            this.tabChart.SelectedIndex = 0;
            this.tabChart.Size = new System.Drawing.Size(857, 489);
            this.tabChart.TabIndex = 0;
            // 
            // pageServersAndQueue
            // 
            this.pageServersAndQueue.Controls.Add(this.chartServerAndQueue);
            this.pageServersAndQueue.Location = new System.Drawing.Point(4, 23);
            this.pageServersAndQueue.Name = "pageServersAndQueue";
            this.pageServersAndQueue.Padding = new System.Windows.Forms.Padding(3);
            this.pageServersAndQueue.Size = new System.Drawing.Size(849, 462);
            this.pageServersAndQueue.TabIndex = 0;
            this.pageServersAndQueue.Text = "Servers and Queue";
            this.pageServersAndQueue.UseVisualStyleBackColor = true;
            // 
            // chartServerAndQueue
            // 
            chartArea4.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea4.AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea4.AxisX.Title = "Time";
            chartArea4.AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea4.AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea4.AxisY.Title = "Server";
            chartArea4.Name = "ChartArea1";
            chartArea5.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea5.AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea5.AxisX.Title = "Time";
            chartArea5.AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea5.AxisY.Title = "Queu Lenght";
            chartArea5.Name = "ChartArea2";
            this.chartServerAndQueue.ChartAreas.Add(chartArea4);
            this.chartServerAndQueue.ChartAreas.Add(chartArea5);
            this.chartServerAndQueue.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Alignment = System.Drawing.StringAlignment.Center;
            legend2.DockedToChartArea = "ChartArea2";
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend2.IsDockedInsideChartArea = false;
            legend2.Name = "Legend1";
            this.chartServerAndQueue.Legends.Add(legend2);
            this.chartServerAndQueue.Location = new System.Drawing.Point(3, 3);
            this.chartServerAndQueue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chartServerAndQueue.Name = "chartServerAndQueue";
            this.chartServerAndQueue.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            this.chartServerAndQueue.Size = new System.Drawing.Size(843, 456);
            this.chartServerAndQueue.TabIndex = 3;
            this.chartServerAndQueue.Text = "chart1";
            // 
            // pageClient
            // 
            this.pageClient.Controls.Add(this.chartClient);
            this.pageClient.Location = new System.Drawing.Point(4, 22);
            this.pageClient.Name = "pageClient";
            this.pageClient.Padding = new System.Windows.Forms.Padding(3);
            this.pageClient.Size = new System.Drawing.Size(849, 463);
            this.pageClient.TabIndex = 1;
            this.pageClient.Text = "Client";
            this.pageClient.UseVisualStyleBackColor = true;
            // 
            // chartClient
            // 
            chartArea6.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea6.AxisX.Title = "Time";
            chartArea6.AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea6.AxisY.Title = "Client";
            chartArea6.Name = "ChartArea1";
            this.chartClient.ChartAreas.Add(chartArea6);
            this.chartClient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartClient.Location = new System.Drawing.Point(3, 3);
            this.chartClient.Name = "chartClient";
            this.chartClient.Size = new System.Drawing.Size(843, 457);
            this.chartClient.TabIndex = 0;
            // 
            // pagePie
            // 
            this.pagePie.Controls.Add(this.chartPie);
            this.pagePie.Location = new System.Drawing.Point(4, 22);
            this.pagePie.Name = "pagePie";
            this.pagePie.Size = new System.Drawing.Size(849, 463);
            this.pagePie.TabIndex = 2;
            this.pagePie.Text = "Server Utilization";
            this.pagePie.UseVisualStyleBackColor = true;
            // 
            // tsp
            // 
            this.tsp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSave,
            this.tsbOpen,
            this.tsbCase});
            this.tsp.Location = new System.Drawing.Point(0, 0);
            this.tsp.Name = "tsp";
            this.tsp.Size = new System.Drawing.Size(1178, 25);
            this.tsp.TabIndex = 9;
            // 
            // tsbSave
            // 
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(54, 22);
            this.tsbSave.Text = "Save";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // tsbOpen
            // 
            this.tsbOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpen.Image")));
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(59, 22);
            this.tsbOpen.Text = "Open";
            this.tsbOpen.ToolTipText = "Open";
            this.tsbOpen.Click += new System.EventHandler(this.tsbOpen_Click);
            // 
            // tsbCase
            // 
            this.tsbCase.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsp_SSQ,
            this.tsp_bankAndMcDonald,
            this.tsp_factory,
            this.tsp_computer});
            this.tsbCase.Image = ((System.Drawing.Image)(resources.GetObject("tsbCase.Image")));
            this.tsbCase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCase.Name = "tsbCase";
            this.tsbCase.Size = new System.Drawing.Size(136, 22);
            this.tsbCase.Text = "Choose One Case";
            // 
            // tsp_SSQ
            // 
            this.tsp_SSQ.Name = "tsp_SSQ";
            this.tsp_SSQ.Size = new System.Drawing.Size(189, 22);
            this.tsp_SSQ.Text = "SSQ";
            this.tsp_SSQ.Click += new System.EventHandler(this.tsp_SSQ_Click);
            // 
            // tsp_bankAndMcDonald
            // 
            this.tsp_bankAndMcDonald.Name = "tsp_bankAndMcDonald";
            this.tsp_bankAndMcDonald.Size = new System.Drawing.Size(189, 22);
            this.tsp_bankAndMcDonald.Text = "Bank and McDonald";
            this.tsp_bankAndMcDonald.Click += new System.EventHandler(this.tsp_bankAndMcDonald_Click);
            // 
            // tsp_factory
            // 
            this.tsp_factory.Name = "tsp_factory";
            this.tsp_factory.Size = new System.Drawing.Size(189, 22);
            this.tsp_factory.Text = "Factory";
            this.tsp_factory.Click += new System.EventHandler(this.tsp_factory_Click);
            // 
            // tsp_computer
            // 
            this.tsp_computer.Name = "tsp_computer";
            this.tsp_computer.Size = new System.Drawing.Size(189, 22);
            this.tsp_computer.Text = "Computer";
            this.tsp_computer.Click += new System.EventHandler(this.tsp_computer_Click);
            // 
            // dlgSave
            // 
            this.dlgSave.DefaultExt = "txt";
            // 
            // dlgOpen
            // 
            this.dlgOpen.DefaultExt = "txt";
            this.dlgOpen.FileName = "openFileDialog1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 665);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.tsp);
            this.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Next Event Simulation";
            ((System.ComponentModel.ISupportInitialize)(this.chartPie)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabData.ResumeLayout(false);
            this.pageEventList.ResumeLayout(false);
            this.pageResult.ResumeLayout(false);
            this.tabChart.ResumeLayout(false);
            this.pageServersAndQueue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartServerAndQueue)).EndInit();
            this.pageClient.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartClient)).EndInit();
            this.pagePie.ResumeLayout(false);
            this.tsp.ResumeLayout(false);
            this.tsp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPie;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnRunOneEvent;
        private System.Windows.Forms.Button btnRunToEnd;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.RichTextBox rtbEventList;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartServerAndQueue;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.RichTextBox rtbOutput;
        private System.Windows.Forms.TabControl tabData;
        private System.Windows.Forms.TabPage pageEventList;
        private System.Windows.Forms.TabPage pageResult;
        private System.Windows.Forms.TabControl tabChart;
        private System.Windows.Forms.TabPage pageServersAndQueue;
        private System.Windows.Forms.TabPage pageClient;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartClient;
        private System.Windows.Forms.TabPage pagePie;
        private System.Windows.Forms.ToolStrip tsp;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton tsbOpen;
        private System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.ToolStripDropDownButton tsbCase;
        private System.Windows.Forms.ToolStripMenuItem tsp_SSQ;
        private System.Windows.Forms.ToolStripMenuItem tsp_bankAndMcDonald;
        private System.Windows.Forms.ToolStripMenuItem tsp_factory;
        private System.Windows.Forms.ToolStripMenuItem tsp_computer;
    }
}

