using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
	private static readonly string DIALOG_END = "end";
	private static readonly string BLANK = "";
	public int id;
	public TMP_Text speaker;
	public TMP_Text dialog;
	public GameObject choiceButton1;
	public GameObject choiceButton2;
	public GameObject choiceButton3;
	public TMP_Text choice1;
	public TMP_Text choice2;
	public TMP_Text choice3;
	public int nextId1;
	public int nextId2;
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
		//Debug.Log(dialogParserDatas[id].choice1);
	}

	public bool NextDialog()
	{
		if (dialogParserDatas[cntId].choice1 == DIALOG_END)
		{
			EndDialog();
			Debug.Log("End");
			return true;
		}
		else if(dialogParserDatas[cntId].choice1 != BLANK)
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

	}

	public void EndDialog()
	{
		cntId = -1;
	}

	//버튼 초기화
	private void ButtonInit()
	{
		choice1 = choiceButton1.transform.GetChild(0).GetComponent<TMP_Text>();
		choice2 = choiceButton2.transform.GetChild(0).GetComponent<TMP_Text>();
		choice3 = choiceButton3.transform.GetChild(0).GetComponent<TMP_Text>();
		choiceButton1.SetActive(false);
		choiceButton2.SetActive(false);
		choiceButton3.SetActive(false);
	}
}
