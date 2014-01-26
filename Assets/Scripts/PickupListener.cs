using UnityEngine;
using System.Collections;

public class PickupListener : MonoBehaviour
{

    public enum CandyType
    {
        THIN, FAT
    }

    public CandyType candyType = CandyType.FAT;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            GameObject.Destroy(gameObject);
            if (candyType == CandyType.FAT) collider.gameObject.GetComponent<Character>().makeFatter();
            else if (candyType == CandyType.THIN) collider.gameObject.GetComponent<Character>().makeThinner();
        }
    }
}
