namespace TestNg.Controllers.Identity
{
   public interface IIdentityservice
    {
        public string GenerateToken(string userid, string userName, string secret);
    }
}
