using System;
using System.Collections.Generic;

namespace RealEstateAgency4.Models;

public partial class ServiceView
{
    public int ServiceId { get; set; }

    public string? ServiceName { get; set; }

    public string? ServiceDescription { get; set; }

    public decimal? ServicePrice { get; set; }
}
