namespace RealEstate.Services.Models
{
    public class EstateListingViewModel
    {
        public string Id { get; set; } //Url with id -> button to details for current estate.

        public string Image { get; set; } //Pictures of property

        public string Title { get; set; } //Buy, sell, rent etc.

        public string Description { get; set; } //May be part of description
    }
}