using UnityEngine;

public class HeavySwordFollow : MonoBehaviour
{
    public Transform player;
    public float orbitRadius = 1.5f;
    public float rotateForce = 100f;
    public float maxSpeed = 10f;
    public float centeringForce = 100f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // Kılıçtan oyuncuya doğru vektör
        Vector2 toPlayer = (Vector2)player.position - rb.position;

        // Oyuncunun etrafında dairesel yön (tangente)
        Vector2 tangent = Vector2.Perpendicular(toPlayer).normalized;

        // Giriş kontrolü
        float input = 0f;
        if (Input.GetMouseButton(0)) input = -1f;
        if (Input.GetMouseButton(1)) input = 1f;

        // Dönme kuvveti uygula
        if (input != 0f)
        {
            Vector2 force = tangent * rotateForce * input;
            rb.AddForce(force);
        }

        // Orbit mesafesini korumak için yay kuvveti uygula
        float currentDistance = toPlayer.magnitude;
        float distanceError = currentDistance - orbitRadius;
        Vector2 correctionForce = toPlayer.normalized * (-distanceError * centeringForce);
        rb.AddForce(correctionForce);

        // Hız sınırı uygula
        if (rb.velocity.magnitude > maxSpeed)
            rb.velocity = rb.velocity.normalized * maxSpeed;

        // Dönüş açısı: kabza karaktere bakacak
        float angle = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg;
        rb.MoveRotation(angle + 180f);
    }
}
