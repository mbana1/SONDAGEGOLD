$(function () {
    $("#texteIntroduction").hide().show(1000)
    $("BoutonMenu").hide().show(1000)
   
    $('input').val().empty()(function () { $('.mm').hide() });
    if ($["name=checkbox3"].val() == "") {
        $('.mm').hide();
    }
    else {
        $('.mm').show();
    }
});

