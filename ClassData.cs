using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace CSharpParser
{
	public class ClassData
	{
		/// <summary>
		/// クラス名
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// コンストラクタ情報
		/// </summary>
		public List<ConstructorDeclarationSyntax> ConstructorDeclarations { get; set; } = new List<ConstructorDeclarationSyntax>();

		/// <summary>
		/// メソッド情報
		/// </summary>
		public List<MethodDeclarationSyntax> MethodDeclarations { get; set; } = new List<MethodDeclarationSyntax>();

		/// <summary>
		/// フィールド情報
		/// </summary>
		public List<FieldDeclarationSyntax> FieldDeclarations { get; set; } = new List<FieldDeclarationSyntax>();

		/// <summary>
		/// プロパティ情報
		/// </summary>
		public List<PropertyDeclarationSyntax> PropertyDeclarations { get; set; } = new List<PropertyDeclarationSyntax>();

		/// <summary>
		/// アトリビュート情報
		/// </summary>
		public List<AttributeSyntax> Attributes { get; set; } = new List<AttributeSyntax>();
	}
}
