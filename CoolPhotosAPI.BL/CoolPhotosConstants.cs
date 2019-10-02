using CoolPhotosAPI.Data.Abstract;
using CoolPhotosAPI.Data.Repositories;

namespace CoolPhotosAPI.BL
{
    public delegate TService ServiceResolver<TService>(string serviceKey) where TService : class, IInjectableService;

    public static class CoolPhotosConstants
    {
        public const string REDIRECT_URL_KEY = "redirectUrl";
        public const string COOL_AUTH_SCHEME = "MainCookie";
        public const string EF_UNIT_OF_WORK_DI_KEY = nameof(EFUnitOfWork);
        public const string EXTENDED_UNIT_OF_WORK_DI_KEY = nameof(ExtendedUnitOfWork);
    }
}
