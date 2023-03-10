using System.Data.SqlClient;

namespace Stacktim.Model
{
    public class RessourceRepo
    {

        private readonly IConfiguration? _configuration;

        public RessourceRepo(IConfiguration? configuration)
        {
            this._configuration = configuration;
        }

        public RessourceEntity GetRessById(int idRessource)
        {


            var Ressource = new RessourceEntity();
            var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
            var oSqlCommand = new SqlCommand("Select * from ressource as ress inner join typeressource as typer on ress.idtyper = typer.idtyper \r\ninner join connaissance as co on ress.idconnaissance = co.idconnaissance where idressource = @Id");
            var oSqlParam = new SqlParameter("@Id", idRessource);

            oSqlCommand.Parameters.Add(oSqlParam);
            oSqlConnection.Open();
            oSqlCommand.Connection = oSqlConnection;
            var oSqlDataReader = oSqlCommand.ExecuteReader();

            while (oSqlDataReader.Read())
            {
                Ressource.idRessource = (int)oSqlDataReader["idressource"];
                Ressource.idTypeR = (int)oSqlDataReader["idTypeR"];
                Ressource.idConnaissance = (int)oSqlDataReader["idConnaissance"];
                Ressource.typeRess = (string)oSqlDataReader["typeRessource"];
                Ressource.datePublication = (DateTime)oSqlDataReader["datePublication"];
                Ressource.createur = (string)oSqlDataReader["createur"];
                Ressource.contenu = (string)oSqlDataReader["contenu"];
            };
            oSqlDataReader.Close();
            oSqlConnection.Close();
            return Ressource;
        }

        public List<RessourceEntity> Getall()
        {
            var olistRessource = new List<RessourceEntity>();
            var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
            var oSqlCommand = new SqlCommand("Select * From ressource");
            oSqlConnection.Open();
            oSqlCommand.Connection = oSqlConnection;
            var oSqlDataReader = oSqlCommand.ExecuteReader();
            var ressource = new RessourceEntity();
            var lRead = oSqlDataReader.Read();
            while (lRead)
            {
                ressource = new RessourceEntity
                {

                    idRessource = (int)oSqlDataReader["idressource"],
                    idTypeR = (int)oSqlDataReader["idTypeR"],
                    idConnaissance = (int)oSqlDataReader["idConnaissance"],
                    typeRess = (string)oSqlDataReader["typeRessource"],
                    datePublication = (DateTime)oSqlDataReader["datePublication"],
                    createur = (string)oSqlDataReader["createur"],
                    contenu = (string)oSqlDataReader["contenu"],
            };
                while ((int)oSqlDataReader["idRessource"] == ressource.idRessource)
                {

                    lRead = oSqlDataReader.Read();
                    if (!lRead) break;
                }
                olistRessource.Add(ressource);
            };

            oSqlDataReader.Close();
            oSqlConnection.Close();
            return olistRessource;
        }

        public bool Update(RessourceEntity ressourceEntity)
        {
            try
            {
                var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
                var oSqlParam = new SqlParameter("@idRessource", ressourceEntity.idRessource);
                var oSqlParam2 = new SqlParameter("@idTypeR", ressourceEntity.idTypeR);
                var oSqlParam3 = new SqlParameter("@idConnaissance", ressourceEntity.idConnaissance);
                var oSqlParam4 = new SqlParameter("@typeRess", ressourceEntity.typeRess); 
                var oSqlParam5 = new SqlParameter("@datePub", ressourceEntity.datePublication);
                var oSqlParam6 = new SqlParameter("@createur", ressourceEntity.createur);
                var oSqlParam7 = new SqlParameter("@contenu", ressourceEntity.contenu);


                var oSqlCommand = new SqlCommand("Update ressource Set idTypeR = @idTypeR, idConnaissance = @idConnaissance,typeressource = @typeRess, datepublication = @datePub, createur = @createur, contenu = @contenu  Where idRessource = @idRessource");

                oSqlCommand.Parameters.Add(oSqlParam);
                oSqlCommand.Parameters.AddRange(new SqlParameter[] { oSqlParam2, oSqlParam3, oSqlParam4, oSqlParam5, oSqlParam6, oSqlParam7});

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

        public bool Delete(int idRessource)
        {
            try
            {
                var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
                var oSqlParam = new SqlParameter("@Id", idRessource);
                var oSqlCommand = new SqlCommand("Delete ressource Where idRessource = @Id");

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

        public int Insert(RessourceEntity ressourceEntity)
        {
            var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
            var oSqlParam = new SqlParameter("@idRessource", ressourceEntity.idRessource);
            var oSqlParam2 = new SqlParameter("@idTypeR", ressourceEntity.idTypeR);
            var oSqlParam3 = new SqlParameter("@idConnaissance", ressourceEntity.idConnaissance);
            var oSqlParam4 = new SqlParameter("@typeRess", ressourceEntity.typeRess);
            var oSqlParam5 = new SqlParameter("@datePub", ressourceEntity.datePublication);
            var oSqlParam6 = new SqlParameter("@createur", ressourceEntity.createur);
            var oSqlParam7 = new SqlParameter("@contenu", ressourceEntity.contenu);

            oSqlConnection.Open();
            var oSqlTransaction = oSqlConnection.BeginTransaction();
            try
            {
                var oSqlCommand = new SqlCommand("Insert Into ressource(idTypeR, idConnaissance, typeRessource, datePublication,createur, contenu) Values (@idTypeR, @idConnaissance, @typeRess, @datePub,@createur, @contenu); Select @@Identity;");

                oSqlCommand.Parameters.Add(oSqlParam);
                oSqlCommand.Parameters.AddRange(new SqlParameter[] {oSqlParam2 ,oSqlParam3, oSqlParam4, oSqlParam5, oSqlParam6,oSqlParam7 });

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
    }
}
