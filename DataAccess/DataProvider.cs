using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace DataAccess
{
    public static class DataProvider
    {

        public static string servername = "gcomm.online";
        //public static string servername = "WIN-4V77H9KRLK8";
        //public static string servername = "WINDOWS-6VCPU-1";


        public static DataSet Excute(RequestCollection requests, string session)
        {

            var response = new DataSet();

            SqlTransaction transaction = null;

            string connectString = string.Format(@"Server={0};User Id=sa;
Password=@Dkcnyh20081992;", servername);
            using (SqlConnection connection = new SqlConnection(connectString))
            {
                try
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();
                    transaction = connection.BeginTransaction();

                    foreach (var request in requests)
                    {
                        var category = request["Attributes"]["Category"].Value;
                        var command = request["Attributes"]["Command"].Value;
                        var parameters = request["Parameters"];

                        if (1 == 1 || command.StartsWith("sp_"))
                        {
                            if (ProcessNative(session, category, command, parameters, response) == false)
                            {
                                ProcessSql(connection, transaction, session, category, command, parameters, response);
                            }
                        }
                    }

                    transaction.Commit();

                }
                catch (Exception e)
                {
                    var table = new DataTable("Error");
                    table.Columns.Add("Message");
                    table.Columns.Add("Source");
                    table.Columns.Add("StackTrace");
                    table.Columns.Add("HelpLink");

                    var row = table.NewRow();
                    row["Message"] = e.Message;
                    row["Source"] = e.Source;
                    row["StackTrace"] = e.StackTrace;
                    row["HelpLink"] = e.HelpLink;

                    table.Rows.Add(row);

                    response = new DataSet();
                    response.Tables.Add(table);
                }
                finally
                {
                    connection.Close();
                }

            }



            return response;
        }
        public static async Task<DataSet> ExcuteAsync(RequestCollection requests, string session)
        {

            var response = new DataSet();

            SqlTransaction transaction = null;
            string connectString = string.Format(@"Server={0};User Id=sa;
Password=@Dkcnyh20081992;", servername);


            using (SqlConnection connection = new SqlConnection(connectString))
            {
                try
                {
                    await connection.OpenAsync();
                    transaction = connection.BeginTransaction();

                    foreach (var request in requests)
                    {
                        var category = request["Attributes"]["Category"].Value;
                        var command = request["Attributes"]["Command"].Value;
                        var parameters = request["Parameters"];

                        if (1 == 1 || command.StartsWith("sp_"))
                        {
                            if (ProcessNative(session, category, command, parameters, response) == false)
                            {
                                await ProcessSqlAsync(connection, transaction, session, category, command, parameters, response);
                            }
                        }
                    }

                    transaction.Commit();
                    connection.Close();

                }
                catch (Exception e)
                {
                    var table = new DataTable("Error");
                    table.Columns.Add("Message");
                    table.Columns.Add("Source");
                    table.Columns.Add("StackTrace");
                    table.Columns.Add("HelpLink");

                    var row = table.NewRow();
                    row["Message"] = e.Message;
                    row["Source"] = e.Source;
                    row["StackTrace"] = e.StackTrace;
                    row["HelpLink"] = e.HelpLink;

                    table.Rows.Add(row);

                    response = new DataSet();
                    response.Tables.Add(table);
                }
                finally
                {
                    connection.Close();
                }

            }



            return response;
        }
        private static void ProcessSql(SqlConnection connection, SqlTransaction transaction, string session, string category, string command, NameValueCollection parameters, DataSet response)
        {
            using (SqlCommand cmd = new SqlCommand(category + ".." + command, connection))
            {
                cmd.Transaction = transaction;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SessionID", SqlDbType.VarChar).Value = session;
                cmd.CommandTimeout = 120;
                foreach (var parameter in parameters)
                {
                    var name = parameter.Name;
                    var value = parameter.Value;

                    name = Misc.SafeSqlName(name);

                    if (parameter.IsNull)
                    {
                        value = null;
                    }

                    cmd.Parameters.Add("@" + name, SqlDbType.NVarChar).Value = value;
                }
                //using (var reader = await cmd.ExecuteReaderAsync())
                //{
                //    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                //    dataAdapter.Fill(reader);
                //    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                //    {
                //        var result = new DataSet();

                //        AppendDataSet(response, result);
                //    }
                //}

                //using (var dataAdapter = await cmd.ExecuteReaderAsync())
                //{
                //    var result = new DataSet();
                //    dataAdapter.Fill(result);
                //    AppendDataSet(response, result);
                //}

                //using (SqlDataAdapter dataAdapter = await cmd.ExecuteReaderAsync())
                //{
                //    var result = new DataSet();
                //    dataAdapter.Fill(result);
                //    AppendDataSet(response, result);
                //}

                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                {
                    var result = new DataSet();
                    dataAdapter.Fill(result);
                    AppendDataSet(response, result);
                    //dataAdapter.Fill(result);
                    //AppendDataSet(response, result);
                }
            }
        }
        private static async Task ProcessSqlAsync(SqlConnection connection, SqlTransaction transaction, string session, string category, string command, NameValueCollection parameters, DataSet response)
        {
            using (SqlCommand cmd = new SqlCommand(category + ".." + command, connection))
            {
                cmd.Transaction = transaction;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 120;
                cmd.Parameters.Add("@SessionID", SqlDbType.VarChar).Value = session;

                foreach (var parameter in parameters)
                {
                    var name = parameter.Name;
                    var value = parameter.Value;

                    name = Misc.SafeSqlName(name);

                    if (parameter.IsNull)
                    {
                        value = null;
                    }

                    cmd.Parameters.Add("@" + name, SqlDbType.NVarChar).Value = value;
                }
                //using (var reader = await cmd.ExecuteReaderAsync())
                //{
                //    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                //    dataAdapter.Fill(reader);
                //    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                //    {
                //        var result = new DataSet();

                //        AppendDataSet(response, result);
                //    }
                //}

                //using (var dataAdapter = await cmd.ExecuteReaderAsync())
                //{
                //    var result = new DataSet();
                //    dataAdapter.Fill(result);
                //    AppendDataSet(response, result);
                //}

                //using (SqlDataAdapter dataAdapter = await cmd.ExecuteReaderAsync())
                //{
                //    var result = new DataSet();
                //    dataAdapter.Fill(result);
                //    AppendDataSet(response, result);
                //}

                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                {
                    var result = new DataSet();
                    dataAdapter.Fill(result);
                    AppendDataSet(response, result);
                    //await Task.Run(() => dataAdapter.Fill(result));
                    //await Task.Run(() => AppendDataSet(response, result));

                    //dataAdapter.Fill(result);
                    //AppendDataSet(response, result);
                }
            }
        }
        private static bool ProcessNative(string session, string category, string command, NameValueCollection parameters, DataSet response)
        {
            return false;
        }


        private static void AppendDataSet(DataSet target, DataSet source)
        {
            var source_tables = source.Tables.Cast<DataTable>().ToArray();

            foreach (DataTable table in source_tables)
            {
                source.Tables.Remove(table);

                string tableName = "Table";
                if (target.Tables.Count > 0)
                    tableName += target.Tables.Count;

                table.TableName = tableName;
                target.Tables.Add(table);

            }
        }

    }
}
