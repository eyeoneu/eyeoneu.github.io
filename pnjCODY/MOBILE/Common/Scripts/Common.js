// JScript 파일


/*********************************************************
용도 : 삭제확인 경고
*********************************************************/
function delConfirmSimple()
{
	if(confirm('정말 삭제하시겠습니까?')) 
		{return true;}
	else{return false;}
}



//트리밍
String.prototype.Trim= new Function("return this.replace(/^\\s+|\\s+$/g,'')")	


//확인창 띄우기 - 삭제등 확인하는 팝업을 다르게 띄울수 있도록 이 함수를 사용한다.
//confirm_popup("버튼수(1/2)", "메세지", "색상(blue/red등)", "가로창사이즈", "세로창사이즈")
function confirm_popup(button_count, err_message, info_kind, swidth, sheight){

	
	if(button_count==1){
		alert(err_message);
		return true;
	}else{
		if(confirm(err_message)){
			return true;
		}else{
			return false;
		}
	}
	
	/*************************************************************************************************/
	//팝업창을 만들경우
	/*if(swidth==""){
		swidth=350;
	}
	if(sheight==""){
		sheight=210;
	}
	var value_check=window.showModalDialog('/common/alert.asp?button_count=' + button_count + '&err_message=' + err_message,'Information', 'dialogHeight:' + sheight + 'px; dialogWidth:' + swidth + 'px; edge: Raised; center: yes; help: no; resizable: no; status: no; scroll: no;');
	return value_check;*/
	/**************************************************************************************************/
}


// 필수 입력값 유무체크한후 경고창 뛰우기
//inputValCheck("폼이름", "엘리먼트이름", "항목", "문자/숫자/한글/영문/영문,숫자/이메일/전화번호(S,N,K,E,EN,EM,T)", ["길이구분(S:small, B:big, F:fix, R:range)"], [최소길이], [최대길이], "필수여부(Y/N)")
//if(!inputValCheck("form1", "id", "아이디", "EN", "R", "4", "16", "Y")){return false;}
//if(!inputValCheck("form1", "pwd", "패스워드", "EN", "R", "4", "16", "Y")){return false;}
//if(!inputValCheck("form1", "aa", "셀렉트", "S", "", "", "", "Y")){return false;}
//if(!inputValCheck("form1", "bb", "체크", "S", "", "", "", "Y")){return false;}
//if(!inputValCheck("form1", "cc", "라디오", "S", "", "", "", "Y")){return false;}
function inputValCheck(form_name,elm,msg,strORNum,len_gubun,s_len,b_len,essential){

	if(!eval("document."+ form_name +"."+ elm)){
		confirm_popup(1,elm + " 엘리먼트가 정의되지 않았습니다.", "red", "", "");
		return false;
	}
	var form = eval("document."+ form_name +"."+ elm);
	
	if(form.type=="text" || form.type=="textarea" || form.type=="password" || form.type=="file"){
	//텍스트 박스, 텍스트 영역
		var inputStr = form.value.Trim();
		if(essential=="Y"){
			if (inputStr==""){
				confirm_popup(1,msg + " 항목은 반드시 입력하셔야 됩니다.", "red", "", "");
				form.value="";
				try{
					form.focus();
				}catch(e){
				
				}
				return false;
			}
		}else{
			if (inputStr==""){
				return true;
			}
		}
		
		if(!FuncCheck(strORNum,form_name,elm,msg)){return false;}

		if (len_gubun!=""){
			if(!lenCheck(form_name, elm, msg ,len_gubun,s_len,b_len)){return false;}
		}
	}else if(form.type==undefined || form.type=="radio"){
	//라디오 버튼
		if(!form.length){
			if(form.type=="radio"){
				if(!form.checked){
					confirm_popup(1,msg+" 선택하세요!", "red", "", "");
					return false;	
				}
				return true;
			}else{
				confirm_popup(1,form.type, "red", "", "");
				return false;
			}
			
		}
		var elm_len = form.length;
		var ok = 0;
		if(elm_len<=0){
			confirm_popup(1,elm + " 엘리먼트가 정의되지 않았습니다.", "red", "", "");
			return false;
		}
		for(var i=0;i<elm_len;i++) { 
			if (form[i].checked) { 
				ok = i+1;
			}
		}

		if(ok < 1) { 	
			confirm_popup(1,msg+" 선택하세요!", "red", "", "");
			return false;
		}
	}else if(form.type=="checkbox"){
	//체크박스
		if(essential=="Y"){
			if(!form.checked){
				confirm_popup(1,msg + " 항목은 반드시 체크하셔야 됩니다.", "red", "", "");
				return false;
			}
		}
	}else if(form.type.indexOf("select") != -1){
	//셀렉트 박스
		var inputStr = form.options[form.selectedIndex].value;
		if(essential=="Y"){
			if (inputStr==""){
				confirm_popup(1,msg + " 항목은 반드시 선택하셔야 됩니다.", "red", "", "");
				form.value="";
				form.focus();
				return false;
			}
		}else{
			if (inputStr==""){
				return true;
			}
		}

		if(!FuncCheck(strORNum,form_name,elm,msg)){return false;}

		if (len_gubun!=""){
			if(!lenCheck(form_name, elm, msg ,len_gubun,s_len,b_len)){return false;}
		}
	}else{
	//그외
		confirm_popup(1, form.type, "red", "", "");
		return false;
	}
	return true;
}

