using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChange : MonoBehaviour
{
    public SpriteRenderer newsprite;
    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        newsprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        anim = gameObject.GetComponentInChildren<Animator>();

        if (GameHandler.playercustom == 0) {
            newsprite.sprite = Resources.Load<Sprite>("Art/People/Smugglers_0"); 
            anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/NewAnims/PlayerArt_0"); 
        }
        else if (GameHandler.playercustom == 1) {
            newsprite.sprite = Resources.Load<Sprite>("Art/People/Smugglers_9");
            anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/NewAnims/PlayerArt_1"); 
        } 
        else if (GameHandler.playercustom == 2) {
            newsprite.sprite = Resources.Load<Sprite>("Art/People/Smugglers_18");
            anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/NewAnims/PlayerArt_2"); 
        } 
        else { // sprite num == 3 
            newsprite.sprite = Resources.Load<Sprite>("Art/People/Smugglers_27");
            anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/NewAnims/PlayerArt_3"); 
        }
    }
}
