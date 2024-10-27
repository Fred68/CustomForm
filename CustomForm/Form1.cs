using NcForms;

namespace CustomForm
{
	public partial class Form1 : NcForms.NcForm
	{

		public Form1(NcFormStyle style, NcFormColor color) : base(style, color)
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
			AskClose = false;
			ChangeBarFont(new Font("Courier New",12),NcBars.All);
		}

		public override void OnHelp()
		{
			MessageBox.Show("New help");
		}
	}
}
