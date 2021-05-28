# SpecFirst.TestGenerator.xUnit
SpecFirst.TestGenerator.xUnit is the test generator part of the SpecFirst source generator. It is reponsible for genrating xUnit tests from the decision table objects.  

To see how SpecFirst.TestGenerator.xUnit should be used, see [How to use it](https://github.com/yinghuaxuan/SpecFirst/blob/master/README.md#how-to-use-it) 

## Technical Details
A test generator must implement the [ITestsGenerator](https://github.com/yinghuaxuan/SpecFirst.Core/blob/main/src/SpecFirst.Core/ITestsGenerator.cs) interface from [SpecFirst.Core](https://www.nuget.org/packages/SpecFirst.Core/) package.    

The [SpecFirst.TestGenerator.xUnit](https://www.nuget.org/packages/SpecFirst.xUnit/) nuget package must be installed into the same project as the [SpecFirst](https://www.nuget.org/packages/SpecFirst/) nuget package.
