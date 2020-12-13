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
    public class SeanceServices
    {
        private TheaterServices _theaterServices;
        private CinemaHallServices _hallServices;
        private MovieServices _movieServices;
        public string connect = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        private SqlDataAdapter _adapter;
        private DataSet _ds;

        public void GenereateSeances(DateTime date, int id)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SeanceProcedure", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@id_theater", id);
                cmd.ExecuteNonQuery();
            }
        }

        public IList<SeanceModel> GetSeanceList()
        {
            _movieServices = new MovieServices();
            _hallServices = new CinemaHallServices();
            _theaterServices = new TheaterServices();
            IList<SeanceModel> SeanceList = new List<SeanceModel>();
            _ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetAllSeance", con);
                cmd.CommandType = CommandType.StoredProcedure;
                _adapter = new SqlDataAdapter(cmd);
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0)
                {
                    for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                    {
                        SeanceModel obj = new SeanceModel();
                        obj.Id = Convert.ToInt32(_ds.Tables[0].Rows[i]["Id"]);
                        obj.Theater = _theaterServices.GetTheaterBySeance(obj.Id);
                        obj.CinemaHall = _hallServices.GetCinemaHallBySeance(obj.Id);
                        obj.Movie = _movieServices.GetMovieBySeance(obj.Id);
                        obj.Date = Convert.ToDateTime(_ds.Tables[0].Rows[i]["Date"]);
                        obj.Time = Convert.ToDateTime((_ds.Tables[0].Rows[i]["Time"]).ToString());
                        SeanceList.Add(obj);
                    }
                }

            }

            return SeanceList;
        }


        public void InsertSeance(SeanceModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("AddSeance", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_cinema_hall", model.CinemaHall.Id);
                cmd.Parameters.AddWithValue("@id_movie", model.Movie.Id);
                cmd.Parameters.AddWithValue("@date", model.Date);
                cmd.Parameters.AddWithValue("@time", model.Time);
                cmd.ExecuteNonQuery();
            }
        }

        public SeanceModel GetSeanceById(int id)
        {
            _theaterServices = new TheaterServices();
            _hallServices = new CinemaHallServices();
            _movieServices = new MovieServices();
            var model = new SeanceModel();
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetSeanceById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                _adapter = new SqlDataAdapter(cmd);
                _ds = new DataSet();
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    model.Id = Convert.ToInt32(_ds.Tables[0].Rows[0]["Id"]);
                    model.Theater = _theaterServices.GetTheaterBySeance(model.Id);
                    model.CinemaHall = _hallServices.GetCinemaHallById(model.Id);
                    model.Movie = _movieServices.GetMovieBySeance(model.Id);
                    model.Date = Convert.ToDateTime(_ds.Tables[0].Rows[0]["Date"]);
                    model.Time = Convert.ToDateTime((_ds.Tables[0].Rows[0]["Time"]).ToString());
                }
            }
            return model;
        }

        public void UpdateSeance(SeanceModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateSeance", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", model.Id);
                cmd.Parameters.AddWithValue("@id_theater", model.Theater.Id);
                cmd.Parameters.AddWithValue("@id_hall", model.CinemaHall.Id);
                cmd.Parameters.AddWithValue("@id_movie", model.Movie.Id);
                cmd.Parameters.AddWithValue("@date", model.Date);
                cmd.Parameters.AddWithValue("@time", model.Time);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteSeance(int id)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DeleteSeance", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public IList<SeanceModel> GetSeanceForTheater(int id)
        {
            _movieServices = new MovieServices();
            _hallServices = new CinemaHallServices();
            _theaterServices = new TheaterServices();
            IList<SeanceModel> SeanceList = new List<SeanceModel>();
            _ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetSeanceForTheater", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                _adapter = new SqlDataAdapter(cmd);
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0)
                {
                    for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                    {
                        SeanceModel obj = new SeanceModel();
                        obj.Id = Convert.ToInt32(_ds.Tables[0].Rows[i]["Id"]);
                        obj.Theater = _theaterServices.GetTheaterBySeance(obj.Id);
                        obj.CinemaHall = _hallServices.GetCinemaHallBySeance(obj.Id);
                        obj.Movie = _movieServices.GetMovieBySeance(obj.Id);
                        obj.Date = Convert.ToDateTime(_ds.Tables[0].Rows[i]["Date"]);
                        obj.Time = Convert.ToDateTime((_ds.Tables[0].Rows[i]["Time"]).ToString());
                        SeanceList.Add(obj);
                    }
                }
            }
            return SeanceList;
        }
    }
}