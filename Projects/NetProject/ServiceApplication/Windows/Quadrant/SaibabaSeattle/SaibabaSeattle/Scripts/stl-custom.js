/* Gobal Varibles */

var slPageY, slBodyTop, slideBanner = 1,
    samadhiYoutubeH, playbtnPos, stlNav, randNum,stlDay,stlMonth,stlMonths,stlCrtMonth,stlYear,
    mapH, admnSlImgH, stlScreenWidth, stlNextMonth, stlCrtNextMonth, crtDate, stlNextDate, eventFullDate, eventDate;

/* When Page Load run all functions */

$(document).ready(function() {
    $("#go-up").on("click", goUp)
    $("#stl-read-more").on("click", showHomeMore);
    $("#stl-hide-more").on("click", hideHomeMore);
    $("#gggb-read-more-one").on("click", showGgbMoreOne);
    $("#gggb-hide-more-one").on("click", hideGgbMoreOne);
    $(".closebtn").on("click", closeNav);
    $(".side-nav-btn").on("click", openNav);
    $(".stl-refresh-btn").on("click", randomNumRef);
    adminConsoleTab();
    slideShow();
    latestNewsShow();
    samadhiYoutubeHeight();
    adressSetMiddile();
    stlPanel();
    facyeffect();
    stlTab();
    playbtnPositon();
    randomNumRef();
  
    adminDelBtnPostion();
   

});

/* When Page resize run all functions */

$(window).resize(function() {
    samadhiYoutubeHeight();
    adressSetMiddile();
    adminDelBtnPostion();
    playbtnPositon();
});

/* When Page Scroll run all functions */

$(document).scroll(function() {
    goTop();
    navBarTop();
});
function goTop() {
    slPageY = window.pageYOffset;
    slBodyTop = $(".stl-main-ctnr").offset().top;
    if (slPageY >= slBodyTop) {
        $("#go-up").addClass("show");
    } else {
        $("#go-up").removeClass("show");
    }
}

function navBarTop() {
    slPageY = window.pageYOffset;
    stlScreenWidth = window.innerWidth;
    stlNav = $(".stl-slider-ctnr").offset().top;
    if (slPageY >= stlNav && stlScreenWidth>=700) {
        $(".stl-nav-ctnr").addClass("stl-nav-fixed");
    } else {
        $(".stl-nav-ctnr").removeClass("stl-nav-fixed");
    }
}

/* Saibaba webpage Functions */
$(".g-recaptcha div").css("margin", "auto")
function goUp() {
    $("html,body").animate({
        scrollTop: 0
    }, 300);
}

function stlCustomTable() {
    
    stlDay = new Date();
    stlDate = 0 + stlDay.getDate();
    stlNextDate = 1 + stlDay.getDate();
   
    
    stlMonth = stlDay.getMonth();
  
    stlNextMonth = stlMonth + 1;
 
    stlYear = stlDay.getFullYear();
    stlMonths = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];    
    stlCrtMonth = stlMonths[stlMonth];
    stlCrtNextMonth = stlMonths[stlNextMonth];
    crtDate = stlMonths[stlMonth] + "-" + stlDate + "-" + stlYear;
    
    $(".stl-custum-td:nth-child(2)").addClass("stl-well-cl2");

    $(".stl-well").each(function (index, item) {

        var arr = $(this).children();
        var lastText = $(arr[2]).text();
        var firstText = $(arr[0]).text();
        var lastPelement = $(".stl-custum-td:nth-child(3):eq(" + index + ")").children()[0];
        $(lastPelement).hide();
        $(".stl-custum-td:nth-child(2):eq(" + index + ")").append('<p>' + lastText + '</p>');
        $(".stl-custum-td:nth-child(2):eq(" + index + ") p:eq(0)").prepend('<span class="stl-icon-pstn"><i class="ion ion-ios-calendar-outline"></i></span>' + " " + " ");
        $(".stl-custum-td:nth-child(2):eq(" + index + ") p:eq(1)").prepend('<span class="stl-icon-pstn"><i class="ion ion-ios-time-outline"></i></span>' + " " + " ");
        //$(".stl-custum-td:nth-child(3):eq(" + index + ")").append('<p title="Add to Calendar" class="addeventatc">' + '<span class="start">12/17/2018 08:00 AM</span>' + '<span class="end">12/17/2018 08:00 AM</span>' + '<span class="timezone">America/Los_Angeles</span>' + '<span class="title">' + firstText + '</span> ' + ' <span class="description">Description of the event</span> ' +
        //   '<span class= "location">Location of the event</span>' + 'Event Remainder' + '</p>');
        //eventCreate();
        var lastPelement = $(".stl-custum-td:nth-child(3)").children()[0];
        $(lastPelement).hide();
        $(".stl-custum-td:nth-child(2):eq(" + index + ") p:eq(0)").each(function () {
           
            eventFullDate = $(this).text();
            eventDate = parseInt(eventFullDate.substring(6, 8));
      
            if ($(this).text().includes(stlCrtMonth) && eventDate >= stlDate || ($(this).text().includes(stlCrtNextMonth))) {
                $(this).parents(".stl-well").show();
                $(this).parents(".stl-well").addClass("event-active");
            }
           
            else {
                $(this).parents(".stl-well").hide();
            }

        });
        $(".stl-custum-td:nth-child(2):eq(" + index + ") p:eq(0)").each(function () {

            if ($(this).text().includes(stlCrtMonth)) {
               
                $(this).parents(".stl-well").addClass("event-active");
            }
            else {
                
                $(this).parents(".stl-well").removeClass("event-active");

            }

        });
    });
}

