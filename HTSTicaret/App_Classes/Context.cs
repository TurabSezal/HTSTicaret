
using HTSTicaret.Models;

namespace HTSTicaret.WebUI.App_Classes
{
    public class Context
    {
		private static Entity2 baglanti;

		public static Entity2 Baglanti
		{
			get { if(baglanti == null )
					baglanti= new Entity2();
				return baglanti;
			}
			set { baglanti = value; }
		}

	}
}