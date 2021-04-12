using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RazorHotelDB21.Interfaces;
using RazorHotelDB21.Models;
using RazorHotelDB21.Services;

namespace RazorPageHotelApp.Services
{
    /// <summary>
    /// Hotelservice der bruges til at skabe, slette og opdatere Hoteller
    /// </summary>
    public class HotelService : Connection, IHotelService
    {
        private string queryString = "select * from Hotel";
        private String queryStringFromID = "select * from Hotel where Hotel_No = @ID";
        private string insertSql = "insert into Hotel Values(@ID, @Name, @Address)";
        private string deleteSql = "DELETE FROM Hotel WHERE Hotel_No=@DHotel";
        private string updateSql = "UPDATE Hotel SET Name = @Name, Address = @Address  WHERE Hotel_No = @ID ;";
        private string getByName = "select * from Hotel where Name LIKE @name";
        // lav selv sql strengene færdige og lav gerne yderligere sqlstrings
        /// <summary>
        /// Constuctor til hotelservice
        /// </summary>
        /// <param name="configuration">Bstemmer hvilke service vi kan bruge i klassen</param>
        public HotelService(IConfiguration configuration) : base(configuration)
        {
        }
        /// <summary>
        /// Henter alle hoteller
        /// </summary>
        /// <returns>Giver en liste af alle hoteller</returns>
        public async Task<List<Hotel>> GetAllHotelAsync()
        {
            List<Hotel> hoteller = new List<Hotel>();
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    await command.Connection.OpenAsync();

                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
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

            return hoteller;
        }
        /// <summary>
        /// Henter et bestemt hotel udfra hotel nummer
        /// </summary>
        /// <param name="hotelNr">Hotel nummeret vi bruger til at hente hotellet</param>
        /// <returns>Aflevere hottlet</returns>
        public async Task<Hotel> GetHotelFromIdAsync(int hotelNr)
        {
            Hotel hotel = null;
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(queryStringFromID, connection))
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

            return hotel;
        }
        /// <summary>
        /// Metode til at skabe et hotel
        /// </summary>
        /// <param name="hotel">Hotel objektet der skal lægges i databasen</param>
        /// <returns>Giver et ja, nej svar på om opgave løses</returns>
        public async Task<bool> CreateHotelAsync(Hotel hotel)
        {
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(insertSql, connection))
                {
                    try
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
                    }
                    catch (SqlException)
                    {
                        Console.WriteLine("Database Fejl");
                        return false;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Generel Fejl");
                        return false;
                    }

                    return false;
                }
            }
        }
        /// <summary>
        /// Bruges til at opdatere et bestemt hotel
        /// </summary>
        /// <param name="hotel">Hotel objektet med data der skal opdatere et bestemt hotel</param>
        /// <param name="hotelNr">Hvilket hotel de nye data skal lægges ind</param>
        /// <returns>Giver et ja, nej svar på om opgave løses</returns>
        public async Task<bool> UpdateHotelAsync(Hotel hotel, int hotelNr)
        {
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(updateSql, connection))
                {
                    try
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
                    }
                    catch (SqlException)
                    {
                        Console.WriteLine("Database Fejl");
                        return false;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Generel Fejl");
                        return false;
                    }

                    return false;
                }
            }
        }
        /// <summary>
        /// Metode til at slette et hotel fra en database
        /// </summary>
        /// <param name="hotelNr">Hotel nummret der skal slettes i databasen</param>
        /// <returns>Aflevere hotellet der er blevet slettet</returns>
        public async Task<Hotel> DeleteHotelAsync(int hotelNr)
        {
            Task<Hotel> waithotel = GetHotelFromIdAsync(hotelNr);
            Task.WaitAll(waithotel);
            Hotel placeholder = waithotel.Result;

            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(deleteSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@DHotel", placeholder.HotelNr);
                        await command.Connection.OpenAsync();
                        int noOfRowsAffected = await command.ExecuteNonQueryAsync();
                        if (noOfRowsAffected == 1)
                        {
                            return placeholder;
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
                return null;
            }
        }
        /// <summary>
        /// For fat i et bestemt hotel udfra navn
        /// </summary>
        /// <param name="name">Navn vi søger ud efter</param>
        /// <returns>Aflevere hotellet vi søger efter</returns>
        public async Task<List<Hotel>> GetHotelsByNameAsync(string name)
        {
            List<Hotel> hotels = new List<Hotel>();
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(getByName, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@name", "%" + name + "%");
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

            return hotels;
        }
    }
}
