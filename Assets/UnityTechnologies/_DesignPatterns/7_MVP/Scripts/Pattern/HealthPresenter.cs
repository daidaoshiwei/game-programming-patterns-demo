using DesignPatterns.Utilities;
using UnityEngine;
using UnityEngine.UI;


namespace DesignPatterns.MVP
{
    // Presenter（表现层）。监听用户界面中View的变化并相应地操作Model（Health）。
    // 当Model发生变化时，Presenter会更新View。

    public class HealthPresenter : MonoBehaviour
    {
        [Header("模型")]
        [Tooltip("包含生命值数据的对象")]
        [SerializeField] Health m_Health;

        [Header("视图")]
        [Tooltip("表示生命值条的UI滑块")]
        [SerializeField] Slider m_HealthSlider;
        [Optional]
        [SerializeField] Text m_HealthLabel;

        private void Awake()
        {
            NullRefChecker.Validate(this);
        }

        private void Start()
        {
            m_Health.HealthChanged += Health_HealthChanged;
            InitializeSlider();

            Reset();
            UpdateView();
        }

        private void OnDestroy()
        {
            m_Health.HealthChanged -= Health_HealthChanged;
        }

        private void InitializeSlider()
        {
            m_HealthSlider.maxValue = m_Health.MaxHealth;
        }

        // 向模型发送伤害
        public void Damage(int amount)
        {
            m_Health.Decrement(amount);
        }

        public void Heal(int amount)
        {
            m_Health.Increment(amount);
        }

        // 向模型发送重置
        public void Reset()
        {
            m_Health.Restore();
        }

        public void UpdateView()
        {
            if (m_Health == null)
                return;

            // 为视图格式化数据
            if (m_Health.MaxHealth != 0)
            {
                m_HealthSlider.value = ((float)m_Health.CurrentHealth / (float)m_Health.MaxHealth) *100f;
            }

            if (m_HealthLabel != null)
            {
                m_HealthLabel.text = m_Health.CurrentHealth.ToString();
            }
        }

        // 事件处理方法；监听模型变化并更新视图
        public void Health_HealthChanged()
        {
            UpdateView();
        }
    }
}
