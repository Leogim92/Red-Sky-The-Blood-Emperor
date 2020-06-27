using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;

public class HPCount : MonoBehaviour
{
    public int Hptotal;

    //colei daqui https://answers.unity.com/questions/1633697/cant-drag-slider-onto-inspector.html
    public UnityEngine.UI.Slider sliderHp;
    public TextMeshProUGUI HpText;

    private int HPnow;
    public UnityEngine.UI.Image colorAdjustmentForFill;//Tem de ser específico, se não o Unity se confunde.

    public GameObject speech;

    private void Start() //Por que fazer isso no start? Acredito que se fizer isso em OnBecameVisible poderemos ter alguns bugs;
    {
        sliderHp.maxValue = Hptotal; //Definindo o valor máximo do slider como sendo o valor máximo de HP
        colorAdjustmentForFill.color = Color.cyan;//Por motivos desconhecidos a função não funciona bem aqui

        HPnow = Hptotal; //Se começa com o HP total.
    }

    private void OnBecameVisible() //Salvando aquele quase nada de processamento
    {
        sliderHp.value = HPnow;
        HpText.SetText(Hptotal.ToString());
    }

    public void Damaged(int i) //Desse jeito que fiz não dá para ser estático. That just happened.
    {//Sei que passar mensagens não é a melhor forma de se fazer isso. That just happened²

        HPnow = HPnow - i;
        sliderHp.value = HPnow;
        HpText.SetText(HPnow.ToString()+"/"+Hptotal.ToString());

        ColorAdjustment();

        if (HPnow <= 0)
        {
            SendMessage("Death");
            HpText.SetText("Dead");
            speech.SetActive(true);

            if (this.gameObject.tag != "Player")
            {
                sliderHp.gameObject.SetActive(false);
                Destroy(this.gameObject, 4f);
            }
        }
        else
        {
            SendMessage("DamageTaken");
        }
    }

    private void ColorAdjustment()
    {
        if(HPnow >= Hptotal / 2)
        {
            colorAdjustmentForFill.color = Color.cyan;
        }
        else if(HPnow > 3)
        {
            colorAdjustmentForFill.color = Color.magenta;
        }
        else
        {
            colorAdjustmentForFill.color = Color.red;
        }
    }

}
