namespace VisitorRegistration.BLL.Models
{
    public class VisitorRegistrationErrorDescriber
    {
        public virtual ApiError AlreadyCheckedOut()
        {
            return new ApiError
            {
                Code = nameof(AlreadyCheckedOut)
            };
        }

        public virtual ApiError DbError()
        {
            return new ApiError
            {
                Code = nameof(DbError)
            };
        }
    }
}
