using RealEstateAgency4.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency4.ViewModels
{
    public class SellersViewModel
    {
        public IEnumerable<Seller> sellers { get; set; }
        public PageViewModel pageViewModel { get; set; }
        public SortViewModel sortViewModel { get; set; }

        [Display(Name = "Продавец")]
        public string FullName { get; set; }
        [Display(Name = "Пол")]
        public string Gender { get; set; }
        [Display(Name = "Дата рождения")]
        public DateTime? DateOfBirth { get; set; }
        [Display(Name = "Адрес")]
        public string Address { get; set; }
        [Display(Name = "Телефон")]
        public string Phone { get; set; }
        [Display(Name = "Паспортные данные")]
        public string PassportDate { get; set; }
        [Display(Name = "Адрес квартиры")]
        public string ApartmentAddress { get; set; }
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        [Display(Name = "Дополнительная информация")]
        public string AdditionalInformation { get; set; }

        [Display(Name = "Название квартиры")]
        public string ApartmentName { get; set; }
    }
}
