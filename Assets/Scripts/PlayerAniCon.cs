using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAniCon : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }
    
    public void setWalkBoolTrue()
    {
        animator.SetBool("isWalk",true);
    }
    public void setWalkBoolFalse()
    {
        animator.SetBool("isWalk", false);
    }
    public void setDigBoolTrue()
    {
        animator.SetBool("isDig", true);
    }
    public void setDigBoolFalse()
    {
        animator.SetBool("isDig", false);
    }
    public void setShowBoolTrue()
    {
        animator.SetBool("isShow", true);
    }
    public void setShowBoolFalse()
    {
        animator.SetBool("isShow", false);
    }
}
