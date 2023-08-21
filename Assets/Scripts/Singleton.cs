using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuikaDev
{
    /// <summary>
    /// アマガミナさんの動画を参考にしました。
    /// URL：https://www.youtube.com/watch?v=0LC5BgwPKOc&t=497s
    /// </summary>
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        /// <summary>
        /// 破棄時にゲームオブジェクトごと破棄するか
        /// </summary>
        public virtual bool IsDestroyGameObject => true;

        public virtual bool IsDestroyFollowing => true;

        public static T Instance;

        /// <summary>
        /// シングルトンが有効か
        /// </summary>
        public static bool IsValid => Instance != null;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
                Instance.OverrideAwake();
            }
            else if (IsDestroyGameObject)
            {
                if (IsDestroyFollowing)
                {
                    Destroy(gameObject);
                }
                else
                {
                    Destroy(Instance.gameObject);
                    Instance.OverrideAwake();
                }
            }
            else
            {
                if (IsDestroyFollowing)
                {
                    Destroy(this);
                }
                else
                {
                    Destroy(Instance);
                    Instance.OverrideAwake();
                }
            }
        }

        /// <summary>
        /// 派生クラス用のAwake
        /// </summary>
        protected virtual void OverrideAwake() { }

        public void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        /// <summary>
        /// 派生クラス用のOnDestroy
        /// </summary>
        protected virtual void OverrideDestory() { }
    }
}