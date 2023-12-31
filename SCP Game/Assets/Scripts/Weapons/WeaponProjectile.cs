using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProjectile : MonoBehaviour
{
    public SpriteRenderer sprite;
    public AudioSource source;
    public AudioClip clip;
    [SerializeField] GameObject me;
    [SerializeField] float projectileSpeed = 5.0f;
    int damage;

    public void StartMoving(int d, bool isFacingRight)
    {
        damage = d;
        Vector3 v = this.transform.right;
        v *= projectileSpeed;
        if(isFacingRight)
            this.GetComponentInParent<Rigidbody2D>().AddForce(v);
        else
            this.GetComponentInParent<Rigidbody2D>().AddForce(-v);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Projectile")
        {
            return;
        }
        if(other.gameObject.tag == "Player")
        {
            if(other.gameObject.GetComponentInParent<PlayerHealth>())
                other.gameObject.GetComponentInParent<PlayerHealth>().TakeDamage();
        }
        else if(other.gameObject.tag == "EntitySpecial")
        {
            if(other.gameObject.GetComponentInParent<SCP106Barrier>())
                other.gameObject.GetComponentInParent<SCP106Barrier>().TakeDamage(damage);
        }
        else if(other.gameObject.tag == "Entity")
        {
            if(other.gameObject.GetComponentInParent<EnemyHealth>())
                other.gameObject.GetComponentInParent<EnemyHealth>().TakeDamage(damage);
        }
        
        StartCoroutine(DoDestruction());
    }

    IEnumerator DoDestruction()
    {
        Destroy(sprite);
        source.clip = clip;
        source.Play();
        yield return new WaitForSeconds(.4f);
        Destroy(me);
    }
}
