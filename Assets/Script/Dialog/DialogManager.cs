using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class DialogManager : MonoBehaviour
{
	private static readonly string DIALOG_END = "end";
	private static readonly string BLANK = "";
	public int id;
	public TMP_Text speaker;
	public TMP_Text dialog;
	public GameObject dialogPanel;
	public List<GameObject> choiceButtons;
	public List<TMP_Text> choices;

	public int cntId;
	private DataParser dataParser;
	public static DialogManager instance = null;
	List<DialogParserData> dialogParserDatas = new List<DialogParserData>();


	void Awake()
	{
		if (null == instance)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Destroy(this.gameObject);
			Debug.Log("DialogManager오브젝트가 싱글톤에 의해 삭제");
		}

		ButtonInit();
		dialogPanel.SetActive(false);
		cntId = -1;
	}
	private void Start()
	{
		dataParser = GetComponent<DataParser>();
		GetDialog();
	}
	//데이터 가져오기
	private void GetDialog()
	{
		dialogParserDatas = dataParser.dialogDatas;
	}
	//대화를 화면에 띄우는 함수
	public void StartDialog(int id)
	{
		dialogPanel.SetActive(true);
		cntId = id;
		speaker.text = dialogParserDatas[cntId].speaker;
		dialog.text = dialogParserDatas[cntId].dialogue;
		for (int i = 0; i < choiceButtons.Count; i++)
		{
			choiceButtons[i].SetActive(false);
		}
	}
	//대화를 다음으로 넘기는 함수
	public bool NextDialog()
	{
		if (dialogParserDatas[cntId].choices[0] == DIALOG_END)
		{
			EndDialog();
			Debug.Log("End");
			return true;
		}
		else if(dialogParserDatas[cntId].choices[0] != BLANK)
		{
			ChoicesDialog();
			Debug.Log("Choices");
			return false;
		}
		else
		{
			cntId++;
			StartDialog(cntId);
			Debug.Log("Next");
			return false;
		}
	}
	//선택지 활성화
	public void ChoicesDialog()
	{
		ChoiceButtonsActive();
	}
	//대화 종료
	public void EndDialog()
	{
		dialogPanel.SetActive(false);
		cntId = -1;
	}

	//버튼 초기화
	private void ButtonInit()
	{
        foreach (var button in choiceButtons)
        {
			choices.Add(button.transform.GetChild(0).GetComponent<TMP_Text>());
			button.SetActive(false);
        }
	}
	//선택지 버튼 활성화
	private void ChoiceButtonsActive()
	{
		for (int i = 0; i < choiceButtons.Count; i++)
		{
			if(dialogParserDatas[cntId].choices[i] != "")
			{
				int index = i;
				choiceButtons[index].SetActive(true);
				choices[index].text = dialogParserDatas[cntId].choices[index];
				//Debug.Log($"next = {dialogParserDatas[cntId].nextId[index]}");
				Debug.Log($"index = {dialogParserDatas[cntId].nextId[index]}");
				choiceButtons[index].GetComponent<Button>().onClick.RemoveAllListeners();
				choiceButtons[index].GetComponent<Button>().onClick.AddListener(() => StartDialog(dialogParserDatas[cntId].nextId[index]));
			}
		}
    }
}
