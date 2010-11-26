﻿namespace Scripting.SSharp.Execution.Compilers.Dom
{
  internal class CodeForStatement : CodeStatement
  {
    public CodeExpression Init
    {
      get;
      private set;
    }

    public CodeExpression Condition
    {
      get;
      private set;
    }

    public CodeExpression Next
    {
      get;
      private set;
    }

    public CodeStatement Statement
    {
      get;
      private set;
    }

    public CodeForStatement(CodeExpression init, CodeExpression condition,CodeExpression next, CodeStatement statement)
    {
      Init = init;
      Condition = condition;
      Next = next;
      Statement = statement;
    }
  }

}
