
jQuery(function () {

    $('.checklist .checklist-button').on('click', function (e) {

        // var w = window.open('/assets/checklist.png');
        // w.print();
        // setTimeout(function () {
        //     w.close();
        // }, 25)
        printChecklist();

    });

    $('.region-selector').select2({
        width: 'style',
        minimumResultsForSearch: -1,
        dropdownPosition: 'below',

    });

    $('.checklist-header').css("content_security_policy", "default-src 'self' style-src 'self' 'unsafe-inline'");
    $('.checklist-item-name').css("content_security_policy", "default-src 'self' style-src 'self' 'unsafe-inline'");
    $('.checklist-item-status').css("content_security_policy", "default-src 'self' style-src 'self' 'unsafe-inline'");


    $('.checklist-header').css('font-family', 'WorkSans, Arial, simsum');
    $('.checklist-item-name').css('font-family', 'WorkSans, Arial, simsum');

    $('.checklist-item').css('cursor', 'pointer');

    $('.checklist-header').css('margin-bottom', '1.5rem');
    $('.checklist-header').css('font-weight', '600');

    $('.checklist-header').css('color', 'rgb(98, 181, 221)');

    $('.checklist-item-status').css("width", "1.5rem");
    $('.checklist-item-status').css("text-align", "center");
    $('.checklist-item-status').css("vertical-align", "top");
    $('.checklist-item-status').css("font-weight", "600");
    $('.checklist-item-status').css("font-size", "1rem");

    $('.checklist-item-name').css('padding-bottom', '0.5rem');

    $('.checklist-item-status').html('🔲');
    // $('.checklist-item-status').html('✅');
    $('.checklist-item-status').css('opacity', '0.5');


    $('.checklist-item').on('click', function () {
        if ($(this).children('.checklist-item-status').html() == "🔲") {
            $(this).children('.checklist-item-status').html('✅');
            $(this).children('.checklist-item-status').css('opacity', '1');
        }
        else if ($(this).children('.checklist-item-status').html() == "✅") {
            $(this).children('.checklist-item-status').html('🔲');
            $(this).children('.checklist-item-status').css('opacity', '0.5');
        }
    })

    $('.airport-transfer-actions-buttons').on('click', function(){

        if($(this).hasClass('call-us')) 
        {
            $('.choose-hospital-overlay.call-us').addClass('show');
        }
        else if($(this).hasClass('email-us'))
        {
            $('.choose-hospital-overlay.email-us').addClass('show');
        }
       
    })

    $('.choose-hospital-overlay .close-overlay').on('click', function(){
        $('.choose-hospital-overlay').removeClass('show');
    })


})


var di = '   <div style="margin-top: 1rem; margin-bottom: 0.5rem;" class="di">Notes: </div>        <div style="border: 1px solid black; width: 500px; height: 350px;" class="di"></div>';

function printChecklist() {
    $('.checklist-button').css('display', 'none');
    $('.checklist-header').css('font-size', '1.325rem');

    $('.checklist-item-name').css('padding-top', '0.25rem');

    $('.checklist-button').after(di);


    var divContents = "<div id='printCheck' style='width: fit-content; padding: 1rem; '> "
    divContents += $('.checklist').html();
    divContents += '</div>'

    $('.checklist-button').css('display', '');
    $('.checklist-header').css('font-size', '');
    $('.checklist-item-name').css('padding-top', '');

    $('.di').remove();

    // var a = window.open(' ', 'Checklist');
    // a.document.write('<html>');
    // a.document.write('<body >');
    // a.document.write(divContents);
    // a.document.write('</body></html>');
    // a.document.close();
    // a.print();
    $("body").after(divContents);

    // html2canvas(divContents, {
    //     onrendered: function (canvas) {
    //         var myImage = canvas.toDataURL();
    //         downloadURI(myImage, "MaSimulation.png");
    //     }
    // });
    // a.close();

    html2canvas(document.querySelector("#printCheck")).then(canvas => {
        $('#printCheck').remove();
        saveAs(canvas.toDataURL(), 'checklist.png');
    });


}

function saveAs(uri, filename) {

    var link = document.createElement('a');

    if (typeof link.download === 'string') {

        link.href = uri;
        link.download = filename;

        //Firefox requires the link to be in the body
        document.body.appendChild(link);

        //simulate click
        link.click();

        //remove the link when done
        document.body.removeChild(link);

    } else {
        window.open(uri);

    }
}