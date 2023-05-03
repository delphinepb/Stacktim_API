namespace Stacktim.Model
{
    public class StatutProjetEntity
    {
        public int idStatut { get; set; }
        public string? nom { get; set; }

        public StatutProjetEntity() { }

        public StatutProjetEntity(int idStatut, string nom)
        {
            this.idStatut = idStatut;
            this.nom = nom; 
        }
    }
}
