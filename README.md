# SpecFirst.TestGenerator.xUnit
SpecFirst.TestGenerator.xUnit is the test generator part of the SpecFirst source generator. It is reponsible for genrating the xUnit tests from the decision table objects.  
To see how SpecFirst.TestGenerator.xUnit should be used, see [here](https://github.com/yinghuaxuan/SpecFirst/blob/4ef31dbcbc6fad4977cdbe328a8671c39f64bb49/README.md#usage) 

## Technical Details
A test generator must implement the [ITestsGenerator](https://github.com/yinghuaxuan/spec-first/blob/develop/src/SpecFirst.Core/ITestsGenerator.cs) interface from [SpecFirst.Core](https://www.nuget.org/packages/SpecFirst.Core/) package.    
