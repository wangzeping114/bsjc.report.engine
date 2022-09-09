using System;
using System.Collections.Generic;
using Bogus;
using N6.Bsjc.Reporting.Domain.DataSource;
using N6.Bsjc.Reporting.Domain.Shared.Emuns;
using N6.Bsjc.Reporting.Domain.Shared.Extends;
using N6.Bsjc.Reporting.Domain.Shared.SourceParameters;
using N6.Bsjc.Reporting.Domain.Test.Tests.Dto;
using Newtonsoft.Json;
using Shouldly;
using Volo.Abp.Json;
using Xunit;

namespace N6.Bsjc.Reporting.Domain.Test.Tests
{
	public class WebDesignerTests : ReportDomainTestBase
	{
		private readonly IDataSourceFactory _dataSourceFactory;
		private readonly IJsonSerializer _jsonSerializer;
		public WebDesignerTests()
		{
			_dataSourceFactory = GetRequiredService<IDataSourceFactory>();
			_jsonSerializer = GetRequiredService<IJsonSerializer>();
		}

		[Fact]
		public void Should_Set_ReportDataSourceType_ObjectType()
		{
			var persons = new TestPerson().BatchPopulate(5);
			var personsAsJson = JsonConvert.SerializeObject(persons);
			var faker = new Faker<ReportDefineTestDto>()
				   .RuleFor(c => c.Id, Guid.NewGuid())
				   .RuleFor(c => c.ReportDataSourceType, ReportDataSourceType.Object)
				   .RuleFor(c => c.ReportSourceName, f => f.Person.FullName)
				   .RuleFor(c => c.ReportDataSourceValue, f => personsAsJson);
			var dto = faker.Generate();
			var dataSrouceFactory = _dataSourceFactory.GetDataSourceProvider(dto.ReportDataSourceType);
			var objectResult = dataSrouceFactory.CreateDataSrouce(dto.ReportSourceName, dto.ReportDataSourceValue);
			objectResult.ShouldNotBe(null);
		}

		[Fact]
		public void Should_Set_ReportDataSourceType_UriType()
		{
			var uriParmarters = new List<UriParameter>();
			uriParmarters.Add(new UriParameter
			{
				IsExpression = false,
				IsRequired = true,
				UriParameterType = UriParameterType.Query,
				Name = "SkipCount",
				ValueType=typeof(int),
				DefaultValue = "0",
			});
			uriParmarters.Add(new UriParameter
			{
				IsExpression = false,
				IsRequired = true,
				UriParameterType = UriParameterType.Query,
				ValueType=typeof(System.Int32),
				Name = "MaxResultCount",
				DefaultValue = "2",
			});

			var asUriParmaeterJson = uriParmarters.ToJson();

			var faker = new Faker<ReportDefineTestDto>()
				.RuleFor(c => c.Id, Guid.NewGuid())
				.RuleFor(c => c.ReportDataSourceType, ReportDataSourceType.Uri)
				.RuleFor(c => c.ReportSourceName, f => f.Person.FullName)
				.RuleFor(c => c.ReportDataSourceValue, f => "http://192.168.1.23:44392/api/Instrument/Instruments/list")
				.RuleFor(c => c.ReportParameterJson, f => asUriParmaeterJson);

			var dto = faker.Generate();
			var dataSrouceFactory = _dataSourceFactory.GetDataSourceProvider(dto.ReportDataSourceType);
			var objectResult = dataSrouceFactory.CreateDataSrouce(dto.ReportSourceName, dto.ReportDataSourceValue, dto.ReportParameterJson);
			objectResult.ShouldNotBe(null);
		}
	}
}
