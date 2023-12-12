using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab6.Models;

public partial class Service
{
    public int Id { get; set; }
    [Display(Name = "Название")]
    public string Name { get; set; }
    [Display(Name = "Описание")]
    public string Description { get; set; }
    [Display(Name = "Стоимость")]
    public decimal Price { get; set; }
}
