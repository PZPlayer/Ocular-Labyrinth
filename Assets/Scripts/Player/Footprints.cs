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
        private int turn = 0;

        private void Update()
        {
            timer += Time.deltaTime;

            if (timer > _spawnTime && IsWalking)
            {
                turn += 1;
                if( turn == 2 ) { turn = 0; }
                timer = 0;
                print(turn);
                GameObject fPrint = Instantiate(_footprint, transform.position, transform.rotation);
                fPrint.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer> ().flipX = 1 == turn ? true : false;
                Destroy(fPrint, _liveTime);
            }
        }
    }
}
