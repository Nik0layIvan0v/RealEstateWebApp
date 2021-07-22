using System.IO;

namespace RealEstate.Common
{
    using System;

    public static class GlobalConstants
    {
        #region Area

        public const int MaxAreaNameLength = 50;
        public const int MinAreaNameLength = 10;

        #endregion

        #region City

        public const int MaxCityNameLength = 50;
        public const int MinCityNameLength = 10;

        #endregion

        #region Neighborhood

        public const int MaxNeighborhoodNameLength = 100;
        public const int MinNeighborhoodNameLength = 10;

        #endregion

        #region Currency

        public const int MaxCurrencyCodeLength = 3;
        public const int MinCurrencyCodeLength = 3;

        #endregion

        #region Estate

        public const int MaxDescriptionLength = 500;
        public const int MinDescriptionLength = 5;

        #endregion

        #region EstateType

        public const int MaxEstateTypeLength = 50;
        public const int MinEstateTypeLength = 20;
        #endregion

        #region Future

        public const int MaxFutureDescriptionLength = 50;
        public const int MinFutureDescriptionLength = 5;

        #endregion

        #region Note

        public const int MaxNoteLength = 100;
        public const int MinNoteLength = 1;

        #endregion

        #region Report

        public const int MaxReportDescriptionLength = 255;
        public const int MinReportDescriptionLength = 10;

        #endregion

        #region Image

        public static byte[] DefaultEstateImage = File.ReadAllBytes(@"..\RealEstate\wwwroot\Img\RealEstateLogo.png");

        #endregion

        #region TradeType
        public const int MaxTradeTypeLength = 100;
        public const int MinTradeTypeLength = 100;
        #endregion

        #region EstateService

        public const int DefaultLastEstatesCount = 3;

        #endregion

        #region Broker

        public const int MaxBrokerNameLength = 30;
        public const int MinBrokerNameLength = 5;

        public const int MaxDealerPhoneNumberLength = 18;
        public const int MinDealerPhoneNumberLength = 5;
        public const string DealerPhoneNumberRegex = @"[0-9]+";

        #endregion
    }
}
