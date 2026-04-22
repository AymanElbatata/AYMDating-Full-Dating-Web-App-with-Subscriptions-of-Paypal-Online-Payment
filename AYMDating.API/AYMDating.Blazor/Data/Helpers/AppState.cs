using AYMDating.API.DTO;

namespace AYMDating.Blazor.Data.Helpers
{
    public class AppState
    {
        public UserHistoryDTO CurrentUserHistoryDTO { get; set; }
        //public string? CurrentUserId { get; set; }
        //public string? CurrentUserName { get; set; }
        public string? IMessageToUserName { get; set; }
        public string? IMessageToUserId { get; set; }
        public bool isAuthenticated { get; set; } = false;
        public List<UserHistoryDTO> AllUsers { get; set; }
        //public  UserHistoryDTO? CurrentUser { get; set; }
        public int CounterOfNewMessages { get; set; }
        public int CounterOfNewLikes { get; set; }
        public int CounterOfNewViews { get; set; }
        public int CounterOfNewFavorites { get; set; }
        public int CounterOfNewBlocks { get; set; }

        public int TotalCounter { get; set; }
    }
}

