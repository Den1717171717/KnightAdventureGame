using UnityEngine;

public class SwordSlashVisual : MonoBehaviour
{
    [SerializeField] private Sword sword;
    private const string ATTACK = "Attack";
    private Animator animator;
    private void Awake()
    {
       
       animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (sword != null)
        {
            sword.OnSwordSwing += Sword_OnSwordSwing;
        }
        else
        {
            Debug.LogError("null");
        }

    }

    private void Sword_OnSwordSwing(object sender, System.EventArgs e)
    {
        animator.SetTrigger(ATTACK);
    }
}
