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
    public class CinemaHallServices
    {
        private TheaterServices _theaterServices;
        public string connect = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        private SqlDataAdapter _adapter;
        private DataSet _ds;

        public IList<CinemaHallModel> GetCinemaHallList()
        {
            _theaterServices = new TheaterServices();
            IList<CinemaHallModel> cinemaHallList = new List<CinemaHallModel>();
            _ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetAllCinemaHall", con);
                cmd.CommandType = CommandType.StoredProcedure;
                _adapter = new SqlDataAdapter(cmd);
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0)
                {
                    for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                    {
                        CinemaHallModel obj = new CinemaHallModel();
                        obj.Id = Convert.ToInt32(_ds.Tables[0].Rows[i]["Id"]);
                        obj.Theater = _theaterServices.GetTheaterByCinemaHall(obj.Id);
                        obj.Number = Convert.ToInt32(_ds.Tables[0].Rows[i]["Number"]);
                        obj.CountPlaces = Convert.ToInt32(_ds.Tables[0].Rows[i]["Count_places"]);
                        obj.CountRows = Convert.ToInt32(_ds.Tables[0].Rows[i]["Count_rows"]);
                        cinemaHallList.Add(obj);
                    }
                }

            }

            return cinemaHallList;
        }


        public void InsertCinemaHall(CinemaHallModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("AddCinemaHall", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_movie_theater", model.Theater.Id);
                cmd.Parameters.AddWithValue("@number", model.Number);
                cmd.Parameters.AddWithValue("@count_places", model.CountPlaces);
                cmd.Parameters.AddWithValue("@count_rows", model.CountRows);
                cmd.ExecuteNonQuery();
            }
        }

        public CinemaHallModel GetCinemaHallById(int id)
        {
            _theaterServices = new TheaterServices();
            var model = new CinemaHallModel();
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetCinemaHallById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                _adapter = new SqlDataAdapter(cmd);
                _ds = new DataSet();
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    model.Id = Convert.ToInt32(_ds.Tables[0].Rows[0]["Id"]);
                    model.Theater = _theaterServices.GetTheaterByCinemaHall(model.Id);
                    model.Number = Convert.ToInt32(_ds.Tables[0].Rows[0]["Number"]);
                    model.CountPlaces = Convert.ToInt32(_ds.Tables[0].Rows[0]["Count_places"]);
                    model.CountRows = Convert.ToInt32(_ds.Tables[0].Rows[0]["Count_rows"]);
                }
            }
            return model;
        }

        public void UpdateCinemaHall(CinemaHallModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateCinemaHall", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", model.Id);
                cmd.Parameters.AddWithValue("@id_movie_theater", model.Theater.Id);
                cmd.Parameters.AddWithValue("@number", model.Number);
                cmd.Parameters.AddWithValue("@count_places", model.CountPlaces);
                cmd.Parameters.AddWithValue("@count_rows", model.CountRows);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteCinemaHall(int id)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DeleteCinemaHall", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public CinemaHallModel GetCinemaHallBySeance(int id)
        {
            _theaterServices = new TheaterServices();
            var model = new CinemaHallModel();
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetCinemaHallBySeance", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                _adapter = new SqlDataAdapter(cmd);
                _ds = new DataSet();
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    model.Id = Convert.ToInt32(_ds.Tables[0].Rows[0]["Id"]);
                    model.Theater = _theaterServices.GetTheaterByCinemaHall(model.Id);
                    model.Number = Convert.ToInt32(_ds.Tables[0].Rows[0]["Number"]);
                    model.CountPlaces = Convert.ToInt32(_ds.Tables[0].Rows[0]["Count_places"]);
                    model.CountRows = Convert.ToInt32(_ds.Tables[0].Rows[0]["Count_rows"]);
                }
            }
            return model;
        }
    }
}