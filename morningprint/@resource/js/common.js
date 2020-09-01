$(function(){
	
	jQuery.fn.center = function () {
		this.css("position","fixed");
		return this;
	};

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


	// 스크롤
	$(window).scroll(function(){
		var nowScrollTop = $(this).scrollTop();
		var headerHeight = $('header').outerHeight();
		var footerPosTop = $(document).height() - $(window).height() - $('footer').outerHeight() + 180;
		var btnCartHeight = $('header').outerHeight() + $('.main-wrap').outerHeight()
		
		if(nowScrollTop > footerPosTop){
			$('.btn-top-wrap').css('bottom', (nowScrollTop - footerPosTop) + 100 +'px');
		}else{
			$('.btn-top-wrap').css('bottom', '100px');
		}

		if(nowScrollTop > headerHeight){
			$('.btn-top-wrap').addClass('visible');
			$('.checkout-wrap').addClass('fix');
			$('.item.detail footer').addClass('active');
		}else{
			$('.btn-top-wrap').removeClass('visible');
			$('.checkout-wrap').removeClass('fix');
			$('.item.detail footer').removeClass('active');
		}

		if(nowScrollTop > btnCartHeight){
			$('.btn-cart-wrap').addClass('fix');
		}else{
			$('.btn-cart-wrap').removeClass('fix');
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
	// $('.my-product .chk-wrap label').on('click', function () {
	// 	var chkBox = $(this).children('input');
	// 	if(chkBox.is(":checked")){
	// 		$('.checkout-wrap').addClass('active');
	// 		$('footer').addClass('active');
	// 	} else {
	// 		$('.checkout-wrap').removeClass('active');
	// 		$('footer').removeClass('active');
	// 	}
	// });

	// 메뉴슬라이드
	$('.slide-togg > dt, .slide-togg > .dt').on('click', function () {
		if ($(this).hasClass('on')) {
			slideUp();
		} else {
			slideUp();
			$(this).addClass('on').next().slideDown();
		}
		function slideUp() {
			$('.slide-togg > dt, .slide-togg > .dt').removeClass('on').next().slideUp();
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

	// toggle
	$('.ico-ques').on('click', function () {
		$(this).siblings('.ques-txt').toggleClass('active');
	});

	// input file
	var fileTarget = $('.file-box .upload-hidden');
	fileTarget.on('change', function(){
		if(window.FileReader){
			var filename = $(this)[0].files[0].name;
		} else { 
			var filename = $(this).val().split('/').pop().split('\\').pop(); 
		} 
		$(this).siblings('.upload-name').val(filename);
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

// 브라우저 사이즈에 따라 class 부여
$(document).ready(function () {
	var winWidth = 0;

   function setWidthSize(){
		winWidth = window.innerWidth;
		if(winWidth <= 960){
			$('.ques-box').addClass('togg-box');
			$('.ques-box').removeClass('hover-box');

			$('.gnb > li').on('click', function () {
				if ($(this).hasClass('active')) {
					slideUp();
				} else {
					slideUp();
					$(this).addClass('active').children('.depth02').slideDown();
				}
				function slideUp() {
					$('.gnb > li').removeClass('active').children('.depth02').slideUp();
				};
			});
		} else {
			$('.ques-box').addClass('hover-box');
			$('.ques-box').removeClass('togg-box');

			$('.gnb > li').mouseover(function(){
				$(this).addClass('active');
				$(this).siblings('li').addClass('out');
			})
			$('.gnb > li').mouseout(function(){
				$(this).removeClass('active');
				$(this).siblings('li').removeClass('out');
			});
		}
	}
	setWidthSize();
	$(window).resize(setWidthSize);
});


