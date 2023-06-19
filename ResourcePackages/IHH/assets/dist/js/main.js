
  function formTxtFieldShowErr(element, showErr, errMsg) {
    if (element != undefined) {
      if (showErr) {
        // display error message
        element.next(".select-error").css("display", "block");
        element.next().next(".select-error").css("display", "block");
        if (errMsg != undefined) {
          element.next(".select-error").html(errMsg);
          element.next().next(".select-error").html(errMsg);
        }
      } else {
        // hide error message
        element.next(".select-error").css("display", "none");
        element.next().next(".select-error").css("display", "none");
      }
    }
  }

  function countryCodeShowErr(element, showErr, errMsg) {
    if (element != undefined) {
      if (showErr) {
        // display error message
        element.parent().parent().next(".select-error").css("display", "block");
        if (errMsg != undefined) {
          element.parent().parent().next(".select-error").html(errMsg);
        }
      } else {
        // hide error message
        element.parent().parent().next(".select-error").css("display", "none");
      }
    }
  }

  function phoneNumShowErr(element, showErr, errMsg) {
    if (element != undefined) {
      if (showErr) {
        // display error message
        element.parent().next(".select-error").css("display", "block");
        if (errMsg != undefined) {
          element.parent().next(".select-error").html(errMsg);
        }
      } else {
        // hide error message
        element.parent().next(".select-error").css("display", "none");
      }
    }
  }

  function formTxtFieldCheckValidity(textFieldElement) {
    var validity = textFieldElement.val().length > 0;
    formTxtFieldShowErr(textFieldElement, !validity);
    return validity;
  }

  function emailValidity(email) {
    var regex =
      /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
    var validity = regex.test(email.val());
    formTxtFieldShowErr(email, !validity, "Invalid Email");
    return validity;
  }

  function countryCodeValidity(countryCode) {
    var regex = /^\+?(\d+)/;
    var validity = regex.test(countryCode.val());
    countryCodeShowErr(countryCode, !validity, "Invalid Country Code");


    return validity;
  }

  function phoneNumValidity(phoneNumber) {
    var regex = /^\d{9,11}$/;
    var validity = regex.test(phoneNumber.val());
    phoneNumShowErr(phoneNumber, !validity, "Invalid Phone Number");
    return validity;
  }

  // var tokenEndPoint = window.location.origin + "/Sitefinity/Authenticate/OpenID/connect/token";
  var tokenEndPoint = window.location.origin + "/sitefinity/oauth/token";

  var client_id = "postman";
  var client_secret = "secret";

  var accessToken;
  var refreshToken;

  function getToken() {
    var username = "test@gmail.com";
    var password = "test123";

    return new Promise((resolve, reject) => {
      $.ajax({
        url: tokenEndPoint,
        data: {
          username: username,
          password: password,
          grant_type: "password",
          //scope: "openid offline_access",
          client_id: client_id,
          client_secret: client_secret,
        },
        method: "POST",
        success: function (data) {
          resolve(data);
          console.log(data);
          console.log("getToken success");
          accessToken = data.access_token;
          refreshToken = data.refresh_token;
        },
        error: function (err) {
          reject(err);
          console.log("getToken failed");
          console.log(err);
        },
      });
    });
  }


//daterangepicker function
var tommorow = new Date();
tommorow.setDate(tommorow.getDate() + 4);