//함수값체크
function FuncCheck(value,form_name,elm,msg){
	switch (value){
		case "S":
			//문자
			break;
		case "N":
			//숫자
			if(!IsNumber(form_name,elm,msg)){return false;}
			break;
		case "K":
			//한글
			if(!IsKor(form_name,elm,msg)){return false;}
			break;
		case "E":
			//영문
			if(!IsEng(form_name,elm,msg)){return false;}
			break;
		case "EN":
			//영문,숫자
			if(!IsNumEng(form_name,elm,msg)){return false;}
			break;
		case "EM":
			//이메일
			if(!IsEmail(form_name,elm,msg)){return false;}
			break;
		case "T":
			//전화번호
			if(!IsPhone(form_name,elm,msg)){return false;}
			break;
		default:
	}
	return true;
}

// 셀렉트박스 입력값 유무체크한후 경고창 뛰우기
function selectCheck(form_name,elm,msg,essential){
//사용법 selectCheck("폼이름", "엘리먼트이름", "항목", "필수여부(Y/N)")

	var objSel = eval("document."+ form_name +"."+ elm);
	inputStr = objSel.options[objSel.selectedIndex].value;

	if (essential=='N')
	{
		if (inputStr==''){return true;}
	}else{
		if (inputStr==''){
			confirm_popup(1,msg + " 항목은 반드시 선택하셔야 됩니다.", "red", "", "");
			form.value="";
			form.focus();
			return false;
		}
		return true;
	}
}

// 체크/라디오박스 체크 유무체크한후 경고창 뛰우기
function checkCheck(form_name,elm,msg,lenGubun, checkCnt){
//사용법 checkCheck("폼이름", "엘리먼트이름", "항목", "갯수 구분", "선택할갯수")
	var form = eval("document."+ form_name +"."+ elm);
	var trueCnt = 0
	for(i = 0 ; i < form.length; i++){
		if(form[i].checked==true){
			trueCnt++;		
		}
	}

	checkCnt = (checkCnt=='')? 1:checkCnt

	if(lenGubun=='F'){
		if(parseInt(checkCnt)!=trueCnt){
			if(parseInt(checkCnt)==1){
				alert(msg + '항목은 반드시 선택하셔야 합니다.');
			}else{
				alert(msg + '항목은 반드시 '+ checkCnt +'개를 선택하셔야 합니다.');
			}
			return false
		}		
	}else if(lenGubun=='S'){
		if(parseInt(checkCnt)<trueCnt){
			alert(msg + '항목은 반드시 '+ checkCnt +'개 이하를 선택하셔야 합니다.');
			return false
		}		
	}else if(lenGubun=='B'){
		if(parseInt(checkCnt)>trueCnt){
			alert(msg + '항목은 반드시 '+ checkCnt +'개 이상을 선택하셔야 합니다.');
			return false
		}		
	}
	return true;
}
//입력값이 숫자인지 체크
function IsNumber(form_name,elm,msg) {
	//사용법 IsNumber(폼이름, 엘리먼트이름, 유효하지 않을경우 메세지)
	var form = eval("document."+ form_name +"."+ elm);
	for(var i = 0; i < form.value.length; i++) {
		var chr = form.value.substr(i,1);
		if(chr < '0' || chr > '9') {            
			confirm_popup(1,msg + " 항목은 숫자로 입력해 주세요.", "red", "", "");
			form.focus();
			return false;
		}
	}
	return true;   
}


