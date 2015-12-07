(function () {
    var $navbar,
        $dropMenu;

    $navbar = $(".navbar");
    $dropMenu = $(".dropdown-menu", $navbar);

    $(".dropdown-toggle", $navbar).html('Employees <b class="caret"></b>');

    $(".container > .award-panel", $navbar).addClass("hide");
    $(".container > .employee-panel", $navbar).removeClass("hide");

})();