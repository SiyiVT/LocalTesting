jQuery(()=>{


    let currentCulture = "en";
 
    if(window.location.href.includes("/id/"))
    {
        currentCulture  = "id";
     
    }

    function generateSpecialty(data){

    
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
    
        
        for (let i = 0; i < specialties.length; i++) {
            $(".specialties-items").append('<div class="specialty-item col-lg-4 col-md-6">'+ specialties[i] +'</div>');
        }
    
    }

    function initialSpecialtyDoctorDDL()
    {
        var xhr = new XMLHttpRequest();
       
        if(currentCulture == "id")
        {
            var url = '/api/default/ihhdoctors?$select=SubSpecialtyName,MainSpecialtyName&$orderby=Name&sf_culture=id';
        }
        else
        {
            var url = '/api/default/ihhdoctors?$select=SubSpecialtyName,MainSpecialtyName&$orderby=Name';

        }
        xhr.open("GET", url, true);

        // function execute after request is successful 
        xhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                var json = JSON.parse(this.responseText);
                doctorLength = json.value.length;
                generateSpecialty(json.value);
            }
        }

        // Sending our request 
        xhr.send();
    }

    initialSpecialtyDoctorDDL();

})