function adminDelBtnPostion() {
    admnSlImgH = $(".imageBlock img").height();
    $(".admn-img-delbtn").css({
        "top": -admnSlImgH + "px"
    });

}

function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

function latestNewsShow() {
    $('#latest-news').modal('show')
}

function showHomeMore() {
    $(".home-more").fadeIn();
    $(this).hide();
}

function hideHomeMore() {
    $(".home-more").fadeOut();
    $("#stl-read-more").fadeIn();
}

function showGgbMoreOne() {
    $(".gggb-more-one").fadeIn();
    $(this).hide();
}

function hideGgbMoreOne() {
    $(".gggb-more-one").fadeOut();
    $("#gggb-read-more-one").fadeIn();
}

function slideShow() {
    $('#fade').cycle();
}

function facyeffect() {
    $(".fancybox").fancybox({
        openEffect: "none",
        closeEffect: "none"
    });
}

function samadhiYoutubeHeight() {
    samadhiYoutubeH = $(".sai-samadhi-img").height();
    $(".stl-youtube-two").css({
        "height": samadhiYoutubeH + "px"
    })
}

function adressSetMiddile() {
    mapH = $(".stl-map-ctnr").height();
    $(".stl-adrs-ctnt").css("height", mapH + "px")
}

function stlPanel() {
    $('.collapse.in').prev('.panel-heading').addClass('active');
    $('#accordion, #bs-collapse')
        .on('show.bs.collapse', function(a) {
            $(a.target).prev('.panel-heading').addClass('active');
        })
        .on('hide.bs.collapse', function(a) {
            $(a.target).prev('.panel-heading').removeClass('active');
        });
}

function stlTab() {
    $(".stl-dn-tab-body").eq(0).show();
    $(".stl-dn-tab").each(function(index, item) {
        $(this).click(function() {
            $(".stl-dn-tab").removeClass("stl-dn-tab-active");
            $(".stl-dn-tab-body").hide();
            $(this).addClass("stl-dn-tab-active");
            $(".stl-dn-tab-body").eq(index).fadeIn();
        })
    })
}

function openNav() {
    $("#mySidenav").css({
        "width": 280 + "px",
        "paddingLeft": 15 + "px",
        "paddingRight": 15 + "px"
    })
    $(".side-nav-btn").css("opacity", 0);
}

function closeNav() {
    $("#mySidenav").css({
        "width": 0 + "px",
        "paddingLeft": 0 + "px",
        "paddingRight": 0 + "px"
    })
    $(".side-nav-btn").css("opacity", 1);
}

$('#pause').hide();
var audioElement = document.getElementById('saiAudio');
$('#play').click(function() {
    $('#play').hide();
    $('#pause').show();
    audioElement.play();
});

$('#pause').click(function() {
    $('#play').show();
    $('#pause').hide();
    audioElement.pause();
});

function playbtnPositon() {
    playbtnPos = $(window).height() / 2 - 40;
    $(".stl-audio").css("top", playbtnPos + "px")
    return false;
}
function randomNumRef() {
    randNum = Math.floor(Math.random() * 1000000);
    $(".stl-captch-code").text(randNum);
    return false;
}
function adminConsoleTab() {
    $(".admin-tab").eq(0).show();
    $(".admin-tab-btn").each(function (index, item) {
        $(this).click(function () {
            $(".admin-tab").hide();
            $(".admin-tab").eq(index).fadeIn();
        })
    })
}