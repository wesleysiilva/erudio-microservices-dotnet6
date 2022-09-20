using AutoMapper;

namespace GeekShopping.CartAPI.Config
{
    public class Mappingconfig
    {
        public static MapperConfiguration RegisterMapps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
//                config.CreateMap<ProductVO, Product>();
//                config.CreateMap<Product, ProductVO>();
            });

            return mappingConfig;
        }
    }
}
