using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpParser
{
	public class Parser
	{
		/// <summary>
		/// クラスデータのリスト（解析後に使用可能）
		/// </summary>
		public List<ClassData> ClassDataList { get; } = new List<ClassData>();

		/// <summary>
		/// コンストラクタ
		/// この時点でソリューションの解析を開始し、結果をClassDataListへ格納します
		/// </summary>
		/// <param name="solution_name">ソリューション名</param>
		/// <param name="project_name">プロジェクト名</param>
		public Parser(string solution_name, string project_name)
		{
			var compilation = ParserUtility.GetCompilation(solution_name, project_name);

			foreach (var class_symbol in ParserUtility.GetClassSymbol(compilation))
			{
				var class_decl = ParserUtility.GetClassDeclaration(class_symbol.SyntaxTree.GetRoot());

				if (class_decl == null) { continue; }

				ClassData class_data = new ClassData();

				//!< クラス名
				class_data.Name = class_decl.Identifier.ValueText;
				foreach (var member_decl in ParserUtility.GetMemberDeclaration(class_decl))
				{
					var property = ParserUtility.GetPropertyDeclaration(member_decl);
					if (property != null)
					{
						class_data.PropertyDeclarations.Add(property);
					}
					var method = ParserUtility.GetMethodDeclaration(member_decl);
					if (property != null)
					{
						class_data.MethodDeclarations.Add(method);
					}
					var constructor = ParserUtility.GetConstructorDeclaration(member_decl);
					if (property != null)
					{
						class_data.ConstructorDeclarations.Add(constructor);
					}
				}
				foreach (var attribute in ParserUtility.GetAttributes(class_decl))
				{
					if (attribute != null)
					{
						class_data.Attributes.Add(attribute);
					}
				}
				ClassDataList.Add(class_data);
			}
		}
	}
}
