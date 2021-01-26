using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    private StoneProperties StoneProperties = new StoneProperties();

    public void SetSign(Sign sign) => StoneProperties.Sign = sign;
    public Sign GetSign() => StoneProperties.Sign;

    public void SetAlphabet(Alphabet type) => StoneProperties.Alphabet = type;
    public Alphabet GetAlphabet() => StoneProperties.Alphabet;
}
