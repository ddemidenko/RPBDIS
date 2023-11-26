using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency4.Models;

public partial class Seller
{
    public int Id { get; set; }
    [Display(Name = "Продавец")]
    public string FullName { get; set; }
    [Display(Name = "Пол")]
    public string Gender { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Дата рождения")]
    public DateTime DateOfBirth { get; set; }
    [Display(Name = "Адрес")]
    public string Address { get; set; }
    [Display(Name = "Телефон")]
    public string Phone { get; set; }
    [Display(Name = "Паспортные данные")]
    public string PassportData { get; set; }
    [Display(Name = "Название квартиры")]
    public int ApartmentId { get; set; }
    [Display(Name = "Адрес квартиры")]
    public string ApartmentAddress { get; set; }
    [Display(Name = "Стоимость")]
    public decimal Price { get; set; }
    [Display(Name = "Дополнительная информация")]
    public string AdditionalInformation { get; set; }

    [Display(Name = "Название квартиры")]
    public virtual Apartment? Apartment { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}
