#### [![CI](https://github.com/yinghuaxuan/SpecFirst.MarkdownParser/workflows/ci/badge.svg)](https://github.com/yinghuaxuan/SpecFirst.MarkdownParser/actions?query=workflow%3ACI) [![Nuget](https://img.shields.io/nuget/v/SpecFirst.MarkdownParser)](https://www.nuget.org/packages/SpecFirst.MarkdownParser/)

# SpecFirst.MarkdownParser
SpecFirst.MarkdownParser is the markdown parser part of the SpecFirst source generator. It is reponsible for converting the decision tables written in markdown text to decision table objects.

To see how SpecFirst.MarkdownParser should be used, see [How to use it](https://github.com/yinghuaxuan/SpecFirst/blob/master/README.md#how-to-use-it) 

To see how the SpecFirst source generator works, see [SpecFirst](https://github.com/yinghuaxuan/SpecFirst)

## Technical Details
The SpecFirst.MarkdownParser package uses the [markdown-it](https://github.com/markdown-it/markdown-it) parser with [markdown-it-multimd-table](https://github.com/redbug312/markdown-it-multimd-table) support for its intuitive way to author decision tables. 

A markdown parser must implement the [IDecisionTableMarkdownParser](https://github.com/yinghuaxuan/spec-first/blob/develop/src/SpecFirst.Core/IDecisionTableMarkdownParser.cs) interface from SpecFirst.Core package.  

The [SpecFirst.MarkdownParser](https://www.nuget.org/packages/SpecFirst.MarkdownParser/) nuget package must be installed into the same project as the [SpecFirst](https://www.nuget.org/packages/SpecFirst/) nuget package.
