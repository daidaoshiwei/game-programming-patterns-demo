using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DesignPatterns.Observer
{
    public class Subject: MonoBehaviour
    {
        // 使用自定义委托定义事件
        //public delegate void ExampleDelegate();
        //public static event ExampleDelegate ExampleEvent;

        //... 或者直接使用System.Action
        public event Action ThingHappened;

        // 调用事件以广播给任何监听者/观察者
        public void DoThing()
        {
            ThingHappened?.Invoke();
        }
    }
}

