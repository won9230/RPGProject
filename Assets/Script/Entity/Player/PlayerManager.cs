using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : Entity
{
	[Header("컴포넌트")]
	public CharacterController controller;
	public Animator animator;

	[Header("플레이어 이동 관련")]
	public float moveSpeed;
	public float sprintSpeed = 7f;
	[Range(0.0f, 0.3f)]
	public float rotationSmoothTime = 0.12f;
	public float speedChangeRate = 1f;
	[Header("플레이어 점프 관련")]
	public float jumpHeight = 1.2f;
	public float fallTimeout = 0.15f;
	public float jumpTimeout = 0.70f;
	public float gravity = -15.0f;
	[Header("플레이어 뷰 관련")]
	public float viewAngle = 0f;
	public float viewRadius = 1f;
	public LayerMask tragetMask;
	public LayerMask obstacleMask;
	[Header("스킬 키")]
	public KeyCode[] keys;
	[Header("기타")]
	public LayerMask groundLayer;
	public float groundCheck = 0.2f;
	public GameObject mainCamera;
	//숨김
	//이동 관련
	[HideInInspector] public float speed;
	[HideInInspector] public float targetRotation = 0f;
	[HideInInspector] public float rotationVelocity = 0f;
	[HideInInspector] public float verticalVelocity = 0f;
	//점프 관련
	public float fallTimeoutDelta;
	public float jumpTimeoutDelta;
	public float terminalVelocity = 53.0f;
	public bool isGround = false;   //바닥 체크
	//애니메이션 관련
	public bool hasAnimator;
	public int animIDSpeed;
	public int animIDJump;
	public float animationBlend;

	//기타

	private void Awake()
	{
		hasAnimator = TryGetComponent(out animator);
		controller = GetComponent<CharacterController>();
		keys = new KeyCode[] { KeyCode.Q, KeyCode.E, KeyCode.R, KeyCode.F, KeyCode.Tab };
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	private void Start()
	{
		AssignAnimationIDs();
		fallTimeoutDelta = fallTimeout;
		jumpTimeoutDelta = jumpTimeout;
	}

	//애니메이션 아이디 저장
	private void AssignAnimationIDs()
	{
		animIDSpeed = Animator.StringToHash("Speed");
		animIDJump = Animator.StringToHash("Jump");
	}

}