////길이체크
//function lenCheck(form_name, elm, msg ,len_gubun,s_len,b_len){
////사용법 lenCheck("폼이름", "엘리먼트이름", "항목", "길이구분(S:small, B:big, F:fix, R:range)", [최소길이], [최대길이])

//	var form = eval("document."+ form_name +"."+ elm);
//	var inputStr = form.value.Trim();
//	if (len_gubun=="S"){	//보다 작은 값
//		if (inputStr.length > parseInt(s_len)-1){
//			confirm_popup(1,msg + " 항목은 " + s_len + "자 이하입니다.", "red", "", "");
//			form.focus();
//			return false;
//		}
//	}else if (len_gubun=="B"){	//보다 큰 값
//		if (inputStr.length < parseInt(s_len)){
//			confirm_popup(1,msg + " 항목은 " + s_len + "자 이상입니다.", "red", "", "");
//			form.focus();
//			return false;
//		}
//	}else if (len_gubun=="F"){	//고정된 값
//		if (inputStr.length != parseInt(s_len)){
//			confirm_popup(1,msg + " 항목은 " + s_len + "자 입니다.", "red", "", "");
//			form.focus();
//			return false;
//		}
//	}else if (len_gubun=="R"){	//범위 값
//		if (inputStr.length < parseInt(s_len) || inputStr.length > parseInt(b_len)){
//			confirm_popup(1,msg + " 항목은 " + s_len + "자 이상 "+ b_len +"자 이하 입니다.", "red", "", "");
//			form.focus();
//			return false;
//		}
//	}
//	return true;
//}


//////////////////////////////////////
//길이체크, 수정 : 장형일 2008.12.26//
//////////////////////////////////////
function lenCheck(form_name, elm, msg ,len_gubun,s_len,b_len){
//사용법 lenCheck("폼이름", "엘리먼트이름", "항목", "길이구분(S:small, B:big, F:fix, R:range)", [최소길이], [최대길이])

	var form = eval("document."+ form_name +"."+ elm);
	var inputStr = form.value.Trim();
	if (len_gubun=="S"){	//보다 작은 값
		if (inputStr.length > parseInt(s_len)-1){
			//confirm_popup(1,msg + " 항목은 " + s_len + "자 이하입니다.", "red", "", "");
			
			//form.focus();
			event.returnValue=false;
			return;
		}
	}else if (len_gubun=="B"){	//보다 큰 값
		if (inputStr.length < parseInt(s_len)){
			//confirm_popup(1,msg + " 항목은 " + s_len + "자 이상입니다.", "red", "", "");
			//form.focus();
			
			event.returnValue=false;
			return;
		}
	}else if (len_gubun=="F"){	//고정된 값
		if (inputStr.length != parseInt(s_len)){
			//confirm_popup(1,msg + " 항목은 " + s_len + "자 입니다.", "red", "", "");
			//form.focus();
			
			event.returnValue=false;
			return;
		}
	}else if (len_gubun=="R"){	//범위 값
		if (inputStr.length < parseInt(s_len) || inputStr.length > parseInt(b_len)){
			//confirm_popup(1,msg + " 항목은 " + s_len + "자 이상 "+ b_len +"자 이하 입니다.", "red", "", "");
			//form.focus();
			
			event.returnValue=false;
			return;
		}
	}
	return true;
}




