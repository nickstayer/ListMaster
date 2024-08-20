using System;
using System.Collections.Generic;

namespace ListMaster
{
    public class Dossier
    {
        public string Fullname { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Othername { get; set; }
        public DateTime Bdate { get; set; }
        public string Nationality { get; set; }
        public string BirthPlace { get; set; }
        public string DeathSigal { get; set; }
        public string DeathCert { get; set; }
        public List<Document> Documents { get; set; }
        public List<Registration> Registrations { get; set; }
        public bool HasFormOne { get; set; }

        public Dossier()
        {
            Documents = new List<Document>();
            Registrations = new List<Registration>();
        }
    }
}
