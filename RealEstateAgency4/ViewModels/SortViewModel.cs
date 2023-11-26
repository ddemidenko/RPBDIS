namespace RealEstateAgency4.ViewModels
{
    public enum SortState
    {
        //Contracts
        No,
        DateOfContractAsc,
        DateOfContractDesc,
        DealAmountAsc,
        DealAmountDesc,
        ServiceCostAsc,
        ServiceCostDesc,


        //Services
        PriceAsc,
        PriceDesc,

        //Sellers
        FullNameAsc,
        FullNameDesc,
        GenderAsc,
        GenderDesc,
        DateOfBirthAsc,
        DateOfBirthDesc,
        AddressAsc,
        AddressDesc,
        PhoneAsc,
        PhoneDesc,
        PassportDataAsc,
        PassportDataDesc,
        ApartmentAddressAsc,
        ApartmentAddressDesc,
        PriceSellerAsc,
        PriceSellerDesc,
        AdditionalInformationAsc,
        AdditionalInformationDesc,
        ApartmentNameAsc,
        ApartmentNameDesc,

        //Apartments
        DescriptionApartmentAsc,
        DescriptionApartmentDesc,
        NumberOfRoomsAsc,
        NumberOfRoomsDesc,
        AreaAsc,
        AreaDesc,
        SeparateBathroomAsc,
        SeparateBathroomDesc,
        HasPhoneAsc,
        HasPhoneDesc,
        MaxPriceAsc,
        MaxPriceDesc,




    }
    public class SortViewModel
    {
            //Contracts
            public SortState DateOfContractSort { get; set; }
            public SortState DealAmountSort { get; set; }
            public SortState ServiceCostSort { get; set; }



            public SortState CurrentState { get; set; }

            //Services
            public SortState PriceSort { get; set; }

            //Sellers
            public SortState DateOfBirthSort { get; set; }
            public SortState PriceSellerSort { get; set; }


            //Apartments

            public SortState NumberOfRoomsSort { get; set; }
            public SortState AreaSort { get; set; }
            public SortState SeparateBathroomSort { get; set; }
            public SortState HasPhoneSort { get; set; }
            public SortState MaxPriceSort { get; set; }

        public SortViewModel(SortState sortOrder)
            {
                //Apartments              
                NumberOfRoomsSort = sortOrder == SortState.NumberOfRoomsAsc ? SortState.NumberOfRoomsDesc : SortState.NumberOfRoomsAsc;
                AreaSort = sortOrder == SortState.AreaAsc ? SortState.AreaDesc : SortState.AreaAsc;
                SeparateBathroomSort = sortOrder == SortState.SeparateBathroomAsc ? SortState.SeparateBathroomDesc : SortState.SeparateBathroomAsc;
                HasPhoneSort = sortOrder == SortState.HasPhoneAsc ? SortState.HasPhoneDesc : SortState.HasPhoneAsc;
                MaxPriceSort = sortOrder == SortState.MaxPriceAsc ? SortState.MaxPriceDesc : SortState.MaxPriceAsc;           

                //Contracts
                DateOfContractSort = sortOrder == SortState.DateOfContractAsc ? SortState.DateOfContractDesc : SortState.DateOfContractAsc;
                DealAmountSort = sortOrder == SortState.DealAmountAsc ? SortState.DealAmountDesc : SortState.DealAmountAsc;
                ServiceCostSort = sortOrder == SortState.ServiceCostAsc ? SortState.ServiceCostDesc : SortState.ServiceCostAsc;

                //Services
                PriceSort = sortOrder == SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;

                //Sellers
                DateOfBirthSort = sortOrder == SortState.DateOfBirthAsc ? SortState.DateOfBirthDesc : SortState.DateOfBirthAsc;
                PriceSellerSort = sortOrder == SortState.PriceSellerAsc ? SortState.PriceSellerDesc : SortState.PriceSellerAsc;
            CurrentState = sortOrder;
            }
    }
}
