using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float canFire = 0.5f;
    private MainManager mainManager;
    public int scoreValue;
    [SerializeField] float fireRate = 1.0f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject explosionPrefab;

    void Start()
    {
        mainManager = GameObject.Find("Main Manager").GetComponent<MainManager>();
    }
    void Update()
    {
        if (Time.time > canFire)
        {
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
            canFire = Time.time + fireRate;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerProjectile"))
        {
            Destroy(gameObject);
            //Destroy(other.gameObject);
            other.gameObject.SetActive(false);
            mainManager.UpdateScore(scoreValue);
            Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
        }
    }
}
