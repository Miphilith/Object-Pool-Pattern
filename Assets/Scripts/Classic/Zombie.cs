using System.Collections;
using UnityEngine;

namespace Patterns.ObjectPool
{
    [RequireComponent(typeof(PooledObject))]
    [RequireComponent(typeof(Rigidbody))]
    public class Zombie : MonoBehaviour
    {
        [SerializeField] private float timeoutDelay = 20f;
        private Rigidbody rb;
        private float speed;
        private PooledObject pooledObject;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            pooledObject = GetComponent<PooledObject>();
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

            pooledObject.Release();
            gameObject.SetActive(false);
        }
        public void Deactvate()
        {
            StartCoroutine(DeactivateRoutine(timeoutDelay));
        }
    }
}
 
