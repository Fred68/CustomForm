using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace NcForms
{
	[Flags]
	public enum NcWindowsStyles
	{
		None		= 0,
		Menu		= 1 << 0,
		MinMax		= 1 << 1,
		Help		= 1 << 2,
		Resizable	= 1 << 3,
		All			= -1
	}

	public enum NcFormWindowState
	{
		/// <summary>
		///  A default sized window.
		/// </summary>
		Normal = FormWindowState.Normal,
		/// <summary>
		///  A minimized window.
		/// </summary>
		Minimized = FormWindowState.Minimized,

		/// <summary>
		///  A maximized window.
		/// </summary>
		Maximized = FormWindowState.Maximized,

		/// <summary>
		/// Title bar only window.
		/// </summary>
		BarOnly = 3
	}


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

		bool hasMenu;
		bool hasMinMax;	
		bool hasHelp;
		bool isResizable;	

		NcFormWindowState ncWindowsState, prevNcWindowsState;

		protected NcFormWindowState NcWindowsState
		{
			get	{return ncWindowsState;}
			set
			{
				switch(value)
				{
					case NcFormWindowState.Normal:
						this.WindowState = FormWindowState.Normal;
						this.prevNcWindowsState = ncWindowsState;
						if(this.prevNcWindowsState == NcFormWindowState.BarOnly)
						{
							this.Size = prevSz;
							tsStat.Visible = true;
							tsMin.Visible = tsMax.Visible = true;
							this.ncWindowsState = NcFormWindowState.Normal;
						}
						else if(this.prevNcWindowsState == NcFormWindowState.Minimized)
						{
							tsStat.Visible = true;
							this.ncWindowsState = NcFormWindowState.Normal;
						}
						else if(this.prevNcWindowsState == NcFormWindowState.Maximized)
						{
							tsStat.Visible = true;
							this.ncWindowsState = NcFormWindowState.Normal;
						}
						tsMax.Text = "+";
						break;

					case NcFormWindowState.Minimized:
						this.WindowState = FormWindowState.Minimized;
						this.prevNcWindowsState = ncWindowsState;
						this.ncWindowsState = NcFormWindowState.Minimized;
						break;

					case NcFormWindowState.Maximized:
						this.WindowState = FormWindowState.Maximized;
						tsMax.Text = "-";
						this.prevNcWindowsState = ncWindowsState;
						this.ncWindowsState = NcFormWindowState.Maximized;
						break;

					case NcFormWindowState.BarOnly:
						this.prevNcWindowsState = ncWindowsState;
						if(this.prevNcWindowsState == NcFormWindowState.Normal)
						{
							this.WindowState = FormWindowState.Normal;
							prevSz = this.Size;
							this.ncWindowsState = NcFormWindowState.BarOnly;
							tsStat.Visible = false;
							tsMin.Visible = tsMax.Visible = false;
							this.Size = new Size(Size.Width,ts.Height);
						}
						break;

					default:
						break;
				}

			}

		}

		protected NcForm(NcWindowsStyles style = NcWindowsStyles.All)
		{

			hasMenu		= (style & NcWindowsStyles.Menu) != 0;
			hasMinMax	= (style & NcWindowsStyles.MinMax) != 0;		
			hasHelp		= (style & NcWindowsStyles.Help) != 0;
			isResizable	= (style & NcWindowsStyles.Resizable) != 0;
			
			try
			{
				InitializeComponent();
			}
			catch(Exception ex)
			{
				MessageBox.Show($"Errore {ex.Message}","Errore");
				Environment.Exit(0);
			}
			dragging = resizing = false;
			opacity = 0.7f;
			showTsHelp = true;			// Show Help icon and menu item
			showTsMenu = true;			// Show dropdown menu
			showTsMaxMin = true;		// Show maximize / minimize buttons
			showTsBar = true;			// Show OnlyBar button
			colorTitle = colorStatusBar = colorBackground = Color.White;

			ncWindowsState = prevNcWindowsState = NcFormWindowState.Normal;
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
			// tsStat
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
			Resize += NcForm_Resize;
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
			SetTitleBar();
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
				SetTitleBar(value);
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

#if false
		private bool ShowTsHelp
		{
			get { return showTsHelp; }
			set
			{
				showTsHelp = value;
				tsHelp.Visible = value;
				tsmiHelp.Visible = value;
				SetTitleBar();
			}
		}
		private bool ShowTsMaxMin
		{
			get { return showTsMaxMin; }
			set
			{
				showTsMaxMin = value;
				tsMax.Visible = value;
				tsMin.Visible = value;
				SetTitleBar();
			}
		}
		private bool ShowTsBar
		{
			get { return showTsBar; }
			set
			{
				showTsBar = value;
				tsBar.Visible = value;
				SetTitleBar();
			}
		}
		private bool ShowTsMenu
		{
			get { return showTsMenu; }
			set
			{
				showTsMenu = value;
				tsMenu.Visible = value;
				SetTitleBar();
			}
		}
#endif

		private void SetTitleBar(string? txt = null)
		{
			showTsMenu = hasMenu;				// Usa i flag del costruttore
			showTsHelp = hasHelp;
			showTsMaxMin = hasMinMax;

			// Imposta visibilità
			reszLabel.Visible = isResizable;	// Usa il flag del costruttore
			tsMenu.Visible = showTsMenu;
			tsHelp.Visible = tsmiHelp.Visible = showTsHelp;
			tsMax.Visible = tsMin.Visible = tsBar.Visible = showTsMaxMin;

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
			if((NcWindowsState == NcFormWindowState.Normal) || (NcWindowsState == NcFormWindowState.BarOnly))
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
			if(dragging && ((NcWindowsState == NcFormWindowState.Normal) || (NcWindowsState == NcFormWindowState.BarOnly) ))
			{
				Point dif = Point.Subtract(Cursor.Position,new Size(startCur));
				this.Location = Point.Add(startDrag,new Size(dif));
				Invalidate();
			}
			if(resizing && (NcWindowsState == NcFormWindowState.Normal))
			{
				Point dif = Point.Subtract(Cursor.Position,new Size(startResz));
				Size newSz = Size.Add(startSz,new Size(dif));
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
			if(NcWindowsState == NcFormWindowState.Normal)
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
			SetTitleBar();
		}
		private void tsMin_Click(object sender,EventArgs e)
		{
			NcWindowsState = NcFormWindowState.Minimized;
		}
		private void tsMax_Click(object sender,EventArgs e)
		{
			if(NcWindowsState == NcFormWindowState.Normal)
			{
				NcWindowsState = NcFormWindowState.Maximized;
			}
			else
			{
				NcWindowsState = NcFormWindowState.Normal;
			}
		}
		private void tsBar_Click(object sender,EventArgs e)
		{
			if(NcWindowsState == NcFormWindowState.Normal)
			{
				NcWindowsState = NcFormWindowState.BarOnly;
			}
			else if(NcWindowsState == NcFormWindowState.BarOnly)
			{
				NcWindowsState = NcFormWindowState.Normal;
			}
		}
		// Attivato da restore da minimized
		private void NcForm_Resize(object sender,EventArgs e)
		{
			if(NcWindowsState == NcFormWindowState.Minimized)
			{
				NcWindowsState = NcFormWindowState.Normal;
			}
		}
	}
}
