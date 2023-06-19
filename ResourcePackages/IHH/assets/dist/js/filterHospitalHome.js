var hosLocation = "";
var hosLocationMobile = "";
jQuery(()=>{

    function filterhos()
    {
        if(hosLocation != "")
        {
            $('.location-tab .hospital-lists-item').each(function(index){
                if($(this).data('location') == hosLocation)
                {
                    $(this).fadeIn(150);
                }
                else
                {
                    $(this).hide();
                }
    
            })
        }
        else{
            $('.location-tab .hospital-lists-item').fadeIn(150);
        }
    }

    function filterhosMobile()
    {
        if(hosLocationMobile != "")
        {
            $('.location-tab-mobile .hospital-lists-item').each(function(index){
                if($(this).data('location') == hosLocationMobile)
                {
                    $(this).fadeIn(150);
                }
                else
                {
                    $(this).hide();
                }
    
            })
        }
        else{
            $('.location-tab-mobile .hospital-lists-item').hide();
        }
    }


    function checkHosIsEmpty(hospitalClass)
    {
        isEmpty = true;

        $('.location-tab .' + hospitalClass + ' .hospital-lists-item').each(function(){
            if($(this).is(":visible"))
            {
                isEmpty = false;
            }
        })

        if(isEmpty)
        {
            $('.location-tab .' + hospitalClass).parent().hide();
        }

    }

    function checkHosIsEmptyMobile(hospitalClass)
    {
        isEmpty = true;

        $('.location-tab-mobile .' + hospitalClass + ' .hospital-lists-item').each(function(){
            if($(this).css('display') != 'none')
            {
                isEmpty = false;
            }
        })

        if(isEmpty)
        {
            $('.location-tab-mobile .' + hospitalClass + ' .isempty-text').fadeIn(150);
        }
        else
        {
            $('.location-tab-mobile .' + hospitalClass + ' .isempty-text').hide();
        }
    }

    $('.location-tab .location-selectors-item').on("click", function(){

        $('.location-tab .location-show .col-4').fadeIn(150);

        if($(this).hasClass('active'))
        {
            hosLocation = $(this).data('location');
        }
        else
        {
            hosLocation = "";
        }
        
        filterhos();

        var brands = ["gleneagles", "pantai", "prince-court"];
        for(var index = 0; index < brands.length; index++)
        {
            checkHosIsEmpty(brands[index]);
        }
    })

    $('.location-tab-mobile .location-selectors-item').on("click", function(){

        // if($('.location-tab-mobile .selectt-wrapper').hasClass("collapsed"))
        // {
        //     var orderNum = $('.location-tab-mobile .selectt-wrapper').attr('data-selected');

        //     $('.location-tab-mobile .location-selectors-item').each(function(){
        //         if($(this).attr('data-order-start') == orderNum)
        //         {
        //             hosLocationMobile = $(this).find(".location-name").data("location");
        //         }
        //     })
            
        // }

        hosLocationMobile = $(this).find(".location-name").data("location");

        filterhosMobile();

        // var brands = ["gleneagles", "pantai", "prince-court"];
        // for(var index = 0; index < brands.length; index++)
        // {
        //     checkHosIsEmptyMobile(brands[index]);
        // }
        setTimeout(() => {
            normalizeCarousel();
        }, 50);
    })
})
