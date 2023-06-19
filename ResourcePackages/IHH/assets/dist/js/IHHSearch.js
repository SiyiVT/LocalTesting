jQuery(()=>{

    $('.search-results-item').filter(function() {
        return $(this).children().length === 0;
      }).remove();

})
