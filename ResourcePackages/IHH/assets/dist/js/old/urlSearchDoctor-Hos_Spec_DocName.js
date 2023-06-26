let hospitalName = "";
let specialtyName = "";
let doctorName = "";

function generateDoctorNameInDDL(allDocs){

    $(".doctor-search").remove();

    for (let i = 0; i < allDocs.length; i++) {
        $("#ddlDoctors").append('<option value="'+ allDocs[i].dataset.doctorname +'" class="doctor-search">'+ allDocs[i].dataset.doctorname +'</option>');
        console.log(allDocs[i].dataset.doctorname);
    }
    
    $('.select2#ddlDoctors').select2();
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

    $(".hos-specialty").hide();
    $(".hos-specialty").each(function(index){
        if($.inArray($(this).data('specialty'), specialties) != -1)
        {
            $(this).show();
        }
        
        if($(this).hasClass("all"))
        {
            $(this).show();
            $(this).attr("selected", "selected");
        }
    })

    console.log(specialties);

}

function showData(data){
    
    $(".find-doctors .doctors-items").empty();

    for (let index = 0; index < data.length; index++) {
        $(".find-doctors .doctors-items").append('<div class="doctors-item"><div class="doctors-item-img-wrapper"><img src="' + data[index].dataset.docimgsrc + '" alt="" class="doctors-item-img"></div><div class="doctors-texts"><div class="doctors-texts-name">' + data[index].dataset.doctorname + '</div><div class="doctors-texts-specialty">' + data[index].dataset.mainspecialty + '</div><div class="doctors-texts-experience">10</div><div class="doctors-texts-l"><i class="fas fa-map-marker-alt"></i><div class="doctors-texts-location">'+ data[index].dataset.hospital +'</div></div></div><div class="doctors-buttons"><a href="'+ data[index].dataset.docappturl +'" class="doctors-buttons-appointment"><div>Make an Appointment</div></a><a href="' + data[index].dataset.docurl +'" class="doctors-buttons-profile"><div>View Profile</div></a></div></div>');
        console.log("Name: " + data[index].dataset.doctorname + ", Hospital: "+ data[index].dataset.hospital+ ", Main Specialty: "+ data[index].dataset.mainspecialty + ", Sub Specialty: "+ data[index].dataset.subspecialty);  
    }
    
}

function filterDoctor(){
    var filterData = allDocs.filter(function(index){
        return allDocs[index].dataset.doctorname.includes(doctorName);
    });

    showData(filterData);
}

function filter(num) {

    var filterData = allDocs.filter(function(index){
        return (allDocs[index].dataset.mainspecialty.includes(specialtyName) || allDocs[index].dataset.subspecialty.includes(specialtyName)) && allDocs[index].dataset.hospital.includes(hospitalName);
    });
    
    generateDoctorNameInDDL(filterData);

    if(num == 2)
    {
        showSpecialtyInDDL(filterData);
    }
    
    showData(filterData);
}

$( document ).ready(function() {

    // $("#ddlDoctors").select2();

    allDocs = $('#hiddendoctorlist span');

    $('select#ddlSpeciality').on('change', function() {
        hospitalName = $('select#ddlHospital').val();
        if(hospitalName == "All")
        {
            hospitalName = "";
        }
        specialtyName = this.value;
        if(specialtyName == "All")
        {
            specialtyName = "";
        }
        filter(1);
    });

    $('select#ddlDoctors').on('change', function() {
        hospitalName = $('select#ddlHospital').val();
        if(hospitalName == "All")
        {
            hospitalName = "";
        }
        specialtyName = this.value;
        if(specialtyName == "All")
        {
            specialtyName = "";
        }
        doctorName = $('select#ddlDoctors').val().trim();
        filterDoctor();
    });


    $('select#ddlHospital').on('change', function() {
        if(window.location.pathname == "/doctors")
        {
            hospitalName = $('select#ddlHospital').val();
            if(hospitalName == "All")
            {
                hospitalName = "";
            }
            $('select#ddlSpeciality').val("All");
            specialtyName = "";

            $('.select-specialty').select2({

                dropdownAutoWidth: true,
                dropdownPosition: 'below',
                dropdownCssClass: "select-specialty",
        
                minimumResultsForSearch: -1,
                placeholder: "Your Preferred Specialty",
        
            });

            filter(2);

        }
        else
        {
            window.location.href = $('select#ddlHospital').find(":selected").data("url");
        }
        
    });

});
