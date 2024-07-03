using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using BehaviorTree;

public class PlayerBT : Tree
{
	public PlayerManager pm;

	//플레이어 스킬
	//플레이어 죽음
	//플레이어 아이템 먹기
	//플레이어 아이템 사용
	//플레이어 장비 착용
	//플레이어 장비 & 아이템 버리기
	//플레이어 인벤토리
	//플레이어 퀘스트
	//플레이어 다이얼로그
	protected override void OnEnter()
	{
		pm = GetComponent<PlayerManager>();
	}
	protected override Node SetupTree()
	{
		Node root = new Selector(new List<Node>
		{
			new Parallel(1, new List<Node>
			{
				new PlayerMove(pm),	//플레이어 이동
				new PlayerJump(pm),	//플레이어 점프
			}),
			new PlayerAttack(pm),		//플레이어 공격
			new PlayerDialog(pm),

		});
		return root;
	}
}
