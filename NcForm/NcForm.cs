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
		private ToolStrip tsStat;
		private ToolStripLabel statLabel;
		private ToolStripLabel reszLabel;
		private NcConfig config;

		Point startDrag, startCur, startResz;
		Size startSz, prevSz;
		bool dragging;
		bool resizing;
		float opacity;
		Size minTitleSz;
		bool showTsHelp, showTsMenu, showTsMaxMin, showTsBar;
		Color colorTitle, colorStatusBar, colorBackground;
		private ToolStripButton tsMin;
		private ToolStripButton tsMax;
		private ToolStripButton tsBar;
		bool isBarOnly, isNormal;

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
			showTsHelp = showTsMenu = showTsMaxMin = showTsBar = true;
			colorTitle = colorStatusBar = colorBackground = Color.White;
			CheckNormalWindowsState();
			isBarOnly = false;
		}

		protected void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NcForm));
			ts = new ToolStrip();
			tsMenu = new ToolStripDropDownButton();
			settingsToolStripMenuItem = new ToolStripMenuItem();
			tsmiHelp = new ToolStripMenuItem();
			tsmiQuit = new ToolStripMenuItem();
			tsTitle = new ToolStripLabel();
			tsQuit = new ToolStripButton();
			tsHelp = new ToolStripButton();
			tsMax = new ToolStripButton();
			tsMin = new ToolStripButton();
			tsBar = new ToolStripButton();
			tsStat = new ToolStrip();
			statLabel = new ToolStripLabel();
			reszLabel = new ToolStripLabel();
			ts.SuspendLayout();
			tsStat.SuspendLayout();
			SuspendLayout();
			// 
			// ts
			// 
			ts.BackColor = SystemColors.Control;
			ts.Items.AddRange(new ToolStripItem[] { tsMenu,tsTitle,tsQuit,tsHelp,tsMax,tsMin,tsBar });
			ts.Location = new Point(0,0);
			ts.Name = "ts";
			ts.Size = new Size(679,25);
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
			tsHelp.RightToLeft = RightToLeft.No;
			tsHelp.Size = new Size(23,22);
			tsHelp.Text = "?";
			tsHelp.ToolTipText = "Help";
			// 
			// tsMax
			// 
			tsMax.Alignment = ToolStripItemAlignment.Right;
			tsMax.DisplayStyle = ToolStripItemDisplayStyle.Text;
			tsMax.Image = (Image)resources.GetObject("tsMax.Image");
			tsMax.ImageTransparentColor = Color.Magenta;
			tsMax.Name = "tsMax";
			tsMax.RightToLeft = RightToLeft.Yes;
			tsMax.Size = new Size(23,22);
			tsMax.Text = "+";
			tsMax.ToolTipText = "Maximize";
			tsMax.Click += tsMax_Click;
			// 
			// tsMin
			// 
			tsMin.Alignment = ToolStripItemAlignment.Right;
			tsMin.DisplayStyle = ToolStripItemDisplayStyle.Text;
			tsMin.Image = (Image)resources.GetObject("tsMin.Image");
			tsMin.ImageTransparentColor = Color.Magenta;
			tsMin.Name = "tsMin";
			tsMin.RightToLeft = RightToLeft.Yes;
			tsMin.Size = new Size(23,22);
			tsMin.Text = "_";
			tsMin.ToolTipText = "Minimize";
			tsMin.Click += tsMin_Click;
			// 
			// tsBar
			// 
			tsBar.Alignment = ToolStripItemAlignment.Right;
			tsBar.DisplayStyle = ToolStripItemDisplayStyle.Text;
			tsBar.Image = (Image)resources.GetObject("tsBar.Image");
			tsBar.ImageTransparentColor = Color.Magenta;
			tsBar.Name = "tsBar";
			tsBar.RightToLeft = RightToLeft.Yes;
			tsBar.Size = new Size(23,22);
			tsBar.Text = "=";
			tsBar.ToolTipText = "BarOnly";
			tsBar.Click += tsBar_Click;
			// 
			// statTs
			// 
			tsStat.Dock = DockStyle.Bottom;
			tsStat.Items.AddRange(new ToolStripItem[] { statLabel,reszLabel });
			tsStat.Location = new Point(0,349);
			tsStat.Name = "tsStat";
			tsStat.Size = new Size(679,25);
			tsStat.TabIndex = 2;
			tsStat.Text = "tsStat";
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
			ClientSize = new Size(679,374);
			Controls.Add(tsStat);
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
			tsStat.ResumeLayout(false);
			tsStat.PerformLayout();
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
		protected bool ShowTsHelp
		{
			get { return showTsHelp; }
			set
			{
				showTsHelp = value;
				tsHelp.Visible = value;
				tsmiHelp.Visible = value;
				SetTitle();
			}
		}
		protected bool ShowTsMaxMin
		{
			get { return showTsMaxMin; }
			set
			{
				showTsMaxMin = value;
				tsMax.Visible = value;
				tsMin.Visible = value;
				SetTitle();
			}
		}
		protected bool ShowTsBar
		{
			get { return showTsBar; }
			set
			{
				showTsBar = value;
				tsBar.Visible = value;
				SetTitle();
			}
		}
		protected bool ShowTsMenu
		{
			get { return showTsMenu; }
			set
			{
				showTsMenu = value;
				tsMenu.Visible = value;
				SetTitle();
			}
		}

		protected Color TitleColor
		{
			get { return colorTitle; }
			set
			{
				colorTitle = value;
				ts.BackColor = colorTitle;
			}
		}
		protected Color StatusBarColor
		{
			get { return colorStatusBar; }
			set
			{
				colorStatusBar = value;
				tsStat.BackColor = colorStatusBar;
			}
		}
		protected Color BackgroundColor
		{
			get { return colorBackground; }
			set
			{
				colorBackground = value;
				this.BackColor = colorBackground;
			}
		}

		private bool CheckNormalWindowsState()
		{
			isNormal = (this.WindowState == FormWindowState.Normal);
			return isNormal;
		}
		private void SetTitle(string? txt = null)
		{
#warning Correggere il centraggio del testo, non è preciso
			if(txt != null) tsTitle.Text = txt;
			tsTitle.AutoSize = true;
			minTitleSz = tsTitle.Size;
			tsTitle.AutoSize = false;
			int availWidth = this.Width
								- (showTsMenu ? tsMenu.Width + tsItemExtraWidth : tsItemExtraWidth)
								- (showTsHelp ? tsHelp.Width + tsItemExtraWidth : tsItemExtraWidth)
								- (showTsMaxMin ? tsMax.Width + tsMin.Width + tsBar.Width + 3 * tsItemExtraWidth : 3 * tsItemExtraWidth)
								- (tsQuit.Width + tsItemExtraWidth);
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
			if(isNormal || isBarOnly)
			{
				startDrag = this.Location;
				startCur = Cursor.Position;
				dragging = true;
				this.Capture = true;
			}
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
			if(dragging && (isNormal || isBarOnly))
			{
				Point dif = Point.Subtract(Cursor.Position,new Size(startCur));
				this.Location = Point.Add(startDrag,new Size(dif));
				Invalidate();
			}
			if(resizing && isNormal)
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
			if(isNormal)
			{
				resizing = true;
				startResz = Cursor.Position;
				startSz = this.Size;
				this.Capture = true;
			}
		}
		// Con this.Capture, gli eventi del mouse sono catturati dal form, non da reszLabel_MouseUp(...)
		private void NcForm_MouseUp(object sender,MouseEventArgs e)
		{
			if(resizing)
			{
				this.Capture = false;
				resizing = false;
				NcForm_ResizeEnd(sender,e);
			}
		}

		private void NcForm_ResizeEnd(object sender,EventArgs e)
		{
			SetTitle();
		}

		private void tsMin_Click(object sender,EventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;
			isNormal = isBarOnly = false;
		}

		private void tsMax_Click(object sender,EventArgs e)
		{
			if(isBarOnly)
			{
				this.Size = prevSz;
				tsStat.Visible = true;
				isBarOnly = false;
				CheckNormalWindowsState();
			}
			this.WindowState = (this.WindowState == FormWindowState.Normal) ? FormWindowState.Maximized : FormWindowState.Normal;
			tsMax.Text = (this.WindowState == FormWindowState.Normal) ? "+" : "-";
			if(!CheckNormalWindowsState())
			{
				isBarOnly = false;
			}
		}

		private void tsBar_Click(object sender,EventArgs e)
		{
			if(!isBarOnly && isNormal)
			{
				prevSz = this.Size;
				isBarOnly = true;
				isNormal = false;
				tsStat.Visible = false;
				this.Size = new Size(Size.Width, ts.Height);
			}
			else
			{
				this.Size = prevSz;
				tsStat.Visible = true;
				isBarOnly = false;
				CheckNormalWindowsState();
			}
		}
	}
}
