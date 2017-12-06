using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;
using WishListRestService.Services;

[assembly: OwinStartup(typeof(WishListRestService.Startup))]

namespace WishListRestService {
    public partial class Startup {
        public void ConfigureServices(IServiceCollection services) {
            services.AddSingleton<IWishListRepository, WishListRepository>();
        }

        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}