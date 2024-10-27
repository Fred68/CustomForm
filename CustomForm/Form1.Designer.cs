namespace CustomForm
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private new void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			toolStrip1 = new ToolStrip();
			toolStripButton1 = new ToolStripButton();
			button1 = new Button();
			label1 = new Label();
			label2 = new Label();
			toolStrip1.SuspendLayout();
			SuspendLayout();
			// 
			// toolStrip1
			// 
			toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButton1 });
			toolStrip1.Location = new Point(0,25);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new Size(480,25);
			toolStrip1.TabIndex = 1;
			toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButton1
			// 
			toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
			toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
			toolStripButton1.ImageTransparentColor = Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new Size(23,22);
			toolStripButton1.Text = "toolStripButton1";
			// 
			// button1
			// 
			button1.Location = new Point(12,68);
			button1.Name = "button1";
			button1.Size = new Size(26,27);
			button1.TabIndex = 2;
			button1.Text = "B";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(12,50);
			label1.Name = "label1";
			label1.Size = new Size(38,15);
			label1.TabIndex = 3;
			label1.Text = "label1";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(105,113);
			label2.Name = "label2";
			label2.Size = new Size(38,15);
			label2.TabIndex = 4;
			label2.Text = "label2";
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F,15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.Control;
			ClientSize = new Size(480,321);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(button1);
			Controls.Add(toolStrip1);
			Name = "Form1";
			Text = "Form1";
			Load += Form1_Load;
			Controls.SetChildIndex(toolStrip1,0);
			Controls.SetChildIndex(button1,0);
			Controls.SetChildIndex(label1,0);
			Controls.SetChildIndex(label2,0);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private ToolStrip toolStrip1;
		private Button button1;
		private ToolStripButton toolStripButton1;
		private Label label1;
		private Label label2;
	}
}
