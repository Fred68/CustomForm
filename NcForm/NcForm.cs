using System.Windows.Forms.VisualStyles;


namespace NcForms
{
	public class NcForm:Form
	{
		public const int tsItemExtraWidth = 4;
		private System.ComponentModel.IContainer components;
		private ToolStripDropDownButton tsMenu;
		private ToolStripButton tsQuit;
		private ToolStripButton tsHelp;
		private ToolStripMenuItem tsmiHelp;
		private ToolStripMenuItem tsmiQuit;
		private ToolStripMenuItem settingsToolStripMenuItem;
		private ToolStripLabel tsTitle;
		private ToolStrip ts;
		private ToolStrip statTs;
		private ToolStripLabel statLabel;
		private ToolStripLabel reszLabel;
		private NcConfig config;

		Point startDrag, startCur, startResz;
		Size startSz;
		bool dragging;
		bool resizing;
		float opacity;
		Size minTitleSz;
		bool showHelp, showMenu;
		Color colorTitle, colorStatusBar, colorBackground;

		protected NcForm()
		{
			try
			{
				InitializeComponent();
			}
			catch(Exception ex)
			{
				MessageBox.Show($"Errore {ex.Message}","Errore");
				Environment.Exit(0);
			}
			//config = new NcConfig();
			dragging = resizing = false;
			opacity = 0.7f;
			showHelp = showMenu = true;
			colorTitle = colorStatusBar = colorBackground = Color.White;
		}

