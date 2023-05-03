using System.Data.SqlClient;

namespace Stacktim.Model
{
    public class ProjetRepo
    {

        private readonly IConfiguration? _configuration;

        public ProjetRepo(IConfiguration? configuration)
        {
            this._configuration = configuration;
        }

        public ProjetEntity GetProjetById(int idProjet)
        {

            var projet = new ProjetEntity();
            var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
            var oSqlCommand = new SqlCommand("Select * from projet as p inner join statutprojet as s on p.idstatut = s.idstatut where idProjet = @Id");
            var oSqlParam = new SqlParameter("@Id", idProjet);

            oSqlCommand.Parameters.Add(oSqlParam);
            oSqlConnection.Open();
            oSqlCommand.Connection = oSqlConnection;
            var oSqlDataReader = oSqlCommand.ExecuteReader();

            while (oSqlDataReader.Read())
            {
                projet.idProjet = (int)oSqlDataReader["idProjet"];
                projet.idStatut = (int)oSqlDataReader["idStatut"];
                projet.descriptif = (string)oSqlDataReader["descriptif"];
                projet.dateCreation = (DateTime)oSqlDataReader["dateCreation"];
                projet.createur = (string)oSqlDataReader["createur"];
                projet.etatProjet = (string)oSqlDataReader["etatProjet"];

            };
            oSqlDataReader.Close();
            oSqlConnection.Close();
            return projet;
        }

        public List<ProjetEntity> Getall()
        {
            var oListProjet = new List<ProjetEntity>();
            var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
            var oSqlCommand = new SqlCommand("Select * From projet");
            oSqlConnection.Open();
            oSqlCommand.Connection = oSqlConnection;
            var oSqlDataReader = oSqlCommand.ExecuteReader();
            var projet = new ProjetEntity();
            var lRead = oSqlDataReader.Read();
            while (lRead)
            {
                projet = new ProjetEntity
                {
                    idProjet = (int)oSqlDataReader["idProjet"],
                    idStatut = (int)oSqlDataReader["idStatut"],
                    descriptif = (string)oSqlDataReader["descriptif"],
                    dateCreation = (DateTime)oSqlDataReader["dateCreation"],
                    createur = (string)oSqlDataReader["createur"],
                    etatProjet = (string)oSqlDataReader["etatProjet"]
                };
                while ((int)oSqlDataReader["idProjet"] == projet.idProjet)
                {

                    lRead = oSqlDataReader.Read();
                    if (!lRead) break;
                }
                oListProjet.Add(projet);
            };

            oSqlDataReader.Close();
            oSqlConnection.Close();
            return oListProjet;
        }

        public bool Update(ProjetEntity projetEntity)
        {
            try
            {
                var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
                var oSqlParam = new SqlParameter("@idProjet", projetEntity.idProjet);
                var oSqlParam1 = new SqlParameter("@idStatut", projetEntity.idStatut);
                var oSqlParam2 = new SqlParameter("@descriptif", projetEntity.descriptif);
                var oSqlParam3 = new SqlParameter("@date", projetEntity.dateCreation);
                var oSqlParam4 = new SqlParameter("@createur", projetEntity.createur);
                var oSqlParam5 = new SqlParameter("@etat", projetEntity.etatProjet);

                var oSqlCommand = new SqlCommand("Update projet Set idStatut = @idStatut, descriptif = @descriptif, dateCreation = @date, createur = @createur, etatProjet = @etat Where idProjet = @idProjet");

                oSqlCommand.Parameters.Add(oSqlParam);
                oSqlCommand.Parameters.AddRange(new SqlParameter[] {oSqlParam1, oSqlParam2, oSqlParam3, oSqlParam4, oSqlParam5 });

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

        public bool Delete(int idProjet)
        {
            try
            {
                var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
                var oSqlParam = new SqlParameter("@Id", idProjet);
                var oSqlCommand = new SqlCommand("Delete projet Where idProjet = @Id");

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

        public int Insert(ProjetEntity projetEntity)
        {
            var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
            var oSqlparam1 = new SqlParameter("@idStatut", projetEntity.idStatut);
            var oSqlParam2 = new SqlParameter("@descriptif", projetEntity.descriptif);
            var oSqlParam3 = new SqlParameter("@date", projetEntity.dateCreation);
            var oSqlParam4 = new SqlParameter("@createur", projetEntity.createur);
            var oSqlParam5 = new SqlParameter("@etat", projetEntity.etatProjet);

            oSqlConnection.Open();
            var oSqlTransaction = oSqlConnection.BeginTransaction();
            try
            {
                var oSqlCommand = new SqlCommand("Insert Into projet(idStatut,descriptif, dateCreation, createur, etatProjet) Values (@idStatut, @descriptif,@date, @createur, @etat); Select @@Identity;");

                oSqlCommand.Parameters.Add(oSqlparam1);
                oSqlCommand.Parameters.AddRange(new SqlParameter[] {oSqlParam2, oSqlParam3, oSqlParam4, oSqlParam5 });

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

        public ProjetEntity GetProjetByIdConnaissance(int idConnaissance)
        {

            var projet = new ProjetEntity();
            var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
            var oSqlCommand = new SqlCommand("Select * from projet as p inner join statutprojet as s on p.idstatut = s.idstatut where idProjet = @Id");
            var oSqlParam = new SqlParameter("@Id", idConnaissance);

            oSqlCommand.Parameters.Add(oSqlParam);
            oSqlConnection.Open();
            oSqlCommand.Connection = oSqlConnection;
            var oSqlDataReader = oSqlCommand.ExecuteReader();

            while (oSqlDataReader.Read())
            {
                projet.idProjet = (int)oSqlDataReader["idProjet"];
                projet.idStatut = (int)oSqlDataReader["idStatut"];
                projet.descriptif = (string)oSqlDataReader["descriptif"];
                projet.dateCreation = (DateTime)oSqlDataReader["dateCreation"];
                projet.createur = (string)oSqlDataReader["createur"];
                projet.etatProjet = (string)oSqlDataReader["etatProjet"];

            };
            oSqlDataReader.Close();
            oSqlConnection.Close();
            return projet;
        }
    }
}
