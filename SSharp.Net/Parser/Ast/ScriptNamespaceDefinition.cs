/*
 * Copyright � 2011, Petro Protsyk, Denys Vuika
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *  http://www.apache.org/licenses/LICENSE-2.0
 *  
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Scripting.SSharp.Runtime;
using Scripting.SSharp.Parser.FastGrammar;

namespace Scripting.SSharp.Parser.Ast
{
  /// <summary>
  /// 
  /// </summary>
  internal class ScriptNamespaceDefinition : ScriptStatement
  {
    private readonly string _name;
    private readonly ScriptAst _statement;

    public string Name { get { return _name; } }

    public ScriptNamespaceDefinition(AstNodeArgs args)
        : base(args)
    {
      _name = (string)((TokenAst)args.ChildNodes[1]).Value;
      _statement = args.ChildNodes[2] as ScriptAst;
    }

    public override void Evaluate(IScriptContext context)
    {
      context.CreateScope(RuntimeHost.ScopeFactory.Create(ScopeTypes.Namespace, context.Scope, _name));
        _statement.Evaluate(context);
      context.RemoveLocalScope();
    }
  }
}
