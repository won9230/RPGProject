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
	public List<GameObject> choiceButtons;
	public List<TMP_Text> choices;

	public int nextId3;
	private bool isSelector = false;
	public int cntId;
	private DataParser dataParser;
	public static DialogManager instance = null;

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
		cntId = -1;
	}
	List<DialogParserData> dialogParserDatas = new List<DialogParserData>();
	private void Start()
	{
		dataParser = GetComponent<DataParser>();
		GetDialog();
	}
	private void GetDialog()
	{
		dialogParserDatas = dataParser.dialogDatas;
	}
	
	public void StartDialog(int id)
	{
		cntId = id;
		speaker.text = dialogParserDatas[id].speaker;
		dialog.text = dialogParserDatas[id].dialogue;
		for (int i = 0; i < choiceButtons.Count; i++)
		{
			choiceButtons[i].SetActive(false);
		}
		Debug.Log($"speaker {dialogParserDatas[id].speaker}\n id {id}");
	}

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

	public void ChoicesDialog()
	{
		ChoiceButtonsActive();
	}

	public void EndDialog()
	{
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

	private void ChoiceButtonsActive()
	{
		for (int i = 0; i < choiceButtons.Count; i++)
		{
			if(dialogParserDatas[cntId].choices[i] != "")
			{
				int index = i;
				choiceButtons[index].SetActive(true);
				choices[index].text = dialogParserDatas[cntId].choices[index];
				Debug.Log($"next = {dialogParserDatas[cntId].nextId[index]}");
				//Debug.Log($"next = {dialogParserDatas.Count}");
				choiceButtons[index].GetComponent<Button>().onClick.AddListener(() => StartDialog(dialogParserDatas[cntId].nextId[index]));
			}
		}
    }
}
