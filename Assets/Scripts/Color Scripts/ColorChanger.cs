using System.Collections;
using Handler_Scripts;
using Managers;
using UnityEngine;

namespace Color_Scripts
{
    public class ColorChanger : MonoBehaviour
    {
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
            }
            else
            {
                gameObject.GetComponent<Collider>().enabled = false;

                GameObject splash = Instantiate(Resources.Load("splash1"), target.gameObject.transform, true) as GameObject;
                Destroy(splash, 0.1f);

                target.gameObject.name = "color";
                target.gameObject.tag = "Red";

                StartCoroutine(ChangeColor(target.gameObject));
            }
        }

        private IEnumerator ChangeColor(GameObject target)
        {
            yield return new WaitForSeconds(0.1f);

            var targetMesh = target.GetComponent<MeshRenderer>();
            targetMesh.enabled = true;
            targetMesh.material.color = Singleton.Instance.GameManager.ballHandler.ballColor;

            StartCoroutine(BallDestroy(gameObject, 0));
        }

        private IEnumerator BallDestroy(GameObject ball, float timer)
        {
            yield return new WaitForSeconds(timer);

            Singleton.Instance.ObjectPool.ReturnToPool(0, ball);
        }
    }
}