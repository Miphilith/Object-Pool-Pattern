using UnityEngine;

namespace Patterns.ObjectPool
{
    public class ZombieGenerator : MonoBehaviour
    {
        [SerializeField] ObjectPool objectPool;
        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && objectPool != null)
            {
                GameObject zombieObject = objectPool.GetPooledObject().gameObject;
                if (zombieObject == null)
                {
                    return;
                }
                zombieObject.SetActive(true);
                zombieObject.transform.position = GenerateRandomPosition();
                Zombie zombie = zombieObject.GetComponent<Zombie>();
                zombie?.Deactvate();
            }
        }
        private Vector3 GenerateRandomPosition()
        {
            float randZ = Random.Range(0f, -8f);
            return new Vector3(-13f, 0f, randZ);
        }
    }  
}

    
