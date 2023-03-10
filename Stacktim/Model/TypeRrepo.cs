using System.Data.SqlClient;

namespace Stacktim.Model
{
    public class TypeRrepo
    {

        private readonly IConfiguration? _configuration;

        public TypeRrepo(IConfiguration? configuration)
        {
            this._configuration = configuration;
        }

        public TypeREntity GetTypeRById(int idTypeR)
        {

            var TypeR = new TypeREntity();
            var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
            var oSqlCommand = new SqlCommand("Select * from typeRessource where idTypeR = @Id");
            var oSqlParam = new SqlParameter("@Id", idTypeR);

            oSqlCommand.Parameters.Add(oSqlParam);
            oSqlConnection.Open();
            oSqlCommand.Connection = oSqlConnection;
            var oSqlDataReader = oSqlCommand.ExecuteReader();

            while (oSqlDataReader.Read())
            {
                TypeR.idTypeR = (int)oSqlDataReader["idtyper"];
                TypeR.descriptif = (string)oSqlDataReader["descriptif"];
                TypeR.image = (string)oSqlDataReader["image"];
            };
            oSqlDataReader.Close();
            oSqlConnection.Close();
            return TypeR;
        }

        public List<TypeREntity> Getall()
        {
            var oListType = new List<TypeREntity>();
            var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
            var oSqlCommand = new SqlCommand("Select * From typeRessource");
            oSqlConnection.Open();
            oSqlCommand.Connection = oSqlConnection;
            var oSqlDataReader = oSqlCommand.ExecuteReader();
            var typeR = new TypeREntity();
            var lRead = oSqlDataReader.Read();
            while (lRead)
            {
                typeR = new TypeREntity
                {
                    idTypeR = (int)oSqlDataReader["idTypeR"],
                    descriptif = (string)oSqlDataReader["descriptif"],
                    image = (string)oSqlDataReader["image"]
                };
                while ((int)oSqlDataReader["idTypeR"] == typeR.idTypeR)
                {

                    lRead = oSqlDataReader.Read();
                    if (!lRead) break;
                }
                oListType.Add(typeR);
            };

            oSqlDataReader.Close();
            oSqlConnection.Close();
            return oListType;
        }

        public bool Update(TypeREntity typeREntity)
        {
            try
            {
                var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
                var oSqlParam = new SqlParameter("@idType", typeREntity.idTypeR);
                var oSqlParam2 = new SqlParameter("@descriptif", typeREntity.descriptif);
                var oSqlParam3 = new SqlParameter("@image", typeREntity.image);
                var oSqlCommand = new SqlCommand("Update typeRessource Set descriptif = @descriptif, image = @image  Where idTypeR = @idType");

                oSqlCommand.Parameters.Add(oSqlParam);
                oSqlCommand.Parameters.AddRange(new SqlParameter[] { oSqlParam2, oSqlParam3 });

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

        public bool Delete(int idTypeR)
        {
            try
            {
                var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
                var oSqlParam = new SqlParameter("@Id", idTypeR);
                var oSqlCommand = new SqlCommand("Delete typeRessource Where idTypeR = @Id");

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

        public int Insert(TypeREntity typeREntity)
        {
            var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
            var oSqlParam2 = new SqlParameter("@descriptif", typeREntity.descriptif);
            var oSqlParam3 = new SqlParameter("@image", typeREntity.image);

            oSqlConnection.Open();
            var oSqlTransaction = oSqlConnection.BeginTransaction();
            try
            {
                var oSqlCommand = new SqlCommand("Insert Into typeRessource(descriptif, image) Values (@descriptif, @image); Select @@Identity;");

                oSqlCommand.Parameters.Add(oSqlParam2);
                oSqlCommand.Parameters.AddRange(new SqlParameter[] {oSqlParam3});

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
