# LightMoq

Extensions for [LightMock.Generator] to make it more like [Moq].

## Why?

I had written a lot of tests for C# [Godot] projects using Moq, but Moq relies on dynamic code generation at runtime to create the proxies that power mocks. That currently doesn't play nicely with collectible assemblies inside Godot, and I think the future of mocking in C# will probably center around source generators, anyways.

I didn't want to update hundreds of tests, and I like Moq's syntax. So I created a few extensions for LightMock to make it behave more like Moq.

## Usage

<!-- Credits -->
[Godot]: https://godotengine.org
[LightMock.Generator]: https://github.com/anton-yashin/LightMock.Generator
[Moq]: https://github.com/moq/moq4
