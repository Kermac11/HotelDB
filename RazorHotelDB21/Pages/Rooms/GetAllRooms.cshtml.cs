using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelDBConsole21.Interfaces;
using HotelDBConsole21.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB21.Interfaces;

namespace RazorPageHotelApp.Pages.Rooms
{
    public class GetAllRoomsModel : PageModel
    {
        private IRoomService _rs;
        public List<Room> Rooms { get; set; }
        [BindProperty] public int HotelNo { get; set; }
        public string Message;
        public GetAllRoomsModel(IRoomService roomservice)
        {
            _rs = roomservice;
        }
        public void OnGet()
        {
        }
        /// <summary>
        /// Metode til at hente alle rum i et bestemt hotel
        /// </summary>
        /// <param name="id">Hotel nummret hvor alle rum skal hentes/param>
        public void OnGetMyRooms(int id)
        {
            Rooms = _rs.GetAllRoomFromHotelAsync(id).Result;
            HotelNo = id;
        }
        /// <summary>
        /// Metode der sletter et rum i et hotel
        /// </summary>
        /// <param name="roomNr">Rum nummer der skal slettes</param>
        /// <param name="hotelNr">Hotel nummeret hvor rummet hører til</param>
        /// <returns>Giver tilbage samme side med nyt info</returns>
        public async Task<IActionResult> OnPostDeleteAsync(int roomNr, int hotelNr)
        {
            await _rs.DeleteRoomAsync(roomNr, hotelNr);
            OnGetMyRooms(hotelNr);
            return Page();
        }
        /// <summary>
        /// Metode til at filtrer hotelrum
        /// </summary>
        /// <param name="type">Hvilken type rummene der søges efter</param>
        /// <param name="pris">Hvilken pris man vil søge efter</param>
        /// <param name="hotelNr">Hotelnummer hvor rummne hører til</param>
        /// <param name="TypeSwitch">skal sættes til on for at bruge det søge felt</param>
        /// <param name="PriceSwitch">skal sættes til on for at bruge det søge felt</param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostSearchAsync(char type, double pris, int hotelNr, string TypeSwitch, string PriceSwitch)
        {
            //if (type != '\0' || pris != 0)
            //{
            //    Rooms = await _rs.SearchForRoomFilterAsync(type, pris, hotelNr);
            //}
            List<KeyValuePair<string, object>> el = new List<KeyValuePair<string, object>>();
            el.Add(new KeyValuePair<string, object>("Hotel_No", hotelNr));
            if (type != '\0' && TypeSwitch == "on")
            {
                el.Add(new KeyValuePair<string, object>("Types", type));
            }
            if (pris != 0 && PriceSwitch == "on")
            {
                el.Add(new KeyValuePair<string, object>("Price", pris));
            }
            if ((type != '\0' && TypeSwitch == "on") || (pris != 0 && PriceSwitch == "on"))
            {
                Rooms = await _rs.TestSearchRoomsAsync(el);
            }
            if ((type == '\0' || TypeSwitch == null) && (pris == 0 || PriceSwitch == null))
            {
                OnGetMyRooms(hotelNr);
            }
            HotelNo = hotelNr;
            return Page();
        }

    }
}
