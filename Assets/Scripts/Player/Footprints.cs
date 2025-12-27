using UnityEngine;

namespace OculusionLabyrinth.Player
{
    public class Footprints : MonoBehaviour
    {
        public bool IsWalking = false;

        [SerializeField] private GameObject _footprint;
        [SerializeField] private float _liveTime;
        [SerializeField] private float _spawnTime = 0.5f;
        
        private float timer;

        private void Update()
        {
            timer += Time.deltaTime;

            if (timer > _spawnTime && IsWalking)
            {
                timer = 0;
                GameObject fPrint = Instantiate(_footprint, transform.position, transform.rotation);
                Destroy(fPrint, _liveTime);
            }
        }
    }
}
