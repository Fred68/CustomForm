using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NcForms
{


	/// <summary>
	/// Non Client Area Window base class
	/// </summary>
	public class NcForm:Form
	{
		public const int tsItemExtraWidth = 3;
		public const int contentMinHeight = 10;

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

		Point startDrag, startCur, startResz;
		DateTime lastMouseDownEventTime;
		TimeSpan doubleClickDelay;
		Size startSz, prevSz;
		bool dragging;
		bool resizing;
		float opacity;

		Size minTitleSz;
		bool showTsHelp, showTsMenu, showTsMaxMin, showTsBar, showTsTop;
		Color colorTitle, colorStatusBar, colorBackground;
		int availWidthUpper, availWidthLower;

		private ToolStripButton tsMin;
		private ToolStripButton tsMax;
		private ToolStripButton tsBar;
		private ToolStripButton tsTop;

		bool hasMenu;
		bool hasMinMax;
		bool hasHelp;
		bool isResizable;
		bool hasTopMost;

		NcFormWindowStates ncWindowsState, prevNcWindowsState;

#pragma warning disable CS8618

		/// <summary>
		/// Empty constructor
		/// </summary>
		public NcForm() : this(NcFormStyle.Normal,NcFormColor.Normal) { }

#warning	Aggiungere SizeToContent() !!!


		/// <summary>
		/// Constructor with params.
		/// NcForm potentially null fields are checked by a try...catch
		/// </summary>
		/// <param name="style"></param>		
		public NcForm(NcFormStyle style,NcFormColor color)        // public NcForm(NcWindowsStyles style = NcWindowsStyles.All)
		{
			hasMenu = (style.ncWindowsStyle & NcWindowsStyles.Menu) != 0;
			hasMinMax = (style.ncWindowsStyle & NcWindowsStyles.MinMax) != 0;
			hasHelp = (style.ncWindowsStyle & NcWindowsStyles.Help) != 0;
			isResizable = (style.ncWindowsStyle & NcWindowsStyles.Resizable) != 0;
			hasTopMost = (style.ncWindowsStyle & NcWindowsStyles.TopMost) != 0;

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
			showTsHelp = true;          // Show Help icon and menu item
			showTsMenu = true;          // Show dropdown menu
			showTsMaxMin = true;        // Show maximize / minimize buttons
			showTsBar = true;           // Show OnlyBar button
			showTsTop = true;           // Show Topmost button
			TopMost = hasTopMost;       // Imposta subito lo stato topMost in base al flag

			ncWindowsState = prevNcWindowsState = style.ncFormWindowState;

			TitleColor = color.titleBarColor;
			StatusBarColor = color.statusBarColor;
			BackgroundColor = color.backgroundColor;
			Opacity = color.opacity;
			lastMouseDownEventTime = DateTime.Now;
			DoubleClickDelay = TimeSpan.FromSeconds(0.3);
		}
#pragma warning restore

		/// <summary>
		/// Set/Get WindowsState: Minimized, Maximized, Normal, BarOnly
		/// </summary>
		protected NcFormWindowStates NcWindowsState
		{
			get { return ncWindowsState; }
			set
			{
				switch(value)
				{
					case NcFormWindowStates.Normal:
						this.WindowState = FormWindowState.Normal;
						this.prevNcWindowsState = ncWindowsState;
						if(this.prevNcWindowsState == NcFormWindowStates.BarOnly)
						{
							this.Size = prevSz;
							tsStat.Visible = true;
							tsMin.Visible = tsMax.Visible = true;
							SetTitleBar();
							this.ncWindowsState = NcFormWindowStates.Normal;
						}
						else if(this.prevNcWindowsState == NcFormWindowStates.Minimized)
						{
							tsStat.Visible = true;
							this.ncWindowsState = NcFormWindowStates.Normal;
						}
						else if(this.prevNcWindowsState == NcFormWindowStates.Maximized)
						{
							tsStat.Visible = true;
							this.ncWindowsState = NcFormWindowStates.Normal;
						}
						tsMax.Text = "+";
						break;

					case NcFormWindowStates.Minimized:
						this.WindowState = FormWindowState.Minimized;
						this.prevNcWindowsState = ncWindowsState;
						this.ncWindowsState = NcFormWindowStates.Minimized;
						break;

					case NcFormWindowStates.Maximized:
						this.WindowState = FormWindowState.Maximized;
						tsMax.Text = "-";
						this.prevNcWindowsState = ncWindowsState;
						this.ncWindowsState = NcFormWindowStates.Maximized;
						break;

					case NcFormWindowStates.BarOnly:
						this.prevNcWindowsState = ncWindowsState;
						if(this.prevNcWindowsState == NcFormWindowStates.Normal)
						{
							this.WindowState = FormWindowState.Normal;
							prevSz = this.Size;
							this.ncWindowsState = NcFormWindowStates.BarOnly;
							tsStat.Visible = false;
							tsMin.Visible = tsMax.Visible = false;
							//this.Size = new Size(Size.Width,ts.Height);
							this.Size = new Size(Size.Width - int.Min(availWidthUpper,availWidthLower),ts.Height);
							tsTitle.AutoSize = true;
							//Invalidate();
						}
						break;

					default:
						break;
				}

			}

		}

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NcForm));
			ts = new ToolStrip();
			tsMenu = new ToolStripDropDownButton();
			settingsToolStripMenuItem = new ToolStripMenuItem();
			tsmiHelp = new ToolStripMenuItem();
			tsmiQuit = new ToolStripMenuItem();
			tsTop = new ToolStripButton();
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
			ts.CanOverflow = false;
			ts.Items.AddRange(new ToolStripItem[] { tsMenu,tsTop,tsTitle,tsQuit,tsHelp,tsMax,tsMin,tsBar });
			ts.Location = new Point(0,0);
			ts.Name = "ts";
			ts.Size = new Size(596,25);
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
			tsmiHelp.Click += tsHelp_Click;
			// 
			// tsmiQuit
			// 
			tsmiQuit.Name = "tsmiQuit";
			tsmiQuit.Size = new Size(116,22);
			tsmiQuit.Text = "Quit";
			tsmiQuit.Click += tsmiQuit_Click;
			// 
			// tsTop
			// 
			tsTop.DisplayStyle = ToolStripItemDisplayStyle.Text;
			tsTop.Image = (Image)resources.GetObject("tsTop.Image");
			tsTop.ImageTransparentColor = Color.Magenta;
			tsTop.Name = "tsTop";
			tsTop.Size = new Size(23,22);
			tsTop.Text = "T";
			tsTop.ToolTipText = "OnTop";
			tsTop.Click += tsTop_Click;
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
			tsQuit.Click += tsClose_Click;
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
			tsHelp.Click += tsHelp_Click;
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
			tsStat.CanOverflow = false;
			tsStat.Dock = DockStyle.Bottom;
			tsStat.Items.AddRange(new ToolStripItem[] { statLabel,reszLabel });
			tsStat.Location = new Point(0,351);
			tsStat.Name = "tsStat";
			tsStat.Size = new Size(596,25);
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
			reszLabel.DisplayStyle = ToolStripItemDisplayStyle.Text;
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
			ClientSize = new Size(596,376);
			Controls.Add(tsStat);
			Controls.Add(ts);
			FormBorderStyle = FormBorderStyle.None;
			Name = "NcForm";
			FormClosing += NcForm_FormClosing;
			Load += NcForm_Load;
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

		/// <summary>
		/// Function to be called at the end of the derived class constructor
		/// </summary>
		/// <param name="control"></param>
		protected void SetupControls(Control control)
		{
			SetEnterLeaveForControls(control,eMouseEnter,eMouseLeave);
			SetEnterLeaveForSingleControl(ts,eMouseEnter,eMouseLeave,etsMouseEnter,etsMouseLeave);
			SetMouseMoveSingleControl(ts,Mouse_Move);
			SetTitleBar();

		}
		/// <summary>
		/// Text on the status bar
		/// </summary>
		protected string StatusText
		{
			get { return statLabel.Text; }
			set { statLabel.Text = value; }
		}
		/// <summary>
		/// Title text
		/// </summary>
		protected string Title
		{
			get { return tsTitle.Text; }
			set
			{
				SetTitleBar(value);
			}
		}
		/// <summary>
		/// Opacity
		/// </summary>
		protected new float Opacity
		{
			get { return opacity; }
			set
			{
				opacity = value;
				base.Opacity = opacity;
			}
		}
		/// <summary>
		/// Title bar color
		/// </summary>
		protected Color TitleColor
		{
			get { return colorTitle; }
			set
			{
				colorTitle = value;
				ts.BackColor = colorTitle;
			}
		}
		/// <summary>
		/// Status bar color
		/// </summary>
		protected Color StatusBarColor
		{
			get { return colorStatusBar; }
			set
			{
				colorStatusBar = value;
				tsStat.BackColor = colorStatusBar;
			}
		}
		/// <summary>
		/// Background color
		/// </summary>
		protected Color BackgroundColor
		{
			get { return colorBackground; }
			set
			{
				colorBackground = value;
				this.BackColor = colorBackground;
			}
		}
		/// <summary>
		/// Title height
		/// </summary>
		protected int BarHeight
		{
			get { return minTitleSz.Height + tsItemExtraWidth; }
		}
		protected TimeSpan DoubleClickDelay
		{
			get {return doubleClickDelay;}
			set {doubleClickDelay = value;}
		}
		private void SetTitleBar(string? txt = null)
		{
			showTsMenu = hasMenu;               // Usa i flag del costruttore
			showTsHelp = hasHelp;
			showTsMaxMin = hasMinMax;
			showTsTop = hasTopMost;

			// Imposta visibilità
			reszLabel.Visible = isResizable;    // Usa il flag del costruttore
			tsMenu.Visible = showTsMenu;
			tsHelp.Visible = tsmiHelp.Visible = showTsHelp;
			tsMax.Visible = tsMin.Visible = tsBar.Visible = showTsMaxMin;
			tsTop.Visible = showTsTop;

			if(txt != null) tsTitle.Text = txt;
			tsTitle.AutoSize = true;
			minTitleSz = tsTitle.Size;

			availWidthUpper = this.Width
								- (showTsMenu ? tsMenu.Width + tsItemExtraWidth : tsItemExtraWidth)
								- (showTsHelp ? tsHelp.Width + tsItemExtraWidth : tsItemExtraWidth)
								- (showTsTop ? tsTop.Width + tsItemExtraWidth : tsItemExtraWidth)
								- (showTsMaxMin ? tsMax.Width + tsMin.Width + tsBar.Width + 3 * tsItemExtraWidth : 3 * tsItemExtraWidth)
								- (tsQuit.Width + tsItemExtraWidth);
			availWidthLower = this.Width - statLabel.Width - reszLabel.Width;

			if(availWidthUpper < minTitleSz.Width)
			{
				tsTitle.Visible = false;
			}
			else
			{
				tsTitle.AutoSize = false;
				tsTitle.Visible = true;
				tsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
				tsTitle.Size = new Size(availWidthUpper,minTitleSz.Height);
			}
		}

		private void AdjustWidth()
		{
			bool resize = false;
			Size newsize = this.Size;

			int minWidth = this.Width - int.Min(availWidthUpper,availWidthLower);
			int minHeight = ts.Height + tsStat.Height + contentMinHeight;

			if(Size.Width < minWidth)
			{
				newsize.Width = minWidth;
				resize = true;
			}
			if(Size.Height < minHeight)
			{
				newsize.Height = minHeight;
				resize = true;
			}
			if(resize)
			{
				this.Size = newsize;
			}

		}
		private void SwitchBarOnly()
		{
			if(NcWindowsState == NcFormWindowStates.Normal)
			{
				NcWindowsState = NcFormWindowStates.BarOnly;
			}
			else if(NcWindowsState == NcFormWindowStates.BarOnly)
			{
				NcWindowsState = NcFormWindowStates.Normal;
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
		private void tsClose_Click(object sender,EventArgs e)
		{
			Close();
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
			base.Opacity = 1f;
		}
		private void eMouseLeave(object sender,EventArgs e)
		{
			base.Opacity = Opacity;
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
			DateTime dateTime = DateTime.Now;
			if(dateTime - lastMouseDownEventTime < doubleClickDelay)
			{
				tsBar_DoubleClick();
				return;
			}
			lastMouseDownEventTime = dateTime;
			if((NcWindowsState == NcFormWindowStates.Normal) || (NcWindowsState == NcFormWindowStates.BarOnly))
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
			if(dragging && ((NcWindowsState == NcFormWindowStates.Normal) || (NcWindowsState == NcFormWindowStates.BarOnly)))
			{
				Point dif = Point.Subtract(Cursor.Position,new Size(startCur));
				this.Location = Point.Add(startDrag,new Size(dif));
				Invalidate();
			}
			if(resizing && (NcWindowsState == NcFormWindowStates.Normal))
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
			if(NcWindowsState == NcFormWindowStates.Normal)
			{
				resizing = true;
				startResz = Cursor.Position;
				startSz = this.Size;
				this.Capture = true;
			}
		}
		/// <summary>
		/// With this.Capture mouse events are captured by the form, not by reszLabel_MouseUp(...)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
			AdjustWidth();
		}
		private void tsMin_Click(object sender,EventArgs e)
		{
			NcWindowsState = NcFormWindowStates.Minimized;
		}
		private void tsMax_Click(object sender,EventArgs e)
		{
			if(NcWindowsState == NcFormWindowStates.Normal)
			{
				NcWindowsState = NcFormWindowStates.Maximized;
			}
			else
			{
				NcWindowsState = NcFormWindowStates.Normal;
			}
		}
		private void tsBar_Click(object sender,EventArgs e)
		{
			SwitchBarOnly();
		}
		/// <summary>
		/// Called when the form is restore after having been minimized
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NcForm_Resize(object sender,EventArgs e)
		{
			if(NcWindowsState == NcFormWindowStates.Minimized)
			{
				NcWindowsState = NcFormWindowStates.Normal;
			}
		}
		private void tsTop_Click(object sender,EventArgs e)
		{
			TopMost = !(TopMost);   // Scambia lo stato
			tsTop.Text = TopMost ? "T" : "t";
		}
		private void tsHelp_Click(object sender,EventArgs e)
		{
			MessageBox.Show("Help");
		}
		private void NcForm_Load(object sender,EventArgs e)
		{
			SetupControls(this);
			NcWindowsState = ncWindowsState;
		}
		private void tsBar_DoubleClick()
		{
			SwitchBarOnly();
		}
	}

}
