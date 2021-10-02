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
        private readonly ClassFieldsGenerator _classFieldGenerator;
        private readonly ImplMethodCallExpressionGenerator _implMethodCallExpressionGenerator;
        private readonly AssertStatementsGenerator _assertStatementGenerator;
        private readonly ImplMethodDeclarationGenerator _implMethodDeclarationGenerator;
        private readonly DecorationMethodDeclarationGenerator _decorationMethodDeclarationGenerator;
        private readonly DecorationVariablesGenerator _decorationVariablesGenerator;

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
            _classFieldGenerator = new ClassFieldsGenerator(primitiveDataSerializer);
            _implMethodCallExpressionGenerator = new ImplMethodCallExpressionGenerator(parameterConverter, classNameConverter);
            _assertStatementGenerator = new AssertStatementsGenerator(parameterConverter);
            _implMethodDeclarationGenerator = new ImplMethodDeclarationGenerator(parameterConverter, classNameConverter);
            _decorationMethodDeclarationGenerator = new DecorationMethodDeclarationGenerator(parameterConverter);
            _decorationVariablesGenerator = new DecorationVariablesGenerator(parameterConverter);
        }

        public object[] GetTemplateData(IEnumerable<DecisionTable> decisionTables)
        {
            object[] templateData = new object[decisionTables.Count()];
            for (int i = 0; i < decisionTables.Count(); i++)
            {
                DecisionTable decisionTable = decisionTables.ElementAt(i);
                object singleTemplateData = GetTemplateData(decisionTable);
                templateData[i] = singleTemplateData;
            }

            return templateData;
        }

        private object GetTemplateData(DecisionTable decisionTable)
        {
            var testClass = _testClassDeclarationGenerator.Convert(decisionTable.TableName);
            var classVariables =
                _classFieldGenerator.Convert(decisionTable.DecisionVariables);
            var testMethod = 
                _testMethodGenerator.Convert(decisionTable.TableName, decisionTable.TableHeaders);
            var implMethodCallExpression =
                _implMethodCallExpressionGenerator.Convert(decisionTable.TableName, decisionTable.TableHeaders);
            var assertStatements = _assertStatementGenerator.Convert(decisionTable.TableHeaders);
            var testData = _testDataGenerator.Convert(decisionTable.TableHeaders, decisionTable.TableData);
            var implMethodDeclaration =
                _implMethodDeclarationGenerator.Convert(decisionTable.TableName, decisionTable.TableHeaders);
            var decorationMethodDeclaration =
                _decorationMethodDeclarationGenerator.Convert(decisionTable.TableHeaders);
            var decorationVariables = _decorationVariablesGenerator.Convert(decisionTable.TableHeaders);

            return new
            {
                testClass.class_name,
                classVariables.class_variables,
                testMethod.test_parameters,
                implMethodCallExpression.impl_return_values,
                implMethodCallExpression.impl_arguments,
                assertStatements.assert_statements,
                testData.test_data_and_comments,
                implMethodDeclaration.impl_return_types,
                implMethodDeclaration.impl_input_parameters,
                decorationMethodDeclaration.decoration_methods,
                decorationVariables.decoration_variables
            };
        }
    }
}
