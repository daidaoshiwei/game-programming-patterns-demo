using System;
using DesignPatterns.Utilities;
using UnityEngine;
using UnityEngine.Serialization;


namespace DesignPatterns.DirtyFlag
{
    /// <summary>
    /// 每个Sector根据与玩家的距离管理关卡特定部分内容的加载和卸载。
    ///
    /// 与GameSectors脚本配合工作，设置/取消设置脏标记以最小化不必要的更新。
    /// </summary>
    public class Sector : MonoBehaviour
    {
        [Header("场景资源")] [SerializeField]
        SceneLoader m_SceneLoader;

        [SerializeField] string m_ScenePath;

        [Tooltip("相对于变换位置的偏移")]
        public Vector3 m_CenterOffset;

        [Tooltip("加载的最小距离")] public float m_LoadRadius;

        [Header("可视化")] [Tooltip("当扇区内容已加载时使用的材质。")] [SerializeField]
        Material m_ActiveMaterial;

        [Tooltip("当扇区内容已卸载时使用的材质。")] [SerializeField]
        Material m_InactiveMaterial;

        // 用于可视化的MeshRenderer引用
        MeshRenderer m_MeshRenderer;

        // 属性
        public bool IsLoaded { get; private set; } = false;
        public bool IsDirty { get; private set; } = false;

        void Awake()
        {
            m_MeshRenderer = GetComponent<MeshRenderer>();
            m_SceneLoader = FindFirstObjectByType<SceneLoader>();

            if (m_SceneLoader == null)
            {
                Debug.LogError("[Sector]: SceneLoader not found in the scene.");
            }

            // 开始时重置脏标记
            Clean();

            IsLoaded = false;
        }

        // 将扇区标记为需要更新
        public void MarkDirty()
        {
            IsDirty = true;

            Debug.Log("Sector " + gameObject.name + " is marked dirty");
        }

        // 加载扇区内容
        public void LoadContent()
        {
            // 实现内容加载逻辑
            IsLoaded = true;

            if (m_MeshRenderer != null)
                m_MeshRenderer.material = m_ActiveMaterial;

            Debug.Log($"{gameObject.name} Loading sector content...");
            
            if (!string.IsNullOrEmpty(m_ScenePath))
                m_SceneLoader.LoadSceneAdditivelyByPath(m_ScenePath);
        }

        // 卸载扇区内容
        public void UnloadContent()
        {
            // 内容卸载逻辑
            IsLoaded = false;

            if (m_MeshRenderer != null)
                m_MeshRenderer.material = m_InactiveMaterial;

            m_SceneLoader.UnloadSceneByPath(m_ScenePath);
            Debug.Log("Unloading sector content...");
        }

        // 检查玩家是否足够接近以考虑加载此扇区
        public bool IsPlayerClose(Vector3 playerPosition)
        {
            return Vector3.Distance(playerPosition, transform.position + m_CenterOffset) <= m_LoadRadius;
        }

        // 更新后重置脏标记
        public void Clean()
        {
            IsDirty = false;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position + m_CenterOffset, m_LoadRadius);
        }

        void OnDestroy()
        {
            m_SceneLoader.UnloadSceneImmediately(m_ScenePath);
        }
    }
}