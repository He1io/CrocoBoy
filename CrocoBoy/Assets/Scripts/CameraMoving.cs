using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class CameraMoving : MonoBehaviour
    {
        public float speed = 5.5f;
        private void Update()
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);

            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && GameObject.FindGameObjectsWithTag("Spike").Length == 0)
            {
                enabled = false;
            }
        }
    }
}
