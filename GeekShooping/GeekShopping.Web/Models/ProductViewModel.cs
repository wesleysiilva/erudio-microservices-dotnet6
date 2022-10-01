using System.ComponentModel.DataAnnotations;

namespace GeekShopping.Web.Models
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        [Range (1,100)]
        public int Count { get; set; } = 1;
        public string SubstringName() => SubstringText(Name, 24);
        public string SubstringDescription() => SubstringText(Description, 355);
        private static string SubstringText(string text, int maxSize) => text.Length < maxSize ? text : $"{text[..(maxSize - 3)]}...";
    }
}
