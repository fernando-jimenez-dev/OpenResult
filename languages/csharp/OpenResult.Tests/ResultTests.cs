namespace OpenResult.Tests;

public class ResultTests
{
    [Fact]
    public void Success_Creates_Successful_Result()
    {
        var result = Result.Success();

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Null(result.Error);
        Assert.True(result.Succeeded());
        Assert.False(result.Failed());
    }

    [Fact]
    public void Success_SugarSyntax_Creates_Successful_Result()
    {
        var result = Result.Success(0);

        Assert.IsType<Result<int>>(result);
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Null(result.Error);
        Assert.True(result.Succeeded());
        Assert.False(result.Failed());
    }

    [Fact]
    public void Success_SugarSyntax_Creates_Successful_Result_WithOutValue()
    {
        var result = Result.Success(0);

        Assert.IsType<Result<int>>(result);
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Null(result.Error);
        Assert.True(result.Succeeded(out var value));
        Assert.Equal(0, value);
        Assert.False(result.Failed());
    }

    [Fact]
    public void Failure_Creates_Failed_Result_With_Error()
    {
        var error = new Error("Test error");
        var result = Result.Failure(error);

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(error, result.Error);
        Assert.False(result.Succeeded());
        Assert.True(result.Failed());
    }

    [Fact]
    public void Failed_Out_Sets_Error_When_Failed()
    {
        var error = new Error("err");
        var result = Result.Failure(error);

        Assert.True(result.Failed(out var e));
        Assert.Equal(error, e);
    }

    [Fact]
    public void Failed_Out_Sets_Null_When_Successful()
    {
        var result = Result.Success();

        Assert.False(result.Failed(out var e));
        Assert.Null(e);
    }

    [Fact]
    public void Error_Is_Null_On_Success()
    {
        var result = Result.Success();
        Assert.Null(result.Error);
    }

    [Fact]
    public void Error_Is_Not_Null_On_Failure()
    {
        var error = new Error("bad");
        var result = Result.Failure(error);

        Assert.NotNull(result.Error);
    }

    [Fact]
    public void Failure_Throws_On_Null_Error()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => Result.Failure(null!));
        Assert.Contains("Result.Failure was called with a null error", ex.Message);
    }

    [Fact]
    public void IsSuccess_And_IsFailure_Are_Always_Opposites()
    {
        var error = new Error("err");
        var success = Result.Success();
        var failure = Result.Failure(error);

        Assert.NotEqual(success.IsSuccess, success.IsFailure);
        Assert.NotEqual(failure.IsSuccess, failure.IsFailure);
    }

    [Fact]
    public void Multiple_Success_Calls_Return_Independent_Instances()
    {
        var a = Result.Success();
        var b = Result.Success();
        Assert.NotSame(a, b);
    }

    [Fact]
    public void Multiple_Failure_Calls_With_Same_Error_Return_Independent_Instances()
    {
        var error = new Error("e");
        var a = Result.Failure(error);
        var b = Result.Failure(error);
        Assert.NotSame(a, b);
    }

    [Fact]
    public void Can_Check_Failed_With_Inline_Declaration()
    {
        var error = new Error("err");
        var result = Result.Failure(error);

        // Out var declaration in 'if'
        if (result.Failed(out var e))
        {
            Assert.Equal(error, e);
        }
        else
        {
            Assert.Fail("Expected result to fail.");
        }
    }

    [Fact]
    public void Failed_Returns_False_And_Null_On_Success()
    {
        var result = Result.Success();
        Assert.False(result.Failed(out var e));
        Assert.Null(e);
    }

    [Fact]
    public void Succeeded_Returns_True_On_Success()
    {
        var result = Result.Success();
        Assert.True(result.Succeeded());
    }

    [Fact]
    public void Succeeded_Returns_False_On_Failure()
    {
        var error = new Error("fail");
        var result = Result.Failure(error);
        Assert.False(result.Succeeded());
    }

    [Fact]
    public void Error_Can_Be_Accessed_After_Failure()
    {
        var error = new Error("fail");
        var result = Result.Failure(error);
        Assert.Equal("fail", result.Error?.Message);
    }

    [Fact]
    public void Failed_Out_Still_Works_After_Accessing_Error()
    {
        var error = new Error("fail");
        var result = Result.Failure(error);
        _ = result.Error; // Access it first
        Assert.True(result.Failed(out var e));
        Assert.Equal(error, e);
    }

    [Fact]
    public void Failure_Does_Not_Change_Error_Instance()
    {
        var error = new Error("specific");
        var result = Result.Failure(error);

        Assert.Same(error, result.Error);
    }

    [Fact]
    public void Success_Does_Not_Change_Error_Value()
    {
        var result = Result.Success();
        Assert.Null(result.Error);
    }

    // Defensive: ensure Error never leaks from prior instance (no static/shared state)
    [Fact]
    public void Multiple_Success_Results_Do_Not_Share_Error_State()
    {
        var s1 = Result.Success();
        var s2 = Result.Success();

        Assert.Null(s1.Error);
        Assert.Null(s2.Error);
    }

    [Fact]
    public void Defensive_Failure_Cannot_Be_Constructed_With_Null_Error()
    {
        // Throws as designed, should not succeed
        Assert.Throws<ArgumentNullException>(() => Result.Failure(null!));
    }
}

