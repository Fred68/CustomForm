using NcForms;

namespace CustomForm
{
	public partial class Form1:NcForms.NcForm
	{

		public Form1(NcWindowsStyles style) : base(style)
		{

			InitializeComponent();
			FormOpacity = 0.5f;
			
			SetupControls(this);
		}

		private void button1_Click(object sender,EventArgs e)
		{
			this.ClientSize = new System.Drawing.Size(300,200);
			TitleColor = Color.LightBlue;
			BackgroundColor = Color.LightGray;
			StatusBarColor = Color.Gray;
			Title = "Pippo";
			StatusText = "OK";
		}
	}
}
