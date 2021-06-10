$(document).ready(function () {

    // 체크문항
    $(".ques-list").hide();
    $(".ques-list").eq(0).show();

    $(".ques-list input[type='radio']").change(function () {
        if ($(this).is(":checked")) {
            $(this).parents(".ques-list").children("footer").children(".btnNext, #btnSubmit").removeClass("disabled").removeAttr("disabled");
        }
    });

    $(".btnNext").click(function () {
        $(this).parents(".ques-list").hide();
        $(this).parents(".ques-list").next(".ques-list").fadeIn(750);
        $('html, body').animate({
            scrollTop: 0
        }, 350);
    });

});

// layer
function openLayer(el) {
    var temp = $('#' + el);
    if(!$('.pop-layer:visible').length) {
        $("<div/>", {
            "class": "dimmed",
        }).appendTo('body').fadeIn(200);
    }
    temp.fadeIn(200);
    $('html').addClass('full');

    $('.dimmed, .pop-close').on('click', function() {
        temp.fadeOut(200);
        if($('.dimmed').length) {
            $('.dimmed').fadeOut(200, function() {
                $(this).remove();
            });
        }
        $('html').removeClass('full');
    });
}