﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HotelDBConsole21.Interfaces;
using HotelDBConsole21.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.WebEncoders.Testing;
using RazorHotelDB21.Services;

namespace RazorPageHotelApp.Services
{
    /// <summary>
    /// Service til at skabe, slette, og opdatere hotelrum
    /// </summary>
    public class RoomService : Connection, IRoomService
    {
        private string queryString = "select * from Room";
        private String queryStringFromHotelNr = "select* from Room WHERE Hotel_No = @ID";
        private String queryStringFromHotelNrAndRoomNr = "select* from Room WHERE Hotel_No = @ID AND Room_No = @RmID";
        private string insertSql = "INSERT into Room Values(@RmID, @HtID, @Type, @Price)";
        private string deleteSql = "DELETE FROM Room WHERE Hotel_No = @ID AND Room_No = @rmID";

        private string updateSql = "UPDATE Room SET Types = @type, Price = @price WHERE Hotel_No = @ID AND Room_No = @rmID;";

        private String queryStringFromType = "select* from Room WHERE Hotel_No = @ID AND Types = @Type";

        private String queryStringFromTypeAndPrice = "select* from Room WHERE Hotel_No = @ID AND Types = @Type AND Price <= @Price";

        private String queryStringFromPrice = "select* from Room WHERE Hotel_No = @ID AND Price <= @Price";
        /// <summary>
        /// Constuctor til Rumservice
        /// </summary>
        /// <param name="configuration">Bstemmer hvilke service vi kan bruge i klassen</param>
        public RoomService(IConfiguration configuration) : base(configuration)
        {
        }
        /// <summary>
        /// Henter alle rum
        /// </summary>
        /// <returns>Aflevere en liste af alle rum</returns>
        public async Task<List<Room>> GetAllRoomAsync()
        {
            List<Room> Rooms = new List<Room>();

            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (reader.Read())
                        {
                            int RoomNr = reader.GetInt32(0);
                            int HotelRoomNr = reader.GetInt32(1);
                            Char Type = reader.GetString(2)[0];
                            double Price = reader.GetDouble(3);
                            Room room = new Room(RoomNr, Type, Price, HotelRoomNr);
                            Rooms.Add(room);
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

            return Rooms;
        }
        /// <summary>
        /// Metode til at hente alle rum fra et bestemt hotel
        /// </summary>
        /// <param name="hotelNr">Hotelnummret vi henter rum udfra</param>
        /// <returns>Aflevere en liste liste udfra hotelnummret</returns>
        public async Task<List<Room>> GetAllRoomFromHotelAsync(int hotelNr)
        {
            List<Room> Rooms = new List<Room>();
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(queryStringFromHotelNr, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ID", hotelNr);
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (reader.Read())
                        {
                            int RoomNr = reader.GetInt32(0);
                            int HotelRoomNr = reader.GetInt32(1);
                            Char Type = reader.GetString(2)[0];
                            double Price = reader.GetDouble(3);
                            Room room = new Room(RoomNr, Type, Price, HotelRoomNr);
                            Rooms.Add(room);
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

            return Rooms;
        }
        /// <summary>
        /// Metode til at hente et bestemet rum udfra rumnummer og hotelnummer
        /// </summary>
        /// <param name="roomNr">Rumnummer vi søger efter</param>
        /// <param name="hotelNr">Hotelnummer vi søger efter</param>
        /// <returns>Aflevere rummet vi søger efter</returns>
        public async Task<Room> GetRoomFromIdAsync(int roomNr, int hotelNr)
        {
            Room room = null;
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(queryStringFromHotelNrAndRoomNr, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ID", hotelNr);
                        command.Parameters.AddWithValue("@RmID", roomNr);
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (reader.Read())
                        {
                            int RoomNr = reader.GetInt32(0);
                            int HotelRoomNr = reader.GetInt32(1);
                            Char Type = reader.GetString(2)[0];
                            double Price = reader.GetDouble(3);
                            room = new Room(RoomNr, Type, Price, HotelRoomNr);
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

            return room;
        }
        /// <summary>
        /// Metode til at skabe et rum hvordan de sættes ind i databasen
        /// </summary>
        /// <param name="hotelNr">Hotel nummeret hvor rummet skal sættes til</param>
        /// <param name="room">Rum objektet der skal sættes ind i databasen</param>
        /// <returns>Aflevere et ja eller nej svar om opgaven løses</returns>
        public async Task<bool> CreateRoomAsync(int hotelNr, Room room)
        {
            //(@RmID, @HtID, @Type, @Price)
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(insertSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@RmID", room.Room_No);
                        command.Parameters.AddWithValue("@HtID", room.Hotel_No);
                        command.Parameters.AddWithValue("@Type", room.Types);
                        command.Parameters.AddWithValue("@Price", room.Price);
                        await command.Connection.OpenAsync();
                        int noOfRowsAffected = await command.ExecuteNonQueryAsync();
                        if (noOfRowsAffected == 1)
                        {
                            return true;
                        }

                        return false;
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
                }
            }
        }
        /// <summary>
        /// Opdatere et bestemt hotelrum
        /// </summary>
        /// <param name="room">Rum objektet med de nye data der skal opdateres med</param>
        /// <param name="roomNr">Hvilket rum skal opdateres</param>
        /// <param name="hotelNr">Hvilket hotel rummet hører til</param>
        /// <returns>Aflevere et ja eller nej svar om opgaven løses</returns>
        public async Task<bool> UpdateRoomAsync(Room room, int roomNr, int hotelNr)
        {
            //@type, Price = @price WHERE Hotel_No = @ID AND Room_No = @rmID
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(updateSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@rmID", roomNr);
                        command.Parameters.AddWithValue("@ID", hotelNr);
                        command.Parameters.AddWithValue("@type", room.Types);
                        command.Parameters.AddWithValue("@price", room.Price);
                        await command.Connection.OpenAsync();
                        int noOfRowsAffected = await command.ExecuteNonQueryAsync();
                        if (noOfRowsAffected == 1)
                        {
                            return true;
                        }

                        return false;
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
                }
            }
        }
        /// <summary>
        /// Metode til at slette et rum fra databasen
        /// </summary>
        /// <param name="roomNr">Rummet der skal slettes</param>
        /// <param name="hotelNr">Hotel hvor rummet hører til</param>
        /// <returns>Aflevere rummet der lige er blevet slettet</returns>
        public async Task<Room> DeleteRoomAsync(int roomNr, int hotelNr)
        {
            //Hotel_No = @ID AND Room_No = @rmID"
            Room placeholder = await GetRoomFromIdAsync(roomNr, hotelNr);
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(deleteSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ID", placeholder.Hotel_No);
                        command.Parameters.AddWithValue("@rmID", placeholder.Room_No);
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
        /// Metode til at henter flere rum udfra flere kriterier
        /// </summary>
        /// <param name="type">Hvilken type rummene er</param>
        /// <param name="pris">Hvilken pris rummene der filtrers efter</param>
        /// <param name="hotelnumber">Hotelnummeret rummene skal hentes fra</param>
        /// <returns>Aflevere en liset af rum udfra kriterierne vi har sat</returns>
        public async Task<List<Room>> SearchForRoomFilterAsync(char type, int pris, int hotelnumber)
        {
            if (pris == 0)
            {
                queryStringFromTypeAndPrice = queryStringFromType;
            }

            if (type == '\0')
            {
                queryStringFromTypeAndPrice = queryStringFromPrice;
            }

            List<Room> rooms = new List<Room>();
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(queryStringFromTypeAndPrice, connection))
                {
                    command.Parameters.AddWithValue("@ID", hotelnumber);
                    if (type != '\0')
                    {
                        command.Parameters.AddWithValue("@Type", type);
                    }

                    if (pris != 0)
                    {
                        command.Parameters.AddWithValue("@Price", pris);
                    }

                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        int RoomNr = reader.GetInt32(0);
                        int HotelRoomNr = reader.GetInt32(1);
                        Char Type = reader.GetString(2)[0];
                        double Price = reader.GetDouble(3);
                        Room room = new Room(RoomNr, Type, Price, HotelRoomNr);
                        rooms.Add(room);
                    }
                }
            }

            return rooms;
        }
        /// <summary>
        /// Samme som SearchForRoomFilterAsync men med nogle andre løsninger
        /// </summary>
        /// <param name="searchlist">En liste af alle kriterierne</param>
        /// <returns>Aflevere en liste udfra kriterierne</returns>
        public async Task<List<Room>> TestSearchRoomsAsync(List<KeyValuePair<string, object>> searchlist)
        {
            string querycommand = "select * from Room WHERE ";
            List<Room> rooms = new List<Room>();
            Room rt = new Room();
            List<PropertyInfo> test2 = rt.GetType().GetProperties().ToList();
            Func<object, string> checktype = o => o is double ? "<=" : "=";
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            //char type, int pris,int hotelNr
            for (int i = 0; i < searchlist.Count; i++)
            {
                if (i != 0)
                {
                    querycommand += " AND ";
                }
                PropertyInfo check = test2.Find(o => o.PropertyType == searchlist[i].Value.GetType() && o.Name == searchlist[i].Key);
                string propname = check.Name;
                parameters.Add(new KeyValuePair<string, object>($"@object{i}", searchlist[i].Value));
                querycommand += propname + " " + checktype(searchlist[i].Value) + " " + $"@object{i}";
            }

            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(querycommand, connection))
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.AddWithValue(parameter.Key, parameter.Value);

                    }
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        int RoomNr = reader.GetInt32(0);
                        int HotelRoomNr = reader.GetInt32(1);
                        Char Type = reader.GetString(2)[0];
                        double Price = reader.GetDouble(3);
                        Room room = new Room(RoomNr, Type, Price, HotelRoomNr);
                        rooms.Add(room);
                    }
                }
            }

            return rooms;
        }
    }
}


