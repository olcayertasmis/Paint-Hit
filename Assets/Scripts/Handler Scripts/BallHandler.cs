using UnityEngine;

namespace Handler_Scripts
{
    public class BallHandler : MonoBehaviour
    {
        [Header("Circle")]
        public static float RotationSpeed = 75f;
        private int _circleNo;

        [Header("Ball")]
        public static Color OneColor = Color.green;
        public GameObject ball;
        private int _ballsCount;

        private float _speed = 100f;

        void Start()
        {
            MakeANewCircle();
        }

        void Update()
        {
            if (Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.Space))
            {
                HitBall();
            }
        }

        public void HitBall()
        {
            if (_ballsCount <= 1)
            {
                Invoke(nameof(MakeANewCircle), .4f);
            }

            GameObject spawnedBall = Instantiate(ball, new Vector3(0, 0, -8), Quaternion.identity);
            spawnedBall.GetComponent<MeshRenderer>().material.color = OneColor;
            spawnedBall.GetComponent<Rigidbody>().AddForce(Vector3.forward * _speed, ForceMode.Impulse);
        }

        private void MakeANewCircle()
        {
            GameObject newCircle = Instantiate(Resources.Load("round" + Random.Range(1, 5))) as GameObject;
            newCircle.transform.position = new Vector3(0, 20, 23);
            newCircle.name = "Circle";
        }
    }
}