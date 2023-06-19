jQuery(() => {
  let language_prefix = "";

  if (window.location.href.includes("/id/")) {
    language_prefix = "/id";
  }

  function checkItem() {
    if ($("#search-result-dropdown").find("div").length == 0) {
      $(".no-result").show();
    } else {
      $(".no-result").hide();
    }
  }

  function generateHospitalNameInDDL(data) {
    $(".search-result-hospital").remove();

    var length = 5;

    if (data.length < 5) {
      length = data.length;
    }

    if (length > 0) {
      $("#search-result-dropdown").append(
        '<div class="search-result-hospital  search-items search-items-title">Hospitals</div>'
      );
      for (let i = 0; i < length; i++) {
        var hosUrl = "#";
        switch (data[i].UrlName) {
          case "pantai-hospital-ayer-keroh":
            hosUrl = "/pantai/ayer-keroh";
            break;
          case "pantai-hospital-kuala-lumpur":
            hosUrl = "/pantai/kuala-lumpur";
            break;
          case "pantai-hospital-penang":
            hosUrl = "/pantai/penang";
            break;
          case "prince-court-medical-centre":
            hosUrl = "/princecourt-medicalcentre";
            break;
          case "gleneagles-hospital-kota-kinabalu":
            hosUrl = "/gleneagles/kota-kinabalu";
            break;
          case "gleneagles-hospital-kuala-lumpur":
            hosUrl = "/gleneagles/kuala-lumpur";
            break;
          case "gleneagles-hospital-medini-johor":
            hosUrl = "/gleneagles/medini-johor";
            break;
          case "gleneagles-hospital-penang":
            hosUrl = "/gleneagles/penang";
            break;
        }
        $("#search-result-dropdown").append(
          '<a href="' +
            language_prefix +
            hosUrl +
            '" class="search-result-hospital  search-items">' +
            data[i].Title +
            "</a>"
        );
      }
    }
  }

  function generateDoctorNameInDDL(data) {
    $(".search-result-doctor").remove();

    var length = 5;

    if (data.length < 5) {
      length = data.length;
    }

    if (length > 0) {
      $("#search-result-dropdown").append(
        '<div class="search-result-doctor  search-items search-items-title">Doctors</div>'
      );
      for (let i = 0; i < length; i++) {
        $("#search-result-dropdown").append(
          '<a href="' +
            language_prefix +
            data[i].ProfileUrl +
            '" class="search-result-doctor  search-items">' +
            data[i].Salutation +
            " " +
            data[i].Name +
            "</a>"
        );
      }
    }
  }

  function generateArticleInDDL(data) {
    $(".search-result-article").remove();

    var length = 5;

    if (data.length < 5) {
      length = data.length;
    }

    if (length > 0) {
      $("#search-result-dropdown").append(
        '<div class="search-result-article search-items search-items-title">Articles</div>'
      );
      for (let i = 0; i < length; i++) {
        $("#search-result-dropdown").append(
          '<a href="' +
            language_prefix +
            "/articles/" +
            data[i].UrlName +
            '" class="search-result-article  search-items">' +
            data[i].Title +
            "</a>"
        );
      }
    }
  }

  function generateSpecialtyInDDL(data) {
    $(".search-result-spec").remove();

    var length = 5;

    if (data.length < 5) {
      length = data.length;
    }

    if (length > 0) {
      $("#search-result-dropdown").append(
        '<div class="search-result-spec  search-items search-items-title">Specialty</div>'
      );
      for (let i = 0; i < length; i++) {
        $("#search-result-dropdown").append(
          '<a href="' +
            language_prefix +
            "/medical-specialties/" +
            data[i].UrlName +
            '" class="search-result-spec  search-items">' +
            data[i].Title +
            "</a>"
        );
      }
    }
  }

  function callHospitaApi(inputquery) {
    var xhr = new XMLHttpRequest();

    var input = encodeURIComponent(inputquery);

    var url =
      "/api/default/hospitals1?$filter=contains(Title,%27" +
      input +
      "%27)&$select=Title,UrlName&$orderby=Title";

    xhr.open("GET", url, true);

    // function execute after request is successful
    xhr.onreadystatechange = function () {
      if (this.readyState == 4 && this.status == 200) {
        var json = JSON.parse(this.responseText);
        generateHospitalNameInDDL(json.value);
      }
    };

    // Sending our request
    xhr.send();
  }

  function callDoctorApi(inputquery) {
    var xhr = new XMLHttpRequest();

    var input = encodeURIComponent(inputquery);

    var url =
      "/api/default/ihhdoctors?$filter=contains(Name,%27" +
      input +
      "%27)&$select=Name,Salutation,ProfileUrl&$orderby=Name";

    xhr.open("GET", url, true);

    // function execute after request is successful
    xhr.onreadystatechange = function () {
      if (this.readyState == 4 && this.status == 200) {
        var json = JSON.parse(this.responseText);
        generateDoctorNameInDDL(json.value);
      }
    };

    // Sending our request
    xhr.send();
  }

  function callArticleApi(inputquery) {
    var xhr = new XMLHttpRequest();

    var input = encodeURIComponent(inputquery);

    var url =
      "/api/default/articles?$filter=contains(Title,%27" +
      input +
      "%27)&$select=Title,UrlName&$orderby=Title";

    xhr.open("GET", url, true);

    // function execute after request is successful
    xhr.onreadystatechange = function () {
      if (this.readyState == 4 && this.status == 200) {
        var json = JSON.parse(this.responseText);
        generateArticleInDDL(json.value);
      }
    };

    // Sending our request
    xhr.send();
  }

  function callSpecialtyApi(inputquery) {
    var xhr = new XMLHttpRequest();

    var input = encodeURIComponent(inputquery);

    var url =
      "/api/default/specialities?$filter=IsFeatured%20eq%20true%20and%20contains(Title,%27" +
      input +
      "%27)&$select=Title,UrlName&$orderby=Title";

    xhr.open("GET", url, true);

    // function execute after request is successful
    xhr.onreadystatechange = function () {
      if (this.readyState == 4 && this.status == 200) {
        var json = JSON.parse(this.responseText);
        generateSpecialtyInDDL(json.value);
      }
    };

    // Sending our request
    xhr.send();
  }

  function debounce(func, wait) {
    var timeout;
    return function () {
      var context = this,
        args = arguments;
      clearTimeout(timeout);
      timeout = setTimeout(function () {
        func.apply(context, args);
      }, wait);
    };
  }

  var ajax = function () {
      $(".no-result").hide();
      q = $(this).val();

      if ((q != "") & (q.length > 2)) {
        callHospitaApi(q);
        callDoctorApi(q);
        callSpecialtyApi(q);
        callArticleApi(q);
        setTimeout(function () {
          checkItem();
        }, 500);
      } else {
        $(".search-result-hospital").remove();
        $(".search-result-doctor").remove();
        $(".search-result-article").remove();
        $(".search-result-spec").remove();
      }
    },
    query = debounce(ajax, 500);

  $("#search").on("keyup", query);
});
