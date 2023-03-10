using System.Data.SqlClient;


namespace Stacktim.Model
{
    public class CategorieRepo
    {
        private readonly IConfiguration? _configuration;

        public CategorieRepo(IConfiguration? configuration)
        {
            this._configuration = configuration;
        }

        public CategorieEntity GetCategorieByNom(string cCategorie) { 

            var categorie = new CategorieEntity();
            var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
            var oSqlCommand = new SqlCommand("Select * from categorie where nom = @Nom");
            var oSqlParam = new SqlParameter("@Nom", cCategorie);

            oSqlCommand.Parameters.Add(oSqlParam);
            oSqlConnection.Open();
            oSqlCommand.Connection = oSqlConnection;
            var oSqlDataReader = oSqlCommand.ExecuteReader();

            while (oSqlDataReader.Read())
            {
                categorie.idCategorie = (int)oSqlDataReader["IDCATEGORIE"];
                categorie.nom = (string)oSqlDataReader["NOM"];
            };
            oSqlDataReader.Close();
            oSqlConnection.Close();
            return categorie;
        }

        public bool Update(CategorieEntity categorieEntity)
        {
            try
            {
                var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
                var oSqlParam = new SqlParameter("@idCategorie", categorieEntity.idCategorie);
                var oSqlParam2 = new SqlParameter("@nom", categorieEntity.nom);
                var oSqlCommand = new SqlCommand("Update categorie Set nom = @nom Where idCategorie = @idCategorie");

                oSqlCommand.Parameters.Add(oSqlParam);
                oSqlCommand.Parameters.AddRange(new SqlParameter[] { oSqlParam2 });

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

        public bool Delete(int idCategorie)
        {
            try
            {
                var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
                var oSqlParam = new SqlParameter("@Id", idCategorie);
                var oSqlCommand = new SqlCommand("Delete categorie Where idCategorie = @Id");

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

        public int Insert(CategorieEntity categorieEntity)
        {
            var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
            var oSqlParam2 = new SqlParameter("@Nom", categorieEntity.nom);
            oSqlConnection.Open();
            var oSqlTransaction = oSqlConnection.BeginTransaction();
            try
            {
                var oSqlCommand = new SqlCommand("Insert Into categorie(nom) Values (@Nom); Select @@Identity;");

                oSqlCommand.Parameters.Add(oSqlParam2);

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

        public List<CategorieEntity> Getall()
        {
            var oListCategorie = new List<CategorieEntity>();
            var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
            var oSqlCommand = new SqlCommand("Select * From categorie");
            oSqlConnection.Open();
            oSqlCommand.Connection = oSqlConnection;
            var oSqlDataReader = oSqlCommand.ExecuteReader();
            var categorie = new CategorieEntity();
            var lRead = oSqlDataReader.Read();
            while (lRead)
            {
                categorie = new CategorieEntity
                {
                    idCategorie = (int)oSqlDataReader["idCategorie"],
                    nom = (string)oSqlDataReader["nom"]
                };
                while ((int)oSqlDataReader["idCategorie"] == categorie.idCategorie)
                {
                    
                    lRead = oSqlDataReader.Read();
                    if (!lRead) break;
                }
                oListCategorie.Add(categorie);
            };

            oSqlDataReader.Close();
            oSqlConnection.Close();
            return oListCategorie;
        }

    }
}
