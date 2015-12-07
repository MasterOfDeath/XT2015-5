(function () {
    var $navbar,
        $dropMenu;

    $navbar = $(".navbar");
    $dropMenu = $(".dropdown-menu", $navbar);

    $(".dropdown-toggle", $navbar).html('Awards <b class="caret"></b>');

    $(".container > .employee-panel", $navbar).addClass("hide");
    $(".container > .award-panel", $navbar).removeClass("hide");

})();