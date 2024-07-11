using System;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Animator Animator;

    [SerializeField]
    private CharacterController Controller;

    [SerializeField]
    [Range(0f, 10f)]
    private float Speed = 3.5f;

    [SerializeField]
    [Range(0, 10)]
    private int Gravity = 50;

    [SerializeField]
    [Range(0, 10)]
    private int Life = 1;

    private bool canWalk;

    public static Action OnPlayerDied;

    private async void Start()
    {
        canWalk = false;
        await Task.Delay(3000);
        canWalk = true;
    }

    private void Update()
    {
        if(canWalk)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Rotate(horizontal, vertical);

            Controller.Move(GetMoveDirection(horizontal, vertical));

            var isMoving = horizontal != 0 || vertical != 0;
            SetAnimation(isMoving);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            if(Life > 1)
            {
                Life--;
                Debug.Log($"Current Life is: {Life}");
            }
            else
            {
                OnPlayerDied?.Invoke();
                Debug.Log($"Player Died");
            }
        }
    }

    private void Rotate(float horizontal, float vertical)
    {
        var angle = Mathf.Atan2(Math.Sign(horizontal), Math.Sign(vertical)) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(transform.rotation.x, angle, transform.rotation.z);
    }

    private void SetAnimation(bool isMooving)
    {
        if (isMooving)
            Animator.Play("Slow Run");
        else
            Animator.Play("Idle");
    }

    private Vector3 GetMoveDirection(float horizontal, float vertical)
    {
        var speedFactor = horizontal != 0 && vertical != 0 ? Mathf.Sqrt(2) / 2f : 1f;

        var x = Speed * speedFactor * horizontal * Time.deltaTime;
        var y = -(Gravity * Time.deltaTime);
        var z = Speed * speedFactor * vertical * Time.deltaTime;

        return new Vector3(x, y, z);
    }
}
