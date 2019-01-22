
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MySql.Data.MySqlClient;


namespace STATISTIKA_UPLOUD
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime dateTime = DateTime.Now;
            Funkcie funkcie = new Funkcie();

            string dateTimeString = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            Console.WriteLine(dateTimeString);
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=statistika;";

            var records =  new Vyrobok
            {
                datum = dateTimeString,
                objednavka = "000001072295",
                index_okna = "jjjjj",
                specialny_znak = "fffff",
                pin1 = true,
                pin2 = true,
                pin3 = true,
                vtok1 = true,
                vtok2 = true,
                vtok3 = true,
                vtok4 = true,
                farba_odtien = "zelena",
                lista = true,
                celkomNOK = true,
                celkomOK = true,
                datamatrix_precitany = "ttt",
                datamatrix_vytlaceny = "ttttt",
                meno = "JURAJ",
                nazov_chyby = "Chyba",
                priezvisko = "Mrkva",
                vyroba_koniec = dateTimeString,
                vyroba_start = dateTimeString,
                vyrobene_ks_okna_NOK = 20,
                vyrobene_ks_okna_OK = 1000,
                vyrobene_ks_palety = 4
            };
            string txt;
            if (Funkcie.UploudDb(connectionString, records))
            {

                records.poslany = true;
            }
            else
            {
                records.poslany = false; 
            }
            txt = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};{16};{17};{18};{19};{20};{21};{22};{23};{24};{25}",
                records.poslany, records.datum, records.objednavka, records.index_okna, records.specialny_znak, records.pin1, records.pin2, records.pin3, records.vtok1, records.vtok2, records.vtok3, records.vtok4,
                records.lista, records.celkomOK, records.celkomNOK, records.farba_odtien, records.nazov_chyby, records.datamatrix_vytlaceny, records.datamatrix_precitany, records.vyroba_start, records.vyroba_koniec,
                records.vyrobene_ks_palety, records.vyrobene_ks_okna_OK, records.vyrobene_ks_okna_NOK, records.meno, records.priezvisko);
            Console.WriteLine(txt);
            Console.ReadKey();


           
        }                         
       
    }
}
