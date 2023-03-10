namespace Stacktim.Model
{
    public class CategorieEntity
    {
        public int idCategorie { get; set; }
        public string? nom { get; set; }

        public CategorieEntity() { 
        
        }

        public CategorieEntity(int idCategorie, string? nom)
        {
            this.idCategorie = idCategorie;
            this.nom = nom;
        }
    }


}
