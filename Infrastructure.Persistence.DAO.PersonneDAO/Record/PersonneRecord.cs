using System; //pour DateTime


namespace Infrastructure.Persistence.DAO.PersonneDAO.Record
{
    public class PersonneRecord //Représente les caractéristiques(que celles utilisées ici) d'un enregistrement de la Table des Personnes en base.
    {
        public int id;
        public string nom;
        public string prenom;
        public bool? isHomme;
        public DateTime? dateHeuresInscr;
        public DateTime? dateNaissance;
        public string chaineChar30;
        public int? entierNullable;
        public bool? boolNullable;
        public double? unDoubleNullable;

    };
}
