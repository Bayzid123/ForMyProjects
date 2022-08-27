using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ApiForMyProjects.DTO;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ApiForMyProjects.Helper
{
    public class StoreProcedureCall
    {
        // public readonly Context _contextW;
        // public readonly Context _contextR;
        private static string _connection = Connection.iBOS;
        public StoreProcedureCall()
        {
            // _contextW = new Context();
            // _contextR = new Context();
        }

        public static List<KeyValue> PostJson<T>(string StoredProcedure, string InputJson, List<T> JsonObject, List<KeyValue> Output)
        {
            var json = JsonConvert.SerializeObject(JsonObject);

            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            // var _contextW = new Context();

            // conn.ConnectionString = _contextW.Database.GetConnectionString();
            conn.ConnectionString = _connection;
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = StoredProcedure;

            cmd.Parameters.AddWithValue(InputJson, json);

            foreach (var o in Output)
            {
                var typeName = o.Key.GetType().Name;
                SqlDbType type = SqlDbType.VarChar;

                if (typeName == "String") type = SqlDbType.VarChar;
                else if (typeName == "Int32") type = SqlDbType.Int;

                cmd.Parameters.Add(o.Key, type, 100);
                cmd.Parameters[o.Key].Direction = ParameterDirection.Output;
            }

            conn.Open();
            int i = cmd.ExecuteNonQuery();

            foreach (var o in Output)
            {
                o.Value = cmd.Parameters[o.Key].Value;
            }

            conn.Close();

            return Output;
        }

        public static List<KeyValue> PostJson(string StoredProcedure, List<KeyValue> JsonObject, List<KeyValue> Output)
        {
            // var json = JsonConvert.SerializeObject(JsonObject);

            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            //var _contextW = new Context();

            conn.ConnectionString = _connection;
            // conn.ConnectionString = _contextW.Database.GetConnectionString();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = StoredProcedure;

            JsonObject.ForEach(x =>
            {
                var json = JsonConvert.SerializeObject(x.Value);
                var name = x.Key;
                cmd.Parameters.AddWithValue(name, json);
            });

            // cmd.Parameters.AddWithValue(InputJson, json);

            foreach (var o in Output)
            {
                var typeName = o.Key.GetType().Name;
                SqlDbType type = SqlDbType.VarChar;

                if (typeName == "String") type = SqlDbType.VarChar;
                else if (typeName == "Int32") type = SqlDbType.Int;

                cmd.Parameters.Add(o.Key, type, 100);
                cmd.Parameters[o.Key].Direction = ParameterDirection.Output;
            }

            conn.Open();
            int i = cmd.ExecuteNonQuery();

            foreach (var o in Output)
            {
                o.Value = cmd.Parameters[o.Key].Value;
            }

            conn.Close();

            return Output;
        }

        public static List<KeyValue> PostJsonWithParam<T>(string StoredProcedure, string InputJson, List<T> JsonObject, List<KeyValue> InputParam, List<KeyValue> Output)
        {
            var json = JsonConvert.SerializeObject(JsonObject);

            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            // var _contextW = new Context();

            conn.ConnectionString = _connection;
            // conn.ConnectionString = _contextW.Database.GetConnectionString();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = StoredProcedure;

            cmd.Parameters.AddWithValue(InputJson, json);

            foreach (var input in InputParam)
            {
                cmd.Parameters.AddWithValue(input.Key, input.Value);
            }

            foreach (var o in Output)
            {
                var typeName = o.Key.GetType().Name;
                SqlDbType type = SqlDbType.VarChar;

                if (typeName == "String") type = SqlDbType.VarChar;
                else if (typeName == "Int32") type = SqlDbType.Int;

                cmd.Parameters.Add(o.Key, type, 100);
                cmd.Parameters[o.Key].Direction = ParameterDirection.Output;
            }

            conn.Open();
            int i = cmd.ExecuteNonQuery();

            foreach (var o in Output)
            {
                o.Value = cmd.Parameters[o.Key].Value;
            }

            conn.Close();

            return Output;
        }

        public static List<KeyValue> PostJsonWithParam(string StoredProcedure, List<KeyValue> JsonObject, List<KeyValue> InputParam, List<KeyValue> Output)
        {
            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            // var _contextW = new Context();

            conn.ConnectionString = _connection;
            // conn.ConnectionString = _contextW.Database.GetConnectionString();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = StoredProcedure;

            foreach (var input in JsonObject)
            {
                var json = JsonConvert.SerializeObject(input.Value);
                cmd.Parameters.AddWithValue(input.Key, json);
            }

            foreach (var input in InputParam)
            {
                cmd.Parameters.AddWithValue(input.Key, input.Value);
            }

            foreach (var o in Output)
            {
                var typeName = o.Key.GetType().Name;
                SqlDbType type = SqlDbType.VarChar;

                if (typeName == "String") type = SqlDbType.VarChar;
                else if (typeName == "Int32") type = SqlDbType.Int;

                cmd.Parameters.Add(o.Key, type, 100);
                cmd.Parameters[o.Key].Direction = ParameterDirection.Output;
            }

            conn.Open();
            int i = cmd.ExecuteNonQuery();

            foreach (var o in Output)
            {
                o.Value = cmd.Parameters[o.Key].Value;
            }

            conn.Close();

            return Output;
        }

        public static List<KeyValue> PostParam(string StoredProcedure, List<KeyValue> InputParam, List<KeyValue> Output)
        {
            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            // var _contextW = new Context();

            conn.ConnectionString = _connection;
            // conn.ConnectionString = _contextW.Database.GetConnectionString();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = StoredProcedure;

            // foreach (var input in JsonObject)
            // {
            //     var json = JsonConvert.SerializeObject(input.Value);
            //     cmd.Parameters.AddWithValue(input.Key, json);
            // }

            foreach (var input in InputParam)
            {
                cmd.Parameters.AddWithValue(input.Key, input.Value);
            }

            foreach (var o in Output)
            {
                var typeName = o.Key.GetType().Name;
                SqlDbType type = SqlDbType.VarChar;

                if (typeName == "String") type = SqlDbType.VarChar;
                else if (typeName == "Int32") type = SqlDbType.Int;

                cmd.Parameters.Add(o.Key, type, 100);
                cmd.Parameters[o.Key].Direction = ParameterDirection.Output;
            }

            conn.Open();
            int i = cmd.ExecuteNonQuery();

            foreach (var o in Output)
            {
                o.Value = cmd.Parameters[o.Key].Value;
            }

            conn.Close();

            return Output;
        }

        public static KeyValue PostJson<T>(string StoredProcedure, string InputJson, List<T> JsonObject, KeyValue Output)
        {
            var json = JsonConvert.SerializeObject(JsonObject);

            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            // var _contextW = new Context();

            conn.ConnectionString = _connection;
            // conn.ConnectionString = _contextW.Database.GetConnectionString();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = StoredProcedure;

            cmd.Parameters.AddWithValue(InputJson, json);

            var typeName = Output.Key.GetType().Name;
            SqlDbType type = SqlDbType.VarChar;

            // if (typeName == "String") type = SqlDbType.VarChar;
            if (typeName == "Int32") type = SqlDbType.Int;

            cmd.Parameters.Add(Output.Key, type, 100);
            cmd.Parameters[Output.Key].Direction = ParameterDirection.Output;


            conn.Open();
            int i = cmd.ExecuteNonQuery();

            Output.Value = cmd.Parameters[Output.Key].Value;

            conn.Close();

            return Output;
        }

        public static List<T> GetDataTable<T>(string StoredProcedure, List<KeyValue> InputParam)
        {
            DataTable dt = new DataTable();
            // var _contextW = new Context();

            using (var connection = new SqlConnection(_connection))
            {
                string sql = StoredProcedure;
                using (SqlCommand sqlCmd = new SqlCommand(sql, connection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    foreach (var input in InputParam)
                    {
                        sqlCmd.Parameters.AddWithValue(input.Key, input.Value);
                    }

                    connection.Open();
                    using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
                    {
                        sqlAdapter.Fill(dt);
                    }
                    connection.Close();
                }

            }

            return DataTableToList.ConvertDataTable<T>(dt);
        }

        public static DataTable GetDataTableUnknown(string StoredProcedure, List<KeyValue> InputParam)
        {
            DataTable dt = new DataTable();
            // var _contextW = new Context();

            using (var connection = new SqlConnection(_connection))
            {
                string sql = StoredProcedure;
                using (SqlCommand sqlCmd = new SqlCommand(sql, connection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    foreach (var input in InputParam)
                    {
                        sqlCmd.Parameters.AddWithValue(input.Key, input.Value);
                    }

                    connection.Open();
                    using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
                    {
                        sqlAdapter.Fill(dt);
                    }
                    connection.Close();
                }

            }

            return dt;
            // return DataTableToList.ConvertDataTable<T>(dt);
        }
    }
}
