namespace LTDT_Do_an_cuoi_ky
{
    public class MyPanel :System.Windows.Forms.Panel
    {
        public MyPanel()
        {
            this.DoubleBuffered = true;
        }
    }

    public class MyListView : System.Windows.Forms.ListView
    {
        public MyListView()
        {
            this.DoubleBuffered = true;
        }
    }

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
            this.components = new System.ComponentModel.Container();
            this.openButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.resetButton = new System.Windows.Forms.Button();
            this.primButton = new System.Windows.Forms.Button();
            this.findConnectedbButton = new System.Windows.Forms.Button();
            this.kruskalButton = new System.Windows.Forms.Button();
            this.thongbao = new System.Windows.Forms.Label();
            this.result = new System.Windows.Forms.Label();
            this.beginVertexBox = new System.Windows.Forms.ComboBox();
            this.endVertexBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dijstraButton = new System.Windows.Forms.Button();
            this.algoName = new System.Windows.Forms.Label();
            this.eulerButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.TPLT = new System.Windows.Forms.Label();
            this.TPLT2 = new System.Windows.Forms.Label();
            this.L = new System.Windows.Forms.Label();
            this.pauseButton = new System.Windows.Forms.Button();
            this.listView1 = new LTDT_Do_an_cuoi_ky.MyListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new LTDT_Do_an_cuoi_ky.MyPanel();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // openButton
            // 
            this.openButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openButton.ForeColor = System.Drawing.Color.Blue;
            this.openButton.Location = new System.Drawing.Point(12, 12);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(132, 29);
            this.openButton.TabIndex = 1;
            this.openButton.Text = "Open";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // resetButton
            // 
            this.resetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetButton.ForeColor = System.Drawing.Color.Red;
            this.resetButton.Location = new System.Drawing.Point(156, 12);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(132, 29);
            this.resetButton.TabIndex = 0;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // primButton
            // 
            this.primButton.AutoSize = true;
            this.primButton.Location = new System.Drawing.Point(75, 84);
            this.primButton.Name = "primButton";
            this.primButton.Size = new System.Drawing.Size(149, 31);
            this.primButton.TabIndex = 3;
            this.primButton.Text = "Prim Algorithm";
            this.primButton.UseVisualStyleBackColor = true;
            this.primButton.Click += new System.EventHandler(this.primButton_Click);
            // 
            // findConnectedbButton
            // 
            this.findConnectedbButton.AutoSize = true;
            this.findConnectedbButton.Location = new System.Drawing.Point(75, 47);
            this.findConnectedbButton.Name = "findConnectedbButton";
            this.findConnectedbButton.Size = new System.Drawing.Size(149, 31);
            this.findConnectedbButton.TabIndex = 4;
            this.findConnectedbButton.Text = "Find Connected Component";
            this.findConnectedbButton.UseVisualStyleBackColor = true;
            this.findConnectedbButton.Click += new System.EventHandler(this.findConnectedbButton_Click);
            // 
            // kruskalButton
            // 
            this.kruskalButton.AutoSize = true;
            this.kruskalButton.Location = new System.Drawing.Point(75, 121);
            this.kruskalButton.Name = "kruskalButton";
            this.kruskalButton.Size = new System.Drawing.Size(149, 31);
            this.kruskalButton.TabIndex = 5;
            this.kruskalButton.Text = "Kruskal Algorithm";
            this.kruskalButton.UseVisualStyleBackColor = true;
            this.kruskalButton.Click += new System.EventHandler(this.kruskalButton_Click);
            // 
            // thongbao
            // 
            this.thongbao.AutoSize = true;
            this.thongbao.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.thongbao.ForeColor = System.Drawing.Color.Blue;
            this.thongbao.Location = new System.Drawing.Point(410, 9);
            this.thongbao.Name = "thongbao";
            this.thongbao.Size = new System.Drawing.Size(0, 22);
            this.thongbao.TabIndex = 0;
            // 
            // result
            // 
            this.result.AutoSize = true;
            this.result.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.result.ForeColor = System.Drawing.Color.Red;
            this.result.Location = new System.Drawing.Point(719, 9);
            this.result.Name = "result";
            this.result.Size = new System.Drawing.Size(0, 22);
            this.result.TabIndex = 6;
            // 
            // beginVertexBox
            // 
            this.beginVertexBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.beginVertexBox.FormattingEnabled = true;
            this.beginVertexBox.Location = new System.Drawing.Point(84, 254);
            this.beginVertexBox.Name = "beginVertexBox";
            this.beginVertexBox.Size = new System.Drawing.Size(42, 21);
            this.beginVertexBox.TabIndex = 7;
            // 
            // endVertexBox
            // 
            this.endVertexBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.endVertexBox.FormattingEnabled = true;
            this.endVertexBox.Location = new System.Drawing.Point(84, 287);
            this.endVertexBox.Name = "endVertexBox";
            this.endVertexBox.Size = new System.Drawing.Size(42, 21);
            this.endVertexBox.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 255);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Begin Vertex";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(2, 288);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "End Vertex";
            // 
            // dijstraButton
            // 
            this.dijstraButton.AutoSize = true;
            this.dijstraButton.Location = new System.Drawing.Point(75, 158);
            this.dijstraButton.Name = "dijstraButton";
            this.dijstraButton.Size = new System.Drawing.Size(149, 31);
            this.dijstraButton.TabIndex = 11;
            this.dijstraButton.Text = "Dijkstra Algorithm";
            this.dijstraButton.UseVisualStyleBackColor = true;
            this.dijstraButton.Click += new System.EventHandler(this.dijstraButton_Click);
            // 
            // algoName
            // 
            this.algoName.AutoSize = true;
            this.algoName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.algoName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.algoName.Location = new System.Drawing.Point(517, 669);
            this.algoName.Name = "algoName";
            this.algoName.Size = new System.Drawing.Size(0, 25);
            this.algoName.TabIndex = 12;
            // 
            // eulerButton
            // 
            this.eulerButton.AutoSize = true;
            this.eulerButton.Location = new System.Drawing.Point(75, 196);
            this.eulerButton.Name = "eulerButton";
            this.eulerButton.Size = new System.Drawing.Size(149, 31);
            this.eulerButton.TabIndex = 13;
            this.eulerButton.Text = "Find Euler Circuit";
            this.eulerButton.UseVisualStyleBackColor = true;
            this.eulerButton.Click += new System.EventHandler(this.eulerButton_Click);
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(117, 631);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 35);
            this.button1.TabIndex = 14;
            this.button1.Text = "About";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TPLT
            // 
            this.TPLT.AutoSize = true;
            this.TPLT.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TPLT.Location = new System.Drawing.Point(12, 331);
            this.TPLT.Name = "TPLT";
            this.TPLT.Size = new System.Drawing.Size(0, 22);
            this.TPLT.TabIndex = 16;
            // 
            // TPLT2
            // 
            this.TPLT2.AutoSize = true;
            this.TPLT2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TPLT2.Location = new System.Drawing.Point(72, 331);
            this.TPLT2.Name = "TPLT2";
            this.TPLT2.Size = new System.Drawing.Size(0, 22);
            this.TPLT2.TabIndex = 17;
            // 
            // L
            // 
            this.L.AutoSize = true;
            this.L.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L.Location = new System.Drawing.Point(9, 553);
            this.L.Name = "L";
            this.L.Size = new System.Drawing.Size(0, 15);
            this.L.TabIndex = 18;
            // 
            // pauseButton
            // 
            this.pauseButton.AutoSize = true;
            this.pauseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pauseButton.Location = new System.Drawing.Point(217, 279);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(71, 32);
            this.pauseButton.TabIndex = 19;
            this.pauseButton.Text = "Pause";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.Location = new System.Drawing.Point(52, 369);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(200, 163);
            this.listView1.TabIndex = 15;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Conected Component";
            this.columnHeader1.Width = 115;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Vertex List";
            this.columnHeader2.Width = 126;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(310, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1034, 632);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint_1);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown_1);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove_1);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp_1);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(217, 255);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(71, 20);
            this.numericUpDown1.TabIndex = 20;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(134, 255);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 15);
            this.label3.TabIndex = 21;
            this.label3.Text = "Time Interval";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1362, 741);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.L);
            this.Controls.Add(this.TPLT2);
            this.Controls.Add(this.TPLT);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.eulerButton);
            this.Controls.Add(this.algoName);
            this.Controls.Add(this.dijstraButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.endVertexBox);
            this.Controls.Add(this.beginVertexBox);
            this.Controls.Add(this.result);
            this.Controls.Add(this.thongbao);
            this.Controls.Add(this.kruskalButton);
            this.Controls.Add(this.findConnectedbButton);
            this.Controls.Add(this.primButton);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.openButton);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Advance Graph Theory Project";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LTDT_Do_an_cuoi_ky.MyPanel panel1;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button primButton;
        private System.Windows.Forms.Button findConnectedbButton;
        private System.Windows.Forms.Button kruskalButton;
        private System.Windows.Forms.Label thongbao;
        private System.Windows.Forms.Label result;
        private System.Windows.Forms.ComboBox beginVertexBox;
        private System.Windows.Forms.ComboBox endVertexBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button dijstraButton;
        private System.Windows.Forms.Label algoName;
        private System.Windows.Forms.Button eulerButton;
        private System.Windows.Forms.Button button1;
        private LTDT_Do_an_cuoi_ky.MyListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label TPLT;
        private System.Windows.Forms.Label TPLT2;
        private System.Windows.Forms.Label L;
        private System.Windows.Forms.Button pauseButton;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label3;
    }
}

