namespace BP.Ecommerce.Application.Exceptions
{
    /// <summary>
    /// Exception para lanzar error por el incumplimiento de reglas negocios
    /// </summary>
    [Serializable]
    public class BusinessException : Exception
    {
        public string FriendlyMessage { get; }
        public BusinessException(string friendlyMessage)
        {
            FriendlyMessage = friendlyMessage;
        }

        public BusinessException(string friendlyMessage, string mensajeTecnico) : base(mensajeTecnico)
        {
            FriendlyMessage = friendlyMessage;
        }

        public BusinessException(string friendlyMessage, string mensajeTecnico, Exception inner) : base(mensajeTecnico, inner)
        {
            FriendlyMessage = friendlyMessage;
        }

        protected BusinessException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }


    }
}