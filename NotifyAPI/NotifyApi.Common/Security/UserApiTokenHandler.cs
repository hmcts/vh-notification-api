using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using NotifyApi.Common.Configuration;

namespace NotifyApi.Common.Security
{
    public class UserApiTokenHandler : BaseServiceTokenHandler
    {
        public UserApiTokenHandler(IOptions<AzureAdConfiguration> azureAdConfiguration,
            IOptions<ServicesConfiguration> hearingServicesConfiguration, IMemoryCache memoryCache,
            ITokenProvider tokenProvider) : base(azureAdConfiguration, hearingServicesConfiguration, memoryCache,
            tokenProvider)
        {
        }
        
        protected override string TokenCacheKey => "UserApiServiceToken";
        protected override string ClientResource => ServicesConfiguration.UserApiResourceId;
    }
}