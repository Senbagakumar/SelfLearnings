(function ($) {
    "use strict";

    var bC31OrgScore = [];
    var bC31OtherOrgScore = [];

    var uid = 0;
    uid = $("#lblUserId").val();

    $.ajax({
        type: "GET",
        url: API.OrganizationLevel3Report() + "/" + uid,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: OnSuccess31_,
        error: OnErrorCall31_
    });

    function OnSuccess31_(response) {
        var oo = response.OtherOrg;
        var og = response.Org;

        for (var i = 0; i < oo.length; i++) {
            bC31OtherOrgScore.push(oo[i]);
        }

        for (var i = 0; i < og.length; i++) {
            bC31OrgScore.push(og[i]);
        }
    }

    function OnErrorCall31_(response) {
        alert("Whoops something went wrong!");
    }

    //bar chart
    var ctx1 = document.getElementById("barChart31");
    // ctx1.height = 100;
    var myChart1 = new Chart(ctx1, {
        type: 'bar',

        data: {
            labels: ["group1", "group2", "group3", "group4", "group5", "group6", "group7", "group8", "group9"],
            datasets: [
                {

                    label: "organization Score",
                    data: bC31OrgScore,
                    borderColor: "rgba(0, 123, 255, 0.9)",
                    borderWidth: "0",
                    backgroundColor: "rgba(0, 123, 255, 0.5)"
                },
                {
                    label: "other organization Score",
                    data: bC31OtherOrgScore,
                    borderColor: "rgba(0,0,0,0.09)",
                    borderWidth: "0",
                    backgroundColor: "green"
                }
            ]
        },

        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        //beginAtZero: true
                        min: 0,
                        max: 100,
                        stepSize: 20

                        //steps: 100,
                        //stepValue: 100,
                        //max: 40 
                    }
                }]
            }
        }
    });


    var bC33OrgScore = [];
    var bC33OtherOrgScore = [];

    $.ajax({
        type: "GET",
        url: API.OrganizationLevel3FinalReport() + "/" + uid,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: OnSuccess33_,
        error: OnErrorCall33_
    });

    function OnSuccess33_(response) {
        var oo = response.OtherOrg;
        var og = response.Org;

        for (var i = 0; i < oo.length; i++) {
            bC33OtherOrgScore.push(oo[i]);
        }

        for (var i = 0; i < og.length; i++) {
            bC33OrgScore.push(og[i]);
        }
    }

    function OnErrorCall33_(response) {
        alert("Whoops something went wrong!");
    }

    //bar chart
    var ctx3 = document.getElementById("barChart33");
    //    ctx.height = 200;
    var myChart3 = new Chart(ctx3, {
        type: 'bar',
        data: {
            labels: ["Final Score"],
            datasets: [
                {

                    label: "organization Score",
                    data: bC33OrgScore,
                    borderColor: "rgba(0, 123, 255, 0.9)",
                    borderWidth: "0",
                    backgroundColor: "rgba(0, 123, 255, 0.5)"
                },
                {
                    label: "other organization Score",
                    data: bC33OtherOrgScore,
                    borderColor: "rgba(0,0,0,0.09)",
                    borderWidth: "0",
                    backgroundColor: "green"
                }
            ]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        min: 0,
                        max: 1000,
                        stepSize: 100
                    }
                }]
            }
        }
    });

    var bC34OrgScore = [];
    var bC34OtherOrgScore = [];

    $.ajax({
        type: "GET",
        url: API.SectorLevel3Report() + "/" + uid,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: OnSuccess34_,
        error: OnErrorCall34_
    });

    function OnSuccess34_(response) {
        var oo = response.OtherOrg;
        var og = response.Org;

        for (var i = 0; i < oo.length; i++) {
            bC34OtherOrgScore.push(oo[i]);
        }

        for (var i = 0; i < og.length; i++) {
            bC34OrgScore.push(og[i]);
        }
    }

    function OnErrorCall34_(response) {
        alert("Whoops something went wrong!");
    }

    //bar chart
    var ctx4 = document.getElementById("barChart34");
    // ctx4.height = 100;
    var myChart4 = new Chart(ctx4, {
        type: 'bar',
        data: {
            labels: ["group1", "group2", "group3", "group4", "group5", "group6", "group7", "group8", "group9"],
            datasets: [
                {
                    label: "organization individual Score",
                    data: bC34OrgScore,
                    borderColor: "rgba(0, 123, 255, 0.9)",
                    borderWidth: "0",
                    backgroundColor: "rgba(0, 123, 255, 0.5)"
                },
                {
                    label: "manufacure individual Score",
                    data: bC34OtherOrgScore,
                    borderColor: "rgba(0,0,0,0.09)",
                    borderWidth: "0",
                    backgroundColor: "green"
                }
            ]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        //beginAtZero: true
                        min: 0,
                        max: 100,
                        stepSize: 20
                    }
                }]
            }
        }
    });

    var bC35OrgScore = [];
    var bC35OtherOrgScore = [];

    $.ajax({
        type: "GET",
        url: API.SectorLevel3FinalReport() + "/" + uid,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: OnSuccess35_,
        error: OnErrorCall35_
    });

    function OnSuccess35_(response) {
        var oo = response.OtherOrg;
        var og = response.Org;

        for (var i = 0; i < oo.length; i++) {
            bC35OtherOrgScore.push(oo[i]);
        }

        for (var i = 0; i < og.length; i++) {
            bC35OrgScore.push(og[i]);
        }
    }

    function OnErrorCall35_(response) {
        alert("Whoops something went wrong!");
    }

    //bar chart
    var ctx5 = document.getElementById("barChart35");
    //    ctx.height = 200;
    var myChart5 = new Chart(ctx5, {
        type: 'bar',
        data: {
            labels: ["Final Score"],
            datasets: [
                {

                    label: "organization individual Score",
                    data: bC35OrgScore,
                    borderColor: "rgba(0, 123, 255, 0.9)",
                    borderWidth: "0",
                    backgroundColor: "rgba(0, 123, 255, 0.5)"
                },
                {
                    label: "manufacture individual Score",
                    data: bC35OtherOrgScore,
                    borderColor: "rgba(0,0,0,0.09)",
                    borderWidth: "0",
                    backgroundColor: "green"
                }
            ]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        min: 0,
                        max: 1000,
                        stepSize: 100
                    }
                }]
            }
        }
    });

})(jQuery);