//한글만 입력
//사용법 IsKor(폼이름, 엘리먼트이름, 항목)
function IsKor(form_name,elm,msg) {
	var form = eval("document."+ form_name +"."+ elm);
	var inputStr = form.value.Trim();
	for(var i = 0; i < inputStr.length; i++) {
		 var chr = inputStr.substr(i,1);         
		 if ((chr > '0' && chr < '9') || (chr > 'a' && chr < 'z') || (chr > 'A' && chr < 'Z')) {
			confirm_popup(1,msg + " 항목은 한글로 입력해 주세요.", "red", "", "");
			form.focus();
			return false;
		 }
	}
	return true;
}


//영문 입력
//사용법 IsEng(폼이름, 엘리먼트이름, 항목)
function IsEng(form_name,elm,msg) {
	var form = eval("document."+ form_name +"."+ elm);
	var inputStr = form.value.Trim()
	for(var i = 0; i < inputStr.length; i++) {
		 var chr = form.value.substr(i,1);         
		 if ((chr < 'a' || chr > 'z') && (chr < 'A' || chr > 'Z')) {
			confirm_popup(1,msg + " 항목은 영문으로 입력해 주세요.", "red", "", "");
			form.focus();
			return false;
		 }
	}
	return true;   
}

//영문 숫자조합 입력
//사용법 IsNumEng(폼이름, 엘리먼트이름, 항목)
function IsNumEng(form_name,elm,msg) {
	var form = eval("document."+ form_name +"."+ elm);
	var inputStr = form.value.Trim()
	for(var i = 0; i < inputStr.length; i++) {
		 var chr = form.value.substr(i,1);         
		 if ((chr < '0' || chr > '9') && (chr < 'a' || chr > 'z') && (chr < 'A' || chr > 'Z')) {
			confirm_popup(1,msg + " 항목은 영문 또는 영문/숫자 조합으로 입력해 주세요.", "red", "", "");
			form.focus();
			return false;
		 }
	}
	return true;   
}


//이메일체크(도메인)
//사용법 IsEmail(폼이름, 엘리먼트이름, 항목)
function IsEmail(form_name,elm,msg) {
	var form = eval("document."+ form_name +"."+ elm);
	var inputStr = form.value.Trim();
	if (inputStr=='')
	{
		confirm_popup(1,msg+"을 입력해주세요.", "red", "", "");
		form.focus();
		return false;
	}
	emailchk = 0;
	for (var j=0; j < inputStr.length ; j++ ) {
		var ch= inputStr.substring(j,j+1);
		if (ch == "@" | ch== "." ) {
			emailchk = emailchk + 1;
		}
	}
	if (emailchk < 2 ) {
		confirm_popup(1,msg+" 주소가 유효하지 않습니다.", "red", "", "");
		form.focus();
		return false;
	}
	return true;
}

function IsEmail_1(str) {	
	emailchk = 0
	var	comp= '`~!#$%^&*()=+|\{}[];:"\'<>,?\/ ';
	for (var j=0; j < str.length ; j++ ) {
		// 공백 특수문자 검색
		if( comp.indexOf(str.substring(j,j+1))>=0) {
			return false;
			break ; 
		}
		var ch= str.substring(j,j+1);
		if (ch == "@" | ch== "." ) {
			emailchk = emailchk + 1;
		}
	}
	if (emailchk < 2 ) {
		return false;
	}
	return true;
}






/*********************************************************
용도 : 필수 입력 체크
*********************************************************/
function inputCheck(el, el_msg)
{	

	if(el.value=="")
	{
		alert(el_msg + "을(를) 입력해 주십시오.");
		el.focus();
		return false;
	}
	return true;
}



