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
			Debug.Log("DialogManager������Ʈ�� �̱��濡 ���� ����");
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
	//������ ��������
	private void GetDialog()
	{
		dialogParserDatas = dataParser.dialogDatas;
	}
	//��ȭ�� ȭ�鿡 ���� �Լ�
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
	//��ȭ�� �������� �ѱ�� �Լ�
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
	//������ Ȱ��ȭ
	public void ChoicesDialog()
	{
		ChoiceButtonsActive();
	}
	//��ȭ ����
	public void EndDialog()
	{
		dialogPanel.SetActive(false);
		cntId = -1;
	}

	//��ư �ʱ�ȭ
	private void ButtonInit()
	{
        foreach (var button in choiceButtons)
        {
			choices.Add(button.transform.GetChild(0).GetComponent<TMP_Text>());
			button.SetActive(false);
        }
	}
	//������ ��ư Ȱ��ȭ
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
