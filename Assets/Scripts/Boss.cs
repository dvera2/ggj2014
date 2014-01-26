using UnityEngine;

public class Boss : MonoBehaviour {
    public float projectileDelay = 2f;
    private float delayTimer = 0f;
    public Transform projectile;
	
	// Update is called once per frame
	void Update () {
        delayTimer += Time.deltaTime;
        if(delayTimer >= projectileDelay)
        {
            delayTimer = 0f;
            Instantiate(projectile, transform.position, Quaternion.identity);
        }
	}
}
