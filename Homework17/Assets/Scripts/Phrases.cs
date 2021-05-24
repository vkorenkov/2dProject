using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phrases : MonoBehaviour
{
    public List<string> dialogs;

    private void Awake()
    {
        FillDialogList();
    }

    void FillDialogList()
    {
        dialogs = new List<string>()
        {
            "Wat!? Is it a skeleton? And does he have a beer? What the f@#% is this?",
            "Wow! Someone threw a bottle of healing potion here. AND KNIFE!?",
            "Did these skeletons steal the beer? Are you, f@#%$!, kidding me?",
            "The bridge looks fragile. I need to be careful.",
            "F@#%! Looks dangerous.",
            "Why are the axes hanging here? What's going on here? Well... I'll have to go down.",
            "Cart? In a cave? Perhaps this is not in vain.",
            "Dangerous place! I wouldn't be surprised if bombs fall from the ceiling here.",
            "High. How can I get up?",
            "Is this some kind of stone elevator?",
            "There is a door ahead. Does it open for beer?",
            "What? How did I get here? Everything is very strange here!",
            "Damn it! They know how to walk ... ",
            "Was it lightning seriously now? Strange, strange place!",
            "Lol! Second moon =)",
            "What's with the architecture in this place?",
            "The third moon is already too much."
        };
    }
}
