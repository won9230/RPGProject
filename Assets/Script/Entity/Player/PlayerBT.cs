using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using BehaviorTree;

public class PlayerBT : Tree
{
	public PlayerManager pm;

	//�÷��̾� ��ų
	//�÷��̾� ����
	//�÷��̾� ������ �Ա�
	//�÷��̾� ������ ���
	//�÷��̾� ��� ����
	//�÷��̾� ��� & ������ ������
	//�÷��̾� �κ��丮
	//�÷��̾� ����Ʈ
	//�÷��̾� ���̾�α�
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
				new PlayerMove(pm),	//�÷��̾� �̵�
				new PlayerJump(pm),	//�÷��̾� ����
			}),
			new PlayerAttack(pm),		//�÷��̾� ����
			new PlayerDialog(pm),

		});
		return root;
	}
}
