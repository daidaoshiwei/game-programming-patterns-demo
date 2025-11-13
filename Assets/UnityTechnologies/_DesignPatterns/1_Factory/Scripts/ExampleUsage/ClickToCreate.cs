using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DesignPatterns.Factory
{
    public class ClickToCreate : MonoBehaviour
    {
        [SerializeField] 
        private LayerMask m_LayerToClick;

        [SerializeField] 
        private Vector3 m_Offset;

        [SerializeField] 
        private Factory[] m_Factories;

        // 用于跟踪所有已创建产品的列表
        private List<GameObject> m_CreatedProducts = new List<GameObject>();

        private void Update()
        {
            GetProductAtClick();
        }

        private void GetProductAtClick()
        {
            // 检查是否点击了鼠标左键
            if (Input.GetMouseButtonDown(0))
            {
                // 从列表中获取一个随机工厂
                Factory selectedFactory = m_Factories[Random.Range(0, m_Factories.Length)];
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                // 检查射线是否击中了我们想要点击的图层上的碰撞体
                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, m_LayerToClick) && selectedFactory != null)
                {
                    IProduct product = selectedFactory.GetProduct(hitInfo.point + m_Offset);
                    
                    // 将创建的产品GameObject添加到列表中
                    if (product is Component component) 
                    {
                        m_CreatedProducts.Add(component.gameObject);
                    }
                }
            }
        }
        
        private void OnDestroy()
        {
            foreach (GameObject product in m_CreatedProducts)
            {
                Destroy(product);
            }
            // 当对象被销毁时清空列表
            m_CreatedProducts.Clear(); 
        }
    }
}

