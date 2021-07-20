using System.IO;

namespace RealEstate.Common
{
    using System;

    public static class GlobalConstants
    {
        #region Area

        public const int AreaMaxNameLength = 50;
        public const int AreaMinNameLength = 10;

        #endregion

        #region City

        public const int MaxCityNameLength = 50;
        public const int MinCityNameLength = 10;

        #endregion

        #region Neighborhood

        public const int MaxNeighborhoodNameLength = 100;
        public const int MixNeighborhoodNameLength = 10;

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

        #endregion

        #region Future

        public const int MaxFutureDescriptionLength = 50;
        public const int MinFutureDescriptionLength = 5;

        #endregion

        #region Note

        public const int MaxNoteLength = 100;
        public const int MinNoteLength = 100;

        #endregion

        #region Report

        public const int MaxReportDescriptionLength = 255;
        public const int MinReportDescriptionLength = 10;

        #endregion

        #region Image

        public static byte[] DefaultEstateImage = File.ReadAllBytes(@"..\RealEstate\wwwroot\Img\RealEstateLogo.png");

        #endregion
    }   
}
