using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ML;

namespace BL
{
    public class Cliente
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    var query = "ClienteGetAll";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        DataTable dataTable = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in dataTable.Rows)
                            {
                                ML.Cliente cliente = new ML.Cliente();
                                cliente.IdCliente = int.Parse(row[0].ToString());
                                cliente.Nombre = row[1].ToString();

                                result.Objects.Add(cliente);
                            }
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.ErrorMessage = Ex.Message;
                result.Ex = Ex;
            }
            return result;
        }
        public static ML.Result GetById(int IdCliente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    var query = "ClienteGetById";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] parameters = new SqlParameter[1];

                        parameters[0] = new SqlParameter("@IdCliente", System.Data.SqlDbType.Int);
                        parameters[0].Value = IdCliente;

                        cmd.Parameters.AddRange(parameters);
                        DataTable dataTable = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count > 0)
                        {
                            DataRow row = dataTable.Rows[0];
                            ML.Cliente cliente = new ML.Cliente();
                            cliente.IdCliente = int.Parse(row[0].ToString());
                            cliente.Nombre = row[1].ToString();

                            result.Object = cliente;
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.ErrorMessage = Ex.Message;
                result.Ex = Ex;
            }
            return result;

        }
        public static ML.Result Add(ML.Cliente cliente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AGutierrezBABELEntities context = new DL.AGutierrezBABELEntities())
                {
                    var query = context.ClienteAdd(cliente.Nombre);
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }

                }
            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.ErrorMessage = Ex.Message;
                result.Ex = Ex;
            }
            return result;
        }
        public static ML.Result Update(ML.Cliente cliente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AGutierrezBABELEntities context = new DL.AGutierrezBABELEntities())
                {
                    var query = context.ClienteUpdate(cliente.IdCliente, cliente.Nombre);
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.ErrorMessage = Ex.Message;
                result.Ex = Ex;
            }
            return result;
        }
        public static ML.Result Delete(int IdCliente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AGutierrezBABELEntities context = new DL.AGutierrezBABELEntities())
                {
                    var query = context.ClienteDelete(IdCliente);
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception Ex)
            {
                result.ErrorMessage = Ex.Message;
                result.Ex = Ex;
                result.Correct = false;
            }
            return result;
        }
    }

}
