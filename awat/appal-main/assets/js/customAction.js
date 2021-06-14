$(document).ready(function () {});

var shareInit = {
    twitterButton: ".twt",
    kakaotalkButton: ".kk",
    facebookButton: ".fb",
    urlcopyButton: ".url",

    kakaoJavascriptID: "d43347af19d611be51bedcf2de4bbf55",
    url: "https://www.fitcloud.co.kr/AWAT",
    hashtag: "AWS용어능력고사"
}

function testStart() {
    var version = Math.floor(Math.random() * 3) + 1;
    location.href = 'awat-stage-' + version + '.html';
}

function score() {
    var score = 0;
    var checked = $('input[name^="Q"]:checked');
    for (var i = 0; i < checked.length; i++) {
        score += Number(checked[i].value);
    }

    var page_result = '';
    if (score >= 80) {
        page_result = 'awat-result-3.html';
    } else if (score >= 40) {
        page_result = 'awat-result-2.html';
    } else {
        page_result = 'awat-result-1.html';
    }

    location.href = page_result + '?score=' + score; 
}

function viewResult() {
    var score = location.href.substr(location.href.lastIndexOf('=') + 1);
    $("[name='scoreSpan']").text(score)
}