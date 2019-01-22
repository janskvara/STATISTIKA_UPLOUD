using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace STATISTIKA_UPLOUD
{
    class Funkcie
    {

        public static bool UploudDb(string connectionstring , Vyrobok records)
        {
            string query = "INSERT INTO ford( datum, objednavka, index_okna, specialny_znak, pin1, pin2, pin3, vtok1, vtok2, vtok3, vtok4, farba_odtien, lista, celkomOK,celkomNOK, nazov_chyby," +
               "datamatrix_vytlaceny, datamatrix_precitany, vyroba_start, vyroba_koniec, vyrobene_ks_palety, vyrobene_ks_okna_OK, vyrobene_ks_okna_NOK, meno,priezvisko )";

            query += "VALUES ( @datum, @objednavka, @index_okna, @specialny_znak, @pin1, @pin2, @pin3, @vtok1, @vtok2, @vtok3, @vtok4, @farba_odtien, @lista, @celkomOK, @celkomNOK, @nazov_chyby," +
                    "@datamatrix_vytlaceny, @datamatrix_precitany, @vyroba_start, @vyroba_koniec, @vyrobene_ks_palety, @vyrobene_ks_okna_OK, @vyrobene_ks_okna_NOK, @meno, @priezvisko)";

            try
            {
                MySqlConnection databaseConnection = new MySqlConnection(connectionstring);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);

                databaseConnection.Open();
                commandDatabase.Parameters.AddWithValue("@datum", records.datum);
                commandDatabase.Parameters.AddWithValue("@objednavka", records.objednavka);
                commandDatabase.Parameters.AddWithValue("@index_okna", records.index_okna);
                commandDatabase.Parameters.AddWithValue("@specialny_znak", records.specialny_znak);
                commandDatabase.Parameters.AddWithValue("@pin1", records.pin1);
                commandDatabase.Parameters.AddWithValue("@pin2", records.pin2);
                commandDatabase.Parameters.AddWithValue("@pin3", records.pin3);
                commandDatabase.Parameters.AddWithValue("@vtok1", records.vtok1);
                commandDatabase.Parameters.AddWithValue("@vtok2", records.vtok2);
                commandDatabase.Parameters.AddWithValue("@vtok3", records.vtok3);
                commandDatabase.Parameters.AddWithValue("@vtok4", records.vtok4);
                commandDatabase.Parameters.AddWithValue("@farba_odtien", records.farba_odtien);
                commandDatabase.Parameters.AddWithValue("@lista", records.lista);
                commandDatabase.Parameters.AddWithValue("@celkomOK", records.celkomOK);
                commandDatabase.Parameters.AddWithValue("@celkomNOK", records.celkomNOK);
                commandDatabase.Parameters.AddWithValue("@nazov_chyby", records.nazov_chyby);
                commandDatabase.Parameters.AddWithValue("@datamatrix_vytlaceny", records.datamatrix_vytlaceny);
                commandDatabase.Parameters.AddWithValue("@datamatrix_precitany", records.datamatrix_precitany);
                commandDatabase.Parameters.AddWithValue("@vyroba_start", records.vyroba_start);
                commandDatabase.Parameters.AddWithValue("@vyroba_koniec", records.vyroba_koniec);
                commandDatabase.Parameters.AddWithValue("@vyrobene_ks_palety", records.vyrobene_ks_palety);
                commandDatabase.Parameters.AddWithValue("@vyrobene_ks_okna_OK", records.vyrobene_ks_okna_OK);
                commandDatabase.Parameters.AddWithValue("@vyrobene_ks_okna_NOK", records.vyrobene_ks_okna_NOK);
                commandDatabase.Parameters.AddWithValue("@meno", records.meno);
                commandDatabase.Parameters.AddWithValue("@priezvisko", records.priezvisko);
                commandDatabase.ExecuteNonQuery();
                databaseConnection.Close();
                return true;
            }catch(MySqlException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            
        }
       
    }
}
