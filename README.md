# SpecFirst.xUnit
SpecFirst.xUnit is a .NET [source generator](https://devblogs.microsoft.com/dotnet/introducing-c-source-generators/) to automatically generate xUnit tests from [decision tables](https://github.com/yinghuaxuan/spec-first/blob/develop/tests/SpecFirst.Specs/DecisionTable/Validator/DecisionTable.spec.md) authored in markdown.

It is a combination of the following three packages:
- SpecFirst
- SpecFirst.MarkdownParser
- SpecFirst.TestGenerator.xUnit

## Usage
- Add a spec file (.spec.md) containing some decision tables to the current project 
- Mark the file as 'C# analyzer additional file' in 'Build Action'
- Install SpecFirst.xUnit nuget package
- Add a config file named [specfirst.config](https://github.com/yinghuaxuan/spec-first/blob/develop/tests/SpecFirst.Specs/specfirst.config) (optional - [default settings](https://github.com/yinghuaxuan/spec-first/blob/develop/src/SpecFirst/Setting/SpecFirstSettingManager.cs#L11) will be used instead)
- Rebuild the current project  
- Two test files will be auto-generated for each spec file containing at least one decision table: one for the skeleton of the tests and the other for the implementation of the tests.  