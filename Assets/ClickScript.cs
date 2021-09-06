using UnityEngine;
using UnityEngine.UI;

public class ClickScript : MonoBehaviour
{
    //Own Shit
    public int activeScreen; // 0 = Main Screen / 1 = Buildings Screen / 2 = Ground Screen / 3 = Main Menu
    public Animator anim;

    //Episode 1
    public Text leekText;
    public Text clickValueText;
    public double leeks;
    public double leeksClickValue;
    

    //Episode 2
    public Text leeksPerSecondText;
    public Text activeUp1Text;
    public Text passiveUp1Text;
    public Text passiveUp2Text;

    public double leeksPerSecond;
    public double activeUp1Cost;
    public int activeUp1Level;

    public double passiveUp1Cost;
    public int passiveUp1Level;

    //Episode 3
    public double passiveUp2Cost;
    public double passiveUp2Power;
    public int passiveUp2Level;

    //Episode 6
    public Text soulsText;
    public Text fertilizerBoostText;
    public Text soulsToGetText;

    public double souls;
    public double fertilizer;
    public double fertilizerBoost;
    public double soulsToGet;
    public double fertilizerToGet;

    public void Start()
    {
        Load();
    }

    public void Load()
    {
        leeks = double.Parse(PlayerPrefs.GetString("leeks", "0"));
        leeksClickValue = double.Parse(PlayerPrefs.GetString("leeksClickValue", "1"));
        activeUp1Cost = double.Parse(PlayerPrefs.GetString("activeUp1Cost", "10"));
        passiveUp1Cost = double.Parse(PlayerPrefs.GetString("passiveUp1Cost", "25"));
        passiveUp2Cost = double.Parse(PlayerPrefs.GetString("passiveUp2Cost", "250"));
        passiveUp2Power = double.Parse(PlayerPrefs.GetString("passiveUp2Power", "5"));

        souls = double.Parse(PlayerPrefs.GetString("souls", "0"));
        fertilizer = double.Parse(PlayerPrefs.GetString("fertilizer", "0"));

        activeUp1Level = PlayerPrefs.GetInt("activeUp1Level", 0);
        passiveUp1Level = PlayerPrefs.GetInt("passiveUp1Level", 0);
        passiveUp2Level = PlayerPrefs.GetInt("passiveUp2Level", 0);
    }

    public void Save()
    {
        PlayerPrefs.SetString("leeks", leeks.ToString());
        PlayerPrefs.SetString("leeksClickValue", leeksClickValue.ToString());
        PlayerPrefs.SetString("activeUp1Cost", activeUp1Cost.ToString());
        PlayerPrefs.SetString("passiveUp1Cost", passiveUp1Cost.ToString());
        PlayerPrefs.SetString("passiveUp2Cost", passiveUp2Cost.ToString());
        PlayerPrefs.SetString("passiveUp2Power", passiveUp2Power.ToString());

        PlayerPrefs.SetString("souls", souls.ToString());
        PlayerPrefs.SetString("fertilizer", fertilizer.ToString());

        PlayerPrefs.SetInt("activeUp1Level", activeUp1Level);
        PlayerPrefs.SetInt("passiveUp1Level", passiveUp1Level);
        PlayerPrefs.SetInt("passiveUp2Level", passiveUp2Level);
    }

    public void Update()
    {
        soulsToGet = (System.Math.Sqrt(leeks));
        fertilizerToGet = (System.Math.Sqrt(leeks));

        fertilizerBoost = (fertilizer * 0.05) + 1;

        soulsText.text = "Souls: " + System.Math.Floor(souls).ToString("F0");
        fertilizerBoostText.text = fertilizerBoost.ToString("F2") + "x boost";

        soulsToGetText.text = "Harvest:\n+ " + System.Math.Floor(soulsToGet).ToString("F0") + "Souls";

        leeksPerSecond = (passiveUp1Level + (passiveUp2Power * passiveUp2Level)) * fertilizerBoost;

        if ((leeksClickValue * fertilizerBoost) > 1000)
        {
            var exponent = (System.Math.Floor(System.Math.Log10(System.Math.Abs((leeksClickValue * fertilizerBoost)))));
            var mantissa = ((leeksClickValue * fertilizerBoost) / System.Math.Pow(10, exponent));
            clickValueText.text = "+" + mantissa.ToString("F2") + "e" + exponent;
        }
        else
        {
            clickValueText.text = "+" + (leeksClickValue * fertilizerBoost).ToString("F0");
        }

        if (leeks > 1000)
        {
            var exponent = (System.Math.Floor(System.Math.Log10(System.Math.Abs(leeks))));
            var mantissa = (leeks / System.Math.Pow(10, exponent));
            leekText.text = mantissa.ToString("F2") + "e" + exponent;
        }
        else
        {
            leekText.text = leeks.ToString("F0");
        }

        leeksPerSecondText.text = leeksPerSecond.ToString("F0") + " leeks/s";
        activeUp1Text.text = "Click Upgrade 1\nCost: " + activeUp1Cost.ToString("F0") + " leeks\nPower: +" + fertilizerBoost.ToString("F0") +  " Click\nLevel: " + activeUp1Level;

        passiveUp1Text.text = "Building Upgrade 1\nCost: " + passiveUp1Cost.ToString("F0") + " leeks\nPower: +" + fertilizerBoost.ToString("F0") + " Leeks/s\nLevel: " + passiveUp1Level;
        passiveUp2Text.text = "Building Upgrade 2\nCost: " + passiveUp2Cost.ToString("F0") + " leeks\nPower: +" + (passiveUp2Power * fertilizerBoost).ToString("F0") + " Leeks/s\nLevel: " + passiveUp2Level;

        leeks += leeksPerSecond * Time.deltaTime;

        Save();
    }

    // Harvest
    public void Harvest()
    {
        if (leeks > 420)
        {
            leeks = 0;
            leeksClickValue = 1;
            activeUp1Cost = 10;
            passiveUp1Cost = 25;
            passiveUp2Cost = 250;
            passiveUp2Power = 5;

            activeUp1Level = 0;
            passiveUp1Level = 0;
            passiveUp2Level = 0;


            souls += soulsToGet;
            fertilizer += fertilizerToGet;
        }
    }

    // Switch Screen
    public void SwitchScreen()
    {
        if (activeScreen == 1)
        {
            activeScreen = 0;
        }
        else
        {
            activeScreen = 1;
        }
    }

    public void Click()
    {
        anim.SetTrigger("Hit");
        leeks += (leeksClickValue * fertilizerBoost);
    }

    public void BuyActiveUpgrade1()
    {
        if (leeks >= activeUp1Cost)
        {
            activeUp1Level++;
            leeks -= activeUp1Cost;
            activeUp1Cost *= 1.08;
            leeksClickValue++;
        }
    }


    public void BuyPassiveUpgrade1()
    {
        if (leeks >= passiveUp1Cost)
        {
            passiveUp1Level++;
            leeks -= passiveUp1Cost;
            passiveUp1Cost *= 1.08;
        }
    }

    public void BuyPassiveUpgrade2()
    {
        if (leeks >= passiveUp2Cost)
        {
            passiveUp2Level++;
            leeks -= passiveUp2Cost;
            passiveUp2Cost *= 1.10;
        }
    }

}
