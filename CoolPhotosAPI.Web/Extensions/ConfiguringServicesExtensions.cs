using AutoMapper;
using CoolPhotosAPI.BL;
using CoolPhotosAPI.Data.Abstract;
using CoolPhotosAPI.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CoolPhotosAPI.Web.Extensions
{
    public static class ConfiguringServicesExtensions
    {
        public static void AddMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.AddProfile<CoolEntitiesMappingProfile>();
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public static void AddServiceResolver(this IServiceCollection services)
        {
            services.AddTransient<ServiceResolver<IInjectableService>>(serviceProvider => key =>
            {
                switch (key)
                {
                    case CoolPhotosConstants.EF_UNIT_OF_WORK_DI_KEY:
                        return serviceProvider.GetService<EFUnitOfWork>();
                    case CoolPhotosConstants.EXTENDED_UNIT_OF_WORK_DI_KEY:
                        return serviceProvider.GetService<ExtendedUnitOfWork>();
                    default:
                        throw new ArgumentException();
                }
            });
        }
    }
}
