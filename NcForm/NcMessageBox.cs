using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NcForms
{

	public class NcMessageBox:NcForms.NcForm
	{

		#region Private memebers
		const string STRING_EMPTY = "";
		const float MB_OPACITY = 1.0f;

		private System.Windows.Forms.Button button1;

		private System.Windows.Forms.Button[] bts;
		private RichTextBox richTextBox1;
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
			NcFormStyle ncFs = new NcFormStyle(NcWindowsStyles.None,NcFormWindowStates.Normal);     // NcWindowsStyles.None: no lower bar
			NcFormColor ncFc = (ncf != null) ? new NcFormColor(ncf.BackColor,ncf.TitleColor,ncf.StatusBarColor,ncf.ButtonsColor,MB_OPACITY) : NcFormColor.Normal;
			using(_mb = new NcMessageBox(ncFs,ncFc,buttons))
			{
				_mb.richTextBox1.BackColor = ncFc.backColor;
				_mb.ButtonsColor = ncFc.buttonsColor;
				_mb.SetText(text,caption);
				res = _mb.ShowDialog();
			}
			return res;
		}

		#region Private functions
		private NcMessageBox(NcFormStyle style,NcFormColor color,MessageBoxButtons buttons = MessageBoxButtons.OK) : base(style,color)
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
			button1 = new System.Windows.Forms.Button();
			richTextBox1 = new RichTextBox();
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
			// richTextBox1
			// 
			richTextBox1.BorderStyle = BorderStyle.None;
			richTextBox1.Dock = DockStyle.Top;
			richTextBox1.Location = new Point(0,25);
			richTextBox1.Name = "richTextBox1";
			richTextBox1.ReadOnly = true;
			richTextBox1.Size = new Size(483,325);
			richTextBox1.TabIndex = 5;
			richTextBox1.Text = "";
			// 
			// NcMessageBox
			// 
			ClientSize = new Size(483,452);
			Controls.Add(richTextBox1);
			Controls.Add(button1);
			Name = "NcMessageBox";
			Shown += NcMessageBox_Shown;
			Controls.SetChildIndex(button1,0);
			Controls.SetChildIndex(richTextBox1,0);
			ResumeLayout(false);
			PerformLayout();
		}

		private void AdjustIfNoLowerBar()
		{
			if((this.NcStyle.ncWindowsStyle & NcWindowsStyles.LowerBar) == 0)   // No lower bar
			{
				SuspendLayout();
				int i = 0;
				foreach(System.Windows.Forms.Button b in bts)
				{
					b.Location = new Point(b.Location.X,b.Location.Y + LowerBarHeight);
					i++;
				}
				richTextBox1.Size = new Size(richTextBox1.Size.Width,richTextBox1.Size.Height + LowerBarHeight);
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

			bts = new System.Windows.Forms.Button[nbuttons];
			Size sz = button1.Size;
			Point lc = button1.Location;

			button1.Visible = false;
			Controls.Remove(button1 as System.Windows.Forms.Button);

			for(int i = 0;i < nbuttons;i++)
			{
				bts[i] = new System.Windows.Forms.Button();
				bts[i].Size = sz;
				bts[i].Location = new Point(lc.X - sz.Width * i,lc.Y);
				bts[i].Visible = true;
				bts[i].BackColor = this.ButtonsColor;
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
			richTextBox1.Text = text;
			Title = caption;
			if(statusText != STRING_EMPTY)
			{
				StatusText = statusText;
			}
			TopMost = true;
			AskClose = false;
		}
		#endregion

		private void NcMessageBox_Shown(object sender,EventArgs e)
		{
			richTextBox1.DeselectAll();
			bts[bts.Length - 1].Select();

			#warning COMPLETARE ridimensionamento NcMessageBoxin base al testo (ridurre soltanto)
			Size sz = GetAdjRtBoxOffset();

			MessageBox.Show($"Delta size= {sz}");
			if((sz.Width !=0) || (sz.Height !=0))
			{
			#warning CORREGGERE LE DIMENSIONI (BORDO MINIMO + BARRA SCORRIMENTO VERTICALE DELLA TEXTBOX)
			#warning CORREGGERE LE DIMENSIONI (larghezza dei tre pulsanti)
			#warning RIDIMENSIONARE ALTEZZA RTBOX
			#warning SPOSTARE O TRE PULSANTI
			

				Size = Size + sz;

			}



			this.CenterToScreen();
		}

		private Size MeasureRtBoxText(RichTextBox rtb)
		{
			Size txtSz = new Size();
			Font fnt = rtb.Font;
			txtSz = TextRenderer.MeasureText(rtb.Text, fnt);
			return txtSz;
		}

		private Size GetAdjRtBoxOffset()
		{
			int dW,dH;
			dW = dH = 0;

			Size txtSz = MeasureRtBoxText(richTextBox1 as RichTextBox);
			Size rtbSz = richTextBox1.Size;
			
			MessageBox.Show($"RichTextBox size= {rtbSz}\n\rText size= {txtSz}");

			if(txtSz.Width < rtbSz.Width)
			{
				dW = txtSz.Width - rtbSz.Width;	
			}
			if(txtSz.Height < rtbSz.Height)
			{
				dH = txtSz.Height - rtbSz.Height;
			}

			return new Size(dW,dH);
		}
	}

}
