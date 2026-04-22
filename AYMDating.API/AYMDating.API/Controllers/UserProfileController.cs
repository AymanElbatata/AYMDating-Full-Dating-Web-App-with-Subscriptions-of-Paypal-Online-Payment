using AutoMapper;
using AYMDating.API.DTO;
using AYMDating.API.Helpers;
using AYMDating.DAL.BaseEntity;
using AYMDating.DAL.Entities;
using AYMDating.DAL.Interfaces;
using AYMDating.DAL.Repositories;
using AYMDating.DAL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AYMDating.API.Controllers
{
    public class UserProfileController : BaseAuthorizedController
    {
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        private readonly string UserInValid1 = "Sorry, Invalid User!";
        private readonly string UserInValid2 = "Sorry, Invalid Users!";
        private readonly string UserInValid3 = "Sorry, Invalid Users or Blocking!";
        //private readonly List<string> UserTokenData2;

        public UserProfileController(IMapper mapper, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
            //UserTokenData2 = new List<string>();
            //GetInfoFromUser();
        }



        #region GET APIS

        //[HttpPost("GetUserHistoryWithImagesByUserId/{UserId}")]
        //[Route("GetSplashIdentity/{CustomerId}")]
        [HttpGet("GetUserHistoriesWithMainImagesForAll")]
        public async Task<ActionResult<ReturnValueDTO>> GetUserHistoriesWithMainImagesForAll()
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                return Ok(new ReturnValueDTO(ReturnType.Success, await MethodGetUserHistoriesWithMainImagesForAll(unitOfWork.AppSession.User1[0])));
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet("GetHistoryWithMainImagesForCurrentUser")]
        public async Task<ActionResult<ReturnValueDTO>> GetHistoryWithMainImagesForCurrentUser()
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }
                return Ok(new ReturnValueDTO(ReturnType.Success, MethodGetUserHistoryWithMainImagesByUserId(unitOfWork.AppSession.User1[0])));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        //[HttpGet("GetHistoryWithMainImageBytUserId/{UserId}")]
        //public async Task<ActionResult<ReturnValueDTO>> GetHistoryWithMainImageBytUserId(string UserId)
        //{
        //    try
        //    {
        //        if (!await MethodGetIsUserValidByUserId(UserId))
        //        {
        //            return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
        //        }
        //        return Ok(new ReturnValueDTO(ReturnType.Success, MethodGetUserHistoryWithMainImagesByUserId(UserId)));
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
        //    }
        //}

        //[HttpGet("GetHistoryWithFullImagesForCurrentUser")]
        //public async Task<ActionResult<ReturnValueDTO>> GetHistoryWithFullImagesForCurrentUser()
        //{
        //    try
        //    {
        //        if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
        //        {
        //            return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
        //        }
        //        return Ok(new ReturnValueDTO(ReturnType.Success, await MethodGetUserHistoryWithFullImagesByUserId(unitOfWork.AppSession.User1[0])));
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
        //    }
        //}

        //[HttpGet("GetMatchHistoriesWithMainImagesForCurrentUser")]
        //public async Task<ActionResult<ReturnValueDTO>> GetMatchHistoriesWithMainImagesForCurrentUser()
        //{
        //    try
        //    {
        //        if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
        //        {
        //            return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
        //        }
        //        return Ok(new ReturnValueDTO(ReturnType.Success, await MethodGetUserMatchHistoriesWithMainImagesByUserId(unitOfWork.AppSession.User1[0])));
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
        //    }
        //}

        [HttpPost("GetUserSearchIdsUsersForCurrentUser")]
        public async Task<ActionResult<ReturnValueDTO>> GetUserSearchIdsUsersForCurrentUser(UserMainSearchDTO model)
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }
                return Ok(new ReturnValueDTO(ReturnType.Success, await MethodGetUserSearchIdsUsersForByUserId(unitOfWork.AppSession.User1[0], model)));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        [HttpGet("GetUserMatchesIdsUsersForCurrentUser")]
        public async Task<ActionResult<ReturnValueDTO>> GetUserMatchesIdsUsersForCurrentUser()
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }
                return Ok(new ReturnValueDTO(ReturnType.Success, await MethodGetUserMatchesIdsUsersForByUserId(unitOfWork.AppSession.User1[0])));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        [HttpGet("GetUserOnlineIdsUsersForCurrentUser")]
        public async Task<ActionResult<ReturnValueDTO>> GetUserOnlineIdsUsersForCurrentUser()
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }
                return Ok(new ReturnValueDTO(ReturnType.Success, await MethodGetUseOnlineIdsUsersForByUserId(unitOfWork.AppSession.User1[0])));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        [HttpGet("GetAllLookUpTablesForUserProfile")]
        public async Task<ActionResult<ReturnValueDTO>> GetAllLookUpTablesForUserProfile()
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }
                return Ok(new ReturnValueDTO(ReturnType.Success, MethodGetAllLookUpTablesForUserProfile()));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }


        [HttpGet("GetAllCountries")]
        public async Task<ActionResult<ReturnValueDTO>> GetAllCountries()
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }
                return Ok(new ReturnValueDTO(ReturnType.Success,  MethodGetGetAllCountries()));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        [HttpGet("GetAllGenders")]
        public async Task<ActionResult<ReturnValueDTO>> GetAllGender()
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }
                return Ok(new ReturnValueDTO(ReturnType.Success,  MethodGetAllGenders()));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        [HttpGet("GetAllEducations")]
        public async Task<ActionResult<ReturnValueDTO>> GetAllEducations()
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }
                return Ok(new ReturnValueDTO(ReturnType.Success,  MethodGetAllEducations()));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        [HttpGet("GetAllMaritalStatuses")]
        public async Task<ActionResult<ReturnValueDTO>> GetAllMaritalStatuses()
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }
                return Ok(new ReturnValueDTO(ReturnType.Success,  MethodGetAllMaritalStatuses()));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        [HttpGet("GetAllPurposes")]
        public async Task<ActionResult<ReturnValueDTO>> GetAllPurposes()
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }
                return Ok(new ReturnValueDTO(ReturnType.Success,  MethodGetAllPurposes()));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        [HttpGet("GetAllJobs")]
        public async Task<ActionResult<ReturnValueDTO>> GetAllJobs()
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }
                return Ok(new ReturnValueDTO(ReturnType.Success, MethodGetAllJobs()));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        [HttpGet("GetAllLanguages")]
        public async Task<ActionResult<ReturnValueDTO>> GetAllLanguages()
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }
                return Ok(new ReturnValueDTO(ReturnType.Success, MethodGetAllLanguages()));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        [HttpGet("GetAllFinancialModes")]
        public async Task<ActionResult<ReturnValueDTO>> GetAllFinancialModes()
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }
                return Ok(new ReturnValueDTO(ReturnType.Success,  MethodGetAllFinancialModes()));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        [HttpGet("GetUserHistoryWithFullImagesByUserId/{UserId}")]
        public async Task<ActionResult<ReturnValueDTO>> GetUserHistoryWithFullImagesByUserId(string UserId)
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(UserId))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }
                return Ok(new ReturnValueDTO(ReturnType.Success, await MethodGetUserHistoryWithFullImagesByUserId(UserId)));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        [HttpPost("GetUserMessagesByUserIdandAnotherUser")]
        public async Task<ActionResult<ReturnValueDTO>> GetUserMessagesByUserIdandAnotherUser(UserMessageDTO model)
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }

                return Ok(new ReturnValueDTO(ReturnType.Success, await MethodGetUserMessagesByUserIdandAnotherUser(unitOfWork.AppSession.User1[0],model.ReceiverAppUserId)));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        [HttpPost("GetLatestUserMessagesForCurrentUser")]
        public async Task<ActionResult<ReturnValueDTO>> GetLatestUserMessagesForCurrentUser()
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }

                return Ok(new ReturnValueDTO(ReturnType.Success, await MethodGetLatestUserMessagesForCurrentUser(unitOfWork.AppSession.User1[0])));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        [HttpPost("GetUserMessageGroupNameOrCreateItByUserId1ToUserId2")]
        public async Task<ActionResult<ReturnValueDTO>> GetUserMessageGroupNameOrCreateItByUserId1ToUserId2(UserViewOrLikeOrFavoriteOrBlockOrReportDTO model)
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }

                return Ok(new ReturnValueDTO(ReturnType.Success, await MethodGetUserMessageGroupNameOrCreateItByUserId1ToUserId2(unitOfWork.AppSession.User1[0], model.ReceiverAppUserId)));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        [HttpPost("GetReturnIsThereBlockToThisUser")]
        public async Task<ActionResult<ReturnValueDTO>> GetReturnIsThereBlockToThisUser(UserViewOrLikeOrFavoriteOrBlockOrReportDTO model)
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }

                return Ok(new ReturnValueDTO(ReturnType.Success, await MethodGetReturnIsThereBlockToThisUser(unitOfWork.AppSession.User1[0], model.ReceiverAppUserId)));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }


        [HttpGet("GetCurrentUserViews")]
        public async Task<ActionResult<ReturnValueDTO>> GetCurrentUserViews()
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }

                return Ok(new ReturnValueDTO(ReturnType.Success, await MethodGetUserViewLikeFavoriteBlockReportByUserId(unitOfWork.AppSession.User1[0], 1)));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        [HttpGet("GetCurrentUserLikes")]
        public async Task<ActionResult<ReturnValueDTO>> GetCurrentUserLikes()
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }

                return Ok(new ReturnValueDTO(ReturnType.Success, await MethodGetUserViewLikeFavoriteBlockReportByUserId(unitOfWork.AppSession.User1[0],2)));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        [HttpGet("GetCurrentUserFavorites")]
        public async Task<ActionResult<ReturnValueDTO>> GetCurrentUserFavorites()
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }

                return Ok(new ReturnValueDTO(ReturnType.Success, await MethodGetUserViewLikeFavoriteBlockReportByUserId(unitOfWork.AppSession.User1[0], 3)));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        [HttpGet("GetCurrentUserBlocks")]
        public async Task<ActionResult<ReturnValueDTO>> GetCurrentUserBlocks()
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }

                return Ok(new ReturnValueDTO(ReturnType.Success, await MethodGetUserViewLikeFavoriteBlockReportByUserId(unitOfWork.AppSession.User1[0], 4)));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }
        #endregion

        #region POST SEND APIS

        [HttpPost("SendUpdateHistoryWithMainImagesForCurrentUser")]
        public async Task<ActionResult<ReturnValueDTO>> SendUpdateHistoryWithMainImagesForCurrentUser(UserHistoryDTO model)
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }
                return Ok(new ReturnValueDTO(ReturnType.Success, await MethodSendUpdateHistoryWithMainImagesForCurrentUser(unitOfWork.AppSession.User1[0], model)));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        [HttpPost("SendUploadUserMainImageForCurrentUser")]
        public async Task<ActionResult<ReturnValueDTO>> SendUploadUserMainImageForCurrentUser([FromForm] IFormFile file)
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }
                return Ok(new ReturnValueDTO(ReturnType.Success, await MethodSendUploadUserMainImageForCurrentUser(unitOfWork.AppSession.User1[0], file)));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        [HttpPost("SendUploadUserOtherImageForCurrentUser1")]
        public async Task<ActionResult<ReturnValueDTO>> SendUploadUserOtherImageForCurrentUser1([FromForm] IFormFile file)
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }
                return Ok(new ReturnValueDTO(ReturnType.Success, await MethodSendUploadUserOtherImageForCurrentUser(unitOfWork.AppSession.User1[0], file, 1)));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        [HttpPost("SendUploadUserOtherImageForCurrentUser2")]
        public async Task<ActionResult<ReturnValueDTO>> SendUploadUserOtherImageForCurrentUser2([FromForm] IFormFile file)
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }
                return Ok(new ReturnValueDTO(ReturnType.Success, await MethodSendUploadUserOtherImageForCurrentUser(unitOfWork.AppSession.User1[0], file, 2)));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        [HttpPost("SendUploadUserOtherImageForCurrentUser3")]
        public async Task<ActionResult<ReturnValueDTO>> SendUploadUserOtherImageForCurrentUser3([FromForm] IFormFile file)
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }
                return Ok(new ReturnValueDTO(ReturnType.Success, await MethodSendUploadUserOtherImageForCurrentUser(unitOfWork.AppSession.User1[0], file, 3)));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        [HttpPost("SendUploadUserOtherImageForCurrentUser4")]
        public async Task<ActionResult<ReturnValueDTO>> SendUploadUserOtherImageForCurrentUser4([FromForm] IFormFile file)
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }
                return Ok(new ReturnValueDTO(ReturnType.Success, await MethodSendUploadUserOtherImageForCurrentUser(unitOfWork.AppSession.User1[0], file, 4)));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        //[HttpGet("GetUserImageFile/{fileName}")]
        //public IActionResult GetUserImageFile(string fileName)
        //{
        //    string filePath = Path.Combine(UserImagesUrlSpecs.UrlUserImagesSave, fileName);

        //    if (System.IO.File.Exists(filePath))
        //    {
        //        var fileExtension = Path.GetExtension(filePath).ToLowerInvariant();
        //        var contentType = fileExtension switch
        //        {
        //            ".jpg" or ".jpeg" => "image/jpeg",
        //            ".png" => "image/png",
        //            ".gif" => "image/gif",
        //            _ => "application/octet-stream",
        //        };

        //        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        //        return File(fileStream, contentType);
        //    }
        //    return null;

        //}
        //[HttpPost("SendUploadUserOtherImagesForCurrentUser")]
        //public async Task<ActionResult<ReturnValueDTO>> UploadUserOtherImagesForCurrentUser(UserImagesUploadDTO model)
        //{
        //    try
        //    {
        //        if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
        //        {
        //            return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
        //        }
        //        return Ok(new ReturnValueDTO(ReturnType.Success, await MethodSendUploadUserOtherImagesForCurrentUser(unitOfWork.AppSession.User1[0], model)));
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
        //    }
        //}

        //[HttpPost("SendHistoryWithMainImagesForMainSearch")]
        //public async Task<ActionResult<ReturnValueDTO>> SendHistoryWithMainImagesForMainSearch([FromQuery] UserMainSearchDTO model)
        //{
        //    try
        //    {
        //        if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
        //        {
        //            return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
        //        }
        //        return Ok(new ReturnValueDTO(ReturnType.Success, await MethodGetUserHistoriesWithMainImagesForSearch(unitOfWork.AppSession.User1[0], model)));
        //    }
        //    catch (Exception ex)
        //    {
        //        return Unauthorized(ex.Message);
        //    }
        //}

        [HttpPost("SendMessageFromCurrentUserToAnother")]
        public async Task<ActionResult<ReturnValueDTO>> SendMessageFromCurrentUserToAnother(UserMessageDTO model)
        {
            try
            {
                if (await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]) && await MethodGetIsUserValidByUserId(model.ReceiverAppUserId) && !await MethodGetReturnIsThereBlockToThisUser(unitOfWork.AppSession.User1[0], model.ReceiverAppUserId))
                {
                    string MessageGuid = Guid.NewGuid().ToString();
                    var Message = new UserMessage
                    {
                        SenderAppUserId = unitOfWork.AppSession.User1[0],
                        ReceiverAppUserId = model.ReceiverAppUserId,
                        Message = model.Message.Trim().ToString().Length > 0 ? model.Message : ".",
                        CreatedUserID = unitOfWork.AppSession.User1[0],
                        MessageGuid = MessageGuid
                    };
                    await unitOfWork.UserMessageRepository.Add(Message);

                    var SavedMessage = unitOfWork.UserMessageRepository.FindByQuery(a => a.MessageGuid == MessageGuid).FirstOrDefault();
                    SavedMessage.Message = GetOneMessageForTwoUsersAccordingToPackages(SavedMessage.Message, unitOfWork.AppSession.User1[0], model.ReceiverAppUserId);
                    return Ok(new ReturnValueDTO(ReturnType.Success, mapper.Map<UserMessageDTO>(SavedMessage)));
                }
                return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid3));
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("SendViewFromCurrentUserToAnother")]
        public async Task<ActionResult<ReturnValueDTO>> SendViewFromCurrentUserToAnother( UserViewOrLikeOrFavoriteOrBlockOrReportDTO model)
        {
            try
            {
                if (await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]) && await MethodGetIsUserValidByUserId(model.ReceiverAppUserId))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Success, await MethodSendViewLikeFavoriteBlockReportFromUserToAnother(unitOfWork.AppSession.User1[0],model.ReceiverAppUserId,1)));
                }
                return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid3));
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("SendLikeFromCurrentUserToAnother")]
        public async Task<ActionResult<ReturnValueDTO>> SendLikeFromCurrentUserToAnother(UserViewOrLikeOrFavoriteOrBlockOrReportDTO model)
        {
            try
            {
                if (await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]) && await MethodGetIsUserValidByUserId(model.ReceiverAppUserId) && !await MethodGetReturnIsThereBlockToThisUser(unitOfWork.AppSession.User1[0], model.ReceiverAppUserId))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Success, await MethodSendViewLikeFavoriteBlockReportFromUserToAnother(unitOfWork.AppSession.User1[0], model.ReceiverAppUserId, 2)));
                }
                return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid3));
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("SendFavoriteCurrentFromUserToAnother")]
        public async Task<ActionResult<ReturnValueDTO>> SendFavoriteCurrentFromUserToAnother( UserViewOrLikeOrFavoriteOrBlockOrReportDTO model)
        {
            try
            {
                if (await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]) && await MethodGetIsUserValidByUserId(model.ReceiverAppUserId) && !await MethodGetReturnIsThereBlockToThisUser(unitOfWork.AppSession.User1[0], model.ReceiverAppUserId))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Success, await MethodSendViewLikeFavoriteBlockReportFromUserToAnother(unitOfWork.AppSession.User1[0], model.ReceiverAppUserId, 3)));
                }
                return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid3));
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("SendBlockFromCurrentUserToAnother")]
        public async Task<ActionResult<ReturnValueDTO>> SendBlockFromCurrentUserToAnother( UserViewOrLikeOrFavoriteOrBlockOrReportDTO model)
        {
            try
            {
                if (await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]) && await MethodGetIsUserValidByUserId(model.ReceiverAppUserId))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Success, await MethodSendViewLikeFavoriteBlockReportFromUserToAnother(unitOfWork.AppSession.User1[0], model.ReceiverAppUserId, 4)));
                }
                return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid3));              

            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("SendReportFromCurrentUserToAnother")]
        public async Task<ActionResult<ReturnValueDTO>> SendReportFromCurrentUserToAnother(UserViewOrLikeOrFavoriteOrBlockOrReportDTO model)
        {
            try
            {
                if (await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]) && await MethodGetIsUserValidByUserId(model.ReceiverAppUserId))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Success, await MethodSendViewLikeFavoriteBlockReportFromUserToAnother(unitOfWork.AppSession.User1[0], model.ReceiverAppUserId, 5)));
                }
                return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid3));
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        //[HttpPost("SendSeenFromCurrentUserMessageByMessageId/{MessageId}")]
        //public async Task<ActionResult<ReturnValueDTO>> SendSeenFromCurrentUserMessageByMessageId(int MessageId)
        //{
        //    try
        //    {
        //        if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
        //        {
        //            return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
        //        }

        //        return Ok(new ReturnValueDTO(ReturnType.Success, await MethodSendSeenUserMessageByMessageId(unitOfWork.AppSession.User1[0],MessageId)));
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
        //    }
        //}

        [HttpPost("SendSwitchedOffFromCurrentUser")]
        public async Task<ActionResult<ReturnValueDTO>> SendSwitchedOffFromCurrentUser()
        {
            try
            {
                if (!await MethodGetIsUserValidToDeleteSwitchedOffByUserId(unitOfWork.AppSession.User1[0]))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }
                await MethodSendSwitchedOffUserAccountByUserId(unitOfWork.AppSession.User1[0]);
                await unitOfWork.SignInManager.SignOutAsync();
                return Ok(new ReturnValueDTO(ReturnType.Success, true));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        [HttpPost("SendDeleteAccountFromCurrentUser")]
        public async Task<ActionResult<ReturnValueDTO>> SendDeleteAccountFromCurrentUser()
        {
            try
            {
                if (!await MethodGetIsUserValidToDeleteSwitchedOffByUserId(unitOfWork.AppSession.User1[0]))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }
                await MethodSendDeleteAccountByUserId(unitOfWork.AppSession.User1[0]);
                await unitOfWork.SignInManager.SignOutAsync();
                return Ok(new ReturnValueDTO(ReturnType.Success, true));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }
        #endregion

        #region DELETE APIS
        
        [HttpPost("DeleteLikeFromCurrentUserToAnother")]
        public async Task<ActionResult<ReturnValueDTO>> DeleteLikeFromCurrentUserToAnother(UserViewOrLikeOrFavoriteOrBlockOrReportDTO model)
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]) || !await MethodGetIsUserValidByUserId(model.ReceiverAppUserId))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid2));
                }

                return Ok(new ReturnValueDTO(ReturnType.Success, await MethodDeleteLikeOrFavoriteOrBlockFromUserToAnother(unitOfWork.AppSession.User1[0], model.ReceiverAppUserId,1)));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        [HttpPost("DeleteFavoriteFromCurrentUserToAnother")]
        public async Task<ActionResult<ReturnValueDTO>> DeleteFavoriteFromCurrentUserToAnother(UserViewOrLikeOrFavoriteOrBlockOrReportDTO model)
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]) || !await MethodGetIsUserValidByUserId(model.ReceiverAppUserId))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid2));
                }

                return Ok(new ReturnValueDTO(ReturnType.Success, await MethodDeleteLikeOrFavoriteOrBlockFromUserToAnother(unitOfWork.AppSession.User1[0], model.ReceiverAppUserId, 2)));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        [HttpPost("DeleteBlockFromCurrentUserToAnother")]
        public async Task<ActionResult<ReturnValueDTO>> DeleteBlockFromCurrentUserToAnother(UserViewOrLikeOrFavoriteOrBlockOrReportDTO model)
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]) || !await MethodGetIsUserValidByUserId(model.ReceiverAppUserId))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid2));
                }

                return Ok(new ReturnValueDTO(ReturnType.Success, await MethodDeleteLikeOrFavoriteOrBlockFromUserToAnother(unitOfWork.AppSession.User1[0], model.ReceiverAppUserId, 3)));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        [HttpPost("DeleteSenderUserMessageByMessageId/{MessageId}")]
        public async Task<ActionResult<ReturnValueDTO>> DeleteSenderUserMessageByMessageId(int MessageId)
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }

                return Ok(new ReturnValueDTO(ReturnType.Success, await MethodDeleteSenderOrReceiverRMessageByMessageId(unitOfWork.AppSession.User1[0],MessageId,1)));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        [HttpPost("DeleteReceiverUserMessageByMessageId/{MessageId}")]
        public async Task<ActionResult<ReturnValueDTO>> DeleteReceiverUserMessageByMessageId(int MessageId)
        {
            try
            {
                if (!await MethodGetIsUserValidByUserId(unitOfWork.AppSession.User1[0]))
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
                }

                return Ok(new ReturnValueDTO(ReturnType.Success, await MethodDeleteSenderOrReceiverRMessageByMessageId(unitOfWork.AppSession.User1[0],MessageId,2)));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        //[HttpPost("DeleteSwitchedOffUserByUserId")]
        //public async Task<ActionResult<ReturnValueDTO>> DeleteSwitchedOffUserByUserId()
        //{
        //    try
        //    {
        //        if (!await MethodGetIsUserValidToDeleteSwitchedOffByUserId(unitOfWork.AppSession.User1[0]))
        //        {
        //            return Ok(new ReturnValueDTO(ReturnType.Failed, UserInValid1));
        //        }
        //        return Ok(new ReturnValueDTO(ReturnType.Success, await MethodDeleteSwitchedOffUserAccountByUserId(unitOfWork.AppSession.User1[0])));
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
        //    }
        //}
        #endregion

        #region GET Methods 
        private async Task<List<UserHistoryDTO>> MethodGetUserHistoriesWithMainImagesForAll(string UserId)
        {
            try
            {
            var CurrentUser = unitOfWork.UserHistoryRepository.FindByQuery(a => a.AppUserId == UserId && a.IsMain == true && a.IsDeleted == false).FirstOrDefault();
            if (CurrentUser != null)
            {
                var AllUsers = new List<UserHistoryDTO>();
                //var users = unitOfWork.UserManager.Users.Where(a => a.Id != UserId).ToList().OrderByDescending(a => a.DateOfJoin);
                var users = await GetAllValidUsers();
                if (users != null)
                {
                    foreach (var item in users)
                    {
                            var UserHistory = unitOfWork.UserHistoryRepository.FindByQuery(a => a.AppUserId == item.Id && a.IsMain == true && a.IsDeleted == false).FirstOrDefault();
                            //if (UserHistory!= null && UserHistory.GenderId != CurrentUser.GenderId)
                            if (UserHistory!= null)
                            {
                                    var UserWithData = mapper.Map<UserHistoryDTO>(UserHistory);
                                    //var UserWithData = GetUSerHistoryMapping(item.Id);
                                    UserWithData.IsCurrentUserVerified = unitOfWork.UserManager.Users.Where(a => a.Id == item.Id && a.Activated == true && a.IsDeleted == false).Any() ? true : false;
                                    UserWithData.IsCurrentUserOnline = unitOfWork.UserTokenTransactionRepository.FindByQuery(a => a.AppUserId == item.Id && a.IsDeleted == false).Any() ? unitOfWork.UserTokenTransactionRepository.FindByQuery(a => a.AppUserId == item.Id && a.IsDeleted == false).LastOrDefault().DateOfExpiration > DateTime.Now : false;
                                    UserWithData.IsLikedFromCurrentUser = unitOfWork.UserLikeRepository.FindByQuery(a => a.SenderAppUserId == UserId && a.ReceiverAppUserId == item.Id && a.IsDeleted == false).Any();
                                    UserWithData.IsBlockedFromCurrentUser = unitOfWork.UserBlockRepository.FindByQuery(a => a.SenderAppUserId == UserId && a.ReceiverAppUserId == item.Id && a.IsDeleted == false).Any();
                                    UserWithData.IsFavoriteFromCurrentUser = unitOfWork.UserFavoriteRepository.FindByQuery(a => a.SenderAppUserId == UserId && a.ReceiverAppUserId == item.Id && a.IsDeleted == false).Any();
                                    var UserMainPhotoExisting = unitOfWork.UserImageRepository.FindByQuery(a => a.AppUserId == item.Id && a.IsMain == true && a.IsDeleted == false).Any();
                                    if (UserMainPhotoExisting)
                                    {
                                        if (unitOfWork.UserImageRepository.FindByQuery(a => a.AppUserId == item.Id && a.IsMain == true && a.IsDeleted == false).Any())
                                        {
                                            var UserMainPhoto = mapper.Map<UserImagesDTO>(unitOfWork.UserImageRepository.FindByQuery(a => a.AppUserId == item.Id && a.IsMain == true && a.IsDeleted == false).FirstOrDefault());
                                            UserWithData.MainImage = UserMainPhoto != null ? UserMainPhoto.LinkName : string.Empty;
                                        }
                                        else
                                            UserWithData.MainImage = string.Empty;

                                    }
                                    AllUsers.Add(UserWithData);
                            }
                        
                    }
                        return AllUsers;
                    }
                }
                return new List<UserHistoryDTO>();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //private async Task<List<UserHistoryDTO>> MethodGetUserHistoriesWithMainImagesForSearch(string UserId, UserMainSearchDTO model)
        //{
        //    var CurrentUser = unitOfWork.UserHistoryRepository.FindByQuery(a => a.AppUserId == UserId && a.IsMain == true && a.IsDeleted == false).FirstOrDefault();
        //    if (await MethodGetIsUserValidByUserId(CurrentUser.AppUserId) && CurrentUser != null)
        //    {
        //        var AllUsers = new List<UserHistoryDTO>();
        //        var users = await GetAllValidUsers();
        //        if (users != null)
        //        {
        //            foreach (var item in users)
        //            {
        //                    var UserHistory = unitOfWork.UserHistoryRepository.FindByQuery(a => a.AppUserId == item.Id && a.IsMain == true && a.IsDeleted == false).FirstOrDefault();
        //                    if (UserHistory != null)
        //                    {
        //                        var UserWithData = mapper.Map<UserHistoryDTO>(UserHistory);
        //                        UserWithData.IsCurrentUserOnline = unitOfWork.UserTokenTransactionRepository.FindByQuery(a => a.AppUserId == item.Id && a.IsDeleted == false).Any() ? unitOfWork.UserTokenTransactionRepository.FindByQuery(a => a.AppUserId == item.Id && a.IsDeleted == false).LastOrDefault().DateOfExpiration > DateTime.Now : false;
        //                        UserWithData.IsBlockedFromCurrentUser = unitOfWork.UserBlockRepository.FindByQuery(a => a.SenderAppUserId == UserId && a.ReceiverAppUserId == item.Id && a.IsDeleted == false).Any();
        //                        UserWithData.IsLikedFromCurrentUser = unitOfWork.UserLikeRepository.FindByQuery(a => a.SenderAppUserId == UserId && a.ReceiverAppUserId == item.Id && a.IsDeleted == false).Any();
        //                        UserWithData.IsFavoriteFromCurrentUser = unitOfWork.UserFavoriteRepository.FindByQuery(a => a.SenderAppUserId == UserId && a.ReceiverAppUserId == item.Id && a.IsDeleted == false).Any();

        //                            var UserMainPhotoExisting = unitOfWork.UserImageRepository.FindByQuery(a => a.AppUserId == item.Id && a.IsMain == true && a.IsDeleted == false).Any();
        //                            if (UserMainPhotoExisting)
        //                            {
        //                                if (unitOfWork.UserImageRepository.FindByQuery(a => a.AppUserId == item.Id && a.IsMain == true && a.IsDeleted == false).Any())
        //                                {
        //                                    var UserMainPhoto = mapper.Map<UserImagesDTO>(unitOfWork.UserImageRepository.FindByQuery(a => a.AppUserId == item.Id && a.IsMain == true && a.IsDeleted == false).FirstOrDefault());
        //                                    UserWithData.MainImage = UserMainPhoto != null ? UserMainPhoto.LinkName : string.Empty;
        //                                }
        //                                else
        //                                    UserWithData.MainImage = string.Empty;
        //                            }
        //                        if (UserWithData.Age >= model.AgeFrom && UserWithData.Age <= model.AgeTo && UserWithData.CountryId == model.Country && UserWithData.GenderId == model.IwantGender && (model.UserHasImage == true ? UserWithData.MainImage != null : false) )
        //                        {
        //                            AllUsers.Add(UserWithData);
        //                        }
        //                    }
                        
        //            }
        //        }
        //        return AllUsers;
        //    }
        //    return new List<UserHistoryDTO>();
        //}

        //private async Task<List<UserHistoryDTO>> MethodGetUserMatchHistoriesWithMainImagesByUserId(string UserId)
        //{
        //    var CurrentUser = unitOfWork.UserHistoryRepository.FindByQuery(a => a.AppUserId == UserId && a.IsMain == true && a.IsDeleted == false).FirstOrDefault();

        //    if (await MethodGetIsUserValidByUserId(CurrentUser.AppUserId) && CurrentUser != null)
        //    {
        //        var AllUsers = new List<UserHistoryDTO>();
        //        var users = await GetAllValidUsers();
        //        if (users != null)
        //        {
        //            foreach (var item in users)
        //            {
        //                var UserHistory = unitOfWork.UserHistoryRepository.FindByQuery(a => a.AppUserId == item.Id && a.IsMain == true && a.IsDeleted == false).FirstOrDefault();
        //                if (UserHistory != null && CurrentUser.GenderId != UserHistory.GenderId)
        //                {
        //                    var UserWithData = mapper.Map<UserHistoryDTO>(UserHistory);
        //                    UserWithData.IsCurrentUserOnline = unitOfWork.UserTokenTransactionRepository.FindByQuery(a => a.AppUserId == item.Id && a.IsDeleted == false).Any() ? unitOfWork.UserTokenTransactionRepository.FindByQuery(a => a.AppUserId == item.Id && a.IsDeleted == false).LastOrDefault().DateOfExpiration > DateTime.Now : false;
        //                    UserWithData.IsBlockedFromCurrentUser = unitOfWork.UserBlockRepository.FindByQuery(a => a.SenderAppUserId == UserId && a.ReceiverAppUserId == item.Id && a.IsDeleted == false).Any();
        //                    UserWithData.IsLikedFromCurrentUser = unitOfWork.UserLikeRepository.FindByQuery(a => a.SenderAppUserId == UserId && a.ReceiverAppUserId == item.Id && a.IsDeleted == false).Any();
        //                    UserWithData.IsFavoriteFromCurrentUser = unitOfWork.UserFavoriteRepository.FindByQuery(a => a.SenderAppUserId == UserId && a.ReceiverAppUserId == item.Id && a.IsDeleted == false).Any();

        //                    var UserMainPhotoExisting = unitOfWork.UserImageRepository.FindByQuery(a => a.AppUserId == item.Id && a.IsMain == true && a.IsDeleted == false).Any();
        //                    if (UserMainPhotoExisting)
        //                    {
        //                        if (unitOfWork.UserImageRepository.FindByQuery(a => a.AppUserId == item.Id && a.IsMain == true && a.IsDeleted == false).Any())
        //                        {
        //                            var UserMainPhoto = mapper.Map<UserImagesDTO>(unitOfWork.UserImageRepository.FindByQuery(a => a.AppUserId == item.Id && a.IsMain == true && a.IsDeleted == false).FirstOrDefault());
        //                            UserWithData.MainImage = UserMainPhoto != null ? UserMainPhoto.LinkName : string.Empty;
        //                        }
        //                        else
        //                            UserWithData.MainImage = string.Empty;
        //                    }

        //                    if (UserWithData.Age >= UserHistory.SearchAgeFrom && UserWithData.Age <= UserHistory.SearchAgeTo)
        //                    {
        //                        AllUsers.Add(UserWithData);
        //                    }
        //                }
        //            }
        //        }
        //        return AllUsers;
        //    }
        //    return new List<UserHistoryDTO>();
        //}

        private async Task<List<string>> MethodGetUserMatchesIdsUsersForByUserId(string UserId)
        {
            var CurrentUser = unitOfWork.UserHistoryRepository.FindByQuery(a => a.AppUserId == UserId && a.IsMain == true && a.IsDeleted == false).FirstOrDefault();

            if (await MethodGetIsUserValidByUserId(CurrentUser.AppUserId) && CurrentUser != null)
            {
                var AllUsers = new List<string>();
                var users = await GetAllValidUsers();
                if (users != null)
                {
                    foreach (var item in users)
                    {
                         var UserHistory = unitOfWork.UserHistoryRepository.FindByQuery(a => a.AppUserId == item.Id && a.IsMain == true && a.IsDeleted == false).FirstOrDefault();
                        if (UserHistory != null && CurrentUser.GenderId != UserHistory.GenderId)
                        {
                            if ((GetCurrentAgeforUser(item.DateOfBirth) >= UserHistory.SearchAgeFrom &&
                                 GetCurrentAgeforUser(item.DateOfBirth) <= UserHistory.SearchAgeTo &&
                                 UserHistory.PurposeId == CurrentUser.PurposeId) &&
                                (UserHistory.EducationId == CurrentUser.EducationId ||
                                 UserHistory.LanguageId == CurrentUser.LanguageId ||
                                 UserHistory.MaritalStatusId == CurrentUser.MaritalStatusId ||
                                 UserHistory.CountryId == CurrentUser.CountryId ||
                                 UserHistory.JobId == CurrentUser.JobId ||
                                 UserHistory.AboutPartner.Contains(CurrentUser.AboutYou) ||
                                 CurrentUser.AboutPartner.Contains(UserHistory.AboutYou) ||
                                 UserHistory.AboutYou.Contains(CurrentUser.AboutPartner) ||
                                 CurrentUser.AboutYou.Contains(UserHistory.AboutPartner) ||
                                 UserHistory.ProfileHeading.Contains(CurrentUser.ProfileHeading) ||
                                 CurrentUser.ProfileHeading.Contains(UserHistory.ProfileHeading)))
                            {
                                AllUsers.Add(UserHistory.AppUserId);
                            }
                        }
                        
                    }
                }
                return AllUsers;
            }
            return new List<string>();
        }

        private async Task<List<string>> MethodGetUseOnlineIdsUsersForByUserId(string UserId)
        {
            var CurrentUser = unitOfWork.UserHistoryRepository.FindByQuery(a => a.AppUserId == UserId && a.IsMain == true && a.IsDeleted == false).FirstOrDefault();

            if (await MethodGetIsUserValidByUserId(CurrentUser.AppUserId) && CurrentUser != null)
            {
                var AllUsers = new List<string>();
                var users = await GetAllValidUsers();
                if (users != null)
                {
                    foreach (var item in users)
                    {
                            var UserHistory = unitOfWork.UserHistoryRepository.FindByQuery(a => a.AppUserId == item.Id && a.IsMain == true && a.IsDeleted == false).FirstOrDefault();
                            var IsCurrentUserOnline = unitOfWork.UserTokenTransactionRepository.FindByQuery(a => a.AppUserId == item.Id && a.IsDeleted == false).Any() ? unitOfWork.UserTokenTransactionRepository.FindByQuery(a => a.AppUserId == item.Id && a.IsDeleted == false).LastOrDefault().DateOfExpiration > DateTime.Now : false;

                            if (UserHistory != null && CurrentUser.GenderId != UserHistory.GenderId && IsCurrentUserOnline)
                            {
                                AllUsers.Add(UserHistory.AppUserId);
                            }
                    }
                }
                return AllUsers;
            }
            return new List<string>();
        }
    
        private async Task<List<string>> MethodGetUserSearchIdsUsersForByUserId(string UserId, UserMainSearchDTO model)
        {
            var CurrentUser = unitOfWork.UserHistoryRepository.FindByQuery(a => a.AppUserId == UserId && a.IsMain == true && a.IsDeleted == false).FirstOrDefault();

            if (await MethodGetIsUserValidByUserId(CurrentUser.AppUserId) && CurrentUser != null)
            {
                var AllUsers = new List<string>();
                var users = unitOfWork.UserManager.Users.ToList().OrderByDescending(a => a.DateOfJoin);
                if (users != null)
                {
                    foreach (var item in users)
                    {
                        if (await MethodGetIsUserValidByUserId(item.Id))
                        { 
                        var UserMainPhotoExisting = unitOfWork.UserImageRepository.FindByQuery(a => a.AppUserId == item.Id && a.IsMain == true && a.IsDeleted == false).Any();
                        var UserHistory = unitOfWork.UserHistoryRepository.FindByQuery(a => a.AppUserId == item.Id && a.IsMain == true && a.IsDeleted == false).FirstOrDefault();
                        if (UserHistory != null && model.IwantGender == UserHistory.GenderId &&
                                 GetCurrentAgeforUser(item.DateOfBirth) >= UserHistory.SearchAgeFrom &&
                                 GetCurrentAgeforUser(item.DateOfBirth) <= UserHistory.SearchAgeTo &&
                                 UserHistory.CountryId == model.Country)
                        {

                                if (model.UserHasImage == true && !UserMainPhotoExisting)
                                {

                                }
                                else
                                {
                                    AllUsers.Add(UserHistory.AppUserId);
                                }
                        }
                        
                        }
                    }
                }
                return AllUsers;
            }
            return new List<string>();
        }

           private async Task<AppUser> GetUserById(string UserId)
            => await unitOfWork.UserManager.FindByIdAsync(UserId);

        private async Task<bool> MethodSendUploadUserMainImageForCurrentUser(string UserId, IFormFile UserImage)
        {
            try
            {
                var GetUser = await GetUserById(UserId);
                var UserMainImage = unitOfWork.UserImageRepository.FindByQuery(a => a.AppUserId == UserId && a.Order == null && a.IsMain == true && a.IsDeleted == false).FirstOrDefault();
                if (UserMainImage != null)
                {
                    UserMainImage.IsDeleted = true;
                    UserMainImage.IsMain = false;
                   await unitOfWork.UserImageRepository.Update(UserMainImage);
                }

                var NewUserMainImage = new UserImage();
                NewUserMainImage.AppUserId = UserId;
                NewUserMainImage.CreatedUserID = UserId;
                NewUserMainImage.IsMain = true;
                NewUserMainImage.Order = null;

                var fileName = $"{GetUser.UserName + "_" + Guid.NewGuid()}{Path.GetExtension(UserImage.FileName)}";
                string _storagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","UserImages");
                string filePath = Path.Combine(_storagePath, fileName);



                // Ensure the directory exists
                if (!Directory.Exists(_storagePath))
                {
                    Directory.CreateDirectory(_storagePath);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await UserImage.CopyToAsync(stream);
                }

                NewUserMainImage.LinkName = fileName;
                await unitOfWork.UserImageRepository.Add(NewUserMainImage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        private async Task<bool> MethodSendUploadUserOtherImageForCurrentUser(string UserId, IFormFile UserImage, int Order)
        {
            try
            {
                var GetUser = await GetUserById(UserId);

                var UserOrderImage = unitOfWork.UserImageRepository.FindByQuery(a => a.AppUserId == UserId && a.IsMain == false && a.Order == Order && a.IsDeleted == false).FirstOrDefault();
                if (UserOrderImage != null)
                {
                    UserOrderImage.IsDeleted = true;
                    await unitOfWork.UserImageRepository.Update(UserOrderImage);
                }

                var NewUserOrderImage = new UserImage();
                NewUserOrderImage.AppUserId = UserId;
                NewUserOrderImage.CreatedUserID = UserId;
                NewUserOrderImage.Order = Order;
                NewUserOrderImage.IsMain = false;

                var fileName = $"{GetUser.UserName + "_" + Guid.NewGuid()}{Path.GetExtension(UserImage.FileName)}";
                string _storagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UserImages");
                string filePath = Path.Combine(_storagePath, fileName);



                // Ensure the directory exists
                if (!Directory.Exists(_storagePath))
                {
                    Directory.CreateDirectory(_storagePath);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await UserImage.CopyToAsync(stream);
                }

                NewUserOrderImage.LinkName = fileName;
                await unitOfWork.UserImageRepository.Add(NewUserOrderImage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        


        private List<Country> MethodGetGetAllCountries()
        {
            var list = unitOfWork.CountryRepository.ListAll().ToList();
            foreach (var item in list)
            {
                if (item.Name.Length > 20)
                {
                    item.Name = item.Name.Substring(0, 20);
                }
            }
            return list;
        }

        private List<Gender> MethodGetAllGenders()
        {
            return (List<Gender>)unitOfWork.GenderRepository.ListAll();
        }
        private List<Education> MethodGetAllEducations()
        {
            return (List<Education>)unitOfWork.EducationRepository.ListAll();
        }
        private List<MaritalStatus> MethodGetAllMaritalStatuses()
        {
            return (List<MaritalStatus>)unitOfWork.MaritalStatusRepository.ListAll();
        }
        private List<Purpose> MethodGetAllPurposes()
        {
            return (List<Purpose>)unitOfWork.PurposeRepository.ListAll();
        }
        private List<FinancialMode> MethodGetAllFinancialModes()
        {
            return (List<FinancialMode>)unitOfWork.FinancialModeRepository.ListAll();
        }
        private List<Job> MethodGetAllJobs()
        {
            var list = unitOfWork.JobRepository.ListAll().ToList();

            foreach (var item in list)
            {
                if (item.Name.Length > 20)
                {
                    item.Name = item.Name.Substring(0, 20);
                }
            }
            return list;
        }

        private List<Language> MethodGetAllLanguages()
        {
            return (List<Language>)unitOfWork.LanguageRepository.ListAll();
        }

        private LookUpTablesDTO MethodGetAllLookUpTablesForUserProfile()
        {
            return new LookUpTablesDTO() { AllCountries = MethodGetGetAllCountries(), AllGenders = MethodGetAllGenders(),
                AllEducations = MethodGetAllEducations(),
                 AllFinancialModes = MethodGetAllFinancialModes(),
                 AllPurposes = MethodGetAllPurposes(),
                 AllJobs = MethodGetAllJobs(),
                 AllLanguages = MethodGetAllLanguages(),
                 AllMaritalStatuses = MethodGetAllMaritalStatuses()
            };
        }


        private UserHistoryDTO MethodGetUserHistoryWithMainImagesByUserId(string UserId)
        {
            var UserHistory = unitOfWork.UserHistoryRepository.FindByQuery(a => a.AppUserId == UserId && a.IsMain == true && a.IsDeleted == false).FirstOrDefault();
            var UserWithData = mapper.Map<UserHistoryDTO>(UserHistory);

            UserWithData.IsCurrentUserOnline = unitOfWork.UserTokenTransactionRepository.FindByQuery(a => a.AppUserId == UserId && a.IsDeleted == false).Any() ? unitOfWork.UserTokenTransactionRepository.FindByQuery(a => a.AppUserId == UserId && a.IsDeleted == false).LastOrDefault().DateOfExpiration > DateTime.Now : false;
            UserWithData.CounterOfNewMessages = unitOfWork.UserMessageRepository.FindByQuery(a => a.ReceiverAppUserId == UserId && a.IsSeen == false && a.IsDeleted == false).Count();
            UserWithData.CounterOfNewViews = unitOfWork.UserViewRepository.FindByQuery(a => a.ReceiverAppUserId == UserId && a.IsSeen == false && a.IsDeleted == false).Count();
            UserWithData.CounterOfNewLikes = unitOfWork.UserLikeRepository.FindByQuery(a => a.ReceiverAppUserId == UserId && a.IsSeen == false && a.IsDeleted == false).Count();
            UserWithData.CounterOfNewFavorites = unitOfWork.UserFavoriteRepository.FindByQuery(a => a.ReceiverAppUserId == UserId && a.IsSeen == false && a.IsDeleted == false).Count();
            UserWithData.CounterOfNewBlocks = unitOfWork.UserBlockRepository.FindByQuery(a => a.ReceiverAppUserId == UserId && a.IsSeen == false && a.IsDeleted == false).Count();

            UserWithData.IsBlockedFromCurrentUser = unitOfWork.UserBlockRepository.FindByQuery(a => a.SenderAppUserId == unitOfWork.AppSession.User1[0] && a.ReceiverAppUserId == UserId && a.IsDeleted == false).Any();
            UserWithData.IsLikedFromCurrentUser = unitOfWork.UserLikeRepository.FindByQuery(a => a.SenderAppUserId == unitOfWork.AppSession.User1[0] && a.ReceiverAppUserId == UserId && a.IsDeleted == false).Any();
            UserWithData.IsFavoriteFromCurrentUser = unitOfWork.UserFavoriteRepository.FindByQuery(a => a.SenderAppUserId == unitOfWork.AppSession.User1[0] && a.ReceiverAppUserId == UserId && a.IsDeleted == false).Any();

            var UserMainPhotoExisting = unitOfWork.UserImageRepository.FindByQuery(a => a.AppUserId == UserId && a.IsMain == true && a.IsDeleted == false).Any();
            if (UserMainPhotoExisting)
            {
                if (unitOfWork.UserImageRepository.FindByQuery(a => a.AppUserId == UserId && a.IsMain == true && a.IsDeleted == false).Any())
                {
                    var UserMainPhoto = mapper.Map<UserImagesDTO>(unitOfWork.UserImageRepository.FindByQuery(a => a.AppUserId == UserId && a.IsMain == true && a.IsDeleted == false).FirstOrDefault());
                    UserWithData.MainImage = UserMainPhoto != null ? UserMainPhoto.LinkName : string.Empty;
                }
                else
                    UserWithData.MainImage = string.Empty;
            }
            return UserWithData;
        }

        private async Task<bool> MethodSendUpdateHistoryWithMainImagesForCurrentUser(string UserId, UserHistoryDTO model)
        {
            try
            {
                var UserHistory = unitOfWork.UserHistoryRepository.FindByQuery(a => a.AppUserId == model.AppUserId && a.IsMain == true && a.IsDeleted == false).FirstOrDefault();
                if (UserHistory != null)
                {
                    var NewUserHistory = new UserHistory();
                    NewUserHistory.CreatedUserID = UserId;
                    NewUserHistory.AppUserId = UserId;
                    NewUserHistory.IsMain = true;
                    NewUserHistory.City = model.City;
                    NewUserHistory.SearchAgeFrom = model.SearchAgeFrom;
                    NewUserHistory.SearchAgeTo = model.SearchAgeTo;
                    NewUserHistory.CountryId = model.CountryId;
                    NewUserHistory.EducationId = model.EducationId;
                    NewUserHistory.JobId = model.JobId;
                    NewUserHistory.FinancialModeId = model.FinancialModeId;
                    NewUserHistory.GenderId = model.GenderId;
                    NewUserHistory.MaritalStatusId = model.MaritalStatusId;
                    NewUserHistory.PurposeId = model.PurposeId;
                    NewUserHistory.LanguageId = model.LanguageId;

                    NewUserHistory.ProfileHeading = model.ProfileHeading.Length > 75 ? model.ProfileHeading.Trim().Substring(0,75) : model.ProfileHeading.Trim();
                    NewUserHistory.AboutYou = model.AboutYou.Length > 900 ? model.AboutYou.Trim().Substring(0, 900) : model.AboutYou.Trim();
                    NewUserHistory.AboutPartner = model.AboutPartner.Length > 900 ? model.AboutPartner.Trim().Substring(0,900) : model.AboutPartner.Trim();

                    NewUserHistory.UserPackageId = UserHistory.UserPackageId;
                    UserHistory.IsMain = false;
                    await unitOfWork.UserHistoryRepository.Update(UserHistory);
                    await unitOfWork.UserHistoryRepository.Add(NewUserHistory);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        private async Task<UserHistoryDTO> MethodGetUserHistoryWithFullImagesByUserId(string UserId)
        {
            var UserHistory = unitOfWork.UserHistoryRepository.FindByQuery(a => a.AppUserId == UserId && a.IsMain == true && a.IsDeleted == false).FirstOrDefault();
            var UserWithData = mapper.Map<UserHistoryDTO>(UserHistory);
            UserWithData.IsCurrentUserOnline = unitOfWork.UserTokenTransactionRepository.FindByQuery(a => a.AppUserId == UserId && a.IsDeleted == false).Any() ? unitOfWork.UserTokenTransactionRepository.FindByQuery(a => a.AppUserId == UserId && a.IsDeleted == false).LastOrDefault().DateOfExpiration > DateTime.Now : false;

            var UserMainPhotoExisting = unitOfWork.UserImageRepository.FindByQuery(a => a.AppUserId == UserId && a.IsMain == true && a.IsDeleted == false).Any();
            if (UserMainPhotoExisting)
            {
                if (unitOfWork.UserImageRepository.FindByQuery(a => a.AppUserId == UserId && a.IsMain == true && a.IsDeleted == false).Any())
                {
                    var UserMainPhoto = mapper.Map<UserImagesDTO>(unitOfWork.UserImageRepository.FindByQuery(a => a.AppUserId == UserId && a.IsMain == true && a.IsDeleted == false).FirstOrDefault());
                    UserWithData.MainImage = UserMainPhoto != null ? UserMainPhoto.LinkName : string.Empty;
                }
                else
                    UserWithData.MainImage = string.Empty;
            }

            var UserOtherImages = UserWithData != null ? unitOfWork.UserImageRepository.FindByQuery(a => a.AppUserId == UserId && a.IsMain == false && a.IsDeleted == false).OrderByDescending(a => a.CreationDate).ToList() : null;
            if (UserOtherImages != null)
            {
                foreach (var item in UserOtherImages)
                {
                    UserWithData.OtherImages.Add(mapper.Map<UserImagesDTO>(item));
                }
            }

            return UserWithData;
        }

        private async Task<List<UserMessageDTO>> MethodGetUserMessagesByUserIdandAnotherUser(string UserId, string UserId2)
        {
            var UserMessages = unitOfWork.UserMessageRepository.FindByQuery(a => ((a.SenderAppUserId == UserId && a.ReceiverAppUserId == UserId2) || (a.SenderAppUserId == UserId2 && a.ReceiverAppUserId == UserId)) && a.IsDeleted == false).OrderBy(a => a.CreationDate).ToList();

            foreach (var item in UserMessages)
            {
                if (item.ReceiverAppUserId == UserId && item.IsSeen == false)
                {
                    item.IsSeen = true;
                    await unitOfWork.UserMessageRepository.Update(item);
                }
                item.Message = GetOneMessageForTwoUsersAccordingToPackages(item.Message, item.SenderAppUserId, item.ReceiverAppUserId);
            }

            return mapper.Map<List<UserMessageDTO>>(UserMessages);
        }

        private async Task<List<UserMessageDTO>> MethodGetLatestUserMessagesForCurrentUser(string UserId)
        {
            var UserMessages = unitOfWork.UserMessageRepository.FindByQuery(a => a.ReceiverAppUserId == UserId && a.IsDeleted == false).OrderByDescending(a => a.CreationDate).ToList();

            foreach (var item in UserMessages)
            {
                item.Message = GetOneMessageForTwoUsersAccordingToPackages(item.Message, item.SenderAppUserId, item.SenderAppUserId);
            }

            return mapper.Map<List<UserMessageDTO>>(UserMessages);
        }


        private async Task<string> MethodGetUserMessageGroupNameOrCreateItByUserId1ToUserId2(string SenderAppUserId, string ReceiverAppUserId)
        {
            var UserMessageGroupExisting = unitOfWork.UserMessageGroupRepository.FindByQuery(a => (a.SenderAppUserId == SenderAppUserId || a.ReceiverAppUserId == ReceiverAppUserId || a.SenderAppUserId == ReceiverAppUserId || a.ReceiverAppUserId == SenderAppUserId) && a.IsDeleted == false).Any();
            if (!UserMessageGroupExisting)
                unitOfWork.UserMessageGroupRepository.Add(new UserMessageGroup { SenderAppUserId = SenderAppUserId, ReceiverAppUserId = ReceiverAppUserId, Name = Guid.NewGuid().ToString() });
            var UserMessageGroup = unitOfWork.UserMessageGroupRepository.FindByQuery(a => (a.SenderAppUserId == SenderAppUserId || a.ReceiverAppUserId == ReceiverAppUserId || a.SenderAppUserId == ReceiverAppUserId || a.ReceiverAppUserId == SenderAppUserId) && a.IsDeleted == false).FirstOrDefault();
            return UserMessageGroup.Name;
        }

        private async Task<List<UserViewOrLikeOrFavoriteOrBlockOrReportDTO>> MethodGetUserViewLikeFavoriteBlockReportByUserId(string UserId, int ProcessType)
        {
            switch (ProcessType)
            {
                case 1:
                    var UserViews = unitOfWork.UserViewRepository.FindByQuery(a => a.ReceiverAppUserId == UserId && a.IsDeleted == false).OrderByDescending(a => a.CreationDate).ToList();
                    foreach (var item in UserViews)
                    {
                        item.IsSeen = true;
                        await unitOfWork.UserViewRepository.Update(item);
                    }
                    return mapper.Map<List<UserViewOrLikeOrFavoriteOrBlockOrReportDTO>>(UserViews);

                case 2:
                    var UserLikes = unitOfWork.UserLikeRepository.FindByQuery(a => a.ReceiverAppUserId == UserId && a.IsDeleted == false).OrderByDescending(a => a.CreationDate).ToList();
                    foreach (var item in UserLikes)
                    {
                        if (item.ReceiverAppUserId == UserId)
                        {
                            item.IsSeen = true;
                            await unitOfWork.UserLikeRepository.Update(item);
                        }
                    }
                    return mapper.Map<List<UserViewOrLikeOrFavoriteOrBlockOrReportDTO>>(UserLikes);

                case 3:
                    var UserFavorites = unitOfWork.UserFavoriteRepository.FindByQuery(a => a.ReceiverAppUserId == UserId && a.IsDeleted == false).OrderByDescending(a => a.CreationDate).ToList();
                    foreach (var item in UserFavorites)
                    {
                        if (item.ReceiverAppUserId == UserId)
                        {
                            item.IsSeen = true;
                            await unitOfWork.UserFavoriteRepository.Update(item);
                        }
                    }
                    return mapper.Map<List<UserViewOrLikeOrFavoriteOrBlockOrReportDTO>>(UserFavorites);

                case 4:
                    var UserBlocks = unitOfWork.UserBlockRepository.FindByQuery(a => a.ReceiverAppUserId == UserId && a.IsDeleted == false).OrderByDescending(a => a.CreationDate).ToList();
                    foreach (var item in UserBlocks)
                    {
                        if (item.ReceiverAppUserId == UserId)
                        {
                            item.IsSeen = true;
                            await unitOfWork.UserBlockRepository.Update(item);
                        }
                    }
                    return mapper.Map<List<UserViewOrLikeOrFavoriteOrBlockOrReportDTO>>(UserBlocks);

                default:
                    break;
            }
            return new List<UserViewOrLikeOrFavoriteOrBlockOrReportDTO>();
        }

        private async Task<bool> MethodGetReturnIsThereBlockToThisUser(string UserId1, string UserId2)
        {
            if (await MethodGetIsUserValidByUserId(UserId1) && await MethodGetIsUserValidByUserId(UserId2))
            {
                return unitOfWork.UserBlockRepository.FindByQuery(a => ( (a.SenderAppUserId == UserId1 && a.ReceiverAppUserId == UserId2) || (a.SenderAppUserId == UserId2 && a.ReceiverAppUserId == UserId1)) && a.IsDeleted == false).Any();
                
            }
            else
            {
                return true;
            }
        }

        private async Task<bool> MethodGetIsUserValidByUserId(string UserId)
        {
            var user = await unitOfWork.UserManager.FindByIdAsync(UserId);
            bool IsUser = user != null ? await unitOfWork.UserManager.IsInRoleAsync(user, "User") : false;
            bool IsValidUser = user != null && user.IsStopped == false && user.IsDeleted == false && user.Activated == true ? true : false;
            bool IsNotSwitchedOff = user != null && unitOfWork.UserHistoryRepository.FindByQuery(a => a.IsMain == true && a.AppUserId == UserId && a.IsDeleted == false).FirstOrDefault().IsSwitchedOff == false ? false : true;
            if (IsValidUser && IsUser && !IsNotSwitchedOff)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<bool> MethodGetIsUserValidToDeleteSwitchedOffByUserId(string UserId)
        {
            var user = await unitOfWork.UserManager.FindByIdAsync(UserId);
            bool IsUser = user != null ? await unitOfWork.UserManager.IsInRoleAsync(user, "User") : false;
            bool IsValidUser = user != null && user.Activated == true ? true : false;
            if (IsValidUser && IsUser)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion


        #region Send Methods 
        private async Task<bool> MethodSendViewLikeFavoriteBlockReportFromUserToAnother(string SenderID, string RecieverID, int ProcessType)
        {
            bool Result = false;
            switch (ProcessType)
            {
                case 1:
                    var UserView = unitOfWork.UserViewRepository.FindByQuery(a => a.SenderAppUserId == SenderID && a.ReceiverAppUserId == RecieverID && a.IsDeleted == false).Any();
                    if (!UserView && SenderID != RecieverID)
                    {
                        var View = new UserView
                        {
                            SenderAppUserId = SenderID,
                            ReceiverAppUserId = RecieverID,
                            CreatedUserID = SenderID
                        };
                        await unitOfWork.UserViewRepository.Add(View);
                        Result = true;
                    }
                    break;

                case 2:
                    var UserLike = unitOfWork.UserLikeRepository.FindByQuery(a => a.SenderAppUserId == SenderID && a.ReceiverAppUserId == RecieverID && a.IsDeleted == false).Any();
                    if (!UserLike && SenderID != RecieverID)
                    {
                        var Like = new UserLike
                        {
                            SenderAppUserId = SenderID,
                            ReceiverAppUserId = RecieverID,
                            CreatedUserID = SenderID
                        };
                        await unitOfWork.UserLikeRepository.Add(Like);
                        Result = true;
                    }
                    break;

                case 3:
                    var UserFavorite = unitOfWork.UserFavoriteRepository.FindByQuery(a => a.SenderAppUserId == SenderID && a.ReceiverAppUserId == RecieverID && a.IsDeleted == false).Any();
                    if (!UserFavorite && SenderID != RecieverID)
                    {
                        var Favorite = new UserFavorite
                        {
                            SenderAppUserId = SenderID,
                            ReceiverAppUserId = RecieverID,
                            CreatedUserID = SenderID
                        };
                        await unitOfWork.UserFavoriteRepository.Add(Favorite);
                        Result = true;
                    }
                    else
                    {
                      var UserFavoriteDeleted =  unitOfWork.UserFavoriteRepository.FindByQuery(a => a.SenderAppUserId == SenderID && a.ReceiverAppUserId == RecieverID && a.IsDeleted == false).FirstOrDefault();
                        if (UserFavoriteDeleted !=null)
                        {
                            UserFavoriteDeleted.IsDeleted = true;
                            UserFavoriteDeleted.LastUpdateDate = DateTime.Now;
                            await unitOfWork.UserFavoriteRepository.Update(UserFavoriteDeleted);
                            Result = true;
                        }
                    }
                    break;

                case 4:
                    var UserBlock = unitOfWork.UserBlockRepository.FindByQuery(a => a.SenderAppUserId == SenderID && a.ReceiverAppUserId == RecieverID && a.IsDeleted == false).Any();
                    if (!UserBlock && SenderID != RecieverID)
                    {
                        var Block = new UserBlock
                        {
                            SenderAppUserId = SenderID,
                            ReceiverAppUserId = RecieverID,
                            CreatedUserID = SenderID
                        };
                        await unitOfWork.UserBlockRepository.Add(Block);
                        Result = true;
                    }
                    else
                    {
                        var UserBlockDeleted = unitOfWork.UserBlockRepository.FindByQuery(a => a.SenderAppUserId == SenderID && a.ReceiverAppUserId == RecieverID && a.IsDeleted == false).FirstOrDefault();
                        if (UserBlockDeleted != null)
                        {
                            UserBlockDeleted.IsDeleted = true;
                            UserBlockDeleted.LastUpdateDate = DateTime.Now;
                            await unitOfWork.UserBlockRepository.Update(UserBlockDeleted);
                            Result = true;
                        }
                    }
                    break;

                case 5:
                    var UserReport = unitOfWork.UserReportRepository.FindByQuery(a => a.SenderAppUserId == SenderID && a.ReceiverAppUserId == RecieverID && a.IsDeleted == false).Any();
                    if (!UserReport && SenderID != RecieverID)
                    {
                        var Report = new UserReport
                        {
                            SenderAppUserId = SenderID,
                            ReceiverAppUserId = RecieverID,
                            CreatedUserID = SenderID
                        };
                        await unitOfWork.UserReportRepository.Add(Report);
                        //Result = true;
                        var email = new SendingEmail()
                        {
                            Title = "User Report Another - AYM DATING APP",
                            Body = "\n" + "Sender User: " + unitOfWork.UserManager.FindByIdAsync(SenderID).Result.Email + "\n" + "Bad User: " + unitOfWork.UserManager.FindByIdAsync(RecieverID).Result.Email + "\n" + "DateOfMaking: " + Report.CreationDate.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                            To = "Ayman_elbatata@outlook.com"
                        };
                        SendingEmailSettings.SendEmail(email);
                    }
                    Result = true;
                    break;

                default:
                    break;
            }
            return Result;
        }

        private async Task<bool> MethodSendSeenUserMessageByMessageId(string RecieverID, int MessageId)
        {
            var UserMessage = unitOfWork.UserMessageRepository.FindByQuery(a => a.ID == MessageId && a.ReceiverAppUserId == RecieverID && a.IsDeleted == false).FirstOrDefault();
            if (UserMessage != null)
            {
                UserMessage.IsSeen = true;
                await unitOfWork.UserMessageRepository.Update(UserMessage);
                return true;
            }
            return false;
        }

        private async Task<bool> MethodSendSwitchedOffUserAccountByUserId(string UserId)
        {
            var UserHistory = unitOfWork.UserHistoryRepository.FindByQuery(a => a.AppUserId == UserId && a.IsMain == true && a.IsSwitchedOff  == false && a.IsDeleted == false).FirstOrDefault();
            if (UserHistory != null)
            {
                //UserHistory.IsDeleted = true;
                UserHistory.IsMain = true;
                UserHistory.IsSwitchedOff = true;
                UserHistory.LastUpdateUserID = UserId;
                UserHistory.LastUpdateDate = DateTime.Now;
                await unitOfWork.UserHistoryRepository.Update(UserHistory);

                var email = new SendingEmail()
                {
                    Title = "User SwitchedOff his/her Profile - AYM DATING APP",
                    Body = "\n" + "SwitchedOff User: " + UserHistory.AppUser.Email + "\n" + "DateOfMaking: " + UserHistory.LastUpdateDate.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                    To = "Ayman_elbatata@outlook.com"
                };
                SendingEmailSettings.SendEmail(email);

                return true;
            }
            return false;
        }

        private async Task<bool> MethodSendDeleteAccountByUserId(string UserId)
        {
            var UserHistory = unitOfWork.UserHistoryRepository.FindByQuery(a => a.AppUserId == UserId && a.IsMain == true && a.IsSwitchedOff == false && a.IsDeleted == false).FirstOrDefault();
            if (UserHistory != null)
            {
                //UserHistory.IsDeleted = true;
                UserHistory.IsMain = true;
                UserHistory.LastUpdateUserID = UserId;
                UserHistory.LastUpdateDate = DateTime.Now;
                await unitOfWork.UserHistoryRepository.Update(UserHistory);

                var user = await unitOfWork.UserManager.FindByIdAsync(UserId);
                user.IsStopped = true;
                user.IsDeleted = true;
                await unitOfWork.UserManager.UpdateAsync(user);

                var email = new SendingEmail()
                {
                    Title = "User Deleted his/her Profile - AYM DATING APP",
                    Body = "\n" + "Deleted User: " + UserHistory.AppUser.Email + "\n" + "DateOfMaking: " + UserHistory.LastUpdateDate.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                    To = "Ayman_elbatata@outlook.com"
                };
                SendingEmailSettings.SendEmail(email);

                return true;
            }
            return false;
        }

        #endregion

        #region Delete Methods 
        private async Task<bool> MethodDeleteSenderOrReceiverRMessageByMessageId(string UserId, int MessageId, int ProcessType)
        {
            var UserMessage = unitOfWork.UserMessageRepository.FindByQuery(a => a.ID == MessageId && a.IsDeleted == false).FirstOrDefault();
            if (UserMessage != null)
            {
                switch (ProcessType)
                {
                    case 1:
                        if (UserMessage.SenderAppUserId == UserId)
                        {
                            UserMessage.IsDeletedFromSender = true;
                        }
                        break;

                    case 2:
                        if (UserMessage.ReceiverAppUserId == UserId)
                        {
                            UserMessage.IsDeletedFromReceiver = true;
                        }
                        break;

                    default:
                        break;
                }
                if (UserMessage.IsDeletedFromReceiver == true && UserMessage.IsDeletedFromSender == true)
                {
                    UserMessage.IsDeleted = true;
                }
                await unitOfWork.UserMessageRepository.Update(UserMessage);
                return true;
            }
            return false;
        }



        private async Task<bool> MethodDeleteLikeOrFavoriteOrBlockFromUserToAnother(string SenderID, string RecieverID, int ProcessType)
        {
            switch (ProcessType)
            {
                case 1:
                    var Like = unitOfWork.UserLikeRepository.FindByQuery(a => a.SenderAppUserId == SenderID && a.ReceiverAppUserId == RecieverID && a.IsDeleted == false).FirstOrDefault();
                    if (Like != null)
                    {
                        Like.IsDeleted = true;
                        Like.LastUpdateUserID = SenderID;
                        Like.LastUpdateDate = DateTime.Now;
                        await unitOfWork.UserLikeRepository.Update(Like);
                        return true;
                    }
                    break;

               case 2:
                    var Favorite = unitOfWork.UserFavoriteRepository.FindByQuery(a => a.SenderAppUserId == SenderID && a.ReceiverAppUserId == RecieverID && a.IsDeleted == false).FirstOrDefault();
                    if (Favorite != null)
                    {
                        Favorite.IsDeleted = true;
                        Favorite.LastUpdateUserID = SenderID;
                        Favorite.LastUpdateDate = DateTime.Now;
                        await unitOfWork.UserFavoriteRepository.Update(Favorite);
                        return true;
                    }
                    break;

                    case 3:
                    var Block = unitOfWork.UserBlockRepository.FindByQuery(a => a.SenderAppUserId == SenderID && a.ReceiverAppUserId == RecieverID && a.IsDeleted == false).FirstOrDefault();
                    if (Block != null)
                    {
                        Block.IsDeleted = true;
                        Block.LastUpdateUserID = SenderID;
                        Block.LastUpdateDate = DateTime.Now;
                        await unitOfWork.UserBlockRepository.Update(Block);
                        return true;
                    }
                    break;

                default:
                    break;
            }

            return false;
        }

        #endregion

        #region Private Methods

        private string GetOneMessageForTwoUsersAccordingToPackages(string Message, string SenderAppUserId, string ReceiverAppUserId)
        {
            var Sender = unitOfWork.UserPackageRepository.FindByQuery(a => a.AppUserId == SenderAppUserId && a.IsDeleted == false).LastOrDefault().PackageId;
            var Reciever = unitOfWork.UserPackageRepository.FindByQuery(a => a.AppUserId == ReceiverAppUserId && a.IsDeleted == false).LastOrDefault().PackageId;
            if (Sender == 1 && Reciever == 1)
            {
                return Message.Length > 5 ? Message.Substring(0, 5) + "....Free Membership" : Message;
            }
            return Message;
        }

        //private UserHistoryDTO GetUSerHistoryMapping(string UserId)
        //{
        //    var CurrentUser = unitOfWork.UserHistoryRepository.FindByQuery(a => a.AppUserId == UserId && a.IsMain == true && a.IsDeleted == false).FirstOrDefault();
        //    return new UserHistoryDTO {
        //        AppUserId = UserId,
        //        JobId = CurrentUser.JobId,
        //        IsMain = CurrentUser.IsMain,
        //        AboutPartner = CurrentUser.AboutPartner,
        //        AboutYou = CurrentUser.AboutYou,
        //        City = CurrentUser.City,
        //        CountryId = CurrentUser.CountryId,
        //        FinancialModeId = CurrentUser.FinancialModeId,
        //        EducationId = CurrentUser.EducationId,
        //        GenderId = CurrentUser.GenderId,
        //        IsSwitchedOff = CurrentUser.IsSwitchedOff,
        //        UserPackageId = CurrentUser.UserPackageId,
        //        MaritalStatusId = CurrentUser.MaritalStatusId,
        //        SearchAgeFrom = CurrentUser.SearchAgeFrom,
        //        PurposeId = CurrentUser.PurposeId,
        //        LanguageId = CurrentUser.LanguageId,
        //        SearchAgeTo = CurrentUser.SearchAgeTo,
        //        ProfileHeading = CurrentUser.ProfileHeading,
        //        AppUser = CurrentUser.AppUser,
        //        UserPackage = CurrentUser.UserPackage,
        //        Job = CurrentUser.Job,
        //        Purpose = CurrentUser.Purpose,
        //        Country = CurrentUser.Country,
        //        Education = CurrentUser.Education,
        //        Gender = CurrentUser.Gender,
        //        FinancialMode = CurrentUser.FinancialMode,
        //        Language = CurrentUser.Language,
        //        MaritalStatus = CurrentUser.MaritalStatus,
        //        Age = GetCurrentAgeforUser(unitOfWork.UserManager.FindByIdAsync(UserId).Result.DateOfBirth),
               
        //    };        
        //}

        private int GetCurrentAgeforUser(DateTime birthday)
           => DateTime.Now.Year - birthday.Year;
        
        private async Task<List<AppUser>> GetAllValidUsers()
        {
            var AllValidUsers = new List<AppUser>();
            var AllUsers = unitOfWork.UserManager.Users.ToList().OrderByDescending(a => a.DateOfJoin);
            foreach (var item in AllUsers)
            {
                if (await MethodGetIsUserValidByUserId(item.Id))
                {
                    AllValidUsers.Add(item);
                }
            }
            return AllValidUsers;
        }
        //private void GetInfoFromUser()
        //{
        //    string token = GetToken();
        //    if (!string.IsNullOrEmpty(token))
        //    {
        //        var handler = new JwtSecurityTokenHandler();
        //        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

        //        foreach (var item in jsonToken.Claims)
        //        {
        //            UserTokenData2.Add(item.Value);
        //        }
        //        UserTokenData2.Add(token);
        //    }
        //}

        //private string GetToken()
        //=> !string.IsNullOrEmpty(unitOfWork.AppSession.AuthorizationToken) && unitOfWork.AppSession.AuthorizationToken != "Bearer" ? unitOfWork.AppSession.AuthorizationToken.Split(" ")[1] : string.Empty;

        //private bool TokenTblIsExpired(string token)
        //{
        //    var TokenExisting = unitOfWork.UserTokenTransactionRepository.FindByQuery(a => a.Token == token).FirstOrDefault();
        //    if (TokenExisting != null)
        //    {
        //        return DateTime.Now > TokenExisting.DateOfExpiration;
        //    }
        //    return true;
        //}

        //private bool RefuseAnyRequestWithInvalidToken()
        //    => string.IsNullOrEmpty(unitOfWork.AppSession.AuthorizationToken) || unitOfWork.AppSession.AuthorizationToken == "Bearer" || TokenTblIsExpired(GetToken());

        //private string GetUserIdFromToken()
        //{
        //    string token = GetToken();
        //    if (!string.IsNullOrEmpty(token))
        //    {
        //        var handler = new JwtSecurityTokenHandler();
        //        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
        //        string UserId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
        //        if (!string.IsNullOrEmpty(UserId))
        //            return UserId;
        //    }
        //    return string.Empty;
        //}
        #endregion

    }
}
