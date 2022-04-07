using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using RestApiOnCore.Services.Instrastucture;

namespace RestApiOnCore.Unit.Tests;

[TestFixture]
public class ServicesTests
{
	private IDateTimeService _dateTimeService;

	[SetUp]
	public void Setup()
	{
		var mockDateTime = new DateTime(2022, 3, 15);
		var service = Substitute.For<IDateTimeService>();
		service.GetDateTimeNow().Returns(mockDateTime);
		_dateTimeService = service;
	}

	[Test]
	public void TestDateTime()
	{
		var savedTime = new DateTime(2022, 3, 14);
		var diff = _dateTimeService.GetDateTimeNow() - savedTime;
		diff.TotalDays.Should().BeLessOrEqualTo(3);
	}
}