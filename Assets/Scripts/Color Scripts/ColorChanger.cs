using System.Collections;
using Handler_Scripts;
using Managers;
using UnityEngine;

namespace Color_Scripts
{
    public class ColorChanger : MonoBehaviour
    {
        [Header("Managers")]
        private GameManager _gameManager;
        private BallHandler _ballHandler;
        private LevelsHandler _levelsHandler;

        [Header("Sound")]
        [SerializeField] private AudioClip hit;

        private void Start()
        {
            _gameManager = Singleton.Instance.GameManager;
            _ballHandler = _gameManager.GetBallHandler();
            _levelsHandler = _gameManager.GetLevelHandler();
        }

        private void OnCollisionEnter(Collision target)
        {
            if (target.gameObject.CompareTag("Red"))
            {
                gameObject.GetComponent<Collider>().enabled = false;

                var targetMesh = target.gameObject.GetComponent<MeshRenderer>();
                targetMesh.enabled = true;
                targetMesh.material.color = Color.red;

                gameObject.GetComponent<Rigidbody>().AddForce(Vector3.down * 50, ForceMode.Impulse);

                StartCoroutine(BallDestroy(gameObject, .5f));

                _gameManager.GetHealthHandler().DecreaseHeart(1);
            }
            else
            {
                _ballHandler.accurateBall++;

                gameObject.GetComponent<Collider>().enabled = false;

                GameObject splash = Instantiate(Resources.Load("splash1"), target.gameObject.transform, true) as GameObject;
                Destroy(splash, 0.1f);

                target.gameObject.name = "color";
                target.gameObject.tag = "Red";

                StartCoroutine(ChangeColor(target.gameObject));

                if (_ballHandler.accurateBall == _levelsHandler.level) _ballHandler.StartCoroutine(_ballHandler.InvokeFromLevelsHandler());
            }
        }

        private IEnumerator ChangeColor(GameObject target)
        {
            yield return new WaitForSeconds(0.1f);

            var targetMesh = target.GetComponent<MeshRenderer>();
            targetMesh.enabled = true;
            targetMesh.material.color = _ballHandler.ballColor;

            Singleton.Instance.AudioManager.PlaySound(hit);

            StartCoroutine(BallDestroy(gameObject, 0));
        }

        private IEnumerator BallDestroy(GameObject ball, float timer)
        {
            yield return new WaitForSeconds(timer);

            Singleton.Instance.ObjectPool.ReturnToPool(0, ball);
        }
    }
}