using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

}


        public static bool WriteToTxt(string path, Vyrobok records)
        {
            string txt = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};{16};{17};{18};{19};{20};{21};{22};{23};{24};{25}",
                    records.poslany, records.datum, records.objednavka, records.index_okna, records.specialny_znak, records.pin1, records.pin2, records.pin3, records.vtok1, records.vtok2, records.vtok3, records.vtok4,
                    records.lista, records.celkomOK, records.celkomNOK, records.farba_odtien, records.nazov_chyby, records.datamatrix_vytlaceny, records.datamatrix_precitany, records.vyroba_start, records.vyroba_koniec,
                    records.vyrobene_ks_palety, records.vyrobene_ks_okna_OK, records.vyrobene_ks_okna_NOK, records.meno, records.priezvisko);
            try { 
                if (File.Exists(path))
                {
                    string tempfile = Path.GetTempFileName();
                    using (var writer = new StreamWriter(tempfile))
                    using (var reader = new StreamReader(path))
                    {
                        writer.WriteLine(reader.ReadLine());
                        writer.WriteLine(txt);
                        while (!reader.EndOfStream)
                            writer.WriteLine(reader.ReadLine());
                    }
                    File.Copy(tempfile, path, true);
                }
                else
                {
                    using (var file = File.Create(path))
                    using (StreamWriter writer = new StreamWriter(file))
                    {
                        writer.WriteLine(";DATUM;OBJEDNAVKA;INDEX OKNA;SPEC. ZNAK;PIN1;PIN2;PIN3;VTOK1;VTOK2;VTOK3;VTOK4;LISTA;OK;NOK;FARBA;NAZOV CHYBY;DATAMATRIX VYT.;DATAMATRIX PREC.;VYROBA START;VYROBA KONIEC;VYROBENE KS PALETY;VYROBENE OKNA OK;VYROBENE OKNA NOK;MENO;PRIEZVISKO");
                        writer.WriteLine(txt);
                    }
                   

                }
                return true; 
            }catch(Exception e )
            {
                Console.WriteLine(e.Message);
                return false; 
            }
        }
        private static bool lineChanger(string path, int line_to_edit, Vyrobok records)
        {
            string txt = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};{16};{17};{18};{19};{20};{21};{22};{23};{24};{25}",
                    records.poslany, records.datum, records.objednavka, records.index_okna, records.specialny_znak, records.pin1, records.pin2, records.pin3, records.vtok1, records.vtok2, records.vtok3, records.vtok4,
                    records.lista, records.celkomOK, records.celkomNOK, records.farba_odtien, records.nazov_chyby, records.datamatrix_vytlaceny, records.datamatrix_precitany, records.vyroba_start, records.vyroba_koniec,
                    records.vyrobene_ks_palety, records.vyrobene_ks_okna_OK, records.vyrobene_ks_okna_NOK, records.meno, records.priezvisko);
            try
            {
                if (File.Exists(path))
                {
                    string[] arrLine = File.ReadAllLines(path);
                    arrLine[line_to_edit] = txt;
                    File.WriteAllLines(path, arrLine);
                }
                else
                {
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public static void UpdateDb(string connectionstring , string path)
        {
            try { 
                string[] lines = File.ReadAllLines(path);
                for (int i = 1; i < lines.Length; i++)
                {

                    string[] data_to_server = lines[i].Split(';');
                    if (data_to_server[0].Equals("false") || data_to_server[0].Equals("False") || data_to_server[0].Equals("FALSE") || data_to_server[0].Equals(0))
                    {
                        Vyrobok records_txt = new Vyrobok
                        {
                            datum = data_to_server[1],
                            objednavka = data_to_server[2],
                            index_okna = data_to_server[3],
                            specialny_znak = data_to_server[4],
                            pin1 = Convert.ToBoolean(data_to_server[5]),
                            pin2 = Convert.ToBoolean(data_to_server[6]),
                            pin3 = Convert.ToBoolean(data_to_server[7]),
                            vtok1 = Convert.ToBoolean(data_to_server[8]),
                            vtok2 = Convert.ToBoolean(data_to_server[9]),
                            vtok3 = Convert.ToBoolean(data_to_server[10]),
                            vtok4 = Convert.ToBoolean(data_to_server[11]),
                            farba_odtien = data_to_server[15],
                            lista = Convert.ToBoolean(data_to_server[12]),
                            celkomNOK = Convert.ToBoolean(data_to_server[14]),
                            celkomOK = Convert.ToBoolean(data_to_server[13]),
                            datamatrix_precitany = data_to_server[18],
                            datamatrix_vytlaceny = data_to_server[17],
                            meno = data_to_server[24],
                            nazov_chyby = data_to_server[16],
                            priezvisko = data_to_server[25],
                            vyroba_koniec = data_to_server[20],
                            vyroba_start = data_to_server[19],
                            vyrobene_ks_okna_NOK = Convert.ToInt16(data_to_server[23]),
                            vyrobene_ks_okna_OK = Convert.ToInt16(data_to_server[22]),
                            vyrobene_ks_palety = Convert.ToInt16(data_to_server[21]),
                        };
                        if (Funkcie.UploudDb(connectionstring, records_txt))
                        {
                            records_txt.poslany = true;
                            Funkcie.lineChanger(path, i, records_txt);
                        }

                    }
                }

            }catch(Exception e)
            {
                Console.WriteLine(e);
            }

        }

        public static void DeleteInTxt(string path)
        {
            try
            {
                string[] lines = File.ReadAllLines(path);

                using (StreamWriter writer = new StreamWriter(path))
                {

                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] data = lines[i].Split(';');
                        if ((i > 100) && (data[0].Equals("true") || data[0].Equals("True") || data[0].Equals("TRUE") || data[0].Equals("1")))
                        {
                        }
                        else
                        {
                            writer.WriteLine(lines[i]);
                        }
                    }
                }
                lines = null;
            }
            catch (Exception)
            {
            }
        }
    }
}
