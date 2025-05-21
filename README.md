# OpenResult

**Dead-simple, dependency-free implementations of the Result pattern for any language.  
Copy, paste, and use—no lock-in, no contracts, no bloat.**

---

## What is OpenResult?

OpenResult is a collection of single-file Result types for popular programming languages, letting you represent the outcome of an operation (success or failure, with optional value or error) in the simplest possible way.

No external dependencies.  
No frameworks.  
No contracts or interfaces.  
Just drop-in code you fully own.

---

## Why OpenResult?

Most "Result" or "Outcome" libraries are:

- Overengineered, with too many features and abstractions.
- Tied to package managers and hidden dependencies.
- Hard to read, modify, or audit.

**OpenResult** shows that you can get 99% of the value in a few lines, in any language.

---

## Usage

1. **Browse** to your language in the `/languages` folder.
2. **Copy** the Result code file (e.g., `Result.cs`, `result.ts`, etc.).
3. **Paste** it directly into your own project.
4. **Use** as needed—no setup, no dependencies, no ceremony.

**Optional:**  
Modify for your own needs.  
Contribute improvements or new languages (keep it minimal!).

---

## Philosophy

- **Minimalism:** Only what’s needed for basic, everyday use (success/failure, optional value/error).
- **Transparency:** Code is beginner-readable and expert-modifiable.
- **Portability:** Works anywhere, in any project.
- **No Lock-in:** No packages, no magic, no contracts.
- **Clarity:** Anyone can understand it at a glance.

---

## Languages Supported

- C# (`/languages/dotnet/Result.cs`)
<!-- - TypeScript (`/languages/typescript/result.ts`)
- Python (`/languages/python/result.py`)
- Go (`/languages/go/result.go`)
- Rust (`/languages/rust/result.rs`)
- Java (`/languages/java/Result.java`)
- ...and more (contributions welcome!) -->

---

## Example (C#)

```csharp
public class Result
{
    public bool IsSuccess { get; }
    public string? Error { get; }

    public bool IsFailure => !IsSuccess;

    private Result(bool isSuccess, string? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, null);
    public static Result Failure(string error) => new(false, error);
}
```
