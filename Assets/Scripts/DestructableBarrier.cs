using UnityEngine;
using System.Collections;

public class DestructableBarrier : MonoBehaviour {
    public float breakTime = 0.5f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach(ContactPoint2D contact in collision.contacts)
        {
            if (contact.collider.tag == "Player" && contact.collider.GetComponent<Character>().size == Character.Size.LARGE)
            {
                StartCoroutine(Collapse());
            }
        }
    }

    IEnumerator Collapse()
    {
        animation.Play();
        yield return new WaitForSeconds(breakTime);
        Destroy(this.gameObject);
    }
}
