using UnityEngine;
using System.Collections;

public class PickupListener : MonoBehaviour
{
    bool disablerenderernext = false;

    public enum CandyType
    {
        THIN, FAT
    }

    public CandyType candyType = CandyType.FAT;

    void Update()
    {
        if (disablerenderernext)
        {
            disablerenderernext = false;
            gameObject.renderer.enabled = false;
        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player" && gameObject.renderer.enabled)
        {
            //GameObject.Destroy(gameObject);
            disablerenderernext = true;
            if (candyType == CandyType.FAT) collider.gameObject.GetComponent<Character>().makeFatter();
            else if (candyType == CandyType.THIN) collider.gameObject.GetComponent<Character>().makeThinner();
        }
    }

    public void refresh()
    {
        gameObject.renderer.enabled = true;
    }
}
