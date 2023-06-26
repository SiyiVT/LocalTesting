function checkItemResult(itemClass)
{
    var check = false;

    $('.'+ itemClass).each(function(i, obj) {
        if($(this).is(":visible"))
        {
            check = true;
        }
    });

    if(!check)
    {
        $(".travel-guide .no-result").show();
    }
}

function updateBrandDDL()
{
    var selected = "";
    if($("#ddlHotelsBrands").val() != "" && $("#ddlHotelsBrands").val() != null)
    {
        selected = $("#ddlHotelsBrands").val();
    }

    var hotelLocation = $('#ddlHotelsLocation').val();

    if(hotelLocation == "all" || hotelLocation == null )
    {
        hotelLocation = "";
    }

    let brands = [];
    
    $('.hotels-item').each(function(i, obj) {
        if($(this).data("location").includes(hotelLocation))
        {
            if (!brands.includes($(this).data("brand"))) {
                brands.push($(this).data("brand"));
            }
        }
    });

    brands.sort();
    
    $(".hos-brand:not(.all)").remove();

    for (let i = 0; i < brands.length; i++) {
        var style = "";
        if(brands[i] == selected)
        {
            style ="selected";
        }
        $("#ddlHotelsBrands").append('<option value="'+ brands[i] +'" data-brand="'+ brands[i] +'" ' +style +' class="hos-brand">'+ brands[i] +'</option>');
    }
}

jQuery(()=>{

    $("#ddlAirlineLocation").on("change", function(){
        $(".travel-guide .no-result").hide();
        $(".flights-item-hos").hide();
        if($('#ddlAirlineLocation').val() != "all" && $('#ddlAirlineLocation').val() != null)
        {
            $('.flights-item').each(function(i, obj) {
                if($(this).data("location") == $('#ddlAirlineLocation').val())
                {
                    $(this).show();
                }
                else{
                    $(this).hide();
                }
            });
        }
        else
        {
            $('.flights-item').show();
        }
        
        checkItemResult('flights-item');
    })
    
    $("#ddlTouristDestinationLocation").on("change", function(){
        $(".travel-guide .no-result").hide();
        $(".places-item-hos").hide();
    
        if($("#ddlTouristDestinationLocation").val() != "all" && $("#ddlTouristDestinationLocation").val() != null)
        {
            $('.places-item').each(function(i, obj) {
                if($(this).data("location") == $('#ddlTouristDestinationLocation').val())
                {
                    $(this).show();
                }
                else{
                    $(this).hide();
                }
            });
        }
        else
        {
            $('.places-item').show();
        }
    
        checkItemResult('places-item');
    })
    
    $("#ddlHotelsLocation").on("change", function(){
        
        console.log($("#ddlHotelsLocation").val());
        
        $(".travel-guide .no-result").hide();
        $(".hotels-item-hos").hide();
    
        if($("#ddlHotelsLocation").val() != "all")
        {
            $('.hotels-item').each(function(i, obj) {
                if($(this).data("location") == $('#ddlHotelsLocation').val())
                {
                    $(this).show();
                }
                else{
                    $(this).hide();
                }
            });
        }
        else{
            $('.hotels-item').show();
        }

        updateBrandDDL();

        $("#ddlHotelsBrands").val("all");
        $('#ddlHotelsBrands').select2({
            width: 'style',
            minimumResultsForSearch: -1,
        });
        
        checkItemResult('hotels-item');
        
    })
    
    $("#ddlHotelsBrands").on("change", function(){
        $(".travel-guide .no-result").hide();
        if($("#ddlHotelsBrands").val() != "all"){
            $(".hotels-item-hos").hide();
            $('.hotels-item').each(function(i, obj) {
                if( $('#ddlHotelsLocation').val() != "all" && $('#ddlHotelsLocation').val() != null)
                {
                    if($(this).data("location") == $('#ddlHotelsLocation').val() && $(this).data("brand") == $('#ddlHotelsBrands').val())
                    {
                        $(this).show();
                    }
                    else{
                        $(this).hide();
                    }
                }
                else
                {
                    if($(this).data("brand") == $('#ddlHotelsBrands').val())
                    {
                        $(this).show();
                    }
                    else{
                        $(this).hide();
                    }
                }
            });
        }
        else{
            if($('#ddlHotelsLocation').val()!= "all" && $('#ddlHotelsLocation').val() != null)
            {
                $('.hotels-item').each(function(i, obj) {
                    if($(this).data("location") == $('#ddlHotelsLocation').val())
                    {
                        $(this).show();
                    }
                    else{
                        $(this).hide();
                    }
                });
            }
            else{
                $('.hotels-item').show();
            }
        }
        
        checkItemResult('hotels-item');
    })

    function initialHotel()
    {
        var hoslocation = $('#ddlHotelsLocation').val();
        var brand = $('#ddlHotelsBrands').val();
        if(hoslocation == "all" || hoslocation == null )
        {
            hoslocation = "";
        }
        if(brand == "all" || brand == null)
        {
            brand = "";
        }
        $(".travel-guide .no-result").hide();
        
        $('.hotels-item').each(function(i, obj) {
            if($(this).data("location").includes(hoslocation) && $(this).data("brand").includes(brand))
            {
                $(this).show();
            }
            else{
                $(this).hide();
            }
        });
        
        updateBrandDDL();

        $('#ddlHotelsBrands').select2({
            width: 'style',
            minimumResultsForSearch: -1,
        });

        checkItemResult("hotels-item");
    }

    if(window.location.href.split('/').pop() == "hotels")
    {
        initialHotel();
    }
    


})