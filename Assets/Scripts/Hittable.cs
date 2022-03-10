using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hittable : MonoBehaviour
{
    [SerializeField] GameObject deathFx;
    [SerializeField] Transform parent;
    [SerializeField] bool scorable = false;
    [SerializeField] int scoreHit = 10;
    [SerializeField] int hitPoints = 1;

    Score score;
    // Start is called before the first frame update
    void Start()
    {
        score = FindObjectOfType<Score>();
        AddNonTriggerCollider();
    }

    void AddNonTriggerCollider()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
        // boxCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnParticleCollision(GameObject other)
    {
        if(scorable)
        {
            score.ScoreHit(scoreHit);
        }
        hitPoints--;
        if(hitPoints <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        GameObject fx = Instantiate(deathFx, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }
}
