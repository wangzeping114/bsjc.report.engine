using System.Collections.Generic;
using Bogus;
using Bogus.DataSets;

namespace N6.Bsjc.Reporting.Domain.Test.Tests.Dto
{
	public class TestPerson: Person
	{
		public TestPerson()
		{

		}

		public List<TestPerson> BatchPopulate(int number)
		{
			var testPersons = new List<TestPerson>();
			var index = 0;
			while (number > index)
			{
				testPersons.Add(Populate());
				index++;
			}
			return testPersons;
		}

		public new TestPerson Populate()
		{
			var person=new TestPerson();
			person.Gender = person.Random.Enum<Name.Gender>();
			person.FirstName = person.DsName.FirstName(person.Gender);
			person.LastName = person.DsName.LastName(person.Gender);
			person.FullName = $"{person.FirstName} {person.LastName}";

			person.UserName = person.DsInternet.UserName(person.FirstName, person.LastName);
			person.Email = person.DsInternet.Email(person.FirstName, person.LastName);
			person.Website = person.DsInternet.DomainName();
			person.Avatar = person.DsInternet.Avatar();

			person.DateOfBirth = person.DsDate.Past(50, Date.SystemClock().AddYears(-20));

			person.Phone = person.DsPhoneNumbers.PhoneNumber();

			person.Address = new CardAddress
			{
				Street = person.DsAddress.StreetAddress(),
				Suite = person.DsAddress.SecondaryAddress(),
				City = person.DsAddress.City(),
				State = person.DsAddress.State(),
				ZipCode = person.DsAddress.ZipCode(),
				Geo = new CardAddress.CardGeo
				{
					Lat = person.DsAddress.Latitude(),
					Lng = person.DsAddress.Longitude()
				}
			};

			person.Company = new CardCompany
			{
				Name = person.DsCompany.CompanyName(),
				CatchPhrase = person.DsCompany.CatchPhrase(),
				Bs = person.DsCompany.Bs()
			};
			return person;
		}
	}
}
