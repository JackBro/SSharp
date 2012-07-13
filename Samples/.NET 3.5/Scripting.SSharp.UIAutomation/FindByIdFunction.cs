﻿using System.Windows.Automation;
using Scripting.SSharp.Runtime;

namespace Scripting.SSharp.UIAutomation
{
  internal class FindByIdFunction : IInvokable
  {
    public bool CanInvoke()
    {
      return true;
    }

    public object Invoke(IScriptContext context, object[] args)
    {
      if (args == null || args.Length < 2) return null;

      AutomationElement element = args[0] as AutomationElement;
      if (element == null) return null;

      string name = args[1].ToString();
      if (string.IsNullOrEmpty(name)) return null;

      return element.FindElementById(name);
    }
  }
}
