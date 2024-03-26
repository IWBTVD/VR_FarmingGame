using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; //GraphicRaycaster 사용하기 위한 지시문

public class CameraEvent : BaseInputModule //BaseInputModule 클래스 상속
{
    public GraphicRaycaster graphicRaycaster; //Canvas에 있는 GraphicRaycaster
    private List<RaycastResult> raycastResults; //Raycast로 충돌한 UI들을 담는 리스트
    private PointerEventData pointerEventData; //Canvas 상의 포인터 위치 및 정보
    public Camera target; //마우스 커서 역할을 대신할 카메라

    protected override void Start()
    {
        pointerEventData = new PointerEventData(null); //pointerEventData 초기화
        pointerEventData.position = new Vector2(target.pixelWidth * 0.5f, target.pixelHeight * 0.5f); //카메라의 중앙으로 포인터 지정
        raycastResults = new List<RaycastResult>(); //리스트 초기화
    }

    private void Update()
    {
        graphicRaycaster.Raycast(pointerEventData, raycastResults); //포인터 위치로부터 Raycast 발생, 결과는 raycastResults에 담긴다

        if (raycastResults.Count > 0) //충돌한 UI가 있으면
        {
            foreach (RaycastResult raycastResult in raycastResults) //충돌한 UI 탐색
            {
                HandlePointerExitAndEnter(pointerEventData, raycastResult.gameObject); //호버링 이벤트 전달
                if (Input.GetKeyDown(KeyCode.Space)) //임의의 버튼을 클릭하면
                    ExecuteEvents.Execute(raycastResult.gameObject, new BaseEventData(eventSystem), ExecuteEvents.submitHandler); //해당 UI에 클릭 이벤트 전달
            }
        }
        else //충돌한 UI가 없으면
        {
            HandlePointerExitAndEnter(pointerEventData, null); //포인터 벗어남 → GameObject가 null이어야 호버링에서 벗어남
        }

        raycastResults.Clear(); //Raycast 결과 리스트 초기화 → 필수
    }

    public override void Process() { } //상속받아야 에러 안뜸
    protected override void OnEnable() { } //상속받아야 에러 안뜸
    protected override void OnDisable() { } //상속받아야 에러 안뜸
}