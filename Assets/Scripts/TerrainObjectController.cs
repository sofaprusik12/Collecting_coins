using UnityEngine;

public class TerrainObjectController : MonoBehaviour
{
    public GameObject particle;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            particle.active = !particle.active;
        }
    }
}
