using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;
using System.Collections.Immutable;
using System.Diagnostics;
using ConvMVVM2.Core.CodeGen.GenInfo;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;
using System.Linq;


namespace ConvMVVM2.Core.CodeGen
{
    [Generator]
    internal class RelayCommandCodeGen : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {

//#if DEBUG
//            if (!Debugger.IsAttached)
//            {
//                Debugger.Launch();
//            }
//#endif

            var classDeclarations = context.SyntaxProvider
                                           .CreateSyntaxProvider(predicate: static (s, _) => RelayCommandCodeGen.IsSyntaxForCodeGen(s),
                                                                 transform: static (ctx, _) => RelayCommandCodeGen.GetTargetForCodeGen(ctx))
                                           .Where(x => x != null);


            IncrementalValueProvider<(Compilation, ImmutableArray<ClassDeclarationSyntax>)> compilationAndClass = context.CompilationProvider.Combine(classDeclarations.Collect());
            context.RegisterSourceOutput(compilationAndClass, static (spc, source) => RelayCommandCodeGen.RelayCommandCodeExecute(source.Item1, source.Item2, spc));
        }



        #region Filter
        internal static bool IsSyntaxForCodeGen(SyntaxNode node)
        {
            if (node is not ClassDeclarationSyntax) return false;
            var classSyntax = (ClassDeclarationSyntax)node;
            if (classSyntax.Members.Count == 0) return false;
            if (classSyntax.Members.Where((syntax) => syntax.AttributeLists.Count > 0).Count() == 0) return false;
            return true;
        }

        internal static ClassDeclarationSyntax GetTargetForCodeGen(GeneratorSyntaxContext context)
        {
            var classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;


            foreach (var memeberSyntax in classDeclarationSyntax.Members)
            {
                foreach (AttributeListSyntax attributeListSyntax in memeberSyntax.AttributeLists)
                {
                    foreach (AttributeSyntax attributeSyntax in attributeListSyntax.Attributes)
                    {
                        if (context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol is not IMethodSymbol attributeSymbol)
                        {
                            continue;
                        }
                        INamedTypeSymbol attributeContainingTypeSymbol = attributeSymbol.ContainingType;
                        string fullName = attributeContainingTypeSymbol.ToDisplayString();
                        if (fullName == "ConvMVVM2.Core.Attributes.RelayCommandAttribute")
                        {
                            return classDeclarationSyntax;
                        }
                    }
                }
            }


            return null;
        }
        #endregion

        #region Generator
        internal static string GetNamespace(Compilation compilation, MemberDeclarationSyntax cls)
        {
            var model = compilation.GetSemanticModel(cls.SyntaxTree);

            foreach (NamespaceDeclarationSyntax ns in cls.Ancestors().OfType<NamespaceDeclarationSyntax>())
            {
                return ns.Name.ToString();
            }

            return "";
        }

        internal static List<AutoMethodInfo> GetMethodList(Compilation compilation, MemberDeclarationSyntax cls)
        {
            List<AutoMethodInfo> methodList = new List<AutoMethodInfo>();
            var model = compilation.GetSemanticModel(cls.SyntaxTree);
            foreach (MethodDeclarationSyntax method in cls.DescendantNodes().OfType<MethodDeclarationSyntax>())
            {
                
                var returnType = method.ReturnType.ToString();
                if (returnType != "void") continue;


                bool detection = false;
                foreach (AttributeListSyntax attributeListSyntax in method.AttributeLists)
                {
                    foreach (AttributeSyntax attributeSyntax in attributeListSyntax.Attributes)
                    {
                        if (model.GetSymbolInfo(attributeSyntax).Symbol is not IMethodSymbol attributeSymbol)
                        {
                            continue;
                        }

                        INamedTypeSymbol attributeContainingTypeSymbol = attributeSymbol.ContainingType;
                        string fullName = attributeContainingTypeSymbol.ToDisplayString();
                        if (fullName == "ConvMVVM2.Core.Attributes.RelayCommandAttribute")
                        {
                            detection = true;
                            break;
                        }
                    }
                }

                if (detection == false) continue;


                switch (method.ParameterList.Parameters.Count)
                {
                    case 0:
                        methodList.Add(new AutoMethodInfo()
                        {
                            MethodName = method.Identifier.ToString(),
                            ArgumentType = "",
                        });
                        break;
                    case 1:
                        methodList.Add(new AutoMethodInfo()
                        {
                            MethodName = method.Identifier.ToString(),
                            ArgumentType = method.ParameterList.Parameters[0].Type.ToString(),
                        });
                        break;
                }
            }

            return methodList;
        }

