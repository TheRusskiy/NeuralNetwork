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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radioSigmoid = new System.Windows.Forms.RadioButton();
            this.radioHyperbolic = new System.Windows.Forms.RadioButton();
            this.buttonRandomize = new System.Windows.Forms.Button();
            this.textSeed = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.buttonSetWeights = new System.Windows.Forms.Button();
            this.buttonGetWeights = new System.Windows.Forms.Button();
            this.buttonCreateNetwork = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonTrain = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textAlpha = new System.Windows.Forms.TextBox();
            this.textLambda = new System.Windows.Forms.TextBox();
            this.textDeltaStop = new System.Windows.Forms.TextBox();
            this.textErrorStop = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.buttonLoadData = new System.Windows.Forms.Button();
            this.checkTrain = new System.Windows.Forms.CheckBox();
            this.checkTest = new System.Windows.Forms.CheckBox();
            this.checkNetwork = new System.Windows.Forms.CheckBox();
            this.colorTrain = new System.Windows.Forms.ColorDialog();
            this.colorNetwork = new System.Windows.Forms.ColorDialog();
            this.buttonTrainColor = new System.Windows.Forms.Button();
            this.buttonNetworkColor = new System.Windows.Forms.Button();
            this.buttonPlot = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // display
            // 
            this.display.BackColor = System.Drawing.Color.White;
            this.display.BackgroundColorBot = System.Drawing.Color.White;//FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonCreateNetwork);
            this.groupBox1.Controls.Add(this.radioHyperbolic);
            this.groupBox1.Controls.Add(this.radioSigmoid);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 406);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(163, 111);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Network structure";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(47, 49);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "1;2;1";
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
            // buttonRandomize
            // 
            this.buttonRandomize.Location = new System.Drawing.Point(9, 50);
            this.buttonRandomize.Name = "buttonRandomize";
            this.buttonRandomize.Size = new System.Drawing.Size(109, 23);
            this.buttonRandomize.TabIndex = 4;
            this.buttonRandomize.Text = "Randomize weights";
            this.buttonRandomize.UseVisualStyleBackColor = true;
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonGetWeights);
            this.groupBox2.Controls.Add(this.buttonSetWeights);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.textSeed);
            this.groupBox2.Controls.Add(this.buttonRandomize);
            this.groupBox2.Location = new System.Drawing.Point(181, 406);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(174, 144);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Weights";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(9, 79);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(156, 20);
            this.textBox2.TabIndex = 7;
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
            // buttonGetWeights
            // 
            this.buttonGetWeights.Location = new System.Drawing.Point(9, 105);
            this.buttonGetWeights.Name = "buttonGetWeights";
            this.buttonGetWeights.Size = new System.Drawing.Size(75, 23);
            this.buttonGetWeights.TabIndex = 9;
            this.buttonGetWeights.Text = "Get weights";
            this.buttonGetWeights.UseVisualStyleBackColor = true;
            // 
            // buttonCreateNetwork
            // 
            this.buttonCreateNetwork.Location = new System.Drawing.Point(9, 76);
            this.buttonCreateNetwork.Name = "buttonCreateNetwork";
            this.buttonCreateNetwork.Size = new System.Drawing.Size(138, 23);
            this.buttonCreateNetwork.TabIndex = 4;
            this.buttonCreateNetwork.Text = "Create network";
            this.buttonCreateNetwork.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numericUpDown1);
            this.groupBox3.Controls.Add(this.textBox3);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.textDeltaStop);
            this.groupBox3.Controls.Add(this.textErrorStop);
            this.groupBox3.Controls.Add(this.textLambda);
            this.groupBox3.Controls.Add(this.textAlpha);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.buttonTrain);
            this.groupBox3.Location = new System.Drawing.Point(480, 406);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(183, 171);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Training";
            // 
            // buttonTrain
            // 
            this.buttonTrain.Location = new System.Drawing.Point(9, 121);
            this.buttonTrain.Name = "buttonTrain";
            this.buttonTrain.Size = new System.Drawing.Size(125, 23);
            this.buttonTrain.TabIndex = 0;
            this.buttonTrain.Text = "Train prediction";
            this.buttonTrain.UseVisualStyleBackColor = true;
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Lambda";
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Stop on delta";
            // 
            // textAlpha
            // 
            this.textAlpha.Location = new System.Drawing.Point(80, 19);
            this.textAlpha.Name = "textAlpha";
            this.textAlpha.Size = new System.Drawing.Size(51, 20);
            this.textAlpha.TabIndex = 5;
            this.textAlpha.Text = "1";
            // 
            // textLambda
            // 
            this.textLambda.Location = new System.Drawing.Point(80, 43);
            this.textLambda.Name = "textLambda";
            this.textLambda.Size = new System.Drawing.Size(51, 20);
            this.textLambda.TabIndex = 6;
            this.textLambda.Text = "0.0001";
            // 
            // textDeltaStop
            // 
            this.textDeltaStop.Location = new System.Drawing.Point(80, 95);
            this.textDeltaStop.Name = "textDeltaStop";
            this.textDeltaStop.Size = new System.Drawing.Size(51, 20);
            this.textDeltaStop.TabIndex = 8;
            this.textDeltaStop.Text = "0.0001";
            // 
            // textErrorStop
            // 
            this.textErrorStop.Location = new System.Drawing.Point(80, 72);
            this.textErrorStop.Name = "textErrorStop";
            this.textErrorStop.Size = new System.Drawing.Size(51, 20);
            this.textErrorStop.TabIndex = 7;
            this.textErrorStop.Text = "1";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonPlot);
            this.groupBox4.Controls.Add(this.buttonNetworkColor);
            this.groupBox4.Controls.Add(this.buttonTrainColor);
            this.groupBox4.Controls.Add(this.checkNetwork);
            this.groupBox4.Controls.Add(this.checkTest);
            this.groupBox4.Controls.Add(this.checkTrain);
            this.groupBox4.Location = new System.Drawing.Point(669, 406);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 128);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Plotting";
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
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(140, 124);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(37, 20);
            this.numericUpDown1.TabIndex = 9;
            this.numericUpDown1.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.buttonLoadData);
            this.groupBox5.Location = new System.Drawing.Point(361, 406);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(113, 100);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Data";
            // 
            // buttonLoadData
            // 
            this.buttonLoadData.Location = new System.Drawing.Point(6, 24);
            this.buttonLoadData.Name = "buttonLoadData";
            this.buttonLoadData.Size = new System.Drawing.Size(75, 23);
            this.buttonLoadData.TabIndex = 0;
            this.buttonLoadData.Text = "From file";
            this.buttonLoadData.UseVisualStyleBackColor = true;
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
            // buttonTrainColor
            // 
            this.buttonTrainColor.Location = new System.Drawing.Point(98, 20);
            this.buttonTrainColor.Name = "buttonTrainColor";
            this.buttonTrainColor.Size = new System.Drawing.Size(75, 23);
            this.buttonTrainColor.TabIndex = 3;
            this.buttonTrainColor.Text = "Color...";
            this.buttonTrainColor.UseVisualStyleBackColor = true;
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
            // buttonPlot
            // 
            this.buttonPlot.Location = new System.Drawing.Point(6, 94);
            this.buttonPlot.Name = "buttonPlot";
            this.buttonPlot.Size = new System.Drawing.Size(75, 23);
            this.buttonPlot.TabIndex = 5;
            this.buttonPlot.Text = "Plot";
            this.buttonPlot.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 662);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.display);
            this.Name = "MainForm";
            this.Text = "Neuron Network";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioHyperbolic;
        private System.Windows.Forms.RadioButton radioSigmoid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonCreateNetwork;
        private System.Windows.Forms.Button buttonRandomize;
        private System.Windows.Forms.TextBox textSeed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonGetWeights;
        private System.Windows.Forms.Button buttonSetWeights;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
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
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonNetworkColor;
        private System.Windows.Forms.Button buttonTrainColor;
        private System.Windows.Forms.CheckBox checkNetwork;
        private System.Windows.Forms.CheckBox checkTest;
        private System.Windows.Forms.CheckBox checkTrain;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button buttonLoadData;
        private System.Windows.Forms.ColorDialog colorTrain;
        private System.Windows.Forms.ColorDialog colorNetwork;
        private System.Windows.Forms.Button buttonPlot;
    }
}

