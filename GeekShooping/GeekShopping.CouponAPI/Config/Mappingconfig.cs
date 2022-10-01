using AutoMapper;
using GeekShopping.CouponAPI.Data.ValueObjects;
using GeekShopping.CouponAPI.Model;

namespace GeekShopping.CouponAPI.Config
{
    public class Mappingconfig
    {
        public static MapperConfiguration RegisterMapps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Coupon, CouponVO>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
