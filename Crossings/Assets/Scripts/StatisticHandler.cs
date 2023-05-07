using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatisticHandler : MonoBehaviour
{
    // Start is called before the first frame update

    public int bankBalance;
    public int immigrantsMigrated;
    public int someStat;

    public Text bankText;
    public Text immigrantText;
    public Text someStatText;

    void Start()
    {
      //bankBalance = GameHandler.getCurrentBankBalance();
      bankBalance = 10;
      immigrantsMigrated = 10;
      someStat = 10;

      setBalance();
      setImmigrant();
      setSomeStat();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setBalance(){
      bankText.text = bankBalance.ToString();
    }

    public void setImmigrant(){
      immigrantText.text = immigrantsMigrated.ToString();
    }

    public void setSomeStat(){
      //someStatText.text = someStat.ToString();
    }


}
