function isFormValid() {
  var isValid = true;

  $(
    "#txtFullName, #txtNationality, #txtEmail, #txtContactNumber, #txtEnquiryMessage"
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

  if ($("#consent_checkbox").prop("checked") != true) {
    isValid= false;
    $("#consent_checkbox").parent().next().next(".select-error").css("opacity", 1);

  }
  else{
    $("#consent_checkbox").parent().next().next(".select-error").css("opacity", 0);
  }

  if ($("#agree_receive_message").prop("checked") != true && $("#not_agree_receive_message").prop("checked") != true) {
    isValid= false;
    $("#agree_receive_message").parent().next().next(".select-error").css("display", "block");
  }
  else
  {
      $("#agree_receive_message").parent().next().next(".select-error").css("display", "none");
  }
  return isValid;
}

function getData() {
    var agreeReceiveMessage = false;
    var consentCheckbox = false;
    if ($("#agree_receive_message").prop("checked") == true) {
      agreeReceiveMessage = true;
    }

    if ($("#consent_checkbox").prop("checked") == true) {
      consentCheckbox = true;
    }

    var parms = JSON.stringify({
      IncludeInSitemap: false,
      UrlName: md5(Math.random().toString(36).substring(2, 8) + Date.now()),
      FullName: $("#txtFullName").val().trim(),
      Nationality: $("#txtNationality").val().trim(),
      Email: $("#txtEmail").val().trim(),
      CountryCode: $("#txtCountryCode").val().trim(),
      ContactNumber: $("#txtContactNumber").val().trim(),
      HospitalName: $("#ddlHospital").data("hospital").trim(),
      AgreeReceiveMessages: agreeReceiveMessage,
      AcknowledgePDP: consentCheckbox,
      SubmissionDateTime: new Date().toISOString(),
      Rating: 0,
      Message: $("#txtEnquiryMessage").val().trim(),
    });

    return parms;
}

$(document).ready(function () {

  $("#agree_receive_message").on("change", function() {
    if ($(this).prop("checked")) {
        $("#not_agree_receive_message").prop("checked", false);
    }
  });

  $("#not_agree_receive_message").on("change", function() {
      if ($(this).prop("checked")) {
          $("#agree_receive_message").prop("checked", false);
      }
  });

  $('#enquiry-button').attr("disabled", true);

  $("#enquiry-button").on("click",function () {

    $("#enquiry-button").prop('disabled', true);
    $("#enquiry-button").addClass("btn-disabled");

    if (isFormValid()) {
      getToken().then(function () {
        $.ajax({
          url: "/api/default/enquiries",
          method: "POST",
          dataType: "json",
          contentType: "application/json; charset=utf-8",
          data: getData(),
          beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "Bearer " + accessToken);
          },
          success: function (data) {
            console.log("succesful update enquiry!");
            console.log(data);
            window.location.href =  window.location.href + "/thank-you";
          },
          error: function (err) {
            console.log("FAILLLLLLLLL enquiry");
            console.log(err.responseText);
            $(".enquiry-form").hide();
            $(".fail-submit").show();
          },
        });
      });
    }
    else{
      $("#enquiry-button").prop('disabled', false);
      if($("#enquiry-button").hasClass("btn-disabled"))
      {
        $("#enquiry-button").removeClass("btn-disabled");
      }
    }
  });
});
