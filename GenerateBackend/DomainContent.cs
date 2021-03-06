using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenerateBackendService
{
    public class DomainContent
    {
        public string generateValueObject(string SOLUTION_NAME, string API_NAME, string CLASS_NAME, List<Property> properties)
        {
            var txt = $@"
 using SOLUTION_NAME.Services.API_NAME.Domain.SeedWork;
 using System;
 using System.Collections.Generic;
 using System.Text;

 namespace SOLUTION_NAME.Services.API_NAME.Domain.AggregatesModel.API_NAMEAggregate
 {{
public class CLASS_NAME : ValueObject
  {{
";
            foreach (var property in properties)
            {
                if (property.Prefix == null)
                    txt += $@"public {property.InputtypeIDstr()} {property.FieldNameEnglish} {{get; set;}}" + System.Environment.NewLine;
                else
                    txt += $@"public {property.Prefix} {property.InputtypeIDstr()} {property.FieldNameEnglish} {{get; set;}}" + System.Environment.NewLine;


            }

            txt += @"
public CLASS_NAME()
{{}}

public CLASS_NAME(";
foreach (var property in properties)
            {
                txt += $@"{property.InputtypeIDstr()} _{ReplaceFirstCharacterToLowerVariant(property.FieldNameEnglish)}, ";
            }
            txt = txt.Remove(txt.Length - 2, 2);
            txt += $@")
{{
 ";
            foreach (var property in properties)
            {
                txt += $@"{property.FieldNameEnglish} = _{ReplaceFirstCharacterToLowerVariant(property.FieldNameEnglish)};" + System.Environment.NewLine;
            }
            txt+=$@"
}}

protected override IEnumerable<object>GetAtomicValues()
{{
 // Using a yield return statement to return each element one at a time
 " + GenerateAtomicValues(properties) + $@"
}}
  }}
 }}

";

            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            txt = txt.Replace("CLASS_NAME", CLASS_NAME);

            return txt;
        }

        public string GenerateAtomicValues(List<Property> properties)
        {
            StringBuilder txt = new StringBuilder();
            foreach (var property in properties)
            {
                txt.Append($@"yield return {property.FieldNameEnglish};" + System.Environment.NewLine);
            }
            return txt.ToString();

        }


        public static string GenerateAggregatesModel(string SOLUTION_NAME, string API_NAME, string CLASS_NAME, List<Property> properties)
        {
            var txt = $@"
                using SOLUTION_NAME.Services.API_NAME.Domain.SeedWork;
                using System;
                using System.Collections.Generic;
                using System.Text;

                namespace SOLUTION_NAME.Services.API_NAME.Domain.AggregatesModel.API_NAMEAggregate
                {{
                    public class CLASS_NAME : Entity, IAggregateRoot
                    {{
                        ";
            foreach (var property in properties)
            {
                if (property.Prefix == null)
                    txt += $@"public {property.InputtypeIDstr()} {property.FieldNameEnglish} {{get; set;}}" + System.Environment.NewLine;
                else
                    txt += $@"public {property.Prefix} {property.InputtypeIDstr()} {property.FieldNameEnglish} {{get; set;}}" + System.Environment.NewLine;


            }

        txt+= @"





            public CLASS_NAME()
                        {{
    
                        }}
                        public CLASS_NAME(";
            foreach (var property in properties)
            {
                txt += $@"{property.InputtypeIDstr()} _{ReplaceFirstCharacterToLowerVariant(property.FieldNameEnglish)}, ";
            }
            txt = txt.Remove(txt.Length - 2, 2);
            txt += $@")
                        {{
                            ";

            foreach (var property in properties)
            {
                txt += $@"{property.FieldNameEnglish} = _{ReplaceFirstCharacterToLowerVariant(property.FieldNameEnglish)};" + System.Environment.NewLine;
            }
            
            txt+=$@"
                        }}


                    }}
                }}

            ";

            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            txt = txt.Replace("CLASS_NAME", CLASS_NAME);

            return txt;
        }

        public static string  GenerateEntityRepository(string SOLUTION_NAME, string API_NAME, string CLASS_NAME)
        {
            var txt = $@"
 using SOLUTION_NAME.Services.API_NAME.Domain.SeedWork;
 using System;
 using System.Collections.Generic;
 using System.Text;
 using System.Threading.Tasks;

 namespace SOLUTION_NAME.Services.API_NAME.Domain.AggregatesModel.API_NAMEAggregate
 {{
  public interface ICLASS_NAMERepository : IRepository<CLASS_NAME>
  {{
CLASS_NAME Add(CLASS_NAME " + ReplaceFirstCharacterToLowerVariant(CLASS_NAME) + $@");

void Update(CLASS_NAME " + ReplaceFirstCharacterToLowerVariant(CLASS_NAME) + $@");

Task<CLASS_NAME>GetAsync(int " + ReplaceFirstCharacterToLowerVariant(CLASS_NAME) + $@");

void Delete(int " + ReplaceFirstCharacterToLowerVariant(CLASS_NAME) + $@");
  }}
 }}

";

            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            txt = txt.Replace("CLASS_NAME", CLASS_NAME);

            return txt;
        }


        public static string GeneratePermanentDomainEvent(string SOLUTION_NAME, string API_NAME, string className)
        {
            var txt = $@"
 using MediatR;
 using System;
 using System.Collections.Generic;
 using System.Text;
 using SOLUTION_NAME.Services.API_NAME.Domain.AggregatesModel.API_NAMEAggregate;

 namespace SOLUTION_NAME.Services.API_NAME.Domain.Events
 {{
  public class classNamePermanentDomainEvent : INotification
  {{
public SOLUTION_NAME.Services.API_NAME.Domain.AggregatesModel.API_NAMEAggregate.className _className{{get; }}

public classNamePermanentDomainEvent(SOLUTION_NAME.Services.API_NAME.Domain.AggregatesModel.API_NAMEAggregate.className className)
{{
 _className = className;
}}
  }}
 }}
";

            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            txt = txt.Replace("className", className);
            return txt;

        }

        public static string GenerateDomainException(string SOLUTION_NAME, string API_NAME)
        {
            var txt = $@"
 using System;
 using System.Collections.Generic;
 using System.Text;

 namespace SOLUTION_NAME.Services.API_NAME.Domain.Exceptions
 {{
  ///<summary>
  /// Exception type for domain exceptions
  ///</summary>
  public class API_NAMEDomainException : Exception
  {{
public API_NAMEDomainException()
{{}}

public API_NAMEDomainException(string message)
 : base(message)
{{}}

public API_NAMEDomainException(string message, Exception innerException)
 : base(message, innerException)
{{}}
  }}
 }}

";

            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            return txt;

        }


        public static string GenerateDomainCsProjMethod(string API_NAME)
        {
            string txt = $@"
<Project Sdk=""Microsoft.NET.Sdk"">
<PropertyGroup>
  <TargetFramework>net5.0</TargetFramework>
<RootNamespace>API_NAME.Domain</RootNamespace>
</PropertyGroup>
 <ItemGroup>
  <PackageReference Include = ""MediatR"" Version = ""7.0.0"" />
  </ItemGroup>  
</Project>";
            txt = txt.Replace("API_NAME", API_NAME);
            return txt;
        }


        public static string GenerateEntity(string SOLUTION_NAME, string API_NAME)
        {
            var txt = $@"
 using System;
 using MediatR;
 using System.Collections.Generic;

 namespace SOLUTION_NAME.Services.API_NAME.Domain.SeedWork
 {{
 
  public abstract class Entity
  {{
int? _requestedHashCode;
int _Id;  
public virtual  int Id 
{{
 get
 {{
  return _Id;
 }}
 protected set
 {{
  _Id = value;
 }}
}}

private List<INotification>_domainEvents;
public IReadOnlyCollection<INotification>DomainEvents =>_domainEvents?.AsReadOnly();

public void AddDomainEvent(INotification eventItem)
{{
 _domainEvents = _domainEvents ?? new List<INotification>();
 _domainEvents.Add(eventItem);
}}

public void RemoveDomainEvent(INotification eventItem)
{{
 _domainEvents?.Remove(eventItem);
}}

public void ClearDomainEvents()
{{
 _domainEvents?.Clear();
}}

public bool IsTransient()
{{
 return this.Id == default(Int32);
}}

public override bool Equals(object obj)
{{
 if (obj == null || !(obj is Entity))
  return false;

 if (Object.ReferenceEquals(this, obj))
  return true;

 if (this.GetType() != obj.GetType())
  return false;

 Entity item = (Entity)obj;

 if (item.IsTransient() || this.IsTransient())
  return false;
 else
  return item.Id == this.Id;
}}

public override int GetHashCode()
{{
 if (!IsTransient())
 {{
  if (!_requestedHashCode.HasValue)
_requestedHashCode = this.Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

  return _requestedHashCode.Value;
 }}
 else
  return base.GetHashCode();

}}
public static bool operator ==(Entity left, Entity right)
{{
 if (Object.Equals(left, null))
  return (Object.Equals(right, null)) ? true : false;
 else
  return left.Equals(right);
}}

public static bool operator !=(Entity left, Entity right)
{{
 return !(left == right);
}}
  }}
 }}

";

            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);

            return txt;
        }

        public static string GenerateEnumeration(string SOLUTION_NAME, string API_NAME)
        {
            var txt = $@"
 using System;
 using System.Collections.Generic;
 using System.Linq;
 using System.Reflection;

 namespace SOLUTION_NAME.Services.API_NAME.Domain.SeedWork
 {{
  public abstract class Enumeration : IComparable
  {{
public string Name {{get; private set; }}

public int Id {{get; private set; }}

protected Enumeration(int id, string name)
{{
 Id = id;
 Name = name;
}}

public override string ToString() =>Name;

public static IEnumerable<T>GetAll<T>() where T : Enumeration
{{
 var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

 return fields.Select(f =>f.GetValue(null)).Cast<T>();
}}

public override bool Equals(object obj)
{{
 var otherValue = obj as Enumeration;

 if (otherValue == null)
  return false;

 var typeMatches = GetType().Equals(obj.GetType());
 var valueMatches = Id.Equals(otherValue.Id);

 return typeMatches && valueMatches;
}}

public override int GetHashCode() =>Id.GetHashCode();

public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
{{
 var absoluteDifference = Math.Abs(firstValue.Id - secondValue.Id);
 return absoluteDifference;
}}

public static T FromValue<T>(int value) where T : Enumeration
{{
 var matchingItem = Parse<T, int>(value, ""value"", item =>item.Id == value);
return matchingItem;
  }}


 public static T FromDisplayName<T>(string displayName) where T : Enumeration
{{
 var matchingItem = Parse<T, string>(displayName, ""display name"", item =>item.Name == displayName);
 return matchingItem;
}}

private static T Parse<T, K>(K value, string description, Func<T, bool>predicate) where T : Enumeration
{{
 var matchingItem = GetAll<T>().FirstOrDefault(predicate);

 if (matchingItem == null)
  throw new InvalidOperationException($""'{{value}}' is not a valid {{description}} in {{typeof(T)}}"");

 return matchingItem;
}}

public int CompareTo(object other) =>Id.CompareTo(((Enumeration)other).Id);
  }}
 }}

";

            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);

            return txt;
        }

        public static string GenerateIAggregateRoot(string SOLUTION_NAME, string API_NAME)
        {
            var txt = $@"
 namespace SOLUTION_NAME.Services.API_NAME.Domain.SeedWork
 {{
 
  public interface IAggregateRoot {{}}

 }}  
";

            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            return txt;

        }

        public static string GenerateIRepository(string SOLUTION_NAME, string API_NAME)
        {
            var txt = $@"
 namespace SOLUTION_NAME.Services.API_NAME.Domain.SeedWork
 {{
  public interface IRepository<T>where T : IAggregateRoot
  {{
IUnitOfWork UnitOfWork {{get; }}
  }}
 }}
";

            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            return txt;
        }

        public static string GenerateIUnitOfWork(string SOLUTION_NAME, string API_NAME)
        {
            var txt = $@"
 using System;
 using System.Threading;
 using System.Threading.Tasks;

 namespace SOLUTION_NAME.Services.API_NAME.Domain.SeedWork
 {{
  public interface IUnitOfWork : IDisposable
  {{
Task<int>SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
Task<bool>SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
  }}
 }}
";

            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            return txt;
        }


        public static string GenerateValueObject(string SOLUTION_NAME, string API_NAME)
        {
            var txt = $@"
 using System.Collections.Generic;
 using System.Linq;

 namespace SOLUTION_NAME.Services.API_NAME.Domain.SeedWork
 {{
  public abstract class ValueObject
  {{
protected static bool EqualOperator(ValueObject left, ValueObject right)
{{
 if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
 {{
  return false;
 }}
 return ReferenceEquals(left, null) || left.Equals(right);
}}

protected static bool NotEqualOperator(ValueObject left, ValueObject right)
{{
 return !(EqualOperator(left, right));
}}

protected abstract IEnumerable<object>GetAtomicValues();

public override bool Equals(object obj)
{{
 if (obj == null || obj.GetType() != GetType())
 {{
  return false;
 }}
 ValueObject other = (ValueObject)obj;
 IEnumerator<object>thisValues = GetAtomicValues().GetEnumerator();
 IEnumerator<object>otherValues = other.GetAtomicValues().GetEnumerator();
 while (thisValues.MoveNext() && otherValues.MoveNext())
 {{
  if (ReferenceEquals(thisValues.Current, null) ^ ReferenceEquals(otherValues.Current, null))
  {{
return false;
  }}
  if (thisValues.Current != null && !thisValues.Current.Equals(otherValues.Current))
  {{
return false;
  }}
 }}
 return !thisValues.MoveNext() && !otherValues.MoveNext();
}}

public override int GetHashCode()
{{
 return GetAtomicValues()
  .Select(x =>x != null ? x.GetHashCode() : 0)
  .Aggregate((x, y) =>x ^ y);
}}

public ValueObject GetCopy()
{{
 return this.MemberwiseClone() as ValueObject;
}}
  }}
 }}

";

            txt = txt.Replace("SOLUTION_NAME", SOLUTION_NAME);
            txt = txt.Replace("API_NAME", API_NAME);
            return txt;
        }
        public static string ReplaceFirstCharacterToLowerVariant(string name)
        {
            return String.Format("{0}{1}", name.First().ToString().ToLowerInvariant(), name.Substring(1));
        }



    }
}
