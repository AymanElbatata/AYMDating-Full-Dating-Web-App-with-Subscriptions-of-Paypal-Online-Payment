using AYMDating.Blazor.Data.DTO;
using static AYMDating.Blazor.Pages.Login;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Blazored.LocalStorage;
using AYMDating.API.DTO;
using System.IdentityModel.Tokens.Jwt;
using AYMDating.DAL.Repositories;
using AYMDating.DAL.Interfaces;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using AYMDating.DAL.Entities;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PayPal.Api;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Components;
using AYMDating.Blazor.Pages;
using Microsoft.AspNetCore.Components.Authorization;
using AYMDating.API.Helpers;

namespace AYMDating.Blazor.Data.Helpers
{
    public class ApiServiceLogin
    {

        private readonly HttpClient _httpClient;


        private readonly ILocalStorageService _localStorageService;

        private readonly List<string> UserTokenData;

        //[Inject]
        //private Login login { get; set; }
        //private readonly UserDTO userDTO;

        public ApiServiceLogin(ILocalStorageService localStorageService)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(UserImagesUrlSpecs.UrlApiToRead);
            _localStorageService = localStorageService;
            UserTokenData = new List<string>();
        }
        
        public class PaymentResponse
        {
            public string ApprovalUrl { get; set; }
        }

        public async Task<UserDTO> AddTokenForUserFromLocalStorage()
        {
            await GetInfoFromUser();
            if (UserTokenData != null && UserTokenData.Count() > 0)
            {
                return new UserDTO()
                {
                    UserId = UserTokenData[3],
                    Email = UserTokenData[0],
                    UserName = UserTokenData[1],
                    UserRole = UserTokenData[2]
                };
            }
            return new UserDTO();
        }
        private async Task AddTokenForRequestAPI()
        {
            var token = await _localStorageService.GetItemAsync<string>("AYMauthToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

        }


        
        public async Task<bool> SendMessageContactUsFromAnonymous(object loginModel)
        {
            var response = await _httpClient.PostAsJsonAsync("Account/SendContactUsFromAnonymous", loginModel);
            //response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_boolDTO>();
                return responseContent.Data;
            }
            return false;
        }

