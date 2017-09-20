using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class CameraMoving : MonoBehaviour
    {
        public float speed = 5.5f;

        public Transform skullTransform;
        Skull skull;

        void Start()
        {
            skull = skullTransform.GetComponent<Skull>();
        }

        private void Update()
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);

            if (skull.HP == 0)
            {
                enabled = false;
            }
        }
    }
}
