using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Interceptors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Server.Auth;
using Server.Backend;

namespace Server
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddHttpContextAccessor();

            services.AddAuthorization(x =>
            {
                x.AddPolicy("hr-department", builder =>
                    builder
                        .RequireAuthenticatedUser()
                        .RequireRole("hr")
                );

                x.AddPolicy("DevDepartment", builder =>
                    builder.RequireRole("dev")
                );
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = "audience",
                    ValidIssuer = "issuer",
                    RequireSignedTokens = false,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretsecretsecret"))
                };

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
            });

            services.AddGraphQL(
                SchemaBuilder
                    .New()
                    .AddQueryType<Query>()
                    .AddMutationType<Mutation>()
                    .AddAuthorizeDirectiveType()
                    .Create()
            );

            services.AddQueryRequestInterceptor(AuthenticationInterceptor());

        }

        private static OnCreateRequestAsync AuthenticationInterceptor()
        {
            return (context, builder, token) =>
            {
                if (context.GetUser().Identity.IsAuthenticated)
                {
                    builder.SetProperty("currentUser",
                        new CurrentUser(Guid.Parse(context.User.FindFirstValue(ClaimTypes.NameIdentifier)),
                            context.User.Claims.Select(x => $"{x.Type} : {x.Value}").ToList()));
                }

                return Task.CompletedTask;
            };
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UsePlayground();
            app.UseGraphQL();
        }
    }
}