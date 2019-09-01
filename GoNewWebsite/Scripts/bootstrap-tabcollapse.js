/* accordion.js */

function getAccordion(element_id, screen) {
  //  $(window).resize(function () { location.reload(); });

    if ($(window).width() < screen) {
        var concat = '';
        obj_tabs = $(element_id + " li").toArray();
        obj_cont = $(".tab-content .tab-pane").toArray();
        jQuery.each(obj_tabs, function (n, val) {
            //if (n === 0) {
            //    concat += '<div id="' + n + '" class="panel panel-default">';
            //    concat += '<div class="panel-heading" role="tab" id="heading' + n + '">';

            //    concat += '<h4 class="panel-title"><a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapse' + n + '" aria-expanded="true" aria-controls="collapse' + n + '">' + val.innerText + ' </a></h4>';
            //    concat += '</div>';
            //    concat += '<div id="collapse' + n + '" class="panel-collapse in collapse show" role="tabpanel" aria-labelledby="heading' + n + '">';
            //    concat += '<div class="panel-body">' + obj_cont[n].innerHTML + '</div>';
            //    concat += '</div>';
            //    concat += '</div>';
            //} else {
             
            //}
            concat += '<div id="' + n + '" class="panel panel-default">';
            concat += '<div class="panel-heading" role="tab" id="heading' + n + '">';

            concat += '<h4 class="panel-title"><a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapse' + n + '" aria-expanded="false" aria-controls="collapse' + n + '">' + val.innerText + '</a></h4>';
            concat += '</div>';
            concat += '<div id="collapse' + n + '" class="panel-collapse collapse" role="tabpanel" aria-labelledby="heading' + n + '">';
            concat += '<div class="panel-body">' + obj_cont[n].innerHTML + '</div>';
            concat += '</div>';
            concat += '</div>';

            
        });
        $("#accordion").html(concat);
        $("#accordion").find('.panel-collapse:first').addClass("in");
        $("#accordion").find('.panel-title a:first').attr("aria-expanded", "true");
        $(element_id).remove();
        $(".tab-content").remove();

        $('.panel-collapse').on('show.bs.collapse', function () {
            $(this).siblings('.panel-heading').addClass('active');
        });

        $('.panel-collapse').on('hide.bs.collapse', function () {
            $(this).siblings('.panel-heading').removeClass('active');
        });

        //$(".pdsa-panel-toggle").
        //    addClass("glyphicon glyphicon-chevron-down");

        //var list = $(".in");
        //for (var i = 0; i < list.length; i++) {
        //    $($("a[href='#" + $(list[i]).attr("id") +
        //            "']")).next()
        //        .removeClass("glyphicon glyphicon-chevron-down")
        //        .addClass("glyphicon glyphicon-chevron-up");
        //}



   

    }

    //function toggleGlyphs(ctl) {
    //    if ($(ctl).hasClass("glyphicon glyphicon - chevron - up")) {
    //        $(ctl).removeClass("glyphicon glyphicon - chevron - up");
    //        $(ctl).addClass("glyphicon glyphicon - chevron - down");
    //    }
    //    else {
    //        $(ctl).removeClass("glyphicon glyphicon - chevron - down");
    //        $(ctl).addClass("glyphicon glyphicon-chevron-up");
    //    }
    //}
}
