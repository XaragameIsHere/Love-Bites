
/// <summary>
/// this is the componenent of the UI that controls the dialogue in game
/// ye
/// </summary>
/// 
[System.Serializable]
public class dialogueParsing
{
    [System.Serializable]
    public class dialogueLine
    {
        public string dialogueText;
        public string Subject;
        public choice[] choices;
    }

    [System.Serializable]
    public class choice
    {
        public string Text;
        public int Rapport;
    }
    

    [System.Serializable]
    public class Dialogue
    {
        public dialogueLine[] startDialogue;
        public dialogueLine[] restaurantDialogue;
        public dialogueLine[] walkingDialogue;
        public dialogueLine[] endDialogue;
    }


}

