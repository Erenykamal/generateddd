using System;
using System.Collections.Generic;
using System.Text;

namespace GenerateBackendService
{
    public static class ApiContent
    {

        public static string GernerateCommand(string SOLUTION_NAME, string API_NAME, string CONTROLLER_NAME, string ACTION_NAME, List<Property> ScreenComponents)
        {
            string txt = @"
using MediatR;
using System.Runtime.Serialization;

namespace SOLUTION_NAME.Services.API_NAME.API.Application.Commands
{
 [DataContract]
 public class ACTION_NAMECONTROLLER_NAMECommand : IRequest<bool>
 {
  
";
            foreach (var item in ScreenComponents)
            {
                txt += $@"[DataMember]
                        public {item.InputtypeIDstr()} {item.FieldNameEnglish} {{get; private set; }}" + Environment.NewLine;
            }
            txt += @"

  public ACTION_NAMECONTROLLER_NAMECommand()
  {

  }

  public ACTION_NAMECONTROLLER_NAMECommand(";
            foreach (var item in ScreenComponents)
            {
                txt += $@"{item.InputtypeIDstr()} _{item.FieldNameEnglish},";
            }


            txt = txt.Remove(txt.Length - 1, 1);
            txt += @")
  {";
            foreach (var item in ScreenComponents)
            {
                txt += $@"{item.FieldNameEnglish}= _{item.FieldNameEnglish};" + Environment.NewLine;
            }

            txt += @"}
    }

}

";
            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            txt = txt.Replace("CONTROLLER_NAME", CONTROLLER_NAME);
            txt = txt.Replace("ACTION_NAME", ACTION_NAME);
            Console.WriteLine(txt);
            return txt;

        }
    
        public static string GernerateCommandHandler(string SOLUTION_NAME, string API_NAME, string CONTROLLER_NAME, string ACTION_NAME)
        {

            string txt = @"
using MediatR;
using SOLUTION_NAME.Services.API_NAME.Domain.AggregatesModel.API_NAMEAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SOLUTION_NAME.Services.API_NAME.API.Application.Commands
{
 public class ACTION_NAMECONTROLLER_NAMECommandHandler : IRequestHandler<ACTION_NAMECONTROLLER_NAMECommand, bool>
 {
  private readonly ICONTROLLER_NAMERepository _Repository;
  private readonly IMediator _mediator;
  public ACTION_NAMECONTROLLER_NAMECommandHandler(IMediator mediator, ICONTROLLER_NAMERepository Repository)
  {
_mediator = mediator;
_Repository = Repository;
  }

  public async Task<bool>Handle(ACTION_NAMECONTROLLER_NAMECommand request, CancellationToken cancellationToken)
  {
    //you must call ctor  with data
var CONTROLLER_NAME = new CONTROLLER_NAME();
_Repository.Add(CONTROLLER_NAME);

return await _Repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
  }

 }
}

";

            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            txt = txt.Replace("CONTROLLER_NAME", CONTROLLER_NAME);
            txt = txt.Replace("ACTION_NAME", ACTION_NAME);

            Console.WriteLine(txt);
            return txt;

        }
        public static string GernerateController(string SOLUTION_NAME, string API_NAME, string CONTROLLER_NAME)
        {
            string txt = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using SOLUTION_NAME.Services.API_NAME.API.Application.Commands;
using SOLUTION_NAME.Services.API_NAME.API.Application.Queries;
using System.Net;
using SOLUTION_NAME.Services.API_NAME.API.Application.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SOLUTION_NAME.Services.API_NAME.API.Controllers
{
 [Route(""api/[controller]"")]
 public class CONTROLLER_NAMEController : Controller
 {
  private readonly IMediator _mediator;
  private readonly ICONTROLLER_NAMEQueries _CONTROLLER_NAMEQueries;
  public CONTROLLER_NAMEController(IMediator mediator, ICONTROLLER_NAMEQueries CONTROLLER_NAMEQueries )
  {
_mediator = mediator;
			_CONTROLLER_NAMEQueries = CONTROLLER_NAMEQueries;
  }
  // GET: api/CONTROLLER_NAME
  [HttpPost(""getall"")]
 [ProducesResponseType(typeof(IEnumerable<CONTROLLER_NAMEView>), (int)HttpStatusCode.OK)]
 public async Task<ActionResult> Get(CONTROLLER_NAMEView _CONTROLLER_NAMEView)
  {
   var page = new PageVM(); 

            page.pageIndex = _CONTROLLER_NAMEView.pageNumber; 

            page.pageSize = _CONTROLLER_NAMEView.pageSize; 

            page.data = await  _CONTROLLER_NAMEQueries.GetAllCONTROLLER_NAMEAsync( _CONTROLLER_NAMEView);
            page.length =  _CONTROLLER_NAMEQueries.GetCONTROLLER_NAMELength();

            if ( page.data == null)
            {
                return BadRequest();
            }
            return Ok(page);
  }

  // GET api/CONTROLLER_NAME/5
  [HttpGet(""{id}"")]
    [ProducesResponseType(typeof(CONTROLLER_NAMEView), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
  public async Task<ActionResult> Get(int id)
  {
 var result = await  _CONTROLLER_NAMEQueries.GetCONTROLLER_NAMEAsync(id);
  if (result == null)
  {
       return  NotFound();
            }
            return Ok(result);
  }

  // POST api/CONTROLLER_NAME
  [HttpPost]
 [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
  public async Task<ActionResult>Post([FromBody] CreateCONTROLLER_NAMECommand createCONTROLLER_NAMECommand)
  {
            bool success = await _mediator.Send(createCONTROLLER_NAMECommand);
            if (success) return Ok();
            else
                return BadRequest();
  }

  // PUT api/CONTROLLER_NAME/5
  [HttpPut(""{id}"")]
  
		public async Task<ActionResult>PutAsync([FromBody] UpdateCONTROLLER_NAMECommand CONTROLLER_NAMECommand)
 {
  return Ok(await _mediator.Send(CONTROLLER_NAMECommand));

 
  }

  // DELETE api/CONTROLLER_NAME/5
  [HttpDelete(""{id}"")]

			public async Task<ActionResult>DeleteAsync(DeleteCONTROLLER_NAMECommand CONTROLLER_NAMECommand)
 {
  return Ok(await _mediator.Send(CONTROLLER_NAMECommand));

  
  }
 }
}

";
            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            txt = txt.Replace("CONTROLLER_NAME", CONTROLLER_NAME);
            Console.WriteLine(txt);
            return txt;
        }

        public static string GernerateAPIQueries(string SOLUTION_NAME, string API_NAME, string CONTROLLER_NAME, List<Property> ScreenComponents)
        {
            char open = '{';
            char close = '}';
            string txt = @"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SOLUTION_NAME.Services.API_NAME.Domain.AggregatesModel.API_NAMEAggregate;
using System.Data.SqlClient;
using Dapper;
using System.Text;
using System.Reflection;
using SOLUTION_NAME.Services.API_NAME.API.Application.Models;
namespace SOLUTION_NAME.Services.API_NAME.API.Application.Queries
{
 public class CONTROLLER_NAMEQueries : ICONTROLLER_NAMEQueries

 {
  private string _connectionString = string.Empty;



  public CONTROLLER_NAMEQueries(string constr)
  {
_connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));

  }
  		public async Task<IEnumerable<CONTROLLER_NAMEView>>GetAllCONTROLLER_NAMEAsync(CONTROLLER_NAMEView _CONTROLLER_NAMEView)
		  {
				using (var connection = new SqlConnection(_connectionString))
				{
				 connection.Open();

                List<CONTROLLER_NAMEView> returnedResult = new List<CONTROLLER_NAMEView>();
            
                string qr = @$""
			select * from(
			SELECT  ROW_NUMBER() OVER ( ORDER BY Id) AS RowNum ,*  FROM [API_NAMEDb].[dbo].[CONTROLLER_NAMEsView] where 
				{CONTROLLER_NAMEQueries.Sqlparamatervalues(_CONTROLLER_NAMEView)} 1=1""; ";
         
          
            txt += "if (_CONTROLLER_NAMEView.pageNumber<=0) {_CONTROLLER_NAMEView.pageNumber = 1; }";

            txt += "if (_CONTROLLER_NAMEView.pageSize <=0){_CONTROLLER_NAMEView.pageSize = 5;}";

            txt += $"int start = (((_CONTROLLER_NAMEView.pageNumber ?? 1) - 1) * _CONTROLLER_NAMEView.pageSize ?? 5); \n";
            txt += $"int end = start +_CONTROLLER_NAMEView.pageSize ?? 5; \n";

            txt += $"qr += $\"" + ")as r \" " + "; \n";
            txt += $" qr += $\"" + "where RowNum > {start} AND RowNum<={end}\"" + "; \n";
            txt += $"qr += $\"" + "ORDER BY RowNum\"; \n";

             txt += $"returnedResult =  connection.QueryAsync<CONTROLLER_NAMEView>(qr).Result.ToList(); \n";

            txt += @" return  returnedResult;



}






    }

   
        private  static StringBuilder Sqlparamatervalues(CONTROLLER_NAMEView _CONTROLLER_NAMEView)
        {
            StringBuilder paramater=new StringBuilder();
            foreach (PropertyInfo prop in _CONTROLLER_NAMEView.GetType().GetProperties())
            {
                if (prop.Name == ""pageNumber"" || prop.Name == ""pageSize"")
                    continue;
            if (prop.GetValue(_CONTROLLER_NAMEView) != null)
                paramater.Append($""({ prop.Name}={prop.GetValue(_CONTROLLER_NAMEView)})AND"");
        }
            return paramater;
        }


    public  int GetCONTROLLER_NAMELength()
        {
            int length = 0;
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    connection.ExecuteScalar<int>(""SELECT COUNT(*) FROM[API_NAMEDb].[dbo].[CONTROLLER_NAMEsView]"");
                }
            return length;
        }

    public async Task<CONTROLLER_NAMEView>GetCONTROLLER_NAMEAsync(int id)
  {
dynamic result ;
using (var connection = new SqlConnection(_connectionString))
{
 connection.Open();

  result = await connection.QueryAsync<dynamic>
  (@""Select * from [API_NAMEDb].[dbo].[CONTROLLER_NAMEs] where Id = @id""

, new {id }
);

 if (result.Count == 0)
  return null;

  
}
return MaptoCONTROLLER_NAMEView(result);
  }
public CONTROLLER_NAMEView MaptoCONTROLLER_NAMEView(dynamic result)
        {

 var CONTROLLER_NAMEview = new CONTROLLER_NAMEView
            {";

            foreach (var item in ScreenComponents)
            {

                txt += @$"{item.FieldNameEnglish} = result[0].{item.FieldNameEnglish}," + Environment.NewLine;

            }


            txt += @" 
            };
  return CONTROLLER_NAMEview;

 }
}
}
";
            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            txt = txt.Replace("CONTROLLER_NAME", CONTROLLER_NAME);

            Console.WriteLine(txt);
            return txt;
        }
        public static string GernerateIAPIQuery(string SOLUTION_NAME, string API_NAME, string CONTROLLER_NAME)
        {
            string txt = @"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SOLUTION_NAME.Services.API_NAME.API.Application.Models;
namespace SOLUTION_NAME.Services.API_NAME.API.Application.Queries
{
public interface ICONTROLLER_NAMEQueries
{
Task<CONTROLLER_NAMEView>GetCONTROLLER_NAMEAsync(int id);


  Task<IEnumerable<CONTROLLER_NAMEView>> GetAllCONTROLLER_NAMEAsync(CONTROLLER_NAMEView _CONTROLLER_NAMEView);

        int GetCONTROLLER_NAMELength();
}
 
}
";
            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            txt = txt.Replace("CONTROLLER_NAME", CONTROLLER_NAME);
            Console.WriteLine(txt);
            return txt;
        }
        public static string GernerateAPIViewModel(string SOLUTION_NAME, string API_NAME, string CONTROLLER_NAME, List<Property> ScreenComponents)
        {

            string txt = @"using SOLUTION_NAME.Services.API_NAME.Domain.AggregatesModel.API_NAMEAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SOLUTION_NAME.Services.API_NAME.API.Application.Models;
namespace SOLUTION_NAME.Services.API_NAME.API.Application.Queries
{

 public class CONTROLLER_NAMEView  :ModelPaging
 {
[Key]
[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
public int? Id { get; set; }
";
            foreach (var item in ScreenComponents)
            {
                txt += $" public {item.InputtypeIDstr()} {item.FieldNameEnglish} {{get; set; }}\n";
            }

            txt += @"}
}
";




            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            txt = txt.Replace("CONTROLLER_NAME", CONTROLLER_NAME);
            Console.WriteLine(txt);
            return txt;
        }

        public static string GenerateModelPagingClass(string SOLUTION_NAME, string API_NAME)
        {
            StringBuilder sb = new StringBuilder("");


            sb.Append(@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOLUTION_NAME.Services.API_NAME.API.Application.Models
{
    public class ModelPaging
    {
        public int? pageNumber { get; set; }
        public int? pageSize { get; set; }
    }
}
");
            return sb.ToString().Replace("SOLUTION_NAME", SOLUTION_NAME)
                                .Replace("API_NAME", API_NAME);

        }
        public static string GenerateModelPageVmClass(string SOLUTION_NAME, string API_NAME)
        {
            StringBuilder sb = new StringBuilder("");


            sb.Append(@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOLUTION_NAME.Services.API_NAME.API.Application.Models
{
    public class PageVM
    {
        public int length { get; set; }
        public int? pageIndex { get; set; }
        public int? pageSize { get; set; }

        public IEnumerable<dynamic> data { get; set; }
    }
}

");
            return sb.ToString().Replace("SOLUTION_NAME", SOLUTION_NAME)
                                .Replace("API_NAME", API_NAME);

        }
        public static string GernerateIAPIntegrationEventService(string SOLUTION_NAME, string API_NAME)
        {

            string txt = @"using SOLUTION_NAME.BuildingBlocks.EventBus.Events;
using System;
using System.Threading.Tasks;

namespace SOLUTION_NAME.Services.API_NAME.API.Application.IntegrationEvents
{
 public interface IAPI_NAMEIntegrationEventService
 {
  Task PublishEventsThroughEventBusAsync(Guid transactionId);
  Task AddAndSaveEventAsync(IntegrationEvent evt);
 }
}

";

            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);


            Console.WriteLine(txt);
            return txt;
        }
        public static string GernerateAPIntegrationEventService(string SOLUTION_NAME, string API_NAME)
        {

            string txt = @"

using Microsoft.EntityFrameworkCore;
using SOLUTION_NAME.BuildingBlocks.EventBus.Abstractions;
using SOLUTION_NAME.BuildingBlocks.EventBus.Events;
using SOLUTION_NAME.BuildingBlocks.IntegrationEventLogEF;
using SOLUTION_NAME.BuildingBlocks.IntegrationEventLogEF.Services;
using SOLUTION_NAME.Services.API_NAME.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace SOLUTION_NAME.Services.API_NAME.API.Application.IntegrationEvents
{
 public class API_NAMEIntegrationEventService : IAPI_NAMEIntegrationEventService
 {
  private readonly Func<DbConnection, IIntegrationEventLogService>_integrationEventLogServiceFactory;
  private readonly IEventBus _eventBus;
  private readonly API_NAMEContext _API_NAMEContext;
  private readonly IIntegrationEventLogService _eventLogService;
  private readonly ILogger<API_NAMEIntegrationEventService>_logger;

  public API_NAMEIntegrationEventService(IEventBus eventBus,
API_NAMEContext API_NAMEContext,
IntegrationEventLogContext eventLogContext,
Func<DbConnection, IIntegrationEventLogService>integrationEventLogServiceFactory,
ILogger<API_NAMEIntegrationEventService>logger)
  {
_API_NAMEContext = API_NAMEContext ?? throw new ArgumentNullException(nameof(API_NAMEContext));
_integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
_eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
_eventLogService = _integrationEventLogServiceFactory(_API_NAMEContext.Database.GetDbConnection());
_logger = logger ?? throw new ArgumentNullException(nameof(logger));
  }

  public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
  {
var pendingLogEvents = await _eventLogService.RetrieveEventLogsPendingToPublishAsync(transactionId);

foreach (var logEvt in pendingLogEvents)
{
 _logger.LogInformation(""----- Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})"", logEvt.EventId, Program.AppName, logEvt.IntegrationEvent);

 try
 {
  await _eventLogService.MarkEventAsInProgressAsync(logEvt.EventId);
  _eventBus.Publish(logEvt.IntegrationEvent);
  await _eventLogService.MarkEventAsPublishedAsync(logEvt.EventId);
 }
 catch (Exception ex)
 {
  _logger.LogError(ex, ""ERROR publishing integration event: {IntegrationEventId} from {AppName}"", logEvt.EventId, Program.AppName);

  await _eventLogService.MarkEventAsFailedAsync(logEvt.EventId);
 }
}
  }

  public async Task AddAndSaveEventAsync(IntegrationEvent evt)
  {
_logger.LogInformation(""----- Enqueuing integration event {IntegrationEventId} to repository ({@IntegrationEvent})"", evt.Id, evt);

await _eventLogService.SaveEventAsync(evt, _API_NAMEContext.GetCurrentTransaction());
  }
 }
}
";
            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);


            Console.WriteLine(txt);
            return txt;
        }
        public static string GernerateIntegrationEvent(string SOLUTION_NAME, string API_NAME, string CONTROLLER_NAME, string ACTION_NAME, List<Property> ScreenComponents)
        {
            string txt = @"
    using SOLUTION_NAME.BuildingBlocks.EventBus.Events;

using System.Collections.Generic;
using GAEB.Services.API_NAME.Domain.AggregatesModel.API_NAMEAggregate;
namespace SOLUTION_NAME.Services.API_NAME.API.Application.IntegrationEvents.Events
{
  

    public class CONTROLLER_NAMEACTION_NAMEIntegrationEvent: IntegrationEvent
    {";
            foreach (var item in ScreenComponents)
            {
                txt += $" public   {item.InputtypeIDstr()} {item.FieldNameEnglish} {{get; set; }}\n";
            }


            txt += @"	public CONTROLLER_NAMEACTION_NAMEIntegrationEvent(";
            foreach (var item in ScreenComponents)
            {
                txt += item.InputtypeIDstr() + " _" + item.FieldNameEnglish + ",";

            }

            txt = txt.Remove(txt.Length - 1, 1);
            txt += "){";
            foreach (var item in ScreenComponents)
            {
                txt += item.FieldNameEnglish + "=_" + item.FieldNameEnglish + ";\n";
            }
            txt += "}}}";

            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            txt = txt.Replace("CONTROLLER_NAME", CONTROLLER_NAME);
            txt = txt.Replace("ACTION_NAME", ACTION_NAME);

            Console.WriteLine(txt);
            return txt;
        }

        public static string GernerateIntegrationEventHandling(string SOLUTION_NAME, string API_NAME, string CONTROLLER_NAME, string ACTION_NAME)
        {
            string txt = @"
using MediatR;
using SOLUTION_NAME.BuildingBlocks.EventBus.Abstractions;
using SOLUTION_NAME.BuildingBlocks.EventBus.Extensions;
using Microsoft.Extensions.Logging;
using SOLUTION_NAME.Services.API_NAME.API.Application.Commands;
using SOLUTION_NAME.Services.API_NAME.API.Application.IntegrationEvents.Events;
using Serilog.Context;
using System.Threading.Tasks;

namespace SOLUTION_NAME.Services.API_NAME.API.Application.IntegrationEvents.EventHandling
{
    public class CONTROLLER_NAMEACTION_NAMEIntegrationEventHandler : IIntegrationEventHandler<CONTROLLER_NAMEACTION_NAMEIntegrationEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CONTROLLER_NAMEACTION_NAMEIntegrationEventHandler> _logger;

        public CONTROLLER_NAMEACTION_NAMEIntegrationEventHandler(
            IMediator mediator,
            ILogger<CONTROLLER_NAMEACTION_NAMEIntegrationEventHandler> logger)
        {
            _mediator = mediator;
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }
      



        /// <summary>
        /// Event handler which confirms that the grace period
        /// has been completed and order will not initially be cancelled.
        /// Therefore, the order process continues for validation. 
        /// </summary>
        /// <param name=""event"">       
        /// </param>
        /// <returns></returns>
        public async Task Handle(CONTROLLER_NAMEACTION_NAMEIntegrationEvent @event)
        {
            using (LogContext.PushProperty(""IntegrationEventContext"", $""{@event.Id}-{Program.AppName}""))
            {
                _logger.LogInformation(""----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})"", @event.Id, Program.AppName, @event);

              //  var command = new CreateAPI_NAMEPermanentCommand(@event.EmpId);

                //_logger.LogInformation(
                //    ""----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})"",
                //    command.GetGenericTypeName(),
                //    nameof(command.EmpId),
                //    command.EmpId,
                //    command);

                //await _mediator.Send(command);
            }
        }
    }
}
";

            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            txt = txt.Replace("CONTROLLER_NAME", CONTROLLER_NAME);
            txt = txt.Replace("ACTION_NAME", ACTION_NAME);

            Console.WriteLine(txt);
            return txt;
        }

        public static string gernerateStartup(string SOLUTION_NAME, string API_NAME, List<string> controlersName)
        {
            string txt = @"using Autofac;
using Autofac.Extensions.DependencyInjection;
using global::SOLUTION_NAME.Services.API_NAME.API.Application.IntegrationEvents;
using global::SOLUTION_NAME.Services.API_NAME.API.Application.IntegrationEvents.Events;
using global::SOLUTION_NAME.Services.API_NAME.API.Infrastructure.Filters;
using SOLUTION_NAME.BuildingBlocks.EventBus;
using SOLUTION_NAME.BuildingBlocks.EventBus.Abstractions;
using SOLUTION_NAME.BuildingBlocks.EventBusRabbitMQ;
using SOLUTION_NAME.BuildingBlocks.IntegrationEventLogEF;
using SOLUTION_NAME.BuildingBlocks.IntegrationEventLogEF.Services;
using SOLUTION_NAME.Services.API_NAME.API.Infrastructure.AutofacModules;
using SOLUTION_NAME.Services.API_NAME.Infrastructure;
using SOLUTION_NAME.Services.API_NAME.API.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace SOLUTION_NAME.Services.API_NAME.API
{


 public class Startup
 {
  public Startup(IConfiguration configuration)
  {
Configuration = configuration;
  }

  public IConfiguration Configuration {get; }

  public virtual IServiceProvider ConfigureServices(IServiceCollection services)
  {
services

 .AddApplicationInsights(Configuration)
 .AddCustomMvc()
 .AddHealthChecks(Configuration)
 .AddCustomDbContext(Configuration)
 .AddCustomSwagger(Configuration)
 .AddCustomIntegrations(Configuration)
 .AddCustomConfiguration(Configuration)
 .AddEventBus(Configuration)
 .AddCustomAuthentication(Configuration);
//configure autofac

var container = new ContainerBuilder();
container.Populate(services);

container.RegisterModule(new MediatorModule());
container.RegisterModule(new ApplicationModule(Configuration[""ConnectionString""]));

return new AutofacServiceProvider(container.Build());
  }


  public void Configure(IApplicationBuilder app,  ILoggerFactory loggerFactory)
  {
//loggerFactory.AddAzureWebAppDiagnostics();
//loggerFactory.AddApplicationInsights(app.ApplicationServices, LogLevel.Trace);

var pathBase = Configuration[""PATH_BASE""];
if (!string.IsNullOrEmpty(pathBase))
{
 loggerFactory.CreateLogger<Startup>().LogDebug(""Using PATH BASE '{pathBase}'"", pathBase);
 app.UsePathBase(pathBase);
}


app.UseSwagger();
app.UseSwaggerUI(c =>
{
 c.SwaggerEndpoint($""{(!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json"", ""API_NAME.API V1"");
 c.OAuthClientId(""API_NAMEswaggerui"");
 c.OAuthAppName(""API_NAME Swagger UI"");
});

app.UseRouting();
app.UseCors(""CorsPolicy"");
ConfigureAuth(app);

app.UseEndpoints(endpoints =>
{
 endpoints.MapDefaultControllerRoute();
 endpoints.MapControllers();

 endpoints.MapHealthChecks(""/hc"", new HealthCheckOptions()
 {
  Predicate = _ =>true,
  ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
 });
 endpoints.MapHealthChecks(""/liveness"", new HealthCheckOptions
 {
  Predicate = r =>r.Name.Contains(""self"")
 });
});

ConfigureEventBus(app);
  }


  private void ConfigureEventBus(IApplicationBuilder app)
  {
var eventBus = app.ApplicationServices.GetRequiredService<BuildingBlocks.EventBus.Abstractions.IEventBus>();
";
            foreach (var xITEM in controlersName)
            {
                txt += @"
eventBus.Subscribe<xITEMCreatedIntegrationEvent, IIntegrationEventHandler<xITEMCreatedIntegrationEvent>>();
eventBus.Subscribe<xITEMUpdatedIntegrationEvent, IIntegrationEventHandler<xITEMUpdatedIntegrationEvent>>();

eventBus.Subscribe<xITEMDeletedIntegrationEvent, IIntegrationEventHandler<xITEMDeletedIntegrationEvent>>();


";
                txt = txt.Replace("xITEM", xITEM);
            }
            txt += @"
  }

 
  protected virtual void ConfigureAuth(IApplicationBuilder app)
  {
app.UseAuthentication();
app.UseAuthorization();
  }
 }

 static class CustomExtensionsMethods
 {
  public static IServiceCollection AddApplicationInsights(this IServiceCollection services, IConfiguration configuration)
  {
 

return services;
  }

  public static IServiceCollection AddCustomMvc(this IServiceCollection services)
  {

";

            foreach (var xITEM in controlersName)
            {
                txt += @"
// Add framework services.
services.AddControllers(options =>
{
 options.Filters.Add(typeof(HttpGlobalExceptionFilter));
})
 // Added for functional tests
 .AddApplicationPart(typeof( xITEMController ).Assembly)

 .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
;

";
                txt = txt.Replace("xITEM", xITEM);
            }

            txt += @"
services.AddCors(options =>
{
 options.AddPolicy(""CorsPolicy"",
  builder =>builder
  .SetIsOriginAllowed((host) =>true)
  .AllowAnyMethod()
  .AllowAnyHeader()
  .AllowCredentials());
});

return services;
  }

  public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
  {
var hcBuilder = services.AddHealthChecks();

hcBuilder.AddCheck(""self"", () =>HealthCheckResult.Healthy());

hcBuilder
 .AddSqlServer(
  configuration[""ConnectionString""],
  name: ""API_NAMEDB-check"",
  tags: new string[] {""API_NAMEdb"" });

 hcBuilder.AddRabbitMQ(
$""amqp://{configuration[""EventBusConnection""]}"",
name: ""API_NAME-rabbitmqbus-check"",
tags: new string[] {""rabbitmqbus"" });


return services;
  }

  public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
  {
services.AddDbContext<API_NAMEContext>(options =>
{
 options.UseSqlServer(configuration[""ConnectionString""],
  sqlServerOptionsAction: sqlOptions =>
  {
sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
  });
},
  ServiceLifetime.Scoped  //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
 );

services.AddDbContext<IntegrationEventLogContext>(options =>
{
 options.UseSqlServer(configuration[""ConnectionString""],
 sqlServerOptionsAction: sqlOptions =>
 {
  sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
  //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
  sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
 });
});

return services;
  }

  public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
  {
services.AddSwaggerGen(options =>
{
 options.DescribeAllEnumsAsStrings();

 options.SwaggerDoc(""v1"", new OpenApiInfo
 {
  Title = ""SOLUTION_NAME - API_NAME HTTP API"",
  Version = ""v1"",
  Description = ""The API_NAME Service HTTP API""
 });
 //options.AddSecurityDefinition(""oauth2"", new OpenApiSecurityScheme
 //{
 // Type = SecuritySchemeType.OAuth2,
 // Flows = new OpenApiOAuthFlows()
 // {
 //  Implicit = new OpenApiOAuthFlow()
 //  {
 //AuthorizationUrl = new Uri($""{configuration.GetValue<string>(""IdentityUrlExternal"")}/connect/authorize""),
 //TokenUrl = new Uri($""{configuration.GetValue<string>(""IdentityUrlExternal"")}/connect/token""),
 //Scopes = new Dictionary<string, string>()
 //{
 // {""API_NAME"", ""API_NAME API"" }
 //}
 //  }
 // }
 //});

 //options.OperationFilter<AuthorizeCheckOperationFilter>();
});

return services;
  }

  public static IServiceCollection AddCustomIntegrations(this IServiceCollection services, IConfiguration configuration)
  {



                services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
 sp =>(DbConnection c) =>new IntegrationEventLogService(c));



                services.AddTransient<IAPI_NAMEIntegrationEventService, API_NAMEIntegrationEventService>();



           
         
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
 {
  var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();


  var factory = new ConnectionFactory()
  {
HostName = configuration[""EventBusConnection""],
DispatchConsumersAsync = true
  };

  if (!string.IsNullOrEmpty(configuration[""EventBusUserName""]))
  {
factory.UserName = configuration[""EventBusUserName""];
  }

  if (!string.IsNullOrEmpty(configuration[""EventBusPassword""]))
  {
factory.Password = configuration[""EventBusPassword""];
  }

  var retryCount = 5;
  if (!string.IsNullOrEmpty(configuration[""EventBusRetryCount""]))
  {
retryCount = int.Parse(configuration[""EventBusRetryCount""]);
  }

  return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
 });


return services;
  }

  public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
  {
services.AddOptions();
services.Configure<ApiBehaviorOptions>(options =>
{
 options.InvalidModelStateResponseFactory = context =>
 {
  var problemDetails = new ValidationProblemDetails(context.ModelState)
  {
Instance = context.HttpContext.Request.Path,
Status = StatusCodes.Status400BadRequest,
Detail = ""Please refer to the errors property for additional details.""
  };

  return new BadRequestObjectResult(problemDetails)
  {
ContentTypes = {""application/problem+json"", ""application/problem+xml"" }
  };
 };
});

return services;
  }

  public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
  {
var subscriptionClientName = configuration[""SubscriptionClientName""];


 services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
 {
  var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
  var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
  var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
  var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

  var retryCount = 5;
  if (!string.IsNullOrEmpty(configuration[""EventBusRetryCount""]))
  {
retryCount = int.Parse(configuration[""EventBusRetryCount""]);
  }

  return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
 });


services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

return services;
  }

  public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
  {
// prevent from mapping ""sub"" claim to nameidentifier.
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove(""sub"");

var identityUrl = configuration.GetValue<string>(""IdentityUrl"");


return services;
  }
 }
}
";

            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            Console.WriteLine(txt);
            return txt;
        }

        public static string gernerateProgram(string SOLUTION_NAME, string API_NAME)
        {
            string txt = @"using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;


namespace SOLUTION_NAME.Services.API_NAME.API
{
 public class Program
 {
  public static string Namespace = typeof(Startup).Namespace;
  public static string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);



  public static int Main(string[] args)
  {
var configuration = GetConfiguration();
Log.Logger = CreateSerilogLogger(configuration);


try
{
 Log.Information(""Configuring web host ({ApplicationContext})..."", Program.AppName);
 var host = BuildWebHost(configuration, args);

 LogPackagesVersionInfo();

 Log.Information(""Starting web host ({ApplicationContext})..."", Program.AppName);
 host.Run();

 return 0;
}
catch (Exception ex)
{
 Log.Fatal(ex, ""Program terminated unexpectedly ({ApplicationContext})!"", Program.AppName);
 return 1;
}
finally
{
 Log.CloseAndFlush();
}

  }

  static (int httpPort, int grpcPort) GetDefinedPorts(IConfiguration config)
  {
var grpcPort = config.GetValue(""GRPC_PORT"", 6001);
var port = config.GetValue(""PORT"", 80);
return (port, grpcPort);
  }
 private static IConfiguration GetConfiguration()
  {
var builder = new ConfigurationBuilder()
 .SetBasePath(Directory.GetCurrentDirectory())
 .AddJsonFile(""appsettings.json"", optional: false, reloadOnChange: true)
 .AddEnvironmentVariables();

var config = builder.Build();

//if (config.GetValue<bool>(""UseVault"", false))
//{
// builder.AddAzureKeyVault(
//  $""https://{config[""Vault:Name""]}.vault.azure.net/"",
//  config[""Vault:ClientId""],
//  config[""Vault:ClientSecret""]);
//}

return builder.Build();
  }

  private static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
  {
var seqServerUrl = configuration[""Serilog:SeqServerUrl""];
var logstashUrl = configuration[""Serilog:LogstashgUrl""];
return new LoggerConfiguration()
 .MinimumLevel.Verbose()
 .Enrich.WithProperty(""ApplicationContext"", Program.AppName)
 .Enrich.FromLogContext()
 .WriteTo.File(""webmvc.log.txt"", rollingInterval: RollingInterval.Day)
 .WriteTo.Elasticsearch().WriteTo.Elasticsearch(ConfigureElasticSink(configuration, ""Development""))
 .WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? ""http://seq"" : seqServerUrl)
 .WriteTo.Http(string.IsNullOrWhiteSpace(logstashUrl) ? ""http://logstash:8080"" : logstashUrl)
 .ReadFrom.Configuration(configuration)
 .CreateLogger();


  }

  private static ElasticsearchSinkOptions ConfigureElasticSink(IConfiguration configuration, string environment)
  {
return new ElasticsearchSinkOptions(new Uri(configuration[""Serilog:ElasticConfiguration""]))
{
 BufferCleanPayload = (failingEvent, statuscode, exception) =>
 {
  dynamic e = JObject.Parse(failingEvent);
  return JsonConvert.SerializeObject(new Dictionary<string, object>()
 {
  {""@timestamp"",e[""@timestamp""]},
  {""level"",""Error""},
  {""message"",""Error: ""+e.message},
  {""messageTemplate"",e.messageTemplate},
  {""failingStatusCode"", statuscode},
  {""failingException"", exception}
 });
 },
 MinimumLogEventLevel = LogEventLevel.Verbose,
 AutoRegisterTemplate = true,
 AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
 CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
 IndexFormat = $""{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(""."", ""-"")}-{environment?.ToLower().Replace(""."", ""-"")}-{DateTime.UtcNow:yyyy-MM}"",
 EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
EmitEventFailureHandling.WriteToFailureSink |
EmitEventFailureHandling.RaiseCallback
};
  }

  private static IWebHost BuildWebHost(IConfiguration configuration, string[] args) =>
  WebHost.CreateDefaultBuilder(args)
 .ConfigureKestrel(options =>
 {
  var ports = GetDefinedPorts(GetConfiguration());
  options.Listen(IPAddress.Any, ports.httpPort, listenOptions =>
  {
listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
  });

  options.Listen(IPAddress.Any, ports.grpcPort, listenOptions =>
  {
listenOptions.Protocols = HttpProtocols.Http2;
  });

 })
.CaptureStartupErrors(false)
.ConfigureAppConfiguration(x =>x.AddConfiguration(configuration))
.UseStartup<Startup>()
.UseContentRoot(Directory.GetCurrentDirectory())
.UseSerilog()
.Build();

  private static void LogPackagesVersionInfo()
  {
var assemblies = new List<Assembly>();

foreach (var dependencyName in typeof(Program).Assembly.GetReferencedAssemblies())
{
 try
 {
  // Try to load the referenced assembly...
  assemblies.Add(Assembly.Load(dependencyName));
 }
 catch
 {
  // Failed to load assembly. Skip it.
 }
}

var versionList = assemblies.Select(a =>$""-{a.GetName().Name} - {GetVersion(a)}"").OrderBy(value =>value);

Log.Logger.ForContext(""PackageVersions"", string.Join(""\n"", versionList)).Information(""Package versions ({ApplicationContext})"", Program.AppName);
  }
  private static string GetVersion(Assembly assembly)
  {
try
{
 return $""{assembly.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version} ({assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion.Split()[0]})"";
}
catch
{
 return string.Empty;
}
  }
 

 }
}

";
            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            Console.WriteLine(txt);
            return txt;
        }

        public static string gernerateAppsettings(string SOLUTION_NAME, string API_NAME)
        {
            string txt = @"{
  ""ConnectionString"": ""Server=.;Initial Catalog=EmployeeDb;Integrated Security=true"",
  ""IdentityUrl"": ""http://localhost:5101"",
  ""UseCustomizationData"": false,
  ""Serilog"": {
    ""SeqServerUrl"": ""http://localhost:5341"",
    ""LogstashgUrl"": null,
    ""ElasticConfiguration"": ""http://elasticsearch:9200"",
    ""MinimumLevel"": {
      ""Default"": ""Information"",
      ""Override"": {
        ""Microsoft"": ""Warning"",
        ""FADY"": ""Information"",
        ""System"": ""Warning""
      }
    }
  },
  ""AzureServiceBusEnabled"": false,
  ""SubscriptionClientName"": ""API_NAME"",
  ""GracePeriodTime"": ""1"",
  ""CheckUpdateTime"": ""30000"",
  ""ApplicationInsights"": {
    ""InstrumentationKey"": """"
  },
  ""EventBusRetryCount"": 5,
  ""EventBusConnection"": ""localhost"",
  ""UseVault"": false,
  ""Vault"": {
    ""Name"": ""SOLUTION_NAME"",
    ""ClientId"": ""your-clien-id"",
    ""ClientSecret"": ""your-client-secret""
  }
}



";
            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            Console.WriteLine(txt);
            return txt;


        }

        public static string gernerateAppsettingsDevelopment(string SOLUTION_NAME, string API_NAME)
        {
            string txt = @"{
  ""Logging"": {
 ""LogLevel"": {
""Default"": ""Debug"",
""System"": ""Information"",
""Microsoft"": ""Information""
 }
  }
}
";
            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            Console.WriteLine(txt);
            return txt;
        }

        public static string gerneratelaunchSettings(string SOLUTION_NAME, string API_NAME)
        {
            string txt = @"
{
  ""$schema"": ""http://json.schemastore.org/launchsettings.json"",
  ""iisSettings"": {
 ""windowsAuthentication"": false, 
 ""anonymousAuthentication"": true, 
 ""iisExpress"": {
""applicationUrl"": ""http://localhost:5004"",
""sslPort"": 0
 }
  },
  ""profiles"": {
 ""IIS Express"": {
""commandName"": ""IISExpress"",
""launchBrowser"": true,
""launchUrl"": ""swagger/index.html"",
""applicationUrl"": ""http://localhost:55103/"",
""environmentVariables"": {
  ""ASPNETCORE_ENVIRONMENT"": ""Development""
}
 },
 ""API_NAME.API"": {
""commandName"": ""Project"",
""launchBrowser"": true,
""launchUrl"": ""index.html"",
""applicationUrl"": ""http://localhost:55103/"",
""environmentVariables"": {
  ""ASPNETCORE_ENVIRONMENT"": ""Development""
}
 }
  }
}
";
            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            Console.WriteLine(txt);
            return txt;
        }
        public static string gernerateCsproj(string SOLUTION_NAME, string API_NAME)
        {
            string txt = @"<Project Sdk=""Microsoft.NET.Sdk.Web"">

 <PropertyGroup>
	 <TargetFramework>net5.0</TargetFramework>
<AspNetCoreHostingModel>InProcess</AspNetCoreHostingModel>
<RootNamespace>API_NAME.API</RootNamespace>
 </PropertyGroup>

 <ItemGroup>
	  <PackageReference Include=""AutoMapper"" Version=""10.1.1"" />
	  <PackageReference Include=""Dapper"" Version=""2.0.78"" />
	  <PackageReference Include=""FluentValidation"" Version=""8.0"" />
	  <PackageReference Include=""Swashbuckle.AspNetCore"" Version=""5.0.0-rc2"" />
	  <PackageReference Include=""Swashbuckle.AspNetCore.SwaggerUI"" Version=""5.0.0-rc2"" />
      <PackageReference Include=""AspNetCore.HealthChecks.Rabbitmq"" Version=""5.0.1"" />
      <PackageReference Include=""Autofac.Extensions.DependencyInjection"" Version=""7.1.0"" />
      <PackageReference Include=""Microsoft.OpenApi"" Version=""1.2.3"" />
	  <PackageReference Include=""AspNetCore.HealthChecks.UI"" Version=""2.2.32"" />
	  <PackageReference Include=""AspNetCore.HealthChecks.UI.Client"" Version=""2.2.4"" />
	  <PackageReference Include=""AspNetCore.HealthChecks.SqlServer"" Version=""2.2.1"" />
	  <PackageReference Include=""Serilog"" Version=""2.10.0"" />
	  <PackageReference Include=""System.IdentityModel.Tokens.Jwt"" Version=""6.8.0"" />
	  <PackageReference Include=""Google.Protobuf"" Version=""3.14.0"" />
	  <PackageReference Include=""Grpc.AspNetCore.Server"" Version=""2.34.0"" />
	  <PackageReference Include=""Grpc.Tools"" Version=""2.34.0"" PrivateAssets=""All"" />
	  <PackageReference Include=""Nancy"" Version=""2.0.0"" />
	  <PackageReference Include=""Serilog.AspNetCore"" Version=""3.4.0"" />
	  <PackageReference Include=""Serilog.Enrichers.Environment"" Version=""2.1.3"" />
	  <PackageReference Include=""Serilog.Settings.Configuration"" Version=""3.1.1-dev-00216"" />
	  <PackageReference Include=""Serilog.Sinks.Console"" Version=""4.0.0-dev-00834"" />
	  <PackageReference Include=""Serilog.Sinks.Elasticsearch"" Version=""8.4.1"" />
	  <PackageReference Include=""Serilog.Sinks.Http"" Version=""7.2.0"" />
	  <PackageReference Include=""Serilog.Sinks.Seq"" Version=""4.1.0-dev-00166"" />
  </ItemGroup>

 <ItemGroup>
<ProjectReference Include=""..\..\..\BuildingBlocks\EventBus\EventBusRabbitMQ\EventBusRabbitMQ.csproj"" />
<ProjectReference Include=""..\..\..\BuildingBlocks\EventBus\EventBus\EventBus.csproj"" />
<ProjectReference Include=""..\..\..\BuildingBlocks\EventBus\IntegrationEventLogEF\IntegrationEventLogEF.csproj"" />
<ProjectReference Include=""..\API_NAME.Domain\API_NAME.Domain.csproj"" />
<ProjectReference Include=""..\API_NAME.Infrastructure\API_NAME.Infrastructure.csproj"" />
 </ItemGroup>

 <ProjectExtensions><VisualStudio><UserProperties properties_4launchsettings_1json__JsonSchema="""" /></VisualStudio></ProjectExtensions>

</Project>
";
            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            Console.WriteLine(txt);
            return txt;
        }

        public static string gernerateHttpGlobalExceptionFilter(string SOLUTION_NAME, string API_NAME)
        {
            string txt = @"
namespace SOLUTION_NAME.Services.API_NAME.API.Infrastructure.Filters
{
 using Microsoft.AspNetCore.Mvc;
 using global::SOLUTION_NAME.Services.API_NAME.Domain.Exceptions;
 using Microsoft.AspNetCore.Hosting;
 using Microsoft.AspNetCore.Http;
 using Microsoft.AspNetCore.Mvc.Filters;
 using Microsoft.Extensions.Logging;
 using System.Net;
 using SOLUTION_NAME.Services.API_NAME.API.Infrastructure.ActionResults;

 public class HttpGlobalExceptionFilter : IExceptionFilter
 {
  private readonly IHostingEnvironment env;
  private readonly ILogger<HttpGlobalExceptionFilter>logger;

  public HttpGlobalExceptionFilter(IHostingEnvironment env, ILogger<HttpGlobalExceptionFilter>logger)
  {
this.env = env;
this.logger = logger;
  }

  public void OnException(ExceptionContext context)
  {
logger.LogError(new EventId(context.Exception.HResult),
 context.Exception,
 context.Exception.Message);

if (context.Exception.GetType() == typeof(API_NAMEDomainException))
{
 var problemDetails = new ValidationProblemDetails()
 {
  Instance = context.HttpContext.Request.Path,
  Status = StatusCodes.Status400BadRequest,
  Detail = ""Please refer to the errors property for additional details.""
 };

 problemDetails.Errors.Add(""DomainValidations"", new string[] {context.Exception.Message.ToString() });

 context.Result = new BadRequestObjectResult(problemDetails);
 context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
}
else
{
 var json = new JsonErrorResponse
 {
  Messages = new[] {""An error occur.Try it again."" }
 };

 if (env.IsDevelopment())
 {
  json.DeveloperMessage = context.Exception;
 }

 // Result asigned to a result object but in destiny the response is empty. This is a known bug of .net core 1.1
 // It will be fixed in .net core 1.1.2. See https://github.com/aspnet/Mvc/issues/5594 for more information
 context.Result = new InternalServerErrorObjectResult(json);
 context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
}
context.ExceptionHandled = true;
  }

  private class JsonErrorResponse
  {
public string[] Messages {get; set; }

public object DeveloperMessage {get; set; }
  }
 }
}

";

            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            Console.WriteLine(txt);
            return txt;
        }

        public static string GernerateMediatorModule(string SOLUTION_NAME, string API_NAME, List<string> controlersName)
        {
            string txt = @"
using System.Linq;
using System.Reflection;
using Autofac;
using MediatR;
using SOLUTION_NAME.Services.API_NAME.API.Application.Commands;
using FluentValidation;
using SOLUTION_NAME.Services.API_NAME.API.Application.Validations;
using SOLUTION_NAME.Services.API_NAME.API.Application.Behaviors;


namespace SOLUTION_NAME.Services.API_NAME.API.Infrastructure.AutofacModules
{
 public class MediatorModule : Autofac.Module
 {
  protected override void Load(ContainerBuilder builder)
  {
builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
 .AsImplementedInterfaces();

// Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
";
            foreach (var xITEM in controlersName)
            {
                txt += @"
builder.RegisterAssemblyTypes(typeof(CreatexITEMCommand).GetTypeInfo().Assembly)
 .AsClosedTypesOf(typeof(IRequestHandler<,>));

//// Register the DomainEventHandler classes (they implement INotificationHandler<>) in assembly holding the Domain Events
//builder.RegisterAssemblyTypes(typeof(ValidateOrAddBuyerAggregateWhenOrderStartedDomainEventHandler).GetTypeInfo().Assembly)
// .AsClosedTypesOf(typeof(INotificationHandler<>));

//// Register the Command's Validators (Validators based on FluentValidation library)
builder
 .RegisterAssemblyTypes(typeof(CreatexITEMValidator).GetTypeInfo().Assembly)
 .Where(t =>t.IsClosedTypeOf(typeof(IValidator<>)))
 .AsImplementedInterfaces();


";
                txt = txt.Replace("xITEM", xITEM);

            }




            txt += @"




builder.Register<ServiceFactory>(context =>
{
 var componentContext = context.Resolve<IComponentContext>();
 return t =>{object o; return componentContext.TryResolve(t, out o) ? o : null; };
});

builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
builder.RegisterGeneric(typeof(TransactionBehaviour<,>)).As(typeof(IPipelineBehavior<,>));

  }
 }
}

";
            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            Console.WriteLine(txt);
            return txt;
        }
        public static string GernerateApplicationModule(string SOLUTION_NAME, string API_NAME, List<string> CONTROLERS_NAME)
        {
            string txt = @"
using Autofac;
using SOLUTION_NAME.Services.API_NAME.API.Application.Commands;
using SOLUTION_NAME.Services.API_NAME.API.Application.Queries;
using SOLUTION_NAME.Services.API_NAME.Domain.AggregatesModel.API_NAMEAggregate;

using SOLUTION_NAME.Services.API_NAME.Infrastructure.Repository;
using System.Reflection;

namespace SOLUTION_NAME.Services.API_NAME.API.Infrastructure.AutofacModules
{

 public class ApplicationModule
  :Autofac.Module
 {

  public string QueriesConnectionString {get; }

  public ApplicationModule(string qconstr)
  {
QueriesConnectionString = qconstr;

  }

  protected override void Load(ContainerBuilder builder)
  {
";
            foreach (var xITEM in CONTROLERS_NAME)
            {
                txt += @"
builder.Register(c =>new xITEMQueries(QueriesConnectionString))
.As<IxITEMQueries>()
 .InstancePerLifetimeScope();
builder.RegisterType<xITEMRepository>()
 .As<IxITEMRepository>()
 .InstancePerLifetimeScope();

";

                txt = txt.Replace("xITEM", xITEM);
            }


            txt += @"



//builder.RegisterAssemblyTypes(typeof(CreateAPI_NAMECommandHandler).GetTypeInfo().Assembly)
// .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

  }
 }
}

";

            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            Console.WriteLine(txt);
            return txt;
        }

        public static string GernerateCreateAPIValidator(string SOLUTION_NAME, string API_NAME, string CONTROLER_NAME, string ACTION_NAME)
        {
            string txt = @"
using FluentValidation;
using SOLUTION_NAME.Services.API_NAME.API.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOLUTION_NAME.Services.API_NAME.API.Application.Validations
{
    public class ACTION_NAMECONTROLER_NAMEValidator : AbstractValidator<ACTION_NAMECONTROLER_NAMECommand>
    {
        public ACTION_NAMECONTROLER_NAMEValidator()
        {
            RuleFor(command => command.Name).MinimumLength(6).WithMessage(""The CONTROLER_NAME Name Must be greater than 6"");
        }
    }
}

";
            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            txt = txt.Replace("CONTROLER_NAME", CONTROLER_NAME);
            txt = txt.Replace("ACTION_NAME", ACTION_NAME);

            Console.WriteLine(txt);
            return txt;
        }

        public static string GernerateInternalServerErrorObjectResult(string SOLUTION_NAME, string API_NAME)
        {
            string txt = @"
namespace SOLUTION_NAME.Services.API_NAME.API.Infrastructure.ActionResults
{
 using Microsoft.AspNetCore.Http;
 using Microsoft.AspNetCore.Mvc;

 public class InternalServerErrorObjectResult : ObjectResult
 {
  public InternalServerErrorObjectResult(object error)
: base(error)
  {
StatusCode = StatusCodes.Status500InternalServerError;
  }
 }
}
";
            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            Console.WriteLine(txt);
            return txt;
        }

        public static string GernerateJsonErrorResponse(string SOLUTION_NAME, string API_NAME)
        {
            string txt = @"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOLUTION_NAME.Services.API_NAME.API.Application.Validations
{
 public class JsonErrorResponse
 {
  public string[] Messages {get; set; }

  public object DeveloperMessage {get; set; }
 }
}

";

            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            Console.WriteLine(txt);
            return txt;
        }
        public static string GernerateValidateModelStateFilter(string SOLUTION_NAME, string API_NAME)
        {
            string txt = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace SOLUTION_NAME.Services.API_NAME.API.Application.Validations
{
 public class ValidateModelStateFilter : ActionFilterAttribute
 {
  public override void OnActionExecuting(ActionExecutingContext context)
  {
if (context.ModelState.IsValid)
{
 return;
}

var validationErrors = context.ModelState
 .Keys
 .SelectMany(k =>context.ModelState[k].Errors)
 .Select(e =>e.ErrorMessage)
 .ToArray();

var json = new JsonErrorResponse
{
 Messages = validationErrors
};

context.Result = new BadRequestObjectResult(json);
  }
 }
}
";
            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            Console.WriteLine(txt);
            return txt;
        }

        public static string GernerateLoggingBehavior(string SOLUTION_NAME, string API_NAME)
        {
            string txt = @"
using MediatR;
using SOLUTION_NAME.BuildingBlocks.EventBus.Extensions;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace SOLUTION_NAME.Services.API_NAME.API.Application.Behaviors
{
 public class LoggingBehavior<TRequest, TResponse>: IPipelineBehavior<TRequest, TResponse>
 {
  private readonly ILogger<LoggingBehavior<TRequest, TResponse>>_logger;
  public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>>logger) =>_logger = logger;

  public async Task<TResponse>Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse>next)
  {
_logger.LogInformation(""----- Handling command {CommandName} ({@Command})"", request.GetGenericTypeName(), request);
var response = await next();
_logger.LogInformation(""----- Command {CommandName} handled - response: {@Response}"", request.GetGenericTypeName(), response);

return response;
  }
 }
}

";
            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            Console.WriteLine(txt);
            return txt;
        }

