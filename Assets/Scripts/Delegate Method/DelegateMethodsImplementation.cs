using UnityEngine;

public class DelegateMethodsImplementation : MonoBehaviour {
    private Calculation calculationDelegate;

    private Printing printingDelegate;

    private void Start() {
        printingDelegate = PrintName;
        printingDelegate += PrintAge;

        // printingDelegate();

        calculationDelegate += Multiplication;
        calculationDelegate += Division;
        calculationDelegate += Addition;
        calculationDelegate += Substraction;
        calculationDelegate += (a, b) => {
            var result = a % b;
            // Debug.Log("Mod: " + result);
            return result;
        };

        // calculationDelegate(10, 2);

        var funcs = calculationDelegate.GetInvocationList();
        float total = 0;
        for (var i = 0; i < funcs.Length; i++) {
            var resultAnother = ((Calculation)funcs[i]).Invoke(15, 3);
            total += resultAnother;
        }

        // Debug.Log("Total: " + total);
    }

    private void PrintName() {
        var name = "Umut Kaan �zdemir";
        // Debug.Log(name);
    }

    private void PrintAge() {
        var age = 20;
        Debug.Log(age);
    }

    private float Multiplication(float number1, float number2) {
        var result = number1 * number2;
        // Debug.Log("Multiplication " + result);
        return result;
    }

    private float Division(float number1, float number2) {
        var result = number1 / number2;
        // Debug.Log("Division " + result);
        return result;
    }

    private float Addition(float number1, float number2) {
        var result = number1 + number2;
        // Debug.Log("Addition " + result);
        return result;
    }

    private float Substraction(float number1, float number2) {
        var result = number1 - number2;
        // Debug.Log("Substraction " + result);
        return result;
    }

    private delegate void Printing();

    private delegate float Calculation(float firstNumber, float secondNumber);
}