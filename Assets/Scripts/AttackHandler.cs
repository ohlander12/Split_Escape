using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    public GameObject attackHitbox;

    private void Start()
    {
        if (attackHitbox != null)
            attackHitbox.SetActive(false);
    }

    public void EnableAttackHitbox()
    {
        if (attackHitbox != null)
            attackHitbox.SetActive(true);
    }

    public void DisableAttackHitbox()
    {
        if (attackHitbox != null)
            attackHitbox.SetActive(false);
    }
}
