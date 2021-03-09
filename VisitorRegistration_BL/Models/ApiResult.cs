namespace VisitorRegistration_BLL.Models
{
    public class ApiResult
    {
        public bool Succeeded { get; set; } = false;
        public ApiError Error { get; set; }


        public static ApiResult Success = new ApiResult
        {
            Succeeded = true
        };

        public static ApiResult Failed(ApiError error = null)
        {
            var result = new ApiResult
            {
                Succeeded = false
            };

            if (error != null) result.Error = error;

            return result;
        }
    }
}