        public async Task<ReturnValue_UserDTO> LoginProcessAsync(object loginModel)
        {
            var response = await _httpClient.PostAsJsonAsync("Account/Login", loginModel);
            //response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_UserDTO>();
                return responseContent;
            }
            return new ReturnValue_UserDTO();
        }

        public async Task<ActivateUserDTO> RegisterProcessAsync(object loginModel)
        {
            var response = await _httpClient.PostAsJsonAsync("Account/Register", loginModel);
            //response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_ActivateUserDTO>();
                return responseContent.Data;
            }
            return new ActivateUserDTO();
        }

        public async Task<bool> VerifyActivateUser(object activationModel)
        {
            var response = await _httpClient.PostAsJsonAsync("Account/VerifyActivateUser", activationModel);
            //response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_boolDTO>();
                return responseContent.Data;
            }
            return false;
        }

        public async Task<bool> DoDeleteSwitchedOffUserByUserId(string UserEmail)
        {
            var response = await _httpClient.GetAsync("Account/DeleteSwitchedOffUserByUserId/" + UserEmail);
            //response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_boolDTO>();
                return responseContent.Data;
            }
            return false;
        }

        public async Task<ActivateUserDTO> ActivateAgainForUser(string UserEmail)
        {
            var response = await _httpClient.GetAsync("Account/ActivateAgainForUser/"+ UserEmail);
            //response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_ActivateUserDTO>();
                return responseContent.Data;
            }
            return new ActivateUserDTO();
        }

        public async Task<ForgotPasswordDTO> ForgetPasswordForUser(string UserEmail)
        {
            var response = await _httpClient.GetAsync("Account/ForgetPassword/" + UserEmail);
            //response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_ForgotPasswordDTO>();
                return responseContent.Data;
            }
            return new ForgotPasswordDTO();
        }

        public async Task<bool> ResetPasswordForUser(object activationModel)
        {
            var response = await _httpClient.PostAsJsonAsync("Account/ResetPassword", activationModel);
            //response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_boolDTO>();
                return responseContent.Data;
            }
            return false;
        }


        [Authorize]
        public async Task<bool> LogOutProcessAsync()
        {
            //await AddTokenForUserFromLocalStorage();
            await AddTokenForRequestAPI();
            try
            {
                var response = await _httpClient.PostAsync("Account/Logout", null);
                //response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }


        [Authorize]
        public async Task<PaymentResponse> CreateMemberShipForUser(int selectedAmountInDollars)
        {
            try
            {
                await AddTokenForRequestAPI();

                var response = await _httpClient.PostAsJsonAsync("Payments/CreatePayment", selectedAmountInDollars);

                //response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<PaymentResponse>();
                    return responseContent;
                }
                return new PaymentResponse();

            }
            catch (Exception)
            {
                return new PaymentResponse();
            }
        }

        [Authorize]
        public async Task<bool> ExecuteMemberShipForUser(UserPaymentDTO model)
        {
            try
            {
                await AddTokenForUserFromLocalStorage();

                await AddTokenForRequestAPI();


                model.AppUserId = UserTokenData[3];
                var response = await _httpClient.PostAsJsonAsync("Payments/ExecutePaymentAndSaving", model);
                //var response = await _httpClient.GetAsync($"Payments/ExecutePaymentAndSaving?PaymentId={model.PaymentId}&PayerId={model.PayerId}&token={model.token}");

                //response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_boolDTO>();
                    return responseContent.Data;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        [Authorize]
        public async Task<bool> SendSwitchedOffFromCurrentUser(SwitchOrDeleteDTO model)
        {
            await AddTokenForRequestAPI();
            try
            {
                var response = await _httpClient.PostAsJsonAsync("UserProfile/SendSwitchedOffFromCurrentUser", model);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_boolDTO>();

                    return responseContent.Data;
                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [Authorize]
        public async Task<bool> SendDeleteAccountFromCurrentUser(SwitchOrDeleteDTO model)
        {
            await AddTokenForRequestAPI();
            try
            {
                var response = await _httpClient.PostAsJsonAsync("UserProfile/SendDeleteAccountFromCurrentUser", model);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_boolDTO>();

                    return responseContent.Data;
                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }


        [Authorize]
        public async Task<List<UserHistoryDTO>> GetUserHistoriesWithMainImagesForAll()
        {
            await AddTokenForRequestAPI();
            try
            {
                var response = await _httpClient.GetAsync("UserProfile/GetUserHistoriesWithMainImagesForAll");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_UserHistoriesDTO>();

                    return responseContent.Data;
                }
                return new List<UserHistoryDTO>();

            }
            catch (Exception ex)
            {
                 return new List<UserHistoryDTO>();
            }
        }


        [Authorize]
        public async Task<List<string>> GetUserMatchesIdsUsersForCurrentUser()
        {
            await AddTokenForRequestAPI();
            try
            {
                var response = await _httpClient.GetAsync("UserProfile/GetUserMatchesIdsUsersForCurrentUser");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_ListStringsDTO>();

                    return responseContent.Data;
                }
                return new List<string>();

            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }

        [Authorize]
        public async Task<List<string>> GetUserOnlineIdsUsersForCurrentUser()
        {
            await AddTokenForRequestAPI();
            try
            {
                var response = await _httpClient.GetAsync("UserProfile/GetUserOnlineIdsUsersForCurrentUser");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_ListStringsDTO>();

                    return responseContent.Data;
                }
                return new List<string>();

            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }

        [Authorize]
        public async Task<List<string>> GetUserSearchIdsUsersForCurrentUser(UserMainSearchDTO model)
        {
            await AddTokenForRequestAPI();
            try
            {
                var response = await _httpClient.PostAsJsonAsync("UserProfile/GetUserSearchIdsUsersForCurrentUser", model);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_ListStringsDTO>();

                    return responseContent.Data;
                }
                return new List<string>();

            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }

        [Authorize]
        public async Task<List<Country>> GetAllCountries()
        {
            await AddTokenForRequestAPI();
            try
            {
                var response = await _httpClient.GetAsync("UserProfile/GetAllCountries");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_CountriesDTO>();

                    return responseContent.Data;
                }
                return new List<Country>();

            }
            catch (Exception ex)
            {
                return new List<Country>();
            }
        }

        [Authorize]
        public async Task<List<Gender>> GetAllGenders()
        {
            await AddTokenForRequestAPI();
            try
            {
                var response = await _httpClient.GetAsync("UserProfile/GetAllGenders");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_GendersDTO>();

                    return responseContent.Data;
                }
                return new List<Gender>();

            }
            catch (Exception ex)
            {
                return new List<Gender>();
            }
        }

        [Authorize]
        public async Task<LookUpTablesDTO> GetAllLookUpTablesForUserProfile()
        {
            await AddTokenForRequestAPI();
            try
            {
                var response = await _httpClient.GetAsync("UserProfile/GetAllLookUpTablesForUserProfile");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_LookUpsTablesDTO>();

                    return responseContent.Data;
                }
                return new LookUpTablesDTO();

            }
            catch (Exception ex)
            {
                return new LookUpTablesDTO();
            }
        }

        [Authorize]
        public async Task<List<UserViewOrLikeOrFavoriteOrBlockOrReportDTO>> GetCurrentUserLikes()
        {
            await AddTokenForRequestAPI();
            try
            {
                var response = await _httpClient.GetAsync("UserProfile/GetCurrentUserLikes");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_UserViewOrLikeOrFavoriteOrBlockOrReportListDTO>();

                    return responseContent.Data;
                }
            }
            catch (Exception)
            {
            }

            return new List<UserViewOrLikeOrFavoriteOrBlockOrReportDTO>();
        }

        [Authorize]
        public async Task<List<UserViewOrLikeOrFavoriteOrBlockOrReportDTO>> GetCurrentUserBlocks()
        {
            await AddTokenForRequestAPI();
            try
            {
                var response = await _httpClient.GetAsync("UserProfile/GetCurrentUserBlocks");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_UserViewOrLikeOrFavoriteOrBlockOrReportListDTO>();

                    return responseContent.Data;
                }
            }
            catch (Exception)
            {
            }

            return new List<UserViewOrLikeOrFavoriteOrBlockOrReportDTO>();
        }

        [Authorize]
        public async Task<List<UserViewOrLikeOrFavoriteOrBlockOrReportDTO>> GetCurrentUserFavorites()
        {
            await AddTokenForRequestAPI();
            try
            {
                var response = await _httpClient.GetAsync("UserProfile/GetCurrentUserFavorites");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_UserViewOrLikeOrFavoriteOrBlockOrReportListDTO>();

                    return responseContent.Data;
                }
            }
            catch (Exception)
            {
            }

            return new List<UserViewOrLikeOrFavoriteOrBlockOrReportDTO>();
        }

        [Authorize]
        public async Task<List<UserViewOrLikeOrFavoriteOrBlockOrReportDTO>> GetCurrentUserViews()
        {
            await AddTokenForRequestAPI();
            try
            {
                var response = await _httpClient.GetAsync("UserProfile/GetCurrentUserViews");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_UserViewOrLikeOrFavoriteOrBlockOrReportListDTO>();

                    return responseContent.Data;
                }
            }
            catch (Exception)
            {
            }

            return new List<UserViewOrLikeOrFavoriteOrBlockOrReportDTO>();
        }

        [Authorize]
        public async Task<UserDTO> GetUserEmailAndUserNameByUserId(string UserId)
        {
            await AddTokenForRequestAPI();
            try
            {
                var response = await _httpClient.GetAsync("Account/GetUserEmailAndUserNameByUserId/" + UserId);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_UserDTO>();

                    return responseContent.Data;
                }
            }
            catch (Exception)
            {
            }

            return new UserDTO();
        }

        [Authorize]
        public async Task<Stream> GetUserImageFile(string fileName)
        {
            await AddTokenForRequestAPI();
            try
            {
                string stringToRemove = "https://localhost:7082/";
                string updatedString = fileName.Replace(stringToRemove, "");

                var response = await _httpClient.GetAsync("UserProfile/GetUserImageFile/" + updatedString);

                if (response.IsSuccessStatusCode)
                {
                    var imageStream = await response.Content.ReadAsStreamAsync();
                    return imageStream;
                }
            }
            catch (Exception)
            {
            }

            return null;
        }


        [Authorize]
        public async Task<UserHistoryDTO> GetUserHistoryWithFullImagesByUserId(string UserId)
        {
            await AddTokenForRequestAPI();
            try
            {
                var response = await _httpClient.GetAsync("UserProfile/GetUserHistoryWithFullImagesByUserId/" + UserId);

                //response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_UserHistoryDTO>();
                    return responseContent.Data;
                }
            }
            catch (Exception)
            {
            }

            return new UserHistoryDTO();
        }


        [Authorize]
        public async Task<UserHistoryDTO> GetHistoryWithMainImagesForCurrentUser()
        {
            await AddTokenForRequestAPI();
            try
            {
                var response = await _httpClient.GetAsync("UserProfile/GetHistoryWithMainImagesForCurrentUser");

                //response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_UserHistoryDTO>();
                    return responseContent.Data;
                }
            }
            catch (Exception)
            {
            }

            return new UserHistoryDTO();
        }

        //[Authorize]
        //public async Task<UserHistoryDTO> GetHistoryWithMainImageBytUserId(string UserId)
        //{
        //    await AddTokenForRequestAPI();
        //    try
        //    {
        //        var response = await _httpClient.GetAsync("UserProfile/GetHistoryWithMainImageBytUserId/" + UserId);

        //        //response.EnsureSuccessStatusCode();
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_UserHistoryDTO>();
        //            return responseContent.Data;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }

        //    return new UserHistoryDTO();
        //}

        [Authorize]
        public async Task<bool> SendUpdateHistoryWithMainImagesForCurrentUser(object model)
        {
            await AddTokenForRequestAPI();
            try
            {
                var response = await _httpClient.PostAsJsonAsync("UserProfile/SendUpdateHistoryWithMainImagesForCurrentUser", model);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_boolDTO>();

                    return responseContent.Data;
                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }


        [Authorize]
        public async Task<bool> SendUploadUserMainImageForCurrentUser(MultipartFormDataContent  content)
        {
            await AddTokenForRequestAPI();
            try
            {
                var response = await _httpClient.PostAsync("UserProfile/SendUploadUserMainImageForCurrentUser", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_boolDTO>();

                    return responseContent.Data;
                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [Authorize]
        public async Task<bool> SendUploadUserOtherImageForCurrentUser(MultipartFormDataContent content, int order)
        {
            await AddTokenForRequestAPI();
            try
            {
                var response = await _httpClient.PostAsync("UserProfile/SendUploadUserOtherImageForCurrentUser"+ order, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_boolDTO>();

                    return responseContent.Data;
                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [Authorize]
        public async Task<string> GetUserMessageGroupNameOrCreateItByUserId1ToUserId2(string ReceiverAppUserId)
        {
            await AddTokenForRequestAPI();
            try
            {
                var response = await _httpClient.PostAsJsonAsync("UserProfile/GetUserMessageGroupNameOrCreateItByUserId1ToUserId2", new UserViewOrLikeOrFavoriteOrBlockOrReportDTO() { ReceiverAppUserId = ReceiverAppUserId });

                //response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_UserMessageGroupDTO>();
                    return responseContent.Data;
                }
            }
            catch (Exception)
            {
            }

            return string.Empty;
        }


        [Authorize]
        public async Task<List<UserMessageDTO>> GetUserMessagesByUserIdandAnotherUser(object model)
        {
            await AddTokenForRequestAPI();
            try
            {
                var response = await _httpClient.PostAsJsonAsync("UserProfile/GetUserMessagesByUserIdandAnotherUser", model);

                //response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_UserMessagesDTO>();
                    return responseContent.Data;
                }
            }
            catch (Exception ex)
            {
            }

            return new List<UserMessageDTO>();
        }


        [Authorize]
        public async Task<List<UserMessageDTO>> GetLatestUserMessagesForCurrentUser()
        {
            await AddTokenForRequestAPI();
            try
            {
                var response = await _httpClient.PostAsync("UserProfile/GetLatestUserMessagesForCurrentUser", null);

                //response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_UserMessagesDTO>();
                    return responseContent.Data;
                }
            }
            catch (Exception ex)
            {
            }

            return new List<UserMessageDTO>();
        }

        [Authorize]
        public async Task<bool> GetReturnIsThereBlockToThisUser(string ReceiverAppUserId)
        {
            await AddTokenForRequestAPI();
            try
            {
                var response = await _httpClient.PostAsJsonAsync("UserProfile/GetReturnIsThereBlockToThisUser", new UserViewOrLikeOrFavoriteOrBlockOrReportDTO() { ReceiverAppUserId = ReceiverAppUserId });

                //response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_boolDTO>();
                    return responseContent.Data;
                }
            }
            catch (Exception ex)
            {
            }

            return true;
        }

        [Authorize]
        public async Task<UserMessageDTO> SendMessageFromCurrentUserToAnother(object model)
        {
            await AddTokenForRequestAPI();
            try
            {
                var response = await _httpClient.PostAsJsonAsync("UserProfile/SendMessageFromCurrentUserToAnother", model);

                //response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_UserMessageDTO>();
                    return responseContent.Data;
                }
            }
            catch (Exception)
            {
            }

            return new UserMessageDTO();
        }



        [Authorize]
        public async Task<bool> SendLikeFromCurrentUserToAnother(object model)
        {
            await AddTokenForRequestAPI();
            try
            {
                var response = await _httpClient.PostAsJsonAsync("UserProfile/SendLikeFromCurrentUserToAnother", model);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_boolDTO>();

                    return responseContent.Data;
                }
            }
            catch (Exception ex)
            {
            }

            return false;
        }

        [Authorize]
        public async Task<bool> SendReportFromCurrentUserToAnother(object model)
        {
            await AddTokenForRequestAPI();
            try
            {
                var response = await _httpClient.PostAsJsonAsync("UserProfile/SendReportFromCurrentUserToAnother", model);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_boolDTO>();

                    return responseContent.Data;
                }
            }
            catch (Exception ex)
            {
            }

            return false;
        }

        

        [Authorize]
        public async Task<bool> SendViewFromCurrentUserToAnother(object model)
        {
            await AddTokenForRequestAPI();
            try
            {
                var response = await _httpClient.PostAsJsonAsync("UserProfile/SendViewFromCurrentUserToAnother", model);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_boolDTO>();

                    return responseContent.Data;
                }
            }
            catch (Exception ex)
            {
            }

            return false;
        }

        [Authorize]
        public async Task<bool> SendFavoriteCurrentFromUserToAnother(object model)
        {
            await AddTokenForRequestAPI();
            try
            {
                var response = await _httpClient.PostAsJsonAsync("UserProfile/SendFavoriteCurrentFromUserToAnother", model);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_boolDTO>();

                    return responseContent.Data;
                }
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        [Authorize]
        public async Task<bool> SendBlockFromCurrentUserToAnother(object model)
        {
            await AddTokenForRequestAPI();
            try
            {
                var response = await _httpClient.PostAsJsonAsync("UserProfile/SendBlockFromCurrentUserToAnother", model);

                if (response.IsSuccessStatusCode)
                {
                    //var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_GenericDTO>();

                    return true;
                }
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        [Authorize]
        public async Task<bool> DeleteBlockFromCurrentUserToAnother(object model)
        {
            await AddTokenForRequestAPI();
            try
            {
                var response = await _httpClient.PostAsJsonAsync("UserProfile/DeleteBlockFromCurrentUserToAnother", model);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_boolDTO>();

                    return responseContent.Data;
                }
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        private async Task GetInfoFromUser()
        {
            var token = await _localStorageService.GetItemAsync<string>("AYMauthToken");
            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                foreach (var item in jsonToken.Claims)
                {
                    UserTokenData.Add(item.Value);
                }
                UserTokenData.Add(token);
            }
        }



    }
}
