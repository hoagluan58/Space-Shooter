using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float xRange = 0.87f;
    private float yTopRange = -0.23f;
    private float yBottomRange = -2.25f;
    private float canFire = 0.5f;

    public bool gameOver = false;
    public bool hasPowerUp = false;
    [SerializeField] float speed = 2.5f;
    [SerializeField] float fireRate = 0.3f;

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject explosionPrefab;

    void Update()
    {
        MovePlayer();
        Shoot();
    }

    void MovePlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        // Set left/right bound
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);

        verticalInput = Input.GetAxis("Vertical");
        // Set top/bottom bound
        if (transform.position.y > yTopRange)
        {
            transform.position = new Vector3(transform.position.x, yTopRange, transform.position.z);
        }
        if (transform.position.y < yBottomRange)
        {
            transform.position = new Vector3(transform.position.x, yBottomRange, transform.position.z);
        }
        transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);
    }

    void Shoot()
    {
        if (Input.GetButton("Fire1") && Time.time > canFire)
        {
            //Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
            GameObject pooledProjectile = ObjectPooler.SharedInstance.GetPooledObject();
            if (pooledProjectile != null)
            {
                pooledProjectile.SetActive(true);
                pooledProjectile.transform.position = transform.position;
            }
            if (hasPowerUp == true)
            {
                canFire = Time.time + 0.1f;
            }
            else
            {
                canFire = Time.time + fireRate;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyProjectile"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
            gameOver = true;
        }
        if (other.gameObject.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            hasPowerUp = true;
            StartCoroutine(PowerUpCountDownRoutine());
        }
    }

    IEnumerator PowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(5);
        hasPowerUp = false;
    }
}
