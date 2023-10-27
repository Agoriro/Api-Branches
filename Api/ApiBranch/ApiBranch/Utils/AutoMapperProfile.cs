using AutoMapper;
using ApiBranch.Mappers;
using ApiBranch.Models;
using System.Globalization;

namespace ApiBranch.Utils
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            #region Config Currency
            //realiza el mapeo implicito
            CreateMap<CurrencyTest, CurrencyMapper>().ReverseMap();

            #endregion

            #region Config Branch
            //configuración para el mapeo de los campos adicionales
            CreateMap<BranchTest, BranchMapper>()
                .ForMember(dest =>
                dest.CurrencyName,
                opt => opt.MapFrom(ori => ori.IdCurrencyNavigation.CurrencyName)
                )
                .ForMember(dest =>
                dest.BranchDateCreation,
                opt => opt.MapFrom(ori => ori.BranchDateCreation.ToString("dd/MM/yyyy"))
                );

            CreateMap<BranchMapper, BranchTest>()
                .ForMember(dest =>
                    dest.IdCurrencyNavigation,
                    opt => opt.Ignore()
                )
                .ForMember(dest =>
                    dest.BranchDateCreation,
                    opt => opt.MapFrom(ori => DateTime.ParseExact(ori.BranchDateCreation,"dd/MM/yyyy",CultureInfo.InvariantCulture))
                );
            #endregion
        }
    }
}