//년도입력 체크*******
//////////////////////////////////////////////////////////////////////////
function checkYear( obj )	{
var num = obj.value ;

 /* 널값허용... */
 if(obj.value == "")
  return true;

 for(i=0 ; i<num.length ; i++)  {
	if(num.charAt(i)!="0" &&
		num.charAt(i)!="1" &&
		num.charAt(i)!="2" &&
		num.charAt(i)!="3" &&
		num.charAt(i)!="4" &&
		num.charAt(i)!="5" &&
		num.charAt(i)!="6" &&
		num.charAt(i)!="7" &&
		num.charAt(i)!="8" &&
		num.charAt(i)!="9" &&
		num.charAt(i)!=" " )
	{
		alert(" 년도는 숫자로 입력하여 주십시오...   ");
		obj.value = ""
		obj.focus() ;
		return false;
	}

   if(num.length != 4) {
		alert(" 년도는 4자리로 입력하셔야 합니다\n\n ex) 2001\n   ");
		obj.value = "" ;
		obj.focus() ;
		return false;
   }
 }
    return true;
}
////////////////////////////////////////////////////////////////////////////




//월 입력 체크*******
//////////////////////////////////////////////////////////////////////////
function checkMonth( obj )	{
var num = obj.value ;

 /* 널값허용... */
 if(obj.value == "")
  return true;

 for(i=0 ; i<num.length ; i++)  {
	if(num.charAt(i)!="0" &&
		num.charAt(i)!="1" &&
		num.charAt(i)!="2" &&
		num.charAt(i)!="3" &&
		num.charAt(i)!="4" &&
		num.charAt(i)!="5" &&
		num.charAt(i)!="6" &&
		num.charAt(i)!="7" &&
		num.charAt(i)!="8" &&
		num.charAt(i)!="9" &&
		num.charAt(i)!=" " )
	{
		alert(" 월은 숫자로 입력하여 주십시오...   ");
		obj.value = ""
		obj.focus() ;
		return false;

	}

   if( num<1 || num>12 ) {
		alert("월을 입력하실 때는 1~12의 숫자만 가능합니다... ") ;
		obj.value = "" ;
		obj.focus() ;
		return false;

   }		
   
   if(num.length == 1) {
		obj.value = "0" + num ;
   }
 }
    return true;
}
//////////////////////////////////////////////////////////////////////////



//날짜입력 체크*******
//////////////////////////////////////////////////////////////////////////
function checkDay( obj )	{
var num = obj.value ;

 /* 널값허용... */
 if(obj.value == "")
  return true;

 for(i=0 ; i<num.length ; i++)  {
	if(num.charAt(i)!="0" &&
		num.charAt(i)!="1" &&
		num.charAt(i)!="2" &&
		num.charAt(i)!="3" &&
		num.charAt(i)!="4" &&
		num.charAt(i)!="5" &&
		num.charAt(i)!="6" &&
		num.charAt(i)!="7" &&
		num.charAt(i)!="8" &&
		num.charAt(i)!="9" &&
		num.charAt(i)!=" " )
	{
		alert(" 날짜는 숫자로 입력하여 주십시오...   ");
		obj.value = ""
		obj.focus() ;
		return false;
	}

   if( num<1 || num>31 ) {
		alert("날짜를 입력하실 때는 1~31의 숫자만 가능합니다... ") ;
		obj.value = "" ;
		obj.focus() ;
		return false;
   }		
   
   if(num.length == 1) {
		obj.value = "0" + num ;
   }
 }
    return true;
}
//////////////////////////////////////////////////////////////////////////






