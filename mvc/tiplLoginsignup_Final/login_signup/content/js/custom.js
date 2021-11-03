$(function() {
    var header = $(".header");
    $(window).scroll(function() {    
        var scroll = $(window).scrollTop();
    
        if (scroll >= 50) {
            header.removeClass('header').addClass("header-sticky");
        } else {
            header.removeClass("header-sticky").addClass('header');
        }
    });
});

$(document).ready(function() {
              $('#features-slide').owlCarousel({
                loop: false,
                margin: 30,
				dots: false,
                responsiveClass: true,
				autoplay:false,
				autoplayTimeout:5000,
				autoplayHoverPause:false,
				animateOut: 'fadeOut',
  			 	nav: true,
                responsive: {
                  0: {
                    items: 1
                  },
                  600: {
                    items: 2
                  },
                  1000: {
                    items: 3
				  }
                }
              })
            })


$(document).ready(function(){
  $(".filters").click(function(){
    $(".left-filter").toggle();
  });
});
