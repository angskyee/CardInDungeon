# Card Game

- 상호작용 카드 게임입니다

## 소개

이 프로젝트는 Unity 엔진을 사용하여 개발되었으며, 플레이어 카드를 클릭하거나 상대 카드와 부딧히면서 다양한 상호작용을 할 수 있습니다.

  전투 형식:
  1. 던전 입장전 받을 보상을 랜덤으로 하나씩 뽑습니다. 뽑으면 안좋은 카드(몬스터, 함정)도 하나씩 추가됩니다.
  2. 카드를 랜덤으로 섞고 위에서부터 모든 카드와 상호작용 해야합니다.
  3. 실패시 패널티를 가지게됩니다.
  4. 성공시 모든 보상과 상호작용 할 수 있습니다.

  성장형으로 던전에 실패해도 패널티만 가질지 아니면 죽으면 새로운 게임을 할지는 미정입니다.
  

## 상호작용

- 상대몬스터 : 전투
- 보물상자 : 보상(스텟 증가 물약, 장비, 마을 발전 등...)
- 인벤토리 아이템 : 장착
- 상점 : 방문 및 거래(상점 카드 터치 : 상점 주인 카드 주변에 아이템 카드가 펼쳐져나온다, 아이템 카드 터치 : 일정 금액을 지불하고 상호작용한다)
- 던전 : 상대 몬스터와 보상 세팅 (쌓인 카드 더미와 상호작용하여 보상 카드를 더한다)

## 시작하며

- 마우스 클릭으로만 이루어진 게임입니다
