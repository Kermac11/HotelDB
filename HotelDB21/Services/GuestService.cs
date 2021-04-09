using System;
using System.Collections.Generic;
using System.Text;
using HotelDBConsole21.Models;
using Microsoft.Data.SqlClient;

namespace HotelDBConsole21.Services
{
    class GuestService : Connection
    {
        private string queryString = "select * from Guest";
        private String queryStringFromID = "select * from Guest where Guest_No = @ID";
        private string insertSql = "insert into Hotel Values(@ID, @Name, @Address)";
        private string deleteSql = "DELETE FROM Hotel WHERE Hotel_No=@DHotel";
        private string updateSql = "UPDATE Hotel SET Name = @Name, Address = @Address  WHERE Hotel_No = @ID ;";
        private string getByName = "select * from Hotel where Name = @name";

        public List<Guest> GetAllGuests()
        {
            List<Guest> Guests = new List<Guest>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection)
                )
                {
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int guestnr = reader.GetInt32(0);
                        String guestName = reader.GetString(1);
                        String GuestAdress = reader.GetString(2);
                        Guest guest = new Guest(guestnr, guestName, GuestAdress);
                        Guests.Add(guest);
                    }
                }
            }
            return Guests;
        }
        public Guest GetGuestFromId(int guestNr)
        {
            Guest guest = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryStringFromID, connection))
                {




                    command.Parameters.AddWithValue("@ID", guestNr);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int guestnr = reader.GetInt32(0);
                        String guestName = reader.GetString(1);
                        String GuestAdress = reader.GetString(2);
                        guest = new Guest(guestNr, guestName, GuestAdress);
                    }
                }
            }
            return guest;
        }
    }
}
