using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace OrderManager.ApplicationCore.Services;

public static class FormulaService {

	private record Argument(Type Type, string Name);
	private record FormulaScript(string Code, PortableExecutableReference[] Metadata);

	public static async Task<string> ExecuteFormula<TArg>(string formula, TArg arg, string argname = "arg") {
		
		var script = GenerateScript(formula, new Argument[] { 
			new(typeof(TArg), argname)
		});

		Func<TArg,string> f = await CSharpScript.Create(
						code: script.Code,
						options: ScriptOptions.Default.WithReferences(script.Metadata))
					.ContinueWith<Func<TArg, string>>("FormulaExecutor.Execute")
					.CreateDelegate()
					.Invoke();

		return f(arg);

	}

	public static async Task<string> ExecuteFormula<TArg1, TArg2>(string formula, TArg1 arg1, TArg2 arg2, string arg1name = "arg1", string arg2name = "arg2") {

		var script = GenerateScript(formula, new Argument[] {
			new(typeof(TArg1), arg1name),
			new(typeof(TArg2), arg2name)
		});

		Func<TArg1, TArg2, string> f = await CSharpScript.Create(
						code: script.Code,
						options: ScriptOptions.Default.WithReferences(script.Metadata))
					.ContinueWith<Func<TArg1, TArg2, string>>("FormulaExecutor.Execute")
					.CreateDelegate()
					.Invoke();

		return f(arg1, arg2);

	}

	private static FormulaScript GenerateScript(string formula, params Argument[] arguments) {

		string namespaces = string.Empty;
		string args = string.Empty;
		Dictionary<string, PortableExecutableReference> metadata = new();

		for (int i = 0; i < arguments.Length; i++) {
			var arg = arguments[i];
			args += $"{arg.Type.Name} {arg.Name}" + (i < arguments.Length - 1 ? "," : string.Empty);
			namespaces += $"using {arg.Type.Namespace};\n";

			var location = arg.Type.Assembly.Location;
			if (!metadata.ContainsKey(location))
				metadata.Add(location, MetadataReference.CreateFromFile(location));
		}

		string code = $@"{namespaces}
						public static class FormulaExecutor {{
							public static string Execute({args}) {{
								return ${formula};
							}}
						}}";

		return new(code, metadata.Values.ToArray());

	}

}
