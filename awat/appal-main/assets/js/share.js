
var Share = {
    defaultOptions: {
        twitterButton: "",
        kakaotalkButton: "",
        facebookButton: "",
        urlcopyButton: "",

        kakaoJavascriptID: "",
        url: "",
        hashtag: ""
    },
    init: function (options) {
        var _self = this;
        _self.defaultOptions = $.extend({}, _self.defaultOptions, options !== null ? options : {});
        Kakao.init(_self.defaultOptions.kakaoJavascriptID);

        if (_self.defaultOptions.twitterButton !== "") {
            $(_self.defaultOptions.twitterButton).on("click", function () { _self.twitter(); });
        }
        if (_self.defaultOptions.kakaotalkButton !== "") {
            $(_self.defaultOptions.kakaotalkButton).on("click", function () { _self.kakaotalk(); });  
        }
        if (_self.defaultOptions.facebookButton !== "") {
            $(_self.defaultOptions.facebookButton).on("click", function () { _self.facebook(); });
        }
        if (_self.defaultOptions.urlcopyButton !== "") {
            $(_self.defaultOptions.urlcopyButton).on("click", function (e) {
                _self.urlcopy(this);
                e.preventDefault();
            }); 
        }

    },
    twitter: function () {
        var uri = "https://twitter.com/intent/tweet?hashtags=" + encodeURIComponent(this.defaultOptions.hashtag) + "&url=" + this.defaultOptions.url;
        window.open(uri, "_blank", "toolbar=yes, scrollbars=yes,status=no, resizable=yes,left=500, width=600, height=400");
    },
    facebook: function () {
        var uri = 'https://www.facebook.com/sharer/sharer.php?u=' + this.defaultOptions.url;
        window.open(uri, "_blank", "toolbar=yes, scrollbars=yes,status=no, resizable=yes,left=500, width=600, height=400");
    },
    kakaotalk: function () {
        try {
            var url = this.defaultOptions.url;

            Kakao.Link.sendScrap({
                requestUrl: url
            });

        }
        catch (e) {
            alert(e.message);
        }
    },
    urlcopy: function (obj) {
        var url = this.defaultOptions.url;
        if (url) {
            Clipboard && Clipboard.copy(obj, url);
            alert("URL 복사가 완료 되었습니다.");
        } else {
            window.open(this.defaultOptions.url);
        }
    },
    is_ie: function(){
        if (navigator.userAgent.toLowerCase().indexOf("chrome") !== -1) return false;
        if (navigator.userAgent.toLowerCase().indexOf("msie") !== -1) return true;
        if (navigator.userAgent.toLowerCase().indexOf("windows nt") !== -1) return true;
        return false;
    }
};




//////////////////////////////
///////Clipboard
//////////////////////////////
; (function (win, doc, callback) { 'use strict'; callback = callback || function () { }; function detach() { if (doc.addEventListener) { doc.removeEventListener('DOMContentLoaded', completed); } else { doc.detachEvent('onreadystatechange', completed); } } function completed() { if (doc.addEventListener || event.type === 'load' || doc.readyState === 'complete') { detach(); callback(window, window.jQuery); } } function init() { if (doc.addEventListener) { doc.addEventListener('DOMContentLoaded', completed); } else { doc.attachEvent('onreadystatechange', completed); } } init(); })(window, document, function (win, $) {

    window.Clipboard = (function (window, document, navigator) {
        var textArea,
            copy,
            prevScrollTop;

        function isOS() {
            return navigator.userAgent.match(/ipad|iphone/i);
        }

        function createTextArea(el, text) {
            textArea = document.createElement('textArea');
            textArea.style.width = "0px";
            textArea.style.height = "0px";
            textArea.value = text;

            var agent = navigator.userAgent.toLowerCase();
            el.parentNode.appendChild(textArea);
        }

        function selectText() {
            var range,
                selection;

            if (isOS()) {
                range = document.createRange();
                range.selectNodeContents(textArea);
                selection = window.getSelection();
                selection.removeAllRanges();
                selection.addRange(range);
                textArea.focus();
                textArea.setSelectionRange(0, 999999);
            } else {
                textArea.select();
            }
        }

        function copyToClipboard(el) {
            document.execCommand('copy');
            $(textArea).closest('.event-desc').focus();
            el.parentNode.removeChild(textArea);
        }

        copy = function (el, text) {
            createTextArea(el, text);
            selectText();
            copyToClipboard(el);
        };

        return {
            copy: copy
        };
    })(window, document, navigator);

});