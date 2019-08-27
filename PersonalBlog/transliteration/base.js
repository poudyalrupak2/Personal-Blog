function alpha(item, inpSearch) {
    var input = inpSearch;  //  document.getElementById('ContentPlaceHolder1_txtSearch');
    if (document.selection) {
        input.focus();
        range = document.selection.createRange();
        range.text = item;
        range.select();
    }
    else if (input.selectionStart || input.selectionStart == '0') {
        var startPos = input.selectionStart;
        var endPos = input.selectionEnd;
        var cursorPos = startPos;
        var scrollTop = input.scrollTop;
        var baselength = 0;
        input.value = input.value.substring(0, startPos)
  + item
  + input.value.substring(endPos, input.value.length);
        cursorPos += item.length;
        input.focus();
        input.selectionStart = cursorPos;
        input.selectionEnd = cursorPos;
        input.scrollTop = scrollTop;
    }
    else {
        input.value += item;
        input.focus();
    }
}

function copier(inpSearch) {
    //document.getElementById('ContentPlaceHolder1_txtSearch').focus();
    //document.getElementById('ContentPlaceHolder1_txtSearch').select();
    inpSearch.focus();
    inpSearch.select();
    CopiedTxt = document.selection.createRange();
    CopiedTxt.execCommand("Copy");
}

function copy(inpSearch) {
    textRange = inpSearch.createTextRange();    //  document.getElementById('ContentPlaceHolder1_txtSearch').createTextRange();
    textRange.execCommand("Copy");
    textRange = "";
}

var car;
function annuler(inpSearch) {
    car = inpSearch.value;  //  document.getElementById('ContentPlaceHolder1_txtSearch').value;
    car = car.replace(/\u200b/g, "");
    //document.getElementById('ContentPlaceHolder1_txtSearch').value = car;
    inpSearch.value = car;
}