using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public GameObject particleSystemPrefab;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("test");
        if (collision.tag == "Player" && renderer.enabled)
        {
            GameObject particles = Instantiate(particleSystemPrefab, transform.position, Quaternion.identity) as GameObject;
            particles.GetComponent<ParticleSystem>().Emit(50);
            particles.GetComponent<ParticleSystem>().renderer.sortingLayerName = "particles";
            Destroy(particles, 0.5f);
        }
    }
}
