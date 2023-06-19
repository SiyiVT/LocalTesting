let locationState = "";
let mainSpecialtyName = "";
let allDocs;

function generateDoctorNameInDDL(){
    
    $(".doctor-search").remove();

    for (let i = 0; i < allDocs.length; i++) {
        if((allDocs[i].dataset.mainSpecialty.includes(mainSpecialtyName) || allDocs[i].dataset.subSpecialty.includes(mainSpecialtyName)) && allDocs[i].dataset.hospitalLocation.includes(locationState)){
            $("#ddlDoctors").append('<option value="'+ allDocs[i].dataset.name +'" class="doctor-search">'+ allDocs[i].dataset.name +'</option>');
            console.log(allDocs[i].dataset.name);
        }
    }
    
    $('.select2#ddlDoctors').select2();
    console.log("bom!");
}

function showSpecialties(data){
    let specialties = [];

    for (let index = 0; index < data.length; index++) {
        if (!specialties.includes(data[index].dataset.mainSpecialty)) {
            specialties.push(data[index].dataset.mainSpecialty);
        }
    }
    $(".specialties").empty();
    $(".specialties").append("<p class='specialty selected' style='font-weight: 600;'>All</p>");
    specialties.forEach(element => {
        $(".specialties").append("<p class='specialty'>" + element + "</p>");
    });
    
    $('.specialties .specialty').click(function(){
        mainSpecialtyName = $(this).text();
        if(mainSpecialtyName == "All"){
            mainSpecialtyName = "";
        }
        $('.specialties .specialty').css("font-weight", "normal");
        $(this).css("font-weight", "600");
        filter(2);
        generateDoctorNameInDDL();
    });
}

function showHospital(data){
    let hospitals = [];

    for (let index = 0; index < data.length; index++) {
        if (!hospitals.includes(data[index].dataset.hospitalName)) {
            hospitals.push(data[index].dataset.hospitalName);
        }
    }

    $(".hospitals").empty();
    hospitals.forEach(element => {
        $(".hospitals").append("<p class='hospital'>" + element + "</p>");
    });
    
}

function showData(data){

    $(".json").empty();

    for (let index = 0; index < data.length; index++) {
        $(".json").append("<a href='"+ data[index].dataset.url +"'><p>Name: "+ data[index].dataset.name +"</p></a>");
        console.log("Name: " + data[index].dataset.name + ", Location: "+ data[index].dataset.hospitalLocation + ", Hospital Name: "+ data[index].dataset.hospitalName + ", Main Specialty: "+ data[index].dataset.mainSpecialty);   
    }
}

function hiddenData(json){

    var locations = [], specialties = [], hospitals = [];

    for (let index = 0; index < json.value.length; index++) {
        locations.push(json.value[index].Location);
        specialties.push(json.value[index].MainSpecialtyName);
        hospitals.push(json.value[index].HospitalName);

        var subspec = json.value[index].SubSpecialtyName.split(', ');
        subspec.forEach(function(subs){
                specialties.push(subs);
        });

        $('#hiddendoctorlist').append("<span data-url='/search-doctor"+ json.value[index].ItemDefaultUrl +"' data-name='"+ json.value[index].Name + "' data-hospital-location='"+ json.value[index].Location +"' data-hospital-name='"+ json.value[index].HospitalName +"' data-main-specialty='"+ json.value[index].MainSpecialtyName +"' data-sub-specialty='"+ json.value[index].SubSpecialtyName +"'>"+ json.value[index].Name +"</span>");

    }

    allDocs = $('#hiddendoctorlist span');

    var filteredLocations = locations.filter(function(loc, index){
        return locations.indexOf(loc) == index; 
    });

    var filteredHospitals = hospitals.filter(function(hos, index){
        return hospitals.indexOf(hos) == index; 
    });

    var filteredSpecialties = specialties.filter(function(spec, index){
        return specialties.indexOf(spec) == index; 
    });

    filteredLocations.forEach(function(loc){
        $("#locations").append("<option value="+ loc +">"+loc+"</option>");
    });
    
    filteredHospitals.forEach(function(hos){
        $(".hospitals").append("<p class='hospital'>"+hos+"</p>");
    });

    filteredSpecialties.forEach(function(spec){
        $(".specialties").append("<p class='specialty'>"+spec+"</p>");
    });

    $('.specialties .specialty').click(function(){
        mainSpecialtyName = $(this).text().trim();
        if(mainSpecialtyName == "All"){
            mainSpecialtyName = "";
        }
        $('.specialties .specialty').css("font-weight", "normal");
        $(this).css("font-weight", "600");
        console.log(mainSpecialtyName);
        filter(2);
        generateDoctorNameInDDL();
    });
    
}

function filter(num) {

    var filterData = allDocs.filter(function(index){
        return (allDocs[index].dataset.mainSpecialty.includes(mainSpecialtyName) || allDocs[index].dataset.subSpecialty.includes(mainSpecialtyName)) && allDocs[index].dataset.hospitalLocation.includes(locationState);
    });
    
    switch(num){
        case 1:
            showHospital(filterData);
            showSpecialties(filterData);
            break;
        
        case 2:
            showHospital(filterData);
            break;
    }

    showData(filterData);
    generateDoctorNameInDDL();

    // // Creating Our XMLHttpRequest object 
    // var xhr = new XMLHttpRequest();

    // // Making our connection  
    // var url = '/api/default/doctors?$filter=contains(Location,%27'+ loc +'%27)%20and(%20contains(MainSpecialtyName,%27'+ spec +'%27)%20or%20contains(SubSpecialtyName,%27'+ spec +'%27))';
    // console.log(url);
    // xhr.open("GET", url, true);

    // // function execute after request is successful 
    // xhr.onreadystatechange = function () {
    //     if (this.readyState == 4 && this.status == 200) {
    //         var json = JSON.parse(this.responseText);
    //         showSpecialties(json);
    //         showHospital(json);
    //         showData(json); 
    //     }
    // }


    // // Sending our request 
    // xhr.send();

}

// function filter2(loc, spec) {

//     // Creating Our XMLHttpRequest object 
//     var xhr = new XMLHttpRequest();

//     // Making our connection  
//     var url = '/api/default/doctors?$filter=contains(Location,%27'+ loc +'%27)%20and(%20contains(MainSpecialtyName,%27'+ spec +'%27)%20or%20contains(SubSpecialtyName,%27'+ spec +'%27))';
//     xhr.open("GET", url, true);
//     console.log(url);
//     // function execute after request is successful 
//     xhr.onreadystatechange = function () {
//         if (this.readyState == 4 && this.status == 200) {
//             var json = JSON.parse(this.responseText);
//             showHospital(json);
//             showData(json); 
//         }
//     }

//     // Sending our request 
//     xhr.send();
// }

function filterDoctor(name){
    var filterData = allDocs.filter(function(index){
        return allDocs[index].dataset.name.includes(name);
    });

    showData(filterData);
}

$( document ).ready(function() {

    var xhr = new XMLHttpRequest();

    var url = '/api/default/doctors';
    xhr.open("GET", url, true);

    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var json = JSON.parse(this.responseText);
            hiddenData(json);
            showData(allDocs); 
            generateDoctorNameInDDL();
        }
    }

    xhr.send();

    $('select#locations').on('change', function() {
        locationState = this.value;
        mainSpecialtyName = "";
        filter(1);
    });

    $('.select2#ddlDoctors').on("change", function(){
        var doctor = $('.select2#ddlDoctors').val().trim();
        if(doctor != ""){
            filterDoctor(doctor);
        }
        else{
            filter(1); 
        }

    });

    $('.select2#ddlDoctors').select2();

});
