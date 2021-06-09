$(document).ready(function () {

    $(".ques-list").hide();
    $(".ques-list").eq(0).show();

    $(".ques-list input[type='radio']").change(function () {
        if ($(this).is(":checked")) {
            $(this).parents(".ques-list").children("footer").children(".btnNext").removeClass("disabled").removeAttr("disabled");
        }
    });

    $(".btnNext").click(function () {

        $(this).parents(".ques-list").hide();
        $(this).parents(".ques-list").next(".ques-list").fadeIn(750);

        $('html, body').animate({
            scrollTop: 0
        }, 350);

    });

    $("#btnSubmit").click(function () {

        if ($(this).parents(".ques-list").find("input[type=radio]:checked").length != 1) {
            alert("질문에 대해 답변해주세요.");
            return;
        }

        $(this).unbind("click");
        $(this).bind("click", function () {
            alert("검사 중입니다.");
        });

        var formJson = $("#form1").serializeFormJSON();
        $("#p").val(GetJsonToEncParam(formJson));
        $("#form2").attr("action", "/Test/TestSave");
        $("#form2").submit();
    })
});