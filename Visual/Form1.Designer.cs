﻿namespace Visual
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Canvas = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelBlack = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelWhite = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.button2 = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.labelTurn = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // Canvas
            // 
            this.Canvas.AutoSize = true;
            this.Canvas.Location = new System.Drawing.Point(11, 2);
            this.Canvas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(749, 638);
            this.Canvas.TabIndex = 0;
            this.Canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.Canvas.Resize += new System.EventHandler(this.Canvas_Resize);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.labelBlack);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.labelWhite);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.trackBar2);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.trackBar1);
            this.panel1.Controls.Add(this.labelTurn);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(766, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(278, 638);
            this.panel1.TabIndex = 1;
            // 
            // labelBlack
            // 
            this.labelBlack.AutoSize = true;
            this.labelBlack.Location = new System.Drawing.Point(16, 419);
            this.labelBlack.Name = "labelBlack";
            this.labelBlack.Size = new System.Drawing.Size(42, 17);
            this.labelBlack.TabIndex = 10;
            this.labelBlack.Text = "None";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 388);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Black";
            // 
            // labelWhite
            // 
            this.labelWhite.AutoSize = true;
            this.labelWhite.Location = new System.Drawing.Point(16, 357);
            this.labelWhite.Name = "labelWhite";
            this.labelWhite.Size = new System.Drawing.Size(42, 17);
            this.labelWhite.TabIndex = 8;
            this.labelWhite.Text = "None";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 326);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "White";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 233);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Speed";
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(6, 268);
            this.trackBar2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(161, 56);
            this.trackBar2.TabIndex = 5;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(38, 166);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 48);
            this.button2.TabIndex = 4;
            this.button2.Text = "Play/Pause";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(3, 106);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(164, 56);
            this.trackBar1.TabIndex = 3;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // labelTurn
            // 
            this.labelTurn.AutoSize = true;
            this.labelTurn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTurn.Location = new System.Drawing.Point(3, 78);
            this.labelTurn.Name = "labelTurn";
            this.labelTurn.Size = new System.Drawing.Size(42, 17);
            this.labelTurn.TabIndex = 2;
            this.labelTurn.Text = "Turn";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(38, 20);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 42);
            this.button1.TabIndex = 0;
            this.button1.Text = "New Game";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 638);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Canvas);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Canvas;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label labelTurn;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label labelBlack;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelWhite;
        private System.Windows.Forms.Label label2;
    }
}

