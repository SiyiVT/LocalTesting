
jQuery(()=>{
    var slider = tns({
        container: '.facilities-sliders',
        items: 1,
        slideBy: '1',
        autoplay: true,
        gutter: '15',
        arrowKeys: false,
        responsive: {
            992: {
                items: 4,
            },
            768: {
                items: 2,
            }
        }
    });

    
    $('.bottom-section button[data-controls="prev"]').html('<i class="fas fa-arrow-left"></i>')
    $('.bottom-section button[data-controls="next"]').html('<i class="fas fa-arrow-right"></i>')
})