using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NcForms
{

	public class NcMessageBox:NcForms.NcForm
	{

		#region Private memebers
		const string STRING_EMPTY = "";
		const float MB_OPACITY = 1.0f;

		private Button button1;
		private TextBox textBox1;

		private Button[] bts;
		#endregion

		/// <summary>
		/// Static object for the static funcion: NcMessageBox.Show(...)
		/// Alternative: ThreadLocal<NcMessageBox> _mb;
		/// </summary>
		[ThreadStatic]
		static NcMessageBox? _mb;

		/// <summary>
		/// Show dialog
		/// </summary>
		/// <param name="ncf">Parent NcForm to copy colors from</param>
		/// <param name="text">Content of the message</param>
		/// <param name="caption">Caption on the title bar</param>
		/// <param name="buttons">System.Windows.Forms.MessageBoxButtons argument</param>
		/// <returns></returns>
		public static DialogResult Show(NcForm? ncf,string text,string caption = STRING_EMPTY,MessageBoxButtons buttons = MessageBoxButtons.OK)
		{
			DialogResult res = DialogResult.Cancel;
			NcFormStyle ncFs = new NcFormStyle(NcWindowsStyles.None,NcFormWindowStates.Normal);		// NcWindowsStyles.None: no lower bar
			NcFormColor ncFc = (ncf != null) ? new NcFormColor(ncf.BackColor,ncf.TitleColor,ncf.StatusBarColor,MB_OPACITY) : NcFormColor.Normal;
			using(_mb = new NcMessageBox(ncFs,ncFc,buttons))
			{
				_mb.textBox1.BackColor = ncFc.backColor;
				_mb.SetText(text,caption);
				res = _mb.ShowDialog();
			}
			return res;
		}

		#region Private functions
		private NcMessageBox(NcFormStyle style,NcFormColor color, MessageBoxButtons buttons = MessageBoxButtons.OK) : base(style,color)
		{
			InitializeComponent();
			SetUpButtons(buttons);
			AdjustIfNoLowerBar();
			Invalidate();
		}
		private NcMessageBox() : this(NcFormStyle.Fixed,NcFormColor.Normal) { }
		private NcMessageBox(NcForms.NcForm ncf) : this(ncf.NcStyle,ncf.NcColor) { }
		private NcMessageBox(NcForm ncf,string text,string caption = STRING_EMPTY,MessageBoxButtons buttons = MessageBoxButtons.OK) :
					this(ncf.NcStyle,ncf.NcColor)
		{
			BackColor = ncf.BackColor;
			SetText(text,caption);
		}
		private void InitializeComponent()
		{
			button1 = new Button();
			textBox1 = new TextBox();
			SuspendLayout();
			// 
			// button1
			// 
			button1.Location = new Point(379,356);
			button1.Name = "button1";
			button1.Size = new Size(75,40);
			button1.TabIndex = 3;
			button1.Text = "button1";
			button1.UseVisualStyleBackColor = true;
			// 
			// textBox1
			// 
			textBox1.BorderStyle = BorderStyle.None;
			textBox1.Dock = DockStyle.Top;
			textBox1.Location = new Point(0,25);
			textBox1.Multiline = true;
			textBox1.Name = "textBox1";
			textBox1.ReadOnly = true;
			textBox1.ScrollBars = ScrollBars.Vertical;
			textBox1.Size = new Size(483,325);
			textBox1.TabIndex = 4;
			// 
			// NcMessageBox
			// 
			ClientSize = new Size(483,452);
			Controls.Add(textBox1);
			Controls.Add(button1);
			Name = "NcMessageBox";
			Controls.SetChildIndex(button1,0);
			Controls.SetChildIndex(textBox1,0);
			ResumeLayout(false);
			PerformLayout();
		}
		private void AdjustIfNoLowerBar()
		{
			if((this.NcStyle.ncWindowsStyle & NcWindowsStyles.LowerBar) == 0)	// No lower bar
			{
				SuspendLayout();
				int i=0;
				foreach(Button b in bts)
				{
					b.Location = new Point(b.Location.X,b.Location.Y + LowerBarHeight);
					i++;
				} 
				textBox1.Size = new Size(textBox1.Size.Width,textBox1.Size.Height + LowerBarHeight);

				ResumeLayout(false);
				PerformLayout();
			}

		}
		private void SetUpButtons(MessageBoxButtons buttons)
		{
			int nbuttons = 1;

			switch(buttons)
			{
				case MessageBoxButtons.OK:
					{
					nbuttons = 1;
					}
				break;

				case MessageBoxButtons.OKCancel:
				case MessageBoxButtons.YesNo:
				case MessageBoxButtons.RetryCancel:
					{
					nbuttons = 2;
					}
				break;

				case MessageBoxButtons.YesNoCancel:
				case MessageBoxButtons.AbortRetryIgnore:
				case MessageBoxButtons.CancelTryContinue:
					{
					nbuttons = 3;
					}
				break;

				default:
				break;
			}

			bts = new Button[nbuttons];
			Size sz = button1.Size;
			Point lc = button1.Location;

			button1.Visible = false;
			Controls.Remove(button1 as Button);

			for(int i = 0;i < nbuttons;i++)
			{
				bts[i] = new Button();
				bts[i].Size = sz;
				bts[i].Location = new Point(lc.X - sz.Width * i,lc.Y);
				bts[i].Visible = true;
				Controls.Add(bts[i]);
			}

			switch(buttons)
			{
				case MessageBoxButtons.OK:
				{
					bts[0].DialogResult = DialogResult.OK;
					bts[0].Text = "OK";
				}
				break;
				case MessageBoxButtons.YesNo:
				{
					bts[0].DialogResult = DialogResult.No;
					bts[0].Text = "No";
					bts[1].DialogResult = DialogResult.Yes;
					bts[1].Text = "Yes";

				}
				break;
				case MessageBoxButtons.YesNoCancel:
				{
					bts[0].DialogResult = DialogResult.Cancel;
					bts[0].Text = "Cancel";
					bts[1].DialogResult = DialogResult.No;
					bts[1].Text = "No";
					bts[2].DialogResult = DialogResult.Yes;
					bts[2].Text = "Yes";
				}
				break;
				case MessageBoxButtons.AbortRetryIgnore:
				{
					bts[0].DialogResult = DialogResult.Ignore;
					bts[0].Text = "Ignore";
					bts[1].DialogResult = DialogResult.Retry;
					bts[1].Text = "Retry";
					bts[2].DialogResult = DialogResult.Abort;
					bts[2].Text = "Abort";
				}
				break;
				case MessageBoxButtons.RetryCancel:
				{
					bts[0].DialogResult = DialogResult.Cancel;
					bts[0].Text = "Cancel";
					bts[1].DialogResult = DialogResult.Retry;
					bts[1].Text = "Retry";
				}
				break;
				case MessageBoxButtons.CancelTryContinue:
				{
					bts[0].DialogResult = DialogResult.Continue;
					bts[0].Text = "Continue";
					bts[1].DialogResult = DialogResult.TryAgain;
					bts[1].Text = "TryAgain";
					bts[2].DialogResult = DialogResult.Cancel;
					bts[2].Text = "Cancel";
				}
				break;
				case MessageBoxButtons.OKCancel:
				{
					bts[0].DialogResult = DialogResult.Cancel;
					bts[0].Text = "Cancel";
					bts[1].DialogResult = DialogResult.OK;
					bts[1].Text = "OK";
				}
				break;

			




			}
			Invalidate();
		}
		private void SetText(string text,string caption = "Help",string statusText = STRING_EMPTY)
		{
			textBox1.Text = text;
			Title = caption;
			if(statusText != STRING_EMPTY)
			{
				StatusText = statusText;
			}
			else
			{
				//ts_lo
			}
			AskClose = false;
			TopMost = true;
		}
		#endregion
	}

}
