
jQuery(() => {
    window.onbeforeunload = function () { }
    selects();

    w = $(window).width();

    $(window).on('resize', () => {
        if (w != $(window).width()) {
            selects();
        }

    })

    $('.custom-upload').on('click', () => {
        $('.upload-button').trigger('click');
    })

    // $('.upload-button').on('change', () => {
    //     $('.custom-upload').addClass('uploaded');
    //     $('.custom-upload').html('File Attached');
    // })

    // $('.first-choice-date').daterangepicker({
    //     autoApply: true,
    //     autoUpdateInput: false,


    // });

    $('.checkbox-wrapper').on('click', () => {

        $(this).find('input[type=checkbox]').prop("checked", !$(this).prop("checked"));
    })

    $('.choice-button:not(.active)').on('click', function () {
        setTimeout(() => {
            $('.appt-plswait').addClass('show');
            setTimeout(() => {
                $('.appt-plswait').removeClass('show');
            }, 5000);
        }, 250);
    })
    
});


jQuery(() => {
    var selectcountrycode = $('.select-countrycode + .select2 .select2-selection__placeholder').html();
    setTimeout(function () {
        if (screen.width < 575) {
            $('.select-countrycode + .select2 .select2-selection__placeholder').html('e.g:+60')
        } else {
            $('.select-countrycode + .select2 .select2-selection__placeholder').html(selectcountrycode)
        }
    }, 500)


})

