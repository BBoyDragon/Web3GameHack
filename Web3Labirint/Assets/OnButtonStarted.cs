using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Code.Menu;
public class OnButtonStarted : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // animator.SetBool("CanStart",true);
        animator.GetComponentInParent<UIBehaviour>().StartGame();
    }
}
