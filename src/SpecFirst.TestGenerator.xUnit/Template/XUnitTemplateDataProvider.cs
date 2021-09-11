namespace SpecFirst.TestGenerator.xUnit.Template
{
    using System.Collections.Generic;
    using System.Linq;
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.Core.NamingStrategy;
    using SpecFirst.TestGenerator.xUnit.Generator;
    using SpecFirst.TestGenerator.xUnit.Serialization;

    public class XUnitTemplateDataProvider
    {
        private readonly SnakeCaseNamingStrategy _namingStrategy;
        private readonly TestDataGenerator _testDataGenerator;
        private readonly TestMethodGenerator _testMethodGenerator;
        private readonly TestClassDeclarationGenerator _testClassDeclarationGenerator;
        private readonly ClassVariableGenerator _classVariableGenerator;
        private readonly ImplMethodCallExpressionGenerator _implMethodCallExpressionGenerator;
        private readonly AssertStatementGenerator _assertStatementGenerator;
        private readonly ImplMethodDeclarationGenerator _implMethodDeclarationGenerator;

        public XUnitTemplateDataProvider()
        {
            var stringSerializer = new StringDataSerializer();
            var datetimeSerializer = new DateTimeDataSerializer();
            var booleanSerializer = new BooleanDataSerializer();
            var numberSerializer = new NumberDataSerializer();
            var arraySerializer = new ArrayDataSerializer(stringSerializer, numberSerializer, datetimeSerializer, booleanSerializer);
            var variableSerializer = new TableVariableSerializer();
            _testDataGenerator = new TestDataGenerator(stringSerializer, numberSerializer, datetimeSerializer, booleanSerializer, arraySerializer, variableSerializer);
            _namingStrategy = new SnakeCaseNamingStrategy();
            var parameterConverter = new TableHeaderToParameterConverter(_namingStrategy);
            var classNameConverter = new TableNameToClassNameConverter(_namingStrategy);
            _testMethodGenerator = new TestMethodGenerator(parameterConverter, classNameConverter);
            _testClassDeclarationGenerator = new TestClassDeclarationGenerator(classNameConverter);
            _classVariableGenerator = new ClassVariableGenerator();
            _implMethodCallExpressionGenerator = new ImplMethodCallExpressionGenerator(parameterConverter, classNameConverter);
            _assertStatementGenerator = new AssertStatementGenerator(parameterConverter);
            _implMethodDeclarationGenerator = new ImplMethodDeclarationGenerator(parameterConverter, classNameConverter);
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
            var testClass = _testClassDeclarationGenerator.Convert(decisionTable.TableName);
            var classVariables =
                _classVariableGenerator.Convert(decisionTable.DecisionVariables);
            var testMethod = 
                _testMethodGenerator.Convert(decisionTable.TableName, decisionTable.TableHeaders);
            var implMethodCallExpression =
                _implMethodCallExpressionGenerator.Convert(decisionTable.TableName, decisionTable.TableHeaders);
            var assertStatements = _assertStatementGenerator.Convert(decisionTable.TableHeaders);
            var testData = _testDataGenerator.Convert(decisionTable.TableHeaders, decisionTable.TableData);
            var implMethodDeclaration =
                _implMethodDeclarationGenerator.Convert(decisionTable.TableName, decisionTable.TableHeaders);

            return new XUnitTemplateData
            {
                test_class = testClass,
                class_variable = classVariables,
                test_method = testMethod,
                impl_method_call_expression = implMethodCallExpression,
                assert_statement = assertStatements,
                test_data = testData,
                impl_method_declaration = implMethodDeclaration
            };
        }
    }
}
