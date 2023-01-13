using Fridges.Client.Services.Contracts;

namespace Fridges.Client.Services
{
    public class UnathorizedHandler : DelegatingHandler
    {
        private readonly HttpClient _client;
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<UnathorizedHandler> _logger;

        public UnathorizedHandler(HttpClient client, IAccountService accountService, IHttpContextAccessor httpContextAccessor, ILogger<UnathorizedHandler> logger)
        {
            _client = client;
            _accountService = accountService;
            _contextAccessor = httpContextAccessor;
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                                        CancellationToken cancellationToken)
        { 
            var response = await base.SendAsync(request, cancellationToken);
         
            if(request.RequestUri.ToString() == "https://localhost/Account/refresh-token" && ((int)response.StatusCode) == StatusCodes.Status200OK)
            {

            }
            if (((int)response.StatusCode) == StatusCodes.Status401Unauthorized)
            {
                var accessToken = _contextAccessor.HttpContext.Request.Cookies["accessToken"];
                var refreshToken = _contextAccessor.HttpContext.Request.Cookies["refreshToken"];
                var result = await _accountService.RefreshTokenAsync(accessToken, refreshToken);
                _contextAccessor.HttpContext.Response.Cookies.Delete("accessToken");
                _contextAccessor.HttpContext.Response.Cookies.Append("accessToken", result.AccessToken, new CookieOptions { IsEssential = true });
                _contextAccessor.HttpContext.Response.Cookies.Delete("refreshToken");
                _contextAccessor.HttpContext.Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions { IsEssential = true });
                _contextAccessor.HttpContext.Response.Cookies.Delete("Role");
                _contextAccessor.HttpContext.Response.Cookies.Append("Role", result.Role, new CookieOptions { IsEssential = true });
                //change authorization header
                _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_contextAccessor.HttpContext.Request.Cookies["accessToken"]}");
                
                return await base.SendAsync(request, cancellationToken);

            }
            return response;

        }


    }
}
