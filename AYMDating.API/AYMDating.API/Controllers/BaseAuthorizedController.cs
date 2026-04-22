using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AYMDating.API.Controllers
{
    //[Authorize]
    [Authorize(Roles = "User")]
    public class BaseAuthorizedController : BaseController
    {
    }
}
