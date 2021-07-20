using System.IO;

namespace RealEstate.Common
{
    using System;

    public static class DataBaseAttributesConstants
    {
        #region Area

        public const int AreaMaxNameLength = 50;

        #endregion

        #region City

        public const int MaxCityNameLength = 50;

        #endregion

        #region Neighborhood

        public const int MaxNeighborhoodNameLength = 100;

        #endregion

        #region Currency

        public const int MaxCurrencyCodeLength = 3;

        #endregion

        #region Estate

        public const int MaxDescriptionLength = 500;

        #endregion

        #region EstateType

        public const int MaxEstateTypeLength = 50;

        #endregion

        #region Future

        public const int MaxFutureDescriptionLength = 50;

        #endregion

        #region Note

        public const int MaxNoteLength = 100;

        #endregion

        #region Report

        public const int ReportDescriptionMaxLength = 255;

        #endregion

        #region Image

        public static byte[] DefaultEstateImage = File.ReadAllBytes(@"..\RealEstate\wwwroot\Img\RealEstateLogo.png");

        #endregion
    }   
}
