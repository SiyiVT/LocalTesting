jQuery(()=>{
    if(window.location.hash != "")
    {
        var className = '.' + window.location.hash.substring(1);

        if($(className).length > 0)
        {
            $('html, body').animate({
                scrollTop: $(className).offset().top - $(".navbar").outerHeight()
            }, 300);
        }
    }
    
})