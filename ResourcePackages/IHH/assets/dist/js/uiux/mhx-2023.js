$(() => {
    $(".hospital-highlight-slick").slick({
        dots: false,
        infinite: false,
        arrows: true,
        speed: 300,
        initialSlide: 0,
        slidesToShow: 3,
        slidesToScroll: 1,
        prevArrow: '<button type="button" class="slick-prev square-button primary"><span class="g-ic-angle-left"></span></button>',
        nextArrow: '<button type="button" class="slick-next square-button primary"><span class="g-ic-angle-right"></span></button>',
        responsive: [
            {
                breakpoint: 1200,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 1
                }
            },
            {
                breakpoint: 576,
                settings: {
                    arrows: false,
                    dots: true,
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
            }
        ]
    });

    $(".hospital-highlight-slick .hospital-item").click(function() {
        $(".hospital-details-slick .hosptial-details-item").hide();
        $(`.hospital-details-slick .hosptial-details-item[data-hos="${$(this).data("hos")}"]`).fadeIn();
    });

    $(".hospital-highlight-slick .hospital-item[data-hos='1']").click();

    // $(".hospital-details-slick").slick({
    //     dots: true,
    //     infinite: false,
    //     arrows: true,
    //     speed: 300,
    //     initialSlide: 0,
    //     slidesToShow: 1,
    //     slidesToScroll: 1,
    //     prevArrow: '<button type="button" class="slick-prev square-button primary"><span class="g-ic-angle-left"></span></button>',
    //     nextArrow: '<button type="button" class="slick-next square-button primary"><span class="g-ic-angle-right"></span></button>',
    //     responsive: [
    //         {
    //             breakpoint: 576,
    //             settings: {
    //                 arrows: false
    //             }
    //         }
    //     ]
    // });

    if (window.innerWidth > 768) {
        $(".eh-item-wrapper .eh-item").click(function() {
            if ($(this).hasClass("selected")) return;
    
            $(".eh-item-wrapper .eh-item.selected").removeClass("selected");
            $(this).addClass("selected");
            $(".eh-detils-item-wrapper .eh-details-item").hide();
            $(".eh-detils-item-wrapper .eh-details-item").eq($(".eh-item-wrapper .eh-item").index(this)).fadeIn();
        });
    
        $(".eh-item-wrapper .eh-item").eq(0).click();
    }
    else {
        $(".dropdown.select .dropdown-menu .dropdown-item").click(function() {
            console.log($(this).closest(".dropdown.select"));
            $(this).closest(".dropdown.select").find(".dropdown-toggle").html($(this).html());
            $(".eh-detils-item-wrapper .eh-details-item").hide();
            $(`.eh-detils-item-wrapper .eh-details-item[data-ehtab="${$(this).data('ehtab')}"]`).fadeIn();
        });

        $(".eh-item-dropdown .dropdown-menu li").first().find(".dropdown-item").click();
    }

    // gallery
    var imgToShow = 8;
    var galleryTemplate = "";
    if (window.innerWidth >= 768 && window.innerWidth < 1200) {
        imgToShow = 6;
    }
    else if (window.innerWidth < 768) {
        imgToShow = 4;
    }

    $galleryImg = $(".gallery-images-slick img");

    for (let i = 0; i < 37; i++) {
        if (i % imgToShow == 0) {
            galleryTemplate += `<div class="gallery-images">`;
        }

        galleryTemplate += `<img src="/ResourcePackages/IHH/assets/dist/images/mhx-2023/galley-images/gallery${i + 1}.webp">`;

        if (i % imgToShow == imgToShow - 1) {
            galleryTemplate += `</div>`;
        }
    }

    $(".gallery-images-slick").html(galleryTemplate);

    $(".gallery-images-slick").slick({
        dots: true,
        infinite: false,
        arrows: true,
        speed: 300,
        initialSlide: 0,
        slidesToShow: 1,
        slidesToScroll: 1,
        prevArrow: '<button type="button" class="slick-prev square-button primary"><span class="g-ic-angle-left"></span></button>',
        nextArrow: '<button type="button" class="slick-next square-button primary"><span class="g-ic-angle-right"></span></button>',
        responsive: [
            {
                breakpoint: 768,
                settings: {
                    arrows: false
                }
            }
        ]
    });

    // hpspital section hover
    // $(".hospital-lists .hospital-item.hoverable").mouseenter(function() {
    //     $(this).find(".hospital-img").css("transform", "rotateY(180deg)");
    //     $(this).find(".img-back").css("opacity", "1");
    //     $(this).find(".img-back").css("transition-delay", "0.15s")
    //     $(this).find(".img-back").css("transform", "rotateY(0deg)");
    // });

    // $(".hospital-lists .hospital-item.hoverable").mouseleave(function() {
    //     $(this).find(".hospital-img").css("transform", "rotateY(0deg)");
    //     $(this).find(".img-back").css("opacity", "0");
    //     $(this).find(".img-back").css("transition-delay", "0s")
    //     $(this).find(".img-back").css("transform", "rotateY(180deg)");
    // });
});

// handles idle time
// var idleTime = 0;
// var altIdleTime = 0;
// $(document).ready(function () {
//     // Increment the idle time counter every minute.
//     if (window.innerWidth > 768) {
//         var idleInterval = setInterval(timerIncrement, 2500);

//         // Zero the idle timer on mouse movement.
//         $(this).mousemove(function (e) {
//             idleTime = 0;
//         });
//         $(this).keypress(function (e) {
//             idleTime = 0;
//         });
//         $(this).scroll(function (e) {
//             idleTime = 0;
//         });
//     }
//     else {
        
//     }
// });

// $(window).on('scroll', function() {
//     document.querySelectorAll(".hospital-lists .hospital-item.hoverable").forEach(function(el, index) {
//         if (isElementXPercentInViewport(el, 100)) {
//             $(".hospital-lists .hospital-item.hoverable").eq(index).mouseenter();
//         }
//         else {
//             $(".hospital-lists .hospital-item.hoverable").eq(index).mouseleave();
//         }
//     });
// });

// function timerIncrement() {
//     idleTime = idleTime + 1;
//     if (idleTime >= 2) { 
//         $(".hospital-lists .hospital-item.hoverable").mouseleave();
//         $(".hospital-lists .hospital-item.hoverable").eq((idleTime - 2) % 5).mouseenter();
//     }
// }

// const isElementXPercentInViewport = function(el, percentVisible) {
//     let
//       rect = el.getBoundingClientRect(),
//       windowHeight = (window.innerHeight || document.documentElement.clientHeight);
  
//     return !(
//       Math.floor(100 - (((rect.top >= 0 ? 0 : rect.top) / +-rect.height) * 100)) < percentVisible ||
//       Math.floor(100 - ((rect.bottom - windowHeight) / rect.height) * 100) < percentVisible
//     )
// };
