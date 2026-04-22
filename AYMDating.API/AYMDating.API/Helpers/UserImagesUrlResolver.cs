using AutoMapper;
using AYMDating.API.DTO;
using AYMDating.DAL.Entities;

namespace AYMDating.API.Helpers
{
    //public class UserImagesUrlResolver : IValueResolver<List<UserImage>, UserImagesDTO, List<string>>
    //{
    //    private readonly IConfiguration _configuration;
    //    private List<string> userImageLinks;

    //    public UserImagesUrlResolver(IConfiguration configuration)
    //    {
    //        _configuration = configuration;
    //    }
    //    public List<string> Resolve(List<UserImage> source, UserImagesDTO destination, List<string> destMember, ResolutionContext context)
    //    {
    //        foreach (var item in source)
    //        {
    //            if (!string.IsNullOrEmpty(item.LinkName))
    //            {
    //                userImageLinks.Add(_configuration["ApiUrl"] + item.LinkName);
    //            }
    //        }
    //        if (userImageLinks.Count > 0)
    //            return userImageLinks;

    //        return null;
    //    }
    //}


    //public class UserImagesUrlResolver : IValueResolver<UserImage, UserImagesDTO, string>
    //{
    //    private readonly IConfiguration _configuration;

    //    public UserImagesUrlResolver(IConfiguration configuration)
    //    {
    //        _configuration = configuration;
    //    }
    //    public string Resolve(UserImage source, UserImagesDTO destination, string destMember, ResolutionContext context)
    //    {
    //        if (!string.IsNullOrEmpty(source.LinkName))
    //            return _configuration["ApiUrl"] + source.LinkName;

    //        return null;
    //    }
    // }
}
