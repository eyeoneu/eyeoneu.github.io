<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_Login_form.aspx.cs" Inherits="m_Login_form" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        var objMemId = "<%=txtMemID.ClientID %>";
        var objPassword = "<%=txtPassword.ClientID %>";

        function regFormCheck() {
            var frm = document.form1;

            if (document.aspnetForm[objMemId].value == "") {
                alert("아이디를 입력해 주십시오.");
                document.aspnetForm[objMemId].focus();
                return false;
            }
            if (document.aspnetForm[objPassword].value == "") {
                alert("비밀번호를 입력해 주십시오.");
                document.aspnetForm[objPassword].focus();
                return false;
            }

            var txtMemID = document.getElementById("<%=txtMemID.ClientID %>");
            var hdMemID_Encrypt = document.getElementById("<%=hdMemID_Encrypt.ClientID %>");
            var txtPass = document.getElementById("<%=txtPassword.ClientID %>");
            var hdPass_Encrypt = document.getElementById("<%=hdPass_Encrypt.ClientID %>");

            try {
                hdMemID_Encrypt.value = Encrypt(txtMemID.value);
                hdPass_Encrypt.value = Encrypt(txtPass.value);
                if (hdMemID_Encrypt.value == null || hdPass_Encrypt.value == null) {
                    alert("암호화에 실패했습니다.");
                    return false;
                }
                txtMemID.value = "";
                txtPass.value = "";
            } catch (e) {
                alert("암호화에 실패했습니다.");
                return false;
            }
        }

        //Cookie를 생성하는 Function
        function newCookie(name,value,days) { //쿠기를 생성하는 function

            var days = 100;   // 쿠키저장 일수
            if (days) {
                var date = new Date();  //날짜 객체 생성
                date.setTime(date.getTime()+(days*24*60*60*1000)); //10일로 설정된 시간을 밀리세컨드로 변환
                var expires = "; expires="+date.toGMTString(); //쿠키 만료일을 변수 expires에 설정함
            } 
            else
                var expires = ""; //days 변수가 초기화 안될 경우 expires를 NULL로 초기화
 
            document.cookie = name+"="+value+expires+"; path=/"; //쿠키생성 
        }

        //name의 Cookie값을 검색하여서 값을 가져오는데 없으면  NULL을 반환
        function readCookie(name) {
            //name으로 Cookie의 값을 검색해서 반환한다 없으면 NULL을 반환
            var nameSG = name + "=";
            var nuller = '';
            if (document.cookie.indexOf(nameSG) == -1)  //Cookie를 검색
            return nuller;

            var ca = document.cookie.split(';');
 
            for(var i=0; i<ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0)==' ') c = c.substring(1,c.length);
            if (c.indexOf(nameSG) == 0) return c.substring(nameSG.length,c.length);
            }
            return null;
        }

        //checkbox의 에 따라서 Cookie를 설정(체크이벤트를 잡아서 동작)
        function changCB()
        {
            if ( !document.getElementById('<%= this.chkChecker.ClientID %>').checked)
                delMem();
            else
                toMem();
        }

        //Cookie에 값을 넣는다.
        function toMem()
        {
            newCookie('theName', document.getElementById('<%= this.txtMemID.ClientID %>').value);
        }

        //Cookie에 값을 ""로 초기화 한다.
        function delMem()
        {
            newCookie('theName',"",1);
            document.getElementById('<%= this.txtMemID.ClientID %>').value = '';   // add a line for every field
        }

        //window.load 이벤트에 호출하는 function으로 페이지가 로딩 되면서 값을 세팅하는 부분
        function remCookie()
        {
            if (readCookie("theName") == " " || readCookie("theName") == "" )
                document.getElementById('<%= this.chkChecker.ClientID %>').checked = false;
            else
            {
                document.getElementById('<%= this.txtMemID.ClientID %>').value = readCookie("theName");
                document.getElementById('<%= this.chkChecker.ClientID %>').checked = true;
            }
        }

        //window.load이벤트 호출부
        function addLoadEvent(func) {
            var oldonload = window.onload;
            if (typeof window.onload != 'function') {
                window.onload = func;
            } 
            else 
            {
                window.onload = function() {
                    if (oldonload)
                    {
                        oldonload();
                    }
                    func();
                }
            }
        }

        addLoadEvent(function() { 
        //웹페이지가 로딩되면서 동작하는 function
        remCookie();
        });
    </script>
    <div class="loginh"><img src="../images/m/moblieImage.png" alt="코디서비스"></div>
    <article>
        <!-- 로그인 메뉴 시작 -->
        <section>        
            <div class="logindiv">
                <ul>                
                <li class="input_area">
                 <span class="l">
                    <label>아이디 :</label>
                    </span>
                    <span class="c">
                    	<span id="idb" class="input_txt_area input_txt_area_v1">
                            <asp:TextBox ID="txtMemID" runat="server" CssClass="input_txt input_txtc" TabIndex="1"></asp:TextBox>
                            <asp:HiddenField ID="hdMemID_Encrypt" runat="server" />
                        </span>
                    </span>
                    <span class="r">
                        <label>
                            <asp:CheckBox ID="chkChecker" runat="server" Text="저장하기" Checked="true" onchange="changCB()">
                            </asp:CheckBox>
                        </label>
                    </span>
                </li>
                <li class="input_area">
                <span class="l">
                    <label>비밀번호 :</label>
                    </span>
                    <span class="c">
                    	<span id="pwb" class="input_txt_area input_txt_area_v1">
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="input_txt input_txtc" TextMode="password" TabIndex="2"></asp:TextBox>
                            <asp:HiddenField ID="hdPass_Encrypt" runat="server" />	
                        </span>
                    </span>
                    <span class="r">
                        <asp:Button ID="ibtnLogin" runat="server" Text="로그인" OnClientClick="return regFormCheck(); if (this.chkSaveID.checked) toMem" OnClick="ibtnLogin_Click" CssClass="lbtn ls" />
                    </span>
                </li>
                <li class="input_area">
                    <span class="s">
                        <label>

                        </label>
                    </span>
                </li>
                <li class="input_area"><span class="u">아이디·비밀번호 찾기는 관리자에게 문의하세요.</span></li>
                </ul>               
             </div>
        </section>
    </article>
</asp:Content>
