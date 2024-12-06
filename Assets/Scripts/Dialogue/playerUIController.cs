using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class playerUIController : MonoBehaviour
{

	
	
    public bool dev_SkipCutscene = false;
    public TextAsset mainFile;

    public List<TMP_Text> choiceButtons;
    public float rapport = 1;
    public Image dialoguePlayer;

	public TMP_Text playerText;
	public TMP_Text subjectText;
	
	
    [SerializeField] float dialogueSpeed = .02f;
    [SerializeField] Image playerTextEnter;
    [SerializeField] private Image choicesBox;
    [SerializeField] float camSize;
    [SerializeField] private string dateName;

    [SerializeField] Sprite StartBG;
    [SerializeField] Sprite DateBG;
    [SerializeField] Sprite WalkBG;
    [SerializeField] Sprite EndBG;

    [SerializeField] Sprite DateIcon;
    [SerializeField] Sprite PlayerIcon;
    [SerializeField] Sprite NarratorIcon;
    
    [SerializeField] Image iconImage;
    [SerializeField] Image background;
    
    private bool selected = false;
    private int selection;

    public void quitGame()
    {
	    Application.Quit();
    }
    
    private IEnumerator dialogue(dialogueParsing.dialogueLine[] lines, string dialogueName, dialogueParsing.Dialogue Root)
	{
		dialoguePlayer.transform.DOLocalMoveX(-357, 1);
		background.transform.DOLocalMoveY(540, 1);
		foreach (dialogueParsing.dialogueLine line in lines)
		{
			subjectText.text = line.Subject;
			switch (line.Subject)
			{
				case "Player":
					iconImage.sprite = PlayerIcon;
					break;
				case "Narrator":
					iconImage.sprite = NarratorIcon;
					break;
				case "Linda":
					iconImage.sprite = DateIcon;
					break;
			}
            for (int i = 1; i <= line.dialogueText.Length; i++)
            {
                playerText.text = line.dialogueText.Substring(0, i);
                

                yield return new WaitForSeconds(dialogueSpeed);
            }
            playerText.text = line.dialogueText;



            playerTextEnter.enabled = true;
            yield return new WaitUntil(() => Input.GetButtonDown("Submit"));
	            
            
			if (line.choices != null && line.choices[1].Text != "")
			{
				print(line.choices.Length);
				choicesBox.transform.DOLocalMoveX(960, 1);
				for (int i = 0; i < line.choices.Length; i++)
				{
					print(line.choices[i].Text);
					choicesBox.transform.GetChild(i).GetChild(0).transform.GetComponent<TMP_Text>().text = line.choices[i].Text;
				}
				
				yield return new WaitUntil(() => selected);

				print(selection);
				rapport += line.choices[selection].Rapport;
				selected = false;
				choicesBox.transform.DOLocalMoveX(2170, 1);
				
			}
            
			playerTextEnter.enabled = false;

			playerText.text = "";
			subjectText.text = "";
		}

		dialoguePlayer.transform.DOLocalMoveX(-1788, 1);
		background.transform.DOLocalMoveY(1516, 1);
		
		switch (dialogueName)
		{
			case "Start":
				StartCoroutine(dialogue(Root.walkingDialogue, "Restaurant", Root));
				background.sprite = DateBG;
				break;
			case "Restaurant":
				StartCoroutine(dialogue(Root.restaurantDialogue, "Walk", Root));
				background.sprite = WalkBG;
				break;
			case "Walk":
				StartCoroutine(dialogue(Root.endDialogue, "End", Root));
				background.sprite = EndBG;
				break;
			case "End":
				stopDialogue();
				break;
		}
		
	}

	public void chooseDialogue(int choice)
	{
		selection = choice;
		selected = true;
	}
    
    public void Start()
	{
		dialogueParsing.Dialogue dialogueRoot = JsonUtility.FromJson<dialogueParsing.Dialogue>(mainFile.text);
		
		print("f");

        if (!dev_SkipCutscene)
        {
            
            StartCoroutine(dialogue(dialogueRoot.startDialogue, "Start", dialogueRoot));
			
        }
        
        
    }
    
    private bool isClicked = false;
    int clickedButton;
    public void click(int s) 
    {
        clickedButton = s;
        isClicked = true;
    }


    private float patience = 5;
    
    
    
    private void stopDialogue()
	{       
        dialoguePlayer.transform.DOLocalMoveX(-1712, 1);
        choicesBox.transform.DOLocalMoveX(1712, 1);

        
	}

}
