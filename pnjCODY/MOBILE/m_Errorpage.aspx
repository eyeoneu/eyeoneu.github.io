﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="m_Errorpage.aspx.cs" Inherits="m_Errorpage" %>
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="ko" lang="ko">
<head id="Head1" runat="server">
    <!--언어셋 설정 -->
    <meta charset="UTF-8" />
    <!-- 오페라 미니, 아이팟터치/아이폰 사파리 환경 및 기타 모바일 브라우져용 메타설정 -->
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" user-scalable="no," target-densitydpi="medium-dpi" />    
    <!-- 전화번호 형식 자동 링크 제외 (수동 링크시 <a href="tel:010-1234-5678">링크명</a> , 문자: <a href="sms: ) - 2011-11-23 김재영 추가 -->
    <meta name="format-detection" content="telephone=no" />
    <!-- 최신 IE 버전의 Standards 모드로 설정  -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <!-- 주소창 아이콘 -->
    <link rel="shortcut icon" href="favicon.ico" />
    <!-- 사이트 타이틀 -->
    <title>코디서비스</title>
    <!-- 아이폰 홈스크린 추가시 아이콘 -->
    <link rel="apple-touch-icon" href="images/m/iOS-114.png" />
    <!-- 웹폰트 나눔고딕 적용 -->
    <link rel="stylesheet" href="http://api.mobilis.co.kr/webfonts/css/?fontface=NanumGothicWeb" type='text/css' />
    <!--  IE9 이하 버전에서 HTML5 호환을 위한 스크립트 -->
    <!--[if lt IE 9]>
    <script src="Common/Scripts/html5.js"></script>
    <![endif]-->
    <!-- CSS 불러오기 -->
    <link rel="StyleSheet" href="/Common/CSS/mobile_portrait.css" type="text/css" media="screen" id="orient_css" />
    <style type="text/css">
        .hidden { display:none; }
    </style>
    <script type="text/javascript">
        // 모바일 접속시 주소창 자동으로 위로 올리기
        var ua = window.navigator.userAgent.toLowerCase();
        if (/iphone/.test(ua) || /android/.test(ua) || /opera/.test(ua) || /bada/.test(ua)) { // 모바일 접속시 아래 스크립트 실행 - 2011-11-23 김재영 추가
            if (window.attachEvent) {
                window.attachEvent("onload", function () {
                    setTimeout(scrollTo, 0, 0, 1);
                }, false);
            }
            else {
                window.addEventListener("load", function () {
                    setTimeout(scrollTo, 0, 0, 1);
                }, false);
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
       <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>모바일 웹</strong></p>
    </header>    
    <div class="title">
        <h2 class="mepm_title">Page Error!</h2>
    </div> 
    <article style="padding-bottom:1em;">
        <section>
        <div style="background-image:url('/images/m/error.png'); width:320px; height:310px; margin-left:auto; margin-right:auto; text-align:center;">
        <p style="height:95px;"></p>
        <span style="font-size:1.4em; font-weight:bold;">죄송합니다.<br />
        페이지에 오류가 발생했습니다.<br /></span> 
        <br /><br />
        <p style="color:#09c;">요청하신 페이지를 일시적으로 사용할 수 없습니다.</p><br />
        동일한 문제가 지속적으로 발생하는 경우는<br />
        <span style="font-weight:bold; color:#069;">코디서비스 담당자</span>에게 문의해 주십시오.<br /><br />
        <a onClick="history.back();"><span class="button orange mepm_btn">이전으로 이동</span></a>
        <a href="m_Default.aspx"><span class="button orange mepm_btn">처음으로 이동</span></a>
        </div>        
        </section>
    </article>
    <div class="footer_top">
        <p style="word-spacing:0.8em;">
            <a href="/m_Default.aspx">처음으로</a> | <asp:LinkButton ID="lbtLogOut" 
                runat="server" onclick="lbtLogOut_Click">로그아웃</asp:LinkButton> | 도움말</p>
    </div>
    <footer><p>&#169; CODY SERVICE</p></footer>
    </form>
</body>
</html>
