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
        public void OnGet(int id, int hid)
        {
            Room = _rs.GetRoomFromIdAsync(id, hid).Result;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _rs.UpdateRoomAsync(Room, Room.Room_No, Room.Hotel_No);
            return RedirectToPage("GetAllRooms", "MyRooms", new { id = Room.Hotel_No });
        }
    }
}
