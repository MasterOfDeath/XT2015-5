(function () {
    "use strict";
    
    var $allToRightBtns,
        toRightBtns,
        toLeftBtns,
        allToLeftBtns,
        lSelect,
        rSelect,
		$errAlert,
        $parents,
        i = 0,
        len = 0;
    
    $parents = $(".bControl");
	$(".allToRightBtn", $parents).click(moveAllItemsClick);
	$(".toRightBtn", $parents).click(moveItemClick);
	$(".toLeftBtn", $parents).click(moveItemClick);
	$(".allToLeftBtn", $parents).click(moveAllItemsClick);
	$("select", $parents).click(clickSelect);
	$errAlert = $(".alert", $parents);
	
    function clickSelect(event) {
        var $lLabel,
            $rLabel,
			$container,
			$sel;
		
		$sel = $(event.target);
		$container = $sel.closest('.bControl');
		$lLabel = $(".lLabel", $container);
		$rLabel = $(".rLabel", $container);
        
        if ($sel.hasClass("lSelect")) {
            $lLabel.text("Selected");
            $rLabel.text("Available");
            switchBtn($sel, ".toRightBtn", false);
            switchBtn($sel, ".toLeftBtn", true);
        } else {
            $lLabel.text("Available");
            $rLabel.text("Selected");
            switchBtn($sel, ".toRightBtn", true);
            switchBtn($sel, ".toLeftBtn", false);
        }
    }
    
    function moveItemClick(event) {
        var $container,
			$btn,
			$srcSel,
            $destSel,
            tmp = [],
            i = 0,
            len = 0;
		
		$btn = $(event.target);
		$container = $btn.closest(".bControl");
		

        if ($btn.hasClass("toRightBtn")) {
            $srcSel = $(".lSelect", $container);
            $destSel = $(".rSelect", $container);
        }

        if ($btn.hasClass("toLeftBtn")) {
            $srcSel = $(".rSelect", $container);
            $destSel = $(".lSelect", $container);
        }
		
        if ($srcSel.prop("selectedIndex") === -1) {
			showAlert($(".alert", $container), 2000);
			animateAlert($container);
            return;
        }
		
		tmp.push($srcSel.find(':selected'));
		$destSel.append(tmp);
    }

    function moveAllItemsClick() {
        var $container,
			$btn,
			$srcSel,
            $destSel,
            i = 0,
            len = 0;

        $btn = $(event.target);
		$container = $btn.closest(".bControl");
		

        if ($btn.hasClass("allToRightBtn")) {
            $srcSel = $(".lSelect", $container);
            $destSel = $(".rSelect", $container);
        }

        if ($btn.hasClass("allToLeftBtn")) {
            $srcSel = $(".rSelect", $container);
            $destSel = $(".lSelect", $container);
        }

        if ($srcSel.prop("options").length === 0) {
			showAlert($(".alert", $container), 2000);
			animateAlert($container);
            return;
        }

		$destSel.append($srcSel.prop('options'));
		$destSel.prop("selectedIndex", -1);
        
        switchBtn($btn, ".toRightBtn", true);
        switchBtn($btn, ".toLeftBtn", true);
    }
    
    function switchBtn($element, className, disabled) {
		var $container;
		
		$container = $element.closest(".bControl");
		$(className, $container).prop("disabled", disabled);
    }
	
	function showAlert($element, delay) {
		$element.toggleClass("hide", false);
		setTimeout(function () { $element.addClass("hide"); }, delay);
	}
	
	function animateAlert($element) {
		$element.toggleClass("animated", true);
		setTimeout(function () { $element.removeClass("animated"); }, 1000);
	}
})();