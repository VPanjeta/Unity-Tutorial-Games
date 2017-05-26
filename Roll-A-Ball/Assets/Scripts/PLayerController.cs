using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PLayerController : MonoBehaviour {

    public float speed;

    public Text countText, winText;

    private int count;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        countText.text = "Collected :" + count.ToString();
        winText.text = "";
    }

    private void Update()
    {
        Vector3 dir = Vector3.zero;

        // we assume that the device is held parallel to the ground
        // and the Home button is in the right hand

        // remap the device acceleration axis to game coordinates:
        // 1) XY plane of the device is mapped onto XZ plane
        // 2) rotated 90 degrees around Y axis
        dir.x = -Input.acceleration.y;
        dir.z = Input.acceleration.x;

        // clamp acceleration vector to the unit sphere
        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        // Make it move 10 meters per second instead of 10 meters per frame...
        dir *= Time.deltaTime;

        // Move object
        transform.Translate(dir * speed);

        rb.AddForce(dir * speed);

    }


    // Update is called once per frame
    void FixedUpdate () {

        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);

	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pickup"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            countText.text = "Collected :" + count.ToString();
            if (count == 8)
                winText.text = "You Win";
        }
    }
}
