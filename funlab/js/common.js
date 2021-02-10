
$(document).ready(function () {
	var winWidth = 0;

   function setWidthSize(){
		winWidth = window.innerWidth;
		if(winWidth <= 1040){
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

		}
	}
	setWidthSize();
	$(window).resize(setWidthSize);
});
