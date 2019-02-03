using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddAndRemove : MonoBehaviour
{
    private int canon_startIndex = 0;
    private int crossbowTower_startIndex = 10;

    private int money;
    private int canon_price = 3000;
    private int crossbowTower_price = 4000;

    public GameObject shopPanel;
    public Text moneyText;
    public Text shopMoneyText;


    public static AddAndRemove instance;
    // Use this for initialization
    void Start ()
    {
        instance = this;

        if (PlayerPrefs.HasKey("money"))
        {
            money = PlayerPrefs.GetInt("money");
        }
        else
        {
            money = 100000;
        }

        moneyText.text = money.ToString();
        shopMoneyText.text = money.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BuyBuilding(int index)
    {
        if(index == 0)
        {
            if(money > canon_price)
            {
                SelectAndMove.instance.AddBuilding(canon_startIndex, crossbowTower_startIndex);
                money = money - canon_price;  // pinigu issaugojimas select and move klasej save metode
            }
        }
        else if (index == 1)
        {
            if (money > crossbowTower_price)
            {
                SelectAndMove.instance.AddBuilding(crossbowTower_startIndex, crossbowTower_startIndex + 10);
                money = money - crossbowTower_price;  // pinigu issaugojimas select and move klasej save metode
            }
        }
        shopPanel.SetActive(false);
    }

    public void SellBuilding()
    {
        int buildingIndex = SelectAndMove.instance.selectedBuildingIndex;
        if (buildingIndex > 0 && buildingIndex < crossbowTower_startIndex)
        {
            money = money + canon_price;
        }
        else if(buildingIndex >= crossbowTower_startIndex && buildingIndex < crossbowTower_startIndex + 10)
        {
            money = money + crossbowTower_price;
        }
        SelectAndMove.instance.RemoveBuilding();
    }

    public void SaveMoneyValue()
    {
        PlayerPrefs.SetInt("money", money);
    }
}
