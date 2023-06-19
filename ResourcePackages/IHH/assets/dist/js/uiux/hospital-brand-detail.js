var initialShowCount = 9;
var currentShowCount = initialShowCount;
var c = 0;
var v = '  <div class="d-flex justify-content-center"> <div class="btn-wrapper">  <div class="doctors-view-all services-item nextchunk">View more</div>  </div></div > '

var scrollLeftPrev = 0;


jQuery(() => {

    $(".general-line-href").attr("href", "tel:+6221-2789 9788");

    $('.select-specialty').select2({

        dropdownAutoWidth: true,
        dropdownPosition: 'below',
        dropdownCssClass: "select-specialty",

        minimumResultsForSearch: -1,
        placeholder: "Your Preferred Specialty",




    });


    $('.select-location').select2({

        dropdownAutoWidth: true,
        dropdownPosition: 'below',
        dropdownCssClass: "stylerr",

        minimumResultsForSearch: -1,
        placeholder: "Select Preferred Location",




    });


    $('.select-brand').select2({

        dropdownAutoWidth: true,
        dropdownPosition: 'below',
        dropdownCssClass: "stylerr",

        minimumResultsForSearch: -1,
        placeholder: "Your Preferred Hospital",




    });

    $('.select-doctor').select2({

        dropdownAutoWidth: true,
        dropdownPosition: 'below',
        dropdownCssClass: "stylerr",

        placeholder: "Your Preferred Doctor Name",

    });


    resizeDoctorDivider();

    $(window).on('resize', function () {
        resizeDoctorDivider();
    })


    if ($(window).width() < 768) {
        updateServicesCount();
        updateChunk();
    }

    $(window).on('resize', function () {

        if ($(window).width() < 768) {

            updateServicesCount();
            updateChunk();

        } else {
            resetChunk();
        }

    })
    $('.services-item.nextchunk').on('click', function () {

        currentShowCount += 9;
        updateChunk();
        resizeDoctorDivider();

        if (currentShowCount > c) {
            $(this).remove();
        }
    })

    // var slider = tns({
    //     container: '.facilities-sliders',
    //     items: 1,
    //     slideBy: '1',
    //     autoplay: true,
    //     gutter: '15',
    //     arrowKeys: false,
    //     responsive: {
    //         992: {
    //             items: 4,
    //         },
    //         768: {
    //             items: 2,
    //         }
    //     }
    // });


    $('.goto').parent().parent().parent().addClass('goto-sticky');

    checkGotoSticky();

   
    $(window).on('scroll mousewheel', function(){
        checkGotoSticky();
    })




    checkGotoScroll();

    $('.goto-buttons').on('scroll', function () {

        checkGotoScroll();
    })

    $(window).on('resize', function () {
        checkGotoScroll();
    })

    $('.goto-buttons').siblings('.right-shadow').on('click', function () {
        checkGotoScroll();
        $('.goto-buttons').animate({
            scrollLeft: $('.goto-buttons').scrollLeft() + 300
        }, 150);

    })
    $('.goto-buttons').siblings('.left-shadow').on('click', function () {
        $('.goto-buttons').animate({
            scrollLeft: $('.goto-buttons').scrollLeft() - 300
        }, 150);

    })

    $('.goto-button').each(function () {
        // var target = "";
        target = $(this).attr('data-todiv');
        // console.log(target)

        if (target != undefined) {
            checkDivScroll(target);
        }
    })




    $('.goto-button').on('click', function () {
        var c = $($(this).attr('data-todiv'));
        if ($(c).length > 0) {
            // $(this).addClass('active');
            var d = c.offset().top;
            if ($('.header-spacing').length > 0) {
                d -= $('.goto').height();
            }
            // console.log(d);

            // $('.goto-buttons').scrollLeft($(this))

            $('html, body').animate({
                scrollTop: d
            }, 50);
        }
    })




    resizeDoctorDivider();
    setInterval(function () {
        resizeDoctorDivider();
    }, 1000);
    setInterval(function () {
        resizeDoctorDivider();
    }, 2000);

})




