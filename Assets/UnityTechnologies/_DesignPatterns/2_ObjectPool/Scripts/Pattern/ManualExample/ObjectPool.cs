using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.ObjectPool
{
    public class ObjectPool : MonoBehaviour
    {
        // 初始克隆对象的数量
        [SerializeField] private uint initPoolSize;
        public uint InitPoolSize => initPoolSize;

        // 要池化的对象预制体
        [SerializeField] private PooledObject objectToPool;

        // 在栈中存储池化的对象
        private Stack<PooledObject> stack;

        private void Start()
        {
            SetupPool();
        }

        // 创建对象池（在延迟不明显时调用）
        private void SetupPool()
        {
            // 缺少objectToPool预制体字段
            if (objectToPool == null)
            {
                return;
            }

            stack = new Stack<PooledObject>();

            // 填充对象池
            PooledObject instance = null;

            for (int i = 0; i < initPoolSize; i++)
            {
                instance = Instantiate(objectToPool);
                instance.Pool = this;
                instance.gameObject.SetActive(false);
                stack.Push(instance);
            }
        }

        // 从对象池返回第一个可用的GameObject
        public PooledObject GetPooledObject()
        {
            // 缺少objectToPool字段
            if (objectToPool == null)
            {
                return null;
            }

            // 如果对象池不够大，实例化额外的PooledObject
            if (stack.Count == 0)
            {
                PooledObject newInstance = Instantiate(objectToPool);
                newInstance.Pool = this;
                return newInstance;
            }

            // 否则，从列表中获取下一个对象
            PooledObject nextInstance = stack.Pop();
            nextInstance.gameObject.SetActive(true);
            return nextInstance;
        }

        // 将GameObject返回到对象池
        public void ReturnToPool(PooledObject pooledObject)
        {
            stack.Push(pooledObject);
            pooledObject.gameObject.SetActive(false);
        }
    }
}
