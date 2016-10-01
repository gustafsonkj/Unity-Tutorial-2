using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float yMin, yMax, xMin, xMax;
}

public class PlayerComponent : MonoBehaviour {


    public float speed;
    public float tiltY;
    public float tiltX;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    private float nextFire;

    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            GetComponent<AudioSource>().Play();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical);
        GetComponent<Rigidbody>().velocity = movement * speed;

        GetComponent<Rigidbody>().position = new Vector3
            (
            Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp (GetComponent<Rigidbody>().position.y, boundary.yMin, boundary.yMax),
            0.0f
            );

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(
            GetComponent<Rigidbody>().velocity.y * -tiltY,
            0.0f,
            GetComponent<Rigidbody>().velocity.x * -tiltX);
    }


}