resizeDoctorDivider = () => {
    $('.doctor-bottom-divider').each(function () {
        var h = $(this).siblings('.doctors-item-img-wrapper').height() - $(this).height() * 0.75;
        $(this).css('top', h);
    })
}

updateServicesCount = () => {
    c = $('.medical-services-items.loadchunk').children('.service').length;
    if (!($('.nextchunk').length > 0)) {
        $('.medical-services-items.loadchunk').after(v);
    }
}

// resetInitialCount = () => {
//     c 
// }

updateChunk = () => {
    $('.medical-services-items.loadchunk').children('.service').hide();
    for (let i = 0; i < currentShowCount; i++) {
        $('.medical-services-items.loadchunk').children('.service:' + "lt(" + currentShowCount + ")").show();
    }
}

resetChunk = () => {
    $('.services-item.nextchunk').remove();
    $('.medical-services-items.loadchunk').children('.service').show();
}

checkGotoSticky = () => {
    if ($('.goto').length > 0) {
        var distance = $('.doctors').offset().top;

        if ($(window).scrollTop() >= distance) {
            var navHeight = $('.navbar').outerHeight() * -1;
            // $('.navbar').css('--hidden-top', navHeight + "px");
            // $('.navbar').css('top', navHeight);
            // $('.navbar').css('display', 'none');
            $('.navbar').addClass('hidden-top');
            $('.goto').addClass('show-shadow');
            return; 
        }

        $('.navbar').removeClass('hidden-top');
        $('.goto').removeClass('show-shadow');

    }
}



checkGotoScroll = () => {


    if ($('.goto-buttons').length > 0) {
        var $elem = $('.goto-buttons');
        var newScrollLeft = $elem.scrollLeft(),
            width = $elem.width(),
            scrollWidth = $elem.get(0).scrollWidth;
        var offset = 16;


        if (scrollWidth > width) {

            $('.goto-buttons').siblings('.right-shadow, .left-shadow').addClass('show');

            if (scrollWidth - newScrollLeft <= offset + width) {
                $('.goto-buttons').siblings('.left-shadow').addClass('show');
                $('.goto-buttons').siblings('.right-shadow').removeClass('show');

            }
            if (newScrollLeft === 0) {

                $('.goto-buttons').siblings('.right-shadow').addClass('show');
                $('.goto-buttons').siblings('.left-shadow').removeClass('show');
            }
        } else {
            $('.goto-buttons').siblings('.right-shadow, .left-shadow').removeClass('show');
        }

        scrollLeftPrev = newScrollLeft;
    }
}

checkDivScroll = (target) => {
    $(window).on('scroll mousewheel', function () {

        if ($(target).length > 0) {



            if ($(window).scrollTop() >= $(target).offset().top - $('.goto-sticky').outerHeight()) {
                // $('.goto-button:not([data-todiv="' + target + '"])').removeClass('active')
                $('.goto-button[data-todiv="' + target + '"]').addClass('active');

                if ($(window).scrollTop() - $(target).outerHeight() >= $(target).offset().top - $('.goto-sticky').outerHeight()) {
                    $('.goto-button[data-todiv="' + target + '"]').removeClass('active');
                }
                if ($('.goto-button[data-todiv="' + target + '"]').hasClass('active')) {
                    if ($('.goto-button[data-todiv="' + target + '"]').position().left < 0 || $('.goto-button[data-todiv="' + target + '"]').position().left + $('.goto-button[data-todiv="' + target + '"]').width() > $('.goto-buttons').width())
                        $('.goto-buttons').animate({
                            scrollLeft: $('.goto-buttons').scrollLeft() + $('.goto-button[data-todiv="' + target + '"]').position().left - $('.goto-buttons').parent().width() * 0.25
                        }, 40)

                }
            } else {

                $('.goto-button[data-todiv="' + target + '"]').removeClass('active');
            }

        }
    })
}