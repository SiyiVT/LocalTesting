let locationState = "";
let specialtyName = "";

function filter(num) {

    var filterData = allDocs.filter(function(index){
        return (allDocs[index].dataset.mainspecialty.includes(specialtyName) || allDocs[index].dataset.subspecialty.includes(specialtyName)) && allDocs[index].dataset.hospitallocation.includes(locationState);
    });
    
    switch(num){
        case 1:
            showHospital();
            showSpecialties(filterData);
            break;
        
        case 2:
            showHospital();
            break;
    }

    showData(filterData);
}

function showHospital(){
    if(locationState != "")
    {
        $('.hospitals .hospital').hide();
        $('.hospitals .hospital').each(function(index){
            if($(this).data('location') == locationState)
            {
                $(this).show();
            }
        })
    }
    else{
        $('.hospitals .hospital').show();
    }

}

function showSpecialties(data){
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

    $('.specialties .specialty').hide();
    $('.specialties .specialty').each(function(index){
        if($.inArray($(this).data('specialty'), specialties) != -1)
        {
            $(this).show();
            $(this).find("p").css("font-weight", "normal");
        }
        
        if($(this).hasClass("all"))
        {
            $(this).show();
            $(this).find("p").css("font-weight", "600");
        }
    })

    console.log(specialties);

}

function showData(data){

    $(".json .row").empty();

    for (let index = 0; index < data.length; index++) {
        $(".json .row").append('<div class="col-4"><div class="p-5"><h4><a href="'+ data[index].dataset.docurl +'">'+ data[index].dataset.doctorname +'</a></h4><img src="'+ data[index].dataset.docimgsrc +'" title="'+ data[index].dataset.doctorname +'" style="width: 80%;"></div></div>');
        console.log("Name: " + data[index].dataset.doctorname + ", Location: "+ data[index].dataset.hospitallocation + ", Main Specialty: "+ data[index].dataset.mainspecialty + ", Sub Specialty: "+ data[index].dataset.subspecialty);   
    }

    
}


$( document ).ready(function() {

    allDocs = $('#hiddendoctorlist span');

    $('select#locations').on('change', function() {
        locationState = this.value;
        specialtyName = "";
        filter(1);
    });

    $('.specialties .specialty').click(function(){
        specialtyName = $(this).find("p").text();
        if(specialtyName == "All"){
            specialtyName = "";
        }
        $('.specialties .specialty').find("p").css("font-weight", "normal");
        $(this).find("p").css("font-weight", "600");
        filter(2);

    });
});
