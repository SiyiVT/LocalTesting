let hospitalName = "";
let specialtyName = "";
let doctorName = "";
let hospitalLocation = "";
let hospitalBrand = "All";

function showBrandInDDL(data)
{
    let brands = [];

    for (let index = 0; index < data.length; index++) {

        if (!brands.includes(data[index].dataset.brand)) {
            brands.push(data[index].dataset.brand);
        }
    }

    brands.sort();
    
    $(".hos-brand:not(.all)").remove();

    for (let i = 0; i < brands.length; i++) {
        $("#ddlBrand").append('<option value="'+ brands[i] +'" data-brand="'+ brands[i] +'" class="hos-brand">'+ brands[i] +'</option>');
    }

    $('#ddlbrand').select2({
        dropdownAutoWidth: true,
        dropdownPosition: 'below',
        dropdownCssClass: "stylerr",

        minimumResultsForSearch: -1,
        placeholder: "Your Preferred Hospital",
    });

}


function showSpecialtyInDDL(data){

    $(".hos-specialty").hide();

    let specialties = [];

    for (let index = 0; index < data.length; index++) {

        if (!specialties.includes(data[index].dataset.mainspecialty)) {
            specialties.push(data[index].dataset.mainspecialty);
        }

        if(data[index].dataset.subspecialty != "")
        {
            var subspec = data[index].dataset.subspecialty.split(",");
            subspec.forEach(element => {
                if (!specialties.includes(element.trim())) {
                    specialties.push(element.trim());
                }
            });
        }
    }

    specialties.sort();

    $(".hos-specialty:not(.all)").remove();
    for (let i = 0; i < specialties.length; i++) {
        $("#ddlSpeciality").append('<option value="'+ specialties[i] +'" data-specialty="'+ specialties[i] +'" class="hos-specialty">'+ specialties[i] +'</option>');
    }

    $('#ddlSpeciality').select2({

        dropdownAutoWidth: true,
        dropdownPosition: 'below',
        dropdownCssClass: "select-specialty",

        minimumResultsForSearch: -1,
        placeholder: "Your Preferred Specialty",

    });

}

function generateDoctorNameInDDL(allDocs){

    $(".doctor-search").remove();

    for (let i = 0; i < allDocs.length; i++) {
        $("#ddlDoctors").append('<option value="'+ allDocs[i].dataset.doctorname +'" class="doctor-search">'+ allDocs[i].dataset.doctorname +'</option>');
    }
    
    $('#ddlDoctors').select2({
        dropdownAutoWidth: true,
        dropdownPosition: 'below',
        dropdownCssClass: "stylerr",

        placeholder: "Your Preferred Doctor Name",

    });
}

function filterDoctor(){
    var filterData = allDocs.filter(function(index){
        return allDocs[index].dataset.doctorname.includes(doctorName);
    });

    showData(filterData);
    resetShowCount();
    updateDoctorCount();
    updateChunk();
    resizeDoctorDivider();
    setTimeout(function(){
        resizeDoctorDivider();
    }, 100);
}

function showData(data){
    
    $(".find-doctors .doctors-items").empty();

    for (let index = 0; index < data.length; index++) {
       

        if(data[index].dataset.expyear != "")
        {
            $(".find-doctors .doctors-items").append('<div class="doctors-item"><a href="'+ data[index].dataset.docurl +'" class="doctors-item-img-wrapper"><img src="' + data[index].dataset.docimgsrc + '" alt="" class="doctors-item-img" loading="lazy"></a><img src="/ResourcePackages/IHH/assets/dist/images/common/doctor-img-bottom.webp" alt="" class="doctor-bottom-divider"><div class="doctors-texts"><a href="'+ data[index].dataset.docurl +'" class="doctors-texts-name">' + data[index].dataset.doctorname + '</a><div class="doctors-texts-specialty">' + data[index].dataset.mainspecialty + '</div><div class="doctors-texts-experience">'+data[index].dataset.expyear+'</div><div class="doctors-texts-l"><i class="fas fa-map-marker-alt"></i><div class="doctors-texts-location">'+ data[index].dataset.hospital +'</div></div></div><div class="doctors-buttons"><a href="'+ data[index].dataset.appturl +'" class="doctors-buttons-appointment"><div>Make an Appointment</div></a><a href="' + data[index].dataset.docurl +'" class="doctors-buttons-profile"><div>View Profile</div></a></div></div>');
        }
        else{
            $(".find-doctors .doctors-items").append('<div class="doctors-item"><a href="'+ data[index].dataset.docurl +'" class="doctors-item-img-wrapper"><img src="' + data[index].dataset.docimgsrc + '" alt="" class="doctors-item-img" loading="lazy"></a><img src="/ResourcePackages/IHH/assets/dist/images/common/doctor-img-bottom.webp" alt="" class="doctor-bottom-divider"><div class="doctors-texts"><a href="'+ data[index].dataset.docurl +'" class="doctors-texts-name">' + data[index].dataset.doctorname + '</a><div class="doctors-texts-specialty">' + data[index].dataset.mainspecialty + '</div><div class="doctors-texts-l"><i class="fas fa-map-marker-alt"></i><div class="doctors-texts-location">'+ data[index].dataset.hospital +'</div></div></div><div class="doctors-buttons"><a href="'+ data[index].dataset.appturl +'" class="doctors-buttons-appointment"><div>Make an Appointment</div></a><a href="' + data[index].dataset.docurl +'" class="doctors-buttons-profile"><div>View Profile</div></a></div></div>');
        }
        
    }
    
}

