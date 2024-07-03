using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public struct DialogParserData
{
	public int index;
	public string speaker;
	public string dialogue;
	public string choice1;
	public int next1;
	public string choice2;
	public int next2;
	public string choice3;
	public int next3;
}

public class DataParser : MonoBehaviour 
{
	private static readonly string FILE_DATA = "log";
	private static readonly string INDEX = "Index";
	private static readonly string SPEAKER = "Speaker";
	private static readonly string DIALOGUE = "Dialogue";
	private static readonly string CHOICE1= "Choice1";
	private static readonly string NEXT1 = "Next1";
	private static readonly string CHOICE2 = "Choice2";
	private static readonly string NEXT2 = "Next2";
	private static readonly string CHOICE3 = "Choice3";
	private static readonly string NEXT3 = "Next3";
	public List<DialogParserData> dialogDatas = new List<DialogParserData>();

	private void Awake()
	{
		var parser = CSVReader.Read(FILE_DATA);
        foreach (var item in parser)
        {
			//Debug.Log($"next3.GetType() {item[NEXT3].GetType()}");
			var index = (int)item[INDEX];
			var speaker = (string)item[SPEAKER];
			var dialogue = (string)item[DIALOGUE];
			var choice1 = (string)item[CHOICE1];
			var choice2 = (string)item[CHOICE2];
			var choice3 = (string)item[CHOICE3];
			int next1 = item[NEXT1].GetType() == 1.GetType() ? (int)item[NEXT1] : -1;
			int next2 = item[NEXT2].GetType() == 1.GetType() ? (int)item[NEXT2] : -1;
			int next3 = item[NEXT3].GetType() == 1.GetType() ? (int)item[NEXT3] : -1;

			dialogDatas.Add(GetDialogData(index, speaker, dialogue, choice1, choice2, choice3, next1, next2, next3));
        }
    }

	private DialogParserData GetDialogData(int _id, string _speaker, string _dialogue, string _choice1, string _choice2, string _choice3, int _next1, int _next2, int _next3)
	{
		DialogParserData tmpdate = new DialogParserData();
		tmpdate.index = _id;
		tmpdate.speaker = _speaker;
		tmpdate.dialogue = _dialogue;
		tmpdate.choice1 = _choice1;
		tmpdate.choice2 = _choice2;
		tmpdate.choice3 = _choice3;
		tmpdate.next1 = _next1;
		tmpdate.next2 = _next2;
		tmpdate.next3 = _next3;

		return tmpdate;
	}
}
