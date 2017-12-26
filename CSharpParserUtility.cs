using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpParser
{
	/// <summary>
	/// C#プロジェクトの解析のヘルパー関数群
	/// </summary>
	public static class ParserUtility
	{
		/// <summary>
		/// プロジェクトの編集情報を取得します
		/// 解析するにはまずこの関数を呼び出すとこから
		/// </summary>
		/// <param name="solution_name">ソリューション名</param>
		/// <param name="project_name">プロジェクト名</param>
		/// <returns></returns>
		public static Compilation GetCompilation(string solution_name, string project_name)
		{
			MSBuildWorkspace workspace = MSBuildWorkspace.Create();

			Solution solution = workspace.OpenSolutionAsync(solution_name).Result;
			Project project = solution.Projects.Where((proj) => proj.Name == project_name).FirstOrDefault();

			return project.GetCompilationAsync().Result;
		}

		/// <summary>
		/// クラスのシンボル情報を取得します
		/// </summary>
		/// <param name="compilation"></param>
		/// <returns></returns>
		public static IEnumerable<SemanticModel> GetClassSymbol(Compilation compilation)
		{
			foreach (var sourcetree in GetSourceTree(compilation))
			{
				yield return compilation.GetSemanticModel(sourcetree);
			}
		}

		/// <summary>
		/// クラス定義情報を取得します
		/// </summary>
		/// <param name="root_node"></param>
		/// <returns></returns>
		public static ClassDeclarationSyntax GetClassDeclaration(SyntaxNode root_node)
		{
			var root = (CompilationUnitSyntax)root_node;
			foreach (var member in root.Members)
			{
				if (member is NamespaceDeclarationSyntax)
				{
					foreach (var nsM in (member as NamespaceDeclarationSyntax).Members)
					{
						if (nsM is ClassDeclarationSyntax)
						{
							return (nsM as ClassDeclarationSyntax);
						}
					}
				}
			}
			return null;
		}

		/// <summary>
		/// クラスのアトリビュートを取得します
		/// </summary>
		/// <param name="class_decl"></param>
		/// <returns></returns>
		public static IEnumerable<AttributeSyntax> GetAttributes(ClassDeclarationSyntax class_decl)
		{
			foreach (var attribute in class_decl.AttributeLists)
			{
				foreach(var attr in attribute.Attributes)
				{
					yield return attr;
				}	
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="class_decl"></param>
		/// <returns></returns>
		public static SyntaxList<MemberDeclarationSyntax> GetMemberDeclaration(ClassDeclarationSyntax class_decl)
		{
			return (class_decl as ClassDeclarationSyntax).Members;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="member"></param>
		/// <returns></returns>
		public static ConstructorDeclarationSyntax GetConstructorDeclaration(MemberDeclarationSyntax member)
		{
			return member as ConstructorDeclarationSyntax;
		}

		/// <summary>
		/// プロパティ情報を取得します
		/// </summary>
		/// <param name="member"></param>
		/// <returns></returns>
		public static PropertyDeclarationSyntax GetPropertyDeclaration(MemberDeclarationSyntax member)
		{
			return member as PropertyDeclarationSyntax;
		}

		/// <summary>
		/// メソッド情報を取得します
		/// </summary>
		/// <param name="member"></param>
		/// <returns></returns>
		public static MethodDeclarationSyntax GetMethodDeclaration(MemberDeclarationSyntax member)
		{
			return member as MethodDeclarationSyntax;
		}

		/// <summary>
		/// フィールド情報を取得します
		/// </summary>
		/// <param name="member"></param>
		/// <returns></returns>
		public static FieldDeclarationSyntax GetFieldDeclaration(MemberDeclarationSyntax member)
		{
			return member as FieldDeclarationSyntax;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="compilation"></param>
		/// <returns></returns>
		public static IEnumerable<SyntaxTree> GetSourceTree(Compilation compilation)
		{
			foreach (var location in compilation.Assembly.Modules.FirstOrDefault().Locations)
			{
				yield return location.SourceTree;
			}
		}
	}
}
