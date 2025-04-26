using DG.Tweening;
using UnityEngine;

public class BallScript : MonoBehaviour, IPooledProjectile
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 5f;
    private float timeAlive = 0f;
    private Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        transform.DOLocalRotate(new Vector3(0, 0, 360), 0.5f).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }

    private void OnDisable()
    {
        transform.DOKill();
    }
    private void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive >= lifetime)
        {
            ObjectPoolManager.Instance.ReturnToPool(gameObject.tag, gameObject);
        }
    }
    public void OnObjectSpawn()
    {
        timeAlive = 0f;
        rb.linearVelocity = transform.up * speed;
    }
    // This method is called when the ball collides with another object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ObjectPoolManager.Instance.ReturnToPool(gameObject.tag, gameObject);
    }
    // This method is called when the ball enters a trigger collider
}
  

