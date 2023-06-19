function isFormValid() {
  var isValid = true;

  $(
    "#ddlHospital, #txtFullName, #txtICorPassportNumber, #txtNationality, #txtEmail, #txtGender, #txtDateOfBirth, #txtApptDate, #txtApptTime, #txtApptDate2, #txtApptTime2, #txtGender, #txtMedicalConcern"
  ).each(function () {
    isValid = isValid && formTxtFieldCheckValidity($(this));
    console.log($(this).attr("id") + " " + formTxtFieldCheckValidity($(this)));
  });

  if ($("#txtEmail").val().length > 0) {
    isValid = isValid && emailValidity($("#txtEmail"));
    console.log(
      $("#txtEmail").attr("id") + " " + emailValidity($("#txtEmail"))
    );
  }

  if ($("#txtCountryCode").val().length > 0 && $("#txtContactNumber").val().length > 0) {
    isValid = isValid && countryCodeValidity($("#txtCountryCode"));
    isValid = isValid && phoneNumValidity($("#txtContactNumber"));
    console.log(
      $("#txtCountryCode").attr("id") +
      " " +
      countryCodeValidity($("#txtCountryCode"))
    );
    console.log(
      $("#txtContactNumber").attr("id") +
      " " +
      phoneNumValidity($("#txtContactNumber"))
    );

  } else {
    isValid = false;
    countryCodeShowErr($("#txtCountryCode"), true);
  }

  if ($("#txtCountryCode2").val().length > 0) {
    isValid = isValid && countryCodeValidity($("#txtCountryCode2"));
    console.log(
      $("#txtCountryCode2").attr("id") +
      " " +
      countryCodeValidity($("#txtCountryCode"))
    );
  }
  if ($("#txtContactNumber2").val().length > 0) {
    isValid = isValid && phoneNumValidity($("#txtContactNumber2"));
    console.log(
      $("#txtContactNumber2").attr("id") +
      " " +
      phoneNumValidity($("#txtContactNumber2"))
    );
  }

  if ($("#consent_checkbox").prop("checked") != true) {
    isValid = false;
    $("#consent_checkbox").parent().next().next(".select-error").css("display", "block");

  }
  else {
    $("#consent_checkbox").parent().next().next(".select-error").css("display", "none");
  }

  return isValid;
}

