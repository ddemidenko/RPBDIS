using RealEstateAgency4.Models;
using System.ComponentModel.DataAnnotations;
using static RealEstateAgency4.ViewModels.SortViewModel;

namespace RealEstateAgency4.ViewModels
{
    public class ApartmentsViewModel
    {
        public IEnumerable<Apartment> apartments { get; set; }
        public PageViewModel pageViewModel { get; set; }
        public SortViewModel sortViewModel { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Количество комнат")]
        public int NumberOfRooms { get; set; }
        [Display(Name = "Площадь")]
        public decimal Area { get; set; }
        [Display(Name = "Наличие раздельного санузла")]
        public bool? SeparateBathroom { get; set; }
        [Display(Name = "Наличие телефона")]
        public bool? HasPhone { get; set; }
        [Display(Name = "Максимальная стоимость")]
        public decimal MaxPrice { get; set; }
        [Display(Name = "Дополнительные пожелания")]
        public string AdditionalPreferences { get; set; }
    }
}
