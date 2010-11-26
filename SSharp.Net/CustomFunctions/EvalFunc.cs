using System;
using Scripting.SSharp.Runtime;
using Scripting.SSharp.Parser.Ast;

namespace Scripting.SSharp.CustomFunctions
{
  internal class EvalFunc : IInvokable
  {
    public static EvalFunc FunctionDefinition = new EvalFunc();
    public static string FunctionName = "eval";

    private EvalFunc()
    {
    }

    #region IInvokable Members

    public bool CanInvoke()
    {
      return true;
    }

    public object Invoke(IScriptContext context, object[] args)
    {      
      var code = (String)args[0];
      ScriptAst result;

      RuntimeHost.Lock();
      
      try
      {
        result = Script.Parse(code + ";", false) as ScriptAst;
        //TODO: Create LocalOnlyScope that can't change Parent's variables
        //No, need for doing these. It is already done
        context.CreateScope();
      }
      finally
      {
        RuntimeHost.UnLock();
      }

      if (result != null) result.Evaluate(context);
      context.RemoveLocalScope();
      
      return context.Result;
    }

    #endregion
  }
}