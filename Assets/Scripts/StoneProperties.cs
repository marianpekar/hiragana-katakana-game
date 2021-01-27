using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Alphabet {
    Hiragana,
    Katakana,
}

public enum Sign {
    N, 

    Wa, Wo, 
    
    Ra, Ri, Ru, Re, Ro,
    
    Ya, Yu, Yo,
    
    Ma, Mi, Mu, Me, Mo,
    
    Ha, Hi, Fu, He, Ho,
    Ba, Bi, Bu, Be, Bo,
    Pa, Pi, Pu, Pe, Po,

    Na, Ni, Nu, Ne, No,

    Ta, Chi, Tsu, Te, To,
    Da,      Dzu, De, Do,

    Sa, Shi, Su, Se, So,
    Za, Ji, Zu, Ze, Zo,

    Ka, Ki, Ku, Ke, Ko,
    Ga, Gi, Gu, Ge, Go,

    A, I, U, E, O
}

public class StoneProperties
{
    public Alphabet Alphabet;
    public Sign Sign;
}
