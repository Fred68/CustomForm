namespace CustomForm
{
	public partial class Form1:NcForms.NcForm
	{

		public Form1()
		{

			InitializeComponent();
			FormOpacity = 0.5f;
			
			SetupControls(this);
		}

		private void button1_Click(object sender,EventArgs e)
		{
			this.ClientSize = new System.Drawing.Size(300,200);
			//ShowTsHelp = false;
			//ShowTsMenu = false;
			TitleColor = Color.LightBlue;
			BackgroundColor = Color.LightGray;
			StatusBarColor = Color.Gray;
			Title = "Pippo";
			StatusText = "OK";
			//ShowTsMaxMin = false;	
		}
	}
}
