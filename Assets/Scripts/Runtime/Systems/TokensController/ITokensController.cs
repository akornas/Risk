public interface ITokensController : IEnableable
{
	event System.Action OnRefreshTokensEvent;
	int Tokens { get; }
}
