using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Areas.Admin;

namespace UI.Extension
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection IdentityServerAyarlari(this IServiceCollection services,IConfiguration Configuration)
        {
            services.AddDbContext<CustomIdentityDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("IdentityDb")));

            services.AddIdentity<CustomUser, IdentityRole>().AddEntityFrameworkStores<CustomIdentityDbContext>().AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options => {
                options.Password.RequireDigit = true;//şifrede rakam olsun
                options.Password.RequireLowercase = true;//şifrede küçük harf olsun
                options.Password.RequireUppercase = true;//şifrede büyük harf olsun
                options.Password.RequiredLength = 6;//şifre en az 6 karekter olsun
                options.Password.RequiredUniqueChars = 2;//en az 2 benzersiz karakter olsun
                options.Password.RequireNonAlphanumeric = true;//şifrede harf dışında $,@ gibi karakter olmalı
                options.Lockout.MaxFailedAccessAttempts = 5; //en fazla 5 kere yanlış giriş denenebilir
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);//5 defa yanlış girerse 10 dk boyunca yeni giriş yapamaz
                options.User.AllowedUserNameCharacters = "abcçdefgğhıijklmnoöpqrsştuüvwxyzABCÇDEFGĞHIİJKLMNOÖPQRSTUÜVWXYZ0123456789-._@+";//kullanıcı adı kısmına türkçe karakter desteği
                options.User.RequireUniqueEmail = true; //aynı mail adresiyle birden fazla üyelik yapılamaz
                options.SignIn.RequireConfirmedEmail = true;// üye olduktan sonra e mail adresini doğrulaması gerekiyor
            });

            return services;
        }
        
        public static IServiceCollection CookieAyarlari(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Admin/Home/Login";//default login ekrannı
                options.LogoutPath = "/Admin/Home/Login";//çıkış
                options.AccessDeniedPath = "/Admin/Home/AccessDenied";//Sayfaya yetkin yetmiyorsa bu ekran çıkar
                options.SlidingExpiration = true; // default time 20 dk. üye sitede 20 dk bir işlem yapmazsa otomatik atılır
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = "UyeCookie";
                options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
                //options.ExpireTimeSpan = TimeSpan.FromMinutes(40);
            });
            return services;
        }
    }
}
