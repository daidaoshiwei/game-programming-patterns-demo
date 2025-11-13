using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Factory
{
    /// <summary>
    /// 产品之间的通用接口
    /// </summary>
    public interface IProduct
    {
        // 在此处添加通用属性和方法
        public string ProductName { get; set; }

        // 为每个具体产品自定义此方法
        public void Initialize();
    }
}
