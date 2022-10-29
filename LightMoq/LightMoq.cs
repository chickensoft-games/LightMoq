namespace LightMoq;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using LightMock;
using LightMock.Generator;

/// <summary>
/// Extensions for LightMock.Generator to make it more like Moq.
/// </summary>
public static class LightMoq {
  private static readonly ConditionalWeakTable<object, List<Action>>
    _verifications = new();

  /// <summary>
  /// Stub a method on a mock that can return, throw, or invoke callbacks.
  /// A stubbed method can be verified later.
  /// </summary>
  /// <typeparam name="T">Type of the object being mocked.</typeparam>
  public static IArrangement Setup<T>(
    this Mock<T> mock,
    Expression<Action<T>> matchExpression
  ) where T : class {
    _verifications.GetOrCreateValue(mock).Add(
      () => mock.Assert(matchExpression, Invoked.Once)
    );
    return mock.Arrange(matchExpression);
  }

  /// <summary>
  /// Stub a method on a mock that can return, throw, or invoke callbacks.
  /// A stubbed method can be verified later.
  /// </summary>
  /// <typeparam name="T">Type of the object being mocked.</typeparam>
  /// <typeparam name="TResult">Return type of the method.</typeparam>
  public static IArrangement<TResult> Setup<T, TResult>(
    this Mock<T> mock,
    Expression<Func<T, TResult>> matchExpression
  ) where T : class {
    _verifications.GetOrCreateValue(mock).Add(
      () => mock.Assert(FuncToAction(matchExpression), Invoked.Once)
    );
    return mock.Arrange(matchExpression);
  }

  private static Expression<Action<T>> FuncToAction<T, TResult>(
    Expression<Func<T, TResult>> expression
  ) => System.Linq.Expressions.Expression.Lambda<Action<T>>(
    expression.Body, expression.Parameters
  );

  /// <summary>
  /// Verify that all expected method calls were made. Expected method calls
  /// can be registered with
  /// <see cref="Setup{T}(Mock{T}, Expression{Action{T}})"/>.
  /// </summary>
  /// <typeparam name="T">Type of the object being mocked.</typeparam>
  public static void VerifyAll<T>(this Mock<T> mock) where T : class {
    if (_verifications.TryGetValue(mock, out var verifications)) {
      foreach (var verification in verifications) {
        verification();
      }
    }
    else {
      throw new InvalidOperationException("Can't find any methods to verify.");
    }
  }
}
