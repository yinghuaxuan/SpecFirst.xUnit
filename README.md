# SpecFirst.MarkdownParser
SpecFirst.MarkdownParser is the markdown parser part of the SpecFirst source generator. It is reponsible for converting the decision tables written in markdown text to decision table objects.  
To see how SpecFirst.MarkdownParser should be used, see [here](https://github.com/yinghuaxuan/SpecFirst/blob/4ef31dbcbc6fad4977cdbe328a8671c39f64bb49/README.md#usage) 
## Technical Details
The SpecFirst.MarkdownParser package uses the [markdown-it](https://github.com/markdown-it/markdown-it) parser with [markdown-it-multimd-table](https://github.com/redbug312/markdown-it-multimd-table) support for its intuitive way to author decision tables. 
A markdown parser must implement the [IDecisionTableMarkdownParser](https://github.com/yinghuaxuan/spec-first/blob/develop/src/SpecFirst.Core/IDecisionTableMarkdownParser.cs) interface from SpecFirst.Core package.  