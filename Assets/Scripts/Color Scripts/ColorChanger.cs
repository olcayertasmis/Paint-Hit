using System;
using System.Collections;
using Handler_Scripts;
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

                target.gameObject.GetComponent<MeshRenderer>().enabled = true;
                target.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;

                gameObject.GetComponent<Rigidbody>().AddForce(Vector3.down * 50, ForceMode.Impulse);
                Destroy(gameObject, .5f);
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

        IEnumerator ChangeColor(GameObject target)
        {
            yield return new WaitForSeconds(0.1f);

            target.GetComponent<MeshRenderer>().enabled = true;
            target.GetComponent<MeshRenderer>().material.color = BallHandler.ballColor;
            Destroy(gameObject);
        }
    }
}