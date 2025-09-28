# Unimo Space Adventure
[![YouTube](https://img.shields.io/badge/YouTube-FF0000?style=for-the-badge&logo=youtube&logoColor=white)](https://youtu.be/YOUR_VIDEO_LINK)

## 🗺️ 프로젝트 소개
VR 환경에서 즐기는 로그라이크 기반 닷지 액션 게임입니다. 플레이어는 꿀벌 로봇 ‘리비’가 되어 새로운 행성을 탐험하고, 변화하는 맵과 적의 다양한 패턴을 회피하며 자원을 채취하고 성장합니다.

## 👨‍💻 개발자
**정도균 (JungDoKyoun)**

## 📌 주요 기능

### 🗺️ 육각형 타일 시스템

#### 1. **절차적 육각형 맵 생성**
```csharp
// 연결된 육각형 맵 자동 생성
public void GenerateConnectedMap()
// 이웃 타일 계산 알고리즘
public List<Vector2Int> GetNeighbors(Vector2Int coord)
```
- **Flood Fill 알고리즘**: 중앙에서 시작해 80개의 연결된 타일 생성
- **타일 역할 자동 할당**: Boss(거리별 배치), Event(20% 비율), Mode(탐험/채집)
- **환경 타입 클러스터링**: 화산, 얼음, 어둠 등 환경을 군집으로 배치
- **난이도 시스템**: 중앙에서 거리에 따른 9단계 난이도 자동 설정

#### 2. **타일 역할 분배 알고리즘**
```csharp
private void AssignTileRoles() {
    AssignBossTiles(candidateCoords);  // 거리별 보스 배치
    AssignEventTiles(candidateCoords); // 20% 이벤트 타일
    AssignModeTiles(candidateCoords);  // 나머지 모드 타일
}
```
- **보스 타일 전략적 배치**: 거리별 최소 간격 보장, 각 보스 근처 상점 자동 생성
- **이벤트 타일 분산 배치**: 최소 거리 2칸 유지로 고른 분포
- **환경 클러스터링**: BFS 알고리즘으로 3~8개 타일씩 환경 군집 생성
- **난이도 자동 계산**: 중앙에서의 거리로 9단계 난이도 자동 설정

### 🎪 이벤트 시스템

#### 3. **상점 이벤트 시스템**
```csharp
public void OpenShopUI(List<RelicData> relics, Vector3 worldPos)
// 동적 상점 생성 및 거래 시스템
```
- **랜덤 유물 생성**: 플레이어가 미보유한 유물 중 랜덤 선택
- **화폐 시스템**: InGameCurrency, MetaCurrency, Blueprint 3종 화폐
- **체력 수리 기능**: 10% 수리(100골드), 100% 수리(500골드)
- **즉시 구매 피드백**: 구매 성공/실패 팝업 표시

#### 4. **스크립트 이벤트 시스템**
```csharp
public class ProbabilisticEffect {
    public float _probability;
    public List<EventEffect> _effects;
}
```
- **선택지 기반 이벤트**: 각 이벤트당 최대 4개 선택지
- **확률적 결과**: 선택지별 다중 확률 결과 (0~100%)
- **조건부 선택지**: 특정 유물/건물 보유 시에만 활성화
- **다양한 효과**: 자원 변경, HP/연료 변경, 유물 획득/손실

### 💾 데이터 관리 시스템

#### 5. **게임 상태 저장/복원**
```csharp
public class GameStateManager : MonoBehaviour {
    Dictionary<Vector2Int, TileData> _tileSaveData;
    public void SaveTileStates()
    public void RestoreMapState()
}
```
- **씬 전환 시 맵 보존**: 전투 씬 진입 후 복귀 시 맵 상태 유지
- **타일 클리어 상태 저장**: 이미 클리어한 타일 표시
- **플레이어 위치 저장**: 마지막 위치에서 게임 재개
- **Singleton 패턴**: DontDestroyOnLoad로 영구 보존

#### 6. **IEffect 인터페이스 효과 시스템**
```csharp
public interface IEffect {
    void Execute(EventEffect eventEffect);
}
// 6가지 구현체: ChangeResource, ChangeRelic, ChangeHP 등
```
- **전략 패턴 적용**: 효과별 독립적인 클래스로 확장성 확보
- **Dictionary 매핑**: EffectType enum으로 효과 인스턴스 관리
- **비동기 처리**: Coroutine으로 Firebase 연동 및 UI 업데이트
- **확장 용이성**: 새로운 효과 추가 시 IEffect 구현만으로 가능

## 🎮 조작법

| 키/버튼 | 동작 |
|---------|------|
| 마우스 좌클릭 | 타일 선택 |
| 타일 클릭 | 이동/이벤트 실행 |
| ESC | UI 닫기 |

## 🛠 기술 스택
- **엔진**: Unity 2021.3 LTS
- **언어**: C# (.NET Framework)
- **디자인 패턴**: Singleton, Factory Pattern
- **알고리즘**: Flood Fill, A* Pathfinding, Hexagonal Grid
- **도구**: Visual Studio 2022, Git

## 🎯 시스템 특징
- **육각형 좌표계**: Axial Coordinate System 구현
- **절차적 생성**: 매 게임마다 다른 맵 레이아웃
- **확장 가능한 이벤트**: ScriptableObject로 쉬운 콘텐츠 추가
- **최적화된 렌더링**: 시야 범위 외 오브젝트 비활성화

## 📚 주요 학습 내용
- 육각형 그리드 수학 및 좌표계 변환
- Mesh 생성을 통한 프로시저럴 타일 렌더링
- ScriptableObject를 활용한 데이터 드리븐 설계
- 확률 기반 이벤트 시스템 구현
- Dictionary와 HashSet을 활용한 효율적인 타일 관리

## 🔧 개선 예정 사항
- 턴 기반 전투 시스템 통합
- 타일별 특수 효과 (버프/디버프)
- 미니맵 UI 구현
- 세이브/로드 시스템
- 멀티플레이어 지원

## 📄 라이선스
This project is licensed under the MIT License
