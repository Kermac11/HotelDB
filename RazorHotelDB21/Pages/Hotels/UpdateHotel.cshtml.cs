using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB21.Interfaces;
using RazorHotelDB21.Models;

namespace RazorPageHotelApp.Pages.Hotels
{
    public class UpdateHotelModel : PageModel
    {
        private IHotelService _hs;
        [BindProperty] public Hotel Hotel { get; set; }
        public UpdateHotelModel(IHotelService hotelservice)
        {
            _hs = hotelservice;
        }
        public async Task OnGetAsync(int id)
        {
            Hotel = await _hs.GetHotelFromIdAsync(id);
        }
        /// <summary>
        /// Metode til at updatere hotellet
        /// </summary>
        /// <returns>Sender bruger tilbage til Hotel Index side</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            await _hs.UpdateHotelAsync(Hotel, Hotel.HotelNr);
            return RedirectToPage("GetAllHotels");
        }
    }
}
