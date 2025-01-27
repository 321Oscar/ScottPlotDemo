﻿namespace PlotDemo.Demos
{
    partial class ScottPlotDemoFrm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.rbDataLogger = new System.Windows.Forms.RadioButton();
            this.rbDataStreamer = new System.Windows.Forms.RadioButton();
            this.rbDataStreamerXY = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // formsPlot1
            // 
            this.formsPlot1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.formsPlot1.DisplayScale = 0F;
            this.formsPlot1.Location = new System.Drawing.Point(12, 55);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(619, 388);
            this.formsPlot1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(104, 24);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "stop";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(185, 24);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "add model";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // rbDataLogger
            // 
            this.rbDataLogger.AutoSize = true;
            this.rbDataLogger.Checked = true;
            this.rbDataLogger.Location = new System.Drawing.Point(290, 24);
            this.rbDataLogger.Name = "rbDataLogger";
            this.rbDataLogger.Size = new System.Drawing.Size(83, 16);
            this.rbDataLogger.TabIndex = 4;
            this.rbDataLogger.TabStop = true;
            this.rbDataLogger.Text = "DataLogger";
            this.rbDataLogger.UseVisualStyleBackColor = true;
            // 
            // rbDataStreamer
            // 
            this.rbDataStreamer.AutoSize = true;
            this.rbDataStreamer.Location = new System.Drawing.Point(385, 24);
            this.rbDataStreamer.Name = "rbDataStreamer";
            this.rbDataStreamer.Size = new System.Drawing.Size(95, 16);
            this.rbDataStreamer.TabIndex = 5;
            this.rbDataStreamer.Text = "DataStreamer";
            this.rbDataStreamer.UseVisualStyleBackColor = true;
            // 
            // rbDataStreamerXY
            // 
            this.rbDataStreamerXY.AutoSize = true;
            this.rbDataStreamerXY.Location = new System.Drawing.Point(492, 24);
            this.rbDataStreamerXY.Name = "rbDataStreamerXY";
            this.rbDataStreamerXY.Size = new System.Drawing.Size(107, 16);
            this.rbDataStreamerXY.TabIndex = 6;
            this.rbDataStreamerXY.Text = "DataStreamerXY";
            this.rbDataStreamerXY.UseVisualStyleBackColor = true;
            // 
            // ScottPlotDemoFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 455);
            this.Controls.Add(this.rbDataStreamerXY);
            this.Controls.Add(this.rbDataStreamer);
            this.Controls.Add(this.rbDataLogger);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.formsPlot1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ScottPlotDemoFrm";
            this.Text = "ScottPlot 5 RealTime Demo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ScottPlot.WinForms.FormsPlot formsPlot1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.RadioButton rbDataLogger;
        private System.Windows.Forms.RadioButton rbDataStreamer;
        private System.Windows.Forms.RadioButton rbDataStreamerXY;
    }
}

