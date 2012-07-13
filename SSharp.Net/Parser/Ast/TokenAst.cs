using System.Collections.Generic;

namespace Scripting.SSharp.Parser.Ast
{
  internal class TokenAst : AstNode
  {
    #region Constructor
    protected TokenAst(AstNodeArgs args)
      : base(args)
    {
    }
    #endregion

    #region Properties
    public ITerminal Terminal
    {
      get { return base.Term as ITerminal; }
    }

    public string Text
    {
      get;
      set;
    }

    public object Value
    {
      get;
      set;
    }

    public bool IsError()
    {
      return Terminal.Category == TokenCategory.Error;
    }

    public int Length
    {
      get { return Text == null ? 0 : Text.Length; }
    }

    public bool IsKeyword
    {
      get;
      set;
    }

    public bool MatchByValue
    {
      get
      {
        if (IsKeyword) return true;
        if (Text == null) return false;
        return (Terminal.MatchMode & TokenMatchMode.ByValue) != 0;
      }
    }

    public bool MatchByType
    {
      get
      {
        if (IsKeyword) return false;
        return (Terminal.MatchMode & TokenMatchMode.ByType) != 0;
      }
    }
    #endregion

    #region Methods
    public static TokenAst Create(ITerminal term, CompilerContext context, SourceLocation location, string text)
    {
      return Create(term, context, location, text, text);
    }

    public static TokenAst Create(ITerminal term, CompilerContext context, SourceLocation location, string text, object value)
    {
      int textLen = text == null ? 0 : text.Length;
      SourceSpan span = new SourceSpan(location, textLen);
      AstNodeArgs args = new AstNodeArgs(term, span, null);
      TokenAst token = new TokenAst(args);
      token.Text = text;
      token.Value = value;
      return token;
    }
    #endregion
  }
}
