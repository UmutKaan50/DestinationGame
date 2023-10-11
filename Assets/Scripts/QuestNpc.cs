using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using CodeMonkey.Utils;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;
using y01cu;

// [Obsolete("Obsolete parts need to be cleaned later on. Skipping it right now to save time.")]
public class QuestNpc : Collideable {
    /// <summary>
    /// Player gameobject needs to be named exactly as "Player" in order to find it using gameobject.find method.
    /// </summary>
    private Player player;

    private Button dialogueButton;

    [SerializeField] private List<string> conversations = new List<string>();

    // This value needs to be same. In this case this and panelbase lasting duration are set as 5 manually.
    private readonly float cooldown = 0.5f; // was previously 5
    private float lastShout = 0.5f; // Instant reply at the beginning. // was previously 3

    private float minimumRequiredDistanceForPlayerToTrigger = 0.3f;

    private TextMeshProUGUI dialogueText;

    private DialogueManager dialogueManager;

    private Button_UI buttonUI;

    private GameObject HUD;

    private Animator dialogueButtonAnimator;

    [SerializeField] private PuzzlePanelManager puzzlePanel;

    public bool IsPuzzleAlreadyCreatedAndNotSolved { get; set; }

    public bool IsPuzzleShownRightNow { get; set; }

    [SerializeField] private AudioClip keyPickUpSoundEffect;

    private SpriteRenderer npcSpriteRenderer;

    public Player GetPlayer() {
        return player;
    }

    public DialogueManager GetDialogueManager() {
        return dialogueManager;
    }

    public List<string> GetConversations() {
        return conversations;
    }

    private void FindAndAssignRequiredGameObjectsAndComponents() {
        HUD = GameObject.Find("HUD");

        puzzlePanel = HUD.transform.Find("PuzzlePanelManager").GetComponent<PuzzlePanelManager>();

        puzzleCounterManager = GameObject.Find("Canvas_PuzzleCounter").transform.Find("PuzzleCounterManager")
            .GetComponent<PuzzleCounterManager>();

        // puzzlePanel = GameObject.Find("CanvasPuzzle").transform.Find("PuzzlePanelManager")
        //     .GetComponent<PuzzlePanelManager>();

        dialogueButton = transform.Find("Canvas").Find("Button").GetComponent<Button>();

        dialogueManager = GameObject.Find("DialogueCanvas").transform.Find("DialogueManager")
            .GetComponent<DialogueManager>();

        dialogueText = dialogueManager.transform.Find("DialogueBox").Find("Text").GetComponent<TextMeshProUGUI>();

        buttonUI = dialogueManager.transform.Find("DialogueBox").GetComponent<Button_UI>();

        npcSpriteRenderer = GetComponent<SpriteRenderer>();

        player = GameObject.Find("Player").GetComponent<Player>();

        dialogueButtonAnimator = dialogueButton.GetComponent<Animator>();

        StartCoroutine(FindPuzzlePanel());

        // StartCoroutine(TryToAssignAgain());
    }

    private IEnumerator FindPuzzlePanel() {
        yield return new WaitForSeconds(1.5f);
        puzzlePanel = HUD.transform.Find("PuzzlePanelManager").GetComponent<PuzzlePanelManager>();
        StartCoroutine(FindPuzzlePanel());
    }

    private void Awake() {
        FindAndAssignRequiredGameObjectsAndComponents();
    }

    private void Start() {
        AssignSomeValuesAndFunctionalities();
    }

    private PuzzleCounterManager puzzleCounterManager;

    private void AssignSomeValuesAndFunctionalities() {
        IsPuzzleAlreadyCreatedAndNotSolved = false;
        IsPuzzleShownRightNow = false;
        // yesButtonAction += () => SendMessage("Hi there, you've clicked yes button!");
        yesButtonAction += () => {
            Debug.Log("Yes button is clicked.");

            if (!IsPuzzleAlreadyCreatedAndNotSolved) {
                IsPuzzleAlreadyCreatedAndNotSolved = true;
                ActivatePuzzlePanel();
                IsPuzzleShownRightNow = true;
            }
            else {
                ShowExistingPuzzlePanel();
                IsPuzzleShownRightNow = true;
            }

            if (puzzlePanel.isDeadlyPuzzle) {
                puzzleCounterManager.StartCounter();
            }
        };
        noButtonAction += () => Debug.Log("Hi");

        // There's no need add another functionality to the no button since it's already set as in the optionpanel class.

        conversations = new List<string>();

        audioSource = GetComponent<AudioSource>();
        Invoke("AssignDialogueButtonFunctionality", 0.4f);

        // CreateDialogue();
        // AssignDialogue();
    }

    private void FlipSpriteBasedOnPlayerPosition() {
        bool isPlayerStandsOnTheRightSide = player.transform.position.x >= transform.position.x;

        bool isPlayerCloseEnoughVertically = Mathf.Abs(player.transform.position.y - transform.position.y) < 0.3f;

        if (isPlayerStandsOnTheRightSide && isPlayerCloseEnoughVertically) {
            npcSpriteRenderer.flipX = true;
        }
        else {
            npcSpriteRenderer.flipX = false;
        }
    }

    private void AssignDialogueButtonFunctionality() {
        dialogueButton.onClick.AddListener(() => {
            dialogueManager.gameObject.SetActive(true);
            dialogueManager.SetupText();
            player.IsAnInterruptionOccuring = true;

            dialogueManager.transform.Find("DialogueBox").GetComponent<Button_UI>().ClickFunc.Invoke();


            // bool isItFirstNpc = conversations.Count > 2;
            // if (isItFirstNpc) {
            // }
            //
            // Debug.Log("---> Dialogue button is clicked.");
        });
    }

