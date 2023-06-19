let hospitalName = "";
let specialtyName = "";
let doctorName = "";
let hospitalLocation = "";
let hospitalBrand = "";
let doctorLength = 0;

jQuery(()=>{

    function ShowDoctor(data){
    
        $(".find-doctors .doctors-items").empty();
    
        for (let index = 0; index < data.length; index++) {

            var hospital = data[index].HospitalName.split(',');

            var firstHos = hospital[0];

            docProfileUrl = window.location.origin;
            docApptUrl = window.location.origin;

            switch(firstHos)
            {
                case "Prince Court Medical Centre":
                    docProfileUrl = docProfileUrl + "/princecourt-medicalcentre/doctors" + data[index].ItemDefaultUrl;
                    docApptUrl = docApptUrl + "/princecourt-medicalcentre/appointment" + data[index].ItemDefaultUrl;
                    break;
                case "Pantai Hospital Ayer Keroh":
                    docProfileUrl = docProfileUrl + "/pantai/ayer-keroh/doctors" + data[index].ItemDefaultUrl;
                    docApptUrl = docApptUrl + "/pantai/ayer-keroh/appointment" + data[index].ItemDefaultUrl;
                    break;
                case "Pantai Hospital Kuala Lumpur":
                    docProfileUrl = docProfileUrl + "/pantai/kuala-lumpur/doctors" + data[index].ItemDefaultUrl;
                    docApptUrl = docApptUrl + "/pantai/kuala-lumpur/appointment" + data[index].ItemDefaultUrl;
                    break;
                case "Pantai Hospital Penang":
                    docProfileUrl = docProfileUrl + "/pantai/penang/doctors" + data[index].ItemDefaultUrl;
                    docApptUrl = docApptUrl + "/pantai/penang/appointment" + data[index].ItemDefaultUrl;
                    break;
                case "Gleneagles Hospital Kota Kinabalu":
                    docProfileUrl = docProfileUrl + "/gleneagles/kota-kinabalu/doctors" + data[index].ItemDefaultUrl;
                    docApptUrl = docApptUrl + "/gleneagles/kota-kinabalu/appointment" + data[index].ItemDefaultUrl;
                    break;
                case "Gleneagles Hospital Kuala Lumpur":
                    docProfileUrl = docProfileUrl + "/gleneagles/kuala-lumpur/doctors" + data[index].ItemDefaultUrl;
                    docApptUrl = docApptUrl + "/gleneagles/kuala-lumpur/appointment" + data[index].ItemDefaultUrl;
                    break;
                case "Gleneagles Hospital Medini Johor":
                    docProfileUrl = docProfileUrl + "/gleneagles/medini-johor/doctors" + data[index].ItemDefaultUrl;
                    docApptUrl = docApptUrl + "/gleneagles/medini-johor/appointment" + data[index].ItemDefaultUrl;
                    break;
                case "Gleneagles Hospital Penang":
                    docProfileUrl = docProfileUrl + "/gleneagles/penang/doctors" + data[index].ItemDefaultUrl;
                    docApptUrl = docApptUrl + "/gleneagles/penang/appointment" + data[index].ItemDefaultUrl;
                    break;

            }

            if(data[index].YearsOfExperience != "" && data[index].YearsOfExperience != null)
            {
                $(".find-doctors .doctors-items").append('<div class="doctors-item"><a href="'+ data[index].ItemDefaultUrl +'" class="doctors-item-img-wrapper"><img src="' + window.location.origin + data[index].DoctorImage[0].Url+ '" alt="" class="doctors-item-img" loading="lazy"></a><img src="/ResourcePackages/IHH/assets/dist/images/common/doctor-img-bottom.webp" alt="" class="doctor-bottom-divider"><div class="doctors-texts"><a href="'+ data[index].ItemDefaultUrl +'" class="doctors-texts-name">' + data[index].Salutation + ' ' + data[index].Name + '</a><div class="doctors-texts-specialty">' + data[index].MainSpecialtyName + '</div><div class="doctors-texts-experience">'+data[index].YearsOfExperience+'</div><div class="doctors-texts-l"><i class="fas fa-map-marker-alt"></i><div class="doctors-texts-location">'+ data[index].HospitalName +'</div></div></div><div class="doctors-buttons"><a href="'+ docApptUrl  +'" class="doctors-buttons-appointment"><div>Make an Appointment</div></a><a href="' + docProfileUrl +'" class="doctors-buttons-profile"><div>View Profile</div></a></div></div>');
            }
            else{
                $(".find-doctors .doctors-items").append('<div class="doctors-item"><a href="'+ data[index].ItemDefaultUrl +'" class="doctors-item-img-wrapper"><img src="' + window.location.origin + data[index].DoctorImage[0].Url+ '" alt="" class="doctors-item-img" loading="lazy"></a><img src="/ResourcePackages/IHH/assets/dist/images/common/doctor-img-bottom.webp" alt="" class="doctor-bottom-divider"><div class="doctors-texts"><a href="'+ data[index].ItemDefaultUrl +'" class="doctors-texts-name">' + data[index].Salutation + ' ' + data[index].Name + '</a><div class="doctors-texts-specialty">' + data[index].MainSpecialtyName + '</div><div class="doctors-texts-l"><i class="fas fa-map-marker-alt"></i><div class="doctors-texts-location">'+ data[index].HospitalName +'</div></div></div><div class="doctors-buttons"><a href="'+ docApptUrl +'" class="doctors-buttons-appointment"><div>Make an Appointment</div></a><a href="' + docProfileUrl +'" class="doctors-buttons-profile"><div>View Profile</div></a></div></div>');
            }
            
        }
        
        setTimeout(function(){
            resizeDoctorDivider();
            UpdateViewMoreBtn();
        }, 100);
    }

    function ShowSpecialtyDoctor(data){
    
        $(".find-doctors .doctors-items").empty();

        var count = 0;

        for (let index = 0; index < data.length; index++) {

            if(specialtyName != "")
            {
                if(!CompareSpecialtyDoctor(data[index]))
                {
                    continue;
                }
            }
        
            var hospital = data[index].HospitalName.split(',');

            var firstHos = hospital[0];

            docProfileUrl = window.location.origin;
            docApptUrl = window.location.origin;

            switch(firstHos)
            {
                case "Prince Court Medical Centre":
                    docProfileUrl = docProfileUrl + "/princecourt-medicalcentre/doctors" + data[index].ItemDefaultUrl;
                    docApptUrl = docApptUrl + "/princecourt-medicalcentre/appointment" + data[index].ItemDefaultUrl;
                    break;
                case "Pantai Hospital Ayer Keroh":
                    docProfileUrl = docProfileUrl + "/pantai/ayer-keroh/doctors" + data[index].ItemDefaultUrl;
                    docApptUrl = docApptUrl + "/pantai/ayer-keroh/appointment" + data[index].ItemDefaultUrl;
                    break;
                case "Pantai Hospital Kuala Lumpur":
                    docProfileUrl = docProfileUrl + "/pantai/kuala-lumpur/doctors" + data[index].ItemDefaultUrl;
                    docApptUrl = docApptUrl + "/pantai/kuala-lumpur/appointment" + data[index].ItemDefaultUrl;
                    break;
                case "Pantai Hospital Penang":
                    docProfileUrl = docProfileUrl + "/pantai/penang/doctors" + data[index].ItemDefaultUrl;
                    docApptUrl = docApptUrl + "/pantai/penang/appointment" + data[index].ItemDefaultUrl;
                    break;
                case "Gleneagles Hospital Kota Kinabalu":
                    docProfileUrl = docProfileUrl + "/gleneagles/kota-kinabalu/doctors" + data[index].ItemDefaultUrl;
                    docApptUrl = docApptUrl + "/gleneagles/kota-kinabalu/appointment" + data[index].ItemDefaultUrl;
                    break;
                case "Gleneagles Hospital Kuala Lumpur":
                    docProfileUrl = docProfileUrl + "/gleneagles/kuala-lumpur/doctors" + data[index].ItemDefaultUrl;
                    docApptUrl = docApptUrl + "/gleneagles/kuala-lumpur/appointment" + data[index].ItemDefaultUrl;
                    break;
                case "Gleneagles Hospital Medini Johor":
                    docProfileUrl = docProfileUrl + "/gleneagles/medini-johor/doctors" + data[index].ItemDefaultUrl;
                    docApptUrl = docApptUrl + "/gleneagles/medini-johor/appointment" + data[index].ItemDefaultUrl;
                    break;
                case "Gleneagles Hospital Penang":
                    docProfileUrl = docProfileUrl + "/gleneagles/penang/doctors" + data[index].ItemDefaultUrl;
                    docApptUrl = docApptUrl + "/gleneagles/penang/appointment" + data[index].ItemDefaultUrl;
                    break;

            }
            var style = "";
            if(count > 11)
            {
                style = "display:none;"
            }

            if(data[index].YearsOfExperience != "" && data[index].YearsOfExperience != null)
            {
                $(".find-doctors .doctors-items").append('<div class="doctors-item" style="'+style+'"><a href="'+ data[index].ItemDefaultUrl +'" class="doctors-item-img-wrapper"><img src="' + window.location.origin + data[index].DoctorImage[0].Url+ '" alt="" class="doctors-item-img" loading="lazy"></a><img src="/ResourcePackages/IHH/assets/dist/images/common/doctor-img-bottom.webp" alt="" class="doctor-bottom-divider"><div class="doctors-texts"><a href="'+ data[index].ItemDefaultUrl +'" class="doctors-texts-name">' + data[index].Salutation + ' ' + data[index].Name + '</a><div class="doctors-texts-specialty">' + data[index].MainSpecialtyName + '</div><div class="doctors-texts-experience">'+data[index].YearsOfExperience+'</div><div class="doctors-texts-l"><i class="fas fa-map-marker-alt"></i><div class="doctors-texts-location">'+ data[index].HospitalName +'</div></div></div><div class="doctors-buttons"><a href="'+ docApptUrl  +'" class="doctors-buttons-appointment"><div>Make an Appointment</div></a><a href="' + docProfileUrl +'" class="doctors-buttons-profile"><div>View Profile</div></a></div></div>');
            }
            else{
                $(".find-doctors .doctors-items").append('<div class="doctors-item" style="'+style+'"><a href="'+ data[index].ItemDefaultUrl +'" class="doctors-item-img-wrapper"><img src="' + window.location.origin + data[index].DoctorImage[0].Url+ '" alt="" class="doctors-item-img" loading="lazy"></a><img src="/ResourcePackages/IHH/assets/dist/images/common/doctor-img-bottom.webp" alt="" class="doctor-bottom-divider"><div class="doctors-texts"><a href="'+ data[index].ItemDefaultUrl +'" class="doctors-texts-name">' + data[index].Salutation + ' ' + data[index].Name + '</a><div class="doctors-texts-specialty">' + data[index].MainSpecialtyName + '</div><div class="doctors-texts-l"><i class="fas fa-map-marker-alt"></i><div class="doctors-texts-location">'+ data[index].HospitalName +'</div></div></div><div class="doctors-buttons"><a href="'+ docApptUrl +'" class="doctors-buttons-appointment"><div>Make an Appointment</div></a><a href="' + docProfileUrl +'" class="doctors-buttons-profile"><div>View Profile</div></a></div></div>');
            }
            
            count++;
        }
        
        if(count > 11)
        {
            $(".doctors-view-all").show();
        }

        setTimeout(function(){
            resizeDoctorDivider();
        }, 100);
    }

    function ShowViewMoreDoctor(data){
    
        for (let index = 0; index < data.length; index++) {

            var hospital = data[index].HospitalName.split(',');

            var firstHos = hospital[0];

            docProfileUrl = window.location.origin;
            docApptUrl = window.location.origin;

            switch(firstHos)
            {
                case "Prince Court Medical Centre":
                    docProfileUrl = docProfileUrl + "/princecourt-medicalcentre/doctors" + data[index].ItemDefaultUrl;
                    docApptUrl = docApptUrl + "/princecourt-medicalcentre/appointment" + data[index].ItemDefaultUrl;
                    break;
                case "Pantai Hospital Ayer Keroh":
                    docProfileUrl = docProfileUrl + "/pantai/ayer-keroh/doctors" + data[index].ItemDefaultUrl;
                    docApptUrl = docApptUrl + "/pantai/ayer-keroh/appointment" + data[index].ItemDefaultUrl;
                    break;
                case "Pantai Hospital Kuala Lumpur":
                    docProfileUrl = docProfileUrl + "/pantai/kuala-lumpur/doctors" + data[index].ItemDefaultUrl;
                    docApptUrl = docApptUrl + "/pantai/kuala-lumpur/appointment" + data[index].ItemDefaultUrl;
                    break;
                case "Pantai Hospital Penang":
                    docProfileUrl = docProfileUrl + "/pantai/penang/doctors" + data[index].ItemDefaultUrl;
                    docApptUrl = docApptUrl + "/pantai/penang/appointment" + data[index].ItemDefaultUrl;
                    break;
                case "Gleneagles Hospital Kota Kinabalu":
                    docProfileUrl = docProfileUrl + "/gleneagles/kota-kinabalu/doctors" + data[index].ItemDefaultUrl;
                    docApptUrl = docApptUrl + "/gleneagles/kota-kinabalu/appointment" + data[index].ItemDefaultUrl;
                    break;
                case "Gleneagles Hospital Kuala Lumpur":
                    docProfileUrl = docProfileUrl + "/gleneagles/kuala-lumpur/doctors" + data[index].ItemDefaultUrl;
                    docApptUrl = docApptUrl + "/gleneagles/kuala-lumpur/appointment" + data[index].ItemDefaultUrl;
                    break;
                case "Gleneagles Hospital Medini Johor":
                    docProfileUrl = docProfileUrl + "/gleneagles/medini-johor/doctors" + data[index].ItemDefaultUrl;
                    docApptUrl = docApptUrl + "/gleneagles/medini-johor/appointment" + data[index].ItemDefaultUrl;
                    break;
                case "Gleneagles Hospital Penang":
                    docProfileUrl = docProfileUrl + "/gleneagles/penang/doctors" + data[index].ItemDefaultUrl;
                    docApptUrl = docApptUrl + "/gleneagles/penang/appointment" + data[index].ItemDefaultUrl;
                    break;

            }

            if(data[index].YearsOfExperience != "" && data[index].YearsOfExperience != null)
            {
                $(".find-doctors .doctors-items").append('<div class="doctors-item"><a href="'+ data[index].ItemDefaultUrl +'" class="doctors-item-img-wrapper"><img src="' + window.location.origin + data[index].DoctorImage[0].Url+ '" alt="" class="doctors-item-img" loading="lazy"></a><img src="/ResourcePackages/IHH/assets/dist/images/common/doctor-img-bottom.webp" alt="" class="doctor-bottom-divider"><div class="doctors-texts"><a href="'+ data[index].ItemDefaultUrl +'" class="doctors-texts-name">' + data[index].Salutation + ' ' + data[index].Name + '</a><div class="doctors-texts-specialty">' + data[index].MainSpecialtyName + '</div><div class="doctors-texts-experience">'+data[index].YearsOfExperience+'</div><div class="doctors-texts-l"><i class="fas fa-map-marker-alt"></i><div class="doctors-texts-location">'+ data[index].HospitalName +'</div></div></div><div class="doctors-buttons"><a href="'+ docApptUrl  +'" class="doctors-buttons-appointment"><div>Make an Appointment</div></a><a href="' + docProfileUrl +'" class="doctors-buttons-profile"><div>View Profile</div></a></div></div>');
            }
            else{
                $(".find-doctors .doctors-items").append('<div class="doctors-item"><a href="'+ data[index].ItemDefaultUrl +'" class="doctors-item-img-wrapper"><img src="' + window.location.origin + data[index].DoctorImage[0].Url+ '" alt="" class="doctors-item-img" loading="lazy"></a><img src="/ResourcePackages/IHH/assets/dist/images/common/doctor-img-bottom.webp" alt="" class="doctor-bottom-divider"><div class="doctors-texts"><a href="'+ data[index].ItemDefaultUrl +'" class="doctors-texts-name">' + data[index].Salutation + ' ' + data[index].Name + '</a><div class="doctors-texts-specialty">' + data[index].MainSpecialtyName + '</div><div class="doctors-texts-l"><i class="fas fa-map-marker-alt"></i><div class="doctors-texts-location">'+ data[index].HospitalName +'</div></div></div><div class="doctors-buttons"><a href="'+ docApptUrl +'" class="doctors-buttons-appointment"><div>Make an Appointment</div></a><a href="' + docProfileUrl +'" class="doctors-buttons-profile"><div>View Profile</div></a></div></div>');
            }
            
        }
        
        setTimeout(function(){
            resizeDoctorDivider();
            UpdateViewMoreBtn();
        }, 100);
    }

    function generateBrandDDL(data)
    {
        var selected = "";
        if($("#ddlBrand").val() != "" && $("#ddlBrand").val() != null)
        {
            selected = $("#ddlBrand").val();
        }
    
        let brands = [];
        
        for (let index = 0; index < data.length; index++) {
            if (!brands.includes(data[index].Brand)) {
                brands.push(data[index].Brand);
            }      
        }
    
        brands.sort();

        $(".hos-brand:not(.all)").remove();

        for (let i = 0; i < brands.length; i++) {
            var style = "";
            if(brands[i] == selected)
            {
                style ="selected";
            }

            $("#ddlBrand").append('<option value="'+ brands[i] +'" data-brand="'+ brands[i] +'" ' +style +' class="hos-brand">'+ brands[i] +'</option>');
        }
    
        $('#ddlbrand').select2({
            dropdownAutoWidth: true,
            dropdownPosition: 'below',
            dropdownCssClass: "stylerr",
    
            minimumResultsForSearch: -1,
            placeholder: "Your Preferred Hospital",
        });
    }

    function generateSpecialtyInDDL(data){

        $(".hos-specialty").hide();
    
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

    function generateDoctorNameInDDL(data){

        doctorLength = data.length;

        $(".doctor-search").remove();
    
        for (let i = 0; i < data.length; i++) {
            if(specialtyName != "")
            {
                if(CompareSpecialtyDoctor(data[i]))
                {
                    $("#ddlDoctors").append('<option value="'+ data[i].Name +'" class="doctor-search">'+ data[i].Salutation +' '+ data[i].Name +'</option>');
                }
            }
            else
            {
                $("#ddlDoctors").append('<option value="'+ data[i].Name +'" class="doctor-search">'+ data[i].Salutation +' '+ data[i].Name +'</option>');
            }
            
        }
        
        $('#ddlDoctors').select2({
            dropdownAutoWidth: true,
            dropdownPosition: 'below',
            dropdownCssClass: "stylerr",
    
            placeholder: "Your Preferred Doctor Name",
    
        });
    }
    
    function initialSpecialtyDoctorDDL()
    {
        var xhr = new XMLHttpRequest();

        hospitalLocation = $("#ddlLocation").val();
        hospitalBrand = $("#ddlBrand").val();

        if(hospitalLocation == null || hospitalLocation == "All")
        {
            hospitalLocation = "";
        }

        if(hospitalBrand == null || hospitalBrand == "All")
        {
            hospitalBrand = "";
        }

        var hoslocation = hospitalLocation.replace(' ', '%20');
        var hosBrand = hospitalBrand.replace(' ', '%20');

        var url = '/api/default/doctors?$filter=contains(Location,%27'+ hoslocation +'%27)%20and%20contains(Brand,%27'+ hosBrand +'%27)&$select=SubSpecialtyName,MainSpecialtyName,Name,Salutation&$orderby=Name';

        xhr.open("GET", url, true);

        // function execute after request is successful 
        xhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                var json = JSON.parse(this.responseText);
                doctorLength = json.value.length;
                generateSpecialtyInDDL(json.value);
                generateDoctorNameInDDL(json.value);
            }
        }

        // Sending our request 
        xhr.send();
    }

    function updateBrandDDL()
    {
        var xhr = new XMLHttpRequest();
     
        hospitalLocation = $("#ddlLocation").val();
        if(hospitalLocation == null || hospitalLocation == "All")
        {
            hospitalLocation = "";
        }

        var hoslocation = hospitalLocation.replace(' ', '%20');

        var url = '/api/default/doctors?$filter=contains(Location,%27'+ hoslocation +'%27)&$select=Brand';

        xhr.open("GET", url, true);
        // function execute after request is successful 
        xhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                var json = JSON.parse(this.responseText);
                generateBrandDDL(json.value);
            }
        }
      
        // Sending our request 
        xhr.send();
    }

    function initialDoctor()
    {
        var xhr = new XMLHttpRequest();

        hospitalLocation = $("#ddlLocation").val();
        hospitalBrand = $("#ddlBrand").val();

        if(hospitalLocation == null || hospitalLocation == "All")
        {
            hospitalLocation = "";
        }

        if(hospitalBrand == null || hospitalBrand == "All")
        {
            hospitalBrand = "";
        }

        var hoslocation = hospitalLocation.replace(' ', '%20');
        var hosBrand = hospitalBrand.replace(' ', '%20');

        // var url = '/api/default/doctors?$filter=contains(Location,%27'+ hoslocation +'%27)%20and(%20contains(MainSpecialtyName,%27'+ spec +'%27)%20or%20contains(SubSpecialtyName,%27'+ spec +'%27))';

        var url = '/api/default/doctors?$filter=contains(Location,%27'+ hoslocation +'%27)%20and%20contains(Brand,%27'+ hosBrand +'%27)&$expand=DoctorImage($select=Url)&$orderby=Name&$top=12';

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

    function UpdateViewMoreBtn()
    {
        console.log(doctorLength);
        var currentDoctorItem = $('.doctors-item:visible').length;

        if(doctorLength > currentDoctorItem)
        {
            $(".doctors-view-all").show();
        }
        else
        {
            $(".doctors-view-all").hide();
        }
    }

    function UpdateDoctor()
    {
        var xhr = new XMLHttpRequest();

        hospitalLocation = $("#ddlLocation").val();
        hospitalBrand = $("#ddlBrand").val();

        if(hospitalLocation == null || hospitalLocation == "All")
        {
            hospitalLocation = "";
        }

        if(hospitalBrand == null || hospitalBrand == "All")
        {
            hospitalBrand = "";
        }

        var hoslocation = hospitalLocation.replace(' ', '%20');
        var hosBrand = hospitalBrand.replace(' ', '%20');

        var currentDoctorItemLength = $('.doctors-item:visible').length ;

        // var url = '/api/default/doctors?$filter=contains(Location,%27'+ hoslocation +'%27)%20and(%20contains(MainSpecialtyName,%27'+ spec +'%27)%20or%20contains(SubSpecialtyName,%27'+ spec +'%27))';

        var url = '/api/default/doctors?$filter=contains(Location,%27'+ hoslocation +'%27)%20and%20contains(Brand,%27'+ hosBrand +'%27)&$expand=DoctorImage($select=Url)&$orderby=Name&$skip='+ currentDoctorItemLength +'&$top=12';

        xhr.open("GET", url, true);

        // function execute after request is successful 
        xhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                var json = JSON.parse(this.responseText);
                ShowViewMoreDoctor(json.value);
            }
        }

        // Sending our request 
        xhr.send();
    }

    function filterDoctor()
    {
        var xhr = new XMLHttpRequest();

        hospitalLocation = $("#ddlLocation").val();
        hospitalBrand = $("#ddlBrand").val();

        if(hospitalLocation == null || hospitalLocation == "All")
        {
            hospitalLocation = "";
        }

        if(hospitalBrand == null || hospitalBrand == "All")
        {
            hospitalBrand = "";
        }

        var hoslocation = hospitalLocation.replace(' ', '%20');
        var hosBrand = hospitalBrand.replace(' ', '%20');

        var url = '/api/default/doctors?$filter=contains(Location,%27'+ hoslocation +'%27)%20and%20contains(Brand,%27'+ hosBrand +'%27)%20and%20contains(Name,%27'+ doctorName +'%27)&$expand=DoctorImage($select=Url)&$orderby=Name&$top=12';
        xhr.open("GET", url, true);

        // function execute after request is successful 
        xhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                var json = JSON.parse(this.responseText);
                doctorLength = json.value.length;
                ShowDoctor(json.value);
            }
        }

        // Sending our request 
        xhr.send();
    }

    function UpdateSpecialtyDoctor()
    {

    }

    function UpdateSpecialtyDoctorDDL()
    {
        var xhr = new XMLHttpRequest();

        hospitalLocation = $("#ddlLocation").val();
        hospitalBrand = $("#ddlBrand").val();

        if(hospitalLocation == null || hospitalLocation == "All")
        {
            hospitalLocation = "";
        }

        if(hospitalBrand == null || hospitalBrand == "All")
        {
            hospitalBrand = "";
        }

        var hoslocation = hospitalLocation.replace(' ', '%20');
        var hosBrand = hospitalBrand.replace(' ', '%20');

        var url = '/api/default/doctors?$filter=contains(Location,%27'+ hoslocation +'%27)%20and%20contains(Brand,%27'+ hosBrand +'%27)%20and(%20contains(MainSpecialtyName,%27'+ specialtyName +'%27)%20or%20contains(SubSpecialtyName,%27'+ specialtyName +'%27))&$expand=DoctorImage($select=Url)&$orderby=Name';

        xhr.open("GET", url, true);

        // function execute after request is successful 
        xhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                var json = JSON.parse(this.responseText);
                generateDoctorNameInDDL(json.value);
                ShowSpecialtyDoctor(json.value);
            }
        }

        // Sending our request 
        xhr.send();
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

    function ShowHiddenSpecialtyDoctor()
    {
        var doctorItems = $(".doctors-item");
        var currentDoctorItem = $('.doctors-item:visible').length;
        var showLength = currentDoctorItem + 12;
        
        $(".doctors-view-all").hide();

        if(showLength > doctorItems.length)
        {
            $(".doctors-item").show();
        }
        else
        {
            var showDoctor = $(".doctors-item").slice(0, showLength);
            showDoctor.show();
            
            $(".doctors-view-all").show();
        }

        resizeDoctorDivider();
    }

    initialSpecialtyDoctorDDL();
    updateBrandDDL();
    setTimeout(
        function() {
            initialDoctor();
        }
        , 100);
    

    $(".doctors-view-all").on("click", function(){
        if(specialtyName == "")
        {
            UpdateDoctor();
        }
        else
        {
            ShowHiddenSpecialtyDoctor();
        }
    })

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

        initialSpecialtyDoctorDDL();
        updateBrandDDL();
        setTimeout(
            function() {
                initialDoctor();
            }
            , 100);
        
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

        initialSpecialtyDoctorDDL();
        setTimeout(
            function() {
                initialDoctor();
            }
            , 100);
    });

    $('select#ddlSpeciality').on('change', function() {
       
        hospitalBrand = $('#ddlBrand').val();
        if(hospitalBrand == "All" || hospitalBrand == null)
        {
            hospitalBrand = "";
        }

        hospitalLocation = $('#ddlLocation').val();
        if(hospitalLocation == "All" || hospitalLocation == null)
        {
            hospitalLocation = "";
        }

        specialtyName = this.value;
        if(specialtyName == "All")
        {
            specialtyName = "";
        }

        $(".doctors-view-all").hide();
        
        UpdateSpecialtyDoctorDDL();

    });

    $('select#ddlDoctors').on('change', function() {
       
        doctorName = $('select#ddlDoctors').val().trim();
        if(doctorName == "All")
        {
            doctorName = "";
        }
        filterDoctor();
    });
})

resizeDoctorDivider = () => {
    $('.doctor-bottom-divider').each(function () {
        var h = $(this).siblings('.doctors-item-img-wrapper').height() - $(this).height() * 0.75;
        $(this).css('top', h);
    })
}