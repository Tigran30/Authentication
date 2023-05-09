namespace Authentication.Base
{
    [Serializable]
    public class AuthenticationException : System.Exception
    {
        public ResponseCode Code { get; set; }

        public AuthenticationException()
        {
        }

        public AuthenticationException(ResponseCode code, string message = null)
            : base(message)
        {
            Code = code;
        }

        public AuthenticationException(string message)
            : base(message)
        {
        }
    }
}