		protected void InitializeComponent()
		{
			ts = new ToolStrip();
			tsMenu = new ToolStripDropDownButton();
			settingsToolStripMenuItem = new ToolStripMenuItem();
			tsmiHelp = new ToolStripMenuItem();
			tsmiQuit = new ToolStripMenuItem();
			tsQuit = new ToolStripButton();
			tsHelp = new ToolStripButton();
			tsTitle = new ToolStripLabel();
			statTs = new ToolStrip();
			statLabel = new ToolStripLabel();
			reszLabel = new ToolStripLabel();
			ts.SuspendLayout();
			statTs.SuspendLayout();
			SuspendLayout();
			// 
			// ts
			// 
			ts.BackColor = SystemColors.Control;
			ts.Items.AddRange(new ToolStripItem[] { tsMenu,tsQuit,tsHelp,tsTitle });
			ts.Location = new Point(0,0);
			ts.Name = "ts";
			ts.Size = new Size(521,25);
			ts.TabIndex = 0;
			ts.Text = "ts";
			ts.MouseDown += ts_MouseDown;
			ts.MouseUp += ts_MouseUp;
			// 
			// tsMenu
			// 
			tsMenu.AccessibleDescription = "";
			tsMenu.AccessibleName = "";
			tsMenu.AutoToolTip = false;
			tsMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			tsMenu.DropDownItems.AddRange(new ToolStripItem[] { settingsToolStripMenuItem,tsmiHelp,tsmiQuit });
			tsMenu.Name = "tsMenu";
			tsMenu.Size = new Size(26,22);
			tsMenu.Text = "v";
			// 
			// settingsToolStripMenuItem
			// 
			settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			settingsToolStripMenuItem.Size = new Size(116,22);
			settingsToolStripMenuItem.Text = "Settings";
			settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
			// 
			// tsmiHelp
			// 
			tsmiHelp.Name = "tsmiHelp";
			tsmiHelp.Size = new Size(116,22);
			tsmiHelp.Text = "Help";
			tsmiHelp.Click += tsmiHelp_Click;
			// 
			// tsmiQuit
			// 
			tsmiQuit.Name = "tsmiQuit";
			tsmiQuit.Size = new Size(116,22);
			tsmiQuit.Text = "Quit";
			tsmiQuit.Click += tsmiQuit_Click;
			// 
			// tsQuit
			// 
			tsQuit.Alignment = ToolStripItemAlignment.Right;
			tsQuit.DisplayStyle = ToolStripItemDisplayStyle.Text;
			tsQuit.Name = "tsQuit";
			tsQuit.Size = new Size(23,22);
			tsQuit.Text = "X";
			tsQuit.ToolTipText = "Close";
			tsQuit.Click += tsHelp_Click;
			// 
			// tsHelp
			// 
			tsHelp.Alignment = ToolStripItemAlignment.Right;
			tsHelp.DisplayStyle = ToolStripItemDisplayStyle.Text;
			tsHelp.Name = "tsHelp";
			tsHelp.RightToLeft = RightToLeft.Yes;
			tsHelp.Size = new Size(23,22);
			tsHelp.Text = "?";
			tsHelp.ToolTipText = "Help";
			// 
			// tsTitle
			// 
			tsTitle.AutoSize = false;
			tsTitle.BackColor = SystemColors.Control;
			tsTitle.DisplayStyle = ToolStripItemDisplayStyle.Text;
			tsTitle.ImageTransparentColor = Color.White;
			tsTitle.Name = "tsTitle";
			tsTitle.Size = new Size(100,22);
			tsTitle.Text = "Title";
			tsTitle.TextImageRelation = TextImageRelation.TextBeforeImage;
			tsTitle.MouseDown += tsTitle_MouseDown;
			tsTitle.MouseUp += tsTitle_MouseUp;
			// 
			// toolStrip1
			// 
			statTs.Dock = DockStyle.Bottom;
			statTs.Items.AddRange(new ToolStripItem[] { statLabel,reszLabel });
			statTs.Location = new Point(0,314);
			statTs.Name = "statTs";
			statTs.Size = new Size(521,25);
			statTs.TabIndex = 2;
			statTs.Text = "statTs";
			// 
			// statLabel
			// 
			statLabel.DisplayStyle = ToolStripItemDisplayStyle.Text;
			statLabel.Name = "statLabel";
			statLabel.Size = new Size(28,22);
			statLabel.Text = "nnn";
			// 
			// reszLabel
			// 
			reszLabel.Alignment = ToolStripItemAlignment.Right;
			reszLabel.BackColor = SystemColors.ActiveCaption;
			reszLabel.Name = "reszLabel";
			reszLabel.Size = new Size(23,22);
			reszLabel.Text = "<>";
			reszLabel.MouseDown += reszLabel_MouseDown;
			reszLabel.MouseEnter += reszLabel_MouseEnter;
			reszLabel.MouseLeave += reszLabel_MouseLeave;
			// 
			// NcForm
			// 
			BackColor = SystemColors.Control;
			ClientSize = new Size(521,339);
			Controls.Add(statTs);
			Controls.Add(ts);
			FormBorderStyle = FormBorderStyle.None;
			Name = "NcForm";
			FormClosing += NcForm_FormClosing;
			ResizeEnd += NcForm_ResizeEnd;
			MouseEnter += eMouseEnter;
			MouseLeave += eMouseLeave;
			MouseUp += NcForm_MouseUp;
			ts.ResumeLayout(false);
			ts.PerformLayout();
			statTs.ResumeLayout(false);
			statTs.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		protected void SetupControls(Control control)
		{
			SetEnterLeaveForControls(control,eMouseEnter,eMouseLeave);
			SetEnterLeaveForSingleControl(ts,eMouseEnter,eMouseLeave,etsMouseEnter,etsMouseLeave);
			SetMouseMoveSingleControl(ts,Mouse_Move);
			SetTitle();
		}
		protected string StatusText
		{
			get { return statLabel.Text; }
			set { statLabel.Text = value; }
		}
		protected string Title
		{
			get { return tsTitle.Text; }
			set
			{
				SetTitle(value);
			}
		}
		protected float FormOpacity
		{
			get { return opacity; }
			set
			{
				opacity = value;
				Opacity = opacity;
			}
		}
		protected bool ShowHelp
		{
			get {return showHelp; }	
			set
			{
				tsHelp.Visible = value;
				tsmiHelp.Visible = value;
				SetTitle();
			}
		}
		protected bool ShowMenu
		{
			get {return showMenu; }	
			set
			{
				tsMenu.Visible = value;
				SetTitle();
			}
		}

		protected Color TitleColor
		{
			get {return colorTitle; }
			set
			{
				colorTitle = value;
				ts.BackColor = colorTitle;
			}
		}
		protected Color StatusBarColor
		{
			get {return colorStatusBar; }
			set
			{
				colorStatusBar = value;
				statTs.BackColor = colorStatusBar;
			}
		}
		protected Color BackgroundColor
		{
			get {return colorBackground; }
			set
			{
				colorBackground = value;
				this.BackColor = colorBackground;
			}
		}
		private void SetTitle(string? txt = null)
		{
			#warning Correggere il centraggio del testo, non è corretto
			if (txt != null)	tsTitle.Text = txt;
			tsTitle.AutoSize = true;
			minTitleSz = tsTitle.Size;
			tsTitle.AutoSize = false;
			int availWidth = this.Width
								- (showMenu ? tsMenu.Width + tsItemExtraWidth : tsItemExtraWidth)
								- (showHelp ? tsHelp.Width + tsItemExtraWidth : tsItemExtraWidth)
								- (tsQuit.Width + tsItemExtraWidth ) ;
			if(availWidth < minTitleSz.Width)
			{		
				tsTitle.Visible = false;
			}
			else
			{
				tsTitle.Visible = true;
				tsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
				tsTitle.Size = new Size(availWidth,minTitleSz.Height);
			}
			//MessageBox.Show($"Min tit sz = {tsTitle.Size.Width}");
		}
		private void SetEnterLeaveForControls(Control control,EventHandler eVmouseEnter,EventHandler eVmouseLeave)
		{
			foreach(Control childControl in control.Controls)
			{
				childControl.MouseEnter += eVmouseEnter;
				childControl.MouseLeave += eVmouseLeave;
				SetEnterLeaveForControls(childControl,eVmouseEnter,eVmouseLeave);
			}

		}
		private void SetEnterLeaveForSingleControl(Control control,EventHandler oldVmouseEnter,EventHandler oldVmouseLeave,EventHandler eVmouseEnter,EventHandler eVmouseLeave)
		{
			control.MouseEnter -= oldVmouseEnter;
			control.MouseLeave -= oldVmouseLeave;
			control.MouseEnter += eVmouseEnter;
			control.MouseLeave += eVmouseLeave;
		}
		private void SetMouseMoveSingleControl(Control control,MouseEventHandler eVmouseMove)
		{
			this.MouseMove += eVmouseMove;
			//foreach(Control childControl in control.Controls)
			//{
			//	childControl.MouseMove += eVmouseMove;
			//	ImpostaMouseMovePerControlli(childControl,eVmouseMove);
			//}

		}
		private void tsHelp_Click(object sender,EventArgs e)
		{
			//statLabel.Text = "Quit";
			Close();
		}
		private void tsmiHelp_Click(object sender,EventArgs e)
		{
			statLabel.Text = "Help";
		}
		private void tsmiQuit_Click(object sender,EventArgs e)
		{
			Close();
		}
		private void settingsToolStripMenuItem_Click(object sender,EventArgs e)
		{

#warning DA COMPLETARE
			//config.ShowDialog();

		}
		private void NcForm_FormClosing(object sender,FormClosingEventArgs e)
		{
			if(MessageBox.Show("Quit","Quit ?",MessageBoxButtons.YesNo) != DialogResult.Yes)
			{
				e.Cancel = true;
			}
		}
		private void eMouseEnter(object sender,EventArgs e)
		{
			Opacity = 1f;
		}
		private void eMouseLeave(object sender,EventArgs e)
		{
			Opacity = FormOpacity;
		}
		private void etsMouseEnter(object sender,EventArgs e)
		{
			eMouseEnter(sender,e);
			this.Cursor = Cursors.Cross;
			dragging = false;   // Se esce, non cattura il MouseUp. Al rientro deve interrompere il dragging
		}
		private void etsMouseLeave(object sender,EventArgs e)
		{
			eMouseLeave(sender,e);
			this.Cursor = Cursors.Default;
		}
		private void ts_MouseDown(object sender,MouseEventArgs e)
		{
			Mouse_Down(sender,e);

		}
		private void ts_MouseUp(object sender,MouseEventArgs e)
		{
			Mouse_Up(sender,e);
		}
		private void tsTitle_MouseUp(object sender,MouseEventArgs e)
		{
			Mouse_Up(sender,e);
		}
		private void tsTitle_MouseDown(object sender,MouseEventArgs e)
		{
			Mouse_Down(sender,e);
		}
		private void Mouse_Down(object sender,MouseEventArgs e)
		{
			startDrag = this.Location;
			startCur = Cursor.Position;
			dragging = true;
			this.Capture = true;
			//SetTextStatus($"MouseDown {e.Location.ToString()}");
		}
		private void Mouse_Up(object sender,MouseEventArgs e)
		{
			if(dragging)
			{
				dragging = false;
			}
			if(resizing)
			{
				resizing = false;
			}
			this.Capture = false;
			//SetTextStatus($"MouseUp {e.Location.ToString()}");
		}
		private void Mouse_Move(object sender,MouseEventArgs e)
		{
			if(dragging)
			{
				Point dif = Point.Subtract(Cursor.Position,new Size(startCur));
				this.Location = Point.Add(startDrag,new Size(dif));
				Invalidate();
			}
			if(resizing)
			{
				Point dif = Point.Subtract(Cursor.Position,new Size(startResz));
				Size newSz = Size.Add(startSz,new Size(dif));
#warning CONTROLLARE SIZE MINIMA (DOPO SIZING, RIDIMENSIONARE IL TITOLO)
				this.Size = newSz;
				Invalidate();
			}

		}
		private void reszLabel_MouseEnter(object sender,EventArgs e)
		{
			Cursor = Cursors.SizeNWSE;
			//statLabel.Text = "Enter";
		}
		private void reszLabel_MouseLeave(object sender,EventArgs e)
		{
			Cursor = Cursors.Default;
		}
		private void reszLabel_MouseDown(object sender,MouseEventArgs e)
		{
			resizing = true;
			startResz = Cursor.Position;
			startSz = this.Size;
			this.Capture = true;
		}
		// Con this.Capture, gli eventi del mouse sono catturati dal form, non da reszLabel_MouseUp(...)
		private void NcForm_MouseUp(object sender,MouseEventArgs e)
		{
			if(resizing)
			{
				this.Capture = false;
				resizing = false;
				NcForm_ResizeEnd(sender, e);
			}
		}

		private void NcForm_ResizeEnd(object sender,EventArgs e)
		{
			SetTitle();
		}
	}
}
