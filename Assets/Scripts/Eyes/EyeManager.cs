using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;
using TMPro;

namespace OculusionLabyrinth.Eyes
{
    public class EyeManager : MonoBehaviour
    {
        [SerializeField] private float _collectDistance;
        [SerializeField] private TextMeshProUGUI _gameTextName;
        [SerializeField] private TextMeshProUGUI _gameTextDescription;
        [SerializeField] private TextMeshProUGUI _gameTextNearEye;
        [SerializeField] private Transform _eyesPlace;
        [SerializeField] private Transform _throwPlace;

        private List<Eye> eyesOnMap;
        private Eye closestEye = null;
        private Eye currentEye = null;
        private Coroutine putOffEye;
        private Coroutine putOnEye;
        private Coroutine showDescText;

        private void Awake()
        {
            eyesOnMap = new List<Eye>();
            StartCoroutine(SlowedGameLoop());
        }

        private IEnumerator SlowedGameLoop()
        {
            while (true)
            {
                if (AreEyesNear())
                {
                    _gameTextNearEye.text = "Press 'E' to pick " + closestEye.Name + " up";
                }
                else
                {
                    _gameTextNearEye.text = "";
                }

                yield return new WaitForSeconds(0.05f);
            }
        }

        private IEnumerator MoveObject(GameObject objc, Transform pos, float time, float z = 0.9f, Coroutine corut = null)
        {
            if (z == 0.9f) z = Random.Range(0, 360);

            float timer = 0;

            while (timer < time)
            {
                timer += Time.deltaTime;
                objc.transform.position = Vector2.Lerp(objc.transform.position, pos.position, timer/time);
                objc.transform.rotation = Quaternion.Lerp(objc.transform.rotation, Quaternion.Euler(0, 0, z), timer/time);
                yield return null;
            }

            objc.transform.position = objc.transform.position;
            objc.transform.rotation = Quaternion.Euler(0, 0, z);
            if (corut != null) corut = null;
        }

        private bool AreEyesNear()
        {
            closestEye = null;
            float closestDistance = Mathf.Infinity;
            foreach (Eye e in eyesOnMap)
            {
                if (Vector2.Distance(transform.position, e.transform.position) < _collectDistance && Vector2.Distance(transform.position, e.transform.position) < closestDistance && e != currentEye)
                {
                    closestDistance = Vector2.Distance(transform.position, e.transform.position);
                    closestEye = e;
                }
            }

            return closestEye != null;
        }

        private IEnumerator ShowEyeDescription(Eye eye)
        {
            _gameTextName.gameObject.SetActive(true);
            
            _gameTextName.text = "";
            _gameTextDescription.text = "";

            foreach (char a in eye.Name)
            {
                _gameTextName.text += a;

                yield return new WaitForSeconds(0.03f);
            }

            yield return new WaitForSeconds(1f);

            _gameTextDescription.gameObject.SetActive(true);

            foreach (char a in eye.Description)
            {
                _gameTextDescription.text += a;

                yield return new WaitForSeconds(0.03f);
            }

            yield return new WaitForSeconds(5);

            _gameTextName.gameObject.SetActive(false);
            _gameTextDescription.gameObject.SetActive(false);

            showDescText = null;
        }

        public void AddSelfToManager(Eye eye)
        {
            eyesOnMap.Add(eye);
        }

        public void OnInteract(InputValue value)
        {
            if (closestEye == null) return;

            if (currentEye != null)
            {
                currentEye.EyeStop();
                currentEye.transform.parent = null;

                if (putOffEye != null)
                {
                    StopCoroutine(putOffEye);
                    putOffEye = null;
                }

                putOffEye = StartCoroutine(MoveObject(currentEye.transform.gameObject, _throwPlace, 0.5f, Random.Range(0, 360f), putOffEye));
            }

            closestEye.transform.parent = _eyesPlace.transform;
            
            if (putOnEye != null)
            {
                StopCoroutine(putOnEye);
                putOnEye = null;
            }

            putOnEye = StartCoroutine(MoveObject(closestEye.transform.gameObject, _eyesPlace.transform, 0.3f, _eyesPlace.transform.parent.rotation.eulerAngles.z - 90, putOnEye));

            print(_eyesPlace.transform.parent.rotation.eulerAngles.z + "   " + (_eyesPlace.transform.parent.rotation.eulerAngles.z - 90));

            if(showDescText != null)
            {
                StopCoroutine(showDescText);
            }

            showDescText = StartCoroutine(ShowEyeDescription(closestEye));

            closestEye.EyeStart();
            currentEye = closestEye;
            closestEye = null;
        }
    }
}
