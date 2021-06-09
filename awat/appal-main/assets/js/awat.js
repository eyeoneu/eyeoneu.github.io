$(document).ready(function () {

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