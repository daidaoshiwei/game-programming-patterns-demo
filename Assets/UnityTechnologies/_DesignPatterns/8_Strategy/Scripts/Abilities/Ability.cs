using UnityEngine;
using DesignPatterns.Singleton;


namespace DesignPatterns.Strategy
{
    public abstract class Ability : ScriptableObject
    {
        [SerializeField] protected string m_AbilityName;

        [Tooltip("UI按钮的图像纹理")]
        [SerializeField] protected Sprite m_ButtonIcon;
        [Header("视觉效果")]
        [SerializeField] protected ParticleSystem m_ParticleSystem;
        [SerializeField] protected AudioClip m_AudioClip;
        
        // 每个策略可以使用自定义逻辑。在子类中实现Use方法
        public virtual void Use(GameObject gameObject)
        {
            // Use方法记录名称、播放声音和粒子效果
            Debug.Log($"Using ability: {m_AbilityName}");
            PlaySound();
            PlayParticleFX();
        }

        public Sprite ButtonIcon => m_ButtonIcon;
        
        private void PlayParticleFX()
        {
            if (m_ParticleSystem != null)
            {
                ParticleSystem instance = Instantiate(m_ParticleSystem, Vector3.zero, Quaternion.identity);
                // 确保粒子系统先停止再播放
                instance.Stop();
                instance.Play();
            }
        }

        private void PlaySound()
        {
            if (m_AudioClip)
                AudioManager.Instance.PlaySoundEffect(m_AudioClip);
        }
    }



}