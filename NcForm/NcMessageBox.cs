using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NcForms
{

#warning NcMessageBox classe speciale.
#warning Costruttore privato
#warning Usare alcune funzioni public static DialogResult Show(...)
#warning Passare come argomenti: string? text, string? caption, MessageBoxButtons buttons, NcFormColor color
#warning Lo stile NcFormStyle style è sempre NcForm.Fixed (senza altre aggiunte, nessun Help...)
#warning Prevedere una text box readonly multilinea con barre di scroll.
#warning Aggiungere classi statiche per colore standard, avviso, errore (colore barre)
	public class NcMessageBox:NcForms.NcForm
	{
		const string STRING_EMPTY = "";
		
		private NcMessageBox(NcFormStyle style,NcFormColor color) : base(style,color)
		{
			InitializeComponent();
		}
		private NcMessageBox() : this(NcFormStyle.Fixed,NcFormColor.Normal){}
		private NcMessageBox(NcForms.NcForm ncf) : this(ncf.NcStyle,ncf.NcColor){}

		public static DialogResult Show(NcForm? ncf,string text,string caption = STRING_EMPTY,MessageBoxButtons buttons = MessageBoxButtons.OK)
		{
			DialogResult res = DialogResult.Cancel;

			NcFormStyle ncFs = new NcFormStyle(NcWindowsStyles.None,NcFormWindowStates.Normal);
			NcFormColor ncFc = (ncf != null) ? new NcFormColor(ncf.BackgroundColor,ncf.TitleColor, ncf.StatusBarColor, ncf.Opacity) : NcFormColor.Normal;
			NcMessageBox mb = new NcMessageBox(ncFs,ncFc);
			
			mb.SetText(text,"About");

			#if false
			Button bt1 = new Button();
			bt1.Location = new Point(0,mb.BarHeight);
			bt1.Name = "bt1";
			bt1.Size = new Size(26,27);
			bt1.TabIndex = 1;
			bt1.Text = "OK";
			bt1.UseVisualStyleBackColor = true;
			//bt1.Click += bt1_Click;
			mb.Controls.Add(bt1);
			#endif

			mb.ShowDialog();
			return res;
		}

		private void InitializeComponent()
		{
			button1 = new Button();
			textBox1 = new TextBox();
			SuspendLayout();
			// 
			// button1
			// 
			button1.Location = new Point(404,292);
			button1.Name = "button1";
			button1.Size = new Size(75,23);
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
			textBox1.Size = new Size(491,261);
			textBox1.TabIndex = 4;
			// 
			// NcMessageBox
			// 
			ClientSize = new Size(491,343);
			Controls.Add(textBox1);
			Controls.Add(button1);
			Name = "NcMessageBox";
			Controls.SetChildIndex(button1,0);
			Controls.SetChildIndex(textBox1,0);
			ResumeLayout(false);
			PerformLayout();
		}

		private void bt1_Click(object sender,EventArgs e)
		{
			Close();
		}

		private void SetText(string text, string caption = "Help", string statusText = STRING_EMPTY)
		{
			textBox1.Text = text;
			Title = caption;
			StatusText = statusText;
			AskClose = false;
			TopMost = true;


		}

		
		private Button button1;
		private TextBox textBox1;
	}
	
}
