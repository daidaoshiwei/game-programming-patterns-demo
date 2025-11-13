using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Factory
{
    /// <summary>
    /// 作为所有工厂类型的基类。工厂用于创建产品实例。
    /// </summary>
    public abstract class Factory : MonoBehaviour
    {
        // 抽象方法，用于获取产品实例
        public abstract IProduct GetProduct(Vector3 position);

        // 所有工厂共享的方法
        public string GetLog(IProduct product)
        {
            string logMessage = "Factory: created product " + product.ProductName;
            return logMessage;
        }
    }
}