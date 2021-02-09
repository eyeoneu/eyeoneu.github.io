$(function(){
	
});



// 브라우저 사이즈에 따라 class 부여
$(document).ready(function () {
	var winWidth = 0;

   function setWidthSize(){
		winWidth = window.innerWidth;
		if(winWidth <= 1040){
			// $('html').addClass('m-ver');
            $('.ico-gnb').on('click', function () {
				$(this).toggleClass('close');
				$('nav').toggleClass('active');
				if($('nav').hasClass('active')){
					$("<div/>", {
						"class": "dimmed",
					}).appendTo('html').fadeIn(200);
				} else {
					$('.dimmed').fadeOut(200, function () {
						$(this).remove();
					});
					
				}
			});
		} else {
			// $('html').removeClass('m-ver');

		}
	}
	setWidthSize();
	$(window).resize(setWidthSize);
});
