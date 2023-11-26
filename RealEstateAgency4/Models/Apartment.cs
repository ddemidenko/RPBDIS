using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAgency4.Models;

public partial class Apartment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Display(Name = "Название")]
    public string Name { get; set; }
    [Display(Name = "Описание")]
    public string Description { get; set; }
    [Display(Name = "Количество комнат")]
    public int NumberOfRooms { get; set; }
    [Display(Name = "Площадь")]
    public decimal Area { get; set; }
    [Display(Name = "Наличие раздельного санузла")]
    public bool SeparateBathroom { get; set; }
    [Display(Name = "Наличие телефона")]
    public bool HasPhone { get; set; }
    [Display(Name = "Максимальная стоимость")]
    public decimal MaxPrice { get; set; }

    [Display(Name = "Дополнительные предпочтения")]
    public string AdditionalPreferences { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    public virtual ICollection<Seller> Sellers { get; set; } = new List<Seller>();
}