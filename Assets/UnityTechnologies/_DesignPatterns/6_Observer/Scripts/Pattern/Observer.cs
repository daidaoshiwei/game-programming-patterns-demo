using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Observer
{
    public class ExampleObserver : MonoBehaviour
    {
        // 我们正在观察/监听的Subject引用
        [SerializeField] Subject subjectToObserve;

        // 事件处理方法：函数签名必须与Subject的事件匹配
        private void OnThingHappened()
        {
            // 响应事件的任何逻辑都放在这里
        }

        private void Awake()
        {
            // 订阅/注册到Subject的事件
            if (subjectToObserve != null)
            {
                subjectToObserve.ThingHappened += OnThingHappened;
            }
        }

        private void OnDestroy()
        {
            // 如果销毁对象，取消订阅/注销
            if (subjectToObserve != null)
            {
                subjectToObserve.ThingHappened -= OnThingHappened;
            }
        }
    }
}
