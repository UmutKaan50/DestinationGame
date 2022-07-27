using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DelegateMethodsImplementation : MonoBehaviour {

    private delegate void Printing();
    private delegate float Calculation(float firstNumber, float secondNumber);

    private Printing printingDelegate;
    private Calculation calculationDelegate;

    private void PrintName() {
        string name = "Umut Kaan Özdemir";
        Debug.Log(name);
    }

    private void PrintAge() {
        int age = 20;
        Debug.Log(age);
    }

    void Start() {
        printingDelegate = PrintName;
        printingDelegate += PrintAge;

        printingDelegate();

        calculationDelegate += Multiplication;
        calculationDelegate += Division;
        calculationDelegate += Addition;
        calculationDelegate += Substraction;
        calculationDelegate += (float a, float b) => {
            float result = a % b;
            Debug.Log("Mod: " + result);
            return result;
        };

        calculationDelegate(10, 2);

        Delegate[] funcs = calculationDelegate.GetInvocationList();
        float total = 0;
        for (int i = 0; i < funcs.Length; i++) {
            float resultAnother = ((Calculation)funcs[i]).Invoke(15, 3);
            total += resultAnother;
        }
        Debug.Log("Total: " + total);
    }

    private float Multiplication(float number1, float number2) {
        float result = number1 * number2;
        Debug.Log("Multiplication " + result);
        return result;
    }

    private float Division(float number1, float number2) {
        float result = number1 / number2;
        Debug.Log("Division " + result);
        return result;
    }

    private float Addition(float number1, float number2) {
        float result = number1 + number2;
        Debug.Log("Addition " + result);
        return result;
    }

    private float Substraction(float number1, float number2) {
        float result = number1 - number2;
        Debug.Log("Substraction " + result);
        return result;
    }
}
