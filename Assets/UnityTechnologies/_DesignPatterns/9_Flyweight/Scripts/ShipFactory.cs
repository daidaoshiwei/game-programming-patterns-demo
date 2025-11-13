using UnityEngine;

namespace DesignPatterns.Flyweight
{
    /// <summary>
    /// 
    /// </summary>
    public class ShipFactory : MonoBehaviour
    {
        [SerializeField] private Ship m_ShipPrefab;
        [SerializeField] private ShipData m_SharedData;
        
        [Header("布局")]
        [Tooltip("船只之间的间距")]
        [SerializeField] private float m_Spacing = 1.0f;
        [Tooltip("波浪运动的最大高度")]
        [SerializeField] private float m_Amplitude = 0.075f;
        [Tooltip("波浪运动的振荡速度")]
        [SerializeField] private float m_Frequency = 0.3f;

        void Start()
        {
            GenerateShips(10, 10);
        }

        public void GenerateShips(int rows, int columns)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    // 计算位置
                    Vector3 position = new Vector3(i * m_Spacing, 0, j * m_Spacing);

                    // 实例化并初始化船只
                    Ship newShip = Instantiate(m_ShipPrefab, position, Quaternion.identity, transform);
                    
                    // 设置初始生命值为100
                    newShip.Initialize(m_SharedData, 100);

                    // 可选的振荡运动
                    SineWaveMover oscillation = newShip.gameObject.AddComponent<SineWaveMover>();
                    oscillation.Amplitude = m_Amplitude;
                    oscillation.Frequency = m_Frequency;
                    
                    // 可选的名称
                    newShip.name = $"Ship_{i * columns + j}";
                    
                }
            }
        }
    }
}
