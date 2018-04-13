Imports Ciloci.Flee
Imports Ciloci.Flee.CalcEngine
Imports System.Reflection
Imports System.Reflection.Emit

Module Module1

	Sub Main()
		Dim context As New ExpressionContext()
		context.Options.EmitToAssembly = True

		'context.ParserOptions.RequireDigitsBeforeDecimalPoint = True
		'context.ParserOptions.DecimalSeparator = ","c
		'context.ParserOptions.RecreateParser()
		'context.Options.ResultType = GetType(Decimal)

		context.Imports.AddType(GetType(Math))
		context.Variables.Add("a", 100D)
		context.Variables.Add("b", 3.14)

        Dim e As IDynamicExpression = context.CompileDynamic("sqrt(4)+if(if(1=0,true,false), if(1=0,10,100), -1)")
        'Dim e As IDynamicExpression = context.CompileDynamic("1=1 or (1=1 And (1>0 Or 2>0 Or 3>0))")
		'Dim e As IGenericExpression(Of Double) = context.CompileGeneric(Of Double)("a")
		Dim result As Object = e.Evaluate()
	End Sub

End Module