$(function(){
	
	jQuery.fn.center = function () {
		this.css("position","fixed");
		return this;
	};

	var main = $('.main-wrap');
	var mainSec = main.find('section');
	var mainBG = main.find('.main-bg');
	var sectionLen = mainSec.length;
	var sectionCnt = 0;

	$(window).on('load', function(){
		$('.main-sec1').addClass('on');
		$('body').addClass('onload');

	});

	var mainSectionMoveEvent;
	sectionInterval();
	
	//section autoplay
	function sectionInterval(status){
		clearInterval(mainSectionMoveEvent);

		if(status == 'prev'){
			sectionCountEvent(-1);
		}else if(status == 'next'){
			sectionCountEvent(1);
		}

		mainSectionMoveEvent = setInterval(function(){
			sectionCountEvent(1);
		}, 9000);
	}

	function autoPlay(){
		$('.section-count .active').text('0' + (sectionCnt == sectionLen ? sectionCnt : sectionCnt+1) );
		$('.progress .bar').css('width', (sectionCnt + 1) * 25 + '%');
	}

	//count
	function sectionCountEvent(count){
		if(sectionCnt < sectionLen){
			mainBG.removeClass('on');
			mainSec.removeClass('on');
			sectionCnt = sectionCnt + count;

			if(sectionCnt == sectionLen){
				sectionCnt = 0;
			}else if(sectionCnt < 0){
				sectionCnt = sectionLen-1;
			}
			mainBG.eq(sectionCnt).addClass('on');
			mainSec.eq(sectionCnt).addClass('on');

			autoPlay();
		}
	}
	
	$('.carousel-btn').on('click', function(){
		var carouselBtnVal = $(this).val();
		sectionInterval(carouselBtnVal);
	});

	// my 아이콘
	$('.account-wrap a').on('click', function () {
		var myList = $(this).siblings('.my-list');
		myList.toggleClass('active');
		if (myList.hasClass('active')){
			$(this).siblings('.my-list').show().stop(true).css({'opacity': 0}).animate({ opacity: '1', top: '65px'}, 450);
		} else {
			$(this).siblings('.my-list').show().stop(true).css({'opacity': 1}).animate({ opacity: '0', top: '75px'}, 450);
		}
	});

	// 좌버튼
	$('.btn-all').on('click', function () {
		$('html').toggleClass('side-lt-open');
		$('html, body').stop().animate({scrollTop: '0'}, 0);
		if ($('html').hasClass('side-lt-open')){
			$('.side-lt-bg').addClass('active');
			$('.btn-util').css('display','none');
		} else {
			$('.side-lt-bg').removeClass('active');
			$('.btn-util').css('display','block');
		}
	});

	// 우버튼
	$('.btn-util').on('click', function () {
		$('html').toggleClass('side-rt-open');
		$('html, body').stop().animate({scrollTop: '0'}, 0);
		if ($('html').hasClass('side-rt-open')){
			$('.side-rt-bg').addClass('active');
			$('.btn-all').css('display','none');
		} else {
			$('.side-rt-bg').removeClass('active');
			$('.btn-all').css('display','block');
		}
	});
	
	// gnb 색상
	$('.gnb > li').mouseover(function(){
		$(this).siblings('li').addClass('out');
	})
	$('.gnb > li').mouseout(function(){
		$(this).siblings('li').removeClass('out');
	});

	// 스크롤
	$(window).scroll(function(){
		var nowScrollTop = $(this).scrollTop();
		var headerHeight = $('header').outerHeight();
		var footerPosTop = $(document).height() - $(window).height() - $('footer').outerHeight() + 180;
		
		if(nowScrollTop > footerPosTop){
			$('.btn-top-wrap').css('bottom', (nowScrollTop - footerPosTop) + 100 +'px');
		}else{
			$('.btn-top-wrap').css('bottom', '100px');
		}

		if(nowScrollTop > headerHeight){
			$('.btn-top-wrap').addClass('visible');
		}else{
			$('.btn-top-wrap').removeClass('visible');
		}

	});

	$('.btn-top').on('click', function(){
		$('html, body').stop().animate({scrollTop: '0'}, 800);
	});

	// 탭
	$('ul.tab-menu li').click(function () {
		$('ul.tab-menu li').removeClass('active');
		$(this).addClass('active');
		$('.tab-cont > div').removeClass('active')
			var activeTab = $(this).attr('rel');
		$('#' + activeTab).addClass('active')
	})

	$('ul.select-type li').click(function () {
		$('ul.select-type li').removeClass('active');
		$(this).addClass('active');
	})

	// 체크아웃
	$('.my-product .chk-wrap label').on('click', function () {
		var chkBox = $(this).children('input');
		if(chkBox.is(":checked")){
			$('.checkout-wrap').addClass('active');
			$('footer').addClass('active');
		} else {
			$('.checkout-wrap').removeClass('active');
			$('footer').removeClass('active');
		}
	});

	// 메뉴슬라이드
	$('.slide-togg > dt, .slide-togg > .dt').on('click', function () {
		if ($(this).hasClass('on')) {
			slideUp();
		} else {
			slideUp();
			$(this).addClass('on').next().slideDown(100, 'linear');
		}
		function slideUp() {
			$('.slide-togg > dt, .slide-togg > .dt').removeClass('on').next().slideUp(100, 'linear');
		};
	});

	// slick 넘버링
	var $status = $('.page-num');
	var $slickElement = $('.count-show');

	$slickElement.on('init reInit afterChange', function (event, slick, currentSlide, nextSlide) {
	//currentSlide is undefined on init -- set it to 0 in this case (currentSlide is 0 based)
	var i = (currentSlide ? currentSlide : 0) + 1;
	$status.html('<b>' + i + '</b> /' + slick.slideCount);
	});



});

// popup
function openLayer(el) {
	var temp = $('#' + el);
	var tempInner = $('#' + el + ' .pop-wrap');
	console.log(temp);
	if(!$('.popup:visible').length) {
		$("<div/>", {
			"class": "dim-layer",
		}).appendTo('.popup').fadeIn(200);
	}
	temp.fadeIn(200).center();
	tempInner.fadeIn(200).center();
	//setModalMaxHeight(temp);

	// $('body, html, #wrap').css('overflow','hidden').css('height','100%');
	$('body').on('click touchstart', '.popup .btn-confirm, .popup .btn-cancel, .popup .pop-close, .dim-layer', function() {
		$(this).parent().parent('.popup').hide();
		temp.fadeOut(200);
		if($('.dim-layer').length) {
			$('.dim-layer').fadeOut(200, function() {
				$(this).remove();
			});
		}
		$('body, html, #wrap').css('overflow','').css('height','');
	});
}



