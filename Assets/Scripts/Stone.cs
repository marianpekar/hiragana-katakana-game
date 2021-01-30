using UnityEngine;

public class Stone : MonoBehaviour
{
    private StoneProperties stoneProperties = new StoneProperties();

    public void SetSign(Sign sign) => stoneProperties.Sign = sign;
    public Sign GetSign() => stoneProperties.Sign;

    public void SetAlphabet(Alphabet type) => stoneProperties.Alphabet = type;
    public Alphabet GetAlphabet() => stoneProperties.Alphabet;
}
