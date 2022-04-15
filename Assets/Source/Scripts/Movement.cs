using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float startForceStrength;
    protected float currentForceStrength;
    protected float gameTime = 0;
    private Rigidbody rb;
    private Vector3 horizontal;
    private Vector3 vertical;

    private void Start()
    {
        currentForceStrength = startForceStrength;
        rb = GetComponent<Rigidbody>();
    }

    protected void ResetRigidbody()
    {
        currentForceStrength = startForceStrength;
        rb.velocity = Vector3.zero;
    }

    void Move()
    {
        rb.AddForce((horizontal+vertical)*currentForceStrength);
    }

    protected void SetZeroForce()
    {
        currentForceStrength = 0;
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        gameTime += Time.deltaTime;
        horizontal=Vector3.zero;
        vertical = horizontal;
        if (Input.GetAxis("Horizontal") != 0)
        {
            horizontal = Vector3.right*Input.GetAxis("Horizontal");
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            vertical = Vector3.forward * Input.GetAxis("Vertical");
        }
    }
}
