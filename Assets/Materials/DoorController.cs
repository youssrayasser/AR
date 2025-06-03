using UnityEngine;

public class LamborghiniDoorController : MonoBehaviour
{
    public Animator leftDoorAnimator;
    public Animator rightDoorAnimator;

    public void OpenLeftDoor()
    {
        leftDoorAnimator.SetTrigger("OpenLeftDoor");
    }

    public void CloseLeftDoor()
    {
        leftDoorAnimator.SetTrigger("CloseLeftDoor");
    }

    public void OpenRightDoor()
    {
        rightDoorAnimator.SetTrigger("OpenRightDoor");
    }

    public void CloseRightDoor()
    {
        rightDoorAnimator.SetTrigger("CloseRightDoor");
    }
}