function CompareSpecialty(index)
{
    if(specialtyName !="")
    {
        var subspecialty = allDocs[index].dataset.subspecialty;
        var specialties =[];
        if(subspecialty != "")
        {
            var splitSpecialty = subspecialty.split(',');
            
            for(var i = 0; i < splitSpecialty.length; i++)
            {
                specialties.push(splitSpecialty[i].trim());
            }
        }
        specialties.push(allDocs[index].dataset.mainspecialty);

        for(var y = 0; y < specialties.length; y++)
        {
            if(specialtyName == specialties[y])
            {
                return true;
            }
        }

        return false;
    }
        
    return true;
    
}
function filter(num) {

    var filterData = allDocs.filter(function(index){
        return CompareSpecialty(index) && allDocs[index].dataset.location.includes(hospitalLocation) && allDocs[index].dataset.brand.includes(hospitalBrand);
    });
    
    
    switch(num)
    {
        case 1:
            showSpecialtyInDDL(filterData);
            showBrandInDDL(filterData);
            generateDoctorNameInDDL(filterData);
            break;
        case 2:
            showSpecialtyInDDL(filterData);
            generateDoctorNameInDDL(filterData);
            break;
        case 3:
            generateDoctorNameInDDL(filterData);
            break;
    }

    showData(filterData);
    resetShowCount();
    updateDoctorCount();
    updateChunk();
    resizeDoctorDivider();
    setTimeout(function(){
        resizeDoctorDivider();
    }, 100);
   
    
}


$( document ).ready(function() {

    // $("#ddlDoctors").select2();


    allDocs = $('#hiddendoctorlist span');

    $('select#ddlLocation').on('change', function() {

        hospitalLocation = this.value;
        

        if(hospitalLocation == "All")
        {
            hospitalLocation = "";
        }

        $('#ddlBrand').val("");
        hospitalBrand = "";
    
        $('#ddlBrand').select2({
    
            dropdownAutoWidth: true,
            dropdownPosition: 'below',
            dropdownCssClass: "stylerr",
    
            minimumResultsForSearch: -1,
            placeholder: "Your Preferred Hospital",

        });

        $('select#ddlSpeciality').val("");
        specialtyName = "";

        filter(1);
    });

    $('select#ddlBrand').on('change', function() {

        hospitalBrand = this.value;
        if(hospitalBrand== "All")
        {
            hospitalBrand = "";
        }

        hospitalLocation = $("#ddlLocation").val();
        if(hospitalLocation == "All")
        {
            hospitalLocation = "";
        }

        $('select#ddlSpeciality').val("");
        specialtyName = "";

        filter(2);
    });

    $('select#ddlSpeciality').on('change', function() {
       
        hospitalBrand = $('#ddlBrand').val();
        if(hospitalBrand == "All")
        {
            hospitalBrand = "";
        }

        hospitalLocation = $('#ddlLocation').val();
        if(hospitalLocation == "All")
        {
            hospitalLocation = "";
        }

        specialtyName = this.value;
        if(specialtyName == "All")
        {
            specialtyName = "";
        }
       
        filter(3);
    });
   
    $('select#ddlDoctors').on('change', function() {
       
        doctorName = $('select#ddlDoctors').val().trim();
        if(doctorName == "All")
        {
            doctorName = "";
        }
        filterDoctor();
    });

    // $('select#ddlHospital').on('change', function() {
    //     if(window.location.pathname == "/doctors")
    //     {
    //         hospitalName = $('select#ddlHospital').val();
    //         if(hospitalName == "All")
    //         {
    //             hospitalName = "";
    //         }
    //         $('select#ddlSpeciality').val("All");
    //         specialtyName = "";

    //         $('.select-specialty').select2({

    //             dropdownAutoWidth: true,
    //             dropdownPosition: 'below',
    //             dropdownCssClass: "select-specialty",
        
    //             minimumResultsForSearch: -1,
    //             placeholder: "Your Preferred Specialty",
        
    //         });

    //         filter(2);

    //     }
    //     else
    //     {
    //         window.location.href = $('select#ddlHospital').find(":selected").data("url");
    //     }
        
    // });

});
