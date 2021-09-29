namespace SpecFirst.TestGenerator.xUnit.Template
{
    using System.Collections.Generic;
    using System.Linq;
    using Core.Serialization;
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.Core.NamingStrategy;
    using SpecFirst.TestGenerator.xUnit.Generator;

    public class XUnitTemplateDataProvider
    {
        private readonly SnakeCaseNamingStrategy _namingStrategy;
        private readonly TestDataGenerator _testDataGenerator;
        private readonly TestMethodGenerator _testMethodGenerator;
        private readonly TestClassDeclarationGenerator _testClassDeclarationGenerator;
        private readonly ClassFieldGenerator _classFieldGenerator;
        private readonly ImplMethodCallExpressionGenerator _implMethodCallExpressionGenerator;
        private readonly AssertStatementGenerator _assertStatementGenerator;
        private readonly ImplMethodDeclarationGenerator _implMethodDeclarationGenerator;

        public XUnitTemplateDataProvider()
        {
            var primitiveDataSerializer = new PrimitiveDataSerializer();
            var arraySerializer = new ArrayDataSerializer(primitiveDataSerializer);
            var variableSerializer = new TableVariableSerializer();
            _testDataGenerator = new TestDataGenerator(primitiveDataSerializer, arraySerializer, variableSerializer);
            _namingStrategy = new SnakeCaseNamingStrategy();
            var parameterConverter = new TableHeaderToParameterConverter(_namingStrategy);
            var classNameConverter = new TableNameToClassNameConverter(_namingStrategy);
            _testMethodGenerator = new TestMethodGenerator(parameterConverter, classNameConverter);
            _testClassDeclarationGenerator = new TestClassDeclarationGenerator(classNameConverter);
            _classFieldGenerator = new ClassFieldGenerator(primitiveDataSerializer);
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
            var testClass = _testClassDeclarationGenerator.Convert(decisionTable.TableName).Trim('\r', '\n');
            var classVariables =
                _classFieldGenerator.Convert(decisionTable.DecisionVariables).Trim('\r', '\n');
            var testMethod = 
                _testMethodGenerator.Convert(decisionTable.TableName, decisionTable.TableHeaders).Trim('\r', '\n');
            var implMethodCallExpression =
                _implMethodCallExpressionGenerator.Convert(decisionTable.TableName, decisionTable.TableHeaders).Trim('\r', '\n');
            var assertStatements = _assertStatementGenerator.Convert(decisionTable.TableHeaders).Trim('\r', '\n');
            var testData = _testDataGenerator.Convert(decisionTable.TableHeaders, decisionTable.TableData).Trim('\r', '\n');
            var implMethodDeclaration =
                _implMethodDeclarationGenerator.Convert(decisionTable.TableName, decisionTable.TableHeaders).Trim('\r', '\n');

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
