namespace LightMoqTests;

using System;
using LightMock.Generator;
using LightMoq;
using Shouldly;
using Xunit;

internal interface IMyObject {
  void DoSomething();
  string SaySomething();
}

public class LightMoqTest {
  [Fact]
  public void StubsMethods() {
    var myObject = new Mock<IMyObject>();

    var speech = "Hello World!";

    myObject.Setup(obj => obj.DoSomething());
    myObject.Setup(obj => obj.SaySomething()).Returns(speech);

    myObject.Object.DoSomething();
    myObject.Object.SaySomething().ShouldBe(speech);

    myObject.Assert(obj => obj.DoSomething());
  }

  [Fact]
  public void VerifiesMethodsWhenTheyAreCalled() {
    var myObject = new Mock<IMyObject>();

    var speech = "Hello World!";

    myObject.Setup(obj => obj.DoSomething());
    myObject.Setup(obj => obj.SaySomething()).Returns(speech);

    myObject.Object.DoSomething();
    myObject.Object.SaySomething().ShouldBe(speech);

    Should.NotThrow(() => myObject.VerifyAll());
  }

  [Fact]
  public void ThrowsWhenNoMethodsToVerify() {
    var myObject = new Mock<IMyObject>();

    Should.Throw<InvalidOperationException>(() => myObject.VerifyAll());
  }
}
