using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Factory
{
    public class ConcreteFactoryA : Factory
    {
        // 用于创建预制体的引用
        [SerializeField] 
        private ProductA m_ProductPrefab;

        public override IProduct GetProduct(Vector3 position)
        {
            // 创建预制体实例并获取产品组件
            GameObject instance = Instantiate(m_ProductPrefab.gameObject, position, Quaternion.identity);
            ProductA newProduct = instance.GetComponent<ProductA>();

            // 每个产品包含自己的逻辑
            newProduct.Initialize();

            return newProduct;
        }
    }
}
