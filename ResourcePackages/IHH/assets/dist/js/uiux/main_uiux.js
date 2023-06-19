

jQuery(function () {

    var scrollLocation = 0;
    var scrollTopCheck = true;
    const scrollThreshold = 10;

    var windowWidth = $(window).outerWidth();
    var bslg = 992;
    var bsmd = 768;

    var bslgbigger = 1512;

    var headerheight = $('.header-default .navbar').outerHeight();
    const initialHeaderheight = $('.header-default .navbar').outerHeight();


    $(window).on('scroll', function () {
        scrollLocation = $(this).scrollTop();
        if (scrollLocation <= scrollThreshold) {
            scrollTopCheck = true;
        } else {
            scrollTopCheck = false;
        }
    });

    if ($('.header-default').hasClass('homehome')) {
        headerheight += 20;
    }
    $('.header-spacing').css('height', headerheight);

    // $(window).on('scroll', function () {
    //     if (scrollTopCheck) {
    //         $('.header-spacing').css('margin-top', 0);
    //     } else {
    //         $('.header-spacing').css('margin-top', initialHeaderheight * -0.75);
    //     }
    // })

    $('video.welcome-video').each(function () {
        $(this)[0].autoplay = true;
        $(this)[0].loop = true;
        $(this)[0].muted = true;
        // $(this)[0].play();
        // $(this)[0].attr('webkit-playsinline', 'webkit-playsinline');
        var video = $(this)[0];
        var promise = video.play();

        if (promise !== undefined) {
            promise.then(_ => {
                // Autoplay started!
            }).catch(error => {
                // Autoplay not allowed!
                // Mute video and try to play again
                video.muted = true;
                video.play();

                // Show something in the UI that the video is muted
            });
        }
    })

    // $('.header-spacing').css('background-color', $('.header-spacing').next('div').not('.footer-default').css("background-color"));
    // $('.ihh-breadcrumbs').css('background-color', $('.ihh-breadcrumbs').next('div').not('.footer-default').css("background-color"));


    // if ($('.header-spacing').css('background-color', $('.header-spacing').next('div').not('.footer-default').children('.sfContentBlock').length)) {
    //     $('.header-spacing').css('background-color', $('.header-spacing').next('div').not('.footer-default').find('.sfContentBlock').children('div').children('div').first().css('background-color'));
    // } else {
    //     $('.header-spacing').css('background-color', $('.header-spacing').next('div').not('.footer-default').css("background-color"));
    // }

    if ($('.ihh-breadcrumbs').css('background-color', $('.ihh-breadcrumbs').next('div').not('.footer-default').children('.sfContentBlock').length)) {

        $('.ihh-breadcrumbs').css('background-color', $('.ihh-breadcrumbs').next('div').not('.footer-default').find('.sfContentBlock').children('div').children('div').first().css('background-color'));
    } else {
        $('.ihh-breadcrumbs').css('background-color', $('.ihh-breadcrumbs').next('div').not('.footer-default').css("background-color"));


    }



    // $(window).on('resize', function () {
    //     console.log($('.header-default .navbar').outerHeight())
    //     $('.header-spacing').css('height', $('.header-default .navbar').outerHeight());
    // })

    //home style

    const initialLogo = $('.navbar-brand-img').attr('src');
    const normalLogo = "/ResourcePackages/IHH/assets/dist/images/header/ihhlogo.png";
    const homeLogo = "/ResourcePackages/IHH/assets/dist/images/header/ihhlogo-white.png";
    const notextLogo = '/ResourcePackages/IHH/assets/dist/images/header/ihh-logo-notext.png';
    // const homeLogo = "/assets/ihhlogowhite.png";



    if ($('.header-default').hasClass('home-check')) {
        $('.header-default').addClass('home');
        // $('body').css('background-color', '#b0a3cf');

        $('.navbar-brand-img').attr('src', homeLogo);

        $('.navbar-toggler').on('click', function () {

            if ($(this).hasClass('collapsed')) {



                $('.navbar-collapse').on('hidden.bs.collapse', function () {
                    if (scrollLocation <= scrollThreshold) {
                        $('.header-default').addClass('home');
                        $('.navbar-brand-img').attr('src', homeLogo);
                        $('.navbar').removeClass('show-shadow');
                    }
                })





            } else {

                $('.navbar-brand-img').attr('src', initialLogo);
                $('.header-default').removeClass('home');

            }
        });

        $(window).on('scroll', function () {



            if (scrollLocation <= scrollThreshold) {
                if ($('.navbar-toggler').hasClass('collapsed')) {
                    $('.header-default').addClass('home');
                    $('.navbar-brand-img').attr('src', homeLogo);
                }
                $('.navbar-brand-img').removeClass('scrolled');


            } else {
                $('.header-default').removeClass('home');
                $('.navbar-brand-img').attr('src', initialLogo);

            }
        });

    }




    // $(window).on('scroll', function(){
    //     if($('.navbar-brand-img').hasClass('scrolled')){
    //         $('.navbar-brand-img').attr('src', notextLogo);


    //     }else{
    //         if($('.header-default').hasClass('home-check')){
    //             $('.navbar-brand-img').attr('src', homeLogo);
    //         }else{
    //             $('.navbar-brand-img').attr('src', normalLogo);
    //         }

    //     }
    // })



    //home style end


    //resize extra
    $(window).on('resize', function () {
        windowWidth = $(window).outerWidth();
        // console.log(windowWidth);
        if (windowWidth >= bslg) {
            $('.navbar-toggler').addClass('collapsed');
            $('.navbar-collapse').removeClass('show');
            $('.navbar-toggler-icon').removeClass('close');
        }

        if ($('.header-default').hasClass('home-check')) {
            if (scrollTopCheck && $('.navbar-toggler').hasClass('collapsed')) {
                $('.header-default').addClass('home');
                $('.navbar-brand-img').attr('src', homeLogo);
            }

        }

        if (scrollTopCheck) {
            $('.navbar').removeClass('show-shadow');
        }


    })
    //resize extra end





    $('.navbar-toggler').on('click', function () {
        $('.navbar').addClass('show-shadow');

        if ($(this).hasClass('collapsed')) {

            $('.navbar-toggler-icon').removeClass('close');


            if (scrollLocation != 0) {
                $('.navbar-brand-img').addClass('scrolled');

            } else {
                $('.navbar').removeClass('show-shadow');
            }

        } else {
            $('.navbar-toggler-icon').addClass('close');

            $('.navbar-brand-img').removeClass('scrolled');

            $(this).find('.navbar-toggler-icon').html('')

        }
    });

    $(window).on('scroll', function () {
        scrollLocation = $(this).scrollTop();

        // console.log(scrollLocation)

        if (scrollLocation <= scrollThreshold) {

            $('.navbar-brand-img').removeClass('scrolled');
            if ($('.navbar-toggler').hasClass('collapsed')) {
                $('.navbar').removeClass('show-shadow');
            }
        } else {


            if ($('.navbar-toggler').hasClass('collapsed')) {
                $('.navbar-brand-img').addClass('scrolled');

            }

            $('.navbar').addClass('show-shadow');
            // $('.navbar-brand-img').addClass('scrolled');
        }
    });


    $('.nav-item.dropdown, .language-select-div .dropdown')
        .on('mouseenter', function () {



            if ($(window).width() >= 992) {
                $(this).children().addClass('show');
                $(this).children('.nav-link.dropdown').attr('aria-expanded', 'true');
                $(this).find('.fa.fa-chevron-down').addClass('show');
            }

        })
        .on('mouseleave', function () {



            if ($(window).width() >= 992) {
                $(this).children().removeClass('show');
                $(this).children('.nav-link.dropdown').attr('aria-expanded', 'false');
                $(this).find('.fa.fa-chevron-down').removeClass('show');
            }
        });

    var searchHover;
    $('.header-search').on('mouseenter', function () {
        searchHover = setTimeout(function(){
            $('.header-search').children('.search-divider').addClass('show');
            $('.header-search').children('.search-input').addClass('show');
        }, 150)
 

    }).on('mouseleave', function () {
 
        var search = $(".header-search").children('.search-input');

        if (search.val().length === 0 && !search.is(":focus")) {
            clearTimeout(searchHover);
            $('.header-search').children('.search-divider').removeClass('show');
            search.removeClass('show');
            search.trigger('blur');
        }
    })

    $('.header-search').children('.search-input').on('blur', function () {

        if ($('.header-search').val().length === 0) {
            clearTimeout(searchHover);
            $('.header-search .search-input').siblings('.search-divider').removeClass('show');
            $('.header-search .search-input').removeClass('show');


        }
    })

    //search check



    $('.header-search').on('click', function (event) {


        if ($(this).children('.search-input').val().length === 0) {
            event.preventDefault();

            $(this).children('.search-divider').addClass('show');
            $(this).children('.search-input').addClass('show');

            $(this).children('.search-input').trigger('focus');
        }

    });



    //search check end

    $('.language-select-div')
        .on('mouseenter', function () {
            if ($(window).width() >= 992) {
                $(this).children('.dropdown-menu').addClass('show');
                $(this).find('.dropdown').addClass('show');
                $(this).children('.nav-link.dropdown-toggle').attr('aria-expanded', 'true');
            }



            $(this).find('.fa.fa-chevron-down').addClass('show');


        })
        .on('mouseleave', function () {
            if ($(window).width() >= 992) {
                $(this).find('.dropdown-menu').removeClass('show');
                $(this).find('.dropdown').removeClass('show');
                $(this).children('.nav-link.dropdown-toggle').attr('aria-expanded', 'false');
            }



            $(this).find('.fa.fa-chevron-down').removeClass('show');
        });






    $('.gotop-button').on('click', function () {
        window.scrollTo(0, 0);

        // $('html, body').animate({
        //     scrollTop: 0,
        // }, $(document).height() / 1000 );
        $(this).addClass('active');
    })
    var lastScrollTop = 0, delta = 250;

    $(window).on('scroll', function () {


        if ($(window).width() < bsmd) {
            if (scrollTopCheck) {
                $('.gotop-button').removeClass('show');
                $('.gotop-button').removeClass('active');
                $('.sidebar-default').css('--sidebarbottom', '25px');

            }
            else {

                // - ($('.footer-default').height()) * 1.5
                if ($(window).scrollTop() + $(window).height() > $(document).height() * 0.75) {
                    if (!$('.gotop-button').hasClass('show')) {
                        $('.gotop-button').addClass('show');
                        $('.sidebar-default').css('--sidebarbottom', '105px');
                    }
                } else {
                    $('.gotop-button').removeClass('show');
                    $('.gotop-button').removeClass('active');
                    $('.sidebar-default').css('--sidebarbottom', '25px');
                }
                // else{
                //     var st = $(this).scrollTop();

                //     if (Math.abs(lastScrollTop - st) <= delta)
                //         return;

                //     if (st > lastScrollTop) {
                //         if ($('.gotop-button').hasClass('show')) {
                //             $('.gotop-button').removeClass('show');
                //             $('.gotop-button').removeClass('active');
                //             $('.sidebar-default').css('--sidebarbottom', '25px');
                //         }


                //     } else {
                //         // upscroll code
                //         if (!$('.gotop-button').hasClass('show')) {
                //             $('.gotop-button').addClass('show');
                //             $('.sidebar-default').css('--sidebarbottom', '105px');
                //         }
                //     }
                //     lastScrollTop = st;
                // }


            }
        } else {
            if (scrollTopCheck) {
                $('.gotop-button').removeClass('show');
                $('.gotop-button').removeClass('active');
                $('.sidebar-default').css('--sidebarbottom', '25px');


            } else {
                $('.gotop-button').addClass('show');
                $('.sidebar-default').css('--sidebarbottom', '105px');
            }
        }








    });


    $('.sidebar-expand-button').on('click', function () {
        if (windowWidth < bslgbigger) {
            $('.sidebar-items').toggleClass('show');

            if ($('.sidebar-items').hasClass('show')) {
                $(this).addClass('close');
            } else {
                $(this).removeClass('close');
            }
        }
    });



    if (!sessionStorage.getItem('viewed')) {
        setTimeout(() => {
            $('.sidebar-items').addClass('show');
            $('.sidebar-expand-button').addClass('close');
        }, 350);
        sessionStorage.setItem('viewed', 'yes');

    }



    setTimeout(() => {
        $('.sidebar-items').removeClass('show');
        $('.sidebar-expand-button').removeClass('close');
    }, 1850);

    $(document).on('click', function (e) {





        var target = $(e.target);

        if (windowWidth < bslgbigger) {
            if ($('.sidebar-items').hasClass('show')) {

                if (!target.closest('.sidebar-default').length) {

                    $('.sidebar-items').removeClass('show');
                    $('.sidebar-expand-button').removeClass('close');
                }
            }

        }

        if (windowWidth < bslg) {


            if ($('.navbar-collapse').hasClass('show')) {
                if (!target.closest('.navbar').length) {

                    $('.navbar-collapse').collapse('hide');
                    $('.navbar-collapse').removeClass('show');
                    $('.navbar-toggler').addClass('collapsed');
                    $('.navbar-toggler-icon').removeClass('close');

                    $('.navbar-collapse').on('hidden.bs.collapse', function () {
                        if (scrollLocation <= scrollThreshold) {
                            if ($('.header-default').hasClass('home-check')) {
                                $('.header-default').addClass('home');
                                $('.navbar-brand-img').attr('src', homeLogo);
                            }

                        }
                    })

                    if (scrollLocation <= scrollThreshold) {
                        $('.navbar').removeClass('show-shadow');

                    } else {
                        $('.navbar-brand-img').addClass('scrolled');
                    }

                }



            }







        }






    });




    //accordion-b

    $('.accordion-b-item').attr('aria-expanded', "false");


    $('.accordion-b-item.default').addClass('show');
    $('.accordion-b-item.default').attr('aria-expanded', "true");

    $('.accordion-b-item').not($('.accordion-b-item.default')).children('.description').css('display', 'none');


    var accordionbSpeed = 250;



    $('.accordion-b .accordion-b-item .header').on('click', function () {

        var p = $(this).closest('.accordion-b-item');
        // console.log(p)

        if (!p.closest('.accordion-b').hasClass('multiple')) {
            var c = p.closest('.accordion-b');
            c.children('.accordion-b-item').not($(this)).attr('aria-expanded', "false");
            c.children('.accordion-b-item').not($(this)).children('.description').slideUp(accordionbSpeed, function () {
                $(this).parent('.accordion-b-item').removeClass('show');


            })

            // p.siblings('.accordion-b-item').not($(this)).attr('aria-expanded', "false");
            // p.siblings('.accordion-b-item').not($(this)).children('.description').slideUp(accordionbSpeed, function () {
            //     $(this).parent('.accordion-b-item').removeClass('show');


            // })
        }



        var c = p.find('>.description');
        // console.log(c)

        if (!p.hasClass('show')) {
            p.addClass('show');
            p.attr('aria-expanded', "true");
            c.slideDown(accordionbSpeed);
        }
        else if (p.hasClass('show')) {
            p.attr('aria-expanded', "false");
            c.slideUp(accordionbSpeed, function () {
                p.removeClass('show');
            });
        }


    })

    //-accordion-b end



    $('.mega-side-buttons').on('click', function () {
        $(this).siblings('.mega-side-buttons').removeClass('selected');
        $(this).addClass('selected');
        const target = $(this).attr('data-mega-target');
        $(this).closest('.dropdown-mega').find('.mega-content-item').removeClass('show');
        $(this).closest('.dropdown-mega').find('.mega-content-item#' + target).addClass('show');


    })


    // resizeMega();

    // $(window).on('resize', function () {
    //     resizeMega();
    // })

    // $('.dropdown.mega-menu .dropdown').on('click', function (e) {
    //     if ($(window).width >= 992) {
    //         e.preventDefault();
    //     }
    // })



    $('.navbar-nav .dropdown-menu').on('click', function (e) {
        e.stopPropagation();
    })


    $('.navbar-nav .nav-link').on('click', function () {



        if ($(window).width() < 992) {
            $('.navbar-nav .dropdown-menu').not($(this).siblings('.dropdown-menu')).slideUp(200);
            $(this).siblings('.dropdown-menu').slideToggle(250, function () {
                $(this).toggleClass('show');
            });

            // $('.mega-mobile-item-sub').slideUp(10);
        }
    })

    // $('.navbar-nav .mega-mobile-item').on('click', function () {

    //     if ($(window).width() < 992) {
    //         var target = $(this).attr('data-mega-target');
    //         $(this).siblings('.mega-mobile-item-sub').not($('.mega-mobile-item-sub#' + target)).slideUp(200);
    //         $(this).siblings('.mega-mobile-item-sub#' + target).slideToggle(250);

    //     }
    // })

    $('.navbar-toggler-icon').on('click', function () {
        // if ($(window).width < 992) {
        $('.dropdown-menu').slideUp(200);
        // $('.mega-mobile-item-sub').slideUp(200);
        // 
    })
})



resizeMega = () => {

    var tallest = 0;

    $('.mega-list-wrapper').each(function () {
        var totalHeight = 0;
        $(this).children().each(function () {
            totalHeight += $(this).outerHeight(true);

        });
        totalHeight += parseInt($(this).css('padding-top'), 10) + parseInt($(this).css('padding-bottom'), 10);
        if (totalHeight > tallest) {
            tallest = totalHeight;
            $('.mega-content').css('height', totalHeight);
        }
        console.log(totalHeight)
    });

    console.log(tallest)






}
!function ($) {
    $(function () {
        // carousel demo
        $('#carouselExampleIndicators').carousel();
        $('#patient-support-indicators').carousel();
        $('.carousel').each(function () {
            $(this).carousel();
        })
    })
}(window.jQuery)

jQuery(() => {
    $('.mega-side-buttons[data-mega-target="mega-preferred-hospital"], .mega-mobile-item[data-mega-target="preferred-hospital"]').hide();

})


jQuery(() => {
    $('.main-container ul, .main-container ol').each(function () {
        var last = $(this).prev();


        if (last.html() != null) {
            if (last.html().includes('<strong>') || last.text().charAt(last.text().length - 1) == ":") {
                last.css('margin-bottom', '0')
            }
        }

    })
})
