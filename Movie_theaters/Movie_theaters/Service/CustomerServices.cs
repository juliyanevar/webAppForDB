using Movie_theaters.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Movie_theaters.Service
{
    public class CustomerServices
    {
        public string connect = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        private SqlDataAdapter _adapter;
        private DataSet _ds;

        public IList<CustomerModel> GetCustomerList()
        {
            IList<CustomerModel> customerList = new List<CustomerModel>();
            _ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetAllCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                _adapter = new SqlDataAdapter(cmd);
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0)
                {
                    for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                    {
                        CustomerModel obj = new CustomerModel();
                        obj.Id = Convert.ToInt32(_ds.Tables[0].Rows[i]["Id"]);
                        obj.Login = Convert.ToString(_ds.Tables[0].Rows[i]["Login"]);
                        obj.Password = Convert.ToString(_ds.Tables[0].Rows[i]["Password"]);
                        obj.Email = Convert.ToString(_ds.Tables[0].Rows[i]["Email"]);
                        obj.Role = Convert.ToString(_ds.Tables[0].Rows[i]["Role"]);
                        customerList.Add(obj);
                    }
                }

            }

            return customerList;
        }

        public void InsertCustomer(CustomerModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("AddUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@login", model.Login);
                cmd.Parameters.AddWithValue("@password", model.Password);
                cmd.Parameters.AddWithValue("@email", model.Email);
                cmd.ExecuteNonQuery();
            }
        }

        public CustomerModel GetCustomerById(int id)
        {
            var model = new CustomerModel();
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetCustomerById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                _adapter = new SqlDataAdapter(cmd);
                _ds = new DataSet();
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    model.Id = Convert.ToInt32(_ds.Tables[0].Rows[0]["Id"]);
                    model.Login = Convert.ToString(_ds.Tables[0].Rows[0]["Login"]);
                    model.Password = Convert.ToString(_ds.Tables[0].Rows[0]["Password"]);
                    model.Email = Convert.ToString(_ds.Tables[0].Rows[0]["Email"]);
                    model.Role = Convert.ToString(_ds.Tables[0].Rows[0]["Role"]);
                }
            }
            return model;
        }

        public void UpdateCustomer(CustomerModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", model.Id);
                cmd.Parameters.AddWithValue("@login", model.Login);
                cmd.Parameters.AddWithValue("@password", model.Password);
                cmd.Parameters.AddWithValue("@email", model.Email);
                cmd.Parameters.AddWithValue("@role", model.Role);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteCustomer(int id)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DeleteCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public int LoginExists(string login)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("LoginExists", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@login", login);
                _adapter = new SqlDataAdapter(cmd);
                _ds = new DataSet();
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    result = Convert.ToInt32(_ds.Tables[0].Rows[0]["Id"]);
                }
            }
            return result;
        }

        public int EmailExists(string email)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("EmailExists", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", email);
                _adapter = new SqlDataAdapter(cmd);
                _ds = new DataSet();
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    result = Convert.ToInt32(_ds.Tables[0].Rows[0]["Id"]);
                }
            }
            return result;
        }

        public int Authorization(CustomerModel model)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Authorization", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@login", model.Login);
                cmd.Parameters.AddWithValue("@password", model.Password);
                _adapter = new SqlDataAdapter(cmd);
                _ds = new DataSet();
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    result = Convert.ToInt32(_ds.Tables[0].Rows[0]["Id"]);
                }
            }
            return result;
        }

        public string GetRoleByLogin(string login)
        {
            string role = null;
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetRoleByLogin", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@login", login);
                _adapter = new SqlDataAdapter(cmd);
                _ds = new DataSet();
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    role = Convert.ToString(_ds.Tables[0].Rows[0]["Role"]);
                }
            }
            return role;
        }

        public int GetIdByLogin(string login)
        {
            int id = 0;
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetIdByLogin", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@login", login);
                _adapter = new SqlDataAdapter(cmd);
                _ds = new DataSet();
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    id = Convert.ToInt32(_ds.Tables[0].Rows[0]["Id"]);
                }
            }
            return id;
        }
    }
}