(function () {
    "use strict";
    
    var allToRightBtns,
        toRightBtns,
        toLeftBtns,
        allToLeftBtns,
        lSelect,
        rSelect,
		errAlert,
        parents,
        i = 0,
        len = 0;
    
    parents = document.body.getElementsByClassName("bControl");
    
    for (i = 0, len = parents.length; i < len; i += 1) {
        allToRightBtns = parents.item(i).getElementsByClassName("allToRightBtn").item(0);
        allToRightBtns.name = "allToRightBtn";
        allToRightBtns.onclick = moveAllItemsClick;
        toRightBtns = parents.item(i).getElementsByClassName("toRightBtn").item(0);
        toRightBtns.name = "toRightBtn";
		toRightBtns.disabled = true;
        toRightBtns.onclick = moveItemClick;
        toLeftBtns = parents.item(i).getElementsByClassName("toLeftBtn").item(0);
        toLeftBtns.name = "toLeftBtn";
		toLeftBtns.disabled = true;
        toLeftBtns.onclick = moveItemClick;
        allToLeftBtns = parents.item(i).getElementsByClassName("allToLeftBtn").item(0);
        allToLeftBtns.name = "allToLeftBtn";
        allToLeftBtns.onclick = moveAllItemsClick;
        lSelect = parents.item(i).getElementsByClassName("lSelect").item(0);
        lSelect.name = "lSelect";
        lSelect.onclick = clickSelect;
        rSelect = parents.item(i).getElementsByClassName("rSelect").item(0);
        rSelect.name = "rSelect";
        rSelect.onclick = clickSelect;
		errAlert = parents.item(i).getElementsByClassName("alert").item(0);
    }
	
    function clickSelect(){
        var lLabel,
            rLabel,
            toRightBtn,
            toLeftBtn;
		
		lLabel = getRelativeElementByClass(this, "lLabel");
		rLabel = getRelativeElementByClass(this, "rLabel");
        
        if (this.name === "lSelect") {
            lLabel.innerHTML = "Selected";
            rLabel.innerHTML = "Available";
            switchBtn(this, "toRightBtn", false);
            switchBtn(this, "toLeftBtn", true);
        } else {
            lLabel.innerHTML = "Available";
            rLabel.innerHTML = "Selected";
            switchBtn(this, "toRightBtn", true);
            switchBtn(this, "toLeftBtn", false);
        }
    }
    
    function moveItemClick() {
        var srcSel,
            destSel,
            tmp = [],
            i = 0,
            len = 0;

        if (this.name === "toRightBtn") {
            srcSel = getRelativeElementByClass(this, "lSelect");
            destSel = getRelativeElementByClass(this, "rSelect");
        }

        if (this.name === "toLeftBtn") {
            srcSel = getRelativeElementByClass(this, "rSelect");
            destSel = getRelativeElementByClass(this, "lSelect");
        }
        
        if (srcSel.selectedIndex === -1) {
			showAlert(errAlert, 2000);
			animateAlert(this.parentNode.parentNode);
            return;
        }

        for (i = 0, len = srcSel.length; i < len; i += 1) {
            if (srcSel.options[i].selected) {
                tmp.push(srcSel.options[i]);
            }
        }

        for (i = 0, len = tmp.length; i < len; i += 1) {
            destSel.options[destSel.options.length] = tmp[i];
            destSel.selectedIndex = -1;
        }
    }

    function moveAllItemsClick() {
        var srcSel,
            destSel,
            i = 0,
            len = 0;

        if (this.name === "allToRightBtn") {
            srcSel = getRelativeElementByClass(this, "lSelect");
            destSel = getRelativeElementByClass(this, "rSelect");
        }

        if (this.name === "allToLeftBtn") {
            srcSel = getRelativeElementByClass(this, "rSelect");
            destSel = getRelativeElementByClass(this, "lSelect");
        }


        if (srcSel.options.length === 0) {
			showAlert(errAlert, 2000);
			animateAlert(this.parentNode.parentNode);
            return;
        }

        for (i = 0, len = srcSel.length; i < len; i += 1) {
            destSel.options[destSel.options.length] = srcSel.options[0];
            destSel.selectedIndex = -1;
        }
        
        switchBtn(this, "toRightBtn", true);
        switchBtn(this, "toLeftBtn", true);
    }
    
    function switchBtn(element, btnClassName, disabled) {
        getRelativeElementByClass(element, btnClassName).disabled = disabled;
    }
    
    function getRelativeElementByClass(element, className) {
        return element.parentNode.parentNode.getElementsByClassName(className).item(0);
    }
	
	function showAlert(element, delay) {
		switchAlert(element, true);
		setTimeout(function() { switchAlert(element, false) }, delay);
	}
	
	function switchAlert(element, enabled) {
		var className = element.className;
		
		if (enabled) {
			element.className = className.replace(/ hide( |$)/, "");
		} else {
			if (className.search(/\bhide\b/) === -1) {
				element.className = className + " hide";
			}
		}
	}
	
	function animateAlert(element) {
		var className = element.className;

		if (className.search(/\banimated\b/) === -1) {
			element.className = className + " animated";
			setTimeout(function() { element.className = className.replace(/ animated( |$)/, "") }, 1000);
		}
	}
})();