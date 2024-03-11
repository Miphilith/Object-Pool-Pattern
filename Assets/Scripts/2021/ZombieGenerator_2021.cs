using UnityEngine;
using UnityEngine.Pool;

namespace Patterns.ObjectPool
{
    public class ZombieGenerator_2021 : MonoBehaviour
    {
        [SerializeField] private Zombie_2021 zombiePrefab;

        private IObjectPool<Zombie_2021> objectPool;

        [SerializeField] private bool collectionCheck = true;
        [SerializeField] private int defaultCapacity = 20;
        [SerializeField] private int maxSize = 100;

        private Zombie_2021 Createzombie()
        {
            Zombie_2021 zombieInstance = Instantiate(zombiePrefab);
            zombieInstance.ObjectPool = objectPool;
            return zombieInstance;
        }

        private void OnGetFromPool(Zombie_2021 pooledObject)
        {
            pooledObject.gameObject.SetActive(true);
        }

        private void OnReleaseToPool(Zombie_2021 pooledObject)
        {
            pooledObject.gameObject.SetActive(false);
        }

        private void OnDestroyPooledObject(Zombie_2021 pooledObject)
        {
            Destroy(pooledObject.gameObject);
        }

        private void Awake()
        {
            objectPool = new ObjectPool<Zombie_2021>(Createzombie,
                OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
                collectionCheck, defaultCapacity, maxSize);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && objectPool != null)
            {
                Zombie_2021 zombieObject = objectPool.Get();

                if (zombieObject == null)
                    return;    
                
                zombieObject.transform.position = GenerateRandomPosition();

                zombieObject.Deactvate();
            }
        }
        private Vector3 GenerateRandomPosition()
        {
            float randZ = Random.Range(0f, -8f);
            return new Vector3(-13f, 0f, randZ);
        }
    }
}