using UnityEngine;
using UnityEngine.Events;

namespace OculusionLabyrinth.Eyes
{
    public class Eye : MonoBehaviour
    {
        public string Name;
        public string Description;

        [SerializeField] private UnityEvent _onStart;
        [SerializeField] private UnityEvent _onEnd;
        [SerializeField] private EyeManager _manager;

        private void Start()
        {
            _manager.AddSelfToManager(this);
        }

        public virtual void EyeStart()
        {
            _onStart?.Invoke();
        }

        public virtual void EyeStop()
        {
            _onEnd?.Invoke();
        }
    }
}