public class ResultWithValueTests
{
    [Fact]
    public void Success_Creates_Successful_Result_With_Value()
    {
        var result = Result<int>.Success(123);

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal(123, result.Value);
        Assert.Null(result.Error);
    }

    [Fact]
    public void Failure_Creates_Failed_Result_With_Error_And_Default_Value()
    {
        var error = new Error("bad");
        var result = Result<int>.Failure(error);

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(default, result.Value);
        Assert.Equal(error, result.Error);
    }

    [Fact]
    public void Succeeded_Out_Returns_True_And_Sets_Value_On_Success()
    {
        var result = Result<string>.Success("abc");
        Assert.True(result.Succeeded(out var val));
        Assert.Equal("abc", val);
    }

    [Fact]
    public void Succeeded_Out_Returns_False_And_Default_Value_On_Failure()
    {
        var error = new Error("fail");
        var result = Result<string>.Failure(error);
        Assert.False(result.Succeeded(out var val));
        Assert.Null(val);
    }

    [Fact]
    public void Failed_Returns_True_If_Failure_And_Error_Is_Not_Null()
    {
        var error = new Error("fail");
        var result = Result<int>.Failure(error);
        Assert.True(result.Failed());
        Assert.Equal(error, result.Error);
    }

    [Fact]
    public void Failed_Returns_False_If_Success()
    {
        var result = Result<int>.Success(1);
        Assert.False(result.Failed());
        Assert.Null(result.Error);
    }

    [Fact]
    public void Failed_Out_Returns_True_And_Error_If_Failed()
    {
        var error = new Error("x");
        var result = Result<string>.Failure(error);

        Assert.True(result.Failed(out var e));
        Assert.Equal(error, e);
    }

    [Fact]
    public void Failed_Out_Returns_False_And_Null_If_Success()
    {
        var result = Result<string>.Success("ok");
        Assert.False(result.Failed(out var e));
        Assert.Null(e);
    }

    [Fact]
    public void Value_Is_Default_If_Failed()
    {
        var error = new Error("bad");
        var result = Result<object>.Failure(error);
        Assert.Null(result.Value);
    }

    [Fact]
    public void Success_Throws_On_Null_Value()
    {
        Assert.Throws<ArgumentNullException>(() => Result<string>.Success(null!));
    }

    [Fact]
    public void Failure_Throws_On_Null_Error()
    {
        Assert.Throws<ArgumentNullException>(() => Result<string>.Failure(null!));
    }

    [Fact]
    public void IsSuccess_And_IsFailure_Are_Always_Opposites()
    {
        var s = Result<string>.Success("yo");
        var f = Result<string>.Failure(new Error("err"));
        Assert.NotEqual(s.IsSuccess, s.IsFailure);
        Assert.NotEqual(f.IsSuccess, f.IsFailure);
    }

    [Fact]
    public void Multiple_Success_Calls_With_Same_Value_Are_Different_Instances()
    {
        var a = Result<int>.Success(7);
        var b = Result<int>.Success(7);
        Assert.NotSame(a, b);
    }

    [Fact]
    public void Multiple_Failure_Calls_With_Same_Error_Are_Different_Instances()
    {
        var error = new Error("repeat");
        var a = Result<int>.Failure(error);
        var b = Result<int>.Failure(error);
        Assert.NotSame(a, b);
    }

    [Fact]
    public void Can_Use_Succeeded_Out_Inline()
    {
        var result = Result<string>.Success("abc");
        if (result.Succeeded(out var val))
        {
            Assert.Equal("abc", val);
        }
        else
        {
            Assert.Fail("Expected success.");
        }
    }

    [Fact]
    public void Can_Use_Failed_Out_Inline()
    {
        var error = new Error("fail");
        var result = Result<string>.Failure(error);
        if (result.Failed(out var e))
        {
            Assert.Equal(error, e);
        }
        else
        {
            Assert.Fail("Expected failure.");
        }
    }

    [Fact]
    public void Success_Stores_Value_Correctly_For_Reference_Types()
    {
        var obj = new object();
        var result = Result<object>.Success(obj);
        Assert.Same(obj, result.Value);
    }

    [Fact]
    public void Success_Stores_Value_Correctly_For_Value_Types()
    {
        var result = Result<double>.Success(42.5);
        Assert.Equal(42.5, result.Value);
    }

    [Fact]
    public void Can_Create_Success_And_Failure_For_Nullable_ValueType()
    {
        var resultOk = Result<int?>.Success(5);
        Assert.True(resultOk.IsSuccess);
        Assert.Equal(5, resultOk.Value);

        var err = new Error("fail");
        var resultFail = Result<int?>.Failure(err);
        Assert.True(resultFail.IsFailure);
        Assert.Equal(default, resultFail.Value);
        Assert.Equal(err, resultFail.Error);
    }
}