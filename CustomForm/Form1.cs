using NcForms;
using System.Reflection;

namespace CustomForm
{
	public partial class Form1:NcForms.NcForm
	{

		public Form1(NcFormStyle style,NcFormColor color) : base(style,color)
		{
			InitializeComponent();
		
		}

		private void button1_Click(object sender,EventArgs e)
		{
			this.ClientSize = new System.Drawing.Size(300,200);
			TitleColor = Color.LightBlue;
			BackgroundColor = Color.LightGray;
			StatusBarColor = Color.Gray;
			Title = "Pippo";
			StatusText = "OK";
			Opacity = 0.1f;
			ResizeToContent(20,10);
			SetBarFont(new Font("Courier New",12),NcBars.All);
		}

		protected override void OnHelp()
		{
			base.OnHelp();
			MessageBox.Show(Version(Assembly.GetExecutingAssembly()));
		}

		private void Form1_Load(object sender,EventArgs e)
		{
			AskClose = false;
				Opacity = 0.2f;
		}
	}
}
