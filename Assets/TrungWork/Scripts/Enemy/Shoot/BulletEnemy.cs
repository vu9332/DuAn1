using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [Header("BulletCurve")]
    [SerializeField] private float duration = 1f;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float heightY;
    [SerializeField] private GameObject SpllaterPrefabs;
    [Header("Damage")]
    [SerializeField] private GrapeProjectitle grapeProjectitle;
    private float damage;
    private PlayerHealth playerHealth;
    private bool playerGetHit=false;
    private void Awake()
    {
        playerHealth=GameObject.FindAnyObjectByType<PlayerHealth>().GetComponent<PlayerHealth>();
    }
    private void Start()
    {
        Vector3 playerPos = PlayerController.Instance.transform.position;
        StartCoroutine(ProjectitleCurveRoutine(transform.position,playerPos));
        damage=grapeProjectitle.damageAttack;
    }
    private IEnumerator ProjectitleCurveRoutine(Vector3 startPosition,Vector3 endPosition)
    {
        float timePassed = 0f;
        while (timePassed < duration)
        {
            timePassed+= Time.deltaTime;
            float linearT=timePassed/duration;
            float heightT=animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);
            transform.position=Vector2.Lerp(startPosition, endPosition, linearT) + new Vector2(0f,height);
            yield return null;
        }
        if (!playerGetHit)
        {
            Instantiate(SpllaterPrefabs, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.GetComponent<PlayerController>())
        {
            playerHealth.TakeDamage(damage);
            playerGetHit = true;
            Debug.Log("Mau Player con: " + playerHealth.currentHealth);
        }
    }
}
