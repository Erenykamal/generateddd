
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GenerateBackendService
{
    public class FunctionalTestContent
    {
        public static string GenerateAutoAuthorizeMiddleware(string SOLUTION_NAME, string API_NAME, string IDENTITY_ID_VALUE)
        {
            var txt = $@"using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SOLUTION_NAME.Services.API_NAME.FunctionalTests
{{
                class AutoAuthorizeMiddleware
            {{
        public const string IDENTITY_ID = ""IDENTITY_ID_VALUE"";

        private readonly RequestDelegate _next;

        public AutoAuthorizeMiddleware(RequestDelegate rd)
        {{
            _next = rd;
        }}

        public async Task Invoke(HttpContext httpContext)
        {{
            var identity = new ClaimsIdentity(""cookies"");

            identity.AddClaim(new Claim(""sub"", IDENTITY_ID));
            identity.AddClaim(new Claim(""unique_name"", IDENTITY_ID));
            identity.AddClaim(new Claim(ClaimTypes.Name, IDENTITY_ID));

            httpContext.User.AddIdentity(identity);

            await _next.Invoke(httpContext);
        }}
    }}
    }}
";
            txt = txt.Replace("IDENTITY_ID_VALUE", IDENTITY_ID_VALUE);
            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);

            return txt;
        }


        public static string GenerateServiceTestsStartup(string SOLUTION_NAME, string API_NAME)
        {
            var txt = $@"using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using SOLUTION_NAME.Services.API_NAME.API;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SOLUTION_NAME.Services.API_NAME.FunctionalTests
{{
        public class API_NAMETestsStartup : Startup
    {{
        public API_NAMETestsStartup(IConfiguration env) : base(env)
        {{
        }}

        public override IServiceProvider ConfigureServices(IServiceCollection services)
        {{
            // Added to avoid the Authorize data annotation in test environment. 
            // Property ""SuppressCheckForUnhandledSecurityMetadata"" in appsettings.json
            services.Configure<RouteOptions>(Configuration);
            return base.ConfigureServices(services);
        }}
        protected override void ConfigureAuth(IApplicationBuilder app)
        {{
            if (Configuration[""isTest""] == bool.TrueString.ToLowerInvariant())
            {{
                app.UseMiddleware<AutoAuthorizeMiddleware>();
            }}
            else
            {{
                base.ConfigureAuth(app);
            }}
        }}
    }}
    }}

";
            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            return txt;
        }


        public static string GenerateServiceScenarioBase(string SOLUTION_NAME, string API_NAME, string CONTROLLER_NAME)
        {
            var txt = $@"using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using SOLUTION_NAME.BuildingBlocks.IntegrationEventLogEF;

using SOLUTION_NAME.Services.API_NAME.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;
using System.Reflection;
using SOLUTION_NAME.API_NAME.API;
using SOLUTION_NAME.Services.API_NAME.API.Infrastructure;

namespace SOLUTION_NAME.Services.API_NAME.FunctionalTests
{{
    public class CONTROLLER_NAMEScenarioBase
    {{
        public TestServer CreateServer()
        {{
            var path = Assembly.GetAssembly(typeof(CONTROLLER_NAMEScenarioBase))
                .Location;

            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureAppConfiguration(cb =>
                {{
                    cb.AddJsonFile(""appsettings.json"", optional: false)
                    .AddEnvironmentVariables();
                }}).UseStartup<API_NAMETestsStartup>();

            var testServer = new TestServer(hostBuilder);

            testServer.Host
                .MigrateDbContext<API_NAMEContext>((context, services) =>
                {{
                    var env = services.GetService<IWebHostEnvironment>();
                    var settings = services.GetService<IOptions<API_NAMESettings>>();
                    var logger = services.GetService<ILogger<API_NAMEContextSeed>>();

                    new API_NAMEContextSeed()
                        .SeedAsync(context, env, settings, logger)
                        .Wait();
                }})
                .MigrateDbContext<IntegrationEventLogContext>((_, __) => {{ }});

            return testServer;
        }}

        public static class Get
        {{
            public static string CONTROLLER_NAMEs = ""api/CONTROLLER_NAME"";
            public static string CONTROLLER_NAMEsById(int id) {{ return $""api/CONTROLLER_NAME/{{id}}"";}}
        }}
        public static class PostAsync
        {{
      public static string CONTROLLER_NAMEs = ""api/CONTROLLER_NAME"";
     
        }}
        public static class PutAsync
        {{
            public static string CONTROLLER_NAMEs = ""api/CONTROLLER_NAME"";

        }}
        public static class DeleteAsync
        {{
            public static string CONTROLLER_NAMEs = ""api/CONTROLLER_NAME"";

        }}
    }}
}}
";

            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            txt = txt.Replace("CONTROLLER_NAME", CONTROLLER_NAME);
            return txt;
        }


        public static string GenerateControllerScenarios(string SOLUTION_NAME, string API_NAME, string CONTROLLER_NAME, List<Property> ScreenComponents)
        {
            var txt = $@"using Newtonsoft.Json;
using GAEB.Services.Employee.Domain.AggregatesModel.API_NAMEAggregate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SOLUTION_NAME.Services.API_NAME.API.Application.Commands;
using SOLUTION_NAME.Services.API_NAME.Domain.AggregatesModel.API_NAMEAggregate;
using Xunit;

namespace SOLUTION_NAME.Services.API_NAME.FunctionalTests
{{
    public class CONTROLLER_NAMEScenarios
        : CONTROLLER_NAMEScenarioBase
    {{
        [Fact]
        public async Task Get_get_all_CONTROLLER_NAMEs_and_response_ok_status_code()
        {{
            using (var server = CreateServer())
            {{
                var response = await server.CreateClient()
                    .GetAsync(Get.CONTROLLER_NAMEs);

                response.EnsureSuccessStatusCode();
            }}
        }}
        [Fact]
        public async Task Get_CONTROLLER_NAME_by_id_and_response_not_found()
        {{
            using (var server = CreateServer())
            {{
                var response = await server.CreateClient()
                    .GetAsync(Get.CONTROLLER_NAMEsById(-1));

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }}
        }}

        [Fact]
        public async Task Get_CONTROLLER_NAME_by_id_and_response_ok_request_code()
        {{
            using (var server = CreateServer())
            {{
                var response = await server.CreateClient()
                    .GetAsync(Get.CONTROLLER_NAMEsById(1));

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }}
        }}
        [Fact]
        public async Task Post_CONTROLLER_NAME_and_response_bad_request_code()
        {{
            using (var server = CreateServer())
            {{
                var content = new StringContent(BuildbadCONTROLLER_NAME(), UTF8Encoding.UTF8, ""application / json"");
                var response = await server.CreateIdempotentClient()
                    .PostAsync(PostAsync.CONTROLLER_NAMEs, content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }}
    }}
    [Fact]
    public async Task Post_CONTROLLER_NAME_and_response_ok_status_code()
    {{
        using (var server = CreateServer())
        {{
            var str = BuildgoodCONTROLLER_NAME();
            var content = new StringContent(BuildgoodCONTROLLER_NAME(), UTF8Encoding.UTF8, ""application/json"");

            var response = await server.CreateIdempotentClient()
                .PostAsync(PostAsync.CONTROLLER_NAMEs, content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }}
    }}
    string BuildbadCONTROLLER_NAME()
    {{
        var emp = new CONTROLLER_NAME()
        {{
            //fill uncorrect data
        }};
        return JsonConvert.SerializeObject(emp);
    }}
    string BuildgoodCONTROLLER_NAME()
    {{
        var goodemp = new CreateCONTROLLER_NAMECommand("; foreach (var item in ScreenComponents)
            {
                txt+= $@"{ item.InputtypeIDvalue()},";
            }
            txt = txt.Remove(txt.Length - 1, 1);
            txt += $@" ); 
    return JsonConvert.SerializeObject(goodemp);
    }}
}}
}}

";
            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            txt = txt.Replace("CONTROLLER_NAME", CONTROLLER_NAME);
            return txt;
        }


        public static string GenerateHttpClientExtensions(string SOLUTION_NAME, string API_NAME)
        {
            var txt = $@"using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;

namespace SOLUTION_NAME.Services.API_NAME.FunctionalTests
{{
            static class HttpClientExtensions
            {{
                public static HttpClient CreateIdempotentClient(this TestServer server)
                {{
                    var client = server.CreateClient();
                    // client.DefaultRequestHeaders.Add(""x-requestid"", Guid.NewGuid().ToString());
                    return client;
                }}
            }}
            }}
";
            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            return txt;
        }

        public static string GenerateCSPROJ(string API_NAME)
        {
            var txt = $@"<Project Sdk=""Microsoft.NET.Sdk"">

  <PropertyGroup>
    <TargetFramework>net5.0</TargetFramework>

    <IsPackable>false</IsPackable>
  </PropertyGroup>

  <ItemGroup>
    <None Remove=""appsettings.json"" />
  </ItemGroup>

  <ItemGroup>
    <Content Include=""appsettings.json"">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </Content>
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include=""Microsoft.AspNetCore.Mvc.Testing"" Version=""5.0.2"" />
    <PackageReference Include=""Microsoft.NET.Test.Sdk"" Version=""16.8.3"" />
    <PackageReference Include=""Microsoft.AspNetCore.TestHost"" Version=""5.0.2"" />
    <PackageReference Include=""xunit.runner.visualstudio"" Version=""2.4.3"">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers</IncludeAssets>
    </PackageReference>
    <PackageReference Include=""xunit"" Version=""2.4.1"" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include=""..\..\..\BuildingBlocks\WebHost.Customization\WebHost.Customization.csproj"" />
    <ProjectReference Include=""..\API_NAME.API\API_NAME.API.csproj"" />
    <ProjectReference Include=""..\API_NAME.Domain\API_NAME.Domain.csproj"" />
    <ProjectReference Include=""..\API_NAME.Infrastructure\API_NAME.Infrastructure.csproj"" />
    <ProjectReference Include=""..\API_NAME.UnitTests\API_NAME.UnitTests.csproj"" />
  </ItemGroup>

</Project>
";
            txt = txt.Replace("API_NAME", API_NAME);

            return txt;
        }

        public static string GenerateAppSettings(string API_NAME)
        {
            var txt = $@"{{
  ""CheckUpdateTime"": ""30000"",
  ""ConnectionString"": ""Server=.;Initial Catalog=API_NAMEDb;Integrated Security=true"",
  ""EventBusConnection"": ""localhost"",
  ""API_NAMEUrl"": ""http://localhost:5003"",
  ""GracePeriodTime"": ""1"",
  ""IdentityUrl"": ""http://localhost:5105"",
  ""IdentityUrlExternal"": ""http://localhost:5105"",
  ""isTest"": ""true"",
  ""SubscriptionClientName"": ""API_NAME"",
  ""SuppressCheckForUnhandledSecurityMetadata"": true
}}


";
            txt = txt.Replace("API_NAME", API_NAME);

            return txt;
        }
    }
}
