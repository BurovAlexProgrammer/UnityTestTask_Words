using UnityEngine;

namespace UI.Effects
{
    public class LoopRotate : MonoBehaviour
    {
        [SerializeField] private float _rotateSpeed = 360;
        private Transform _transform;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            _transform = transform;
        }

        // Update is called once per frame
        void Update()
        {
            _transform.Rotate(Vector3.forward * (_rotateSpeed * Time.deltaTime));
        }
    }
}