//달력 레이어
function show_calendar(obj)
{
	if(!document.all["s_calendar"]){
		var oDIV = document.createElement("DIV");
		oDIV.id = "s_calendar";
		oDIV.style.position = 'absolute';
		oDIV.style.display = 'none';
		document.body.appendChild(oDIV);

		var oIFrame = document.createElement("iframe")
		oIFrame.width = 216;
		oIFrame.height = 217;
		oIFrame.src = "/common/popCalendar2.html";
		oIFrame.style.borderBottom = "black 1px solid"
		oIFrame.style.borderLeft = "black 1px solid"
		oIFrame.style.borderRight = "black 1px solid"
		oIFrame.style.borderTop = "black 1px solid"
		oIFrame.frameBorder = "0";
		oIFrame.scrolling = "no"
		document.all["s_calendar"].appendChild(oIFrame);
	}

	if(s_calendar.style.display=="block"&&select_obj==obj){
		close_calendar();
	}else{
		/*
		w_h = document.body.offsetHeight
		w_w = document.body.offsetWidth
		e_h = window.event.clientY
		e_w = window.event.clientX
		if(w_h < (e_h+185))
		{
		s_calendar.style.pixelTop = w_h -200
		}
		else
		{
		s_calendar.style.pixelTop = e_h
		}
		if (w_w < (e_w+170))
		{
		s_calendar.style.pixelLeft = w_w - 200
		} 
		else
		{
		s_calendar.style.pixelLeft = e_w
		}
		*/
		/*레이어 위치 수정 시작(2006-11-20 김은정)*/

		var nav = (document.layers);
		var x = (nav) ? e.pageX : event.clientX+document.body.scrollLeft-10;
		var y = (nav) ? e.pageY : event.clientY+document.body.scrollTop+10;

		s_calendar.style.pixelLeft = x;
		s_calendar.style.pixelTop = y;

		if(document.body.offsetWidth < x+130)
			s_calendar.style.pixelLeft = x- (x +150 - document.body.offsetWidth);
		if(document.body.offsetHeight + document.body.scrollTop + 10 < y + 155)
			s_calendar.style.pixelTop = y - (y + 168 - (document.body.offsetHeight + document.body.scrollTop));

		/*레이어 위치 수정 끝(2006-11-20 김은정)*/

		s_calendar.style.display = "block"
		select_obj = obj
	}
}

function input_date2(y_date,m_date,d_date)
{
	m_date=(parseInt(m_date)+1).toString();
	if(m_date.length == 1)
	{
		m_date = "0" + m_date;
	}
	d_date=d_date.toString();
	if(d_date.length == 1)
	{
		d_date = "0" + d_date;
	}
	
	select_obj.value = y_date + "-" + m_date + "-" + d_date;
	s_calendar.style.display = "none";
}

function close_calendar(){
	s_calendar.style.display="none"
}


/*
function input_date(date)
{
    date = eval(date);
	select_obj.value = date;
	s_calendar.style.display = "none";
}


*/



/*********************************************************
용도 : 새창띄우기(모두 no속성)
strUrl : 새창에서 띄워질 페이지 URL
strName : 새창 이름
strLeft : 새창의 Left 이름
strTop : 새창의 Top 이름
strWidth : 새창의 Width 값
strHeight : 새창의 Height 값
strPosition : 1 - 중앙에 띄움, 0 - 지정해서 띄움
*********************************************************/
function fn_NewWinOpenPop(strUrl, strName, strLeft, strTop, strWidth, strHeight, strPosition){
	var strProperty = 'toolbar=no,menubar=no,statusbar=no,scrollbars=no,resizable=no';
		
	if (strPosition == 1){
	    strLeft = (screen.width - strWidth) / 2;
		strTop = (screen.height - strHeight) / 2;
	}
		
	window.open(strUrl, strName, 'left='+strLeft+',top='+strTop+',width='+strWidth+',height='+strHeight+','+strProperty);
}





//새창 띄우기
function new_win(filename, p_name, s_width, s_height, s_scrol)
{
	x = screen.width;
	y = screen.height;
	wid = (x / 2) - (s_width / 2);
	hei = (y / 2) - (s_height / 2);

   window.open(filename, p_name, "toolbar=0,location=0,directories=0,status=0,menubar=0,resizable=0,scrollbars=" + s_scrol + ",width=" + s_width + ",height=" + s_height + ",top=" + hei + ",left=" + wid + ",scrolbar=no"); 
}



//달력보기
function fn_viewCalendar(strCalBoxName)
{
    var obj = eval("document.all." + strCalBoxName);
    obj.value = "";
    
    document.all.calType.value=strCalBoxName;
    fn_NewWinOpenPop("/Common/popCalendar.html", "cal_win", 0, 0, 170, 260, 1);
    //window.open("/Common/popCalendar.html","cal_win","width=130,height=180");
}