$(".appointment-date").daterangepicker({
  autoApply: true,
  autoUpdateInput: false,
  minDate: tommorow,
  singleDatePicker: true,
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



$(".single-date").daterangepicker({
  // drops: positionDrops,
  autoApply: true,
  autoUpdateInput: false,
  // parentEl: $('.scroller-inner'),

  minDate: new moment().add(2, "day"),
  // opens: Helper.isRTL()?'left':'right',
  singleDatePicker: true,
  locale: {
    // direction: Helper.isRTL()?'rtl':'ltr',
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

$(".single-date.appointment-date").daterangepicker({
  // drops: positionDrops,
  autoApply: true,
  autoUpdateInput: false,
  //disabling weekends
  isInvalidDate: function (date) {
    return date.day() == 0 || date.day() == 6;
  },
  minDate: new moment().add(1, "day"),
  maxDate: new moment().add(3, "month"),
  singleDatePicker: true,
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

$(".single-date.dob").daterangepicker({
  // drops: positionDrops,
  autoApply: true,
  autoUpdateInput: false,
  // parentEl: $('.scroller-inner'),
  minDate: new Date(1900, 1, 1, 0, 0, 0, 0), //new moment().add(-120, 'year'),
  maxDate: new Date(),
  // opens: Helper.isRTL()?'left':'right',
  singleDatePicker: true,
  showDropdowns: true,
  locale: {
    // direction: Helper.isRTL()?'rtl':'ltr',
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

$(document).on("click", ".single-date, .appointment-date", function (e) {
  e.preventDefault();
  e.stopPropagation();
  $(this).trigger("blur");
});

$(".single-date, .appointment-date").on(
  "showCalendar.daterangepicker",
  function (ev, picker) {
    // drops position logic
    if (
      picker.element.offset().top -
        $(window).scrollTop() +
        picker.container.outerHeight() >
      $(window).height()
    ) {
      picker.drops = "up";
    } else {
      picker.drops = "down";
    }

    if ($(this).hasClass("appointment-date")) {
      // Disable Weekends
      picker.container
        .find(".weekend")
        .removeClass("available")
        .addClass("disabled off");
    }
  }
);

$(".single-date, .appointment-date").on(
  "show.daterangepicker",
  function (ev, picker) {
    if (picker.element.parents(".datepicker-wrapper").length != 0) {
      picker.element.parents(".datepicker-wrapper").addClass("is-focus");
    }
  }
);
$(".single-date, .appointment-date").on(
  "hide.daterangepicker",
  function (ev, picker) {
    if (picker.element.parents(".datepicker-wrapper").length != 0) {
      picker.element.parents(".datepicker-wrapper").removeClass("is-focus");
    }
  }
);

$(".single-date, .appointment-date").on(
  "cancel.daterangepicker",
  function (ev, picker) {
    $(this).data("daterangepicker").setStartDate(new Date());
    $(this).data("daterangepicker").setEndDate(new Date());
    $(this).val("");
  }
);

$(".single-date.dob").on("cancel.daterangepicker", function (ev, picker) {
  $(this)
    .data("daterangepicker")
    .setStartDate(new moment().add(-150, "year"));
  $(this).data("daterangepicker").setEndDate(new Date());
  $(this).val("");
});

$(".single-date, .appointment-date").on(
  "apply.daterangepicker",
  function (ev, picker) {
    $(this).val(picker.startDate.format("DD/MM/YYYY"));
  }
);

//upload file
function sitefinityUploadFile(url, method, formData, sfPropertiesValue) {
  return new Promise((resolve, reject) => {
    $.ajax({
      url: url,
      method: method,
      processData: false,
      contentType: false,
      data: formData,
      beforeSend: function (xhr) {
        xhr.setRequestHeader("Authorization", "Bearer " + accessToken);
        xhr.setRequestHeader("X-Sf-Service-Request", "true");
        xhr.setRequestHeader(
          "X-File-Name",
          $("#attachefile")[0].files[0].name
        );
        xhr.setRequestHeader("X-Sf-Properties", sfPropertiesValue);
      },
      success: function (data) {
        resolve(data);
      },
      error: function (err) {
        reject(err);
        console.log(err);
      },
    });
  });
}

// function sitefinityUploadImage(url, method, requestData, sfPropertiesValue) {
//   return new Promise((resolve, reject) => {
//     $.ajax({
//       url: url,
//       method: method,
//       processData: false,
//       contentType: "image/jpeg",
//       data: JSON.stringify(requestData),
//       beforeSend: function (xhr) {
//         xhr.setRequestHeader("Authorization", "Bearer " + accessToken);
//         xhr.setRequestHeader("X-Sf-Service-Request", "true");
//         xhr.setRequestHeader(
//           "X-File-Name",
//           $("#attachefile")[0].files[0].name
//         );
//         xhr.setRequestHeader("X-Sf-Properties", sfPropertiesValue);
//         xhr.setRequestHeader("Content-Type", "image/jpeg");
//       },
//       success: function (data) {
//         console.log(data);
//         resolve(data);
//       },
//       error: function (err) {
//         console.log("TTTTTTTTT");
//         console.log(err);
//         reject(err);
//       },
//     });
//   });
// }