    // private void StartConversationByInvokingButtonFunction() {
    //     buttonUI.ClickFunc.Invoke();
    // }

    private void ShowExistingPuzzlePanel() {
        puzzlePanel.GetComponent<Animator>().Play("Entrance");
    }

    // [SerializeField] private PuzzlePanelManager existingPuzzlePanel;

    public PuzzlePanelManager GetExistingPuzzlePanel() {
        return puzzlePanel;
    }

    // Instantiating canvas element that're forced to be in certain pixel ratio by some parent canvas components was hard and buggy. I've switched to having one prefab in the scene. Acivating and deactivating it. 
    public void ActivatePuzzlePanel() {
        puzzlePanel.gameObject.SetActive(true);
        puzzlePanel.GetComponent<Animator>().Play("Entrance");
    }

    public void ClearExistingPuzzlePanel() {
        // Since it can be a better way for every npc to display one puzzle each there seems no requirement for getting ready another puzzle here.
        Destroy(puzzlePanel, 3f);
    }

    private Action yesButtonAction;
    private Action noButtonAction;

    private void Update() {
        CreateOptionPanelBasedOnInteractionDistance();
        FlipSpriteBasedOnPlayerPosition();
    }


    // [SerializeField] private TextMeshProUGUI storyText;

    public bool isPuzzleSolved = false;
    
    /// <summary>
    /// Called in update to create an option panel based on interaction distance.
    /// </summary>0,0
    private void CreateOptionPanelBasedOnInteractionDistance() {
        // Calculating distance between player and npc that effects a panel animation:

        // Assigning required position values into variables: 
        float npcCurrentX = transform.position.x;
        float npcCurrentY = transform.position.y;

        float playerCurrentX = player.transform.position.x;
        float playerCurrentY = player.transform.position.y;

        // Determining the distance between required points:
        float xDistance = Mathf.Abs(npcCurrentX - playerCurrentX);
        float yDistance = Mathf.Abs(npcCurrentY - playerCurrentY);

        var distance = Mathf.Sqrt(xDistance * xDistance + yDistance * yDistance);

        // if (distance < 0.18f && !isQuestAssurancePanelOpened) {

        float triggerDistance = 0.25f;
        if (distance < triggerDistance && !isPuzzleSolved) {
            // Triggering animation and setting next cooldown:
            if (Time.time - lastShout > cooldown) {
                lastShout = Time.time;
                //---------------------------------------------
                bool isChatboxActive = dialogueButtonAnimator.gameObject.activeSelf;
                if (!isChatboxActive) {
                    dialogueButtonAnimator.gameObject.SetActive(true);
                }

                dialogueButtonAnimator.SetBool("isPlayerCloseEnough", true);

                bool isReadyToAskForPuzzle = !IsPuzzleAlreadyCreatedAndNotSolved || !IsPuzzleShownRightNow;
                if (isReadyToAskForPuzzle) {
                    // string text = "I have puzzle waiting to be solved, wanna try?";
                    // float textWritingSpeed = 0.05f;
                    // TextWriter.AddWriterWithSpeed_Static(storyText, text, textWritingSpeed, true, true,
                    //     StopSoundEffect_TextWriting);

                    // Activate chatbox so that both entrance and idle animations can be played consecutively.
                    // CreateOptionPanel(yesButtonAction, noButtonAction, "I have puzzle waiting to be solved, wanna try?");
                }

                // Debug.Log("Is player here...?");
                // assuranceQuestionAnimator.SetTrigger("show");
                // SendMessage("Welcome, adventurer!");
            }
        }
        else {
            dialogueButtonAnimator.SetBool("isPlayerCloseEnough", false);
            float animationTime = dialogueButtonAnimator.GetCurrentAnimatorStateInfo(0).length;
            StartCoroutine(DeactivateGameObjectAfterSomeTime(dialogueButtonAnimator.gameObject, animationTime * 1.2f));
        }
    }

    void StopSoundEffect_TextWriting() {
        audioSource.Stop();
    }

    private IEnumerator DeactivateGameObjectAfterSomeTime(GameObject gameObj, float delayTime) {
        yield return new WaitForSeconds(delayTime);
        gameObj.SetActive(false);
    }

    private AudioSource audioSource;


    public void GiveKeysToThePlayer() {
        player.SetHasKey();
        audioSource.PlayOneShot(keyPickUpSoundEffect);
    }

    public void SoundManagerPlay(AudioClip audioClip) {
        SoundManager.instance.audioSource.PlayOneShot(audioClip);
    }

    // [SerializeField] private MessagePanel pfMessagePanel;
    //
    // [SerializeField] private OptionPanel pfOptionPanel;

    // private void SendMessage(string message) {
    //     MessagePanel messagePanel =
    //         Instantiate(pfMessagePanel, pfMessagePanel.transform.position, Quaternion.identity)
    //             .GetComponent<MessagePanel>();
    //     messagePanel.SetMessage(message);
    // }
    //
    // private void CreateOptionPanel(Action yesButtonAction, Action noButtonAction, string message) {
    //     OptionPanel optionPanel =
    //         Instantiate(pfOptionPanel, pfOptionPanel.transform.position, Quaternion.identity)
    //             .GetComponent<OptionPanel>();
    //     optionPanel.AddOptionButtonFunctionalities(yesButtonAction, noButtonAction);
    //     optionPanel.SetMessage(message);
    // }
}