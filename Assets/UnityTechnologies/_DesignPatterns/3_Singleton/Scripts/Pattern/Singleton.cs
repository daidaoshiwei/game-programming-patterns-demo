using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace DesignPatterns.Singleton
{
    /// <summary>
    /// 为MonoBehaviour类型提供泛型单例模式的实现。
    /// 确保在应用程序中任何时候都只存在一个单例实例。
    /// 如果访问时找不到实例，此脚本会创建实例。
    /// </summary>
    /// <typeparam name="T">应该成为单例的MonoBehaviour类型。</typeparam>
    public class Singleton<T> : MonoBehaviour where T : Component
    {

        [Tooltip("延迟移除重复实例，直到显式调用（仅用于演示）。")]
        [SerializeField]
        private bool m_DelayDuplicateRemoval;


        private static T s_Instance;

        public static T Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = (T)FindFirstObjectByType(typeof(T));

                    if (s_Instance == null)
                    {
                        SetupInstance();
                    }
                    else
                    {
                        string typeName = typeof(T).Name;

                        Debug.Log("[Singleton] " + typeName + " instance already created: " +
                                  s_Instance.gameObject.name);
                    }
                }

                return s_Instance;
            }
        }

        public virtual void Awake()
        {
            // 为了演示目的，此标志可以延迟移除重复实例
            if (!m_DelayDuplicateRemoval)
                RemoveDuplicates();
        }

        private void OnEnable()
        {
            // 卸载当前场景时清除单例实例
            SceneManager.sceneUnloaded += SceneManager_SceneUnloaded;
        }

        private void OnDisable()
        {
            if (s_Instance == this as T)
            {
                SceneManager.sceneUnloaded -= SceneManager_SceneUnloaded;
            }
        }

        private static void SetupInstance()
        {
            // 延迟实例化
            s_Instance = (T)FindFirstObjectByType(typeof(T));

            if (s_Instance == null)
            {
                GameObject gameObj = new GameObject();
                gameObj.name = typeof(T).Name;

                s_Instance = gameObj.AddComponent<T>();
                DontDestroyOnLoad(gameObj);
            }
        }

        public void RemoveDuplicates()
        {
            if (s_Instance == null)
            {
                s_Instance = this as T;

                // 使用DontDestroyOnLoad使实例持久化，但需要手动清理/释放
                //DontDestroyOnLoad(gameObject);
            }
            else if (s_Instance != this)
            {
                Destroy(gameObject);
            }
        }

        // 事件处理方法
        
        // 卸载场景时销毁单例（仅用于演示）
        private void SceneManager_SceneUnloaded(Scene scene)
        {
            if (s_Instance != null)
                Destroy(s_Instance.gameObject);
            
            s_Instance = null;
        }
    }
}