using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Command
{
public class CommandInvoker
{
    // 用于撤销的命令对象栈
    private static Stack<ICommand> s_UndoStack = new Stack<ICommand>();

    // 用于重做的命令对象栈
    private static Stack<ICommand> s_RedoStack = new Stack<ICommand>();

    // 直接执行命令对象并保存到撤销栈
    public static void ExecuteCommand(ICommand command)
    {
        command.Execute();
        s_UndoStack.Push(command);

        // 如果执行了新操作，清空重做栈
        s_RedoStack.Clear();
    }

    public static void UndoCommand()
    {
        // 如果有命令可以撤销
        if (s_UndoStack.Count > 0)
        {
            ICommand activeCommand = s_UndoStack.Pop();
            s_RedoStack.Push(activeCommand);
            activeCommand.Undo();
        }
    }

    public static void RedoCommand()
    {
        // 如果有命令可以重做
        if (s_RedoStack.Count > 0)
        {
            ICommand activeCommand = s_RedoStack.Pop();
            s_UndoStack.Push(activeCommand);
            activeCommand.Execute();
        }
    }
}
}