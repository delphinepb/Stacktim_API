namespace Stacktim.Model
{
    public class ProjetEntity
    {
        public int idProjet { get; set; }
        public int idStatut { get; set; }

        public string? descriptif { get; set; }

        public DateTime dateCreation { get; set; }
        public string? createur { get; set; }

        public string? etatProjet { get; set; }

        public ProjetEntity() { }
        public ProjetEntity(int idProjet, int idStatut, string? descriptif, DateTime dateCreation, string? createur, string? etatProjet)
        {
            this.idProjet = idProjet;
            this.idStatut = idStatut;
            this.descriptif = descriptif;
            this.dateCreation = dateCreation;
            this.createur = createur;
            this.etatProjet = etatProjet;
        }
    }
}
