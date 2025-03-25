using UnityEngine;

public class SwordMouseOrbit : MonoBehaviour
{
    public Transform player;
    public float orbitRadius = 1.5f;
    public float followSpeed = 10f;

    private float currentAngle;

    void Start()
    {
        // Ba�lang�� a��s�
        currentAngle = 0f;
    }

    void Update()
    {
        if (player == null) return;

        // Fare pozisyonunu al
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mousePos - (Vector2)player.position;

        // A��y� hesapla (radyan)
        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // A�� fark�n� d�zg�n �ekilde yakala (s�reklilik)
        currentAngle = Mathf.LerpAngle(currentAngle, targetAngle, followSpeed * Time.deltaTime);

        // Yeni pozisyon (karakter etraf�nda belirli bir mesafede)
        Vector2 offset = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad)) * orbitRadius;
        transform.position = (Vector2)player.position + offset;

        // K�l�� y�n� fareye baks�n (ucu mouse'a do�ru d�ns�n)
        transform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }
}
