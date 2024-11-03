using NcForms;
using System.ComponentModel.Design;
using System.Reflection;
//using static System.Net.Mime.MediaTypeNames;

namespace CustomForm
{
    public partial class Form1:NcForms.NcForm
	{

		static string LONG_TEXT = "\r\nNel mezzo del cammin di nostra vita\r\nmi ritrovai per una selva oscura,\r\nché la diritta via era smarrita.\r\n\r\nAhi quanto a dir qual era è cosa dura\r\nesta selva selvaggia e aspra e forte\r\nche nel pensier rinova la paura!\r\n\r\nTant' è amara che poco è più morte;\r\nma per trattar del ben ch'i' vi trovai,\r\ndirò de l'altre cose ch'i' v'ho scorte.\r\n\r\nIo non so ben ridir com' i' v'intrai,\r\ntant' era pien di sonno a quel punto\r\nche la verace via abbandonai.\r\n\r\nMa poi ch'i' fui al piè d'un colle giunto,\r\nlà dove terminava quella valle\r\nche m'avea di paura il cor compunto,\r\n\r\nguardai in alto e vidi le sue spalle\r\nvestite già de' raggi del pianeta\r\nche mena dritto altrui per ogne calle.\r\n\r\nAllor fu la paura un poco queta,\r\nche nel lago del cor m'era durata\r\nla notte ch'i' passai con tanta pieta.";
		public Form1(NcFormStyle style,NcFormColor color) : base(style,color)
		{
			InitializeComponent();
		}

		private void button1_Click(object sender,EventArgs e)
		{
			this.ClientSize = new System.Drawing.Size(300,200);
			TitleColor = Color.LightBlue;
			BackColor = Color.LightSalmon;
			ButtonsColor = Color.Red;
			StatusBarColor = Color.Gray;
			Title = "Pippo";
			StatusText = "OK";
			Opacity = 0.1f;
			ResizeToContent(20,10);
			SetBarFont(new Font("Courier New",12),NcBars.All);
		}

		protected override void OnHelp()
		{
			DialogResult dr;
			//base.OnHelp();
			//MessageBox.Show(Version(Assembly.GetExecutingAssembly()) + LONG_TEXT,"Help");
			//DialogResult dr = NcMessageBox.Show(this,Version(Assembly.GetExecutingAssembly()) + LONG_TEXT,"Help",MessageBoxButtons.OK);

			//NcMessageBox mb = new NcMessageBox(this,"AAAA","bbb");
			//dr= mb.ShowDialog();
			//MessageBox.Show(dr.ToString());

			dr = NcMessageBox.Show(this,"AAA","bbb",MessageBoxButtons.OKCancel);
			MessageBox.Show(dr.ToString());
		}

		private void Form1_Load(object sender,EventArgs e)
		{
			#if false
			Button bt = new Button();
			bt.Text = "Pippo";
			bt.Location = new Point(100,200);
			bt.Visible = true;
			this.Controls.Add(bt);
			#endif

			AskClose = false;
			Opacity = 0.2f;
		}
	}
}
