using System;
using System.Collections.Generic;
using System.IO;


namespace GenerateBackendService
{
    class Program
    {
        static void Main(string[] args)
        {


            var SolutionName = "GAEB";
            var APIName = "Employee";  //service name
            var departementroperties = new List<Property>()
            {

                new Property()
                {
                   InputtypeID =  1,
                    FieldNameEnglish = "DeptId",
                    ValidationProperty=new List<string>()
                    {
                        "IsRequired()",

                    }
                },

                new Property()
                {
                    InputtypeID =  2,
                    FieldNameEnglish = "Name",
                    ValidationProperty=new List<string>()
                    {
                        "IsRequired()",
                        "HasMaxLength(100)",
                    }
                },


            };
            var personProperties = new List<Property>()
            {

                new Property()
                {
                   InputtypeID =  1,
                    FieldNameEnglish = "PersonId"
                    ,
                    ValidationProperty=new List<string>()
                    {
                        "IsRequired()"

                    }
                },

                new Property()
                {
                    InputtypeID =  2,
                    FieldNameEnglish = "Name",
                     ValidationProperty=new List<string>()
                    {
                        "IsRequired()",
                        "HasMaxLength(100)",
                    }
                },
                new Property()
                {
                    InputtypeID =  2,
                    FieldNameEnglish = "Age",
                     ValidationProperty=new List<string>()
                    {
                        "IsRequired()",

                    }
                },

            };

            var domainPath = APIName + ".Domain\\";

            var integrationTestPath = APIName + ".FunctionalTests";


            List<ItemProperty> controllersName = new List<ItemProperty>() {
                  new ItemProperty("Employe",personProperties),
                  new ItemProperty("Person",personProperties),
                 new ItemProperty("Departement",departementroperties),



            };
            List<string> controllernamelist = new List<string>();
            foreach (var Item in controllersName)
            {
                controllernamelist.Add(Item.itemName);
            }





            int i = 0;
            foreach (var item in controllersName)
            {

                   GC.Collect(0); GC.Collect(0); MainFunction($"{APIName}.API\\Controllers", $"{item.itemName}Controller.cs", ApiContent.GernerateController(SolutionName, APIName, item.itemName));


                GC.Collect(0); MainFunction($"{APIName}.API\\Application\\Commands", $"Delete{item.itemName}Command.cs", ApiContent.GernerateCommand(SolutionName, APIName, item.itemName, "Delete",item.listproperty));
                GC.Collect(0); MainFunction($"{APIName}.API\\Application\\Commands", $"Create{item.itemName}Command.cs", ApiContent.GernerateCommand(SolutionName, APIName, item.itemName, "Create", item.listproperty));
                GC.Collect(0); MainFunction($"{APIName}.API\\Application\\Commands", $"Update{item.itemName}Command.cs", ApiContent.GernerateCommand(SolutionName, APIName, item.itemName, "Update", item.listproperty));
                GC.Collect(0); MainFunction($"{APIName}.API\\Application\\Commands", $"Create{item.itemName}CommandHandler.cs", ApiContent.GernerateCommandHandler(SolutionName, APIName, item.itemName, "Create","Add"));
                GC.Collect(0); MainFunction($"{APIName}.API\\Application\\Commands", $"Delete{item.itemName}CommandHandler.cs", ApiContent.GernerateCommandHandler(SolutionName, APIName, item.itemName, "Delete","Delete"));
                GC.Collect(0); MainFunction($"{APIName}.API\\Application\\Commands", $"Update{item.itemName}CommandHandler.cs", ApiContent.GernerateCommandHandler(SolutionName, APIName, item.itemName, "Update","Update"));
                GC.Collect(0); MainFunction($"{APIName}.API\\Application\\Queries", $"{item.itemName}Queries.cs", ApiContent.GernerateAPIQueries(SolutionName, APIName, item.itemName,item.listproperty));
                GC.Collect(0); MainFunction($"{APIName}.API\\Application\\Queries", $"I{item.itemName}Query.cs", ApiContent.GernerateIAPIQuery(SolutionName, APIName, item.itemName));
                GC.Collect(0); MainFunction($"{APIName}.API\\Application\\Queries", $"{item.itemName}ViewModel.cs", ApiContent.GernerateAPIViewModel(SolutionName, APIName, item.itemName, item.listproperty));
                GC.Collect(0); MainFunction($"{APIName}.API\\Application\\IntegrationEvents\\Events", $"{item.itemName}CreatedIntegrationEvent.cs", ApiContent.GernerateIntegrationEvent(SolutionName, APIName, item.itemName, "Created", item.listproperty));
                GC.Collect(0); MainFunction($"{APIName}.API\\Application\\IntegrationEvents\\EventHandling", $"{item.itemName}CreatedIntegrationEventHandler.cs", ApiContent.GernerateIntegrationEventHandling(SolutionName, APIName, item.itemName, "Created"));
                GC.Collect(0); MainFunction($"{APIName}.API\\Application\\IntegrationEvents\\Events", $"{item.itemName}UpdatedIntegrationEvent.cs", ApiContent.GernerateIntegrationEvent(SolutionName, APIName, item.itemName, "Updated", item.listproperty));
                GC.Collect(0); MainFunction($"{APIName}.API\\Application\\IntegrationEvents\\EventHandling", $"{item.itemName}UpdatedIntegrationEventHandler.cs", ApiContent.GernerateIntegrationEventHandling(SolutionName, APIName, item.itemName, "Updated"));
                GC.Collect(0); MainFunction($"{APIName}.API\\Application\\IntegrationEvents\\Events", $"{item.itemName}DeletedIntegrationEvent.cs", ApiContent.GernerateIntegrationEvent(SolutionName, APIName, item.itemName, "Deleted", item.listproperty));
                GC.Collect(0); MainFunction($"{APIName}.API\\Application\\IntegrationEvents\\EventHandling", $"{item.itemName}DeletedIntegrationEventHandler.cs", ApiContent.GernerateIntegrationEventHandling(SolutionName, APIName, item.itemName, "Deleted"));




                GC.Collect(0); MainFunction($"{APIName}.API\\Application\\Validations", $"Create{item.itemName}Validator.cs", ApiContent.GernerateCreateAPIValidator(SolutionName, APIName, item.itemName, "Create"));

                GC.Collect(0); MainFunction($"{APIName}.API\\Application\\Validations", $"Update{item.itemName}Validator.cs", ApiContent.GernerateCreateAPIValidator(SolutionName, APIName, item.itemName, "Update"));

                GC.Collect(0); MainFunction($"{APIName}.API\\Application\\Validations", $"Delete{item.itemName}Validator.cs", ApiContent.GernerateCreateAPIValidator(SolutionName, APIName, item.itemName, "Delete"));


                GC.Collect(0); MainFunction($"{APIName}.Domain\\AggregatesModel\\{APIName}Aggregate", item.itemName + ".cs", DomainContent.GenerateAggregatesModel(SolutionName, APIName, item.itemName, item.listproperty));


                GC.Collect(0); MainFunction($"{APIName}.Domain\\Events", $@"{item.itemName}DomainEvent.cs", DomainContent.GeneratePermanentDomainEvent(SolutionName, APIName, item.itemName));
                GC.Collect(0); MainFunction($"{APIName}.Domain\\AggregatesModel\\{APIName}Aggregate", $"I{item.itemName}Repository.cs", DomainContent.GenerateEntityRepository(SolutionName, APIName, item.itemName));
                GC.Collect(0); MainFunction($"{APIName}.Infrastructure\\Repository", $@"{item.itemName}Repository.cs", InfratStructureContent.gernerateRepository(SolutionName, APIName, item.itemName));
                GC.Collect(0); MainFunction($"{APIName}.Infrastructure\\EntityConfigurations", $@"{item.itemName}EntityTypeConfiguration.cs", InfratStructureContent.gernerateEntityTypeConfiguration(SolutionName, APIName, item.itemName, item.listproperty));
                GC.Collect(0); MainFunction($"{APIName}.UnitTests\\Application", $@"{item.itemName}WebApiTest.cs", UnitTestContent.GenerateWebApiTest(SolutionName, APIName, item.itemName));


                GC.Collect(0); MainFunction($"{APIName}.UnitTests", $"{item.itemName}Builder.cs", UnitTestContent.GenerateBuilder(APIName, item.itemName, item.listproperty, SolutionName));

                GC.Collect(0); MainFunction($"{APIName}.UnitTests\\Application", $@"{item.itemName}WebApiTest.cs", UnitTestContent.GenerateWebApiTest(SolutionName, APIName, item.itemName));

                // integration test
                MainFunction(integrationTestPath, item.itemName + "ScenarioBase.cs", FunctionalTestContent.GenerateServiceScenarioBase(SolutionName, APIName, item.itemName));
                MainFunction(integrationTestPath, item.itemName + "Scenarios.cs", FunctionalTestContent.GenerateControllerScenarios(SolutionName, APIName, item.itemName, item.listproperty));
                GC.Collect(0); MainFunction($"{APIName}.UnitTests\\Application", $@"{item.itemName}CommandHandlerTest.cs", UnitTestContent.GenerateCommandHandlerTest(SolutionName, APIName, item.itemName, item.listproperty));



            }


            GC.Collect(0); MainFunction($"{APIName}.API", "Startup.cs", ApiContent.gernerateStartup(SolutionName, APIName, controllernamelist));
            GC.Collect(0); MainFunction($"{APIName}.API", "Program.cs", ApiContent.gernerateProgram(SolutionName, APIName));
            GC.Collect(0); MainFunction($"{APIName}.API", $"{APIName}.API.csproj", ApiContent.gernerateCsproj(SolutionName, APIName));
            GC.Collect(0); MainFunction($"{APIName}.API", "appsettings.json", ApiContent.gernerateAppsettings(SolutionName, APIName));
            GC.Collect(0); MainFunction($"{APIName}.API", "appsettings.Development.json", ApiContent.gernerateAppsettingsDevelopment(SolutionName, APIName));
            GC.Collect(0); MainFunction($"{APIName}.API\\Properties", "launchSettings.json", ApiContent.gerneratelaunchSettings(SolutionName, APIName));
            GC.Collect(0); MainFunction($"{APIName}.API\\Infrastructure\\ActionResults", "InternalServerErrorObjectResult.cs", ApiContent.GernerateInternalServerErrorObjectResult(SolutionName, APIName));
            GC.Collect(0); MainFunction($"{APIName}.API\\Infrastructure\\AutofacModules", "ApplicationModule.cs", ApiContent.GernerateApplicationModule(SolutionName, APIName, controllernamelist));
            GC.Collect(0); MainFunction($"{APIName}.API\\Infrastructure\\AutofacModules", "MediatorModule.cs", ApiContent.GernerateMediatorModule(SolutionName, APIName, controllernamelist));
            GC.Collect(0); MainFunction($"{APIName}.API\\Infrastructure\\Filters", "HttpGlobalExceptionFilter.cs", ApiContent.gernerateHttpGlobalExceptionFilter(SolutionName, APIName));
            GC.Collect(0); MainFunction($"{APIName}.API\\Application\\Behaviors", "ValidatorBehavior.cs", ApiContent.GernerateValidatorBehavior(SolutionName, APIName));
            GC.Collect(0); MainFunction($"{APIName}.API\\Application\\Behaviors", "LoggingBehavior.cs", ApiContent.GernerateLoggingBehavior(SolutionName, APIName));
            GC.Collect(0); MainFunction($"{APIName}.API\\Application\\Behaviors", "TransactionBehaviour.cs", ApiContent.GernerateTransactionBehaviour(SolutionName, APIName));



            GC.Collect(0); MainFunction($"{APIName}.API\\Application\\Validations", "JsonErrorResponse.cs", ApiContent.GernerateJsonErrorResponse(SolutionName, APIName));
            GC.Collect(0); MainFunction($"{APIName}.API\\Application\\Validations", "ValidateModelStateFilter.cs", ApiContent.GernerateValidateModelStateFilter(SolutionName, APIName));
            GC.Collect(0); MainFunction($"{APIName}.API\\Application\\Models", $"ModelPaging.cs", ApiContent.GenerateModelPagingClass(SolutionName, APIName));
            GC.Collect(0); MainFunction($"{APIName}.API\\Application\\Models", $"PageVM.cs", ApiContent.GenerateModelPageVmClass(SolutionName, APIName));


            GC.Collect(0); MainFunction($"{APIName}.Domain\\SeedWork", "Entity.cs", DomainContent.GenerateEntity(SolutionName, APIName));
            GC.Collect(0); MainFunction($"{APIName}.Domain\\SeedWork", "Enumeration.cs", DomainContent.GenerateEnumeration(SolutionName, APIName));
            GC.Collect(0); MainFunction($"{APIName}.Domain\\SeedWork", "IAggregateRoot.cs", DomainContent.GenerateIAggregateRoot(SolutionName, APIName));
            GC.Collect(0); MainFunction($"{APIName}.Domain\\SeedWork", "IRepository.cs", DomainContent.GenerateIRepository(SolutionName, APIName));
            GC.Collect(0); MainFunction($"{APIName}.Domain\\SeedWork", "IUnitOfWork.cs", DomainContent.GenerateIUnitOfWork(SolutionName, APIName));
            GC.Collect(0); MainFunction($"{APIName}.Domain\\SeedWork", "ValueObject.cs", DomainContent.GenerateValueObject(SolutionName, APIName));
            GC.Collect(0); MainFunction($"{APIName}.Domain\\Exceptions", APIName + "DomainException.cs", DomainContent.GenerateDomainException(SolutionName, APIName));

         


            GC.Collect(0); MainFunction($"{APIName}.Domain", $"{APIName}.Domain.csproj", DomainContent.GenerateDomainCsProjMethod(APIName));
            //MainFunction(domainPath + aggregatesModel.pathName, "Address.cs", aggregatesModel.GenerateModel(SolutionName, APIName, "Address", addressProperties));

            GC.Collect(0); MainFunction($"{APIName}.Infrastructure", "MediatorExtension.cs", InfratStructureContent.gernerateMediatorExtension("GAEB", APIName));
            GC.Collect(0); MainFunction($"{APIName}.Infrastructure", APIName+"Context.cs", InfratStructureContent.gernerateContext("GAEB", APIName, controllernamelist));
          

            GC.Collect(0); MainFunction($"{APIName}.Infrastructure", APIName +".Infrastructure.csproj", InfratStructureContent.gernerateCsProj("GAEB", APIName, APIName));
         


            // Integration Testing
            MainFunction(integrationTestPath, "AutoAuthorizeMiddleware.cs", FunctionalTestContent.GenerateAutoAuthorizeMiddleware(SolutionName, APIName, "9e3163b9-1ae6-4652-9dc6-7898ab7b7a00"));;
            MainFunction(integrationTestPath, $"{APIName}.TestStartup.cs", FunctionalTestContent.GenerateServiceTestsStartup(SolutionName, APIName));
            MainFunction(integrationTestPath, "HttpClientExtensions.cs", FunctionalTestContent.GenerateHttpClientExtensions(SolutionName, APIName));
            MainFunction(integrationTestPath, $"{APIName}.FunctionalTests.csproj", FunctionalTestContent.GenerateCSPROJ(APIName));
            MainFunction(integrationTestPath, "appsettings.json", FunctionalTestContent.GenerateAppSettings(APIName));

            GC.Collect(0); MainFunction($"{APIName}.API\\Infrastructure", $"{APIName}ContextSeed.cs", ApiContent.GernerateAPIContextSeed(SolutionName, APIName));
            GC.Collect(0); MainFunction($"{APIName}.API", $"{APIName}Settings.cs", ApiContent.GernerateAPISettings(SolutionName, APIName));
            GC.Collect(0); MainFunction($"{APIName}.UnitTests\\", $"{APIName}.UnitTests.csproj", UnitTestContent.GenerateUnitTestscsproj(APIName, controllersName[0].itemName));

            GC.Collect(0); MainFunction($"{APIName}.API\\Application\\IntegrationEvents", $"I{APIName}IntegrationEventService.cs", ApiContent.GernerateIAPIntegrationEventService(SolutionName, APIName));
            GC.Collect(0); MainFunction($"{APIName}.API\\Application\\IntegrationEvents", $"{APIName}IntegrationEventService.cs", ApiContent.GernerateAPIntegrationEventService(SolutionName, APIName));

            GC.Collect(0); MainFunction($"{APIName}.API\\Infrastructure\\Services", "IdentityService.cs", ApiContent.GernerateIdentityService(SolutionName, APIName));
            GC.Collect(0); MainFunction($"{APIName}.API\\Infrastructure\\Services", "IIdentityService.cs", ApiContent.GernerateIIdentityService(SolutionName, APIName));
            GC.Collect(0); MainFunction($"{APIName}.UnitTests\\Domain", $@"{APIName}AggregateTest.cs", UnitTestContent.GenerateAggregateTest(SolutionName, APIName, controllernamelist, controllersName[0].listproperty));
        }



        public static void MainFunction(string subPath, string fileName, string text)
        {
            string realPath = "D:\\Employee\\";

            realPath = Path.Combine(realPath, subPath);
            if (!(Directory.Exists(realPath)))
                Directory.CreateDirectory(realPath);

            if (fileName != "")
            {
                Console.WriteLine(realPath);
                realPath = Path.Combine(realPath, fileName);
                File.WriteAllText(realPath, text);
            }
        }
    }
}
