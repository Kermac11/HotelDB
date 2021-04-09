using System;
using System.Collections.Generic;
using System.Text;
using HotelDBConsole21.Interfaces;
using HotelDBConsole21.Models;
using Microsoft.Data.SqlClient;

namespace HotelDBConsole21.Services
{


    public class RoomService : Connection, IRoomService
    {
        private string queryString = "select * from Room";
        private String queryStringFromHotelNr = "select* from Room WHERE Hotel_No = @ID";
        private String queryStringFromHotelNrAndRoomNr = "select* from Room WHERE Hotel_No = @ID AND Room_No = @RmID";
        private string insertSql = "INSERT into Room Values(@RmID, @HtID, @Type, @Price)";
        private string deleteSql = "DELETE FROM Room WHERE Hotel_No = @ID AND Room_No = @rmID";
        private string updateSql = "UPDATE Room SET Types = @type, Price = @price WHERE Hotel_No = @ID AND Room_No = @rmID;";
        // lad klassen arve fra interfacet IRoomService og arve fra Connection klassen
        // indsæt sql strings

        //Implementer metoderne som der skal ud fra interfacet

        public List<Room> GetAllRoom()
        {
            List<Room> Rooms = new List<Room>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
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
            }
            return Rooms;
        }


        public List<Room> GetAllRoomFromHotel(int hotelNr)
        {
            List<Room> Rooms = new List<Room>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryStringFromHotelNr, connection))
                {
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
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
            }

            return Rooms;
        }

        public Room GetRoomFromId(int roomNr, int hotelNr)
        {
            Room room = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryStringFromHotelNrAndRoomNr, connection))
                {
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    command.Parameters.AddWithValue("@RmID", roomNr);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int RoomNr = reader.GetInt32(0);
                        int HotelRoomNr = reader.GetInt32(1);
                        Char Type = reader.GetString(2)[0];
                        double Price = reader.GetDouble(3);
                        room = new Room(RoomNr, Type, Price, HotelRoomNr);
                    }
                }
            }

            return room;
        }

        public bool CreateRoom(int hotelNr, Room room)
        {
            //(@RmID, @HtID, @Type, @Price)
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertSql, connection))
                {

                    command.Parameters.AddWithValue("@RmID", room.RoomNr);
                    command.Parameters.AddWithValue("@HtID", room.HotelNr);
                    command.Parameters.AddWithValue("@Type", room.Types);
                    command.Parameters.AddWithValue("@Price", room.Pris);
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

        public bool UpdateRoom(Room room, int roomNr, int hotelNr)
        {
            //@type, Price = @price WHERE Hotel_No = @ID AND Room_No = @rmID
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(updateSql, connection))
                {
                    command.Parameters.AddWithValue("@rmID", roomNr);
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    command.Parameters.AddWithValue("@type", room.Types);
                    command.Parameters.AddWithValue("@price", room.Pris);
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

        public Room DeleteRoom(int roomNr, int hotelNr)
        {
            //Hotel_No = @ID AND Room_No = @rmID"
            Room placeholder = GetRoomFromId(roomNr, hotelNr);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(deleteSql, connection)
                )
                {
                    command.Parameters.AddWithValue("@ID", placeholder.HotelNr);
                    command.Parameters.AddWithValue("@rmID", placeholder.RoomNr);
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
    }
}
