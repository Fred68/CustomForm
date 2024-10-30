using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NcForm
{

	#warning NcMessageBox classe speciale.
	#warning Costruttore privato
	#warning Usare alcune funzioni public static DialogResult Show(...)
	#warning Passare come argomenti: string? text, string? caption, MessageBoxButtons buttons, NcFormColor color
	#warning Lo stile NcFormStyle style è sempre NcForm.Fixed (senza altre aggiunte, nessun Help...)
	#warning Prevedere una text box readonly multilinea con barre di scroll.
	#warning Aggiungere classi statiche per colore standard, avviso, errore (colore barre)
	public class NcMessageBox : NcForms.NcForm
	{

		public NcMessageBox() : base()
		{
			
		}
		public NcMessageBox(NcFormStyle style,NcFormColor color) : base(style, color) { }
		public NcMessageBox(NcForms.NcForm ncf) : base(ncf.NcStyle, ncf.NcColor) { }
		
	}
}
