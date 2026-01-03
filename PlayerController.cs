using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    public CharacterController controller;
    public Animator animator;
    [Header("Movement")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float rotationSpeed = 10f;
    public float gravity = -9.8f;
    Vector3 velocity;
    [Header("Health")]
    public ControlledHealthSystem health_down;
    bool isDead = false;
    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public float fireRate = 0.2f;
    float nextFireTime;
    bool isShooting;
    [Header("Mobile Joystick")]
    public RectTransform joystickBG;
    public RectTransform joystickHandle;
    public float joystickRadius = 100f;
    Vector2 joystickInput;
    [Header("Touch Rotation")]
    public float touchRotationSpeed = 0.2f;
    float touchRotation;

    void Update()
    {
        if (isDead) return;
        HandleMovement();
        HandleTouchRotation();

        HandleShooting();
        if (health_down != null && health_down.totalHealthZero)
            Die();
    }
    void HandleMovement()
    {
        Vector3 direction = new Vector3(joystickInput.x, 0, joystickInput.y);

        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            float speed = walkSpeed; 
            controller.Move(direction.normalized * speed * Time.deltaTime);

            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        ApplyGravity();
        animator.SetBool("isIdle", !isShooting && direction.magnitude <= 0.1f);
    }
    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    void HandleShooting()
    {
        animator.SetBool("isShooting", isShooting);
        if (isShooting && Time.time >= nextFireTime)
        {
            ShootBullet();
            nextFireTime = Time.time + fireRate;
        }
    }
    void ShootBullet()
    {
        if (!bulletPrefab || !firePoint) return;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (!rb) rb = bullet.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = transform.forward * bulletSpeed;
        Destroy(bullet, 5f);
    }
    public void JoystickDrag(BaseEventData data)
    {
        PointerEventData ped = (PointerEventData)data;
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickBG,
            ped.position,
            ped.pressEventCamera,
            out pos
        );
        pos = Vector2.ClampMagnitude(pos, joystickRadius);
        joystickHandle.anchoredPosition = pos;
        joystickInput = pos.normalized;
    }
    public void JoystickRelease()
    {
        joystickInput = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero;
    }
    public void ShootDown()
    {
        isShooting = true;
    }
    public void ShootUp()
    {
        isShooting = false;
    }
    void Die()
    {
        animator.SetBool("isDead", true);
        isDead = true;
    }
    void HandleTouchRotation()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                float deltaX = touch.deltaPosition.x;
                touchRotation = deltaX * touchRotationSpeed;
            }
        }
        else
        {
            touchRotation = 0f;
        }
 
        transform.Rotate(Vector3.up, touchRotation);
    }

}
