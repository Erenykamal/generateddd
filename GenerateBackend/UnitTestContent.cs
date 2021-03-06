using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Humanizer;
namespace GenerateBackendService
{
    public static class UnitTestContent
    {
        public static string GenerateBuilder(string API_NAME, string ENTITY_NAME,List<Property> prolist,string SOLUTION_NAME)
        {
            string props = String.Empty;
           //  Type t = Type.GetType("SOLUTION_NAME.Services.API_NAME.Domain.AggregatesModel.API_NAMEAggregate "+ ENTITY_NAME);
           // Object obj = Activator.CreateInstance(t);
           //// Object obj = new Employee();
            string txt = $@"
            

            namespace UnitTest.{ENTITY_NAME.Pluralize()}
            {{
              using SOLUTION_NAME.Services.API_NAME.Domain.AggregatesModel.API_NAMEAggregate;
            using System.Collections.Generic;
        public class ENTITY_NAMEBuilder
        {{
        private readonly ENTITY_NAME {ENTITY_NAME.Camelize()};

            public ENTITY_NAMEBuilder(";
            foreach (var  propertyInfo in prolist)
            {
                props += propertyInfo.InputtypeIDstr() + " " + propertyInfo.FieldNameEnglish + ",";
            }

            txt += props.Substring(0, props.Length - 1) + ")";

            txt += $@"
                 {{";
            foreach (var propertyInfo in prolist)
            {
                txt += $@"{ENTITY_NAME.Camelize()}.{propertyInfo.FieldNameEnglish}={propertyInfo.FieldNameEnglish};" + System.Environment.NewLine;
            }
            txt += $@"}}";
            txt += $@"
                     public ENTITY_NAMEBuilder AddOne()
                     {{
          
                           return this;
                      }}

        public ENTITY_NAME Build()
        {{
            return {ENTITY_NAME.Camelize()};
        }}
              }}
}}";
            txt = txt.Replace("API_NAME", API_NAME);
            txt = txt.Replace("ENTITY_NAME", ENTITY_NAME);
            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            
            return txt;
        }
        public static string GenerateAggregateTest(string SOLUTION_NAME,string API_NAME, List<string> controlls, List<Property> prolist)
        {
            
            string args = string.Empty;
            foreach (var PropertyInfo  in prolist)
            {
                if (PropertyInfo.InputtypeID ==  2)
                {
                    args += $@""""",";
                }
                else if (PropertyInfo.InputtypeID ==  1)
                {
                    args += $@"_random.Next(1, 10000000),";
                }


            }

            string txt = $@"
namespace  UnitTest.Domain
{{
using SOLUTION_NAME.Services.API_NAME.Domain.AggregatesModel.API_NAMEAggregate;
using System;
using Xunit;

public class API_NAMEAggregateTest
{{
    public API_NAMEAggregateTest()
    {{ }}
";
            foreach (var item in controlls)
            {
                txt += $@"
    [Fact]
    public void Create_{item.Camelize()}_success()
    {{

        //Arrange  
        var _random = new Random();
        var identity = _random.Next(1, 10000000);

        //Act 
        var fake{item}Item = new {item}(); 
           

        //Assert
        Assert.NotNull(fake{item}Item);
    }}";
            }

txt+="}}";
           

            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
          
            return txt;
        }
        public static string GenerateCommandHandlerTest(string SOLUTION_NAME, string API_NAME, string ENTITY_NAME,List<Property> prolist)
        {
           
            string args = string.Empty;
            foreach (var  propertyInfo in  prolist)
            {
                if (propertyInfo.InputtypeID == 2)
                {
                    args += $@""""",";
                }
                else if (propertyInfo.InputtypeID ==  1)
                {
                    args += $@"_random.Next(1, 10000000),";
                }


            }
            string txt = $@"

using SOLUTION_NAME.Services.API_NAME.API.Infrastructure.Services;

using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace UnitTest.API_NAME.Application
{{
    using SOLUTION_NAME.Services.API_NAME.API.Application.Commands;
    using SOLUTION_NAME.Services.API_NAME.API.Application.IntegrationEvents;
    using SOLUTION_NAME.Services.API_NAME.Domain.AggregatesModel.API_NAMEAggregate;

    using MediatR;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using Xunit;

    public class ENTITY_NAMECommandHandlerTest
    {{
        private readonly Mock<IENTITY_NAMERepository> _orderRepositoryMock;
        private readonly Mock<IIdentityService> _identityServiceMock;
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<IAPI_NAMEIntegrationEventService> _API_NAMEIntegrationEventService;

        public ENTITY_NAMECommandHandlerTest()
        {{

            _orderRepositoryMock = new Mock<IENTITY_NAMERepository>();
            _identityServiceMock = new Mock<IIdentityService>();
            _API_NAMEIntegrationEventService = new Mock<IAPI_NAMEIntegrationEventService>();
            _mediator = new Mock<IMediator>();
        }}

        [Fact]
        public async Task Handle_return_false_if_ENTITY_NAME_created()
        {{
       
        }}

        [Fact]
        public void Handle_throws_exception_when_no_ENTITY_NAMEId()
        {{
             var _random = new Random();
        }}

 
        private ENTITY_NAME FakeENTITY_NAME()
        {{
  var _random = new Random();
            return new ENTITY_NAME({args.Substring(0, args.Length - 1)});
        }}

        private CreateENTITY_NAMECommand FakeENTITY_NAMECommand(Dictionary<string, object> args = null)
        {{
            return new CreateENTITY_NAMECommand();
               
        }}
    }}
}}
";
            txt = txt.Replace("API_NAME", API_NAME);
            txt = txt.Replace("ENTITY_NAME", ENTITY_NAME);
            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            return txt;
        }
        public static string GenerateWebApiTest(string SOLUTION_NAME,string API_NAME,string ENTITY_NAME)
        {
            string txt = $@"
using MediatR;
using Microsoft.AspNetCore.Mvc;

using SOLUTION_NAME.Services.API_NAME.API.Controllers;

using Microsoft.Extensions.Logging;
using Moq;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using SOLUTION_NAME.Services.API_NAME.API.Application.Queries;
using SOLUTION_NAME.Services.API_NAME.API.Infrastructure.Services;

namespace UnitTest.ENTITY_NAME.Application
{{
    using GAEB.Services.Employee.Domain.AggregatesModel.API_NAMEAggregate;
    public class ENTITY_NAMEWebApiTest
    {{
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IENTITY_NAMEQueries> _{ENTITY_NAME.Camelize()}QueriesMock;
        private readonly Mock<IIdentityService> _identityServiceMock;
        private readonly Mock<ILogger<ENTITY_NAMEController>> _loggerMock;

        public ENTITY_NAMEWebApiTest()
        {{
            _mediatorMock = new Mock<IMediator>();
            _{ENTITY_NAME.Camelize()}QueriesMock = new Mock<IENTITY_NAMEQueries>();
            _identityServiceMock = new Mock<IIdentityService>();
            _loggerMock = new Mock<ILogger<ENTITY_NAMEController>>();
        }}

 
        [Fact]
        public async Task Get_ENTITY_NAME_success()
        {{
            //Arrange
            var fakeDynamicResult = Enumerable.Empty<ENTITY_NAME>();
             ENTITY_NAMEView _ENTITY_NAMEView =null;
            _identityServiceMock.Setup(x => x.GetUserIdentity())
                .Returns(Guid.NewGuid().ToString());

             _{ENTITY_NAME.Camelize()}QueriesMock.Setup(x => x.GetAllENTITY_NAMEAsync(_ENTITY_NAMEView));

            //Act
            var {ENTITY_NAME.Camelize()}Controller = new ENTITY_NAMEController(_mediatorMock.Object, _{ENTITY_NAME.Camelize()}QueriesMock.Object);
            var actionResult= await {ENTITY_NAME.Camelize()}Controller.Get(_ENTITY_NAMEView);

            //Assert
            Assert.Equal((actionResult as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
        }}  
    }}
}}

";
            txt = txt.Replace("API_NAME", API_NAME);
            txt = txt.Replace("ENTITY_NAME", ENTITY_NAME);
            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            return txt;
        }
        public static string GenerateUnitTestscsproj(string API_NAME, string ENTITY_NAME)
        {
            string txt = @"
       <Project Sdk=""Microsoft.NET.Sdk"">

      <PropertyGroup>
             <TargetFramework >net5.0</TargetFramework >
       </PropertyGroup >
   

         <ItemGroup >
   
           <PackageReference Include = ""MediatR"" Version = ""9.0.0"" />
      
              <PackageReference Include = ""Microsoft.NET.Test.Sdk"" Version = ""16.8.3"" />
         
                 <PackageReference Include = ""Microsoft.NETCore.Platforms"" Version = ""5.0.0"" />
            
                    <PackageReference Include = ""Moq"" Version = ""4.15.2"" />
               
                       <PackageReference Include = ""xunit.runner.visualstudio"" Version = ""2.4.3"" >
                  
                            <PrivateAssets >all</PrivateAssets >
                  
                            <IncludeAssets>runtime; build; native; contentfiles; analyzers</IncludeAssets>
                     
                             </PackageReference >
                     
                             <PackageReference Include = ""xunit"" Version = ""2.4.1""/>
                        
                              </ItemGroup >
                        

                              <ItemGroup >
                        
                                <ProjectReference Include = ""..\API_NAME.API\API_NAME.API.csproj""/>
                         
                                 <ProjectReference Include = ""..\API_NAME.Domain\API_NAME.Domain.csproj""/>
                          
                                  <ProjectReference Include = ""..\API_NAME.Infrastructure\API_NAME.Infrastructure.csproj""/>
                           
                                 </ItemGroup>
                           

                               </Project >


                           ";
            txt = txt.Replace("API_NAME", API_NAME);
            txt = txt.Replace("ENTITY_NAME", ENTITY_NAME);
            return txt;
        }
    }
     
}
