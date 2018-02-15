using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BOA.WeatherForcast.Web.ViewModels
{
    public class CityViewModel
    {
        [Required]
        [DisplayName("Select city to see forecast")]
        public string Id { get; set; }

        public string NameOfCity { get; set; }
        public List<SelectListItem> ListofCities { get; set; }
    }
}
