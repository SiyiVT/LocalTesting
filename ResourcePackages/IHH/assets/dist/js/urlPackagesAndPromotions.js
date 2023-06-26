jQuery(()=>{

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
            $(".package-list .no-result").show();
        }
    }

    function updateBrandDDL()
    {
        var hosLocation = $('#ddlLocation').val();

        if(hosLocation == "All" || hosLocation == null )
        {
            hosLocation = "";
        }

        let brands = [];
        
        $('.package-list .accordion-b-item').each(function(i, obj) {
            if($(this).data("location").includes(hosLocation))
            {
                if (!brands.includes($(this).data("brand"))) {
                    brands.push($(this).data("brand"));
                }
            }
        });

        brands.sort();
        
        $(".hos-brand:not(.all)").remove();

        for (let i = 0; i < brands.length; i++) {
            $("#ddlBrand").append('<option value="'+ brands[i] +'" data-brand="'+ brands[i] +'" class="hos-brand">'+ brands[i] +'</option>');
        }
    }

    function initial()
    {
        var hoslocation = $("#ddlLocation").val();
        var brand = $("#ddlBrand").val();
        if(hoslocation == "All" || hoslocation == null )
        {
            hoslocation = "";
        }
        if(brand == "All" || brand == null)
        {
            brand = "";
        }
        $(".package-list .no-result").hide();
        
        $('.accordion-b-item').each(function (i, obj) {
            if ($(this).data("brand").includes(brand) && $(this).data("location").includes(hoslocation)) {
                $(this).show();
            }
            else {
                $(this).hide();
            }
        });
        
        

        checkItemResult("accordion-b-item");
    }

    initial();

    $('.select-location').select2({

        dropdownAutoWidth: true,
        dropdownPosition: 'below',
        dropdownCssClass: "stylerr",

        minimumResultsForSearch: -1,
        placeholder: preferredLocationLabel ,

    });

    $('.select-brand').select2({

        dropdownAutoWidth: true,
        dropdownPosition: 'below',
        dropdownCssClass: "stylerr",

        minimumResultsForSearch: -1,
        placeholder: preferredHospitalLabel,

    });
    
    $("#ddlLocation").on("change", function () {
        $(".accordion-b-item").hide();
        $(".package-list .no-result").hide();

        $("#ddlBrand").val("");

        $('.select-brand').select2({

            dropdownAutoWidth: true,
            dropdownPosition: 'below',
            dropdownCssClass: "stylerr",
    
            minimumResultsForSearch: -1,
            placeholder: preferredHospitalLabel,
    
        });

        if ($('#ddlLocation').val() != "All" ) {
            $('.accordion-b-item').each(function (i, obj) {
                if ($(this).data("location") == $('#ddlLocation').val()) {
                    $(this).show();
                }
                else {
                    $(this).hide();
                }
            });
        }
        else {
            $('.accordion-b-item').show();
        }

        updateBrandDDL();

        checkItemResult("accordion-b-item");
    })

    $("#ddlBrand").on("change", function () {
        $(".accordion-b-item").hide();
        $(".package-list .no-result").hide();

        var hoslocation = $("#ddlLocation").val();
        if(hoslocation != "" && hoslocation != "All")
        {
            if ($('#ddlBrand').val() != "All" && $('#ddlBrand').val() != null) {
                $('.accordion-b-item').each(function (i, obj) {
                    if ($(this).data("brand") == $('#ddlBrand').val() && $(this).data("location") == hoslocation) {
                        $(this).show();
                    }
                    else {
                        $(this).hide();
                    }
                });
            }
            else {
                $('.accordion-b-item').each(function (i, obj) {
                    if ($(this).data("location") == hoslocation) {
                        $(this).show();
                    }
                    else {
                        $(this).hide();
                    }
                });
            }
        }
        else{

            if ($('#ddlBrand').val() != "All" && $('#ddlBrand').val() != null) {
                $('.accordion-b-item').each(function (i, obj) {
                    if ($(this).data("brand") == $('#ddlBrand').val()) {
                        $(this).show();
                    }
                    else {
                        $(this).hide();
                    }
                });
            }
            else {
                $('.accordion-b-item').show();
            }
        }

        checkItemResult("accordion-b-item");
        
    })


})