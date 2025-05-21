# API

## Result

Represents the outcome of an operation—either **success** or **failure**.

**Properties:**

- **IsSuccess**: `true` if the operation succeeded.
- **IsFailure**: `true` if the operation failed.
- **Error**: The associated `Error` object if failed, or `null` if successful.

**Methods:**

- **Succeeded()**: Returns `true` if successful (same as `IsSuccess`).
- **Failed()**: Returns `true` if failed (same as `IsFailure`).
- **Failed(out Error? error)**: Returns `true` if failed, and outputs the associated `Error` object.

**Static Methods:**

- **Success()**: Creates a successful result.
- **Success(TValue value)**: Creates a successful result of TValue.  **[!] It's just Sugar Syntax for `Result<TValue>`**.
- **Failure(Error error)**: Creates a failed result with an associated error.

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
