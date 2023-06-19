var initialShowCount = 12;
var currentShowCount = initialShowCount;
var c = 0;
var v = '  <div class="d-flex justify-content-center"> <div class="btn-wrapper">  <div class="doctors-view-all nextchunk">View more</div>  </div></div > '


jQuery(() => {
    resizeDoctorDivider();

    $('.selects-list').hide();
    $('.doctors-items').hide();

    updateDoctorCount();
    updateChunk();

    $('.selects-list').show();
    $('.doctors-items').show();

    $('.nextchunk').on('click', function () {
        currentShowCount += 12;
        // updateDoctorCount();
        updateChunk();
        resizeDoctorDivider();

        // if (currentShowCount >= c) {
        //     $(this).hide();
        // }else{
        //     $(this).show();
        // }
        
    })

    // var highesttd = 0;
    // $('.opd-table tbody tr').each(function () {
    //     if ($(this).children('td').length > highesttd) {
    //         highesttd = $(this).children('td').length;
    //     }
    // })

    // $('.opd-table tbody tr').each(function () {
    //     var noToAdd = highesttd - $(this).children('td').length;

    //     p = 0;
    //     while (p < noToAdd) {
    //         $(this).children('td:last-child').after("<td></td>");

    //         p++;
    //     }
    // })
    $('.opd-table').each(function(){
        updateTableLength($(this))
    })
    $(window).on('resize', function () {
        resizeDoctorDivider();
    })

    resizeDoctorDivider();

    setTimeout(()=>{
        resizeDoctorDivider();
    }, 1500);
    // setInterval(function () {
       
    setTimeout(()=>{
        resizeDoctorDivider();
    }, 3000);
    // }, 1000);


})

updateTableLength = (target) => {
    var highesttd = 0;
    target.find('tbody tr').each(function () {
        if ($(this).children('td').length > highesttd) {
            highesttd = $(this).children('td').length;
        }
    })

    target.find('tbody tr').each(function () {
        var noToAdd = highesttd - $(this).children('td').length;

        p = 0;
        while (p < noToAdd) {
            $(this).children('td:last-child').after("<td></td>");

            p++;
        }
    })
}

updateDoctorCount = () => {
    c = $('.doctors-viewchunk').find('.doctors-items').children('.doctors-item').length;
    if (!($('.nextchunk').length > 0)) {
        $('.doctors-viewchunk').find('.doctors-items').after(v);
    }

    
    $('.nextchunk').show();
   if (currentShowCount >= c) {
        $('.nextchunk').hide();
    }

   



}

updateChunk = () => {
    $('.doctors-viewchunk').find('.doctors-items').children('.doctors-item').hide();
    for (let i = 0; i < currentShowCount; i++) {
        $('.doctors-viewchunk').find('.doctors-items').children('.doctors-item:' + "lt(" + currentShowCount + ")").show();
    }

 

}

resetShowCount = () => {
    currentShowCount = initialShowCount;
}

resizeDoctorDivider = () => {
    $('.doctor-bottom-divider').each(function () {
        var h = $(this).siblings('.doctors-item-img-wrapper').height() - $(this).height() * 0.75;
        $(this).css('top', h);
    })
}