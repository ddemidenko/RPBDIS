using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency4.Models;

public partial class Contract
{
    public int Id { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Дата")]
    public DateTime DateOfContract { get; set; }

    [Display(Name = "Продавец")]
    public int SellerId { get; set; }

    [Display(Name = "Сумма сделки")]
    public decimal DealAmount { get; set; }
    [Display(Name = "Стоимость услуги")]
    public decimal ServiceCost { get; set; }
    [Display(Name = "Сотрудник")]
    public string Employee { get; set; }
    [Display(Name = "Покупатель")]
    public string Fiobuyer { get; set; }


    public virtual ICollection<ContractService> ContractServices { get; set; } = new List<ContractService>();

    public virtual Seller? Seller { get; set; }
}
