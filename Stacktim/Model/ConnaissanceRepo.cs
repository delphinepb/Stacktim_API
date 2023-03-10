using System.Data.SqlClient;

namespace Stacktim.Model
{
    public class ConnaissanceRepo
    {

        private readonly IConfiguration? _configuration;

        public ConnaissanceRepo(IConfiguration? configuration)
        {
            this._configuration = configuration;
        }

        public ConnaissanceEntity GetConnaissancebyNom(string cConnaissance)
        {

            var connaissance = new ConnaissanceEntity();
            var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
            var oSqlCommand = new SqlCommand("Select * from connaissance as co inner join categorie as ca on ca.idcategorie = co.idcategorie where CA.nom = @Nom");
            var oSqlParam = new SqlParameter("@Nom", cConnaissance);

            oSqlCommand.Parameters.Add(oSqlParam);
            oSqlConnection.Open();
            oSqlCommand.Connection = oSqlConnection;
            var oSqlDataReader = oSqlCommand.ExecuteReader();

            while (oSqlDataReader.Read())
            {
                connaissance.idConnaissance = (int)oSqlDataReader["idConnaissance"];
                connaissance.idCategorie = (int)oSqlDataReader["idCategorie"];
                connaissance.nom = (string)oSqlDataReader["NOM"];
                connaissance.categorie = (string)oSqlDataReader["categorie"];
                connaissance.descriptionCourte = (string)oSqlDataReader["descriptionCourte"];
                connaissance.descriptionLongue = (string)oSqlDataReader["descriptionLongue"];


            };
            oSqlDataReader.Close();
            oSqlConnection.Close();
            return connaissance;
        }

        public bool Update(ConnaissanceEntity connaissanceEntity)
        {
            try
            {
                var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
                var oSqlParam = new SqlParameter("@idConnaissance", connaissanceEntity.idConnaissance);
                var oSqlParam2 = new SqlParameter("@idCategorie", connaissanceEntity.idCategorie);
                var oSqlParam3 = new SqlParameter("@nom", connaissanceEntity.nom);
                var oSqlParam4 = new SqlParameter("@categorie", connaissanceEntity.categorie);
                var oSqlParam5 = new SqlParameter("@descriptionC", connaissanceEntity.descriptionCourte);
                var oSqlParam6 = new SqlParameter("@descriptionL", connaissanceEntity.descriptionLongue);
                var oSqlCommand = new SqlCommand("Update connaissance Set idCategorie = @idCategorie, nom = @nom, categorie = @categorie, descriptionCourte = @descriptionC, descriptionLongue = @descriptionL Where idConnaissance = @idConnaissance");

                oSqlCommand.Parameters.Add(oSqlParam);
                oSqlCommand.Parameters.AddRange(new SqlParameter[] { oSqlParam2, oSqlParam3,oSqlParam4, oSqlParam5, oSqlParam6 });

                oSqlCommand.Connection = oSqlConnection;
                oSqlConnection.Open();

                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int idConnaissance)
        {
            try
            {
                var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
                var oSqlParam = new SqlParameter("@Id", idConnaissance);
                var oSqlCommand = new SqlCommand("Delete connaissance Where idConnaissance = @Id");

                oSqlCommand.Parameters.Add(oSqlParam);

                oSqlCommand.Connection = oSqlConnection;
                oSqlConnection.Open();

                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int Insert(ConnaissanceEntity connaissanceEntity)
        {
            var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
            var oSqlParam2 = new SqlParameter("@IdCategorie", connaissanceEntity.idCategorie);
            var oSqlParam3 = new SqlParameter("@Nom", connaissanceEntity.nom);
            var oSqlParam4 = new SqlParameter("@Categorie", connaissanceEntity.categorie);
            var oSqlParam5 = new SqlParameter("@descriptionC", connaissanceEntity.descriptionCourte);
            var oSqlParam6 = new SqlParameter("@descriptionL", connaissanceEntity.descriptionLongue);
            oSqlConnection.Open();
            var oSqlTransaction = oSqlConnection.BeginTransaction();
            try
            {
                var oSqlCommand = new SqlCommand("Insert Into categorie(idCategorie,nom,categorie,descriptionCourte, descriptionLongue) Values (@IdCategorie,@Nom, @Categorie, @descritpionC, @descritpionL); Select @@Identity;");
                oSqlCommand.Parameters.Add(oSqlParam2);
                oSqlCommand.Parameters.AddRange(new SqlParameter[] { oSqlParam3, oSqlParam4, oSqlParam5, oSqlParam6 });

                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.Transaction = oSqlTransaction;

                var IdReturn = oSqlCommand.ExecuteScalar();

                oSqlTransaction.Commit();

                return Convert.ToInt32(IdReturn);
            }
            catch (Exception)
            {
                oSqlTransaction.Rollback();
                return -1;
            }
            finally
            {
                oSqlConnection.Close();
            }
        }

        public List<ConnaissanceEntity> Getall()
        {
            var oListConnaissance = new List<ConnaissanceEntity>();
            var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
            var oSqlCommand = new SqlCommand("Select * From connaissance");
            oSqlConnection.Open();
            oSqlCommand.Connection = oSqlConnection;
            var oSqlDataReader = oSqlCommand.ExecuteReader();
            var connaissance = new ConnaissanceEntity();
            var lRead = oSqlDataReader.Read();
            while (lRead)
            {
                connaissance = new ConnaissanceEntity
                {
                    idConnaissance = (int)oSqlDataReader["idConnaissance"],
                    idCategorie = (int)oSqlDataReader["idCategorie"],
                    categorie = (string)oSqlDataReader["categorie"],
                    nom = (string)oSqlDataReader["nom"],
                    descriptionCourte = (string)oSqlDataReader["descriptionCourte"],
                    descriptionLongue = (string)oSqlDataReader["descriptionLongue"]
                };
                while ((int)oSqlDataReader["idConnaissance"] == connaissance.idConnaissance)
                {

                    lRead = oSqlDataReader.Read();
                    if (!lRead) break;
                }
                oListConnaissance.Add(connaissance);
            };

            oSqlDataReader.Close();
            oSqlConnection.Close();
            return oListConnaissance;
        }

    }
}
