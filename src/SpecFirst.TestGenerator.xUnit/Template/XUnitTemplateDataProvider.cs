namespace SpecFirst.TestGenerator.xUnit.Template
{
    using System.Collections.Generic;
    using System.Linq;
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.Core.NamingStrategy;
    using SpecFirst.TestGenerator.xUnit.Converter;
    using SpecFirst.TestGenerator.xUnit.Serialization;

    public class XUnitTemplateDataProvider
    {
        private readonly SnakeCaseNamingStrategy _namingStrategy;
        private readonly TableDataToTestDataConverter _tableDataToTestDataConverter;
        private readonly TableHeaderToTestSignatureConverter _tableHeaderToTestSignatureConverter;
        private readonly TableDataToCommentsConverter _tableDataToCommentsConverter;
        private readonly TableNameToTestNameConverter _tableNameToTestNameConverter;
        private readonly TableVariableToClassVariableConverter _tableVariableConverter;

        public XUnitTemplateDataProvider()
        {
            var stringSerializer = new StringDataSerializer();
            var datetimeSerializer = new DateTimeDataSerializer();
            var booleanSerializer = new BooleanDataSerializer();
            var numberSerializer = new NumberDataSerializer();
            var arraySerializer = new ArrayDataSerializer(stringSerializer, numberSerializer, datetimeSerializer, booleanSerializer);
            var variableSerializer = new TableVariableSerializer();
            _tableDataToTestDataConverter = new TableDataToTestDataConverter(stringSerializer, numberSerializer, datetimeSerializer, booleanSerializer, arraySerializer, variableSerializer);
            _namingStrategy = new SnakeCaseNamingStrategy();
            var parameterConverter = new TableHeaderToParameterConverter(_namingStrategy);
            _tableHeaderToTestSignatureConverter = new TableHeaderToTestSignatureConverter(parameterConverter);
            _tableDataToCommentsConverter = new TableDataToCommentsConverter();
            _tableNameToTestNameConverter = new TableNameToTestNameConverter(_namingStrategy);
            _tableVariableConverter = new TableVariableToClassVariableConverter();
        }

        public XUnitTemplateData[] GetTemplateData(IEnumerable<DecisionTable> decisionTables)
        {
            XUnitTemplateData[] templateData = new XUnitTemplateData[decisionTables.Count()];
            for (int i = 0; i < decisionTables.Count(); i++)
            {
                DecisionTable decisionTable = decisionTables.ElementAt(i);
                XUnitTemplateData singleTemplateData = GetTemplateData(decisionTable);
                templateData[i] = singleTemplateData;
            }

            return templateData;
        }

        private XUnitTemplateData GetTemplateData(DecisionTable decisionTable)
        {
            var signature = 
                _tableHeaderToTestSignatureConverter.Convert(decisionTable.TableHeaders);
            var testData = 
                _tableDataToTestDataConverter.Convert(decisionTable.TableHeaders, decisionTable.TableData);
            var comments =
                _tableDataToCommentsConverter.Convert(decisionTable.TableHeaders.ToArray(), decisionTable.TableData);
            var classVariables =
                _tableVariableConverter.Convert(decisionTable.DecisionVariables);
            XUnitTemplateData templateData = new XUnitTemplateData
            {
                ClassName = _tableNameToTestNameConverter.Convert(decisionTable.TableName),
                TestMethodParameters = signature.TestMethodInputParameters,
                ImplMethodParameters = signature.ImplMethodInputParameters,
                ImplMethodArguments = signature.ImplMethodInputArguments,
                ImplMethodReturnTypes = signature.ImplMethodReturnTypes,
                ImplMethodReturnValues = signature.ImplMethodReturnValues,
                AssertStatements = signature.AssertStatements,
                TestDataAndComments = GetTestDataAndComments(testData, comments),
                ClassVariables = classVariables
            };

            return templateData;
        }

        private IEnumerable<TestDataAndComment> GetTestDataAndComments(string[] testData, string[] comments)
        {
            for (int i = 0; i < testData.Length; i++)
            {
                yield return new TestDataAndComment{TestData = testData[i], Comment = comments[i]};
            }

        }
    }
}
