﻿
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Timers;
using MySql.Data.MySqlClient;


namespace STATISTIKA_UPLOUD
{
    class Program
    {

        private static Vyrobok records;
        private static bool update_all = true;
        private static Timer aTimer;
        static void Main(string[] args)
        {
            DateTime dateTime = DateTime.Now;
            Funkcie funkcie = new Funkcie();

            string dateTimeString = dateTime.ToString("yyyy-MM-dd HH:mm");
            Console.WriteLine(dateTimeString);
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=statistika;";
            string path = @"C:\Users\SKVARA\Desktop\statistika\statistika.csv";
            records = new Vyrobok
            {
                datum = dateTimeString,
                objednavka = "000001072295",
                index_okna = "jjjjj",
                specialny_znak = "fffff",
                pin1 = true,
                pin2 = true,
                pin3 = false,
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

            aTimer = new Timer(1000);
            aTimer.Elapsed += OnTimedEvent;
            if (Funkcie.UploudDb(connectionString, records))
            {

                records.poslany = true;
            }
            else
            {
                   
                records.poslany = false;
                update_all = false;
            }
                if (!Funkcie.WriteToTxt(path, records))
                {
                    aTimer.Enabled = true;
                }
            
                
                Funkcie.UpdateDb(connectionString,path);

            Funkcie.DeleteInTxt(path);
            Console.ReadKey();

        }
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            
            if (Funkcie.WriteToTxt(@"C:\Users\SKVARA\Desktop\statistika\statistika.csv", records ))
            {
                aTimer.Enabled = false;
            }
        }
    }
}                         

