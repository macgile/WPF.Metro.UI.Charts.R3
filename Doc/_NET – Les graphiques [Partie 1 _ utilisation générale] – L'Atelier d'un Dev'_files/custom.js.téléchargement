jQuery(document).ready(function($){

  $('.widget li').has('ul').css('border-bottom', 'none');
  $('.flexslider').flexslider({
    animation: "slide",
    controlNav: true
  });

  $('.flex-recent-posts').flexslider({
    animation: "slide",
    animationLoop: false,
    controlNav: false,
    itemWidth: 292
    //itemMargin: 30
  });

  $('#service-tabs').tabs();

     /* Get the window's width, and check whether it is narrower than 480 pixels */
   var windowWidth = $(window).width();

   if (windowWidth <= 600) {

      /* Clone our navigation */
      var mainNavigation = $('.menu-all-pages-container').clone();

      /* Replace unordered list with a "select" element to be populated with options, and create a variable to select our new empty option menu */
      $('.menu-all-pages-container').html('<select class="menu"></select>');
      var selectMenu = $('select.menu');

      /* Navigate our nav clone for information needed to populate options */
      $(mainNavigation).children('ul').children('li').each(function() {

         /* Get top-level link and text */
         var href = $(this).children('a').attr('href');
         var text = $(this).children('a').text();

         /* Append this option to our "select" */
         $(selectMenu).append('<option value="'+href+'">'+text+'</option>');

         /* Check for "children" and navigate for more options if they exist */
         if ($(this).children('ul').length > 0) {
            $(this).children('ul').children('li').each(function() {

               /* Get child-level link and text */
               var href2 = $(this).children('a').attr('href');
               var text2 = $(this).children('a').text();

               /* Append this option to our "select" */
               $(selectMenu).append('<option value="'+href2+'">--- '+text2+'</option>');
            });
         }
      });
   }

   /* When our select menu is changed, change the window location to match the value of the selected option. */
   $(selectMenu).change(function() {
      location = this.options[this.selectedIndex].value;
   });

});

