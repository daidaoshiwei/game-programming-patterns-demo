using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.StatePattern
{
    public interface IState: IColorable
    {
        public void Enter()
        {
            // 首次进入状态时运行的代码
        }

        public void Execute()
        {
            // 每帧逻辑，包括转换到新状态的条件
        }

        public void Exit()
        {
            // 退出状态时运行的代码
        }
    }
}
