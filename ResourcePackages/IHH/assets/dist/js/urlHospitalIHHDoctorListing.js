let hospitalName = "";
let specialtyName = "";
var makeAnAppt = "Make an Appointment";
var viewProfile = "View Profile";

jQuery(()=>{

    let currentCulture = "en";
    let ihhlocalPrefix = window.location.origin;
    if(window.location.href.includes("/id/"))
    {
        currentCulture  = "id";
        ihhlocalPrefix = window.location.origin + "/id";
        makeAnAppt = "Buat Janji Temu";
        viewProfile = "Lihat Profil";

    }

    function UpdateDoctor()
    {
        var xhr = new XMLHttpRequest();

        var spec = encodeURIComponent(specialtyName);

        if(currentCulture == "id")
        {
            var url = '/api/default/ihhdoctors?$filter=contains(HospitalName,%27'+ hospitalName +'%27)%20and(%20contains(MainSpecialtyName,%27'+ spec +'%27)%20or%20contains(SubSpecialtyName,%27'+ spec +'%27))&$expand=DoctorImage($select=Url)&$orderby=Name&sf_culture=id';
        }
        else
        {
            var url = '/api/default/ihhdoctors?$filter=contains(HospitalName,%27'+ hospitalName +'%27)%20and(%20contains(MainSpecialtyName,%27'+ spec +'%27)%20or%20contains(SubSpecialtyName,%27'+ spec +'%27))&$expand=DoctorImage($select=Url)&$orderby=Name';
        }

        xhr.open("GET", url, true);

        // function execute after request is successful 
        xhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                var json = JSON.parse(this.responseText);
                ShowSpecialtyDoctor(json.value);
            }
        }

        // Sending our request 
        xhr.send();
    }

    function ShowSpecialtyDoctor(data){
    
        $(".doctors .doctors-items").empty();

        var count = 0;

        for (let index = 0; index < data.length; index++) {

            if(specialtyName != "")
            {
                if(!CompareSpecialtyDoctor(data[index]))
                {
                    continue;
                }
            }
            var style = "";
            if(count > 7)
            {
                style = "display:none;"
            }

            if(data[index].YearsOfExperience != "" && data[index].YearsOfExperience != null)
            {
                $(".doctors .doctors-items").append('<div class="doctors-item" style="'+style+'" ><a href="'+ ihhlocalPrefix + data[index].ProfileUrl +'" class="doctors-item-img-wrapper"><img src="' + window.location.origin + data[index].DoctorImage[0].Url+ '" alt="" class="doctors-item-img" loading="lazy"></a><img src="/ResourcePackages/IHH/assets/dist/images/common/doctor-img-bottom.webp?t=1" alt="" class="doctor-bottom-divider"><div class="doctors-texts"><a href="'+  ihhlocalPrefix + data[index].ProfileUrl +'" class="doctors-texts-name">' + data[index].Salutation + ' ' + data[index].Name + '</a><div class="doctors-texts-specialty">' + data[index].MainSpecialtyName + '</div><div class="doctors-texts-experience">'+data[index].YearsOfExperience+'</div><div class="doctors-texts-l"><i class="fas fa-map-marker-alt"></i><div class="doctors-texts-location">'+ data[index].HospitalName +'</div></div></div><div class="doctors-buttons"><a href="'+  ihhlocalPrefix + data[index].AppointmentUrl +'" class="doctors-buttons-appointment"><div>' + makeAnAppt + '</div></a><a href="' + ihhlocalPrefix + data[index].ProfileUrl +'" class="doctors-buttons-profile"><div>' + viewProfile + '</div></a></div></div>');
            }
            else{
                $(".doctors .doctors-items").append('<div class="doctors-item" style="'+style+'" ><a href="'+ ihhlocalPrefix + data[index].ProfileUrl +'" class="doctors-item-img-wrapper"><img src="' + window.location.origin + data[index].DoctorImage[0].Url+ '" alt="" class="doctors-item-img" loading="lazy"></a><img src="/ResourcePackages/IHH/assets/dist/images/common/doctor-img-bottom.webp?t=1" alt="" class="doctor-bottom-divider"><div class="doctors-texts"><a href="'+  ihhlocalPrefix + data[index].ProfileUrl +'" class="doctors-texts-name">' + data[index].Salutation + ' ' + data[index].Name + '</a><div class="doctors-texts-specialty">' + data[index].MainSpecialtyName + '</div><div class="doctors-texts-l"><i class="fas fa-map-marker-alt"></i><div class="doctors-texts-location">'+ data[index].HospitalName +'</div></div></div><div class="doctors-buttons"><a href="'+  ihhlocalPrefix + data[index].AppointmentUrl +'" class="doctors-buttons-appointment"><div>' + makeAnAppt + '</div></a><a href="' + ihhlocalPrefix + data[index].ProfileUrl +'" class="doctors-buttons-profile"><div>' + viewProfile + '</div></a></div></div>');
            }
            
            count++;
        }
        
        setTimeout(function(){
            resizeDoctorDivider();
        }, 100);
    }

    function CompareSpecialtyDoctor(data)
    {
        var specialties =[];

        if(specialtyName != "")
        {
            var subspecialty = data.SubSpecialtyName;
            
            if(subspecialty != "")
            {
                var splitSpecialty = subspecialty.split(',');
                
                for(var i = 0; i < splitSpecialty.length; i++)
                {
                    specialties.push(splitSpecialty[i].trim());
                }
            }
        }
          
        specialties.push(data.MainSpecialtyName);

        for(var y = 0; y < specialties.length; y++)
        {
            if(specialtyName == specialties[y])
            {
                return true;
            }
        }

        return false;
    }

    function ShowDoctor(data){
    
        $(".doctors .doctors-items").empty();
    
        for (let index = 0; index < data.length; index++) {

            if(data[index].YearsOfExperience != "" && data[index].YearsOfExperience != null)
            {
                $(".doctors .doctors-items").append('<div class="doctors-item"><a href="'+ ihhlocalPrefix + data[index].ProfileUrl +'" class="doctors-item-img-wrapper"><img src="' + window.location.origin + data[index].DoctorImage[0].Url+ '" alt="" class="doctors-item-img" loading="lazy"></a><img src="/ResourcePackages/IHH/assets/dist/images/common/doctor-img-bottom.webp?t=1" alt="" class="doctor-bottom-divider"><div class="doctors-texts"><a href="'+  ihhlocalPrefix + data[index].ProfileUrl +'" class="doctors-texts-name">' + data[index].Salutation + ' ' + data[index].Name + '</a><div class="doctors-texts-specialty">' + data[index].MainSpecialtyName + '</div><div class="doctors-texts-experience">'+data[index].YearsOfExperience+'</div><div class="doctors-texts-l"><i class="fas fa-map-marker-alt"></i><div class="doctors-texts-location">'+ data[index].HospitalName +'</div></div></div><div class="doctors-buttons"><a href="'+  ihhlocalPrefix + data[index].AppointmentUrl +'" class="doctors-buttons-appointment"><div>' + makeAnAppt + '</div></a><a href="' + ihhlocalPrefix + data[index].ProfileUrl +'" class="doctors-buttons-profile"><div>' + viewProfile + '</div></a></div></div>');
            }
            else{
                $(".doctors .doctors-items").append('<div class="doctors-item"><a href="'+ ihhlocalPrefix + data[index].ProfileUrl +'" class="doctors-item-img-wrapper"><img src="' + window.location.origin + data[index].DoctorImage[0].Url+ '" alt="" class="doctors-item-img" loading="lazy"></a><img src="/ResourcePackages/IHH/assets/dist/images/common/doctor-img-bottom.webp?t=1" alt="" class="doctor-bottom-divider"><div class="doctors-texts"><a href="'+  ihhlocalPrefix + data[index].ProfileUrl +'" class="doctors-texts-name">' + data[index].Salutation + ' ' + data[index].Name + '</a><div class="doctors-texts-specialty">' + data[index].MainSpecialtyName + '</div><div class="doctors-texts-l"><i class="fas fa-map-marker-alt"></i><div class="doctors-texts-location">'+ data[index].HospitalName +'</div></div></div><div class="doctors-buttons"><a href="'+  ihhlocalPrefix + data[index].AppointmentUrl +'" class="doctors-buttons-appointment"><div>' + makeAnAppt + '</div></a><a href="' + ihhlocalPrefix + data[index].ProfileUrl +'" class="doctors-buttons-profile"><div>' + viewProfile + '</div></a></div></div>');
            }
            
        }
        
        setTimeout(function(){
            resizeDoctorDivider();
        }, 100);
    }

    function initialDoctor()
    {
        var xhr = new XMLHttpRequest();

        hospitalName = $(".hospital-brand-detail .doctors").data("hospital").replaceAll(' ', '%20');
    
        if(currentCulture == "id")
        {
            var url = '/api/default/ihhdoctors?$filter=contains(HospitalName,%27'+ hospitalName +'%27)&$top=8&$expand=DoctorImage($select=Url)&$orderby=Name&sf_culture=id';
        }
        else
        {
            var url = '/api/default/ihhdoctors?$filter=contains(HospitalName,%27'+ hospitalName +'%27)&$top=8&$expand=DoctorImage($select=Url)&$orderby=Name';
        }
        

        xhr.open("GET", url, true);

        // function execute after request is successful 
        xhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                var json = JSON.parse(this.responseText);
                ShowDoctor(json.value);
            }
        }

        // Sending our request 
        xhr.send();

    }

    function initialSpecialtyDDL()
    {
        var xhr = new XMLHttpRequest();

        hospitalName = $(".hospital-brand-detail .doctors").data("hospital").replaceAll(' ', '%20');

        if(currentCulture == "id")
        {
            var url = '/api/default/ihhdoctors?$filter=contains(HospitalName,%27'+ hospitalName +'%27)&$select=SubSpecialtyName,MainSpecialtyName,Name,Salutation&$orderby=Name&sf_culture=id';
        }
        else
        {
            var url = '/api/default/ihhdoctors?$filter=contains(HospitalName,%27'+ hospitalName +'%27)&$select=SubSpecialtyName,MainSpecialtyName,Name,Salutation&$orderby=Name';
        }

        xhr.open("GET", url, true);

        // function execute after request is successful 
        xhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                var json = JSON.parse(this.responseText);
                generateSpecialtyInDDL(json.value);
            }
        }

        // Sending our request 
        xhr.send();
    }

    function generateSpecialtyInDDL(data){
    
        let specialties = [];
    
        for (let index = 0; index < data.length; index++) {
    
            if (!specialties.includes(data[index].MainSpecialtyName)) {
                specialties.push(data[index].MainSpecialtyName);
            }
    
            if(data[index].SubSpecialtyName != "")
            {
                var subspec = data[index].SubSpecialtyName.split(",");
                subspec.forEach(element => {
                    if (!specialties.includes(element.trim())) {
                        specialties.push(element.trim());
                    }
                });
            }
        }
    
        specialties.sort();

        for (let i = 0; i < specialties.length; i++) {
            $("#ddlSpecialty").append('<option value="'+ specialties[i] +'" data-specialty="'+ specialties[i] +'" class="hos-specialty">'+ specialties[i] +'</option>');
        }

        updateSpecialtyDDL();
    }

    initialSpecialtyDDL();
    initialDoctor();

    $('select#ddlSpecialty').on('change', function() {
       
        hospitalName = $(".hospital-brand-detail .doctors").data("hospital");

        specialtyName = this.value;
        if(specialtyName == "all")
        {
            specialtyName = "";
        }
        
        UpdateDoctor();

    });
})

resizeDoctorDivider = () => {
    $('.doctor-bottom-divider').each(function () {
        var h = $(this).siblings('.doctors-item-img-wrapper').height() - $(this).height() * 0.75;
        $(this).css('top', h);
    })
}
