using UnityEngine;

public class CarDoorController : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ToggleDoors()
    {
        isOpen = !isOpen;
        animator.SetBool("IsOpen", isOpen);
    }
}
