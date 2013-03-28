using GraphLib;

namespace NeuralNetwork
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        PlotterDisplayEx display = null;
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.display = new GraphLib.PlotterDisplayEx();
            this.button1 = new System.Windows.Forms.Button();
            this.groupNetwork = new System.Windows.Forms.GroupBox();
            this.buttonCreateNetwork = new System.Windows.Forms.Button();
            this.radioHyperbolic = new System.Windows.Forms.RadioButton();
            this.radioSigmoid = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.textLayers = new System.Windows.Forms.TextBox();
            this.buttonRandomize = new System.Windows.Forms.Button();
            this.textSeed = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupWeights = new System.Windows.Forms.GroupBox();
            this.buttonGetWeights = new System.Windows.Forms.Button();
            this.buttonSetWeights = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.groupTraining = new System.Windows.Forms.GroupBox();
            this.numericTrainPercent = new System.Windows.Forms.NumericUpDown();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textDeltaStop = new System.Windows.Forms.TextBox();
            this.textErrorStop = new System.Windows.Forms.TextBox();
            this.textLambda = new System.Windows.Forms.TextBox();
            this.textAlpha = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonTrain = new System.Windows.Forms.Button();
            this.groupPlotting = new System.Windows.Forms.GroupBox();
            this.buttonPlot = new System.Windows.Forms.Button();
            this.buttonNetworkColor = new System.Windows.Forms.Button();
            this.buttonTrainColor = new System.Windows.Forms.Button();
            this.checkNetwork = new System.Windows.Forms.CheckBox();
            this.checkTest = new System.Windows.Forms.CheckBox();
            this.checkTrain = new System.Windows.Forms.CheckBox();
            this.groupData = new System.Windows.Forms.GroupBox();
            this.buttonLoadData = new System.Windows.Forms.Button();
            this.colorTrain = new System.Windows.Forms.ColorDialog();
            this.colorNetwork = new System.Windows.Forms.ColorDialog();
            this.openDataFile = new System.Windows.Forms.OpenFileDialog();
            this.groupNetwork.SuspendLayout();
            this.groupWeights.SuspendLayout();
            this.groupTraining.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericTrainPercent)).BeginInit();
            this.groupPlotting.SuspendLayout();
            this.groupData.SuspendLayout();
            this.SuspendLayout();
            // 
            // display
            // 
            this.display.BackColor = System.Drawing.Color.White;
            this.display.BackgroundColorBot = System.Drawing.Color.White;
            this.display.BackgroundColorTop = System.Drawing.Color.White;
            this.display.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.display.DashedGridColor = System.Drawing.Color.Blue;
            this.display.Dock = System.Windows.Forms.DockStyle.Top;
            this.display.DoubleBuffering = true;
            this.display.Location = new System.Drawing.Point(0, 0);
            this.display.Name = "display";
            this.display.PlaySpeed = 0.5F;
            this.display.ShowMovingGrid = false;
            this.display.Size = new System.Drawing.Size(889, 400);
            this.display.SolidGridColor = System.Drawing.Color.Blue;
            this.display.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(789, 615);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupNetwork
            // 
            this.groupNetwork.Controls.Add(this.buttonCreateNetwork);
            this.groupNetwork.Controls.Add(this.radioHyperbolic);
            this.groupNetwork.Controls.Add(this.radioSigmoid);
            this.groupNetwork.Controls.Add(this.label1);
            this.groupNetwork.Controls.Add(this.textLayers);
            this.groupNetwork.Location = new System.Drawing.Point(12, 406);
            this.groupNetwork.Name = "groupNetwork";
            this.groupNetwork.Size = new System.Drawing.Size(163, 111);
            this.groupNetwork.TabIndex = 3;
            this.groupNetwork.TabStop = false;
            this.groupNetwork.Text = "Network structure";
            // 
            // buttonCreateNetwork
            // 
            this.buttonCreateNetwork.Location = new System.Drawing.Point(9, 76);
            this.buttonCreateNetwork.Name = "buttonCreateNetwork";
            this.buttonCreateNetwork.Size = new System.Drawing.Size(138, 23);
            this.buttonCreateNetwork.TabIndex = 4;
            this.buttonCreateNetwork.Text = "Create network";
            this.buttonCreateNetwork.UseVisualStyleBackColor = true;
            this.buttonCreateNetwork.Click += new System.EventHandler(this.buttonCreateNetwork_Click);
            // 
            // radioHyperbolic
            // 
            this.radioHyperbolic.AutoSize = true;
            this.radioHyperbolic.Location = new System.Drawing.Point(77, 20);
            this.radioHyperbolic.Name = "radioHyperbolic";
            this.radioHyperbolic.Size = new System.Drawing.Size(75, 17);
            this.radioHyperbolic.TabIndex = 3;
            this.radioHyperbolic.Text = "Hyperbolic";
            this.radioHyperbolic.UseVisualStyleBackColor = true;
            // 
            // radioSigmoid
            // 
            this.radioSigmoid.AutoSize = true;
            this.radioSigmoid.Checked = true;
            this.radioSigmoid.Location = new System.Drawing.Point(9, 20);
            this.radioSigmoid.Name = "radioSigmoid";
            this.radioSigmoid.Size = new System.Drawing.Size(62, 17);
            this.radioSigmoid.TabIndex = 2;
            this.radioSigmoid.TabStop = true;
            this.radioSigmoid.Text = "Sigmoid";
            this.radioSigmoid.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Layers";
            // 
            // textLayers
            // 
            this.textLayers.Location = new System.Drawing.Point(47, 49);
            this.textLayers.Name = "textLayers";
            this.textLayers.Size = new System.Drawing.Size(100, 20);
            this.textLayers.TabIndex = 0;
            this.textLayers.Text = "5;4;4;1";
            // 
            // buttonRandomize
            // 
            this.buttonRandomize.Location = new System.Drawing.Point(9, 50);
            this.buttonRandomize.Name = "buttonRandomize";
            this.buttonRandomize.Size = new System.Drawing.Size(109, 23);
            this.buttonRandomize.TabIndex = 4;
            this.buttonRandomize.Text = "Randomize weights";
            this.buttonRandomize.UseVisualStyleBackColor = true;
            this.buttonRandomize.Click += new System.EventHandler(this.buttonRandomize_Click);
            // 
            // textSeed
            // 
            this.textSeed.Location = new System.Drawing.Point(115, 24);
            this.textSeed.Name = "textSeed";
            this.textSeed.Size = new System.Drawing.Size(25, 20);
            this.textSeed.TabIndex = 5;
            this.textSeed.Text = "-1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Randomization seed";
            // 
            // groupWeights
            // 
            this.groupWeights.Controls.Add(this.buttonGetWeights);
            this.groupWeights.Controls.Add(this.buttonSetWeights);
            this.groupWeights.Controls.Add(this.textBox2);
            this.groupWeights.Controls.Add(this.label2);
            this.groupWeights.Controls.Add(this.textSeed);
            this.groupWeights.Controls.Add(this.buttonRandomize);
            this.groupWeights.Enabled = false;
            this.groupWeights.Location = new System.Drawing.Point(181, 406);
            this.groupWeights.Name = "groupWeights";
            this.groupWeights.Size = new System.Drawing.Size(174, 144);
            this.groupWeights.TabIndex = 4;
            this.groupWeights.TabStop = false;
            this.groupWeights.Text = "Weights";
            // 
            // buttonGetWeights
            // 
            this.buttonGetWeights.Location = new System.Drawing.Point(9, 105);
            this.buttonGetWeights.Name = "buttonGetWeights";
            this.buttonGetWeights.Size = new System.Drawing.Size(75, 23);
            this.buttonGetWeights.TabIndex = 9;
            this.buttonGetWeights.Text = "Get weights";
            this.buttonGetWeights.UseVisualStyleBackColor = true;
            // 
            // buttonSetWeights
            // 
            this.buttonSetWeights.Location = new System.Drawing.Point(90, 105);
            this.buttonSetWeights.Name = "buttonSetWeights";
            this.buttonSetWeights.Size = new System.Drawing.Size(75, 23);
            this.buttonSetWeights.TabIndex = 8;
            this.buttonSetWeights.Text = "Set weights";
            this.buttonSetWeights.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(9, 79);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(156, 20);
            this.textBox2.TabIndex = 7;
            // 
            // groupTraining
            // 
            this.groupTraining.Controls.Add(this.numericTrainPercent);
            this.groupTraining.Controls.Add(this.textBox3);
            this.groupTraining.Controls.Add(this.label7);
            this.groupTraining.Controls.Add(this.textDeltaStop);
            this.groupTraining.Controls.Add(this.textErrorStop);
            this.groupTraining.Controls.Add(this.textLambda);
            this.groupTraining.Controls.Add(this.textAlpha);
            this.groupTraining.Controls.Add(this.label6);
            this.groupTraining.Controls.Add(this.label5);
            this.groupTraining.Controls.Add(this.label4);
            this.groupTraining.Controls.Add(this.label3);
            this.groupTraining.Controls.Add(this.buttonTrain);
            this.groupTraining.Enabled = false;
            this.groupTraining.Location = new System.Drawing.Point(480, 406);
            this.groupTraining.Name = "groupTraining";
            this.groupTraining.Size = new System.Drawing.Size(183, 171);
            this.groupTraining.TabIndex = 5;
            this.groupTraining.TabStop = false;
            this.groupTraining.Text = "Training";
            // 
            // numericTrainPercent
            // 
            this.numericTrainPercent.Location = new System.Drawing.Point(140, 124);
            this.numericTrainPercent.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericTrainPercent.Name = "numericTrainPercent";
            this.numericTrainPercent.Size = new System.Drawing.Size(37, 20);
            this.numericTrainPercent.TabIndex = 9;
            this.numericTrainPercent.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(44, 147);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(51, 20);
            this.textBox3.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Error";
            // 
            // textDeltaStop
            // 
            this.textDeltaStop.Location = new System.Drawing.Point(80, 95);
            this.textDeltaStop.Name = "textDeltaStop";
            this.textDeltaStop.Size = new System.Drawing.Size(51, 20);
            this.textDeltaStop.TabIndex = 8;
            this.textDeltaStop.Text = "0.00001";
            // 
            // textErrorStop
            // 
            this.textErrorStop.Location = new System.Drawing.Point(80, 72);
            this.textErrorStop.Name = "textErrorStop";
            this.textErrorStop.Size = new System.Drawing.Size(51, 20);
            this.textErrorStop.TabIndex = 7;
            this.textErrorStop.Text = "0.01";
            // 
            // textLambda
            // 
            this.textLambda.Location = new System.Drawing.Point(80, 43);
            this.textLambda.Name = "textLambda";
            this.textLambda.Size = new System.Drawing.Size(51, 20);
            this.textLambda.TabIndex = 6;
            this.textLambda.Text = "0.0001";
            // 
            // textAlpha
            // 
            this.textAlpha.Location = new System.Drawing.Point(80, 19);
            this.textAlpha.Name = "textAlpha";
            this.textAlpha.Size = new System.Drawing.Size(51, 20);
            this.textAlpha.TabIndex = 5;
            this.textAlpha.Text = "1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Stop on delta";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Stop on error";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Lambda";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Alpha";
            // 
            // buttonTrain
            // 
            this.buttonTrain.Location = new System.Drawing.Point(9, 121);
            this.buttonTrain.Name = "buttonTrain";
            this.buttonTrain.Size = new System.Drawing.Size(125, 23);
            this.buttonTrain.TabIndex = 0;
            this.buttonTrain.Text = "Train prediction";
            this.buttonTrain.UseVisualStyleBackColor = true;
            this.buttonTrain.Click += new System.EventHandler(this.buttonTrain_Click);
            // 
            // groupPlotting
            // 
            this.groupPlotting.Controls.Add(this.buttonPlot);
            this.groupPlotting.Controls.Add(this.buttonNetworkColor);
            this.groupPlotting.Controls.Add(this.buttonTrainColor);
            this.groupPlotting.Controls.Add(this.checkNetwork);
            this.groupPlotting.Controls.Add(this.checkTest);
            this.groupPlotting.Controls.Add(this.checkTrain);
            this.groupPlotting.Location = new System.Drawing.Point(669, 406);
            this.groupPlotting.Name = "groupPlotting";
            this.groupPlotting.Size = new System.Drawing.Size(200, 128);
            this.groupPlotting.TabIndex = 6;
            this.groupPlotting.TabStop = false;
            this.groupPlotting.Text = "Plotting";
            // 
            // buttonPlot
            // 
            this.buttonPlot.Location = new System.Drawing.Point(6, 94);
            this.buttonPlot.Name = "buttonPlot";
            this.buttonPlot.Size = new System.Drawing.Size(75, 23);
            this.buttonPlot.TabIndex = 5;
            this.buttonPlot.Text = "Plot";
            this.buttonPlot.UseVisualStyleBackColor = true;
            this.buttonPlot.Click += new System.EventHandler(this.buttonPlot_Click);
            // 
            // buttonNetworkColor
            // 
            this.buttonNetworkColor.Location = new System.Drawing.Point(98, 67);
            this.buttonNetworkColor.Name = "buttonNetworkColor";
            this.buttonNetworkColor.Size = new System.Drawing.Size(75, 23);
            this.buttonNetworkColor.TabIndex = 4;
            this.buttonNetworkColor.Text = "Color...";
            this.buttonNetworkColor.UseVisualStyleBackColor = true;
            // 
            // buttonTrainColor
            // 
            this.buttonTrainColor.Location = new System.Drawing.Point(98, 20);
            this.buttonTrainColor.Name = "buttonTrainColor";
            this.buttonTrainColor.Size = new System.Drawing.Size(75, 23);
            this.buttonTrainColor.TabIndex = 3;
            this.buttonTrainColor.Text = "Color...";
            this.buttonTrainColor.UseVisualStyleBackColor = true;
            // 
            // checkNetwork
            // 
            this.checkNetwork.AutoSize = true;
            this.checkNetwork.Location = new System.Drawing.Point(6, 71);
            this.checkNetwork.Name = "checkNetwork";
            this.checkNetwork.Size = new System.Drawing.Size(66, 17);
            this.checkNetwork.TabIndex = 2;
            this.checkNetwork.Text = "Network";
            this.checkNetwork.UseVisualStyleBackColor = true;
            // 
            // checkTest
            // 
            this.checkTest.AutoSize = true;
            this.checkTest.Location = new System.Drawing.Point(6, 48);
            this.checkTest.Name = "checkTest";
            this.checkTest.Size = new System.Drawing.Size(83, 17);
            this.checkTest.TabIndex = 1;
            this.checkTest.Text = "Test sample";
            this.checkTest.UseVisualStyleBackColor = true;
            // 
            // checkTrain
            // 
            this.checkTrain.AutoSize = true;
            this.checkTrain.Location = new System.Drawing.Point(6, 24);
            this.checkTrain.Name = "checkTrain";
            this.checkTrain.Size = new System.Drawing.Size(86, 17);
            this.checkTrain.TabIndex = 0;
            this.checkTrain.Text = "Train sample";
            this.checkTrain.UseVisualStyleBackColor = true;
            // 
            // groupData
            // 
            this.groupData.Controls.Add(this.buttonLoadData);
            this.groupData.Enabled = false;
            this.groupData.Location = new System.Drawing.Point(361, 406);
            this.groupData.Name = "groupData";
            this.groupData.Size = new System.Drawing.Size(113, 100);
            this.groupData.TabIndex = 10;
            this.groupData.TabStop = false;
            this.groupData.Text = "Data";
            // 
            // buttonLoadData
            // 
            this.buttonLoadData.Location = new System.Drawing.Point(6, 24);
            this.buttonLoadData.Name = "buttonLoadData";
            this.buttonLoadData.Size = new System.Drawing.Size(75, 23);
            this.buttonLoadData.TabIndex = 0;
            this.buttonLoadData.Text = "From file";
            this.buttonLoadData.UseVisualStyleBackColor = true;
            this.buttonLoadData.Click += new System.EventHandler(this.buttonLoadData_Click);
            // 
            // openDataFile
            // 
            this.openDataFile.FileName = "openFileDialog1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 662);
            this.Controls.Add(this.groupData);
            this.Controls.Add(this.groupPlotting);
            this.Controls.Add(this.groupTraining);
            this.Controls.Add(this.groupWeights);
            this.Controls.Add(this.groupNetwork);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.display);
            this.Name = "MainForm";
            this.Text = "Neuron Network";
            this.groupNetwork.ResumeLayout(false);
            this.groupNetwork.PerformLayout();
            this.groupWeights.ResumeLayout(false);
            this.groupWeights.PerformLayout();
            this.groupTraining.ResumeLayout(false);
            this.groupTraining.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericTrainPercent)).EndInit();
            this.groupPlotting.ResumeLayout(false);
            this.groupPlotting.PerformLayout();
            this.groupData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupNetwork;
        private System.Windows.Forms.RadioButton radioHyperbolic;
        private System.Windows.Forms.RadioButton radioSigmoid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textLayers;
        private System.Windows.Forms.Button buttonCreateNetwork;
        private System.Windows.Forms.Button buttonRandomize;
        private System.Windows.Forms.TextBox textSeed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupWeights;
        private System.Windows.Forms.Button buttonGetWeights;
        private System.Windows.Forms.Button buttonSetWeights;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.GroupBox groupTraining;
        private System.Windows.Forms.NumericUpDown numericTrainPercent;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textDeltaStop;
        private System.Windows.Forms.TextBox textErrorStop;
        private System.Windows.Forms.TextBox textLambda;
        private System.Windows.Forms.TextBox textAlpha;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonTrain;
        private System.Windows.Forms.GroupBox groupPlotting;
        private System.Windows.Forms.Button buttonNetworkColor;
        private System.Windows.Forms.Button buttonTrainColor;
        private System.Windows.Forms.CheckBox checkNetwork;
        private System.Windows.Forms.CheckBox checkTest;
        private System.Windows.Forms.CheckBox checkTrain;
        private System.Windows.Forms.GroupBox groupData;
        private System.Windows.Forms.Button buttonLoadData;
        private System.Windows.Forms.ColorDialog colorTrain;
        private System.Windows.Forms.ColorDialog colorNetwork;
        private System.Windows.Forms.Button buttonPlot;
        private System.Windows.Forms.OpenFileDialog openDataFile;
    }
}

