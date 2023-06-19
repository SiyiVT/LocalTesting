
let hospitalName = $(".hospital-brand-detail .doctors").data("hospital");
let specialtyName = "";

jQuery(function () {

    var allDocs = $('#hiddendoctorlist span');

    function showData(data){
    
        $(".hospital-brand-detail .doctors-items").empty();
        
        var length = 0;
        if(data.length > 8)
        {
            length = 8;
        }
        else{
            length = data.length;
        }

        for (let index = 0; index < length; index++) {

            if(data[index].dataset.expyear != "")
            {
                $(".hospital-brand-detail .doctors-items").append('<div class="doctors-item"><a href="'+ data[index].dataset.docurl +'" class="doctors-item-img-wrapper"><img src="' + data[index].dataset.docimgsrc + '" alt="" class="doctors-item-img"></a><img src="/ResourcePackages/IHH/assets/dist/images/common/doctor-img-bottom.webp" alt="" class="doctor-bottom-divider"><div class="doctors-texts"><a href="'+ data[index].dataset.docurl +'" class="doctors-texts-name">' + data[index].dataset.doctorname + '</a><div class="doctors-texts-specialty">' + data[index].dataset.mainspecialty + '</div><div class="doctors-texts-experience">'+ data[index].dataset.expyear + '</div><div class="doctors-texts-l"><i class="fas fa-map-marker-alt"></i><div class="doctors-texts-location">'+ data[index].dataset.hospital +'</div></div></div><div class="doctors-buttons"><a href="'+ data[index].dataset.docappturl +'" class="doctors-buttons-appointment"><div>Make an Appointment</div></a><a href="' + data[index].dataset.docurl +'" class="doctors-buttons-profile"><div>View Profile</div></a></div></div>');
            }
            else{
                $(".hospital-brand-detail .doctors-items").append('<div class="doctors-item"><a href="'+ data[index].dataset.docurl +'" class="doctors-item-img-wrapper"><img src="' + data[index].dataset.docimgsrc + '" alt="" class="doctors-item-img"></a><img src="/ResourcePackages/IHH/assets/dist/images/common/doctor-img-bottom.webp" alt="" class="doctor-bottom-divider"><div class="doctors-texts"><a href="'+ data[index].dataset.docurl +'" class="doctors-texts-name">' + data[index].dataset.doctorname + '</a><div class="doctors-texts-specialty">' + data[index].dataset.mainspecialty + '</div><div class="doctors-texts-l"><i class="fas fa-map-marker-alt"></i><div class="doctors-texts-location">'+ data[index].dataset.hospital +'</div></div></div><div class="doctors-buttons"><a href="'+ data[index].dataset.docappturl +'" class="doctors-buttons-appointment"><div>Make an Appointment</div></a><a href="' + data[index].dataset.docurl +'" class="doctors-buttons-profile"><div>View Profile</div></a></div></div>');
            }
            
        }
        
    }
    
    function filter() {
    
        var filterData = allDocs.filter(function(index){
            return (allDocs[index].dataset.mainspecialty.includes(specialtyName) || allDocs[index].dataset.subspecialty.includes(specialtyName)) && allDocs[index].dataset.hospital.includes(hospitalName);
        });
        
        console.log(filterData);
        showData(filterData);
        resizeDoctorDivider();
    }

    

    $('select#ddlSpecialty').on('change', function() {
        specialtyName = this.value;
        if(specialtyName == "all")
        {
            specialtyName = "";
        }
        filter();
    });

});
