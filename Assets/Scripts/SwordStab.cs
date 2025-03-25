using UnityEngine;

public class SwordManager : MonoBehaviour
{
    [Header("Referanslar")]
    public Rigidbody2D rb;
    public LayerMask groundLayer;

    [Header("Saplanma Ayarları")]
    public float stabSpeedThreshold = 8f;         // Saplanmak için gereken çarpma hızı
    public float unstuckLaunchForce = 20f;        // Çıkarken uygulanacak kuvvet (AddForce)
    public float unstuckDelay = 0.3f;             // Saplandıktan sonra çıkış izni süresi
    public Vector2 unstuckDirection = new Vector2(0.7f, 1f); // Yukarıya çapraz kuvvet yönü

    private bool isStuck = false;
    private float timeSinceStuck = 0f;

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isStuck) return;

        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            float impactSpeed = collision.relativeVelocity.magnitude;

            if (impactSpeed >= stabSpeedThreshold)
            {
                StickToGround();
            }
        }
    }

    void FixedUpdate()
    {
        if (!isStuck) return;

        timeSinceStuck += Time.fixedDeltaTime;
        if (timeSinceStuck < unstuckDelay) return;

        // E tuşuna basıldığında kılıcı yukarı doğru kuvvetle fırlat
        if (Input.GetKeyDown(KeyCode.E))
        {
            UnstickFromGround();
        }
    }

    void StickToGround()
    {
        isStuck = true;
        timeSinceStuck = 0f;

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // Kılıcı geçici olarak sabitleme (pozisyonu korusun)
        rb.bodyType = RigidbodyType2D.Kinematic;

        Debug.Log("🧱 Kılıç SAPLANDI!");
    }

    void UnstickFromGround()
    {
        isStuck = false;

        // Dinamik fiziğe geri dön
        rb.bodyType = RigidbodyType2D.Dynamic;

        // Kuvvet uygula (kontrollü ama güçlü hissiyat)
        Vector2 direction = unstuckDirection.normalized;
        rb.AddForce(direction * unstuckLaunchForce, ForceMode2D.Impulse);

        Debug.Log("💥 Kılıç FORCE ile fırladı!");
    }
}
