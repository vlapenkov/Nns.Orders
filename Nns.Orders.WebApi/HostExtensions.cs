using FluentValidation.AspNetCore;
using FluentValidation;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.ComponentModel;
using Nns.Orders.WebApi.Converters;

public static class HostExtensions
    {
        public static void RunApi(this WebApplicationBuilder builder,
         Action<IHostBuilder, IConfiguration, IServiceCollection> configureBuilder,
         Action<WebApplication>? configureApp = null)
        {
            builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(o =>
    {
        o.InvalidModelStateResponseFactory = c => new BadRequestObjectResult(new ValidationError(c.ModelState));
    })
            .AddJsonOptions(options =>
            {

                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.DescribeAllParametersInCamelCase();
                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, true);
            });

            builder.Services.AddFluentValidationRulesToSwagger();

            //ValidatorOptions.Global.LanguageManager = new RussianLanguageManager();
            ValidatorOptions.Global.PropertyNameResolver =
                (Type type, MemberInfo memberInfo, LambdaExpression expression) =>
                    JsonNamingPolicy.CamelCase.ConvertName(memberInfo?.Name ?? string.Empty);
            ValidatorOptions.Global.DisplayNameResolver =
                (Type type, MemberInfo memberInfo, LambdaExpression expression) =>
                {
                    var a = memberInfo?.GetCustomAttribute<DisplayNameAttribute>();
                    return a != null ? a.DisplayName : memberInfo?.Name;
                };

            builder.Services.AddValidatorsFromAssembly(Assembly.GetEntryAssembly());
            builder.Services.AddFluentValidationAutoValidation(c => c.DisableDataAnnotationsValidation = true);

            configureBuilder(builder.Host, builder.Configuration, builder.Services);

            WebApplication app = builder.Build();

            if (configureApp != null)
                configureApp(app);

            app.UseRouting();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.Run();
        }
    }

