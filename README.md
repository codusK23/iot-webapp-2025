# iot-webapp-2025
<p>IoT 개발자 과정 <a href="https://dotnet.microsoft.com/ko-kr/apps/aspnet" target="_blank" style="color:red;">ASP.NET Core</a> 학습 리포지토리</p>

## 1일차

### Web
- 인터넷 상에서 사용되는 서비스 중 하나
- 웹을 표현하는 기술 : HTML(Hyper Text Markup Language). XML(eXtensable Markup Language)의 경량화 버전
- 2014년 이후 HTML5로 적용되고 있음

#### 웹 기술
- 웹 표준기술(프론트엔드) : HTML5(웹페이지 구조) + CSS3(디자인) + JavaScript6(인터렉티브)
- 웹 `서버`기술(백엔드) : ASP.NET Core(C#|VB), SpringBoot(Java), Flask|dJango(Python), CGI(PHP, C), Ruby, ...
- 웹 서비스(서버) : 프론트엔트 + 백엔드
- 웹 브라우저 상에서 동작 : 현재는 웹 브라우저 상에서만 동작하는 경계가 사라졌음

#### HTML5
- 웹페이지를 구성하는 언어(근간, 기본)
- HTML 상에서도 디자인을 할 수 있으나, 현재는 CSS로 분리

#### CSS3
- Cascading Style Sheet : 객체지향에 사용되는 부모 자식 관계로 디자인 하는 기술
- 아주 쉬운 문법으로 구성됨

#### JavaScript
- 표준명 ECMAScript 2024
- Java와 전혀 관계 없음. Java의 문법을 차용해서 사용한 웹 스크립트 언어
- 엄청난 발전을 이뤄 여러가지 기술로 분리
    - React.js, View.js 등의 프론트엔드 기술 언어로 분파
    - Node.js와 같은 웹 서버기술에도 적용
    - VS Code(아톰에디터 기반) 같은 개발도구를 만드는데도 사용
    - 3D 게임, 모바일 개발 등 다양한 분야에 사용

#### 웹 서버기술
- `ASP.NET Core` : C#/VB언어도 웹 서버를 개발
- SpringBoot, Flask 등 다른 언어로 웹 서버를 개발해도 무방

#### 왜 웹을 공부해야하는가?
- 스마트팩토리 솔루션을 대부분 웹으로 개발(사용범위 제약을 없애기 위해서)
    - 웹 사이트, 일부분 모바일 앱 동시 개발
- 스마트홈(IoT), ERP, 병원예약, 호텔예약, 인터넷뱅킹, 온라인서점 ...
- 모든 IT/ICT 개발에 웹 기술은 포함되어 있음

#### HTTP
- Hyter Text Transfer Protocol
- 웹을 요청/응답하는 프로토콜
- HTTPs : PTTP with secure. 보안을 강화한 HTTP 프로토콜

### 웹 표준기술 학습

#### VS Code 확장설치
- Live Server

#### HTML 구조
- [소스](./day01/html01.html)
- html 태그 내에 head, body로 구성 (무조건!)
- README.md에도 HTML 태그를 그대로 사용가능 (heading은 적용 안 됨)
- VS Code에서 html:5 자동생성
- [소스](./day01/html02.html), [소스](./day01/html03.html)
- CSS가 소스라인을 많이 사용. css는 외부스타일로 분리 사용
- JS도 소스라인이 매우 긺. JS도 외부스크립트로 분리 사용
- 웹 브라우저의 개발자모드(F12)로 디버깅을 하는 것이 일반적

#### HTML 기본태그(body에 사용)
- [소스](./day01/html04.html)
- h1 ~ h6 : 제목글자
- p, br, hr : 본문, 한줄내려가기, 가로줄
- a : 링크
- b/strong, i, small, sub, sup, ins/u, del : 굵은체, 이탤릭체, 작은글씨, 아래첨자, 위첨자...
- ul/ol, li : 동그라미목록/순번목록, 목록아이템
- table, tr, th, td : 테이블, 테이블로, 테이블헤더, 테이블컬럼
- img, audio, video : 이미지, 오디오, 비디오 
- [소스](./day01/html05.html), [소스](./day01/html06.html)
- form, input, button, select, textarea, label : 입력양식, 텍스트박스, 버튼, 콤보박스, 여러줄텍스트박스, 라벨
- progress : 진행률
- div, span : 공간분할

#### 공간분할태그
- [소스](./day01/html07.html)
- div 사용 이전엔 table, tr, td로 화면 분할을 활용
- table을 여러번 중복하면 렌더링속도 저하로 화면이 빨리 표시가 안됨
- 웹 기술표준을 적용해서 div 태그로 공간분할을 시작
- div를 CSS로 디자인 적용해서 렌더링속도를 빠르게 변경
- 게시판 목록, 상세보기 등에서는 아직도 table을 사용 중

#### 시맨틱웹
- 웹구조를 좀더 구조적으로 세밀하게 구분짓는 의미로 만들어진 웹 구성방식
- 시맨틱 태그
    - header, nav, main, section, aside, article, footer 등
    - 기본 HTML 태그가 아니고, 필수도 아님
- 최근에는 잘 사용안함. div태그에 id로 부여해서 유사하게 사용 중
- div만 잘 쓰면 됨

### 웹 표준기술 - CSS

#### 개요
- 마크업 언어에 표시방법을 기술하는 종속형 시트(계단식 스타일시트)
- WPF는 CSS와 유사한 방식을 차용
- 문법
    ```css
    태그/아이디/클래스 {
        /* key: value를 반복*/
        key: value; /* C++ 주석 // 한줄 주석은 안됨 */
    }
    ```
- html 태그 속성
    - id : 웹페이지 하나당 한번만 쓸것
    - class : 여러번, 여러 개 사용가능

- UI기술로 많은 분야에서 사용
    - Qt, PyQt, Electron, Flutter(모바일), React Native(모바일), React.js, ...
- [소스](./day01/html08.html)


## 2일차

### 웹 표준기술 - CSS
- HTML, CSS, JS 동일하게 "(쌍따옴표), '(홑따옴표) 동시 사용가능
- Python은 ''를 추천, 웹은 ""를 추천

### 웹 표준기술 - JavaScript
- Java(컴파일러언어)와 아무런 관계없음
- JavaScript(스크립트언어)

#### 기본문법
- HTML내에 Script 태그 내에 작성
- 변수 선언이 var(전역, 지역), let(지역)
- 문장 끝에 ; 생략이 가능하지만 되도록 사용할 것
- 키워드
    <img src="./image/web0001.png" width="600">
- 화면메시지박스
- 디버깅 출력 : console.log()
    ```js
    <script>
        // 변수 선언
        var radius = 10;
        var PI = 3.14159265;

        // 출력
        alert(2 * radius * PI);
        console.log(2 * radius * PI);
        // 개발자도구 > 소스에서 디버깅 가능
        // VisualStudio와 동일
    </script>
    ```
- 변수타입 : 숫자, 문자열, 불린, ...
    - null : undefined
- 연산자 : 비교연산자, 수식연산자, 논리연산자, ...

## 5일차

### ASP.NET Core
- ASP : Active Server Page. Classic ASP라고 부름. 동적인 웹페이지를 만드는 기술
- 프론트엔드(HTML + CSS + JS) 상에서 동작하는 기술을 동적 웹페이지라고 부르지 않음
- 동적 웹페이지 : 사용자가 웹서버에 요청을 해서 값이 변경되는 것

    <img src="./image/web0007.png" width="600">

- ISS : Internet Information Service. MS가 윈도우 운영체제에 포함시킨 웹 서버 기술
    - 윈도우 프로그램 추가 제거(appwiz.cpl)
    - 윈도우 기능 켜기/끄기 > 인터넷 정보 서비스 클릭 후 확인

- 윈도우 설정은 일반 사용자용, 제어판은 개발자용
    - 제어판 > Windows Tools > IIS(인터넷 정보 서비스) 관리자

    <img src="./image/web0008.png" width="600">

- Java가 1995년 출현하면서 Classic ASP, Classic VB 등의 옛날 MS기술이 위협
- 2000년 초반에 .NET 프레임워크를 출시
    - C#, VB(.NET) 새로운 언어들이 포함됨
    - 위의 언어들로 개발할 수 있는 웹 기술인 ASP.NET 등장
    - 이후 언어는 계속 발전
    - 2016년에 멀티플랫폼 ASP.NET Core
    - 2020년에 .NET Framework(Windows전용)을 .NET 5.0(멀티플랫폼)로 변경

#### ASP.NET 장점
- 빠르다 : 초창기 ASP.NET은 C#으로 Winforms 만드는 것처럼 개발(개발생산성은 좋지만 렌더링 속도가 매우 느렸음). MVC모델로 분리하면서 윈폼식 개발을 제거
- 오픈소스 : Java JSP/Spring, Python Flask 등이 오픈소스로 발전하니까 MS도 오픈소스 전향
- 크로스플랫폼 : Windows에서만 동작하던 것을 MacOS, Linux 등으로 확대시킴
- 종속성 주입 : Dependency Injection. Spring 쪽에 특화되던 기술을 접목해옴. 개발시간 절약
- 개발용 웹서버 : IIS가 Visual Studio에 포함. 웹서버 설정을 할 필요가 없음
- 클라우드 친화적 : MS Azure 등의 클라우드와 연계가 쉬움
- MVC 모델 : Model View Controller를 따로 개발. Spring Boot도 동일
- 최적화가 잘 되어 있음

#### ASP.NET Core 활용처
- `웹 사이트` 개발 : 기본적인 내용
- `풀스택` 개발 : 프론트엔드 (React, Vue, Angular js) + 백엔드(ASP.NET Core)
- `API 서버` 개발 : TMDB 영화 데이터 조회 API, Youtube API, 데이터 포털 API 등의 데이터만 주고받는 서비스 개발
- 도메인 특화 솔루션 개발 : MES, ERP, SmartFactory, SmartShip 등
- 이커머스 개발 : 쇼핑몰, 온라인 서점, 여행예매 사이트 등

#### ASP.NET Core 시작
1. Visual Studio 시작 > 새 프로젝트 만들기
2. ASP.NET Core 웹앱(Model-View-Controller) 선택
3. 프로젝트 이름, 위치, 솔루션 이름 입력
4. 추가 정보
    - HTTPS : 보안 인증서를 신청, 다운로드 설정을 해야 함. 복잡
    - '최상위 문 사용 안 함' 만 체크
5. 빌드 후 실행
6. properties > launchSetting.json에서 
