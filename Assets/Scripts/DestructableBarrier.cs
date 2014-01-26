using UnityEngine;
using System.Collections;

public class DestructableBarrier : MonoBehaviour {
    public float breakTime = 0.5f;
    public GameObject particleSystem;

    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach(ContactPoint2D contact in collision.contacts)
        {
            if (contact.collider.tag == "Player" && contact.collider.GetComponent<Character>().size == Character.Size.LARGE)
            {
                StartCoroutine(Collapse(contact));
            }
        }
    }

    IEnumerator Collapse(ContactPoint2D contact)
    {
        GameObject particles = Instantiate(particleSystem, contact.point, Quaternion.identity) as GameObject;
        particles.GetComponent<ParticleSystem>().Emit(50);
        particles.GetComponent<ParticleSystem>().renderer.sortingLayerName = "particles";
        yield return new WaitForSeconds(breakTime);
        Destroy(particles);
        Destroy(this.gameObject);
    }
}