        internal static void RelayCommandCodeExecute(Compilation compilation, ImmutableArray<ClassDeclarationSyntax> classes, SourceProductionContext context)
        {
            if (classes.IsDefaultOrEmpty)
            {
                return;
            }

            IEnumerable<ClassDeclarationSyntax> distinctClasses = classes.Distinct();
            foreach (var cls in distinctClasses)
            {
                // Import all the packages
                var usingDirectives = cls.SyntaxTree.GetCompilationUnitRoot().Usings;
                var usingDeclaration = new StringBuilder($@"{usingDirectives}");

                string clsNamespace = GetNamespace(compilation, cls);
                if (cls.Modifiers.Count == 0) continue;
                if (cls.Modifiers.Where((token) => token.ToString().Contains("partial") == true).Count() == 0)
                {
                    var description = new DiagnosticDescriptor("ConvMVVM20001",
                                                               "Wrong class modifier",
                                                               $"{clsNamespace}.{cls.Identifier.ValueText} class modifier must be partial",
                                                               "Problem",
                                                               DiagnosticSeverity.Error,
                                                               true);
                    context.ReportDiagnostic(Diagnostic.Create(description, Location.None));
                }

                if (cls.BaseList == null)
                {
                    var description = new DiagnosticDescriptor("ConvMVVM20000",
                                           "Wrong class modifier",
                                           $"Invalid base class information",
                                           "Problem",
                                           DiagnosticSeverity.Error,
                                           true);
                    context.ReportDiagnostic(Diagnostic.Create(description, Location.None));
                    return;
                }

                if (cls.BaseList.Types.Count == 0)
                {
                    var description = new DiagnosticDescriptor("ConvMVVM20002",
                                           "None inheritance",
                                           $"{clsNamespace}.{cls.Identifier.ValueText} class have to inhert ViewModelBase",
                                           "Problem",
                                           DiagnosticSeverity.Error,
                                           true);
                    context.ReportDiagnostic(Diagnostic.Create(description, Location.None));
                }

                List<AutoMethodInfo> methodList = GetMethodList(compilation, cls);
                if (methodList.Count == 0) continue;

                var source = """
                {using}
                using System.Windows.Input;

                namespace {clsNamespace}{
                    partial class {clsName}{
                
                {methodCollection}
                        
                    }
                }
                """;

                var propertyCodeGroup = "";
                foreach (var method in methodList)
                {
                    if(method.ArgumentType == "")
                    {
                        string propertyCode = """

                                public ICommand _{methodName}Command = null;
                                public ICommand {methodName}Command{
                                    get{
                                        _{methodName}Command = new RelayCommand(()=> this.{methodName}());
                                        return _{methodName}Command;
                                    }
                                }

                        """;

                        propertyCode = propertyCode.Replace("{methodName}", method.MethodName);
                        propertyCodeGroup += propertyCode;
                    }
                    else
                    {
                        string propertyCode = """

                                public ICommand _{methodName}Command = null;
                                public ICommand {methodName}Command{
                                    get{
                                        _{methodName}Command = new RelayCommand<{arg}>((arg)=> this.{methodName}(arg));
                                        return _{methodName}Command;
                                    }
                                }

                        """;

                        propertyCode = propertyCode.Replace("{methodName}", method.MethodName);
                        propertyCode = propertyCode.Replace("{arg}", method.ArgumentType);
                        propertyCodeGroup += propertyCode;
                    }
                   
                }

                source = source.Replace("{using}", usingDeclaration.ToString());
                source = source.Replace("{methodCollection}", propertyCodeGroup);
                source = source.Replace("{clsNamespace}", clsNamespace);
                source = source.Replace("{clsName}", cls.Identifier.ValueText);
                source = source.Replace("{methodCollection}", propertyCodeGroup);

                context.AddSource($"{clsNamespace}.{cls.Identifier.ValueText}.g.cs", SourceText.From(source, Encoding.UTF8));

            }
        }
        #endregion

    }

}
