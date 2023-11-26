using RealEstateAgency4.Models;
using System.ComponentModel.DataAnnotations;
using static RealEstateAgency4.ViewModels.SortViewModel;

namespace RealEstateAgency4.ViewModels
{
    public class ServicesViewModel
    {
        public IEnumerable<Service> services { get; set; }
        public PageViewModel pageViewModel { get; set; }
        public SortViewModel sortViewModel { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
    }
}
