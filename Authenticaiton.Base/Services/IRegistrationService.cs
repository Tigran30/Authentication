namespace Authentication.Base
{
    public interface IRegistrationService
    {
        Task<bool?> Register(RegisterationModel model);
    }
}
