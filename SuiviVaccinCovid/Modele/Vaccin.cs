using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviVaccinCovid.Modele
{
    public class Vaccin
    {
        public int VaccinId { get; set; }
        public DateTime Date { get; set; }
        public string NAMPatient { get; set; }
        public string Type { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Vaccin vaccin &&
                   VaccinId == vaccin.VaccinId &&
                   Date == vaccin.Date &&
                   NAMPatient == vaccin.NAMPatient &&
                   Type == vaccin.Type;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(VaccinId, Date, NAMPatient, Type);
        }

        public override string ToString()
        {
            return $" Vaccin #{VaccinId} ({Type}), adiminstré le {Date} à {NAMPatient}";
        }
    }
}
