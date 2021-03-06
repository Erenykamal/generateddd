using System;
using System.Collections.Generic;
using System.Text;
using Humanizer;
namespace GenerateBackendService
{
    public static class InfratStructureContent
    {
        public static string gernerateMediatorExtension(string SOLUTION_NAME, string API_NAME)
        {
            string txt = $@"
                using SOLUTION_NAME.Services.API_NAME.Domain.SeedWork;
                using MediatR;
                using System;
                using System.Collections.Generic;
                using System.Linq;
                using System.Text;
                using System.Threading.Tasks;

                namespace SOLUTION_NAME.Services.API_NAME.Infrastructure
                {{
                    static class MediatorExtension
                    {{
                        public static async Task DispatchDomainEventsAsync(this IMediator mediator, API_NAMEContext ctx)
                        {{
                            var domainEntities = ctx.ChangeTracker
                                .Entries<Entity>()
                                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

                            var domainEvents = domainEntities
                                .SelectMany(x => x.Entity.DomainEvents)
                                .ToList();

                            domainEntities.ToList()
                                .ForEach(entity => entity.Entity.ClearDomainEvents());

                            var tasks = domainEvents
                                .Select(async (domainEvent) => {{
                                    await mediator.Publish(domainEvent);
                                }});

                            await Task.WhenAll(tasks);
                        }}
                    }}
                }}

            ";

            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            Console.WriteLine(txt);
            return txt;
        }
        public static string gernerateContext(string SOLUTION_NAME, string API_NAME, List<string> controllers)
        {
            string txt = $@"
using SOLUTION_NAME.Services.API_NAME.Domain.SeedWork;
using SOLUTION_NAME.Services.Employee.Infrastructure.EntityConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data;

namespace SOLUTION_NAME.Services.API_NAME.Infrastructure
{{
    public class API_NAMEContext : DbContext, IUnitOfWork
    {{
        public const string DEFAULT_SCHEMA = ""API_NAME"";
       ";
            foreach (var item in controllers)
            {
                txt += $@"public DbSet<API_NAME.Domain.AggregatesModel.API_NAMEAggregate.{item}> {item.Pluralize()} {{ get; set; }}" + System.Environment.NewLine;

            }

            txt += $@"
        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;

        private API_NAMEContext(DbContextOptions<API_NAMEContext> options) : base(options) {{ }}

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;

        public API_NAMEContext(DbContextOptions<API_NAMEContext> options, IMediator mediator) : base(options)
        {{
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));


            System.Diagnostics.Debug.WriteLine(""API_NAMEContext::ctor->"" + this.GetHashCode());
        }} 



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {{";

            foreach (var item in controllers)
            {
                txt += $@"modelBuilder.ApplyConfiguration(new {item}EntityTypeConfiguration());" + System.Environment.NewLine;
            }
            txt += $@"}}";

                       txt += $@"
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {{
            
            await _mediator.DispatchDomainEventsAsync(this);
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }} 



        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {{
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }}

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {{
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($""Transaction {{transaction.TransactionId}} is not current"");

            try
            {{
                await SaveChangesAsync();
                transaction.Commit();
            }}
            catch
            {{
                RollbackTransaction();
                throw;
            }}
            finally
            {{
                if (_currentTransaction != null)
                {{
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }}
            }}
        }}

        public void RollbackTransaction()
        {{
            try
            {{
                _currentTransaction?.Rollback();
            }}
            finally
            {{
                if (_currentTransaction != null)
                {{
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }}
            }}
        }}
   
}}
    public class API_NAMEContextDesignFactory : IDesignTimeDbContextFactory<API_NAMEContext>
    {{
        public API_NAMEContext CreateDbContext(string[] args)
        {{
            var optionsBuilder = new DbContextOptionsBuilder<API_NAMEContext>().UseSqlServer(""Server=.;Initial Catalog=API_NAMEDb;Integrated Security=true"");

            return new API_NAMEContext(optionsBuilder.Options, new NoMediator());

        }}
    }}



    class NoMediator : IMediator
    {{


        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default(CancellationToken)) where TNotification : INotification
        {{
            return Task.CompletedTask;
        }}

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
        {{
            return Task.FromResult<TResponse>(default(TResponse));
        }}

        public Task Send(IRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {{
            return Task.CompletedTask;
        }}
        public Task Publish(object notification, CancellationToken cancellationToken = default(CancellationToken))
        {{
            throw new NotImplementedException();
        }}
    }}

}}

";

            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            return txt;
        }
        public static string gernerateRepository(string SOLUTION_NAME, string API_NAME, string ENTITY_NAME)
        {
            string txt = $@"
                using SOLUTION_NAME.Services.API_NAME.Domain.AggregatesModel.API_NAMEAggregate;
            using System;
            using System.Collections.Generic;
            using System.Text;
            using SOLUTION_NAME.Services.Employee.Domain.SeedWork;
            using System.Threading.Tasks;
            using Microsoft.EntityFrameworkCore;

namespace SOLUTION_NAME.Services.API_NAME.Infrastructure.Repository
    {{
        public class ENTITY_NAMERepository : IENTITY_NAMERepository
        {{


            private readonly API_NAMEContext _context;

            public IUnitOfWork UnitOfWork
            {{
                get
                {{
                    return _context;
                }}
            }}

            public ENTITY_NAMERepository(API_NAMEContext context)
            {{
                _context = context ?? throw new ArgumentNullException(nameof(context));

            }}
            public Domain.AggregatesModel.API_NAMEAggregate.ENTITY_NAME Add(Domain.AggregatesModel.API_NAMEAggregate.ENTITY_NAME ENTITY_NAME)
            {{
                return _context.{ENTITY_NAME.Pluralize()}.Add(ENTITY_NAME).Entity;
            }}

            public async Task<Domain.AggregatesModel.API_NAMEAggregate.ENTITY_NAME> GetAsync(int ENTITY_NAMEId)
            {{
                var ENTITY_NAME = await _context.{ENTITY_NAME.Pluralize()}.FindAsync(ENTITY_NAMEId);
              

                return ENTITY_NAME;
            }}

            public void Update(Domain.AggregatesModel.API_NAMEAggregate.ENTITY_NAME ENTITY_NAME)
            {{
                _context.Entry(ENTITY_NAME).State = EntityState.Modified;
            }}

            public void Delete(int ENTITY_NAMEId)
            {{
                var ENTITY_NAME = _context.{ENTITY_NAME.Pluralize()}.Find(ENTITY_NAMEId);
                if (ENTITY_NAME != null)
                {{
                    _context.Entry(ENTITY_NAME).State = EntityState.Deleted;
                }}
            }}
        }}
    }}

";

            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            txt = txt.Replace("ENTITY_NAME", ENTITY_NAME);

            return txt;
        }
        public static string gernerateEntityTypeConfiguration(string SOLUTION_NAME, string API_NAME, string ENTITY_NAME,List<Property>props)
        {
            string txt = @"
            using SOLUTION_NAME.Services.API_NAME.Domain.SeedWork;
            using Microsoft.EntityFrameworkCore;

            using Microsoft.EntityFrameworkCore.Storage;
            using System;
            using System.Collections.Generic;
            using System.Text;
            using System.Threading;
            using System.Threading.Tasks;
            using SOLUTION_NAME.Services.API_NAME.Domain.AggregatesModel.API_NAMEAggregate;
            using Microsoft.EntityFrameworkCore.Metadata.Builders;

            namespace SOLUTION_NAME.Services.API_NAME.Infrastructure.EntityConfigurations
            {
               public class ENTITY_NAMEEntityTypeConfiguration: IEntityTypeConfiguration<API_NAME.Domain.AggregatesModel.API_NAMEAggregate.ENTITY_NAME>
                {
                    public void Configure(EntityTypeBuilder<Domain.AggregatesModel.API_NAMEAggregate.ENTITY_NAME> ENTITY_NAMEConfiguration)
                    {
         
                        ENTITY_NAMEConfiguration.HasKey(o => o.Id);
        
                        ENTITY_NAMEConfiguration.Ignore(b => b.DomainEvents);

                    //   ENTITY_NAMEConfiguration.OwnsOne(o => o.Address,
                    //        sa =>
                    //        {
                    //            sa.Property(p => p.Street).HasColumnName(""Address_Street"");
                    //            sa.Property(p => p.City).HasColumnName(""Address_City"");
                    //    sa.Property(p => p.Country).HasColumnName(""Address_Country"");
                    //});
";
            foreach (var item in props)
            {
                if (item.ReadFromModuleID!=null) {
                   txt+= $@"ENTITY_NAMEConfiguration
        .HasMany(b => b.{item.FieldNameEnglish})" + Environment.NewLine;
                }

            }

               

            foreach (var item in props)
            {
                txt += $@"ENTITY_NAMEConfiguration.Property(o => o.{item.FieldNameEnglish})" + Environment.NewLine;
                if (item.ValidationProperty !=null)
                    foreach (var validate in item.ValidationProperty)
                    {
                        txt += $@".{validate}" + Environment.NewLine;

                    }
                txt += @";";
            }
           

                  
            txt+=@";
            }


         }

 }";

            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            txt = txt.Replace("ENTITY_NAME", ENTITY_NAME);

            return txt;
        }
        public static string gernerateCsProj(string SOLUTION_NAME, string API_NAME, string ENTITY_NAME)
        {
            string txt = $@"
<Project Sdk= ""Microsoft.NET.Sdk"">

<PropertyGroup>

<TargetFramework>net5.0</TargetFramework>
   

   <RootNamespace>API_NAME.Infrastructure</RootNamespace>
   

   </PropertyGroup>
   


   <ItemGroup>
   

   <Compile Remove = ""Repository\ENTITY_NAMERepository.cs"" />
    

    </ItemGroup>
    



    <ItemGroup>

	  <PackageReference Include=""MediatR"" Version=""7.0.0"" />
	   <PackageReference Include=""Microsoft.EntityFrameworkCore"" Version=""5.0.2"" />
	  <PackageReference Include=""Microsoft.EntityFrameworkCore.Design"" Version=""5.0.2"" />
	    <PackageReference Include=""Microsoft.EntityFrameworkCore.SqlServer"" Version=""5.0.2"" />
	  <PackageReference Include=""Microsoft.EntityFrameworkCore.Relational"" Version=""5.0.2"" />
	 	  <PackageReference Include=""Microsoft.EntityFrameworkCore.Tools"" Version=""5.0.0"">
        <PrivateAssets> all </PrivateAssets>

        <IncludeAssets> runtime; build; native; contentfiles; analyzers; buildtransitive </IncludeAssets>
   
         </PackageReference>
   

     </ItemGroup>
        


        <ItemGroup>
        

        <ProjectReference Include = ""..\API_NAME.Domain\API_NAME.Domain.csproj"" />
         

         </ItemGroup>
         




         </Project>

                  ";

            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            txt = txt.Replace("ENTITY_NAME", ENTITY_NAME);

            return txt;
        }
    }
}
