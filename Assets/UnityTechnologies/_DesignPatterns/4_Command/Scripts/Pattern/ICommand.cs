using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Command
{
    // 用于将操作封装为"命令对象"的接口
    public interface ICommand
    {
        public void Execute();
        public void Undo();
    }
}