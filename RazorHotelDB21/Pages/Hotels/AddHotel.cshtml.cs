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
    public class AddHotelModel : PageModel
    {
        /// <summary>
        /// Bruges til at håndtere hotelservice metoder
        /// </summary>
        private IHotelService _hotelService;
        [BindProperty]
        
        public Hotel Hotel { get; set; }
        
        public AddHotelModel(IHotelService hotelservice)
        {
            _hotelService = hotelservice;
        }
        public void OnGet()
        {
        }
        /// <summary>
        /// Onpost metode til at at sætte hotel ind i databasen
        /// </summary>
        /// <returns>Aflevere siden der indholder alle hoteller</returns>
        public IActionResult OnPost()
        {
            _hotelService.CreateHotelAsync(Hotel);
            return RedirectToPage("GetAllHotels");
        }
    }
}
