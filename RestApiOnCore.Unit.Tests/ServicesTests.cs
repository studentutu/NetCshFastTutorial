using System;
using System.Reflection;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using RestApiOnCore.Services.Instrastucture;

namespace RestApiOnCore.Unit.Tests;

public static class EventExtensions
{
	public static void Raise(this object instance, string eventName, EventArgs eventArgs = null)
	{
		const BindingFlags staticFlags = BindingFlags.Instance | BindingFlags.NonPublic;
		var type = instance.GetType();
		var eventField = type.GetField(eventName, staticFlags);

		if (string.IsNullOrEmpty(eventName))
		{
			throw new Exception($"Event with name {eventName} could not be found.");
		}

		var multicastDelegate = eventField.GetValue(instance) as Delegate;

		if (eventArgs != null)
		{
			multicastDelegate.DynamicInvoke(eventArgs);
		}
		else
		{
			multicastDelegate.DynamicInvoke();
		}
	}
}

[TestFixture]
public class ServicesTests
{
	private IDateTimeService _dateTimeService;

	public class SomeCLassWithEvent
	{
		public event System.Action CloseClick;
	}


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

	[Test]
	public void TestRaiseEvent()
	{
		var savedTime = new SomeCLassWithEvent();
		bool clicked = false;
		savedTime.CloseClick += () => { clicked = true; };
		savedTime.Raise(nameof(SomeCLassWithEvent.CloseClick));

		clicked.Should().BeTrue();
	}
}