namespace Stacktim.Model
{
    public class RessourceEntity
    {
        public int idRessource { get; set; }
        public int idTypeR{ get; set; }
        public int idConnaissance { get; set; }

        public string? typeRess { get; set; }

        public DateTime? datePublication { get; set; }

        public string? createur { get; set; }
        public string? contenu { get; set; }

        public RessourceEntity() { }
        public RessourceEntity(int idRessource, int idTypeR, int idConnaissance, string? typeRess, DateTime? datePublication, string? createur, string? contenu)
        {
            this.idRessource = idRessource;
            this.idTypeR = idTypeR;
            this.idConnaissance = idConnaissance;
            this.typeRess = typeRess;
            this.datePublication = datePublication;
            this.createur = createur;
            this.contenu = contenu;
        }
    }
}
