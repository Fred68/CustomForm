

#define LONGTEXT

using NcForms;
using System.ComponentModel.Design;
using System.Reflection;
//using static System.Net.Mime.MediaTypeNames;

namespace CustomForm
{
    public partial class Form1:NcForms.NcForm
	{
		#if LONGTEXT
		static string LONG_TEXT = "\r\nNel mezzo del cammin di nostra vita\r\nmi ritrovai per una selva oscura,\r\n"+
								"ché la diritta via era smarrita.\r\n\r\nAhi quanto a dir qual era è cosa dura\r\n"+
								"esta selva selvaggia e aspra e forte\r\nche nel pensier rinova la paura!\r\n\r\n"+
								"Tant' è amara che poco è più morte;\r\nma per trattar del ben ch'i' vi trovai,\r\n"+
								"dirò de l'altre cose ch'i' v'ho scorte.\r\n\r\nIo non so ben ridir com' i' v'intrai,\r\n"+
								"tant' era pien di sonno a quel punto\r\nche la verace via abbandonai.\r\n\r\n"+
								"Ma poi ch'i' fui al piè d'un colle giunto,\r\nlà dove terminava quella valle\r\n"+
								"che m'avea di paura il cor compunto,\r\n\r\nguardai in alto e vidi le sue spalle\r\n"+
								"vestite già de' raggi del pianeta\r\nche mena dritto altrui per ogne calle.\r\n\r\n"+
								"Allor fu la paura un poco queta,\r\nche nel lago del cor m'era durata\r\n"+
								"la notte ch'i' passai con tanta pieta.";
		#else
		static string LONG_TEXT = "-";
		#endif

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
			dr = NcMessageBox.Show(this,Version(Assembly.GetExecutingAssembly()) + LONG_TEXT,"Help",MessageBoxButtons.YesNoCancel);
			MessageBox.Show(dr.ToString());
		}

		private void Form1_Load(object sender,EventArgs e)
		{
			AskClose = false;
			Opacity = 0.4f;
		}
	}
}
