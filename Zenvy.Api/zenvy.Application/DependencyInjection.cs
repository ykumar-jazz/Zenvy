using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using zenvy.application.Interfaces.Services;
using zenvy.application.Service;

namespace zenvy.application
{
    public static class DependencyInjection
    {
        public static IServiceCollection
            AddApplication(
            this IServiceCollection services)
        {
            services.AddScoped<IUserService,UserService>();

            //services.AddScoped<IAuthService,AuthService>();

            return services;
        }
    }
}
