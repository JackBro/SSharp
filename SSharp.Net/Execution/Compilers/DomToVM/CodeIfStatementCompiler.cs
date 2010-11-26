﻿namespace Scripting.SSharp.Execution.Compilers
{
  using Dom;
  using VM;
  using VM.Operations;

  [CompilerType(typeof(CodeIfStatement))]
  internal class CodeIfStatementCompiler : IVMCompiler
  {
    #region IVMCompiler Members

    public ExecutableMachine Compile(CodeObject code, ExecutableMachine machine)
    {
      // CCCC(indexA)SSSSSS(indexB)EEEEEE(indexC)
      
      var ifStatement = (CodeIfStatement)code;

      CodeDomCompiler.Compile(ifStatement.Condition, machine);
      //Put AX (result of condition evaluation) to BBX
      var exch = machine.CreateOperation<RegisterOperation>();
      exch.Source = MachineRegisters.AX;
      exch.Destination = MachineRegisters.BBX;
      //Jmp To Else
      var jmpToElse = machine.CreateOperation<JmpIfOperation>();
      int eCount = machine.CommandCount;    
      
      //Compile Statement
      CodeDomCompiler.Compile(ifStatement.Statement, machine);
      jmpToElse.Offset = machine.CommandCount - eCount + 1;     
      
      //Compile Else if Any
      if (ifStatement.ElseStatement != null)
      {
        //Jmp To Next
        var jmpToNext = machine.CreateOperation<JmpOperation>();
        //Modify jmpToElse
        jmpToElse.Offset++;

        int sCount = machine.CommandCount;
        CodeDomCompiler.Compile(ifStatement.ElseStatement, machine);
        jmpToNext.Offset = machine.CommandCount - sCount + 1;
      }

      return machine;
    }

    #endregion
  }
}
