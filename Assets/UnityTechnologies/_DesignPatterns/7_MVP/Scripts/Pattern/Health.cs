using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DesignPatterns.MVP
{
    // Model（模型层）。包含MVP模式的数据。也可以是System.Object或ScriptableObject。
    public class Health : MonoBehaviour
    {
        // 此事件通知Presenter生命值已发生变化。
        // 如果设置值（例如保存到磁盘或存储到数据库）需要一些时间，这很有用。
        public event Action HealthChanged;

        private const int k_MinHealth = 0;
        private const int k_MaxHealth = 100;
        private int m_CurrentHealth;

        // 属性
        public int CurrentHealth
        {
            get => m_CurrentHealth;
            set
            {
                m_CurrentHealth = Mathf.Clamp(value, k_MinHealth, k_MaxHealth);
                HealthChanged?.Invoke();
            }
        }
        public int MinHealth => k_MinHealth;
        public int MaxHealth => k_MaxHealth;



        public void Increment(int amount)
        {
            CurrentHealth += amount;
        }

        public void Decrement(int amount)
        {
            CurrentHealth -= amount;
        }

        // 将生命值设置为最大值
        public void Restore()
        {
            CurrentHealth = k_MaxHealth;
        }
    }
}
