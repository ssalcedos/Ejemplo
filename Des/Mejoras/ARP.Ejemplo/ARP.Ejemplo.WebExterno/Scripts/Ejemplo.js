/// <reference path="jquery-1.4.1-vsdoc.js" />
window.onresize = setContent;
function setLoading() {
    var w = $(window).width();
    var h = $(window).height();
    //$("div#Loading").animate({ 'opacity': 0.6 });
    //$("div#Loading").css("width", w);
    //$("div#Loading").css("height", h);
    w = w / 2;
    h = h / 2;
    $("div#Loading").css("padding-top", h - 60);
    //    $("div#Error").css("padding-left", w - 504);
    //    $("div#Error").css("padding-top", h - 168);
}
function setContent() {
    //Ajustar el panel que se muestra cuando se ejecuta una acción, se encarga de centrar la imagen de Procesando
    setLoading();
    //Arreglar las transparencias de los PNG en IE6
    $(document).pngFix();
    //Controlar el diseño de la plantilla contenedora general
    var height = $(window).height();
    var heightContent = height - 311; //es el alto del header + el alto del footer
    var heightMainContent = $("div#mainContent").height();
    if (heightMainContent > heightContent) {
        $("div#mainContent").css("height", heightMainContent);
    }
    else {
        $("div#mainContent").css("height", heightContent);
    }
    //Ocultar el div que tiene el listado de los perfiles del usuario
    $("div.perfiles_Lista").hide();
    //Controlar la ubicación del titulo H1
    //$(".tag").html($("h1"));
    //Controlar el div que muestra los perfiles del usuario
    $("#Perfiles").click(function (e) {
        e.preventDefault();
    });
    $("#Perfiles").mouseover(function () {
        if ($("div.perfiles_Lista").css("display") == "none") {
            $("div.perfiles_Lista").show("fast", function () {
                $("#Perfiles").css("background-color", "#ebf1f7");
                $("#Perfiles").css("color", "#1b0aa7");
                $("#Perfiles").css("padding", "4px");
            });
        }
    });
    $("div.info").mouseleave(function () {
        if ($("div.perfiles_Lista").css("display") == "block") {
            $("div.perfiles_Lista").hide("fast", function () {
                $("#Perfiles").css("background", "none");
                $("#Perfiles").css("color", "#fff");
            });
        }
    });
    //Quitar el efecto para los botones de los tabs principales, con esto se evita que se haga el href
    $(".btnContrato, .btnSede").parent().click(function (e) {
        e.preventDefault();
    });
}
function prettyPhotoConfiguration() {
    $("a[rel^='prettyPhoto']").prettyPhoto({
        animation_speed: 'fast', /* fast/slow/normal */
        slideshow: false, /* false OR interval time in ms */
        autoplay_slideshow: false, /* true/false */
        opacity: 0.60, /* Value between 0 and 1 */
        show_title: false, /* true/false */
        allow_resize: false, /* Resize the photos bigger than viewport. true/false */
        counter_separator_label: '/', /* The separator for the gallery counter 1 "of" 2 */
        theme: 'light_rounded', /* light_rounded / dark_rounded / light_square / dark_square / facebook */
        hideflash: false, /* Hides all the flash object on a page, set to TRUE if flash appears over prettyPhoto */
        wmode: 'transparent', /* Set the flash wmode attribute */
        autoplay: false, /* Automatically start videos: True/False */
        modal: true, /* If set to true, only the close button will close the window */
        overlay_gallery: true, /* If set to true, a gallery will overlay the fullscreen image on mouse over */
        keyboard_shortcuts: false, /* Set to false if you open forms inside prettyPhoto */
        changepicturecallback: function () { $("div.ppt").html($("div#pp_full_res iframe").contents().find("h2").html()); }, /* Called everytime an item is shown/changed */
        callback: function () { location.reload(); } /* Called when prettyPhoto is closed */
    });
}
function MostrarMensaje(tipoMensaje) {
    $.openPopupLayer({
        name: "msgform",
        url: "/ARP.AdminSeguridad.WebExterno/Mensaje.aspx?typeMsg=" + tipoMensaje,
        cache: false,
        afterClose: function () {
        }
    });
}
function MostrarVersionamiento() {
    $.openPopupLayer({
        name: "versionform",
        url: "/ARP.AdminSeguridad.WebExterno/Versiones.aspx",
        cache: false,
        afterClose: function () {
        }
    });
}
$(window).resize(function () {
    setContent();
});
$(document).ready(function () {
    setContent();
});

/*------------------- METODOS PARA INVOCACION DESPUES DE EJECUCION AJAX -------------------*/
function loadEvents() {
    prettyPhotoConfiguration();
    setContent();
}

//Este metodo pageLoad() se invoca en cada ejecución de ajax
function pageLoad() {
    $(function () {
        loadEvents();

    });
}
/*-----------------------------------------------------------------------------------------*/

function validarTextoAlfabetico(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 8) return true;
    patron = /[a-zA-ZÑñ  áéíóúAÉÍÓÚÑñ]/;
    te = String.fromCharCode(tecla);
    return patron.test(te);
}

function validarTextoAlfaNumerico(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 8) return true;
    patron = /[0-9a-zA-Z]/;
    te = String.fromCharCode(tecla);
    return patron.test(te);
}

function AjustarTamanoValidadores() {
    $(".validacion").click(function () {
        setContent();
    });
}

