using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace Patterns.ObjectPool
{
    public class Zombie_2021 : MonoBehaviour
    {
        [SerializeField] private float timeoutDelay = 20f;

        private Rigidbody rb;
        private float speed;

        private IObjectPool<Zombie_2021> objectPool;

        public IObjectPool<Zombie_2021> ObjectPool { set =>  objectPool = value; }

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            speed = Random.Range(40f, 100f);
        }
        private void FixedUpdate()
        {
            rb.velocity = new Vector3(speed * Time.deltaTime, rb.velocity.y, rb.velocity.z);
        }
        IEnumerator DeactivateRoutine(float delay)
        {
            yield return new WaitForSeconds(delay);

            rb.velocity = new Vector3(0f, 0f, 0f);
            rb.angularVelocity = new Vector3(0f, 0f, 0f);

            objectPool.Release(this);
        }
        public void Deactvate()
        {
            StartCoroutine(DeactivateRoutine(timeoutDelay));
        }
    }
}