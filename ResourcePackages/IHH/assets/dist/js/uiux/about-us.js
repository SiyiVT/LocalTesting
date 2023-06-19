$('.aboutus-awards-list').slick({
    slidesToShow: 6,
    slidesToScroll: 6,
    // swipeToSlide: true,
    infinite: true,
    arrows: true,
    autoplay: true,
    autoplaySpeed: 5000,
    // centerMode: true,


    responsive: [

        {
            breakpoint: 1400,
            settings: {
                slidesToShow: 5,
                slidesToScroll: 5,

            }
        },
        {
            breakpoint: 1200,
            settings: {
                slidesToShow: 4,
                slidesToScroll: 4,

            }
        },
        {
            breakpoint: 992,
            settings: {
                slidesToShow: 3,
                slidesToScroll: 3,


            }
        },
        {
            breakpoint: 768,
            settings: {
                slidesToShow: 3,
                slidesToScroll: 3,
                dots: true,
                // swipeToSlide: true,
                arrows: false,
            }
        },
        {
            breakpoint: 576,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 2,
                dots: true,
                // centerMode: true,
                arrows: false,
            }
        }
        
    ]

});