
using RealEstateAgency4.Models;
using System.ComponentModel.DataAnnotations;
using static RealEstateAgency4.ViewModels.SortViewModel;

namespace RealEstateAgency4.ViewModels
{

    public class ContractsViewModel
    {
        public IEnumerable<Contract> contracts { get; set; }
        public PageViewModel pageViewModel { get; set; } 
        public SortViewModel sortViewModel { get; set; }
        [Display(Name = "Дата заключения")]
        public DateTime? DateOfContract { get; set; }

        [Display(Name = "Продавец")]
        public string SellerName { get; set; }
        [Display(Name = "Цена сделки")]
        public decimal DealAmount { get; set; }
        [Display(Name = "Стоимость услуг")]
        public decimal ServiceCost { get; set; }
        [Display(Name = "Сотрудник")]
        public string Employee { get; set; }
        [Display(Name = "Покупатель")]
        public string Fiobuyer { get; set; }
    }
}