$(document).ready(function () {
  var healthReportUrl = "";
  var healthReportID;

  let currentCulture = "en";

  if (window.location.href.includes("/id/")) {
    currentCulture = "id";
  }

  if (window.location.pathname != '/appointment') {
    SetAdvanceBookingDays($("#ddlHospital option:selected").data('abdc'));
  }

  $('#validation-button').attr("disabled", true);

  $("#txtApptDate").on("apply.daterangepicker", function (ev, picker) {
    $("#txtApptDate2").val("");

    SetAppointmentDate2();

  });

  function GetAdvanceBookingDayAvailable(days) {
    var _days = parseInt(days);
    var timeZoneOffset = new Date().getTimezoneOffset();
    var nowUtc = moment().add(timeZoneOffset, 'm');
    var nowMy = nowUtc.clone().add(420, 'm');
    var availableDate = nowMy.clone().add(_days, 'day');
    // if (_days > 3) {
    //     return availableDate;
    // }
    var dayOfWeek = availableDate.day(); // 0 is Sunday, 6 is Saturday
    var notInWorkingDay = {
      0: 1, // add 1 day when Sunday
    };
    var daysAdd = notInWorkingDay[dayOfWeek];
    if (!daysAdd) {
      daysAdd = 0; // if in working day
    }
    if (nowMy.day() == 6) {
      daysAdd = daysAdd + 1;
    }
    // check submited after 12PM for Friday
    if (nowMy.hours() >= 12 && nowMy.day() == 5) {
      daysAdd = daysAdd + 1;
    }
    availableDate = nowMy.clone().add(_days + daysAdd, 'day');
    return {
      date: availableDate.clone(),
      daysAdd: _days + daysAdd
    };
  }

  function SetAdvanceBookingDays(days) {
    var holidays = $("#ddlHospital option:selected").data("holidays");
    if (holidays) {
      holidays = holidays.split(',').map(function (item) {
        return item.trim();
      });
    }
    else {
      holidays = [];
    }

    var minDate = GetAdvanceBookingDayAvailable(days);

    //console.log(`SetAdvanceBookingDays : ${days}`);

    $(".single-date.appointment-date").val("");
    $(".single-date.appointment-date").daterangepicker({
      // drops: positionDrops,
      autoApply: true,
      autoUpdateInput: false,
      // parentEl: $('.scroller-inner'),
      //disabling weekends
      isInvalidDate: function (date) {
        // return (date.day() == 0 || date.day() == 6);
        var thisMonth = date._d.getMonth() + 1;
        if (thisMonth < 10) {
          thisMonth = "0" + thisMonth;
        }
        var thisDate = date._d.getDate();
        if (thisDate < 10) {
          thisDate = "0" + thisDate;
        }
        var thisYear = date._d.getYear() + 1900;

        var thisCompare = thisMonth + "/" + thisDate + "/" + thisYear;

        if ($.inArray(thisCompare, holidays) != -1) {
          return true;
        }
        return (date.day() == 0); // deny only Sunday , allow Saturday
      },
      // minDate: new moment().add(days, 'day'),
      minDate: new moment().add(minDate.daysAdd, 'day'),
      maxDate: new moment().add(3, 'month'),
      startDate: new moment().add(minDate.daysAdd, 'day'),
      endDate: new moment().add(minDate.daysAdd, 'day'),
      // opens: Helper.isRTL()?'left':'right',
      singleDatePicker: true,
      locale: {
        // direction: Helper.isRTL()?'rtl':'ltr',
        cancelLabel: 'Clear Dates',
        "daysOfWeek": [
          "S",
          "M",
          "T",
          "W",
          "T",
          "F",
          "S"
        ],
        "monthNames": [
          "January",
          "February",
          "March",
          "April",
          "May",
          "June",
          "July",
          "August",
          "September",
          "October",
          "November",
          "December"
        ],
      }
    });

    $("#txtApptDate").on("apply.daterangepicker", function (ev, picker) {
      $("#txtApptDate2").val("");

      SetAppointmentDate2();

    });

    $(".single-date.appointment-date").on('apply.daterangepicker', function (ev, picker) {
      $(this).val(picker.startDate.format('DD/MM/YYYY'));
    });
  }

  function SetAppointmentDate2() {
    console.log("qwewqe");
    var days = $("#ddlHospital option:selected").data('abdc');
    if (days == "" || days == null) {
      $(".appointment-date#txtApptDate2").daterangepicker({
        autoApply: true,
        autoUpdateInput: false,
        minDate: tommorow,
        singleDatePicker: true,
        isInvalidDate: function (date) {
          var currDate = moment(date._d).format("DD/MM/YYYY");

          return ([$("#txtApptDate").val()].indexOf(currDate) != -1) || date.day() == 0 || date.day() == 6;
        },
        locale: {
          cancelLabel: "Clear Dates",
          daysOfWeek: ["S", "M", "T", "W", "T", "F", "S"],
          monthNames: [
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December",
          ],
        },
      });


    }
    else {
      var holidays = $("#ddlHospital option:selected").data("holidays");
      if (holidays) {
        holidays = holidays.split(',').map(function (item) {
          return item.trim();
        });
      }
      else {
        holidays = [];
      }

      var minDate = GetAdvanceBookingDayAvailable(days);

      $(".appointment-date#txtApptDate2").daterangepicker({
        autoApply: true,
        autoUpdateInput: false,
        singleDatePicker: true,
        isInvalidDate: function (date) {
          var thisMonth = date._d.getMonth() + 1;
          if (thisMonth < 10) {
            thisMonth = "0" + thisMonth;
          }
          var thisDate = date._d.getDate();
          if (thisDate < 10) {
            thisDate = "0" + thisDate;
          }
          var thisYear = date._d.getYear() + 1900;

          var thisCompare = thisMonth + "/" + thisDate + "/" + thisYear;

          var currDate = moment(date._d).format("DD/MM/YYYY");
          var firstApptDate = [$("#txtApptDate").val()].indexOf(currDate) != -1;

          if ($.inArray(thisCompare, holidays) != -1) {
            return true || firstApptDate;
          }
          return (date.day() == 0 || firstApptDate);

        },
        minDate: new moment().add(minDate.daysAdd, 'day'),
        maxDate: new moment().add(3, 'month'),
        startDate: new moment().add(minDate.daysAdd, 'day'),
        endDate: new moment().add(minDate.daysAdd, 'day'),
        locale: {
          cancelLabel: "Clear Dates",
          daysOfWeek: ["S", "M", "T", "W", "T", "F", "S"],
          monthNames: [
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December",
          ],
        },
      });


    }

    $(".appointment-date#txtApptDate2").on('apply.daterangepicker', function (ev, picker) {
      $(this).val(picker.startDate.format('DD/MM/YYYY'));
    });
  }

  function generateDoctorNameInDDL(doc) {
    $(".doctor-appt").remove();

    for (let index = 0; index < doc.value.length; index++) {
      $("#ddlDoctors").append(
        '<option value="' +
        doc.value[index].Name +
        '" class="doctor-appt">' +
        doc.value[index].Salutation +
        " " +
        doc.value[index].Name +
        "</option>"
      );
    }
  }

  function generateSpecialtiesInDDL(doc) {
    var specialties = [];

    for (let index = 0; index < doc.value.length; index++) {
      specialties.push(doc.value[index].MainSpecialtyName);

      var subspec = doc.value[index].SubSpecialtyName.split(",, ");

      subspec.forEach(function (subs) {
        specialties.push(subs);
      });
    }

    var filteredSpecialties = specialties.filter(function (spec, index) {
      return specialties.indexOf(spec) == index;
    });

    function removeItemAll(arr, value) {
      var i = 0;
      while (i < arr.length) {
        if (arr[i] === value) {
          arr.splice(i, 1);
        } else {
          ++i;
        }
      }
      return arr;
    }

    removeItemAll(filteredSpecialties, "");
    filteredSpecialties.sort();

    $(".specialty-appt").remove();

    filteredSpecialties.forEach(function (spec) {
      $("#ddlSpecialty").append(
        '<option value="' +
        spec +
        '" class="specialty-appt">' +
        spec +
        "</option>"
      );
    });
  }

  function filterSpecialtyInDDL() {
    var hospitalName = encodeURIComponent($("#ddlHospital").val());
    // Creating Our XMLHttpRequest object
    var xhr = new XMLHttpRequest();

    // Making our connection

    if (currentCulture == "id") {
      var url =
        "/api/default/ihhdoctors?$filter=contains(HospitalName,%27" +
        hospitalName +
        "%27)%20&$select=SubSpecialtyName,MainSpecialtyName,Name,Salutation&$orderby=Name&sf_culture=id";
    }
    else {
      var url =
        "/api/default/ihhdoctors?$filter=contains(HospitalName,%27" +
        hospitalName +
        "%27)%20&$select=SubSpecialtyName,MainSpecialtyName,Name,Salutation&$orderby=Name";
    }

    xhr.open("GET", url, true);

    // function execute after request is successful
    xhr.onreadystatechange = function () {
      if (this.readyState == 4 && this.status == 200) {
        var json = JSON.parse(this.responseText);
        generateSpecialtiesInDDL(json);
        generateDoctorNameInDDL(json);
      }
    };

    // Sending our request
    xhr.send();
  }

  function filterDoctorInDDL() {
    var specialty = encodeURIComponent($("#ddlSpecialty").val());
    var hospitalName = encodeURIComponent($("#ddlHospital").val());
    // Creating Our XMLHttpRequest object
    var xhr = new XMLHttpRequest();

    // Making our connection


    if (currentCulture == "id") {
      var url =
        "/api/default/ihhdoctors?$filter=contains(HospitalName,%27" +
        hospitalName +
        "%27)%20and(%20contains(MainSpecialtyName,%27" +
        specialty +
        "%27)%20or%20contains(SubSpecialtyName,%27" +
        specialty +
        "%27))%20&$select=Name,Salutation&$orderby=Name&sf_culture=id";
    }
    else {
      var url =
        "/api/default/ihhdoctors?$filter=contains(HospitalName,%27" +
        hospitalName +
        "%27)%20and(%20contains(MainSpecialtyName,%27" +
        specialty +
        "%27)%20or%20contains(SubSpecialtyName,%27" +
        specialty +
        "%27))%20&$select=Name,Salutation&$orderby=Name";
    }

    xhr.open("GET", url, true);

    // function execute after request is successful
    xhr.onreadystatechange = function () {
      if (this.readyState == 4 && this.status == 200) {
        var json = JSON.parse(this.responseText);
        generateDoctorNameInDDL(json);
      }
    };

    // Sending our request
    xhr.send();
  }

  if (window.location.href.split("/").pop() == "appointment") {
    filterSpecialtyInDDL();
    filterDoctorInDDL();
    selects();
  }

  $("#ddlSpecialty").on("change", function () {
    console.log(this.value);
    filterDoctorInDDL(this.value);
    selects();
  });

  $("#ddlHospital").on("change", function () {
    console.log(this.value);
    filterSpecialtyInDDL();
    filterDoctorInDDL();
    selects();
    SetAdvanceBookingDays($("#ddlHospital option:selected").data('abdc'))
  });


  //file attach
  function uploadHealthReport() {
    return new Promise((resolve, reject) => {
      var url = window.location.origin + "/api/default/documents";
      var fileName = $("#attachefile")[0].files[0].name;
      var loadDocData = JSON.stringify({
        Title: fileName,
        IncludeInSitemap: false,
        UrlName: md5(fileName + Date.now()),
        ParentId: "b56a6306-7e01-466b-a037-ee3baa39e09f",
      });

      //files info
      var formData = new FormData();
      formData.append("file", $("#attachefile")[0].files[0]);
      console.log(formData);
      getToken()
        .then((data) => {
          sitefinityUploadFile(url, "POST", formData, loadDocData)
            .then((success) => {
              console.log("uploadHealthReport Success!");
              console.log(success);
              healthReportUrl = window.location.origin + success.Url;
              healthReportID = success.Id;
              resolve(success);
            })
            .catch((failed) => {
              console.log("uploadHealthReport Failed");
              console.log(failed);
              reject(failed);
            });
        })
        .catch((err) => {
          console.log(err);
        });
    });
  }

  $("#attachefile").on("change", function () {
    var fileSize = $(this)[0].files[0].size / 1024 / 1024; // in MB
    if (fileSize > 10) {
      $(this).next().next(".select-error").css("display", "block");
      $("#attachefile").val(""); //clearing if invalid
      $('.custom-upload').addClass('error');
      if ($('.custom-upload').hasClass('uploaded')) {
        $('.custom-upload').removeClass('uploaded');
      }
    }
    else {
      $(this).next().next(".select-error").css("display", "none");
      $('.custom-upload').addClass('uploaded');
      $('.custom-upload').html('File Attached');
      if ($('.custom-upload').hasClass('error')) {
        $('.custom-upload').removeClass('error');
      }
    }
  });

  function getImageBase64() {
    return new Promise((resolve, reject) => {
      var imageFile = $("#attachefile")[0].files[0];
      var fileReader = new FileReader();

      fileReader.onload = () => {
        var image64 = fileReader.result.split(',')[1];
        resolve(image64);
      };
      fileReader.onerror = reject;
      fileReader.readAsDataURL(imageFile);
    });
  }

  function getSummary() {
    $("#hospitalTxt").text($("#ddlHospital").val().trim());
    $("#doctorTxt").text($("#ddlDoctors").val().trim());
    $("#specialtyTxt").text($("#ddlSpecialty").val().trim());
    $("#medicalConcernTxt").text($("#txtMedicalConcern").val().trim());

    $("#firstApptDateTxt").text($("#txtApptDate").val().trim());
    $("#firstApptTimeTxt").text($("#txtApptTime").val().trim());
    $("#secondApptDateTxt").text($("#txtApptDate2").val().trim());
    $("#secondApptTimeTxt").text($("#txtApptTime2").val().trim());

    $("#nameTxt").text($("#txtFullName").val().trim());
    $("#icPassportTxt").text($("#txtICorPassportNumber").val().trim());
    $("#nationalityTxt").text($("#txtNationality").val().trim());
    $("#genderTxt").text($("#txtGender").val().trim());

    $("#dobTxt").text($("#txtDateOfBirth").val().trim());
    $("#contactNumTxt").text($("#txtCountryCode").val().trim() + $("#txtContactNumber").val().trim());
    $("#contactNumTxt2").text($("#txtCountryCode2").val().trim() + $("#txtContactNumber2").val().trim());
    $("#emailTxt").text($("#txtEmail").val().trim());

  }

  function isAcknowledge() {
    var isAcknowledge = true;
    if ($("#contact").prop("checked") != true) {
      isAcknowledge = false;

    }

    if ($("#accurate").prop("checked") != true) {
      isAcknowledge = false;
    }

    if (!isAcknowledge) {
      $("#accurate").parent().next(".select-error").css("display", "block");
    }

    return isAcknowledge;
  }

  async function getData() {
    var agreeReceiveMessage = false;
    var consentCheckbox = false;


    if ($("#agree_receive_message").prop("checked") == true) {
      agreeReceiveMessage = true;
    }

    if ($("#consent_checkbox").prop("checked") == true) {
      consentCheckbox = true;
    }

    var image64 = "";
    if ($("#attachefile")[0].files[0] != null) {
      if ($("#attachefile")[0].files[0].type == "image/jpeg") {
        image64 = await getImageBase64();
      }
    }

    var parms = JSON.stringify({
      IncludeInSitemap: false,
      UrlName: md5(Math.random().toString(36).substring(2, 8) + Date.now()),
      FullName: $("#txtFullName").val().trim(),
      ICorPassportNumber: $("#txtICorPassportNumber").val().trim(),
      Nationality: $("#txtNationality").val().trim(),
      Email: $("#txtEmail").val().trim(),
      Gender: $("#txtGender").val().trim(),
      CountryCode: $("#txtCountryCode").val().trim(),
      ContactNumber: $("#txtContactNumber").val().trim(),
      CountryCode2: $("#txtCountryCode2").val().trim(),
      ContactNumber2: $("#txtContactNumber2").val().trim(),
      DateofBirth: $("#txtDateOfBirth").val().trim(),
      CurrentMedicalConditionsSymptoms: $("#txtMedicalConcern").val().trim(),
      FirstAppointmentDate: $("#txtApptDate").val().trim(),
      FirstAppointmentTime: $("#txtApptTime").val().trim(),
      SecondAppointmentDate: $("#txtApptDate2").val().trim(),
      SecondAppointmentTime: $("#txtApptTime2").val().trim(),
      AppointmentType: "Doctor appointment",
      DoctorName: $("#ddlDoctors").val().trim(),
      HospitalName: $("#ddlHospital").val().trim(),
      SpecialtyName: $("#ddlSpecialty").val().trim(),
      AgreeReceiveMessages: agreeReceiveMessage,
      AcknowledgePDP: consentCheckbox,
      AdditionalServices: "",
      Rating: 0,
      Language: $(".appointment").data("language"),
      ImageBase64: image64,
      HealthReportURL: healthReportUrl,
      HealthReportID: healthReportID,
      ParentId: $("#ddlHospital").find(":selected").data("hosid").trim(),
      SubmissionDateTime: new Date().toISOString(),

    });

    return parms;

  }

  $('#validation-button').on("click", function () {
    if (isFormValid()) {
      //$('#additionalServicesPopUp').trigger('click');
      $(".appointment-form").hide();
      getSummary();
      getData();
      $(".appointment-summary").show();
      $("html, body").animate({ scrollTop: 0 });
    }
  })

  $("#make-appointment").on("click", function () {
    $("#make-appointment").prop('disabled', true);
    $("#make-appointment").addClass("btn-disabled");

    if (isFormValid() && isAcknowledge()) {

      if ($("#attachefile")[0].files[0] != null && $("#attachefile")[0].files[0].type == "application/pdf") {
        uploadHealthReport().then(function () {
          getToken().then(async function () {
            $.ajax({
              url: "/api/default/appointments",
              method: "POST",
              dataType: "json",
              contentType: "application/json; charset=utf-8",
              data: await getData(),
              beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + accessToken);
                $('.appt-plswait').addClass('show');
              },
              success: function (data) {
                console.log("succesful update appt!");
                console.log(data);
                window.location.href = $("#ddlHospital").find(":selected").data("appturl") + "/thank-you";
              },
              error: function (err) {
                console.log(err.responseText);
              },
            });
          });
        });
      }
      else {
        getToken().then(async function () {
          $.ajax({
            url: "/api/default/appointments",
            method: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: await getData(),
            beforeSend: function (xhr) {
              xhr.setRequestHeader("Authorization", "Bearer " + accessToken);
              $('.appt-plswait').addClass('show');
            },
            success: function (data) {
              console.log("succesful update appt!");
              console.log(data);
              window.location.href = $("#ddlHospital").find(":selected").data("appturl") + "/thank-you";

            },
            error: function (err) {
              console.log(err.responseText);
              $('.appt-plswait').removeClass('show');
            },
          });
        });
      }
    }
    else {
      $("#make-appointment").prop('disabled', false);
      if ($("#make-appointment").hasClass("btn-disabled")) {
        $("#make-appointment").removeClass("btn-disabled");
      }
    }
  });

 
});
