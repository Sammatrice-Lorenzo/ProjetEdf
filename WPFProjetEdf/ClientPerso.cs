using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFProjetEdf
{
    public class ClientPerso
    {
        public int NumClient { get; set; }
        public string NomClient { get; set; }
        public string PrenomClient { get; set; }
        public int AnceinReleve { get; set; }
        public int DernierReleve { get; set; }
        public int  IdControleur{ get; set; }
        public int  Montant{ get; set; }
    }
}
