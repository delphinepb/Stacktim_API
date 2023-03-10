namespace Stacktim.Model
{
    public class TypeREntity
    {
        public int idTypeR { get; set; }
        public string? descriptif { get; set; }
        public string? image { get; set; }

        public TypeREntity() { }
        public TypeREntity(int idTypeR, string? descriptif, string? image)
        {
            this.idTypeR = idTypeR;
            this.descriptif = descriptif;
            this.image = image;
        }
    }

   
}
