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
            var singularDataSerializer = new SingularDataSerializer();
            var arraySerializer = new ArrayDataSerializer(singularDataSerializer);
            _testDataGenerator = new TestDataGenerator(singularDataSerializer, arraySerializer);
            var namingStrategy = new SnakeCaseNamingStrategy();
            var parameterConverter = new TableHeaderToParameterConverter(namingStrategy);
            var classNameConverter = new TableNameToClassNameConverter(namingStrategy);
            _testMethodGenerator = new TestMethodGenerator(parameterConverter, classNameConverter);
            _testClassDeclarationGenerator = new TestClassDeclarationGenerator(classNameConverter);
            _classFieldGenerator = new ClassFieldsGenerator(singularDataSerializer);
            _implMethodCallExpressionGenerator = new ImplMethodCallExpressionGenerator(parameterConverter, classNameConverter);
            _assertStatementGenerator = new AssertStatementsGenerator(parameterConverter);
            _implMethodDeclarationGenerator = new ImplMethodDeclarationGenerator(parameterConverter, classNameConverter);
            _decorationMethodDeclarationGenerator = new DecorationMethodDeclarationGenerator(parameterConverter);
            _decorationVariablesGenerator = new DecorationVariablesGenerator(parameterConverter);
        }

        public IEnumerable<object> GetTemplateData(IEnumerable<DecisionTable> decisionTables)
        {
            foreach (var table in decisionTables)
            {
                object singleTemplateData = GetTemplateData(table);
                yield return singleTemplateData;
            }
        }

        private object GetTemplateData(DecisionTable decisionTable)
        {
            var testClass = _testClassDeclarationGenerator.Convert(decisionTable);
            var classVariables = _classFieldGenerator.Convert(decisionTable);
            var testMethod = _testMethodGenerator.Convert(decisionTable);
            var implMethodCallExpression = _implMethodCallExpressionGenerator.Convert(decisionTable);
            var assertStatements = _assertStatementGenerator.Convert(decisionTable);
            var testData = _testDataGenerator.Convert(decisionTable);
            var implMethodDeclaration = _implMethodDeclarationGenerator.Convert(decisionTable);
            var decorationMethodDeclaration = _decorationMethodDeclarationGenerator.Convert(decisionTable);
            var decorationVariables = _decorationVariablesGenerator.Convert(decisionTable);

            return new
            {
                is_setup_table = decisionTable.TableType == TableType.Setup,
                setup_class_name = "",
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
