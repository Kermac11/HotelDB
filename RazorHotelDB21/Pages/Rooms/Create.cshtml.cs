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
    public class CreateModel : PageModel
    {
        private IRoomService _rs;
        [BindProperty]
        public int HotelNo { get; set; }
        [BindProperty] public Room Room { get; set; }
        public CreateModel(IRoomService roomservice)
        {
            _rs = roomservice;
        }
        public void OnGet(int id)
        {
            HotelNo = id;
        }
        /// <summary>
        /// Metode til at skabe nyt hotel og sende det ind i databasen
        /// </summary>
        /// <returns>Sender tilbage til hotel rum index side</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            Room.Hotel_No = HotelNo;
            await _rs.CreateRoomAsync(Room.Hotel_No, Room);
            return RedirectToPage("GetAllRooms", "MyRooms", new { id = Room.Hotel_No});
        }

    }
}
