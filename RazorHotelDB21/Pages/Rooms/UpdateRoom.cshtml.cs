using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelDBConsole21.Interfaces;
using HotelDBConsole21.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPageHotelApp.Pages.Rooms
{
    public class UpdateRoomModel : PageModel
    {
        private IRoomService _rs;
        [BindProperty] public Room Room { get; set; }
        public UpdateRoomModel(IRoomService roomservice)
        {
            _rs = roomservice;
        }
        /// <summary>
        /// Rum nummer der skal ændres
        /// </summary>
        /// <param name="id">Rum nummer der skal ændres</param>
        /// <param name="hid">Hotel nummer hvor rummet hører til</param>
        public void OnGet(int id, int hid)
        {
            Room = _rs.GetRoomFromIdAsync(id, hid).Result;
        }

        /// <summary>
        /// Metoden der opdatere rummet 
        /// </summary>
        /// <returns>Sender brugeren tilbage til hotel siden hvor rummet hører til</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            await _rs.UpdateRoomAsync(Room, Room.Room_No, Room.Hotel_No);
            return RedirectToPage("GetAllRooms", "MyRooms", new { id = Room.Hotel_No });
        }
    }
}
