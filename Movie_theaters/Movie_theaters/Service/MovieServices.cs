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
    public class MovieServices
    {
        public string connect = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        private SqlDataAdapter _adapter;
        private DataSet _ds;
        private GenreServices _genreServices;


        public IList<MovieModel> GetMovieList()
        {
            _genreServices = new GenreServices();
            IList<MovieModel> movieList = new List<MovieModel>();
            _ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetAllMovies", con);
                cmd.CommandType = CommandType.StoredProcedure;
                _adapter = new SqlDataAdapter(cmd);
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0)
                {
                    for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                    {
                        MovieModel obj = new MovieModel();
                        obj.Id = Convert.ToInt32(_ds.Tables[0].Rows[i]["Id"]);
                        obj.Title = Convert.ToString(_ds.Tables[0].Rows[i]["Title"]);
                        obj.AgeBracket = Convert.ToString(_ds.Tables[0].Rows[i]["Age_bracket"]);
                        obj.Duration = Convert.ToInt32(_ds.Tables[0].Rows[i]["Duration"]);
                        obj.FirstDay = Convert.ToDateTime(_ds.Tables[0].Rows[i]["First_day_of_rental"]);
                        obj.LastDay = Convert.ToDateTime(_ds.Tables[0].Rows[i]["Last_day_of_rental"]);
                        obj.Genres = _genreServices.ListGenreByMovieId(obj.Id);
                        movieList.Add(obj);
                    }
                }

            }

            return movieList;
        }


        public void InsertMovie(MovieModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("AddMovie", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@title", model.Title);
                cmd.Parameters.AddWithValue("@age_bracket", model.AgeBracket);
                cmd.Parameters.AddWithValue("@duration", model.Duration);
                cmd.Parameters.AddWithValue("@first_day", model.FirstDay);
                cmd.Parameters.AddWithValue("@last_day", model.LastDay);
                cmd.ExecuteNonQuery();
            }
        }

        public MovieModel GetMovieById(int id)
        {
            _genreServices = new GenreServices();
            var model = new MovieModel();
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetMovieById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                _adapter = new SqlDataAdapter(cmd);
                _ds = new DataSet();
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    model.Id = Convert.ToInt32(_ds.Tables[0].Rows[0]["Id"]);
                    model.Title = Convert.ToString(_ds.Tables[0].Rows[0]["Title"]);
                    model.AgeBracket = Convert.ToString(_ds.Tables[0].Rows[0]["Age_bracket"]);
                    model.Duration = Convert.ToInt32(_ds.Tables[0].Rows[0]["Duration"]);
                    model.FirstDay = Convert.ToDateTime(_ds.Tables[0].Rows[0]["First_day_of_rental"]);
                    model.LastDay = Convert.ToDateTime(_ds.Tables[0].Rows[0]["Last_day_of_rental"]);
                    model.Genres = _genreServices.ListGenreByMovieId(model.Id);
                }
            }
            return model;
        }

        public void UpdateMovie(MovieModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateMovie", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", model.Id);
                cmd.Parameters.AddWithValue("@title", model.Title);
                cmd.Parameters.AddWithValue("@age_bracket", model.AgeBracket);
                cmd.Parameters.AddWithValue("@duration", model.Duration);
                cmd.Parameters.AddWithValue("@first_day", model.FirstDay);
                cmd.Parameters.AddWithValue("@last_day", model.LastDay);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteMovie(int id)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DeleteMovie", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }


        public void AddGenreForMovie(int id, string name)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("AddMovieGenre", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_movie", id);
                cmd.Parameters.AddWithValue("@name_genre", name);
                cmd.ExecuteNonQuery();
            }
        }

        public MovieModel GetMovieBySeance(int id)
        {
            _genreServices = new GenreServices();
            var model = new MovieModel();
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetMovieBySeance", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                _adapter = new SqlDataAdapter(cmd);
                _ds = new DataSet();
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    model.Id = Convert.ToInt32(_ds.Tables[0].Rows[0]["Id"]);
                    model.Title = Convert.ToString(_ds.Tables[0].Rows[0]["Title"]);
                    model.AgeBracket = Convert.ToString(_ds.Tables[0].Rows[0]["Age_bracket"]);
                    model.Duration = Convert.ToInt32(_ds.Tables[0].Rows[0]["Duration"]);
                    model.FirstDay = Convert.ToDateTime(_ds.Tables[0].Rows[0]["First_day_of_rental"]);
                    model.LastDay = Convert.ToDateTime(_ds.Tables[0].Rows[0]["Last_day_of_rental"]);
                    model.Genres = _genreServices.ListGenreByMovieId(model.Id);
                }
            }
            return model;
        }

    }
}
