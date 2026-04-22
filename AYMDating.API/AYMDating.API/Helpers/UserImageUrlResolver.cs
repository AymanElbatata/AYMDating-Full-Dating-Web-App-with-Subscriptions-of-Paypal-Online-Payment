using AutoMapper;
using AYMDating.API.DTO;
using AYMDating.DAL.Entities;

namespace AYMDating.API.Helpers
{
    public class UserImageUrlResolver : IValueResolver<UserImage, UserImagesDTO, string>
    {
        private readonly IConfiguration _configuration;

        public UserImageUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(UserImage source, UserImagesDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.LinkName))
                return _configuration["ApiUrl"] + source.LinkName;

            return null;
        }
    }
}
