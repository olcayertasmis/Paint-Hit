using Managers;
using UnityEngine;

namespace Ball_Scripts
{
    public class Ball : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Rigidbody rb;

        [Header("Sound")]
        [SerializeField] private AudioClip hit;

        private void Start()
        {
            Singleton.Instance.AudioManager.PlaySound(hit);
        }

        public void Thrown(float ballSpeed)
        {
            rb.AddForce(Vector3.forward * ballSpeed, ForceMode.Impulse);
        }

        public void ChangeColor(Color color)
        {
            meshRenderer.material.color = color;
        }
    }
}