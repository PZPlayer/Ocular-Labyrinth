using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;

namespace OculusionLabyrinth.Player
{
    public class Health : MonoBehaviour
    {
        public bool CanDestroyRays = false;

        [SerializeField] private string _levelGoWin;
        [SerializeField] private UnityEvent _onDeath;
        [SerializeField] private UnityEvent _onWin;

        private Coroutine corut;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Death"))
            {
                if(CanDestroyRays) { Destroy(other.gameObject); return;  }
                _onDeath.Invoke();
                if (corut != null) return;
                corut = StartCoroutine(WaitFor(1.5f));
            }

            if (other.CompareTag("Win"))
            {
                _onWin.Invoke();
                if (corut != null) return;
                corut = StartCoroutine(WaitFor(1.5f, true));
            }
        }

        private IEnumerator WaitFor(float delay, bool win = false)
        {
            yield return new WaitForSeconds(delay);

            if(win)
            {
                ChangeScene(_levelGoWin);
                yield break;
            }

            ChangeScene();
        }

        public void RayDestroye(bool can) { CanDestroyRays = can; }

        public void ChangeScene(string level = "")
        {
            print("Going to " + level);
            SceneManager.LoadScene(level == "" ? SceneManager.GetActiveScene().name : level);
        }
    }
}

