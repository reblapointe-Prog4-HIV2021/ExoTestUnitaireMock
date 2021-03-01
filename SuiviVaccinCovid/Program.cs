using SuiviVaccinCovid.Modele;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SuiviVaccinCovid
{
    public class Program
    {
        public VaccinContext Contexte { get; set; }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Contexte = new VaccinContext();
            p.Peupler();

            p.EnregistrerVaccin("LAPM12345678", "Pfizer");

            Vaccin lePlusRecent = p.LePlusRecent();
            Console.WriteLine(lePlusRecent);
        }

        public void Peupler()
        {
            if (Contexte.Vaccins.Count() == 0)
            {
                Vaccin dose1Mylene = new Vaccin
                {
                    Date = new DateTime(2021, 01, 24),
                    NAMPatient = "LAPM12345678",
                    Type = "Moderna"
                };

                Vaccin dose1Gaston = new Vaccin
                {
                    Date = new DateTime(2021, 01, 15),
                    NAMPatient = "BHEG12345678",
                    Type = "Pfizer"
                };

                Contexte.Vaccins.Add(dose1Mylene);
                Contexte.Vaccins.Add(dose1Gaston);

                Contexte.SaveChanges();
            }
        }

        public void EnregistrerVaccin(string nam, string type)
        {
            var memePatient = Contexte.Vaccins.Where(v => v.NAMPatient == nam);
            if (memePatient.Count() > 1)
                throw new ArgumentException("Patient déjà vacciné deux fois");
            if (memePatient.Count() == 1 && memePatient.First().Type != type)
                throw new ArgumentException("Un patient ne peut pas recevoir deux types de vaccins");

            Vaccin v = new Vaccin
            {
                NAMPatient = nam,
                Type = type,
                Date = DateTime.Now
            };
            Contexte.Add(v);
            Contexte.SaveChanges();
        }

        public Vaccin LePlusRecent(IEnumerable<Vaccin> vaccins)
        {
            return vaccins.OrderBy(v => v.Date).Last();
        }

        public Vaccin LePlusRecent()
        {
            return LePlusRecent(Contexte.Vaccins);
        }
    }
}
