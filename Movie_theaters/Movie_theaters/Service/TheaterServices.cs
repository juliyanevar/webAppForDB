using Movie_theaters.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;

namespace Movie_theaters.Service
{
    public class TheaterServices
    {
        public string connect = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        private SqlDataAdapter _adapter;
        private DataSet _ds;

        public IList<TheaterModel> GetTheaterList()
        {
            IList<TheaterModel> theaterList = new List<TheaterModel>();
            _ds = new DataSet(); 

            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetAllMovieTheater", con);
                cmd.CommandType = CommandType.StoredProcedure;
                _adapter = new SqlDataAdapter(cmd);
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0)
                {
                    for(int i=0; i<_ds.Tables[0].Rows.Count; i++)
                    {
                        TheaterModel obj = new TheaterModel();
                        obj.Id = Convert.ToInt32(_ds.Tables[0].Rows[i]["Id"]);
                        obj.Name = Convert.ToString(_ds.Tables[0].Rows[i]["Name"]);
                        obj.Adress = Convert.ToString(_ds.Tables[0].Rows[i]["Address"]);
                        obj.CountHalls = Convert.ToInt32(_ds.Tables[0].Rows[i]["Count_cinema_hall"]);
                        theaterList.Add(obj);
                    }
                }

            }

                return theaterList;
        }


        public void InsertTheater(TheaterModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("AddMovieTheater", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", model.Name);
                cmd.Parameters.AddWithValue("@address", model.Adress);
                cmd.Parameters.AddWithValue("@count_hall", model.CountHalls);
                cmd.ExecuteNonQuery();
            }
        }

        public TheaterModel GetTheaterById(int id)
        {
            var model = new TheaterModel();
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetTheaterById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_movie", id);
                _adapter = new SqlDataAdapter(cmd);
                _ds = new DataSet();
                _adapter.Fill(_ds);
                if(_ds.Tables.Count>0 && _ds.Tables[0].Rows.Count > 0)
                {
                    model.Id = Convert.ToInt32(_ds.Tables[0].Rows[0]["Id"]);
                    model.Name = Convert.ToString(_ds.Tables[0].Rows[0]["Name"]);
                    model.Adress = Convert.ToString(_ds.Tables[0].Rows[0]["Address"]);
                    model.CountHalls = Convert.ToInt32(_ds.Tables[0].Rows[0]["Count_cinema_hall"]);
                }
            }
            return model;
        }

        public void UpdateTheater(TheaterModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateMovieTheater", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", model.Id);
                cmd.Parameters.AddWithValue("@name", model.Name);
                cmd.Parameters.AddWithValue("@address", model.Adress);
                cmd.Parameters.AddWithValue("@countHall", model.CountHalls);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteTheater(int id)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DeleteMovieTheater", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public TheaterModel GetTheaterByCinemaHall(int id)
        {
            var model = new TheaterModel();
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetTheaterByCinemaHall", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_hall", id);
                _adapter = new SqlDataAdapter(cmd);
                _ds = new DataSet();
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    model.Id = Convert.ToInt32(_ds.Tables[0].Rows[0]["Id"]);
                    model.Name = Convert.ToString(_ds.Tables[0].Rows[0]["Name"]);
                    model.Adress = Convert.ToString(_ds.Tables[0].Rows[0]["Address"]);
                    model.CountHalls = Convert.ToInt32(_ds.Tables[0].Rows[0]["Count_cinema_hall"]);
                }
            }
            return model;
        }

        public TheaterModel GetTheaterBySeance(int id)
        {
            var model = new TheaterModel();
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetTheaterBySeance", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                _adapter = new SqlDataAdapter(cmd);
                _ds = new DataSet();
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    model.Id = Convert.ToInt32(_ds.Tables[0].Rows[0]["Id"]);
                    model.Name = Convert.ToString(_ds.Tables[0].Rows[0]["Name"]);
                    model.Adress = Convert.ToString(_ds.Tables[0].Rows[0]["Address"]);
                    model.CountHalls = Convert.ToInt32(_ds.Tables[0].Rows[0]["Count_cinema_hall"]);
                }
            }
            return model;
        }   
    }
}