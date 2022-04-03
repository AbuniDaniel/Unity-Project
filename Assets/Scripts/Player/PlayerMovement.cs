using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;
    private void Awake()
    {
        instance= this;
    }
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y ,lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
    public IEnumerator Knockback(float knockbackDuration, float knockbackPower, Transform obj, Vector3 direction)
    {
        float timer=0;
        while (knockbackDuration> timer)
        {
            
            timer += Time.fixedDeltaTime;
            rb.AddForce( direction.normalized * knockbackPower , ForceMode2D.Force);
            yield return 0;
        }
        
    }
}