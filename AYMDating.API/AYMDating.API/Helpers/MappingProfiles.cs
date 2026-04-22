using AutoMapper;
using AYMDating.API.DTO;
using AYMDating.DAL.Entities;

namespace AYMDating.API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
          CreateMap<Job, JobDTO>().ReverseMap();
          CreateMap<UserMessage, UserMessageDTO>().ReverseMap();
            //CreateMap<UserHistory, UserHistoryDTO>().ReverseMap();
            CreateMap<UserHistory, UserHistoryDTO>()
            //.ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.AppUserId))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => DateTime.Now.Year - src.AppUser.DateOfBirth.Year)).ReverseMap();
          //.ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country.Name))
          //.ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language.Name))
          //.ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.Name))
          //.ForMember(dest => dest.MaritalStatus, opt => opt.MapFrom(src => src.MaritalStatus.Name))
          //.ForMember(dest => dest.Job, opt => opt.MapFrom(src => src.Job.Name))
          //.ForMember(dest => dest.Purpose, opt => opt.MapFrom(src => src.Purpose.Name))
          //.ForMember(dest => dest.FinancialMode, opt => opt.MapFrom(src => src.FinancialMode.Name))
          //.ForMember(dest => dest.Education, opt => opt.MapFrom(src => src.Education.Name))
          //.ForMember(dest => dest.UserPackage, opt => opt.MapFrom(src => src.UserPackage.Package.Name));


           CreateMap<UserView, UserViewOrLikeOrFavoriteOrBlockOrReportDTO>().ReverseMap();
           CreateMap<UserLike, UserViewOrLikeOrFavoriteOrBlockOrReportDTO>().ReverseMap();
           CreateMap<UserFavorite, UserViewOrLikeOrFavoriteOrBlockOrReportDTO>().ReverseMap();
           CreateMap<UserBlock, UserViewOrLikeOrFavoriteOrBlockOrReportDTO>().ReverseMap();
           CreateMap<UserReport, UserViewOrLikeOrFavoriteOrBlockOrReportDTO>().ReverseMap();

            CreateMap<UserImage, UserImagesDTO>()
            //.ForMember(dest => dest.LinkName, opt => opt.MapFrom(src => src.LinkName))
            .ForMember(dest => dest.LinkName, opt => opt.MapFrom<UserImageUrlResolver>());

        }

    }
}