function input_date(strYear, intMon, intDay)
{
    var strMon = String(intMon);
    var strDay = String(intDay);
   if(strMon.length==1)
        strMon = "0" + strMon;
   
   if(strDay.length==1)
        strDay = "0" + strDay;
   
  
    var strDate = strYear + "-" + strMon+ "-" + strDay;
    
    var calTypeObj = document.all.calType.value;
    var dateObj = eval("document.all." + calTypeObj);
    
    //document.all[document.all.calType.value].value=strDate;
    
    if(dateObj)
    {
        if(dateObj.length)
        {
            for(var i=0;i<dateObj.length;i++)
            {
                dateObj[i].value = strDate;
            }
        }
        else
        {
            dateObj.value=strDate;    
        }
    }
    
    
}


function onlyNum(code){// code값이 숫자인지를 확인한다.
   if ( (code>=48 && code<=57) || code == 13) return true ;
   else return false;
}

function onlyJumin(code){// code값이 숫자, '-' 인지를 확인한다.
   if ( (code>=48 && code<=57) || code == 45 || code == 13) return true ;
   else return false;
}

function onlyTel(code){// code값이 숫자, '-', ',' 인지를 확인한다.
   if ( (code>=48 && code<=57) || code == 45   || code == 44 || code == 13) return true ;
   else return false;
}

function onlyDot(code){// code값이 숫자, '.' 인지를 확인한다.
   if ( (code>=48 && code<=57) || code == 46 || code == 13) return true ;
   else return false;
}

function EnterCheck(chkKey){// 입력값의 형태에 따라, 입력형태 체크
	var errMsg, errValue
	if (chkKey == 'num'){
		if (onlyNum(event.keyCode) == false){
			errMsg = "\n숫자만 입력 가능합니다."
			errValue = 'true'
		}
	}else if (chkKey == 'tel'){
		if (onlyTel(event.keyCode) == false){
			errMsg = "\n숫자, \'-\', \',\'만 입력 가능합니다."
			errValue = 'true'
		}
	}else if (chkKey == 'jumin'){
		if (onlyJumin(event.keyCode) == false){
			errMsg = "\n숫자, \'-\' 만 입력 가능합니다."
			errValue = 'true'
		}
	}else if (chkKey == 'dot'){
		if (onlyDot(event.keyCode) == false){
			errMsg = "\n숫자, \'.\' 만 입력 가능합니다."
			errValue = 'true'
		}
	}

	if (errValue == 'true'){
		alert(errMsg);
		event.returnValue=false;
		return ;
	}
	
	
	event.returnValue=true;
}

function comma(objGum){
	var num = objGum.value;
	if (num.length >= 4) {
		re = /^\$|,/g;// "$" and "," 입력 제거
		num = num.replace(re, "");
		fl=""
		if(isNaN(num)){
			alert("문자는 사용할 수 없습니다.");
			return 0
		}
		if(num==0) return num
		if(num<0){
			num=num*(-1)
			fl="-"
                }else{
                	num=num*1 //처음 입력값이 0부터 시작할때 이것을 제거한다.
                }
                num = new String(num)
                temp=""
                co=3
                num_len=num.length
                while (num_len>0){
                	num_len=num_len-co
                	if(num_len<0){co=num_len+co;num_len=0}
                	temp=","+num.substr(num_len,co)+temp
                }
                objGum.value = fl+temp.substr(1);
         }
}




//iframe 사이즈 조절
function doResize(name)
{
	try
	{
		
		var oBody   = document.frames(name).document.body;
		var oIFrame = document.all(name);
		var frmWidth  = oBody.scrollWidth;
		var frmHeight = oBody.scrollHeight;

		oIFrame.style.height = frmHeight;
		oIFrame.style.width = frmWidth;
	}
	catch (e)
	{
		
	}
}




//xmlHttp 사용
function sendXmlHttp(url){

    var currentUrl = document.location.toString();
    currentUrl = currentUrl.replace("http://", "");
    var index = currentUrl.indexOf("\/");
    
    var currentDomain = currentUrl.substring(0,index);
   
    
	if(url){
		var xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
//		xmlHttp.open("get", "http://"+ currentDomain +":8077/"+ url, false);
		xmlHttp.open("get", "http://"+ currentDomain + url, false);
		xmlHttp.send();
		
		return xmlHttp.responseText;
	}else{
		alert("호출 경로가 지정해 주세요");
	}

}



