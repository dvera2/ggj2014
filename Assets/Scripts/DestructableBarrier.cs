using UnityEngine;
using System.Collections;

public class DestructableBarrier : MonoBehaviour {
    public float breakTime = 2f;
    public GameObject particleSystemPrefab;
	public AudioClip crumbleAudio;

	private bool _collapsing = false;

	void Start()
	{
		_collapsing = false;
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
		if (_collapsing)
			return;

        foreach(ContactPoint2D contact in collision.contacts)
        {
            if (contact.collider.tag == "Player" && contact.collider.GetComponent<Character>().size == Character.Size.LARGE)
            {
                Collapse(contact);

				contact.collider.GetComponent<AudioSource>().PlayOneShot(crumbleAudio);
            }
        }
    }

    void Collapse(ContactPoint2D contact)
    {
		_collapsing = true;

        GameObject particles = Instantiate(particleSystemPrefab, contact.point, Quaternion.identity) as GameObject;
        particles.GetComponent<ParticleSystem>().Emit(50);
        particles.GetComponent<ParticleSystem>().renderer.sortingLayerName = "particles";
        Destroy(particles, breakTime);
        Destroy(this.gameObject, .2f);
    }
}
