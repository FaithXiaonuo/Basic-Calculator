using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyCalculator : MonoBehaviour
{
    #region vaules of Inputfield
    string inputNumStr = "0";
    //string preFormula = "";
    public Text inputNumField, preFormulaField;
    #endregion

    float answer = 0;
    //0+ 1- 2* 3/
    int operatored = 0;
    bool isStartNewNum = false;

    // Start is called before the first frame update
    void Start()
    {
        // for test
        Debug.Log(float.Parse("100."));
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region Calculator
    //0 for +, 1 for -, 2 for *, 3 for /, 4 for =
    public void Operator(int indexOpt)
    {
        //If click on operator again but input None Number
        //Invalid operation
        if (!isStartNewNum)
        {
            operatored = indexOpt;
            RefreshPreFormulaField(operatored);
            return;
        }

        calculateAnswer();
        inputNumStr = answer.ToString();
        RefreshInputNumField();

        operatored = indexOpt;
        RefreshPreFormulaField(operatored);
        isStartNewNum = false;
    }

    //finish this turn
    public void Equal()
    {
        //If click on Equal but input None Number
        //Invalid operation
        if (!isStartNewNum)
        {
            inputNumStr = answer.ToString();
            initialization();
        }

        calculateAnswer();
        inputNumStr = answer.ToString();
        initialization();
    }

    void calculateAnswer()
    {
        switch (operatored)
        {
            case 0:
                answer = answer + float.Parse(inputNumStr);
                break;
            case 1:
                answer = answer - float.Parse(inputNumStr);
                break;

            case 2:
                answer = answer * float.Parse(inputNumStr);
                break;

            case 3:
                answer = answer / float.Parse(inputNumStr);
                break;
        }
    }
    #endregion

    #region Input
    //0-9 for 0-9, 10 for poing, 11 for positive/negative signal
    public void InputNum(int indexNum)
    {
        //for number
        if (indexNum < 10)
        {
            if (isStartNewNum)
            {
                inputNumStr = inputNumStr + indexNum.ToString();
            }
            else
            {
                isStartNewNum = true;
                inputNumStr = indexNum.ToString();
            }
        }
        //for "."
        else if (indexNum == 10)
        {
            if (inputNumStr.Contains("."))
                return;
            inputNumStr = inputNumStr + ".";
        }
        //for "+/-"
        else if (indexNum == 11)
        {
            if (inputNumStr[0] == '-')
            {
                inputNumStr = inputNumStr.Substring(1, inputNumStr.Length - 1);
            }
            else
            {
                inputNumStr = "-" + inputNumStr;
            }
        }
        RefreshInputNumField();
    }

    public void DeleteNum()
    {
        //if display the answer
        // return 0
        if (!isStartNewNum)
        {
            inputNumStr = "0";
            RefreshInputNumField();
            return;
        }

        //if like -2
        //return 2
        if (inputNumStr.Length == 2)
        {
            if (inputNumStr[0] == '-')
            {
                inputNumStr = inputNumStr.Substring(1);
                RefreshInputNumField();
                return;
            }
        }
        //if like 2
        //return 0
        else if (inputNumStr.Length == 1)
        {
            inputNumStr = "0";
            RefreshInputNumField();
            isStartNewNum = false;
            return;
        }
        inputNumStr = inputNumStr.Substring(0, inputNumStr.Length - 1);
        RefreshInputNumField();
    }
    #endregion

    #region Refresh
    void initialization()
    {
        //Sometimes, misoperations in the editor may change the initial value.
        RefreshInputNumField();
        RefreshPreFormulaField(4);
        answer = 0;
        operatored = 0;
        //preFormula = "";
        isStartNewNum = false;
    }

    void RefreshInputNumField()
    {
        inputNumField.text = inputNumStr;
    }

    void RefreshPreFormulaField(int operatoredIndex)
    {
        string signIndex = " ";
        //01234 for +-*/=
        switch (operatoredIndex)
        {
            case 0:
                signIndex += "+";
                break;
            case 1:
                signIndex += "-";
                break;
            case 2:
                signIndex += "*";
                break;
            case 3:
                signIndex += "/";
                break;
            default:
                preFormulaField.text = "";
                return;
        }
        //preFormula = answer + " " + signIndex;
        preFormulaField.text = answer + " " + signIndex;
    }
    #endregion
}
