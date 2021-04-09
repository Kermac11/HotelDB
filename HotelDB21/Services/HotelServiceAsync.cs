using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HotelDBConsole21.Interfaces;
using HotelDBConsole21.Models;
using Microsoft.Data.SqlClient;

namespace HotelDBConsole21.Services
{
    class HotelServiceAsync : Connection, IHotelServiceAsync
    {
        private string queryString = "select * from Hotel";
        private String queryStringFromID = "select * from Hotel where Hotel_No = @ID";
        private string insertSql = "insert into Hotel Values(@ID, @Name, @Address)";
        private string deleteSql = "DELETE FROM Hotel WHERE Hotel_No=@DHotel";
        private string updateSql = "UPDATE Hotel SET Name = @Name, Address = @Address  WHERE Hotel_No = @ID ;";
        private string getByName = "select * from Hotel where Name Like '%@name%'";

        public async Task<List<Hotel>> GetAllHotelAsync()
        {
            List<Hotel> hoteller = new List<Hotel>();

            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
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

        public async Task<Hotel> GetHotelFromIdAsync(int hotelNr)
        {
            Hotel hotel = null;
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryStringFromID, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ID", hotelNr);
                        command.Connection.Open();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
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

        public async Task<bool> CreateHotelAsync(Hotel hotel)
        {
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertSql, connection))
                {
                    command.Parameters.AddWithValue("@ID", hotel.HotelNr);
                    command.Parameters.AddWithValue("@Name", hotel.Navn);
                    command.Parameters.AddWithValue("@Address", hotel.Adresse);
                    await command.Connection.OpenAsync();
                    int noOfRowsAffected = await command.ExecuteNonQueryAsync();
                    if (noOfRowsAffected == 1)
                    {
                        return true;
                    }

                    return false;
                }
            }
        }

        public Task<bool> UpdateHotelAsync(Hotel hotel, int hotelNr)
        {
            throw new NotImplementedException();
        }

        public async Task<Hotel> DeleteHotelAsync(int hotelNr)
        {
            Task<Hotel> waithotel = GetHotelFromIdAsync(hotelNr);
            Task.WaitAll(waithotel);
            Hotel placeholder = waithotel.Result;

            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(deleteSql, connection))
                {

                    command.Parameters.AddWithValue("@DHotel", placeholder.HotelNr);
                    await command.Connection.OpenAsync();
                    int noOfRowsAffected = await command.ExecuteNonQueryAsync();
                    if (noOfRowsAffected == 1)
                    {
                        return placeholder;
                    }
                }
                return null;
            }
        }

        public async Task<List<Hotel>> GetHotelsByNameAsync(string name)
        {
            List<Hotel> hotels = new List<Hotel>();
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(getByName, connection))
                {

                    command.Parameters.AddWithValue("@name", name);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
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


