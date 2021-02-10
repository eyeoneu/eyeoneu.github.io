// progress list
function progressList() {
	var table = $(".markup-list");
	var myUrl = $(location).attr('pathname');
	var myPath = myUrl.substring(0, myUrl.lastIndexOf('/'));
	var myProject = myPath.replace("/@floworks","");

	//console.log(myUrl);
	//console.log(myPath);
	//console.log(myProject);

	// line number
	table.find("tr td:first-child").each(function(i, v) {
		$(v).text(i + 1);
	});

	var stateFields = table.find("td.state");

	stateFields.each(function(i, v) {
		var current = $(v);
		var fields = current.parent('tr');

		var prop = {
			//type: current.is(".validation") && "http://validator.w3.org/check?uri=" + myProject || "..",
			directory: fields.find('.path').text(),
			pageId: fields.find('.pageid').text()
		};

		var wrapAnchor = $("<a>")
			.attr("target", "_blank")
			.attr("href", myProject +"/"+ prop.directory + "/" + prop.pageId + ".html")
			.text(v.textContent);

		if (current.text() == "미정") {
			current.addClass("undecided");
		} else if (current.text() == "진행") {
			current.addClass("working");
		} else if (current.text() == "완료") {
			current.addClass("complete");
		} else if (current.text() == "예정") {
			current.addClass("before");
		} else if (current.text() == "삭제") {
			current.addClass("del");
		} else if (current.text() == "검증") {
			current.addClass("validation");
		} else if (current.text() == "수정요청") {
			current.addClass("apply");
		} else {
			current.addClass("modify");
		}
		current.html(wrapAnchor);
	});
}

// call function
jQuery(function() {
	progressList();
});
