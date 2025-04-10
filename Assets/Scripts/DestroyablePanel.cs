using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestroyablePanel : BaseRewind
{
    [SerializeField] private RewindObject[] particlePrefabs;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private new Collider2D collider;
    [SerializeField] private LayerMask ballLayer;
    [SerializeField] private float maxWriteTime;

    private List<RewindObject> spawnedParticles = new();
    private float elapsedTime;
    private bool destroyed;

    void Update()
    {
        if (destroyed && elapsedTime < maxWriteTime) 
        {
            elapsedTime += Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (destroyed) 
        {
            return;
        }
        
        if ((ballLayer & (1 << c.gameObject.layer)) != 0) 
        {            
            destroyed = true;
            spriteRenderer.enabled = false;
            collider.enabled = false;

            foreach (var i in particlePrefabs) 
            {
                var obj = Instantiate(i, transform.position, Quaternion.identity);
                obj.Rigidbody.AddForce(Random.onUnitSphere, ForceMode2D.Impulse);
                spawnedParticles.Add(obj);
            }
        }
    }

    public override void StartRewind()
    {
        IEnumerator Rewind() 
        {
            if (elapsedTime < maxWriteTime)
            {
                yield break;
            }

            float time = elapsedTime, t = 0;
            while (t < time) 
            {
                t += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            
            destroyed = false;
            spriteRenderer.enabled = true;
            collider.enabled = true;
            spawnedParticles.ForEach(i => Destroy(i));
            spawnedParticles.Clear();
        }
        StartCoroutine(Rewind());
    }
}
