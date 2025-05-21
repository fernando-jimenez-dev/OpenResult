# OpenResult

Dead-simple, dependency-free implementations of the Result pattern for any language.
Copy, paste, and use—no lock-in, no contracts, no bloat.

---

## What is OpenResult?

OpenResult is a collection of single-file Result types for popular programming languages, letting you represent the outcome of an operation (success or failure, with optional value or error) in the simplest possible way.

- No external dependencies.
- No frameworks.
- No contracts or interfaces.
- Just drop-in code you fully own.

---

## Why OpenResult?

Most "Result" or "Outcome" libraries are:

- Overengineered, with too many features and abstractions.
- Tied to package managers and hidden dependencies.
- Hard to read, modify, or audit.
- Force you to **install** them.

**OpenResult** shows that you can get 99% of the value in a few lines, in any language, without having to **install a third-party dependency** just for a type!

---

## Usage

1. **Browse** to your language in the `/languages` folder.
2. **Copy** the Result/Error code files (e.g., `Result.cs`, `error.ts`, etc.).
3. **Paste** it directly into your own project.
4. **Use** as needed—no setup, no dependencies, no ceremony.

_Optional:_

- Modify for your own needs.
- Contribute improvements or new languages (keep it minimal!).

### ⚠️ Don’t Install as a Package—Just Copy!

OpenResult exists as NuGet, NPM, and other packages for **personal use, experimentation, or prototyping** only.

**For production:**

- Do **not** install this as a package or dependency.
- Instead, copy-paste or fork the source file directly into your codebase.
- This ensures no lock-in, total control, and full transparency.

If you want to try it quickly, go ahead and install the package.  
But for real projects, **copying or forking** is the intended path.

---

## Philosophy

- **Minimalism:** Only what’s needed for basic, everyday use (success/failure, optional value/error).
- **Transparency:** Code is beginner-readable and expert-modifiable.
- **Portability:** Works anywhere, in any project.
- **No Lock-in:** No packages, no magic, no contracts.
- **Clarity:** Anyone can understand it at a glance.

---

## Languages Supported

- C# | CSharp | .NET 8
  - [Result File](https://github.com/fernando-jimenez-dev/OpenResult/blob/main/languages/csharp/OpenResult/Result.cs)
  - [Error File](https://github.com/fernando-jimenez-dev/OpenResult/blob/main/languages/csharp/OpenResult/Error.cs)

_Interested in contributing a minimal Result implementation in another language? PRs welcome!_

---

## Example (C#)

```csharp
public record Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public Error? Error { get; }
    public bool IsFailure => !IsSuccess;

    private Result(T? value, Error? error = null)
    {
        IsSuccess = error is null;
        Error = error;
        Value = value;
    }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(Error error) => new(default, error);
}
```

---

## Contributing

- Open a pull request to add a new language implementation or improve an existing one.
- Keep each implementation in a single file and as minimal as possible.
- No dependencies, no contracts—show the world how simple the Result pattern can be.

---

## The Result Pattern: Plain English Reference

**Result:**  
Represents the outcome of an operation—either “success” or “failure.”

- Has a flag or property that is `true` when the operation was successful, and `false` when it failed.
- If it failed, provides access to an error object or message.

**Result with Value (Generic/Typed):**

- Same as above, but if successful, also carries a value (the result of the operation).
- If failed, the value is absent, null, or set to a default.

**Error:**

- Describes what went wrong if an operation failed.
- Always has a human-readable message.
- Optionally may include:
  - A code or identifier (for programmatic checks)
  - A wrapped exception or error (for debugging/logging)
  - A reference to an “inner” error (for chaining or causality)

_Any implementation in any language should follow this pattern: clear, explicit, zero-magic, and easy to copy and modify._
