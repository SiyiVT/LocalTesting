$(document).ready(() => {
    resizeFooterMargin();

    $(window).on('resize', function(){
        resizeFooterMargin();
    })

});

resizeFooterMargin = () => {
    if (window.innerWidth < 768) {
        $(".gotop-default .gotop-button").css("margin-bottom", `${$(".mobile-sidebar").outerHeight() + 15}px`);
        $(".footer-default").css("margin-bottom", `${$(".mobile-sidebar").outerHeight()}px`);
        return;
    }
    $(".gotop-default .gotop-button").css("margin-bottom", "");
    $(".footer-default").css("margin-bottom", "");
}