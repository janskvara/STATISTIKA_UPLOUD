using System;
using System.Collections.Generic;
using System.Text;

namespace STATISTIKA_UPLOUD
{
    class Vyrobok
    {
        public bool poslany { get; set; }
        public string datum { get; set; }
        public string objednavka { get; set; } 
        public string index_okna { get; set; }
        public string specialny_znak { get; set; }
        public bool pin1 { get; set; }
        public bool pin2 { get; set; }
        public bool pin3 { get; set; }
        public bool vtok1 { get; set; }
        public bool vtok2 { get; set; }
        public bool vtok3 { get; set; }
        public bool vtok4 { get; set; }
        public string farba_odtien { get; set; }
        public bool lista { get; set; }
        public bool celkomOK { get; set; }
        public bool celkomNOK { get; set; }

        public string nazov_chyby { get; set; }
        public string datamatrix_vytlaceny { get; set; }
        public string datamatrix_precitany { get; set; }
        
        public string vyroba_start { get; set; }
        public string vyroba_koniec { get; set; }

        public int vyrobene_ks_palety { get; set; }
        public int vyrobene_ks_okna_OK { get; set; }
        public int vyrobene_ks_okna_NOK { get; set; }

        public string meno { get; set; }
        public string priezvisko { get; set; }
    }
}
