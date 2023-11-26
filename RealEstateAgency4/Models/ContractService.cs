using System;
using System.Collections.Generic;

namespace RealEstateAgency4.Models;

public partial class ContractService
{
    public int Id { get; set; }

    public int ContractId { get; set; }

    public int ServiceId { get; set; }

    public virtual Contract? Contract { get; set; }

    public virtual Service? Service { get; set; }
}
