# LightMoq

Extensions for [LightMock.Generator] to make it more like [Moq].

## Why?

I had written a lot of tests for C# [Godot] projects using Moq, but Moq relies on dynamic code generation at runtime to create the proxies that power mocks. That currently doesn't play nicely with collectible assemblies inside Godot, and I think the future of mocking in C# will probably center around source generators, anyways.

I didn't want to update hundreds of tests, and I like Moq's syntax. So I created a few extensions for LightMock to make it behave more like Moq.

## Usage

Just make a mock (exactly how you would with `LightMoq.Generator`) and call `Setup` on it to stub methods. You can also use `VerifyAll`, which will verify all stubbed methods are called at least once, in any order.

```csharp
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
  public void TestObjectIsUsable() {
    var myObject = new Mock<IMyObject>();

    var speech = "Hello World!";

    myObject.Setup(obj => obj.DoSomething());
    myObject.Setup(obj => obj.SaySomething()).Returns(speech);

    myObject.Object.DoSomething();
    myObject.Object.SaySomething().ShouldBe(speech);

    // Make sure all stubbed methods were called at least once, in any order.
    myObject.VerifyAll();
  }
}
```

<!-- Credits -->
[Godot]: https://godotengine.org
[LightMock.Generator]: https://github.com/anton-yashin/LightMock.Generator
[Moq]: https://github.com/moq/moq4
