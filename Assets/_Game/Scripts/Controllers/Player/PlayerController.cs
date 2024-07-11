using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
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
    private GameObject capsule;

    public static int Life = 3;

    private bool canWalk;
    private bool canLoseLife = true;

    public static Action OnPlayerDied;
    public static Action OnLoseLife;

    private async void Start()
    {
        Life = 3;
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
        if (other.tag.Equals("Enemy") && canLoseLife)
        {
            if(Life > 1)
            {
                Life--;
                Debug.Log($"Current Life is: {Life}");
                SetupDamage();
            }
            else
            {
                Life--;
                OnPlayerDied?.Invoke();
                Debug.Log($"Player Died");
            }
            OnLoseLife?.Invoke();
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

    private async void SetupDamage()
    {
        canLoseLife = false;
        capsule.SetActive(true);
        await Task.Delay(3000);
        canLoseLife = true;
        capsule.SetActive(false);
    }
}
