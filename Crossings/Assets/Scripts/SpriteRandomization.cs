using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRandomization : MonoBehaviour
{
    // Start is called before the first frame update
    public int spritenum;

    public SpriteRenderer newsprite;
    public Animator anim;

    void Start()
    {
        newsprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        

        spritenum = Random.Range(0, 4);

        // imm randomization
        if (gameObject.tag == "Immigrant") {
            if (spritenum == 0) {
                newsprite.sprite = Resources.LoadAll<Sprite>("Art/People")[0];
            }
            else if (spritenum == 1) {
                newsprite.sprite = Resources.LoadAll<Sprite>("Art/People")[1];
            } 
            else if (spritenum == 2) {
                newsprite.sprite = Resources.LoadAll<Sprite>("Art/People")[2];
            } 
            else { // sprite num == 3 
                newsprite.sprite = Resources.LoadAll<Sprite>("Art/People")[3];
            }
        }
        

        // patrol randomization 
        if (gameObject.tag == "Patrol") {
            anim = gameObject.GetComponentInChildren<Animator>();

            if (spritenum == 0) {
                newsprite.sprite = Resources.Load<Sprite>("Art/People/Patrols_0"); 
                anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/NewAnims/PatrolArt_0"); 
            }
            else if (spritenum == 1) {
                newsprite.sprite = Resources.Load<Sprite>("Art/People/Patrols_9");
                anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/NewAnims/PatrolArt_1"); 
            } 
            else if (spritenum == 2) {
                newsprite.sprite = Resources.Load<Sprite>("Art/People/Patrols_18");
                anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/NewAnims/PatrolArt_2"); 
            } 
            else { // sprite num == 3 
                newsprite.sprite = Resources.Load<Sprite>("Art/People/Patrols_27");
                anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/NewAnims/PatrolArt_3"); 
            }

        }
    }

}
