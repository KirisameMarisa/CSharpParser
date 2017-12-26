## About this Project
This project is a project to analyze C # code using Microsoft.CodeAnalysis.

## Sample
```CSharp
public static void Main(string[] args)
{
    const string pathToSolution = @"";
    const string projectName = "";

    CSharpParser.Parser parser = new CSharpParser.Parser(pathToSolution, projectName);

    foreach (var class_data in parser.ClassDataList)
    {
        //!< Classname.
        Console.WriteLine(class_data.Name);
        foreach (var attr in class_data.Attributes)
        {
            //!< Attribute name.
            Console.WriteLine(attr.Name.ToString());
        }
    }
}
```

## Libraries
Using these libraries

### [Microsoft.CodeAnalysis](https://github.com/dotnet/roslyn-analyzers)
* Purpose : CodeAnalysis
* License : Apache License 2.0

## License
This software is licensed under the MIT License.
