
using System;
using System.Globalization;
using System.Timers;
using S7.Net;


namespace STATISTIKA_UPLOUD
{
    class Program
    {

        private static Vyrobok records;
        private static Timer aTimer;
        private static Timer TimerPlc;

        static void Main(string[] args)
        {
            DateTime dateTime = DateTime.Now;
            Funkcie funkcie = new Funkcie();
            TimerPlc = new Timer();
            TimerPlc.Elapsed += new ElapsedEventHandler(OnPLC);
            TimerPlc.Interval = 1000;
            TimerPlc.Enabled = true;
            //aTimer = new Timer(1000);
            //aTimer.Elapsed += OnTimedEvent;
            //Plc plc = new Plc(CpuType.S71500, "199.166.4.1", 0, 1);
            
            //plc.Open();
            //plc.Write("DB615.DBX230.0", true);
            //plc.Close();
            Console.ReadKey();

        }
        

        private static void OnPLC(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Timer_Spusteni");
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=statistika;";

            string path = @"C:\Users\SKVARA\Desktop\statistika\statistika.csv";

            Plc plc = new Plc(CpuType.S71500, "199.166.4.1", 0, 1);
            int db = 615;
            plc.Open();
           
            if ((bool)plc.Read("DB615.DBX230.0")) {
                TimerPlc.Enabled = false;
                byte[] citam_objednavka = plc.ReadBytes(DataType.DataBlock, db, 8, 15);
                byte[] citam_index_okna = plc.ReadBytes(DataType.DataBlock, db, 26, 15);
                byte[] citam_specialny_znak = plc.ReadBytes(DataType.DataBlock, db, 44, 15);
                byte[] citam_farba_odtien = plc.ReadBytes(DataType.DataBlock, db, 64, 16);
                byte[] citam_nazov_chyby = plc.ReadBytes(DataType.DataBlock, db, 84, 30);
                byte[] citam_datamatrix_vytlaceny = plc.ReadBytes(DataType.DataBlock, db, 116, 21);
                byte[] citam_datamatrix_precitany = plc.ReadBytes(DataType.DataBlock, db, 140, 21);
                byte[] citam_meno = plc.ReadBytes(DataType.DataBlock, db, 186, 20);
                byte[] citam_priezvisko = plc.ReadBytes(DataType.DataBlock, db, 208, 21);

                byte[] datum = plc.ReadBytes(DataType.DataBlock, db, 0, 6);
                byte[] vyroba_start = plc.ReadBytes(DataType.DataBlock, db, 164, 6);
                byte[] vyroba_koniec = plc.ReadBytes(DataType.DataBlock, db, 172, 6);


                DateTime dt;
                string datum_ = "20" + BitConverter.ToString(datum);
                dt = DateTime.ParseExact(datum_, "yyyy-MM-dd-HH-mm-ss", CultureInfo.CurrentUICulture);
                datum_ = dt.ToString("yyyy-MM-dd HH:mm:ss");

                string datum_koniec = "20" + BitConverter.ToString(vyroba_koniec);
                dt = DateTime.ParseExact(datum_koniec, "yyyy-MM-dd-HH-mm-ss", CultureInfo.CurrentUICulture);
                datum_koniec = dt.ToString("yyyy-MM-dd HH:mm:ss");

                string datum_start = "20" + BitConverter.ToString(vyroba_start);
                dt = DateTime.ParseExact(datum_start, "yyyy-MM-dd-HH-mm-ss", CultureInfo.CurrentUICulture);
                datum_start = dt.ToString("yyyy-MM-dd HH:mm:ss");
            
                
                
                records = new Vyrobok
                {
                    datum = datum_,
                    objednavka = Funkcie.ConverterStringPLC(citam_objednavka),
                    index_okna = Funkcie.ConverterStringPLC(citam_index_okna),
                    specialny_znak = Funkcie.ConverterStringPLC(citam_specialny_znak),
                    pin1 = (bool)plc.Read("DB615.DBX62.0"),
                    pin2 = (bool)plc.Read("DB615.DBX62.1"),
                    pin3 = (bool)plc.Read("DB615.DBX62.2"),
                    vtok1 = (bool)plc.Read("DB615.DBX62.3"),
                    vtok2 = (bool)plc.Read("DB615.DBX62.4"),
                    vtok3 = (bool)plc.Read("DB615.DBX62.5"),
                    vtok4 = (bool)plc.Read("DB615.DBX62.6"),

                    farba_odtien = Funkcie.ConverterStringPLC(citam_farba_odtien),

                    lista = (bool)plc.Read("DB615.DBX82.0"),
                    celkomNOK = (bool)plc.Read("DB615.DBX82.1"),
                    celkomOK = (bool)plc.Read("DB615.DBX82.2"),

                    datamatrix_precitany = Funkcie.ConverterStringPLC(citam_datamatrix_precitany),
                    datamatrix_vytlaceny = Funkcie.ConverterStringPLC(citam_datamatrix_vytlaceny),
                    meno = Funkcie.ConverterStringPLC(citam_meno),
                    nazov_chyby = Funkcie.ConverterStringPLC(citam_nazov_chyby),
                    priezvisko = Funkcie.ConverterStringPLC(citam_priezvisko),
                    vyroba_koniec = datum_koniec,
                    vyroba_start = datum_start,
                    vyrobene_ks_okna_NOK = (ushort)plc.Read("DB615.DBW184.0"),
                    vyrobene_ks_okna_OK = (ushort)plc.Read("DB615.DBW182.0"),
                    vyrobene_ks_palety = (ushort)plc.Read("DB615.DBW180.0"),
                };
            
                if (Funkcie.UploudDb(connectionString, records)){records.poslany = true;}
                else{records.poslany = false;}
                Funkcie.WriteToTxt(path, records);

                Funkcie.UpdateDb(connectionString, path);
                Funkcie.DeleteInTxt(path);
                Console.WriteLine("ZAPISANE");
                TimerPlc.Enabled = true;
            }

            plc.Write("DB615.DBX230.0", false);
            plc.Close();
        }
    }
}                         