        public static string GernerateTransactionBehaviour(string SOLUTION_NAME, string API_NAME)
        {
            string txt = @"
using MediatR;
using Microsoft.EntityFrameworkCore;
using SOLUTION_NAME.BuildingBlocks.EventBus.Extensions;
using Microsoft.Extensions.Logging;
using SOLUTION_NAME.Services.API_NAME.API.Application.IntegrationEvents;
using Serilog.Context;
using System;
using System.Threading;
using System.Threading.Tasks;
using SOLUTION_NAME.Services.API_NAME.Infrastructure;

namespace SOLUTION_NAME.Services.API_NAME.API.Application.Behaviors
{
 public class TransactionBehaviour<TRequest, TResponse>: IPipelineBehavior<TRequest, TResponse>
 {
  private readonly ILogger<TransactionBehaviour<TRequest, TResponse>>_logger;
  private readonly API_NAMEContext _dbContext;
  private readonly IAPI_NAMEIntegrationEventService _API_NAMEIntegrationEventService;

  public TransactionBehaviour(API_NAMEContext dbContext,
IAPI_NAMEIntegrationEventService API_NAMEIntegrationEventService,
ILogger<TransactionBehaviour<TRequest, TResponse>>logger)
  {
_dbContext = dbContext ?? throw new ArgumentException(nameof(API_NAMEContext));
_API_NAMEIntegrationEventService = API_NAMEIntegrationEventService ?? throw new ArgumentException(nameof(API_NAMEIntegrationEventService));
_logger = logger ?? throw new ArgumentException(nameof(ILogger));
  }

  public async Task<TResponse>Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse>next)
  {
var response = default(TResponse);
var typeName = request.GetGenericTypeName();

try
{
 if (_dbContext.HasActiveTransaction)
 {
  return await next();
 }

 var strategy = _dbContext.Database.CreateExecutionStrategy();

 await strategy.ExecuteAsync(async () =>
 {
  Guid transactionId;

  using (var transaction = await _dbContext.BeginTransactionAsync())
  using (LogContext.PushProperty(""TransactionContext"", transaction.TransactionId))
  {
_logger.LogInformation(""----- Begin transaction {TransactionId} for {CommandName} ({@Command})"", transaction.TransactionId, typeName, request);

response = await next();

_logger.LogInformation(""----- Commit transaction {TransactionId} for {CommandName}"", transaction.TransactionId, typeName);

await _dbContext.CommitTransactionAsync(transaction);

transactionId = transaction.TransactionId;
  }

  await _API_NAMEIntegrationEventService.PublishEventsThroughEventBusAsync(transactionId);
 });

 return response;
}
catch (Exception ex)
{
 _logger.LogError(ex, ""ERROR Handling transaction for {CommandName} ({@Command})"", typeName, request);

 throw;
}
  }
 }
}
";
            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            Console.WriteLine(txt);
            return txt;
        }
        public static string GernerateValidatorBehavior(string SOLUTION_NAME, string API_NAME)
        {
            string txt = @"
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SOLUTION_NAME.Services.API_NAME.API.Application.Behaviors
{
 public class ValidatorBehavior<TRequest, TResponse>: IPipelineBehavior<TRequest, TResponse>where TRequest : IRequest<TResponse>
 {
  private readonly IEnumerable<IValidator<TRequest>>_validators;

  public ValidatorBehavior(IEnumerable<IValidator<TRequest>>validators)
  {
_validators = validators;
  }

  public Task<TResponse>Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse>next)
  {
var context = new ValidationContext(request);
var failures = _validators
 .Select(v =>v.Validate(context))
 .SelectMany(result =>result.Errors)
 .Where(f =>f != null)
 .ToList();

if (failures.Count != 0)
{
 throw new ValidationException(failures);
}

return next();
  }


 }
}";
            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            Console.WriteLine(txt);
            return txt;
        }

        public static string GernerateAPIContextSeed(string SOLUTION_NAME, string API_NAME)
        {
            string txt = @"
namespace SOLUTION_NAME.Services.Employee.API.Infrastructure
{

   using Microsoft.AspNetCore.Hosting;
   using Microsoft.EntityFrameworkCore;
   using SOLUTION_NAME.Services.API_NAME.Domain.AggregatesModel.API_NAMEAggregate;

   using SOLUTION_NAME.Services.Employee.Domain.SeedWork;
   using Microsoft.Extensions.Logging;
   using Microsoft.Extensions.Options;
   using API_NAME.Infrastructure;
   using Polly;
   using Polly.Retry;
   using System;
   using System.Collections.Generic;
   using System.Data.SqlClient;
   using System.IO;
   using System.Linq;
   using System.Threading.Tasks;
   using SOLUTION_NAME.API_NAME.API;

   public class API_NAMEContextSeed
   {
       public async Task SeedAsync(API_NAMEContext context, IWebHostEnvironment env, IOptions<API_NAMESettings> settings, ILogger<API_NAMEContextSeed> logger)
       {
       }

   

       private string[] GetHeaders(string[] requiredHeaders, string csvfile)
       {
           string[] csvheaders = File.ReadLines(csvfile).First().ToLowerInvariant().Split(',');

           if (csvheaders.Count() != requiredHeaders.Count())
           {
               throw new Exception($""requiredHeader count '{ requiredHeaders.Count()}' is different then read header '{csvheaders.Count()}'"");
           }

           foreach (var requiredHeader in requiredHeaders)
           {
               if (!csvheaders.Contains(requiredHeader))
               {
                   throw new Exception($""does not contain required header '{requiredHeader}'"");
               }
           }

           return csvheaders;
       }


       private AsyncRetryPolicy CreatePolicy(ILogger<API_NAMEContextSeed> logger, string prefix, int retries = 3)
       {
           return Policy.Handle<SqlException>().
               WaitAndRetryAsync(
                   retryCount: retries,
                   sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                   onRetry: (exception, timeSpan, retry, ctx) =>
                   {
                       logger.LogWarning(exception, ""[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}"", prefix, exception.GetType().Name, exception.Message, retry, retries);
                   }
               );
       }
   }
}

";
            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            Console.WriteLine(txt);
            return txt;
        }

        public static string GernerateAPISettings(string SOLUTION_NAME, string API_NAME)
        {
            string txt = @"
namespace SOLUTION_NAME.API_NAME.API
{
   public class API_NAMESettings
   {
       public bool UseCustomizationData { get; set; }

       public string ConnectionString { get; set; }

       public string EventBusConnection { get; set; }

   }
}

";
            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            Console.WriteLine(txt);
            return txt;


        }


        public static string GernerateIdentityService(string SOLUTION_NAME, string API_NAME)
        {
            string txt = @"
using Microsoft.AspNetCore.Http;
using System;

namespace SOLUTION_NAME.Services.API_NAME.API.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private IHttpContextAccessor _context;

        public IdentityService(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public string GetUserIdentity()
        {
            return _context.HttpContext.User.FindFirst(""sub"").Value;
        }

        public string GetUserName()
        {
            return _context.HttpContext.User.Identity.Name;
        }
    }
}
";
            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            Console.WriteLine(txt);
            return txt;
        }

        public static string GernerateIIdentityService(string SOLUTION_NAME, string API_NAME)
        {
            String txt = @"
namespace SOLUTION_NAME.Services.API_NAME.API.Infrastructure.Services
{
    public interface IIdentityService
    {
        string GetUserIdentity();

        string GetUserName();
    }
}

";
            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            Console.WriteLine(txt);
            return txt;
        }
    }
}