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
    public class TicketServices
    {
        public string connect = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        private SqlDataAdapter _adapter;
        private DataSet _ds;

        public int IsAvailableTicket(TicketModel model)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("IsAvailableTicket", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_seance", model.Seance.Id);
                cmd.Parameters.AddWithValue("@row", model.Row);
                cmd.Parameters.AddWithValue("@place", model.Place);
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

        public void AddBooking(int idUser, int idTicket)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("AddBooking", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_user", idUser);
                cmd.Parameters.AddWithValue("@id_ticket", idTicket);
                cmd.ExecuteNonQuery();
            }
        }


        public IList<TicketModel> GetTicketForSeance(int idSeance)
        {
            SeanceServices seanceServices = new SeanceServices();
            IList<TicketModel> ticketList = new List<TicketModel>();
            _ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetTicketForSeance", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_seance", idSeance);
                _adapter = new SqlDataAdapter(cmd);
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0)
                {
                    for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                    {
                        TicketModel obj = new TicketModel();
                        obj.Id = Convert.ToInt32(_ds.Tables[0].Rows[i]["Id"]);
                        obj.Seance = seanceServices.GetSeanceById(Convert.ToInt32(_ds.Tables[0].Rows[i]["Seance_id"]));
                        obj.Cost = Convert.ToDouble(_ds.Tables[0].Rows[i]["Cost"]);
                        obj.Row = Convert.ToInt32(_ds.Tables[0].Rows[i]["Row"]);
                        obj.Place = Convert.ToInt32(_ds.Tables[0].Rows[i]["Place"]);
                        ticketList.Add(obj);
                    }
                }

            }

            return ticketList;
        }

        public TicketModel GetTicketById(int id)
        {
            SeanceServices seanceServices = new SeanceServices();
            var model = new TicketModel();
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetTicketById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                _adapter = new SqlDataAdapter(cmd);
                _ds = new DataSet();
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    model.Id = Convert.ToInt32(_ds.Tables[0].Rows[0]["Id"]);
                    model.Seance = seanceServices.GetSeanceById(Convert.ToInt32(_ds.Tables[0].Rows[0]["Seance_id"]));
                    model.Cost = Convert.ToDouble(_ds.Tables[0].Rows[0]["Cost"]);
                    model.Row = Convert.ToInt32(_ds.Tables[0].Rows[0]["Row"]);
                    model.Place = Convert.ToInt32(_ds.Tables[0].Rows[0]["Place"]);
                    model.IsAvailable = Convert.ToBoolean(_ds.Tables[0].Rows[0]["IsAvailable"]);
                }
            }
            return model;
        }

        public void UpdateStatusTicket(TicketModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateStatusTicket", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_seance", model.Seance.Id);
                cmd.Parameters.AddWithValue("@row", model.Row);
                cmd.Parameters.AddWithValue("@place", model.Place);
                cmd.ExecuteNonQuery();
            }
        }
    }
}