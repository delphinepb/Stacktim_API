namespace Stacktim.Model
{
    public class ConnaissanceEntity
    {
        public int idConnaissance { get; set; }
        public int idCategorie { get; set; }
        public string? categorie { get; set; }

        public string? nom { get; set; }

        public string? descriptionCourte { get; set; }

        public string? descriptionLongue { get; set; }

        public ConnaissanceEntity() { } 

        public ConnaissanceEntity(int idConnaissance, int idCategorie, string? categorie, string? nom, string? descriptionCourte, string? descriptionLongue)
        {
            this.idConnaissance = idConnaissance;
            this.idCategorie = idCategorie;
            this.categorie = categorie;
            this.nom = nom;
            this.descriptionCourte = descriptionCourte;
            this.descriptionLongue = descriptionLongue;
        }
    }
}
