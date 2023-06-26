jQuery(() => {

    normalizeCarousel();

    $('.background-top').css('height', ($('.header-spacing').height() + $('.welcome-section').height() + 50));



    $(window).on('resize', () => {

        normalizeCarousel();
        $('.background-top').css('height', ($('.header-spacing').height() + $('.welcome-section').height() + 50));


    })




    // $('.selectt-wrapper').on('click', function () {

    //     if ($(this).hasClass('collapsed')) {
    //         $(this).removeClass('collapsed');
    //     }

    // })

    $('.location-tab .location-selectors-item').on('click', function () {
        $(this).siblings('.location-selectors-item').each(function () {
            $(this).removeClass('active');
        })
        $(this).toggleClass('active');


        var contentElement = $(".location-tab");
        var offset = $('.header-spacing').outerHeight() + 45;
        var targetScrollTop = contentElement.offset().top - offset;

        $("html, body").animate({ scrollTop: targetScrollTop }, 50);


        setTimeout(() => {
            normalizeCarousel();
        }, 100);


    })

    $('.selectt-wrapper .location-selectors-item').on('click', function () {

        if ($('.selectt-wrapper').hasClass('collapsed')) {
            $('.selectt-wrapper').removeClass('collapsed');
        }
        else {
            $(this).attr('data-order', '0');
            $(this).css('order', '0');

            $('.selectt-wrapper').attr('data-selected', $(this).attr('data-order-start'));

            $(this).siblings().each(function () {
                if ($(this).attr('data-order-start') == "0") {
                    $(this).attr('data-order', "1");
                    $(this).css('order', "1");
                } else {

                    $(this).attr('data-order', $(this).attr('data-order-start'));
                    $(this).css('order', $(this).attr('data-order-start'));
                }

            })

            if ($(this).attr('data-order-start') == "5") {

                $(this).siblings('[data-order-start="4"]').attr('data-order', "5");
                $(this).siblings('[data-order-start="4"]').css('order', "5");
            }
            $('.selectt-wrapper').addClass('collapsed');
        }



    })

    $('.location-selectors').click(function () {
        $('.show-tab').css('border-right', '1px solid lightgray');
        $('.show-tab:visible:last').css('border-right', 'none');
    })

    $('#home-select-location').select2({
        dropdownAutoWidth: "true",
        dropdownPosition: "below",
        dropdownCssClass: ["styler", "home-select-location"],
        placeholder: "Search by location or hospital",
        width: "style"
    });

    $('select').on('select2:open', function () {
        setTimeout(function () {
            $('.select2-search__field').blur();
        }, 1)

    });


    $('#home-select-location').on('change', function () {

        var selected = $(this).val();

        var goto;
        switch (selected) {
            case 'gmh': {
                goto = '/gleneagles/medini-johor'
                break;
            }
            case 'gkl': {
                goto = '/gleneagles/kuala-lumpur'
                break;
            }
            case 'gkk': {
                goto = '/gleneagles/kota-kinabalu'
                break;
            }
            case 'gpg': {
                goto = '/gleneagles/kota-kinabalu'
                break;
            }
            case 'phkl': {
                goto = '/pantai/kuala-lumpur'
                break;
            }
            case 'phak': {
                goto = '/pantai/ayer-keroh'
                break;
            }
            case 'php': {
                goto = '/pantai/penang'
                break;
            }
            case 'pcmc': {
                goto = '/princecourt-medicalcentre'
                break;
            }
            default: {
                goto = null;
                break;
            }
        }

        if (goto != null) {
            goToPage(goto);
        }
    })
})

goToPage = (link) => {
    window.location.href = link;
}


normalizeCarousel = () => {
    $('.carousel-inner').each(function () {
        var tallest = 0;
        $(this).children('.carousel-item').each(function () {
            if ($(this).height() > tallest) {
                tallest = $(this).height();

            }
        })
        $(this).css('height', tallest + 5);
    })
}

checkVisible = (check) => {
    // Get the content element you want to check visibility for
    var contentElement = check;

    // Define the offset for the visible viewport
    var offset = 5; // Adjust this value as per your needs

    // Get the dimensions of the viewport including the offset
    var viewportHeight = $(window).height() - offset;
    var viewportWidth = $(window).width();

    // Get the offset position of the content element
    var elementOffset = contentElement.offset();
    var elementTop = elementOffset.top;
    var elementBottom = elementTop + contentElement.outerHeight();
    var elementLeft = elementOffset.left;
    var elementRight = elementLeft + contentElement.outerWidth();

    // Check if the content element is fully or partially within the viewport
    var isVisible = (elementTop < viewportHeight) && (elementBottom >= 0) && (elementLeft < viewportWidth) && (elementRight >= 0);

    if (isVisible) {
        console.log("The content element is visible on the screen with the offset.");
    } else {
        console.log("The content element is not visible on the screen with the offset.");
    }

}