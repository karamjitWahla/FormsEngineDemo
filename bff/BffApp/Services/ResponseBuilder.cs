using BffApp.Models;

namespace BffApp.Services
{
    public static class ResponseBuilder
    {
        public static ResponseModel Build(string? action, UserInfo user)
        {
            return action switch
            {
                "getData" => new ResponseModel
                {
                    Message = "Mock data returned successfully",
                    Action = action,
                    User = user
                },
                _ => new ResponseModel
                {
                    Message = "Unknown action",
                    Action = action,
                    User = user
                }
            };
        }
    }
}
