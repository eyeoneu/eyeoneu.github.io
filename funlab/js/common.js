$(document).ready(function() {
	mobileNav();
});

function mobileNav(){
	
	$('.ico-gnb').click(function(){
		if($('.ico-gnb').hasClass('close')==true){
			$('nav').removeClass('active');
			$('.dimmed').fadeOut(200, function () {
				$(this).remove();
			});
		}else{   
			$('nav').addClass('active');
			$("<div/>", {
				"class": "dimmed",
			}).appendTo('html').fadeIn(200);
		}
		$('.ico-gnb').toggleClass('close');
	});


};

