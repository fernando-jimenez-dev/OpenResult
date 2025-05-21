namespace OpenResult.Tests;

public class ErrorTests
{
    [Fact]
    public void Error_Default_Values_Are_Empty()
    {
        var error = new Error();
        Assert.Equal("", error.Message);
        Assert.Null(error.Code);
        Assert.Null(error.Exception);
        Assert.Null(error.InnerError);
    }

    [Fact]
    public void Error_Constructs_With_Message_And_Optional_Fields()
    {
        var ex = new InvalidOperationException("fail");
        var inner = new Error("Inner");
        var error = new Error("Test", "CODE", ex, inner);

        Assert.Equal("Test", error.Message);
        Assert.Equal("CODE", error.Code);
        Assert.Equal(ex, error.Exception);
        Assert.Equal(inner, error.InnerError);
    }

    [Fact]
    public void Error_Root_Returns_Self_When_No_Inner()
    {
        var error = new Error("Root");
        Assert.Same(error, error.Root);
    }

    [Fact]
    public void Error_Root_Returns_Deepest_InnerError()
    {
        var root = new Error("Root");
        var mid = new Error("Mid", InnerError: root);
        var outer = new Error("Outer", InnerError: mid);

        Assert.Same(root, outer.Root);
        Assert.Same(root, mid.Root);
    }

    [Fact]
    public void IsExceptional_Returns_True_If_Exception_Is_Present()
    {
        var ex = new Exception();
        var error = new Error("msg", Exception: ex);

        Assert.True(error.IsExceptional());
    }

    [Fact]
    public void IsExceptional_Returns_False_If_Exception_Is_Null()
    {
        var error = new Error("msg");
        Assert.False(error.IsExceptional());
    }

    [Fact]
    public void IsExceptional_Out_Returns_Exception_And_True()
    {
        var ex = new ArgumentException();
        var error = new Error("msg", Exception: ex);

        Assert.True(error.IsExceptional(out var result));
        Assert.Equal(ex, result);
    }

    [Fact]
    public void IsExceptional_Out_Returns_False_And_Null_If_No_Exception()
    {
        var error = new Error("msg");

        Assert.False(error.IsExceptional(out var result));
        Assert.Null(result);
    }

    [Fact]
    public void Error_Can_Be_Chained_And_All_Links_Accessible()
    {
        var innerMost = new Error("InnerMost");
        var middle = new Error("Middle", InnerError: innerMost);
        var outer = new Error("Outer", InnerError: middle);

        Assert.Equal("Outer", outer.Message);
        Assert.Equal("Middle", outer.InnerError?.Message);
        Assert.Equal("InnerMost", outer.InnerError?.InnerError?.Message);
        Assert.Same(innerMost, outer.InnerError?.InnerError);
    }
}