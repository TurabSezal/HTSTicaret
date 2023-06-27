
using HTSTicaret.Models;

namespace HTSTicaret.WebUI.App_Classes
{
    public class Context
    {
		private static Entity baglanti;

		public static Entity Baglanti
		{
			get { if(baglanti == null )
					baglanti= new Entity();
				return baglanti;
			}
			set { baglanti = value; }
		}

	}
}