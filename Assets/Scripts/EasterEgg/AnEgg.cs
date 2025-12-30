using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OculusionLabyrinth.EasterEgg
{
    public class AnEgg : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _spikes;
        [SerializeField] private UnityEvent _event;

        private void Start ()
        {
            StartCoroutine(GameLoopSlow());
        }

        private IEnumerator GameLoopSlow()
        {
            while (true)
            {
                int c = 0;
                foreach (GameObject go in _spikes)
                {
                    if (go == null)
                    {
                        c++;
                    }
                }

                if(_spikes.Count <= c)
                {
                    _event.Invoke();
                }
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
