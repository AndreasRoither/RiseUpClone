using System;
using UnityEngine;

namespace Objects
{
    public class FloatingObject : MonoBehaviour
    {
        public enum Direction
        {
            Left,
            Right
        }

        public float speed = 1f;
        public Direction direction;

        private void Update()
        {
            if (direction == Direction.Left)
            {
                transform.position =
                    new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.y);
            }
            else
            {
                transform.position =
                    new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.y);
            }
        }
    }
}