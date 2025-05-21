# API

## Result

Represents the outcome of an operation—either **success** or **failure**.

**Properties:**

- **IsSuccess**: `true` if the operation succeeded.
- **IsFailure**: `true` if the operation failed.
- **Error?**: The associated `Error` object if failed, or `null` if successful.

**Methods:**

- **Succeeded()**: Returns `true` if successful (same as `IsSuccess`).
- **Failed()**: Returns `true` if failed (same as `IsFailure`).
- **Failed(out Error? error)**: Returns `true` if failed, and outputs the associated `Error` object.

**Static Methods:**

- **Success()**: Creates a successful result.
- **Success(TValue value)**: Creates a successful result for `Result<TValue>`.  
  _Sugar Syntax for result creation with a Value - type is inferred from the value_.
- **Failure(Error error)**: Creates a failed result with an associated error.

### Type Inference in C#

C# allows for sugar syntax like `Result.Success(value)` because it can **deduce the type** from the value provided.
However, it **cannot** infer the type for failures from just the error object.

You must specify the type explicitly, for example:

```csharp
Result<int>.Failure(error);
// or
Result.Failure<int>(error);
```

#### Workaround for Clean Calls

If you want to avoid repeating the type at every failure, you can use a local helper method:

```csharp
private Result<MyType> Fail(Error error) => Result<MyType>.Failure(error); // <-- Helper

// Usage:
if (shouldFail) return Fail(new Error("failed!"));
return Result.Success(0);
```

This is a C# language limitation, not a library design issue.
C# cannot automatically infer the return type from the consuming method's signature—it can only do so when a value is provided.

**This will not compile:**

```csharp
private Result<int> MyMethod() 
{
	//...
	return Result.Failure(new Error("failed!")); 

	// Compilation error!
	// Result.Failure is returning a Result, not a Result<int>.


	----
	Corrected by:

	return Result<int>.Failure(new Error("failed!")); 
	or
	return Result.Failure<int>(new Error("failed!")); 
}
```

---

## Result&lt;TValue&gt; (Generic Version)

All of the above, plus:

**Property:**

- **Value**: The value produced by a successful operation, or `default` if failed.

**Method:**

- **Succeeded(out TValue? value)**: Returns `true` if successful, and outputs the value.

---

## Error

Describes an error condition in a structured, chainable way.

**Properties:**

- **Message**: Human-readable error message.
- **Code**: Optional code/identifier for programmatic handling.
- **Exception**: Optional `Exception` instance if error was caused by an exception.
- **InnerError**: Optional reference to another `Error` that caused this error.
- **Root**: The deepest, original error in a chain of errors.

**Methods:**

- **IsExceptional()**: Returns `true` if this error wraps an `Exception`.
- **IsExceptional(out Exception? exception)**: Returns `true` and outputs the wrapped exception if present.
