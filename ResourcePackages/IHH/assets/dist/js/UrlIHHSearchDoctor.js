let hospitalName = "";
let specialtyName = "";
let doctorName = "";
let hospitalLocation = "";
let hospitalBrand = "";
let doctorLength = 0;

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
    
    function initialSpecialtyDoctorDDLFilterBySpecialty()
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

        var hoslocation = hospitalLocation.replaceAll(' ', '%20');
        var hosBrand = hospitalBrand.replaceAll(' ', '%20');

        if(currentCulture == "id")
        {
            var url = '/api/default/ihhdoctors?$filter=contains(Location,%27'+ hoslocation +'%27)%20and%20contains(Brand,%27'+ hosBrand +'%27)&$select=SubSpecialtyName,MainSpecialtyName,Name,Salutation&$orderby=Name&sf_culture=id';
        }
        else
        {
            var url = '/api/default/ihhdoctors?$filter=contains(Location,%27'+ hoslocation +'%27)%20and%20contains(Brand,%27'+ hosBrand +'%27)&$select=SubSpecialtyName,MainSpecialtyName,Name,Salutation&$orderby=Name';
        }

        xhr.open("GET", url, true);

        // function execute after request is successful 
        xhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                var json = JSON.parse(this.responseText);
                doctorLength = json.value.length;
                generateSpecialtyInDDLFilterBySpecialty(json.value);
                generateDoctorNameInDDL(json.value);
            }
        }

        // Sending our request 
        xhr.send();
    }
    function ShowDoctor(data){
    
        $(".find-doctors .doctors-items").empty();
    
        for (let index = 0; index < data.length; index++) {
            if(data[index].YearsOfExperience != "" && data[index].YearsOfExperience != null)
            {
                $(".find-doctors .doctors-items").append('<div class="doctors-item"><a href="'+ ihhlocalPrefix + data[index].ProfileUrl +'" class="doctors-item-img-wrapper"><img src="' + window.location.origin + data[index].DoctorImage[0].Url+ '" alt="" class="doctors-item-img" loading="lazy"></a><img src="/ResourcePackages/IHH/assets/dist/images/common/doctor-img-bottom.webp?t=1" alt="" class="doctor-bottom-divider"><div class="doctors-texts"><a href="'+  ihhlocalPrefix + data[index].ProfileUrl +'" class="doctors-texts-name">' + data[index].Salutation + ' ' + data[index].Name + '</a><div class="doctors-texts-specialty">' + data[index].MainSpecialtyName + '</div><div class="doctors-texts-experience">'+data[index].YearsOfExperience+'</div><div class="doctors-texts-l"><i class="fas fa-map-marker-alt"></i><div class="doctors-texts-location">'+ data[index].HospitalName +'</div></div></div><div class="doctors-buttons"><a href="'+  ihhlocalPrefix + data[index].AppointmentUrl +'" class="doctors-buttons-appointment"><div>' + makeAnAppt +'</div></a><a href="' + ihhlocalPrefix + data[index].ProfileUrl +'" class="doctors-buttons-profile"><div>' + viewProfile +'</div></a></div></div>');
            }
            else{
                $(".find-doctors .doctors-items").append('<div class="doctors-item"><a href="'+ ihhlocalPrefix + data[index].ProfileUrl +'" class="doctors-item-img-wrapper"><img src="' + window.location.origin + data[index].DoctorImage[0].Url+ '" alt="" class="doctors-item-img" loading="lazy"></a><img src="/ResourcePackages/IHH/assets/dist/images/common/doctor-img-bottom.webp?t=1" alt="" class="doctor-bottom-divider"><div class="doctors-texts"><a href="'+  ihhlocalPrefix + data[index].ProfileUrl +'" class="doctors-texts-name">' + data[index].Salutation + ' ' + data[index].Name + '</a><div class="doctors-texts-specialty">' + data[index].MainSpecialtyName + '</div><div class="doctors-texts-l"><i class="fas fa-map-marker-alt"></i><div class="doctors-texts-location">'+ data[index].HospitalName +'</div></div></div><div class="doctors-buttons"><a href="'+  ihhlocalPrefix + data[index].AppointmentUrl +'" class="doctors-buttons-appointment"><div>' + makeAnAppt +'</div></a><a href="' + ihhlocalPrefix + data[index].ProfileUrl +'" class="doctors-buttons-profile"><div>' + viewProfile +'</div></a></div></div>');
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
            var style = "";
            if(count > 11)
            {
                style = "display:none;"
            }

            if(data[index].YearsOfExperience != "" && data[index].YearsOfExperience != null)
            {
                $(".find-doctors .doctors-items").append('<div class="doctors-item" style="'+style+'" ><a href="'+ ihhlocalPrefix + data[index].ProfileUrl +'" class="doctors-item-img-wrapper"><img src="' + window.location.origin + data[index].DoctorImage[0].Url+ '" alt="" class="doctors-item-img" loading="lazy"></a><img src="/ResourcePackages/IHH/assets/dist/images/common/doctor-img-bottom.webp?t=1" alt="" class="doctor-bottom-divider"><div class="doctors-texts"><a href="'+  ihhlocalPrefix + data[index].ProfileUrl +'" class="doctors-texts-name">' + data[index].Salutation + ' ' + data[index].Name + '</a><div class="doctors-texts-specialty">' + data[index].MainSpecialtyName + '</div><div class="doctors-texts-experience">'+data[index].YearsOfExperience+'</div><div class="doctors-texts-l"><i class="fas fa-map-marker-alt"></i><div class="doctors-texts-location">'+ data[index].HospitalName +'</div></div></div><div class="doctors-buttons"><a href="'+  ihhlocalPrefix + data[index].AppointmentUrl +'" class="doctors-buttons-appointment"><div>' + makeAnAppt +'</div></a><a href="' + ihhlocalPrefix + data[index].ProfileUrl +'" class="doctors-buttons-profile"><div>' + viewProfile +'</div></a></div></div>');
            }
            else{
                $(".find-doctors .doctors-items").append('<div class="doctors-item" style="'+style+'" ><a href="'+ ihhlocalPrefix + data[index].ProfileUrl +'" class="doctors-item-img-wrapper"><img src="' + window.location.origin + data[index].DoctorImage[0].Url+ '" alt="" class="doctors-item-img" loading="lazy"></a><img src="/ResourcePackages/IHH/assets/dist/images/common/doctor-img-bottom.webp?t=1" alt="" class="doctor-bottom-divider"><div class="doctors-texts"><a href="'+  ihhlocalPrefix + data[index].ProfileUrl +'" class="doctors-texts-name">' + data[index].Salutation + ' ' + data[index].Name + '</a><div class="doctors-texts-specialty">' + data[index].MainSpecialtyName + '</div><div class="doctors-texts-l"><i class="fas fa-map-marker-alt"></i><div class="doctors-texts-location">'+ data[index].HospitalName +'</div></div></div><div class="doctors-buttons"><a href="'+  ihhlocalPrefix + data[index].AppointmentUrl +'" class="doctors-buttons-appointment"><div>' + makeAnAppt +'</div></a><a href="' + ihhlocalPrefix + data[index].ProfileUrl +'" class="doctors-buttons-profile"><div>' + viewProfile +'</div></a></div></div>');
            }
            
            count++;
        }
        
        if(count > 11)
        {
            if($('.doctors-item:hidden').length > 0)
            {
                $(".doctors-view-all").show();
            }
        }

        setTimeout(function(){
            resizeDoctorDivider();
        }, 100);
    }

    function ShowViewMoreDoctor(data){
    
        for (let index = 0; index < data.length; index++) {

            if(data[index].YearsOfExperience != "" && data[index].YearsOfExperience != null)
            {
                $(".find-doctors .doctors-items").append('<div class="doctors-item"><a href="'+ ihhlocalPrefix + data[index].ProfileUrl +'" class="doctors-item-img-wrapper"><img src="' + window.location.origin + data[index].DoctorImage[0].Url+ '" alt="" class="doctors-item-img" loading="lazy"></a><img src="/ResourcePackages/IHH/assets/dist/images/common/doctor-img-bottom.webp?t=1" alt="" class="doctor-bottom-divider"><div class="doctors-texts"><a href="'+  ihhlocalPrefix + data[index].ProfileUrl +'" class="doctors-texts-name">' + data[index].Salutation + ' ' + data[index].Name + '</a><div class="doctors-texts-specialty">' + data[index].MainSpecialtyName + '</div><div class="doctors-texts-experience">'+data[index].YearsOfExperience+'</div><div class="doctors-texts-l"><i class="fas fa-map-marker-alt"></i><div class="doctors-texts-location">'+ data[index].HospitalName +'</div></div></div><div class="doctors-buttons"><a href="'+  ihhlocalPrefix + data[index].AppointmentUrl +'" class="doctors-buttons-appointment"><div>' + makeAnAppt +'</div></a><a href="' + ihhlocalPrefix + data[index].ProfileUrl +'" class="doctors-buttons-profile"><div>' + viewProfile +'</div></a></div></div>');
            }
            else{
                $(".find-doctors .doctors-items").append('<div class="doctors-item"><a href="'+ ihhlocalPrefix + data[index].ProfileUrl +'" class="doctors-item-img-wrapper"><img src="' + window.location.origin + data[index].DoctorImage[0].Url+ '" alt="" class="doctors-item-img" loading="lazy"></a><img src="/ResourcePackages/IHH/assets/dist/images/common/doctor-img-bottom.webp?t=1" alt="" class="doctor-bottom-divider"><div class="doctors-texts"><a href="'+  ihhlocalPrefix + data[index].ProfileUrl +'" class="doctors-texts-name">' + data[index].Salutation + ' ' + data[index].Name + '</a><div class="doctors-texts-specialty">' + data[index].MainSpecialtyName + '</div><div class="doctors-texts-l"><i class="fas fa-map-marker-alt"></i><div class="doctors-texts-location">'+ data[index].HospitalName +'</div></div></div><div class="doctors-buttons"><a href="'+  ihhlocalPrefix + data[index].AppointmentUrl +'" class="doctors-buttons-appointment"><div>' + makeAnAppt +'</div></a><a href="' + ihhlocalPrefix + data[index].ProfileUrl +'" class="doctors-buttons-profile"><div>' + viewProfile +'</div></a></div></div>');
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
            if (data[index].Brand.includes(',')) {
                var bs = data[index].Brand.split(",");
                bs.forEach(element => {
                    if (!brands.includes(element.trim())) {
                        brands.push(element.trim());
                    }
                });
            }
            else 
            {
                if (!brands.includes(data[index].Brand)) {
                    brands.push(data[index].Brand);
                }  
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
            placeholder: preferredHospitalLabel,
        });
    }

    function generateSpecialtyInDDLFilterBySpecialty(data){

        $(".hos-specialty").hide();
    
        let specialties = [];
    
        for (let index = 0; index < data.length; index++) {
    
            if (!specialties.includes(data[index].MainSpecialtyName)) {
                specialties.push(data[index].MainSpecialtyName);
            }
    
            if(data[index].SubSpecialtyName != "")
            {
                var subspec = data[index].SubSpecialtyName.split(",,");
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
            $("#ddlSpecialty").append('<option value="'+ specialties[i] +'" data-specialty="'+ specialties[i] +'" class="hos-specialty">'+ specialties[i] +'</option>');
        }
    
        $('#ddlSpecialty').val(specialtyName); 
        
        $('#ddlSpecialty').select2({
    
            dropdownAutoWidth: true,
            dropdownPosition: 'below',
            dropdownCssClass: "select-specialty",
    
            minimumResultsForSearch: -1,
            placeholder: preferredSpecialtyLabel,
    
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
                var subspec = data[index].SubSpecialtyName.split(",,");
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
            $("#ddlSpecialty").append('<option value="'+ specialties[i] +'" data-specialty="'+ specialties[i] +'" class="hos-specialty">'+ specialties[i] +'</option>');
        }

        $('#ddlSpecialty').select2({
    
            dropdownAutoWidth: true,
            dropdownPosition: 'below',
            dropdownCssClass: "select-specialty",
    
            minimumResultsForSearch: -1,
            placeholder: preferredSpecialtyLabel,
    
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
    
            placeholder: preferredDoctorNameLabel,
    
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

        if(currentCulture == "id")
        {
            var url = '/api/default/ihhdoctors?$filter=contains(Location,%27'+ hoslocation +'%27)%20and%20contains(Brand,%27'+ hosBrand +'%27)&$select=SubSpecialtyName,MainSpecialtyName,Name,Salutation&$orderby=Name&sf_culture=id';
        }
        else
        {
            var url = '/api/default/ihhdoctors?$filter=contains(Location,%27'+ hoslocation +'%27)%20and%20contains(Brand,%27'+ hosBrand +'%27)&$select=SubSpecialtyName,MainSpecialtyName,Name,Salutation&$orderby=Name';

        }
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

        if(currentCulture == "id")
        {
            var url = '/api/default/ihhdoctors?$filter=contains(Location,%27'+ hoslocation +'%27)&$select=Brand&sf_culture=id';
        }
        else
        {
            var url = '/api/default/ihhdoctors?$filter=contains(Location,%27'+ hoslocation +'%27)&$select=Brand';
        }

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

        if(currentCulture == "id")
        {
            var url = '/api/default/ihhdoctors?$filter=contains(Location,%27'+ hoslocation +'%27)%20and%20contains(Brand,%27'+ hosBrand +'%27)&$expand=DoctorImage($select=Url)&$orderby=Name&$top=12&sf_culture=id';
        }
        else
        {
            var url = '/api/default/ihhdoctors?$filter=contains(Location,%27'+ hoslocation +'%27)%20and%20contains(Brand,%27'+ hosBrand +'%27)&$expand=DoctorImage($select=Url)&$orderby=Name&$top=12';

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

        
        if(currentCulture == "id")
        {
            var url = '/api/default/ihhdoctors?$filter=contains(Location,%27'+ hoslocation +'%27)%20and%20contains(Brand,%27'+ hosBrand +'%27)&$expand=DoctorImage($select=Url)&$orderby=Name&$skip='+ currentDoctorItemLength +'&$top=12&sf_culture=id';
        }
        else
        {
            var url = '/api/default/ihhdoctors?$filter=contains(Location,%27'+ hoslocation +'%27)%20and%20contains(Brand,%27'+ hosBrand +'%27)&$expand=DoctorImage($select=Url)&$orderby=Name&$skip='+ currentDoctorItemLength +'&$top=12';

        }

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

        if(currentCulture == "id")
        {
            var url = '/api/default/ihhdoctors?$filter=contains(Location,%27'+ hoslocation +'%27)%20and%20contains(Brand,%27'+ hosBrand +'%27)%20and%20contains(Name,%27'+ doctorName +'%27)&$expand=DoctorImage($select=Url)&$orderby=Name&$top=12&sf_culture=id';
        }
        else
        {
            var url = '/api/default/ihhdoctors?$filter=contains(Location,%27'+ hoslocation +'%27)%20and%20contains(Brand,%27'+ hosBrand +'%27)%20and%20contains(Name,%27'+ doctorName +'%27)&$expand=DoctorImage($select=Url)&$orderby=Name&$top=12';
        }
        
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
        var spec = encodeURIComponent(specialtyName);

        if(currentCulture == "id")
        {
            var url = '/api/default/ihhdoctors?$filter=contains(Location,%27'+ hoslocation +'%27)%20and%20contains(Brand,%27'+ hosBrand +'%27)%20and(%20contains(MainSpecialtyName,%27'+ spec +'%27)%20or%20contains(SubSpecialtyName,%27'+ spec +'%27))&$expand=DoctorImage($select=Url)&$orderby=Name&sf_culture=id';
        }
        else
        {
            var url = '/api/default/ihhdoctors?$filter=contains(Location,%27'+ hoslocation +'%27)%20and%20contains(Brand,%27'+ hosBrand +'%27)%20and(%20contains(MainSpecialtyName,%27'+ spec +'%27)%20or%20contains(SubSpecialtyName,%27'+ spec +'%27))&$expand=DoctorImage($select=Url)&$orderby=Name';
        }
        

        xhr.open("GET", url, true);

        // function execute after request is successful 
        xhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                var json = JSON.parse(this.responseText);
                console.log(json);
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
                var splitSpecialty = subspecialty.split(',,');

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

    if(window.location.search != "" && window.location.search != null)
    {
        specialtyName = window.location.search.substring(3);
        specialtyName = decodeURIComponent(specialtyName);

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

        $(".doctors-view-all").hide();
        
        initialSpecialtyDoctorDDLFilterBySpecialty();
        UpdateSpecialtyDoctorDDL();
        
    }
    else
    {
        initialSpecialtyDoctorDDL();
        updateBrandDDL();
        setTimeout(
            function() {
                initialDoctor();
            }
            , 100);
        setTimeout(function(){
            resizeDoctorDivider();
            UpdateViewMoreBtn();
        }, 500);
    }

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
            placeholder: preferredHospitalLabel,

        });

        $('select#ddlSpecialty').val("");
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

        $('select#ddlSpecialty').val("");
        specialtyName = "";

        initialSpecialtyDoctorDDL();
        setTimeout(
            function() {
                initialDoctor();
            }
            , 100);
    });

    $('select#ddlSpecialty').on('change', function() {
       
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