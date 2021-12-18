using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginUser
{
    class Usuario
    {
        public int id;
        public string nome;
        public string email;
        public string senha;


        public bool gravarUsuario()
        {
            Banco banco = new Banco();

            SqlConnection cn = banco.abrirConexao();
            SqlTransaction tran = cn.BeginTransaction();
            SqlCommand command = new SqlCommand();

            command.Connection = cn;
            command.Transaction = tran;
            command.CommandType = System.Data.CommandType.Text;


            command.CommandText = "insert into usuario values (@nome, @email, @senha)";
            command.Parameters.Add("@nome", SqlDbType.VarChar);
            command.Parameters.Add("@email", SqlDbType.VarChar);
            command.Parameters.Add("@senha", SqlDbType.VarChar);
            command.Parameters[0].Value = nome;
            command.Parameters[1].Value = email;
            command.Parameters[2].Value = senha;


            try
            {
                command.ExecuteNonQuery();
                tran.Commit();
                return true;

            }
            catch (Exception ex)
            {
                tran.Rollback();
                return false;
            }
            finally
            {
                banco.fecharConexao();
            }


            return true;
        }

        public Usuario consultaUsuario(string email)
        {
            Banco bd = new Banco();

            try
            {
                SqlConnection cn = bd.abrirConexao();
                SqlCommand command = new SqlCommand("select * from usuario ", cn);

                SqlDataReader reader = command.ExecuteReader();

         
                while (reader.Read())
                {
                    if (reader.GetString(2).Equals(email))
                    {
                        this.id = reader.GetInt32(0);
                        nome = reader.GetString(1);
                        email = reader.GetString(2);
                        senha = reader.GetString(3);

                        return this;
                    }
                }
                return null;

            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                bd.fecharConexao();
            }
        }


        public bool validarUsuario(string email, string senha)
        {

            Usuario u = new Usuario();
            u = consultaUsuario(email);

            Simetrico s = new Simetrico();
            


            // Utilizar criptografia
            if (senha.Equals(s.DecryptData(u.senha, "UsEr")))
            {
                return true;
            } else
            {
                return false;
            }
            
        }




    }
}
