function test(msg) {
    alert(msg);
}

(function () {
    var $navbar,
        $dropMenu;

    $navbar = $(".navbar");
    $dropMenu = $(".dropdown-menu", $navbar);

    $("li a", $dropMenu).click(clickHeaderMenuItem);

    function clickHeaderMenuItem(event) {
        var $thisItem,
            selText;

        $thisItem = $(event.target);
        selText = $thisItem.text();

        $thisItem.parents(".dropdown").find(".dropdown-toggle").html(selText + ' <b class="caret"></b>');

        if ($thisItem.data("id") === 0) {
            $(".container > .employee-panel").removeClass("hide");
            $(".container > .award-panel").addClass("hide");
        }

        if ($thisItem.data("id") === 1) {
            $(".container > .employee-panel").addClass("hide");
            $(".container > .award-panel").removeClass("hide");
        }
    }

})();