function moveItemClick(element) {
    "use strict";  
    
    var srcSel,
        destSel,
        tmp = [],
        i = 0,
        len = 0;

    if (element.name == "toRightBtn") {
        srcSel = element.form.lSelect;
        destSel = element.form.rSelect;
    }  
    
    if (element.name == "toLeftBtn") {
        srcSel = element.form.rSelect;
        destSel = element.form.lSelect;
    }  
    
    if (srcSel.selectedIndex === -1) {
        alert("The item should be selected.");
        return;
    }

    for (i = 0, len = srcSel.length; i < len; i++) {
        if (srcSel.options[i].selected) {
            tmp.push(srcSel.options[i]);
        }
    }
    
    for (i = 0, len = tmp.length; i < len; i++) {
        destSel.options[destSel.options.length] = tmp[i];
        destSel.selectedIndex = -1;
    }
}

function moveAllItemsClick(element) {
    "use strict";
    var srcSel,
        destSel,
        i = 0,
        len = 0;
    
    if (element.name == "allToRightBtn") {
        srcSel = element.form.lSelect;
        destSel = element.form.rSelect;
    }  
    
    if (element.name == "allToLeftBtn") {
        srcSel = element.form.rSelect;
        destSel = element.form.lSelect;
    }
    
    
    if (srcSel.options.length === 0) {
        alert("Nothing to move.");
        return;
    }
    
    for (i = 0, len = srcSel.length; i < len; i++) {
        destSel.options[destSel.options.length] = srcSel.options[0];
        destSel.selectedIndex = -1;
    }
}

function optionSelect(el) {
    var form = el.form.toRightBtn.disabled = false;
    
//    var button = document.getElementById("bb");
//    button.disabled = false;
}