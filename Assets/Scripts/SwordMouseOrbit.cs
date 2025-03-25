using UnityEngine;

public class SwordMouseOrbit : MonoBehaviour
{
    public Transform player;
    public float orbitRadius = 1.5f;
    public float followSpeed = 10f;

    private float currentAngle;

    void Start()
    {
        // Baþlangýç açýsý
        currentAngle = 0f;
    }

    void Update()
    {
        if (player == null) return;

        // Fare pozisyonunu al
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mousePos - (Vector2)player.position;

        // Açýyý hesapla (radyan)
        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // Açý farkýný düzgün þekilde yakala (süreklilik)
        currentAngle = Mathf.LerpAngle(currentAngle, targetAngle, followSpeed * Time.deltaTime);

        // Yeni pozisyon (karakter etrafýnda belirli bir mesafede)
        Vector2 offset = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad)) * orbitRadius;
        transform.position = (Vector2)player.position + offset;

        // Kýlýç yönü fareye baksýn (ucu mouse'a doðru dönsün)
        transform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }
}
