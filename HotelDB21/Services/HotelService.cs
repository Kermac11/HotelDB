using System;
using System.Collections.Generic;
using System.Text;
using HotelDBConsole21.Interfaces;
using HotelDBConsole21.Models;
using Microsoft.Data.SqlClient;

namespace HotelDBConsole21.Services
{
    public class HotelService : Connection, IHotelService
    {
        private string queryString = "select * from Hotel";
        private String queryStringFromID = "select * from Hotel where Hotel_No = @ID";
        private string insertSql = "insert into Hotel Values(@ID, @Name, @Address)";
        private string deleteSql = "DELETE FROM Hotel WHERE Hotel_No=@DHotel";
        private string updateSql = "UPDATE Hotel SET Name = @Name, Address = @Address  WHERE Hotel_No = @ID ;";
        private string getByName = "select * from Hotel where Name Like '@name'";
        // lav selv sql strengene færdige og lav gerne yderligere sqlstrings


        public List<Hotel> GetAllHotel()
        {
            List<Hotel> hoteller = new List<Hotel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    try
                    {


                        command.Connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int hotelNr = reader.GetInt32(0);
                            String hotelNavn = reader.GetString(1);
                            String hotelAdr = reader.GetString(2);
                            Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                            hoteller.Add(hotel);
                        }
                    }
                    catch (SqlException)
                    {
                        Console.WriteLine("Database Fejl");
                        return null;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Generel Fejl");
                        return null;
                    }
                }
            }
            return hoteller;
        }

        public Hotel GetHotelFromId(int hotelNr)
        {
            Hotel hotel = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryStringFromID, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ID", hotelNr);
                        command.Connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int hotelNrRead = reader.GetInt32(0);
                            String hotelNavn = reader.GetString(1);
                            String hotelAdr = reader.GetString(2);
                            hotel = new Hotel(hotelNrRead, hotelNavn, hotelAdr);
                        }
                    }
                    catch (SqlException) { }
                }
            }

            return hotel;
        }

        public bool CreateHotel(Hotel hotel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertSql, connection))
                {
                    command.Parameters.AddWithValue("@ID", hotel.HotelNr);
                    command.Parameters.AddWithValue("@Name", hotel.Navn);
                    command.Parameters.AddWithValue("@Address", hotel.Adresse);
                    command.Connection.Open();
                    int noOfRowsAffected = command.ExecuteNonQuery();
                    if (noOfRowsAffected == 1)
                    {
                        return true;
                    }

                    return false;
                }
            }
        }

        public bool UpdateHotel(Hotel hotel, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(updateSql, connection))
                {
                    command.Parameters.AddWithValue("@ID", hotel.HotelNr);
                    command.Parameters.AddWithValue("@Name", hotel.Navn);
                    command.Parameters.AddWithValue("@Address", hotel.Adresse);
                    command.Connection.Open();
                    int noOfRowsAffected = command.ExecuteNonQuery();
                    if (noOfRowsAffected == 1)
                    {
                        return true;
                    }

                    return false;
                }
            }
        }


        public Hotel DeleteHotel(int hotelNr)
        {
            Hotel placeholder = GetHotelFromId(hotelNr);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(deleteSql, connection))
                {

                    command.Parameters.AddWithValue("@DHotel", placeholder.HotelNr);
                    command.Connection.Open();
                    int noOfRowsAffected = command.ExecuteNonQuery();
                    if (noOfRowsAffected == 1)
                    {
                        return placeholder;
                    }
                }
                return null;
            }
        }

        public List<Hotel> GetHotelsByName(string name)
        {
            List<Hotel> hotels = new List<Hotel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(getByName, connection))
                {

                    command.Parameters.AddWithValue("@name", name);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int hotelNrRead = reader.GetInt32(0);
                        String hotelNavn = reader.GetString(1);
                        String hotelAdr = reader.GetString(2);
                        Hotel hotel = new Hotel(hotelNrRead, hotelNavn, hotelAdr);
                        hotels.Add(hotel);
                    }
                }
            }

            return hotels;
        }
    }
}
