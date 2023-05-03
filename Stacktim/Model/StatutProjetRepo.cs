using System.Data.SqlClient;

namespace Stacktim.Model
{
    public class StatutProjetRepo
    {
        private readonly IConfiguration? _configuration;

        public StatutProjetRepo(IConfiguration? configuration)
        {
            this._configuration = configuration;
        }

        public List<StatutProjetEntity> Getall()
        {
            var oListStatut = new List<StatutProjetEntity>();
            var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
            var oSqlCommand = new SqlCommand("Select * From statutProjet");
            oSqlConnection.Open();
            oSqlCommand.Connection = oSqlConnection;
            var oSqlDataReader = oSqlCommand.ExecuteReader();
            var statutP = new StatutProjetEntity();
            var lRead = oSqlDataReader.Read();
            while (lRead)
            {
                statutP = new StatutProjetEntity
                {
                    idStatut = (int)oSqlDataReader["idStatut"],
                    nom = (string)oSqlDataReader["nom"],
                };
                while ((int)oSqlDataReader["idTypeR"] == statutP.idStatut)
                {

                    lRead = oSqlDataReader.Read();
                    if (!lRead) break;
                }
                oListStatut.Add(statutP);
            };

            oSqlDataReader.Close();
            oSqlConnection.Close();
            return oListStatut;
        }

    }
}