//xmlHttp 호출 후 selectbox의 option 구성
//처리페이지, selectbox개체명, flag-1:전체/2:선택/3:없음, 선택될 값
function xmlHttpCreateOptions(goUrl,reSelectObjName, defaultText, selectedVal){

	var obj = eval("document.all."+reSelectObjName);
	selectInit(obj, defaultText);

	var strv = sendXmlHttp(goUrl);
//alert(strv);
	oXMLText = eval(strv);
	if(oXMLText){
		var option;
		for(i=0;i<oXMLText.length;i++){
			option = document.createElement("option")
			option.value	= oXMLText[i][0];
			option.text		= oXMLText[i][1];

			if(option.value == selectedVal)
				option.selected = true;

			obj.add(option)
//			alert(i);
		}
	}
}



function selectInit(objName, defaultText){

	if(objName.length)
	{
		for (var i=objName.length;i>=0;i--){
			objName.options[0] = null
			objName.options[0] = new Option([defaultText],[""])
		}
	}
}


//셀렉트 초기화
function selectReset(objName){

	if(objName.length)
	{
		for (var i=objName.length;i>=0;i--){
			objName.options[i] = null
		}
	}
}



//전체 선택/선택해제
function setCheckAll(getObj, chkYN)
{
    if(getObj)
    {
        if(getObj.length>0)
        {
            for(var i=0;i<getObj.length;i++)
            {
                getObj[i].checked = chkYN;
            }
        }
        else
            getObj.checked = chkYN;
    }
}


//선택된 개수 리턴
function getCheckCount(getObj)
{
    var chkCnt = 0;
    if(getObj)
    {
        if(getObj.length > 0)
        {
            for(var i=0;i<getObj.length;i++)
            {
                if(getObj[i].checked)
                    chkCnt = chkCnt + 1;
            }
        }
        else
        {
            if(getObj.checked)
                chkCnt = chkCnt + 1;
        }
    }
    
    return chkCnt;
}



//자동포커스
function autoFocus(obj, maxLength, obj2)
{
    if(eval("document.all." + obj).value.length == maxLength)
    {
        eval("document.all."+obj2).focus();
    }
}



//로그인 체크
function loginCheck()
{
    alert("세션이 종료되었습니다. 다시 로그인 해 주세요.99");
    
    
    
    //if(self.opener != null)
    //    self.close();
    
    
    top.location.href = "/Login.aspx";
}

// 쿠키 확인
function notice_getCookie( name ) 
{ 
		var nameOfCookie = name + "="; 
		var x = 0; 
		while ( x <= document.cookie.length ) 
		{ 
				var y = (x+nameOfCookie.length); 
				if ( document.cookie.substring( x, y ) == nameOfCookie ) { 
						if ( (endOfCookie=document.cookie.indexOf( ";", y )) == -1 ) 
								endOfCookie = document.cookie.length; 
						return unescape( document.cookie.substring( y, endOfCookie ) ); 
				} 
				x = document.cookie.indexOf( " ", x ) + 1; 
				if ( x == 0 ) 
						break; 
		} 
		return ""; 
} 

// 팝업창 띄우기
function openPopup(cookieName, popnum, nWidth, nHeight, nTop, nLeft, sScroll)
{
    if ( notice_getCookie( cookieName ) != "done" )
        window.open("/PopupNotice.aspx?PopNum=" + popnum, cookieName, "width=" + nWidth + ", height=" + nHeight + ",top=" + nTop + ",left=" + nLeft + ",scrollbars=" + sScroll);
}

// IE 버젼 확인
function CheckIEVersion()
{
    var rtnVal;
    
    if( navigator.appName.indexOf("Microsoft") > -1 ) // IE?
    {
        rtnVal = "ie";
        
        if( navigator.appVersion.indexOf("MSIE 6") > -1) // IE6?
        {
            rtnVal = "ie6";
        }
        else if(navigator.appVersion.indexOf("MSIE 7") > -1) // IE7?
        {
            rtnVal = "ie7";
        }
    }
    
    return rtnVal;
}