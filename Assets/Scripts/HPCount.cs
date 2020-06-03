using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class HPCount : MonoBehaviour
{
   // public CountOfEnemies count;
    public int HP;
    //colei daqui https://answers.unity.com/questions/1633697/cant-drag-slider-onto-inspector.html
    public UnityEngine.UI.Slider sliderHp;
    public TextMeshProUGUI HpText;

    private void OnBecameVisible()
    {
        sliderHp.value = HP;
        HpText.SetText(HP.ToString());
    }

    public void Damaged(int i) //Desse jeito que fiz não dá para ser estático. That just happened.
    {//Sei que passar mensagens não é a melhor forma de se fazer isso. That just happened²

        HP = HP - i;
        //Debug.Log("Hp dropped " + i + "values");
        sliderHp.value = HP;
        HpText.SetText(HP.ToString());

        if (HP <= 0)
        {
            SendMessage("Death");
            sliderHp.gameObject.SetActive(false);
            HpText.SetText("Dead");

            if (this.gameObject.tag != "Player")
            {
                Destroy(this.gameObject, 4f);
                //count.KilledEnemy();
            }
        }
        else
        {
            SendMessage("DamageTaken");
        }
    }

}
